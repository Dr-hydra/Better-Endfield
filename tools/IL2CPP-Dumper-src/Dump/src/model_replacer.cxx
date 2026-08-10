#define _CRT_SECURE_NO_WARNINGS

#include "../include/model_replacer.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include "../../third_party/minhook/include/MinHook.h"

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <string>
#include <unordered_map>
#include <unordered_set>
#include <utility>
#include <vector>

namespace {

    constexpr uintptr_t RVA_CACHED_LOAD_GAME_OBJECT = 0x04701AF0;
    constexpr uintptr_t RVA_I18N_LOAD_GAME_OBJECT = 0x0304D350;
    constexpr uintptr_t RVA_I18N_RESOLVE_PATH_HASH = 0x0304DB50;
    constexpr uintptr_t RVA_INIT_INITIAL_PATH_HASH = 0x0311B400;
    constexpr uintptr_t RVA_INIT_MAIN_PATH_HASH = 0x04994340;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_IS_INITIALIZED = 0x036D47E0;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_IS_ASYNC_INITING = 0x04CDB190;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_USING_BUNDLE = 0x03C8F590;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_CHECK_STRING = 0x0304B570;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_CHECK_HASH = 0x030492B0;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_RAW_CHECK_HASH = 0x0304A850;
    constexpr uintptr_t RVA_BUNDLE_LOAD_UNTRACKED_HASH = 0x03393F90;
    constexpr uintptr_t RVA_LOAD_ASSET_INTERNAL_HASH = 0x02FCE2B0;
    constexpr uintptr_t RVA_BUNDLE_LOADER_LOAD_ASSET = 0x02FCEE80;
    constexpr uintptr_t RVA_TRY_GET_ASSET_INFO_HASH = 0x02FCE680;
    constexpr uintptr_t RVA_UNTRACKED_HANDLE_GATE = 0x02EFB620;
    constexpr uintptr_t RVA_UNTRACKED_HANDLE_IS_VALID = 0x068AF788;
    constexpr uintptr_t RVA_ASSET_PROXY_HAS_ERROR = 0x02FCDBE0;
    constexpr uintptr_t RVA_ASSET_PROXY_FINISH_ERROR = 0x068C2D48;
    constexpr uintptr_t RVA_BUNDLE_PROXY_LOAD_SYNC = 0x02F67FE0;
    constexpr uintptr_t RVA_BUNDLE_PROXY_GET_FILE_PATH = 0x067DD024;
    constexpr uintptr_t RVA_BUNDLE_PROXY_LOAD_FILE = 0x067DD138;
    constexpr uintptr_t RVA_LOGIN_MANAGER_RELEASE_SCENE = 0x0380E1E0;
    constexpr uintptr_t RVA_LOGIN_SCENE_ROOT_ON_BIND = 0x0380F010;
    constexpr uintptr_t RVA_LOGIN_ANIM_CHANGE_STATE = 0x03408830;
    constexpr uintptr_t RVA_LOGIN_ANIM_RESET_A1 = 0x03D4F930;
    constexpr uintptr_t RVA_LOGIN_ANIM_PLAY_A1S = 0x03D4FD90;
    constexpr uintptr_t RVA_LOGIN_ANIM_PLAY_A1A2 = 0x03D4FB60;
    constexpr uintptr_t RVA_LOGIN_ANIM_TICK = 0x02F446B0;
    constexpr uintptr_t RVA_LOGIN_ANIM_RELEASE = 0x04994870;
    constexpr uintptr_t RVA_ASSET_PROXY_HANDLE_GET = 0x0360A4D0;
    constexpr uintptr_t RVA_INSTANTIATE_GAME_OBJECT_PARENT = 0x03E03C80;
    constexpr uintptr_t RVA_GAME_OBJECT_SET_ACTIVE = 0x0339E900;
    constexpr uintptr_t RVA_TRANSFORM_SET_PARENT = 0x03339FA0;
    constexpr uintptr_t RVA_GAME_OBJECT_GET_COMPONENTS_IN_CHILDREN = 0x04753620;
    constexpr uintptr_t RVA_RENDERER_SET_ENABLED = 0x0339A890;
    constexpr uintptr_t RVA_SKINNED_MESH_RENDERER_SET_BONES = 0x0B3CBFAC;
    constexpr uintptr_t RVA_SKINNED_MESH_RENDERER_SET_ROOT_BONE = 0x039C9B00;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_LOAD_HASH_TYPE = 0x03393BD0;
    constexpr uintptr_t RVA_GAME_OBJECT_ADD_COMPONENT = 0x0539525C;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_CREATE_INJECTED = 0x0421E8F0;
    constexpr uintptr_t RVA_ANIMATION_CLIP_PLAYABLE_CREATE_HANDLE = 0x04126100;
    constexpr uintptr_t RVA_ANIMATION_CLIP_PLAYABLE_SET_OVERRIDE_LOOP_TIME =
        0x0539814C;
    constexpr uintptr_t RVA_ANIMATION_CLIP_PLAYABLE_SET_LOOP_TIME = 0x05398154;
    constexpr uintptr_t RVA_CREATE_ANIMATION_OUTPUT = 0x03FC8C80;
    constexpr uintptr_t RVA_ANIMATION_OUTPUT_SET_TARGET = 0x043C1180;
    constexpr uintptr_t RVA_PLAYABLE_OUTPUT_SET_SOURCE = 0x03E11450;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_SET_WRAP_MODE = 0x0B3F4498;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_SET_SPEED = 0x03D24A90;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_GET_TIME = 0x03B28180;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_SET_TIME = 0x03F1B500;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_SET_UPDATE_MODE = 0x04398AA0;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_PLAY = 0x043DA5F0;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_EVALUATE = 0x03DAE950;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_DESTROY = 0x0450B9D0;
    constexpr uintptr_t RVA_ANIMATION_MIXER_PLAYABLE_CREATE_HANDLE =
        0x03E0F960;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_SET_INPUT_COUNT = 0x03E0F910;
    constexpr uintptr_t RVA_PLAYABLE_GRAPH_CONNECT_INTERNAL = 0x03F2A1D0;
    constexpr uintptr_t RVA_PLAYABLE_HANDLE_SET_INPUT_WEIGHT = 0x038DE2D0;
    constexpr double MIN_SKELETON_MAPPING_COVERAGE = 0.50;
    constexpr size_t MIN_UNIQUE_MAPPED_BONES = 20;
    uint64_t REPLACEMENT_POSTMODEL_PATH_HASH = 0x0C662A5986756356ULL;
    uint64_t REPLACEMENT_POSTMODEL_BUNDLE_HASH = 0x08FF104A90936771ULL;
    bool g_modelReplacementConfiguredEnabled = true;
    float REPLACEMENT_SCALE_MULTIPLIER = 1.0f;
    double REPLACEMENT_WALK_SPEED = 1.0;
    // Retained for loop-capable final clips. Aglina's current special sprint is
    // a one-shot clip, so the mixer path is intentionally bypassed for it.
    double REPLACEMENT_WALK_LOOP_START_SECONDS = 0.968;
    double REPLACEMENT_WALK_LOOP_END_SECONDS = 2.3760002;
    double REPLACEMENT_WALK_CROSSFADE_SECONDS = 0.20;
    bool REPLACEMENT_USE_WALK_CROSSFADE = false;
    bool REPLACEMENT_FINAL_NATIVE_LOOP = true;
    bool REPLACEMENT_FORCE_FINAL_LOOP = false;
    double LOGIN_CAMERA_TURN_DURATION_SECONDS = 3.0333335;
    float LOGIN_SIT_START_YAW_DEGREES = -120.0f;
    double FORWARD_LEAN_POSE_SAMPLE_SECONDS = 1.0;
    constexpr float REPLACEMENT_VERTICAL_FOLLOW_RESPONSE_SECONDS = 0.45f;
    constexpr int DIRECTOR_UPDATE_MODE_UNSCALED_GAME_TIME = 2;
    constexpr int DIRECTOR_WRAP_MODE_HOLD = 0;
    constexpr int DIRECTOR_WRAP_MODE_LOOP = 1;
    constexpr uint8_t ROOT_CATEGORY_MAIN = 0;
    constexpr uint8_t ROOT_CATEGORY_INITIAL = 1;
    constexpr uintptr_t TLS_FLAG_TARGET_INTERNAL = 1ULL << 16;
    constexpr uintptr_t TLS_FLAG_EXPECT_HANDLE_GATE = 1ULL << 17;
    constexpr uintptr_t TLS_FLAG_CAPTURE_LOGIN_LOAD = 1ULL << 18;
    constexpr uintptr_t TLS_FLAG_LOGIN_BIND_SCOPE = 1ULL << 19;
    constexpr const char * LOGIN_SK_ACTOR_F =
        "Assets/Beyond/InitialAssets/Intro/Prefab/SK_actor_female.prefab";
    constexpr uint64_t LOGIN_SK_ACTOR_F_HASH = 0x09916462EE1477FBULL;
    const char * REPLACEMENT_POSTMODEL_PATH =
        "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
        "chr_0013_aglina_postmodel.prefab";
    const char * TARGET_MODEL_PATH = REPLACEMENT_POSTMODEL_PATH;

    enum class ReplacementPhase : int {
        None = -1,
        SitLoop = 0,
        SitSpecial = 1,
        SitToWalk = 2,
        WalkLong = 3,
        Count = 4
    };

    struct ReplacementClipSpec {
        uint64_t pathHash;
        const char * path;
        const char * label;
        double speed;
        bool loop;
        ReplacementPhase automaticNext;
    };

    constexpr size_t REPLACEMENT_CLIP_COUNT = 4;

    constexpr std::array< ReplacementClipSpec, REPLACEMENT_CLIP_COUNT >
        AGLINA_CLIPS { {
        {
            0x0E8C5DF24F56232AULL,
            "assets/beyond/arts/entity/actor/girl/aglina/animations/interact/"
            "a_actor_aglina_interact_sit_loop.fbx##"
            "a_actor_aglina_interact_sit_loop",
            "sit_loop", 1.0, true, ReplacementPhase::None
        },
        {
            0x074580C319A1D8CAULL,
            "assets/beyond/arts/entity/actor/girl/aglina/animations/interact/"
            "a_actor_aglina_interact_sit_sp.fbx##"
            "a_actor_aglina_interact_sit_sp",
            "sit_special", 1.0, false, ReplacementPhase::SitLoop
        },
        {
            0x030DE24CAC90C82AULL,
            "assets/beyond/arts/entity/actor/girl/aglina/animations/interact/"
            "a_actor_aglina_interact_sit_end.fbx##"
            "a_actor_aglina_interact_sit_end",
            "sit_to_walk", 1.0, false, ReplacementPhase::WalkLong
        },
        {
            0x0E82C0CCE73DBEB7ULL,
            "assets/beyond/arts/entity/actor/girl/aglina/animations/dialog/"
            "a_actor_aglina_dialog_state_shy2_walk.fbx##"
            "a_actor_aglina_dialog_state_shy2_walk_loop",
            "aglina_shy2_move_loop", 1.0, true, ReplacementPhase::None
        }
    } };

    constexpr std::array< ReplacementClipSpec, REPLACEMENT_CLIP_COUNT >
        LIZHIYAN_CLIPS { {
        {
            0x0186C27E759EB47CULL,
            "assets/beyond/arts/entity/actor/girl/lizhiyan/animations/interact/"
            "a_actor_lizhiyan_interact_sit_loop.fbx##"
            "a_actor_lizhiyan_interact_sit_loop",
            "sit_loop", 1.0, true, ReplacementPhase::None
        },
        {
            0x000C8BACE38FF67CULL,
            "assets/beyond/arts/entity/actor/girl/lizhiyan/animations/interact/"
            "a_actor_lizhiyan_interact_sit_sp.fbx##"
            "a_actor_lizhiyan_interact_sit_sp",
            "sit_special", 1.0, false, ReplacementPhase::SitLoop
        },
        {
            0x0C9AF4DC6844BFCCULL,
            "assets/beyond/arts/entity/actor/girl/lizhiyan/animations/interact/"
            "a_actor_lizhiyan_interact_sit_end.fbx##"
            "a_actor_lizhiyan_interact_sit_end",
            "sit_to_walk", 1.0, false, ReplacementPhase::WalkLong
        },
        {
            0x0C4356CF7310FE24ULL,
            "assets/beyond/arts/entity/actor/girl/lizhiyan/animations/dialog/"
            "a_actor_lizhiyan_dialog_virtual_single_walk_l.fbx##"
            "a_actor_lizhiyan_dialog_virtual_single_walk_l",
            "lizhiyan_walk_l", 1.0, true, ReplacementPhase::None
        }
    } };

    constexpr ReplacementClipSpec AGLINA_SHY2_LOOP {
        0x0E82C0CCE73DBEB7ULL,
        "assets/beyond/arts/entity/actor/girl/aglina/animations/dialog/"
        "a_actor_aglina_dialog_state_shy2_walk.fbx##"
        "a_actor_aglina_dialog_state_shy2_walk_loop",
        "aglina_shy2_move_loop", 1.0, true, ReplacementPhase::None
    };

    constexpr ReplacementClipSpec AGLINA_RUNBYE {
        0x083110B56FC21954ULL,
        "assets/beyond/arts/entity/actor/girl/aglina/animations/dialog/"
        "a_actor_aglina_dialog_single_runbye.fbx##"
        "a_actor_aglina_dialog_single_runbye",
        "aglina_runbye", 1.0, false, ReplacementPhase::None
    };

    constexpr ReplacementClipSpec AGLINA_DASH_ATTACK {
        0x05FF6BD5255D1BC8ULL,
        "assets/beyond/arts/entity/actor/girl/aglina/animations/battle/"
        "a_actor_aglina_battle_dash_attack.fbx##"
        "a_actor_aglina_battle_dash_attack",
        "aglina_dash_attack", 1.0, false, ReplacementPhase::None
    };

    std::array< ReplacementClipSpec, REPLACEMENT_CLIP_COUNT >
        REPLACEMENT_CLIPS = AGLINA_CLIPS;
    std::string g_configuredPostmodelPath;
    std::array< std::string, REPLACEMENT_CLIP_COUNT > g_configuredClipPaths;
    std::array< std::string, REPLACEMENT_CLIP_COUNT > g_configuredClipLabels;

    constexpr const char * CONFIG_SECTION = "EFStartChange";

    static std::string ReadConfigString(
        const std::string & configPath, const char * key,
        const char * fallback ) {
        char value [ 512 ] = { 0 };
        GetPrivateProfileStringA(
            CONFIG_SECTION, key, fallback, value,
            static_cast< DWORD >( sizeof( value ) ), configPath.c_str( ) );
        return value;
    }

    static std::string NormalizeConfigValue( std::string value ) {
        value.erase(
            std::remove_if( value.begin( ), value.end( ),
                [ ] ( unsigned char ch ) { return std::isspace( ch ) != 0; } ),
            value.end( ) );
        std::transform(
            value.begin( ), value.end( ), value.begin( ),
            [ ] ( unsigned char ch ) {
                return static_cast< char >( std::tolower( ch ) );
            } );
        return value;
    }

    static double ReadConfigNumber(
        const std::string & configPath, const char * key, double fallback,
        double minimum, double maximum ) {
        char fallbackText [ 64 ] = { 0 };
        _snprintf_s(
            fallbackText, sizeof( fallbackText ), _TRUNCATE, "%.12g", fallback );
        std::string value = ReadConfigString(
            configPath, key, fallbackText );
        char * end = nullptr;
        const double parsed = std::strtod( value.c_str( ), &end );
        if ( end == value.c_str( ) || !std::isfinite( parsed ) )
            return fallback;
        return std::clamp( parsed, minimum, maximum );
    }

    static bool ReadConfigBool(
        const std::string & configPath, const char * key, bool fallback ) {
        const std::string value = NormalizeConfigValue( ReadConfigString(
            configPath, key, fallback ? "true" : "false" ) );
        if ( value == "true" || value == "1" || value == "yes" ||
            value == "on" )
            return true;
        if ( value == "false" || value == "0" || value == "no" ||
            value == "off" )
            return false;
        return fallback;
    }

    static uint64_t ReadConfigUInt64(
        const std::string & configPath, const char * key,
        uint64_t fallback = 0 ) {
        std::string value = ReadConfigString( configPath, key, "" );
        if ( value.empty( ) )
            return fallback;
        char * end = nullptr;
        const unsigned long long parsed = std::strtoull(
            value.c_str( ), &end, 0 );
        if ( end == value.c_str( ) || *end != '\0' )
            return fallback;
        return static_cast< uint64_t >( parsed );
    }

    static void SelectCharacterAndAction(
        const std::string & requestedCharacter,
        const std::string & requestedAction,
        std::string & activeCharacter, std::string & activeAction ) {
        activeCharacter = NormalizeConfigValue( requestedCharacter );
        activeAction = NormalizeConfigValue( requestedAction );

        if ( activeCharacter == "lizhiyan" ||
            activeCharacter == "chr_0032_lizhiyan" ) {
            activeCharacter = "chr_0032_lizhiyan";
            REPLACEMENT_POSTMODEL_PATH_HASH = 0x0C73F28350A31E2DULL;
            REPLACEMENT_POSTMODEL_BUNDLE_HASH = 0x0B657B21E302A9F9ULL;
            REPLACEMENT_POSTMODEL_PATH =
                "assets/beyond/dynamicassets/gameplay/actors/postmodels/"
                "characters/chr_0032_lizhiyan_postmodel.prefab";
            TARGET_MODEL_PATH = REPLACEMENT_POSTMODEL_PATH;
            REPLACEMENT_CLIPS = LIZHIYAN_CLIPS;
            activeAction = "a_actor_lizhiyan_dialog_virtual_single_walk_l";
            return;
        }

        activeCharacter = "chr_0013_aglina";
        REPLACEMENT_POSTMODEL_PATH_HASH = 0x0C662A5986756356ULL;
        REPLACEMENT_POSTMODEL_BUNDLE_HASH = 0x08FF104A90936771ULL;
        REPLACEMENT_POSTMODEL_PATH =
            "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
            "chr_0013_aglina_postmodel.prefab";
        TARGET_MODEL_PATH = REPLACEMENT_POSTMODEL_PATH;
        REPLACEMENT_CLIPS = AGLINA_CLIPS;

        if ( activeAction == "aglina_runbye" ||
            activeAction == "a_actor_aglina_dialog_single_runbye" )
            REPLACEMENT_CLIPS [ 3 ] = AGLINA_RUNBYE;
        else if ( activeAction == "aglina_dash_attack" ||
            activeAction == "a_actor_aglina_battle_dash_attack" )
            REPLACEMENT_CLIPS [ 3 ] = AGLINA_DASH_ATTACK;
        else {
            activeAction = "a_actor_aglina_dialog_state_shy2_walk_loop";
            REPLACEMENT_CLIPS [ 3 ] = AGLINA_SHY2_LOOP;
        }
    }

    static bool ApplyConfiguredAssets( const std::string & configPath ) {
        const std::string modelPath = ReadConfigString(
            configPath, "model_path", "" );
        if ( modelPath.empty( ) )
            return false;

        const uint64_t modelPathHash = ReadConfigUInt64(
            configPath, "model_path_hash" );
        const uint64_t modelBundleHash = ReadConfigUInt64(
            configPath, "model_bundle_hash" );
        constexpr std::array< const char *, REPLACEMENT_CLIP_COUNT >
            pathKeys { {
                "sit_loop_path", "sit_special_path", "sit_to_walk_path",
                "final_path"
            } };
        constexpr std::array< const char *, REPLACEMENT_CLIP_COUNT >
            hashKeys { {
                "sit_loop_path_hash", "sit_special_path_hash",
                "sit_to_walk_path_hash", "final_path_hash"
            } };
        constexpr std::array< const char *, REPLACEMENT_CLIP_COUNT >
            labelKeys { {
                "sit_loop_label", "sit_special_label", "sit_to_walk_label",
                "final_label"
            } };

        std::array< std::string, REPLACEMENT_CLIP_COUNT > paths;
        std::array< std::string, REPLACEMENT_CLIP_COUNT > labels;
        std::array< uint64_t, REPLACEMENT_CLIP_COUNT > hashes { };
        bool complete = modelPathHash != 0 && modelBundleHash != 0;
        for ( size_t i = 0; i < REPLACEMENT_CLIP_COUNT; ++i ) {
            paths [ i ] = ReadConfigString( configPath, pathKeys [ i ], "" );
            labels [ i ] = ReadConfigString(
                configPath, labelKeys [ i ], pathKeys [ i ] );
            hashes [ i ] = ReadConfigUInt64( configPath, hashKeys [ i ] );
            complete = complete && !paths [ i ].empty( ) && hashes [ i ] != 0;
        }
        if ( !complete ) {
            Log( "[config] custom asset fields are incomplete; using built-in preset" );
            return false;
        }

        g_configuredPostmodelPath = modelPath;
        g_configuredClipPaths = std::move( paths );
        g_configuredClipLabels = std::move( labels );
        REPLACEMENT_POSTMODEL_PATH_HASH = modelPathHash;
        REPLACEMENT_POSTMODEL_BUNDLE_HASH = modelBundleHash;
        REPLACEMENT_POSTMODEL_PATH = g_configuredPostmodelPath.c_str( );
        TARGET_MODEL_PATH = REPLACEMENT_POSTMODEL_PATH;
        for ( size_t i = 0; i < REPLACEMENT_CLIP_COUNT; ++i ) {
            REPLACEMENT_CLIPS [ i ].pathHash = hashes [ i ];
            REPLACEMENT_CLIPS [ i ].path = g_configuredClipPaths [ i ].c_str( );
            REPLACEMENT_CLIPS [ i ].label = g_configuredClipLabels [ i ].c_str( );
        }
        REPLACEMENT_FINAL_NATIVE_LOOP = ReadConfigBool(
            configPath, "final_native_loop", REPLACEMENT_CLIPS [ 3 ].loop );
        return true;
    }

    static bool LoadRuntimeConfiguration( const std::string & configPath ) {
        const bool exists = GetFileAttributesA( configPath.c_str( ) ) !=
            INVALID_FILE_ATTRIBUTES;
        g_modelReplacementConfiguredEnabled = ReadConfigBool(
            configPath, "model_replacement_enabled", false );
        if ( !g_modelReplacementConfiguredEnabled ) {
            Log( "[config] model replacement configured disabled; model hooks will not be installed" );
            return exists;
        }

        std::string activeCharacter;
        std::string activeAction;
        const std::string requestedCharacter = ReadConfigString(
            configPath, "character", "chr_0013_aglina" );
        const std::string requestedAction = ReadConfigString(
            configPath, "final_action",
            "a_actor_aglina_dialog_state_shy2_walk_loop" );
        SelectCharacterAndAction(
            requestedCharacter, requestedAction, activeCharacter, activeAction );
        if ( ApplyConfiguredAssets( configPath ) ) {
            activeCharacter = NormalizeConfigValue( requestedCharacter );
            activeAction = NormalizeConfigValue( requestedAction );
        }

        REPLACEMENT_SCALE_MULTIPLIER = static_cast< float >(
            ReadConfigNumber( configPath, "scale", 1.0, 0.50, 2.0 ) );
        LOGIN_SIT_START_YAW_DEGREES = static_cast< float >(
            ReadConfigNumber( configPath, "start_yaw", -120.0, -360.0, 360.0 ) );
        LOGIN_CAMERA_TURN_DURATION_SECONDS = ReadConfigNumber(
            configPath, "turn_duration", 3.0333335, 0.10, 15.0 );
        FORWARD_LEAN_POSE_SAMPLE_SECONDS = ReadConfigNumber(
            configPath, "forward_lean_sample", 1.0, 0.0, 5.0 );

        REPLACEMENT_CLIPS [ 0 ].speed = ReadConfigNumber(
            configPath, "sit_loop_speed", 1.0, 0.10, 4.0 );
        REPLACEMENT_CLIPS [ 1 ].speed = ReadConfigNumber(
            configPath, "sit_special_speed", 1.0, 0.10, 4.0 );
        REPLACEMENT_CLIPS [ 2 ].speed = ReadConfigNumber(
            configPath, "sit_to_walk_speed", 1.0, 0.10, 4.0 );
        REPLACEMENT_CLIPS [ 3 ].speed = ReadConfigNumber(
            configPath, "final_speed", 1.0, 0.10, 4.0 );
        REPLACEMENT_FINAL_NATIVE_LOOP = ReadConfigBool(
            configPath, "final_native_loop", REPLACEMENT_CLIPS [ 3 ].loop );
        REPLACEMENT_CLIPS [ 3 ].loop = ReadConfigBool(
            configPath, "final_loop", REPLACEMENT_FINAL_NATIVE_LOOP );
        REPLACEMENT_FORCE_FINAL_LOOP = ReadConfigBool(
            configPath, "force_loop", false );
        REPLACEMENT_WALK_SPEED = REPLACEMENT_CLIPS [ 3 ].speed;

        REPLACEMENT_USE_WALK_CROSSFADE = ReadConfigBool(
            configPath, "use_crossfade", false );
        REPLACEMENT_WALK_LOOP_START_SECONDS = ReadConfigNumber(
            configPath, "loop_start", 0.968, 0.0, 30.0 );
        REPLACEMENT_WALK_LOOP_END_SECONDS = ReadConfigNumber(
            configPath, "loop_end", 2.3760002,
            REPLACEMENT_WALK_LOOP_START_SECONDS + 0.05, 60.0 );
        const double maximumCrossfade =
            ( REPLACEMENT_WALK_LOOP_END_SECONDS -
                REPLACEMENT_WALK_LOOP_START_SECONDS ) * 0.5;
        REPLACEMENT_WALK_CROSSFADE_SECONDS = ReadConfigNumber(
            configPath, "crossfade_duration", 0.20, 0.01,
            maximumCrossfade > 0.01 ? maximumCrossfade : 0.01 );

        char summary [ 2048 ] = { 0 };
        _snprintf_s(
            summary, sizeof( summary ), _TRUNCATE,
            "[config] %s%s | modelEnabled=%s character=%s action=%s yaw=%.3f turn=%.3f "
            "scale=%.3f speeds=%.3f,%.3f,%.3f,%.3f finalLoop=%s "
            "nativeLoop=%s forceLoop=%s crossfade=%s",
            exists ? "loaded " : "using defaults; missing ", configPath.c_str( ),
            g_modelReplacementConfiguredEnabled ? "true" : "false",
            activeCharacter.c_str( ), activeAction.c_str( ),
            LOGIN_SIT_START_YAW_DEGREES,
            LOGIN_CAMERA_TURN_DURATION_SECONDS,
            REPLACEMENT_SCALE_MULTIPLIER,
            REPLACEMENT_CLIPS [ 0 ].speed,
            REPLACEMENT_CLIPS [ 1 ].speed,
            REPLACEMENT_CLIPS [ 2 ].speed,
            REPLACEMENT_CLIPS [ 3 ].speed,
            REPLACEMENT_CLIPS [ 3 ].loop ? "true" : "false",
            REPLACEMENT_FINAL_NATIVE_LOOP ? "true" : "false",
            REPLACEMENT_FORCE_FINAL_LOOP ? "true" : "false",
            REPLACEMENT_USE_WALK_CROSSFADE ? "true" : "false" );
        Log( summary );
        return exists;
    }

    std::atomic_bool g_replacementActive = false;
    std::atomic_bool g_redirectEnabled = true;
    std::atomic_bool g_redirectHit = false;
    std::atomic_bool g_mainCategoryHit = false;
    std::atomic_bool g_mainLoadReturnedHandle = false;
    std::atomic_bool g_initialPathHashEntered = false;
    std::atomic_bool g_initialPathHashCompleted = false;
    std::atomic_bool g_mainPathHashEntered = false;
    std::atomic_bool g_mainPathHashCompleted = false;
    std::atomic_bool g_mainDiagnosticsStarted = false;
    std::atomic_bool g_mainDiagnosticsCompleted = false;
    std::atomic_bool g_resourceManagerInitialized = false;
    std::atomic_bool g_resourceManagerAsyncIniting = false;
    std::atomic_bool g_resourceManagerUsingBundle = false;
    std::atomic_bool g_stringCheckExists = false;
    std::atomic_bool g_hashCheckExists = false;
    std::atomic_bool g_rawHashCheckExists = false;
    std::atomic_uint64_t g_initialPathHashCompletedTick = 0;
    std::atomic_uint64_t g_mainPathHashCompletedTick = 0;
    std::atomic_uint64_t g_targetResolvedHash = 0;
    std::atomic_bool g_loadAssetInternalReturnedProxy = false;
    std::atomic_bool g_bundleLoaderReturnedProxy = false;
    std::atomic_bool g_bundleLoaderProxyHasError = false;
    std::atomic_bool g_loadUntrackedReturnedHandle = false;
    std::atomic_bool g_loadUntrackedDirectIsValid = false;
    std::atomic_bool g_untrackedHandleGatePassed = false;
    std::atomic_bool g_tryGetAssetInfoLoadObserved = false;
    std::atomic_bool g_loadAssetInternalObserved = false;
    std::atomic_bool g_bundleLoaderObserved = false;
    std::atomic_bool g_loadUntrackedObserved = false;
    std::atomic_bool g_untrackedHandleGateObserved = false;
    std::atomic_bool g_assetProxyFinishErrorObserved = false;
    std::atomic_bool g_mainBundlePathObserved = false;
    std::atomic_uint32_t g_bundleFileLoadCount = 0;
    std::atomic_uint32_t g_bundleFileFailureCount = 0;
    std::atomic_uint32_t g_bundleSyncCount = 0;
    std::atomic_uint32_t g_bundleSyncFailureCount = 0;
    std::atomic_bool g_deferredLoadPending = false;
    std::atomic_bool g_deferredLoadAttempted = false;
    std::atomic_bool g_loginLoadContextCaptured = false;
    std::atomic< void * > g_i18nLoadGameObjectMethod = nullptr;
    std::atomic_uint32_t g_loginLoadThreadId = 0;
    std::atomic_uint64_t g_deferredHandleRaw0 = 0;
    std::atomic_uint64_t g_deferredHandleRaw1 = 0;
    std::atomic_uint32_t g_deferredHandleRaw2 = 0;
    std::atomic_bool g_loginBindObserved = false;
    std::atomic_bool g_loginInstantiateCaptured = false;
    std::atomic_bool g_loginSessionActive = false;
    std::atomic_bool g_loginSessionCleanupInProgress = false;
    std::atomic_bool g_loginReloadSuspended = false;
    std::atomic_uint64_t g_loginReloadResumeNextTick = 0;
    std::atomic_uint32_t g_loginSessionGeneration = 0;
    std::atomic_bool g_pelicaPrefabResolved = false;
    std::atomic_bool g_pelicaInstantiateAttempted = false;
    std::atomic_bool g_pelicaInstantiateSucceeded = false;
    std::atomic_bool g_originalLoginActorDisabled = false;
    std::atomic_bool g_skeletonBindAttempted = false;
    std::atomic_bool g_skeletonBindSucceeded = false;
    std::atomic_bool g_originalRenderersDisabled = false;
    std::atomic_bool g_walkClipLoadAttempted = false;
    std::atomic_bool g_walkClipResolved = false;
    std::atomic_bool g_walkPlayableStarted = false;
    std::atomic_int g_pendingReplacementPhase =
        static_cast< int >( ReplacementPhase::None );
    std::atomic_int g_activeReplacementPhase =
        static_cast< int >( ReplacementPhase::None );
    std::atomic_int g_lastRequestedReplacementPhase =
        static_cast< int >( ReplacementPhase::None );
    std::atomic_bool g_loginAnimationGateRequested = false;
    std::atomic_bool g_loginAnimationTickHeld = false;
    std::atomic_bool g_loginAnimationGateTimedOut = false;
    std::atomic_uint64_t g_loginAnimationGateStartTick = 0;
    std::atomic< void * > g_loginActorPrefab = nullptr;
    std::atomic< void * > g_loginActorParent = nullptr;
    std::atomic< void * > g_loginActorInstance = nullptr;
    std::atomic< void * > g_loginInstantiateMethod = nullptr;
    std::atomic< void * > g_pelicaPrefab = nullptr;
    std::atomic< void * > g_pelicaInstance = nullptr;
    std::array< std::atomic< void * >, REPLACEMENT_CLIP_COUNT >
        g_replacementClips { };
    uint32_t g_pelicaPrefabGcHandle = 0;
    std::array< uint32_t, REPLACEMENT_CLIP_COUNT >
        g_replacementClipGcHandles { };
    float g_activeReplacementClipLength = 0.0f;
    double g_activeReplacementClipElapsed = 0.0;

    using CachedLoadGameObjectFn = void * ( __fastcall * )(
        void * result, void * loader, void * path, void * method );
    using I18NLoadGameObjectFn = void * ( __fastcall * )(
        void * result, uint64_t pathHash, uint8_t category, void * method );
    using ResolveI18NPathHashFn = uint64_t( __fastcall * )(
        uint64_t pathHash, uint8_t category, void * method );
    using InitPathHashFn = void( __fastcall * )( void * method );
    using StaticBoolFn = bool( __fastcall * )( void * method );
    using CheckStringFn = bool( __fastcall * )(
        void * path, uint8_t category, void * method );
    using CheckHashFn = bool( __fastcall * )(
        uint64_t pathHash, uint8_t category, void * method );
    using LoadUntrackedHashFn = void * ( __fastcall * )(
        void * result, void * manager, uint64_t pathHash, void * type,
        uint8_t category, void * method );
    using LoadAssetInternalHashFn = void * ( __fastcall * )(
        void * manager, uint64_t pathHash, void * type, uint8_t category,
        bool isAsync, int priority, void * method );
    using BundleLoaderLoadAssetFn = void * ( __fastcall * )(
        void * manager, void * assetInfo, void * type, bool isAsync,
        int priority, void * method );
    using TryGetAssetInfoHashFn = bool( __fastcall * )(
        void * manifest, uint64_t pathHash, void * assetInfo, void * method );
    using HandleGateFn = bool( __fastcall * )( void * handle, void * method );
    using AssetProxyHasErrorFn = bool( __fastcall * )(
        void * assetProxy, void * method );
    using AssetProxyFinishErrorFn = void( __fastcall * )(
        void * assetProxy, void * message, void * method );
    using BundleProxyLoadSyncFn = bool( __fastcall * )(
        void * bundleProxy, void * method );
    using BundleProxyGetFilePathFn = void * ( __fastcall * )(
        void * bundleProxy, void * bundleName, void * method );
    using BundleProxyLoadFileFn = void * ( __fastcall * )(
        void * filePath, void * method );
    using Il2CppThreadCurrentFn = void * ( __fastcall * )( );
    using Il2CppStringNewFn = void * ( __fastcall * )( const char * value );
    using LoginSceneRootOnBindFn = void( __fastcall * )(
        void * instance, void * method );
    using LoginLifecycleActionFn = void( __fastcall * )(
        void * instance, void * method );
    using LoginAnimChangeStateFn = void( __fastcall * )(
        void * instance, int target, void * method );
    using LoginAnimActionFn = void( __fastcall * )(
        void * instance, void * method );
    using LoginAnimTickFn = void( __fastcall * )(
        void * instance, float deltaTime, void * method );
    using AssetProxyHandleGetFn = void * ( __fastcall * )(
        void * handle, void * method );
    using InstantiateGameObjectParentFn = void * ( __fastcall * )(
        void * original, void * parent, bool worldPositionStays, void * method );
    using GameObjectSetActiveFn = void( __fastcall * )(
        void * gameObject, bool active, void * method );
    using TransformSetParentFn = void( __fastcall * )(
        void * transform, void * parent, bool worldPositionStays, void * method );
    using GameObjectGetComponentsInChildrenFn = void * ( __fastcall * )(
        void * gameObject, void * type, bool includeInactive, void * method );
    using RendererSetEnabledFn = void( __fastcall * )(
        void * renderer, bool enabled, void * method );
    using SkinnedMeshRendererSetBonesFn = void( __fastcall * )(
        void * renderer, void * bones, void * method );
    using SkinnedMeshRendererSetRootBoneFn = void( __fastcall * )(
        void * renderer, void * rootBone, void * method );
    using ResourceManagerLoadHashTypeFn = void * ( __fastcall * )(
        void * result, uint64_t pathHash, void * type, uint8_t category,
        int priority, void * method );
    using GameObjectAddComponentFn = void * ( __fastcall * )(
        void * gameObject, void * type, void * method );
    CachedLoadGameObjectFn g_originalCachedLoadGameObject = nullptr;
    I18NLoadGameObjectFn g_originalI18NLoadGameObject = nullptr;
    ResolveI18NPathHashFn g_originalResolveI18NPathHash = nullptr;
    InitPathHashFn g_originalInitInitialPathHash = nullptr;
    InitPathHashFn g_originalInitMainPathHash = nullptr;
    StaticBoolFn g_resourceManagerIsInitialized = nullptr;
    StaticBoolFn g_resourceManagerIsAsyncIniting = nullptr;
    StaticBoolFn g_resourceManagerGetUsingBundle = nullptr;
    CheckStringFn g_resourceManagerCheckString = nullptr;
    CheckHashFn g_resourceManagerCheckHash = nullptr;
    CheckHashFn g_resourceManagerRawCheckHash = nullptr;
    LoadUntrackedHashFn g_originalLoadUntrackedHash = nullptr;
    LoadAssetInternalHashFn g_originalLoadAssetInternalHash = nullptr;
    BundleLoaderLoadAssetFn g_originalBundleLoaderLoadAsset = nullptr;
    TryGetAssetInfoHashFn g_originalTryGetAssetInfoHash = nullptr;
    HandleGateFn g_originalUntrackedHandleGate = nullptr;
    HandleGateFn g_untrackedHandleIsValid = nullptr;
    AssetProxyHasErrorFn g_assetProxyHasError = nullptr;
    AssetProxyFinishErrorFn g_originalAssetProxyFinishError = nullptr;
    BundleProxyLoadSyncFn g_originalBundleProxyLoadSync = nullptr;
    BundleProxyGetFilePathFn g_originalBundleProxyGetFilePath = nullptr;
    BundleProxyLoadFileFn g_originalBundleProxyLoadFile = nullptr;
    Il2CppThreadCurrentFn g_il2cppThreadCurrent = nullptr;
    Il2CppStringNewFn g_il2cppStringNew = nullptr;
    LoginLifecycleActionFn g_originalLoginManagerReleaseScene = nullptr;
    LoginSceneRootOnBindFn g_originalLoginSceneRootOnBind = nullptr;
    LoginAnimChangeStateFn g_originalLoginAnimChangeState = nullptr;
    LoginAnimActionFn g_originalLoginAnimResetA1 = nullptr;
    LoginAnimActionFn g_originalLoginAnimPlayA1s = nullptr;
    LoginAnimActionFn g_originalLoginAnimPlayA1A2 = nullptr;
    LoginAnimTickFn g_originalLoginAnimTick = nullptr;
    LoginLifecycleActionFn g_originalLoginAnimRelease = nullptr;
    AssetProxyHandleGetFn g_assetProxyHandleGet = nullptr;
    InstantiateGameObjectParentFn g_originalInstantiateGameObjectParent = nullptr;
    GameObjectSetActiveFn g_gameObjectSetActive = nullptr;
    TransformSetParentFn g_transformSetParent = nullptr;
    GameObjectGetComponentsInChildrenFn g_gameObjectGetComponentsInChildren = nullptr;
    RendererSetEnabledFn g_rendererSetEnabled = nullptr;
    SkinnedMeshRendererSetBonesFn g_skinnedMeshRendererSetBones = nullptr;
    SkinnedMeshRendererSetRootBoneFn g_skinnedMeshRendererSetRootBone = nullptr;
    ResourceManagerLoadHashTypeFn g_resourceManagerLoadHashType = nullptr;
    GameObjectAddComponentFn g_gameObjectAddComponent = nullptr;
    void * g_cachedLoadGameObjectTarget = nullptr;
    void * g_i18nLoadGameObjectTarget = nullptr;
    void * g_resolveI18NPathHashTarget = nullptr;
    void * g_initInitialPathHashTarget = nullptr;
    void * g_initMainPathHashTarget = nullptr;
    void * g_loadUntrackedHashTarget = nullptr;
    void * g_loadAssetInternalHashTarget = nullptr;
    void * g_bundleLoaderLoadAssetTarget = nullptr;
    void * g_tryGetAssetInfoHashTarget = nullptr;
    void * g_untrackedHandleGateTarget = nullptr;
    void * g_assetProxyFinishErrorTarget = nullptr;
    void * g_bundleProxyLoadSyncTarget = nullptr;
    void * g_bundleProxyGetFilePathTarget = nullptr;
    void * g_bundleProxyLoadFileTarget = nullptr;
    void * g_loginManagerReleaseSceneTarget = nullptr;
    void * g_loginSceneRootOnBindTarget = nullptr;
    void * g_loginAnimChangeStateTarget = nullptr;
    void * g_loginAnimResetA1Target = nullptr;
    void * g_loginAnimPlayA1sTarget = nullptr;
    void * g_loginAnimPlayA1A2Target = nullptr;
    void * g_loginAnimTickTarget = nullptr;
    void * g_loginAnimReleaseTarget = nullptr;
    void * g_instantiateGameObjectParentTarget = nullptr;
    DWORD g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
    uintptr_t g_gameAssemblyBase = 0;
    bool g_minHookInitialized = false;

    void * g_loader = nullptr;
    uint32_t g_loaderGcHandle = 0;
    void * g_targetRoot = nullptr;
    void * g_replacementRoot = nullptr;
    std::vector< void * > g_disabledRenderers;

    void * clsObject = nullptr;
    void * clsResources = nullptr;
    void * clsGameObject = nullptr;
    void * clsComponent = nullptr;
    void * clsTransform = nullptr;
    void * clsRenderer = nullptr;
    void * clsSkinnedMeshRenderer = nullptr;
    void * clsBehaviour = nullptr;
    void * clsAnimator = nullptr;
    void * clsAnimationClip = nullptr;
    void * clsNpcSimpleOneStateGraph = nullptr;
    void * clsResourceManager = nullptr;
    void * clsSimpleAssetLoader = nullptr;
    void * clsStringPathHash = nullptr;
    void * clsAssetLoaderHandle = nullptr;

    void * mFindObjectsOfTypeAll = nullptr;
    void * mObjectGetName = nullptr;
    void * mObjectDestroy = nullptr;
    void * mGameObjectFind = nullptr;
    void * mGameObjectGetTransform = nullptr;
    void * mGameObjectGetLayer = nullptr;
    void * mGameObjectSetLayer = nullptr;
    void * mGameObjectSetActive = nullptr;
    void * mComponentGetGameObject = nullptr;
    void * mGetComponentsInChildren = nullptr;
    void * mTransformGetParent = nullptr;
    void * mTransformSetParent = nullptr;
    void * mGetPosition = nullptr;
    void * mSetPosition = nullptr;
    void * mGetLocalPosition = nullptr;
    void * mSetLocalPosition = nullptr;
    void * mGetLocalRotation = nullptr;
    void * mSetLocalRotation = nullptr;
    void * mGetLocalScale = nullptr;
    void * mSetLocalScale = nullptr;
    void * mRendererSetEnabled = nullptr;
    void * mSkinnedGetBones = nullptr;
    void * mSkinnedSetBones = nullptr;
    void * mSkinnedGetRootBone = nullptr;
    void * mSkinnedSetRootBone = nullptr;
    void * mBehaviourGetEnabled = nullptr;
    void * mBehaviourSetEnabled = nullptr;
    void * mBehaviourGetIsActiveAndEnabled = nullptr;
    void * mAnimatorGetIsHuman = nullptr;
    void * mAnimatorGetIsInitialized = nullptr;
    void * mAnimatorGetHasBoundPlayables = nullptr;
    void * mAnimatorGetAvatar = nullptr;
    void * mAnimatorGetCullingMode = nullptr;
    void * mAnimatorSetCullingMode = nullptr;
    void * mAnimatorGetUpdateMode = nullptr;
    void * mAnimatorSetUpdateMode = nullptr;
    void * mAnimatorRebind = nullptr;
    void * mClipGetLength = nullptr;
    void * mClipGetEmpty = nullptr;
    void * mClipGetLegacy = nullptr;
    void * mClipGetHumanMotion = nullptr;
    void * mNpcGraphCtor = nullptr;
    void * mNpcGraphInit = nullptr;
    void * mNpcGraphSetMainAnimClip = nullptr;
    void * mNpcGraphSetMainClipSpeed = nullptr;
    void * mNpcGraphPlay = nullptr;
    void * mNpcGraphDestroy = nullptr;
    void * mResourceCheckExists = nullptr;
    void * mLoaderCtor = nullptr;
    void * mCreateStringPathHash = nullptr;
    void * mInstantiatePrefab = nullptr;
    void * mHandleLoadImmediate = nullptr;
    void * mHandleGet = nullptr;
    void * mHandleGetHasError = nullptr;
    void * mHandleGetErrorCode = nullptr;

    struct Vector3 {
        float x, y, z;
    };

    struct Quaternion {
        float x, y, z, w;
    };

    struct PlayableGraphData {
        void * handle = nullptr;
        uint32_t version = 0;
        uint32_t padding = 0;
    };

    using PlayableHandleData = PlayableGraphData;
    using PlayableOutputHandleData = PlayableGraphData;

    constexpr size_t MAX_LOGIN_ACTOR_SLOTS = 1;

    struct LoginActorReplacementState {
        int sourcePrefabIndex = -1;
        void * controller = nullptr;
        void * targetAnimator = nullptr;
        void * targetRoot = nullptr;
        void * replacementRoot = nullptr;
        void * replacementTransform = nullptr;
        void * originalAnchor = nullptr;
        void * replacementAnchor = nullptr;
        std::vector< void * > disabledRenderers;
        PlayableGraphData playableGraph { };
        PlayableHandleData activePlayable { };
        PlayableHandleData walkMixer { };
        std::array< PlayableHandleData, 2 > walkPlayables { };
        int walkActiveInput = 0;
        bool walkBlendActive = false;
        bool walkMixerConfigured = false;
        bool walkMixerFaultLogged = false;
        uint32_t walkBlendCount = 0;
        void * graphScript = nullptr;
        uint32_t graphScriptGcHandle = 0;
        int pendingPhase = static_cast< int >( ReplacementPhase::None );
        int activePhase = static_cast< int >( ReplacementPhase::None );
        int lastRequestedPhase = static_cast< int >( ReplacementPhase::None );
        float activeClipLength = 0.0f;
        double activeClipElapsed = 0.0;
        uint64_t gateStartTick = 0;
        bool replacementActive = false;
        bool instantiateAttempted = false;
        bool gateRequested = false;
        bool tickHeld = false;
        bool gateTimedOut = false;
        bool anchorAlignmentLogged = false;
    };

    std::array< LoginActorReplacementState, MAX_LOGIN_ACTOR_SLOTS >
        g_loginActorSlots { };
    std::array< std::atomic< void * >, MAX_LOGIN_ACTOR_SLOTS >
        g_originalLoginPrefabs { };
    std::array< uint32_t, MAX_LOGIN_ACTOR_SLOTS >
        g_originalLoginPrefabGcHandles { };
    std::atomic_int g_broadcastReplacementPhase =
        static_cast< int >( ReplacementPhase::SitLoop );
    std::atomic< void * > g_sequenceDriverController = nullptr;

    using PlayableGraphCreateInjectedFn = void( __fastcall * )(
        void * name, PlayableGraphData * result, void * method );
    using AnimationClipPlayableCreateHandleFn = bool( __fastcall * )(
        PlayableGraphData * graph, void * clip, PlayableHandleData * result,
        void * method );
    using AnimationClipPlayableSetLoopFlagFn = void( __fastcall * )(
        PlayableHandleData * playable, bool value, void * method );
    using AnimationMixerPlayableCreateHandleFn = bool( __fastcall * )(
        PlayableGraphData * graph, PlayableHandleData * result,
        void * method );
    using PlayableHandleSetInputCountFn = void( __fastcall * )(
        PlayableHandleData * playable, int inputCount, void * method );
    using PlayableGraphConnectInternalFn = bool( __fastcall * )(
        PlayableGraphData * graph, PlayableHandleData * source,
        int sourceOutputPort, PlayableHandleData * destination,
        int destinationInputPort, void * method );
    using PlayableHandleSetInputWeightFn = void( __fastcall * )(
        PlayableHandleData * playable, int inputIndex, float weight,
        void * method );
    using CreateAnimationOutputFn = bool( __fastcall * )(
        PlayableGraphData * graph, void * name,
        PlayableOutputHandleData * result, void * method );
    using AnimationOutputSetTargetFn = void( __fastcall * )(
        PlayableOutputHandleData * output, void * animator, void * method );
    using PlayableOutputSetSourceFn = void( __fastcall * )(
        PlayableOutputHandleData * output, PlayableHandleData * source,
        int port, void * method );
    using PlayableHandleSetWrapModeFn = void( __fastcall * )(
        PlayableHandleData * playable, int mode, void * method );
    using PlayableHandleSetSpeedFn = void( __fastcall * )(
        PlayableHandleData * playable, double speed, void * method );
    using PlayableHandleGetTimeFn = double( __fastcall * )(
        PlayableHandleData * playable, void * method );
    using PlayableHandleSetTimeFn = void( __fastcall * )(
        PlayableHandleData * playable, double time, void * method );
    using PlayableGraphSetUpdateModeFn = void( __fastcall * )(
        PlayableGraphData * graph, int mode, void * method );
    using PlayableGraphEvaluateFn = void( __fastcall * )(
        PlayableGraphData * graph, float deltaTime, void * method );
    using PlayableGraphActionFn = void( __fastcall * )(
        PlayableGraphData * graph, void * method );

    PlayableGraphCreateInjectedFn g_playableGraphCreateInjected = nullptr;
    AnimationClipPlayableCreateHandleFn g_animationClipPlayableCreateHandle = nullptr;
    AnimationClipPlayableSetLoopFlagFn
        g_animationClipPlayableSetOverrideLoopTime = nullptr;
    AnimationClipPlayableSetLoopFlagFn g_animationClipPlayableSetLoopTime = nullptr;
    AnimationMixerPlayableCreateHandleFn
        g_animationMixerPlayableCreateHandle = nullptr;
    PlayableHandleSetInputCountFn g_playableHandleSetInputCount = nullptr;
    PlayableGraphConnectInternalFn g_playableGraphConnectInternal = nullptr;
    PlayableHandleSetInputWeightFn g_playableHandleSetInputWeight = nullptr;
    CreateAnimationOutputFn g_createAnimationOutput = nullptr;
    AnimationOutputSetTargetFn g_animationOutputSetTarget = nullptr;
    PlayableOutputSetSourceFn g_playableOutputSetSource = nullptr;
    PlayableHandleSetWrapModeFn g_playableHandleSetWrapMode = nullptr;
    PlayableHandleSetSpeedFn g_playableHandleSetSpeed = nullptr;
    PlayableHandleGetTimeFn g_playableHandleGetTime = nullptr;
    PlayableHandleSetTimeFn g_playableHandleSetTime = nullptr;
    PlayableGraphSetUpdateModeFn g_playableGraphSetUpdateMode = nullptr;
    PlayableGraphActionFn g_playableGraphPlay = nullptr;
    PlayableGraphEvaluateFn g_playableGraphEvaluate = nullptr;
    PlayableGraphActionFn g_playableGraphDestroy = nullptr;
    PlayableGraphData g_walkPlayableGraph { };
    PlayableHandleData g_activeReplacementPlayable { };
    void * g_walkGraphScript = nullptr;
    uint32_t g_walkGraphScriptGcHandle = 0;

    struct RawIl2CppString {
        void * klass;
        void * monitor;
        int32_t length;
        wchar_t chars [ 1 ];
    };

    struct RendererBinding {
        void * renderer = nullptr;
        std::vector< void * > bones;
        void * rootBone = nullptr;
        int mapped = 0;
    };

    static bool RawStringEqualsAscii( void * value, const char * expected ) {
        if ( !value || !expected )
            return false;

        __try {
            auto * str = reinterpret_cast< RawIl2CppString * >( value );
            size_t expectedLength = strlen( expected );
            if ( str->length < 0 ||
                static_cast< size_t >( str->length ) != expectedLength )
                return false;
            for ( int32_t i = 0; i < str->length; ++i ) {
                if ( str->chars [ i ] !=
                    static_cast< wchar_t >( static_cast< unsigned char >(
                        expected [ i ] ) ) )
                    return false;
            }
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static uintptr_t GetRedirectScopeDepth( ) {
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            return 0;
        return reinterpret_cast< uintptr_t >(
            TlsGetValue( g_redirectTlsIndex ) );
    }

    static bool SetRedirectScopeDepth( uintptr_t depth ) {
        return g_redirectTlsIndex != TLS_OUT_OF_INDEXES &&
            TlsSetValue( g_redirectTlsIndex,
                reinterpret_cast< void * >( depth ) ) != FALSE;
    }

    static const char * BoolResult( bool called, bool value ) {
        return called ? ( value ? "true" : "false" ) : "fault";
    }

    static bool CheckPrologue( const char * label, const void * target,
        const uint8_t * expected, size_t length ) {
        constexpr size_t MAX_PROLOGUE_LENGTH = 16;
        uint8_t actual [ MAX_PROLOGUE_LENGTH ] = { 0 };
        bool readable = false;
        bool matches = false;
        if ( target && expected && length > 0 &&
            length <= MAX_PROLOGUE_LENGTH ) {
            __try {
                memcpy( actual, target, length );
                readable = true;
                matches = memcmp( actual, expected, length ) == 0;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                readable = false;
                matches = false;
            }
        }

        if ( matches )
            return true;

        char expectedHex [ MAX_PROLOGUE_LENGTH * 3 + 1 ] = { 0 };
        char actualHex [ MAX_PROLOGUE_LENGTH * 3 + 1 ] = { 0 };
        for ( size_t i = 0; i < length && i < MAX_PROLOGUE_LENGTH; ++i ) {
            snprintf( expectedHex + i * 3,
                sizeof( expectedHex ) - i * 3, "%02X ", expected [ i ] );
            if ( readable )
                snprintf( actualHex + i * 3,
                    sizeof( actualHex ) - i * 3, "%02X ", actual [ i ] );
        }
        char output [ 320 ] = { 0 };
        snprintf( output, sizeof( output ),
            "[hook-diag] signature mismatch %s target=%p expected=%sactual=%s",
            label ? label : "<unknown>", target, expectedHex,
            readable ? actualHex : "<unreadable>" );
        Log( output );
        return false;
    }

    static std::string ReadString( void * str );

    static const char * BundleLoadStatusName( int status ) {
        switch ( status ) {
        case 0: return "Wait";
        case 1: return "Loading";
        case 2: return "SuccessToLoad";
        case 3: return "FailedToLoad";
        case 4: return "Unloaded";
        default: return "Unknown";
        }
    }

    static bool SafeReadBundleProxyState( void * proxy, uint64_t & hashName,
        int & status, void *& assetBundle, void *& errorCode ) {
        if ( !proxy )
            return false;
        __try {
            hashName = *reinterpret_cast< const uint64_t * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x30 );
            assetBundle = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x38 );
            status = *reinterpret_cast< const int * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x40 );
            errorCode = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x68 );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            hashName = 0;
            status = -1;
            assetBundle = nullptr;
            errorCode = nullptr;
            return false;
        }
    }

    static bool SafeReadAssetProxyState( void * proxy, int & status,
        void *& bundleProxy, void *& errorCode ) {
        if ( !proxy )
            return false;
        __try {
            bundleProxy = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x10 );
            status = *reinterpret_cast< const int * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x38 );
            errorCode = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x108 );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            status = -1;
            bundleProxy = nullptr;
            errorCode = nullptr;
            return false;
        }
    }

    static bool SafeCallStaticBool( StaticBoolFn function, bool & value ) {
        if ( !function )
            return false;
        __try {
            value = function( nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static bool SafeCheckString( void * path, uint8_t category, bool & value ) {
        if ( !g_resourceManagerCheckString || !path )
            return false;
        __try {
            value = g_resourceManagerCheckString( path, category, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static bool SafeCheckHash( CheckHashFn function, uint64_t pathHash,
        uint8_t category, bool & value ) {
        if ( !function )
            return false;
        __try {
            value = function( pathHash, category, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static void RunMainResourceDiagnostics( ) {
        if ( g_mainDiagnosticsStarted.exchange( true ) )
            return;

        void * currentThread = nullptr;
        void * targetPath = nullptr;
        __try {
            currentThread = g_il2cppThreadCurrent
                ? g_il2cppThreadCurrent( ) : nullptr;
            targetPath = currentThread && g_il2cppStringNew
                ? g_il2cppStringNew( REPLACEMENT_POSTMODEL_PATH ) : nullptr;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            currentThread = nullptr;
            targetPath = nullptr;
        }

        if ( !currentThread || !targetPath ) {
            Log( "[main-diag] target callback was not IL2CPP-attached or path allocation failed" );
            return;
        }

        bool initialized = false;
        bool asyncIniting = false;
        bool usingBundle = false;
        bool stringExists = false;
        bool hashExists = false;
        bool rawHashExists = false;

        bool initializedCalled = SafeCallStaticBool(
            g_resourceManagerIsInitialized, initialized );
        bool asyncInitingCalled = SafeCallStaticBool(
            g_resourceManagerIsAsyncIniting, asyncIniting );
        bool usingBundleCalled = SafeCallStaticBool(
            g_resourceManagerGetUsingBundle, usingBundle );
        bool stringCalled = SafeCheckString( targetPath, ROOT_CATEGORY_MAIN,
            stringExists );
        bool hashCalled = SafeCheckHash( g_resourceManagerCheckHash,
            REPLACEMENT_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, hashExists );
        bool rawHashCalled = SafeCheckHash( g_resourceManagerRawCheckHash,
            REPLACEMENT_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, rawHashExists );

        if ( initializedCalled )
            g_resourceManagerInitialized = initialized;
        if ( asyncInitingCalled )
            g_resourceManagerAsyncIniting = asyncIniting;
        if ( usingBundleCalled )
            g_resourceManagerUsingBundle = usingBundle;
        if ( stringCalled )
            g_stringCheckExists = stringExists;
        if ( hashCalled )
            g_hashCheckExists = hashExists;
        if ( rawHashCalled )
            g_rawHashCheckExists = rawHashExists;

        char stateMessage [ 320 ] = { 0 };
        snprintf( stateMessage, sizeof( stateMessage ),
            "[main-diag] ResourceManager initialized=%s asyncIniting=%s usingBundle=%s; "
            "InitPathHash entered/completed=%s/%s MainPathHash entered/completed=%s/%s",
            BoolResult( initializedCalled, initialized ),
            BoolResult( asyncInitingCalled, asyncIniting ),
            BoolResult( usingBundleCalled, usingBundle ),
            g_initialPathHashEntered.load( ) ? "true" : "false",
            g_initialPathHashCompleted.load( ) ? "true" : "false",
            g_mainPathHashEntered.load( ) ? "true" : "false",
            g_mainPathHashCompleted.load( ) ? "true" : "false" );
        Log( stateMessage );

        char checkMessage [ 256 ] = { 0 };
        snprintf( checkMessage, sizeof( checkMessage ),
            "[main-diag] Aglina Main CheckExists string=%s hash=%s rawHash=%s",
            BoolResult( stringCalled, stringExists ),
            BoolResult( hashCalled, hashExists ),
            BoolResult( rawHashCalled, rawHashExists ) );
        Log( checkMessage );

        g_mainDiagnosticsCompleted = true;
    }

    static void TryDeferredMainLoad( );
    static void TryInstantiateLoadedPelica( );
    static void * Invoke( void * method, void * obj, void ** params,
        const char * label );
    static std::string ObjectName( void * obj );
    static void * GetTransform( void * gameObject );
    static bool SetActive( void * gameObject, bool active );
    static void DestroyObject( void * obj );
    static bool ResolveCompleteReplacementApis( );
    static bool LoadReplacementClips( );
    static bool ValidateReplacementClips( );
    static bool LoadOriginalLoginPrefabs( );
    static void ReleasePelicaPrefabRoot( );
    static void ReleaseReplacementClipRoots( );
    static void ReleaseOriginalLoginPrefabRoots( );
    static void BeginLoginReplacementSession( const char * source );
    static void SuspendLoginReplacementForReload( const char * source );
    static bool TryResumeLoginReplacementAfterReload( );
    static void CleanupLoginReplacementSession( const char * source,
        bool releaseReusableAssets );
    static void DiscoverFixedLoginActorInstances( );
    static bool AlignReplacementAnchor(
        LoginActorReplacementState & state, bool snapVertical,
        float deltaTime = 0.0f );
    static bool ApplyReplacementPhaseFacing(
        LoginActorReplacementState & state, ReplacementPhase phase );
    static bool UpdateReplacementPhaseFacing(
        LoginActorReplacementState & state );
    static bool PrepareCompleteReplacement(
        LoginActorReplacementState & state, void * targetRoot,
        void * replacementRoot );
    static bool TryEnsureActorReplacement(
        LoginActorReplacementState & state, void * preferredParent = nullptr );
    static void DestroyWalkPlayable( );
    static void DestroyActorPlayable( LoginActorReplacementState & state );
    static void RestoreOriginalRendererHierarchy( );
    static void RestoreActorRendererHierarchy(
        LoginActorReplacementState & state );
    static bool StartReplacementPhase( ReplacementPhase phase,
        const char * source );
    static bool StartActorReplacementPhase(
        LoginActorReplacementState & state, ReplacementPhase phase,
        const char * source );
    static void QueueReplacementPhase( void * controller,
        ReplacementPhase phase, const char * source );
    static bool TickReplacementWalkMixer(
        LoginActorReplacementState & state );
    static void TickReplacementSequence(
        LoginActorReplacementState & state, float deltaTime );
    static bool BindReplacement( void * targetRoot, void * replacementRoot );

    static bool ReadCompleteLoginActorController( void * instance,
        void * & targetAnimator ) {
        targetAnimator = nullptr;
        if ( !instance )
            return false;

        void * clipA1 = nullptr;
        void * clipA1s = nullptr;
        void * clipA1A2 = nullptr;
        void * clipA2 = nullptr;
        __try {
            uintptr_t base = reinterpret_cast< uintptr_t >( instance );
            targetAnimator = *reinterpret_cast< void ** >( base + 0x78 );
            clipA1 = *reinterpret_cast< void ** >( base + 0x80 );
            clipA1s = *reinterpret_cast< void ** >( base + 0x88 );
            clipA1A2 = *reinterpret_cast< void ** >( base + 0xA0 );
            clipA2 = *reinterpret_cast< void ** >( base + 0xB0 );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            targetAnimator = nullptr;
            return false;
        }
        return targetAnimator && clipA1 && clipA1s && clipA1A2 && clipA2;
    }

    static void * ControllerTargetRoot( void * targetAnimator ) {
        if ( !targetAnimator || !mComponentGetGameObject )
            return nullptr;
        return Invoke( mComponentGetGameObject, targetAnimator, nullptr,
            "Component.get_gameObject" );
    }

    static LoginActorReplacementState * FindActorSlot( void * controller,
        bool create ) {
        for ( auto & slot : g_loginActorSlots ) {
            if ( slot.controller == controller && controller )
                return &slot;
        }

        void * targetAnimator = nullptr;
        if ( !ReadCompleteLoginActorController( controller, targetAnimator ) )
            return nullptr;
        void * targetRoot = ControllerTargetRoot( targetAnimator );
        if ( create )
            BeginLoginReplacementSession( "LoginSceneAnimCtrl controller" );

        for ( auto & slot : g_loginActorSlots ) {
            if ( ( targetRoot && slot.targetRoot == targetRoot ) ||
                ( targetAnimator && slot.targetAnimator == targetAnimator ) ) {
                slot.controller = controller;
                slot.targetAnimator = targetAnimator;
                if ( targetRoot )
                    slot.targetRoot = targetRoot;
                return &slot;
            }
        }
        if ( !create )
            return nullptr;

        // The login scene has one live actor. It can be captured before the
        // Unity reflection APIs are ready, so its controller root is not yet
        // resolvable here. Attach that controller to the already captured
        // actor slot instead of manufacturing a second slot for the same root.
        auto & liveActor = g_loginActorSlots [ 0 ];
        if ( liveActor.targetRoot && !liveActor.controller ) {
            liveActor.controller = controller;
            liveActor.targetAnimator = targetAnimator;
            return &liveActor;
        }

        for ( size_t i = 0; i < g_loginActorSlots.size( ); ++i ) {
            auto & slot = g_loginActorSlots [ i ];
            if ( slot.controller || slot.targetRoot )
                continue;
            slot.controller = controller;
            slot.targetAnimator = targetAnimator;
            slot.targetRoot = targetRoot;
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-controller] registered actor slot=%zu controller=%p "
                "animator=%p root=%p name=\"%s\"",
                i, controller, targetAnimator, targetRoot,
                targetRoot ? ObjectName( targetRoot ).c_str( ) : "unresolved" );
            Log( message );
            return &slot;
        }

        Log( "[sequence-controller] ignored complete actor controller: actor slot is occupied" );
        return nullptr;
    }

    static void BeginLoginReplacementSession( const char * source ) {
        if ( g_loginSessionActive.exchange( true ) )
            return;

        uint32_t generation = g_loginSessionGeneration.fetch_add( 1 ) + 1;
        char message [ 256 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[login-session] begin generation=%u source=%s thread=%lu",
            generation, source ? source : "unknown",
            static_cast< unsigned long >( GetCurrentThreadId( ) ) );
        Log( message );
    }

    static void __fastcall LoginManagerReleaseSceneHook( void * instance,
        void * method ) {
        // Stop the injected animation graph while the login Animator and its
        // scene objects are still alive.  The original release then remains
        // free to unload the scene for either login retry or normal game entry.
        CleanupLoginReplacementSession(
            "LoginManager._ReleaseLoginScene", false );
        if ( g_originalLoginManagerReleaseScene )
            g_originalLoginManagerReleaseScene( instance, method );
    }

    static void __fastcall LoginAnimReleaseHook( void * instance,
        void * method ) {
        // This is an idempotent fallback for release paths that tear down the
        // animation component without first going through LoginManager.
        CleanupLoginReplacementSession(
            "LoginSceneAnimCtrl.OnRelease", false );
        if ( g_originalLoginAnimRelease )
            g_originalLoginAnimRelease( instance, method );
    }

    static void __fastcall LoginAnimChangeStateHook( void * instance,
        int target, void * method ) {
        if ( g_originalLoginAnimChangeState )
            g_originalLoginAnimChangeState( instance, target, method );
        if ( target == 1 )
            QueueReplacementPhase( instance,
                ReplacementPhase::SitLoop, "LoginSceneAnimCtrl.A1" );
    }

    static void __fastcall LoginAnimResetA1Hook( void * instance,
        void * method ) {
        if ( g_originalLoginAnimResetA1 )
            g_originalLoginAnimResetA1( instance, method );
        QueueReplacementPhase( instance,
            ReplacementPhase::SitLoop, "LoginSceneAnimCtrl._ResetToA1" );
    }

    static void __fastcall LoginAnimPlayA1sHook( void * instance,
        void * method ) {
        if ( g_originalLoginAnimPlayA1s )
            g_originalLoginAnimPlayA1s( instance, method );
        QueueReplacementPhase( instance, ReplacementPhase::SitSpecial,
            "LoginSceneAnimCtrl._PlayA1sAndTriggerNext" );
    }

    static void __fastcall LoginAnimPlayA1A2Hook( void * instance,
        void * method ) {
        if ( g_originalLoginAnimPlayA1A2 )
            g_originalLoginAnimPlayA1A2( instance, method );
        QueueReplacementPhase( instance, ReplacementPhase::SitToWalk,
            "LoginSceneAnimCtrl._PlayA1A2Impl" );
    }

    static void __fastcall LoginAnimTickHook( void * instance,
        float deltaTime, void * method ) {
        if ( g_loginReloadSuspended.load( ) ) {
            TryResumeLoginReplacementAfterReload( );
            if ( g_originalLoginAnimTick )
                g_originalLoginAnimTick( instance, deltaTime, method );
            return;
        }

        LoginActorReplacementState * state = FindActorSlot( instance, true );
        if ( !state ) {
            if ( g_originalLoginAnimTick )
                g_originalLoginAnimTick( instance, deltaTime, method );
            return;
        }

        void * expectedDriver = nullptr;
        g_sequenceDriverController.compare_exchange_strong(
            expectedDriver, instance );
        if ( g_sequenceDriverController.load( ) != instance ) {
            if ( g_originalLoginAnimTick )
                g_originalLoginAnimTick( instance, deltaTime, method );
            return;
        }

        DiscoverFixedLoginActorInstances( );
        for ( auto & candidate : g_loginActorSlots ) {
            if ( candidate.controller || candidate.targetRoot )
                TryEnsureActorReplacement( candidate );
        }
        bool gateRequested = state->gateRequested;
        bool replacementReady = state->replacementActive &&
            state->replacementRoot;
        bool replacementStillLoading =
            !replacementReady && !state->instantiateAttempted;

        if ( gateRequested && replacementStillLoading ) {
            constexpr uint64_t LOGIN_ANIMATION_GATE_TIMEOUT_MS = 5000;
            uint64_t startTick = state->gateStartTick;
            uint64_t elapsedMs = startTick == 0
                ? 0
                : GetTickCount64( ) - startTick;
            if ( elapsedMs < LOGIN_ANIMATION_GATE_TIMEOUT_MS ) {
                if ( !state->tickHeld ) {
                    state->tickHeld = true;
                    Log( "[sequence-gate] holding actor Tick at A1 until its replacement is ready" );
                }
                return;
            }

            state->tickHeld = false;
            if ( !state->gateTimedOut ) {
                state->gateTimedOut = true;
                Log( "[sequence-gate] timed out after 5 seconds; allowing "
                    "original Tick while waiting for replacement" );
            }

            // Keep the request armed. If loading completes later, the branch
            // below will still restart A1 against the ready replacement.
            if ( g_originalLoginAnimTick )
                g_originalLoginAnimTick( instance, deltaTime, method );
            for ( auto & candidate : g_loginActorSlots ) {
                TickReplacementSequence( candidate, deltaTime );
                AlignReplacementAnchor( candidate, false, deltaTime );
            }
            return;
        }

        if ( gateRequested ) {
            state->gateRequested = false;
            bool wasHeld = state->tickHeld;
            state->tickHeld = false;
            state->gateTimedOut = false;
            state->gateStartTick = 0;
            if ( replacementReady && g_originalLoginAnimResetA1 ) {
                // Main resources become available after the first A1 request.
                // Restart the game's A1 state now so its callbacks and our
                // replacement sequence share the same time origin.
                g_originalLoginAnimResetA1( instance, nullptr );
                int sitLoop = static_cast< int >( ReplacementPhase::SitLoop );
                g_broadcastReplacementPhase = sitLoop;
                for ( auto & candidate : g_loginActorSlots ) {
                    if ( !candidate.controller && !candidate.targetRoot )
                        continue;
                    candidate.lastRequestedPhase = sitLoop;
                    candidate.pendingPhase = sitLoop;
                }
                Log( wasHeld
                    ? "[sequence-gate] replacement ready; released Tick and restarted A1"
                    : "[sequence-gate] replacement became ready before first Tick; restarted A1" );
            }
            else {
                Log( "[sequence-gate] replacement setup did not succeed; "
                    "released original login animation" );
            }
        }

        if ( g_originalLoginAnimTick )
            g_originalLoginAnimTick( instance, deltaTime, method );
        for ( auto & candidate : g_loginActorSlots ) {
            TickReplacementSequence( candidate, deltaTime );
            AlignReplacementAnchor( candidate, false, deltaTime );
        }
    }

    static void __fastcall LoginSceneRootOnBindHook( void * instance,
        void * method ) {
        BeginLoginReplacementSession( "LoginSceneRoot.OnBindToManager" );
        g_loginBindObserved = true;
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = SetRedirectScopeDepth(
            previousState | TLS_FLAG_LOGIN_BIND_SCOPE );
        if ( !scopeSet )
            Log( "[login-capture] TLS login scope failed; actor Instantiate will not be captured" );

        if ( g_originalLoginSceneRootOnBind )
            g_originalLoginSceneRootOnBind( instance, method );

        if ( scopeSet )
            SetRedirectScopeDepth( previousState );
    }

    static void CaptureLoginActorInstance( size_t sourceIndex,
        void * original, void * instance, void * parent, void * method ) {
        if ( sourceIndex >= g_loginActorSlots.size( ) || !instance || !method )
            return;

        BeginLoginReplacementSession( "login actor instance" );

        auto & state = g_loginActorSlots [ sourceIndex ];
        if ( state.targetRoot == instance )
            return;
        if ( state.targetRoot && state.targetRoot != instance ) {
            char duplicate [ 288 ] = { 0 };
            snprintf( duplicate, sizeof( duplicate ),
                "[login-capture] actor source=%zu already has instance=%p; "
                "ignoring duplicate=%p",
                sourceIndex, state.targetRoot, instance );
            Log( duplicate );
            return;
        }

        state.sourcePrefabIndex = static_cast< int >( sourceIndex );
        state.targetRoot = instance;
        state.pendingPhase = g_broadcastReplacementPhase.load( );
        g_loginInstantiateMethod = method;
        if ( sourceIndex == 0 ) {
            g_loginActorPrefab = original;
            g_loginActorParent = parent;
            g_loginActorInstance = instance;
            g_loginInstantiateCaptured = true;
        }

        char message [ 448 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[login-capture] captured fixed actor source=%zu name=%s "
            "original=%p parent=%p instance=%p phase=%d thread=%lu",
            sourceIndex,
            "SK_actor_female",
            original, parent, instance, state.pendingPhase,
            static_cast< unsigned long >( GetCurrentThreadId( ) ) );
        Log( message );

        if ( g_pelicaPrefabResolved.load( ) && g_walkClipResolved.load( ) )
            TryEnsureActorReplacement( state, parent );
        if ( sourceIndex == 0 )
            TryInstantiateLoadedPelica( );
    }

    static void DiscoverFixedLoginActorInstances( ) {
        if ( !mGameObjectFind || !api::string_new ||
            !g_loginInstantiateMethod.load( ) )
            return;

        static constexpr std::array< const char *, MAX_LOGIN_ACTOR_SLOTS >
            cloneNames { { "SK_actor_female(Clone)" } };
        static constexpr std::array< const char *, MAX_LOGIN_ACTOR_SLOTS >
            sceneNames { { "SK_actor_female" } };

        for ( size_t i = 0; i < g_loginActorSlots.size( ); ++i ) {
            if ( g_loginActorSlots [ i ].targetRoot )
                continue;
            void * found = nullptr;
            for ( const char * candidateName : { cloneNames [ i ],
                sceneNames [ i ] } ) {
                void * name = api::string_new( candidateName );
                void * findParams [ 1 ] = { &name };
                found = Invoke( mGameObjectFind, nullptr, findParams,
                    "GameObject.Find" );
                if ( found )
                    break;
            }
            if ( !found )
                continue;

            void * transform = GetTransform( found );
            void * parent = transform
                ? Invoke( mTransformGetParent, transform, nullptr,
                    "Transform.get_parent" )
                : nullptr;
            CaptureLoginActorInstance( i,
                g_originalLoginPrefabs [ i ].load( ), found, parent,
                g_loginInstantiateMethod.load( ) );
        }
    }

    static void * __fastcall InstantiateGameObjectParentHook( void * original,
        void * parent, bool worldPositionStays, void * method ) {
        void * instance = g_originalInstantiateGameObjectParent
            ? g_originalInstantiateGameObjectParent(
                original, parent, worldPositionStays, method )
            : nullptr;

        uintptr_t scopeState = GetRedirectScopeDepth( );
        bool loginScope = ( scopeState & TLS_FLAG_LOGIN_BIND_SCOPE ) != 0;
        bool actorLoadObserved = g_redirectHit.load( );
        if ( !g_redirectEnabled.load( ) || !original || !instance || !method )
            return instance;

        int sourceIndex = -1;
        for ( size_t i = 0; i < g_originalLoginPrefabs.size( ); ++i ) {
            if ( g_originalLoginPrefabs [ i ].load( ) == original ) {
                sourceIndex = static_cast< int >( i );
                break;
            }
        }
        if ( sourceIndex < 0 && mObjectGetName ) {
            std::string originalName = ObjectName( original );
            if ( originalName == "SK_actor_female" )
                sourceIndex = 0;
        }
        if ( sourceIndex < 0 && loginScope && actorLoadObserved &&
            !g_loginInstantiateCaptured.load( ) )
            sourceIndex = 0;

        if ( sourceIndex >= 0 )
            CaptureLoginActorInstance( static_cast< size_t >( sourceIndex ),
                original, instance, parent, method );
        return instance;
    }

    static void ReleasePelicaPrefabRoot( ) {
        if ( g_pelicaPrefabGcHandle != 0 && api::gchandle_free ) {
            api::gchandle_free( g_pelicaPrefabGcHandle );
            g_pelicaPrefabGcHandle = 0;
        }
    }

    static void ReleaseReplacementClipRoots( ) {
        if ( !api::gchandle_free )
            return;
        for ( uint32_t & handle : g_replacementClipGcHandles ) {
            if ( handle != 0 ) {
                api::gchandle_free( handle );
                handle = 0;
            }
        }
    }

    static bool TryEnsureActorReplacement(
        LoginActorReplacementState & state, void * preferredParent ) {
        if ( g_loginReloadSuspended.load( ) )
            return false;
        if ( state.replacementActive && state.replacementRoot )
            return true;
        if ( state.instantiateAttempted )
            return false;

        void * prefab = g_pelicaPrefab.load( );
        void * instantiateMethod = g_loginInstantiateMethod.load( );
        if ( !prefab || !g_walkClipResolved.load( ) || !instantiateMethod ||
            !g_originalInstantiateGameObjectParent || !g_gameObjectSetActive )
            return false;
        if ( !ResolveCompleteReplacementApis( ) )
            return false;

        if ( !state.targetRoot && state.targetAnimator )
            state.targetRoot = ControllerTargetRoot( state.targetAnimator );
        if ( !state.targetRoot )
            return false;

        void * parent = preferredParent;
        if ( !parent ) {
            void * targetTransform = GetTransform( state.targetRoot );
            if ( targetTransform )
                parent = Invoke( mTransformGetParent, targetTransform, nullptr,
                    "Transform.get_parent" );
        }

        state.instantiateAttempted = true;
        g_pelicaInstantiateAttempted = true;
        void * replacement = nullptr;
        __try {
            replacement = g_originalInstantiateGameObjectParent(
                prefab, parent, false, instantiateMethod );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            replacement = nullptr;
        }

        if ( !replacement ) {
            Log( "[complete-replace] actor-slot Instantiate failed; original actor retained" );
            return false;
        }

        // Do not expose the prefab controller's default standing pose while
        // the replacement graph is being built. PrepareCompleteReplacement
        // reactivates it immediately before sampling the requested clip.
        if ( !SetActive( replacement, false ) )
            Log( "[sequence-playable] warning: replacement could not be hidden during graph setup" );

        bool prepared = false;
        __try {
            prepared = PrepareCompleteReplacement(
                state, state.targetRoot, replacement );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            prepared = false;
        }
        if ( !prepared ) {
            DestroyActorPlayable( state );
            RestoreActorRendererHierarchy( state );
            SetActive( replacement, false );
            DestroyObject( replacement );
            Log( "[complete-replace] actor-slot setup failed; original actor retained" );
            return false;
        }

        state.replacementRoot = replacement;
        state.replacementActive = true;
        if ( !AlignReplacementAnchor( state, true ) )
            Log( "[anchor-sync] initial one-shot alignment failed; "
                "replacement kept at copied root pose" );
        g_replacementActive = true;
        g_pelicaInstantiateSucceeded = true;
        if ( !g_replacementRoot ) {
            g_targetRoot = state.targetRoot;
            g_replacementRoot = replacement;
            g_pelicaInstance = replacement;
        }

        size_t slotIndex = static_cast< size_t >(
            &state - g_loginActorSlots.data( ) );
        char message [ 448 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[complete-replace] actor slot=%zu active controller=%p "
            "original=%p replacement=%p name=\"%s\"",
            slotIndex, state.controller, state.targetRoot, replacement,
            ObjectName( state.targetRoot ).c_str( ) );
        Log( message );
        return true;
    }

    static void TryInstantiateLoadedPelica( ) {
        if ( !g_redirectEnabled.load( ) ||
            !g_loginInstantiateCaptured.load( ) )
            return;

        void * prefab = g_pelicaPrefab.load( );
        void * parent = g_loginActorParent.load( );
        void * originalInstance = g_loginActorInstance.load( );
        void * instantiateMethod = g_loginInstantiateMethod.load( );
        if ( !prefab || !g_walkClipResolved.load( ) || !originalInstance ||
            !instantiateMethod ||
            !g_originalInstantiateGameObjectParent || !g_gameObjectSetActive )
            return;

        DWORD currentThreadId = GetCurrentThreadId( );
        DWORD loginThreadId = g_loginLoadThreadId.load( );
        if ( loginThreadId == 0 || currentThreadId != loginThreadId ) {
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[replace-live] skipped: callback thread changed login=%lu current=%lu",
                static_cast< unsigned long >( loginThreadId ),
                static_cast< unsigned long >( currentThreadId ) );
            Log( message );
            return;
        }
        if ( !ResolveCompleteReplacementApis( ) ) {
            Log( "[complete-replace] required Unity/IL2CPP APIs could not be resolved; original actor retained" );
            return;
        }

        LoginActorReplacementState * state = nullptr;
        for ( auto & candidate : g_loginActorSlots ) {
            if ( !candidate.targetRoot && candidate.targetAnimator )
                candidate.targetRoot = ControllerTargetRoot(
                    candidate.targetAnimator );
            if ( candidate.targetRoot == originalInstance ) {
                state = &candidate;
                break;
            }
        }
        if ( !state ) {
            for ( auto & candidate : g_loginActorSlots ) {
                if ( candidate.controller || candidate.targetRoot )
                    continue;
                candidate.targetRoot = originalInstance;
                state = &candidate;
                break;
            }
        }
        if ( !state ) {
            Log( "[complete-replace] no free actor slot for captured login actor" );
            return;
        }

        Log( "[replace-live] assets ready; preparing captured login actor slot" );
        TryEnsureActorReplacement( *state, parent );
        for ( auto & candidate : g_loginActorSlots ) {
            if ( &candidate != state &&
                ( candidate.controller || candidate.targetRoot ) )
                TryEnsureActorReplacement( candidate );
        }
    }

    static void __fastcall InitInitialPathHashHook( void * method ) {
        bool hasActiveLoginGraph = false;
        for ( const auto & slot : g_loginActorSlots ) {
            if ( slot.playableGraph.handle || slot.graphScript ) {
                hasActiveLoginGraph = true;
                break;
            }
        }
        if ( g_loginSessionActive.load( ) &&
            g_replacementActive.load( ) &&
            !g_loginReloadSuspended.load( ) ) {
            DWORD currentThreadId = GetCurrentThreadId( );
            DWORD loginThreadId = g_loginLoadThreadId.load( );
            if ( loginThreadId == 0 || currentThreadId == loginThreadId ) {
                char message [ 320 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[login-session] active replacement observed before "
                    "InitInitPathHash; cleaning reload generation on "
                    "thread=%lu loginThread=%lu graph=%s",
                    static_cast< unsigned long >( currentThreadId ),
                    static_cast< unsigned long >( loginThreadId ),
                    hasActiveLoginGraph ? "true" : "false" );
                Log( message );
                SuspendLoginReplacementForReload(
                    "InitInitPathHash reload preflight" );
            }
            else {
                char message [ 288 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[login-session] active graph reached InitInitPathHash "
                    "on non-login thread=%lu loginThread=%lu; cleanup skipped",
                    static_cast< unsigned long >( currentThreadId ),
                    static_cast< unsigned long >( loginThreadId ) );
                Log( message );
            }
        }
        g_initialPathHashEntered = true;
        Log( "[main-diag] HashStringPathProcessor.InitInitPathHash entered" );
        if ( g_originalInitInitialPathHash )
            g_originalInitInitialPathHash( method );
        g_initialPathHashCompletedTick = GetTickCount64( );
        g_initialPathHashCompleted = true;
        Log( "[main-diag] HashStringPathProcessor.InitInitPathHash completed" );
        if ( g_mainPathHashCompleted.load( ) ) {
            TryDeferredMainLoad( );
            TryResumeLoginReplacementAfterReload( );
        }
    }

    static void __fastcall InitMainPathHashHook( void * method ) {
        g_mainPathHashEntered = true;
        Log( "[main-diag] HashStringPathProcessor.InitMainPathHash entered" );
        if ( g_originalInitMainPathHash )
            g_originalInitMainPathHash( method );
        g_mainPathHashCompletedTick = GetTickCount64( );
        g_mainPathHashCompleted = true;
        Log( "[main-diag] HashStringPathProcessor.InitMainPathHash completed" );
        TryResumeLoginReplacementAfterReload( );
    }

    static uint64_t __fastcall ResolveI18NPathHashHook( uint64_t pathHash,
        uint8_t category, void * method ) {
        uint64_t resolvedHash = g_originalResolveI18NPathHash
            ? g_originalResolveI18NPathHash( pathHash, category, method )
            : pathHash;

        if ( pathHash == REPLACEMENT_POSTMODEL_PATH_HASH ) {
            g_targetResolvedHash = resolvedHash;
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[main-diag] I18N resolve category=%s(%u) input=%016llX output=%016llX%s",
                category == ROOT_CATEGORY_MAIN ? "Main" :
                    ( category == ROOT_CATEGORY_INITIAL ? "Initial" : "Other" ),
                static_cast< unsigned int >( category ),
                static_cast< unsigned long long >( pathHash ),
                static_cast< unsigned long long >( resolvedHash ),
                resolvedHash == pathHash ? " (unchanged)" :
                    ( resolvedHash == 0 ? " (zero)" : "" ) );
            Log( message );
        }
        return resolvedHash;
    }

    static bool ReadRawHandle( void * handle, uint64_t & raw0,
        uint64_t & raw1, uint32_t & raw2 ) {
        if ( !handle )
            return false;
        __try {
            memcpy( &raw0, handle, sizeof( raw0 ) );
            memcpy( &raw1, reinterpret_cast< const uint8_t * >( handle ) + 8,
                sizeof( raw1 ) );
            memcpy( &raw2, reinterpret_cast< const uint8_t * >( handle ) + 16,
                sizeof( raw2 ) );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            raw0 = 0;
            raw1 = 0;
            raw2 = 0;
            return false;
        }
    }

    static void TryDeferredMainLoad( ) {
        if ( !g_redirectEnabled.load( ) ||
            !g_deferredLoadPending.load( ) ||
            g_deferredLoadAttempted.exchange( true ) )
            return;

        g_deferredLoadPending = false;
        void * method = g_i18nLoadGameObjectMethod.load( );
        DWORD currentThreadId = GetCurrentThreadId( );
        DWORD loginThreadId = g_loginLoadThreadId.load( );
        if ( !g_originalI18NLoadGameObject || !method ) {
            Log( "[deferred-load] skipped: captured I18N Load<GameObject> method is unavailable" );
            return;
        }
        if ( loginThreadId == 0 || currentThreadId != loginThreadId ) {
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[deferred-load] skipped: callback thread changed login=%lu current=%lu",
                static_cast< unsigned long >( loginThreadId ),
                static_cast< unsigned long >( currentThreadId ) );
            Log( message );
            return;
        }

        RunMainResourceDiagnostics( );
        g_mainCategoryHit = true;
        Log( "[deferred-load] Main hashes ready; loading Aglina for the current resource generation on the captured game thread" );

        alignas( 16 ) uint8_t handleBuffer [ 24 ] = { 0 };
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = SetRedirectScopeDepth(
            previousState | TLS_FLAG_TARGET_INTERNAL );
        bool called = false;
        __try {
            g_originalI18NLoadGameObject( handleBuffer,
                REPLACEMENT_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, method );
            called = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            called = false;
        }
        if ( scopeSet )
            SetRedirectScopeDepth( previousState );

        uint64_t raw0 = 0;
        uint64_t raw1 = 0;
        uint32_t raw2 = 0;
        bool readable = called && ReadRawHandle( handleBuffer, raw0, raw1, raw2 );
        bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
        g_deferredHandleRaw0 = raw0;
        g_deferredHandleRaw1 = raw1;
        g_deferredHandleRaw2 = raw2;
        g_mainLoadReturnedHandle = nonZero;

        char message [ 288 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[deferred-load] Aglina Main load called=%s handle=%s raw=%016llX:%016llX:%08X",
            called ? "true" : "false", nonZero ? "non-zero" : "zero",
            static_cast< unsigned long long >( raw0 ),
            static_cast< unsigned long long >( raw1 ), raw2 );
        Log( message );

        if ( !nonZero || !g_assetProxyHandleGet ) {
            if ( nonZero )
                Log( "[replace-live] FAssetProxyHandle.Get is unavailable" );
            return;
        }

        void * prefab = nullptr;
        bool getCalled = false;
        __try {
            prefab = g_assetProxyHandleGet( handleBuffer, nullptr );
            getCalled = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            getCalled = false;
            prefab = nullptr;
        }
        if ( !getCalled || !prefab ) {
            char getMessage [ 224 ] = { 0 };
            snprintf( getMessage, sizeof( getMessage ),
                "[replace-live] FAssetProxyHandle.Get failed called=%s prefab=%p",
                getCalled ? "true" : "false", prefab );
            Log( getMessage );
            return;
        }

        g_pelicaPrefab = prefab;
        g_pelicaPrefabResolved = true;
        if ( api::gchandle_new )
            g_pelicaPrefabGcHandle = api::gchandle_new( prefab, false );

        char getMessage [ 256 ] = { 0 };
        snprintf( getMessage, sizeof( getMessage ),
            "[replace-live] FAssetProxyHandle.Get prefab=%p gcHandle=%u",
            prefab, g_pelicaPrefabGcHandle );
        Log( getMessage );

        if ( !ResolveCompleteReplacementApis( ) ) {
            Log( "[walk-load] Unity animation APIs are unavailable" );
            return;
        }
        if ( !LoadOriginalLoginPrefabs( ) )
            Log( "[login-prefab] warning: one or more fixed login prefabs did not preload" );
        DiscoverFixedLoginActorInstances( );
        if ( !LoadReplacementClips( ) )
            return;
        TryInstantiateLoadedPelica( );
    }

    static bool SafeAssetProxyHasError( void * proxy, bool & hasError ) {
        if ( !proxy || !g_assetProxyHasError )
            return false;
        __try {
            hasError = g_assetProxyHasError( proxy, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            hasError = false;
            return false;
        }
    }

    static void __fastcall AssetProxyFinishErrorHook( void * proxy,
        void * message, void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        std::string finishMessage = targetLoad ? ReadString( message ) : "";

        if ( g_originalAssetProxyFinishError )
            g_originalAssetProxyFinishError( proxy, message, method );

        if ( !targetLoad )
            return;

        g_assetProxyFinishErrorObserved = true;
        int assetStatus = -1;
        void * bundleProxy = nullptr;
        void * assetErrorCode = nullptr;
        bool assetReadable = SafeReadAssetProxyState( proxy, assetStatus,
            bundleProxy, assetErrorCode );

        uint64_t bundleHash = 0;
        int bundleStatus = -1;
        void * assetBundle = nullptr;
        void * bundleErrorCode = nullptr;
        bool bundleReadable = SafeReadBundleProxyState( bundleProxy, bundleHash,
            bundleStatus, assetBundle, bundleErrorCode );

        std::string assetError = ReadString( assetErrorCode );
        std::string bundleError = ReadString( bundleErrorCode );
        char output [ 896 ] = { 0 };
        snprintf( output, sizeof( output ),
            "[bundle-diag] AssetProxy._FinishWithError message=\"%.260s\" "
            "assetStatus=%s(%d) assetError=\"%.180s\" bundle=%p "
            "bundleHash=%016llX bundleStatus=%s(%d) bundleAsset=%p "
            "bundleError=\"%.180s\" stateReadable=%s/%s",
            finishMessage.c_str( ), BundleLoadStatusName( assetStatus ),
            assetStatus, assetError.c_str( ), bundleProxy,
            static_cast< unsigned long long >( bundleHash ),
            BundleLoadStatusName( bundleStatus ), bundleStatus, assetBundle,
            bundleError.c_str( ), assetReadable ? "true" : "false",
            bundleReadable ? "true" : "false" );
        Log( output );
    }

    static bool __fastcall BundleProxyLoadSyncHook( void * proxy,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        bool loaded = g_originalBundleProxyLoadSync &&
            g_originalBundleProxyLoadSync( proxy, method );

        if ( !targetLoad )
            return loaded;

        ++g_bundleSyncCount;
        uint64_t hashName = 0;
        int status = -1;
        void * assetBundle = nullptr;
        void * errorCode = nullptr;
        bool readable = SafeReadBundleProxyState( proxy, hashName, status,
            assetBundle, errorCode );
        bool failed = !loaded || !readable || status == 3;
        if ( failed )
            ++g_bundleSyncFailureCount;

        if ( failed || hashName == REPLACEMENT_POSTMODEL_BUNDLE_HASH ) {
            std::string error = ReadString( errorCode );
            char output [ 512 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] BundleProxy.LoadSync kind=%s hash=%016llX "
                "result=%s status=%s(%d) assetBundle=%p error=\"%.240s\" "
                "stateReadable=%s",
                hashName == REPLACEMENT_POSTMODEL_BUNDLE_HASH ? "target" : "dependency",
                static_cast< unsigned long long >( hashName ),
                loaded ? "true" : "false", BundleLoadStatusName( status ),
                status, assetBundle, error.c_str( ), readable ? "true" : "false" );
            Log( output );
        }
        return loaded;
    }

    static void * __fastcall BundleProxyGetFilePathHook( void * proxy,
        void * bundleName, void * method ) {
        void * fullPath = g_originalBundleProxyGetFilePath
            ? g_originalBundleProxyGetFilePath( proxy, bundleName, method )
            : nullptr;
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        if ( !targetLoad )
            return fullPath;

        uint64_t hashName = 0;
        int status = -1;
        void * assetBundle = nullptr;
        void * errorCode = nullptr;
        bool readable = SafeReadBundleProxyState( proxy, hashName, status,
            assetBundle, errorCode );
        if ( readable && hashName == REPLACEMENT_POSTMODEL_BUNDLE_HASH ) {
            g_mainBundlePathObserved = true;
            std::string name = ReadString( bundleName );
            std::string path = ReadString( fullPath );
            char output [ 768 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] target bundle path name=\"%.260s\" fullPath=\"%.420s\"",
                name.c_str( ), path.c_str( ) );
            Log( output );
        }
        return fullPath;
    }

    static void * __fastcall BundleProxyLoadFileHook( void * filePath,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        std::string path = targetLoad ? ReadString( filePath ) : "";
        void * assetBundle = g_originalBundleProxyLoadFile
            ? g_originalBundleProxyLoadFile( filePath, method )
            : nullptr;

        if ( !targetLoad )
            return assetBundle;

        ++g_bundleFileLoadCount;
        bool failed = assetBundle == nullptr;
        if ( failed )
            ++g_bundleFileFailureCount;
        bool mainBundle = path.find( "ca788b482b3c7b99d02c5057.ab" ) !=
            std::string::npos;
        if ( failed || mainBundle ) {
            char output [ 768 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] AssetBundle.LoadFromFile kind=%s result=%p path=\"%.560s\"",
                mainBundle ? "target" : "dependency", assetBundle,
                path.c_str( ) );
            Log( output );
        }
        return assetBundle;
    }

    static bool __fastcall TryGetAssetInfoHashHook( void * manifest,
        uint64_t pathHash, void * assetInfo, void * method ) {
        bool found = g_originalTryGetAssetInfoHash &&
            g_originalTryGetAssetInfoHash( manifest, pathHash, assetInfo, method );
        if ( pathHash == REPLACEMENT_POSTMODEL_PATH_HASH ) {
            bool loadStage =
                ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
            if ( loadStage )
                g_tryGetAssetInfoLoadObserved = true;
            Log( std::string( "[load-diag] TryGetAssetInfoFromPath source=" ) +
                ( loadStage ? "load" : "probe" ) +
                " result=" + ( found ? "true" : "false" ) );
        }
        return found;
    }

    static void * __fastcall BundleLoaderLoadAssetHook( void * manager,
        void * assetInfo, void * type, bool isAsync, int priority,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        void * proxy = g_originalBundleLoaderLoadAsset
            ? g_originalBundleLoaderLoadAsset( manager, assetInfo, type, isAsync,
                priority, method )
            : nullptr;

        if ( targetLoad ) {
            g_bundleLoaderObserved = true;
            bool hasError = false;
            bool errorCalled = SafeAssetProxyHasError( proxy, hasError );
            g_bundleLoaderReturnedProxy = proxy != nullptr;
            if ( errorCalled )
                g_bundleLoaderProxyHasError = hasError;
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] BundleLoader.Manager.LoadAsset proxy=%p hasError=%s "
                "isAsync=%s priority=%d",
                proxy, BoolResult( errorCalled, hasError ),
                isAsync ? "true" : "false", priority );
            Log( message );
        }
        return proxy;
    }

    static void * __fastcall LoadAssetInternalHashHook( void * manager,
        uint64_t pathHash, void * type, uint8_t category, bool isAsync,
        int priority, void * method ) {
        bool targetLoad = pathHash == REPLACEMENT_POSTMODEL_PATH_HASH &&
            category == ROOT_CATEGORY_MAIN;
        uintptr_t previousState = GetRedirectScopeDepth( );
        if ( targetLoad )
            SetRedirectScopeDepth( previousState | TLS_FLAG_TARGET_INTERNAL );

        void * proxy = g_originalLoadAssetInternalHash
            ? g_originalLoadAssetInternalHash( manager, pathHash, type, category,
                isAsync, priority, method )
            : nullptr;

        if ( targetLoad ) {
            g_loadAssetInternalObserved = true;
            g_loadAssetInternalReturnedProxy = proxy != nullptr;
            bool hasError = false;
            bool errorCalled = SafeAssetProxyHasError( proxy, hasError );
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] BundleResourceManager._LoadAssetInternal proxy=%p "
                "hasError=%s isAsync=%s priority=%d",
                proxy, BoolResult( errorCalled, hasError ),
                isAsync ? "true" : "false", priority );
            Log( message );
            SetRedirectScopeDepth( previousState );
        }
        return proxy;
    }

    static void * __fastcall LoadUntrackedHashHook( void * result,
        void * manager, uint64_t pathHash, void * type, uint8_t category,
        void * method ) {
        void * returned = g_originalLoadUntrackedHash
            ? g_originalLoadUntrackedHash( result, manager, pathHash, type,
                category, method )
            : result;

        if ( pathHash == REPLACEMENT_POSTMODEL_PATH_HASH &&
            category == ROOT_CATEGORY_MAIN ) {
            g_loadUntrackedObserved = true;
            uint64_t raw0 = 0;
            uint64_t raw1 = 0;
            uint32_t raw2 = 0;
            bool readable = ReadRawHandle( result, raw0, raw1, raw2 );
            bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
            bool directValid = false;
            bool directValidCalled = false;
            if ( readable && g_untrackedHandleIsValid ) {
                __try {
                    directValid = g_untrackedHandleIsValid( result, nullptr );
                    directValidCalled = true;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    directValid = false;
                    directValidCalled = false;
                }
            }

            g_loadUntrackedReturnedHandle = nonZero;
            if ( directValidCalled )
                g_loadUntrackedDirectIsValid = directValid;

            char message [ 288 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] LoadUntracked raw=%016llX:%016llX:%08X "
                "nonZero=%s directIsValid=%s",
                static_cast< unsigned long long >( raw0 ),
                static_cast< unsigned long long >( raw1 ), raw2,
                nonZero ? "true" : "false",
                BoolResult( directValidCalled, directValid ) );
            Log( message );

            uintptr_t state = GetRedirectScopeDepth( );
            SetRedirectScopeDepth( state | TLS_FLAG_EXPECT_HANDLE_GATE );
        }
        return returned;
    }

    static bool __fastcall UntrackedHandleGateHook( void * handle,
        void * method ) {
        uintptr_t state = GetRedirectScopeDepth( );
        bool expected = ( state & TLS_FLAG_EXPECT_HANDLE_GATE ) != 0;
        bool valid = g_originalUntrackedHandleGate &&
            g_originalUntrackedHandleGate( handle, method );
        if ( expected ) {
            g_untrackedHandleGateObserved = true;
            g_untrackedHandleGatePassed = valid;
            Log( valid
                ? "[load-diag] ResourceManager untracked-handle gate=true"
                : "[load-diag] ResourceManager untracked-handle gate=false (outer Load will zero the handle)" );
            SetRedirectScopeDepth( state & ~TLS_FLAG_EXPECT_HANDLE_GATE );
        }
        return valid;
    }

    static void * __fastcall I18NLoadGameObjectHook( void * result,
        uint64_t pathHash, uint8_t category, void * method ) {
        uint8_t effectiveCategory = category;
        uintptr_t scopeState = GetRedirectScopeDepth( );
        bool redirectScope = ( scopeState & 1U ) != 0;
        bool captureLoginLoad =
            ( scopeState & TLS_FLAG_CAPTURE_LOGIN_LOAD ) != 0;
        bool targetHash = pathHash == REPLACEMENT_POSTMODEL_PATH_HASH;
        bool categoryBridged = false;

        if ( captureLoginLoad && category == ROOT_CATEGORY_INITIAL ) {
            g_i18nLoadGameObjectMethod = method;
            g_loginLoadThreadId = GetCurrentThreadId( );
            if ( !g_loginLoadContextCaptured.exchange( true ) ) {
                char message [ 224 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[deferred-load] captured login Load<GameObject> context method=%p thread=%lu",
                    method, static_cast< unsigned long >(
                        GetCurrentThreadId( ) ) );
                Log( message );
            }
        }

        if ( g_redirectEnabled.load( ) &&
            category == ROOT_CATEGORY_INITIAL &&
            ( redirectScope || targetHash ) ) {
            effectiveCategory = ROOT_CATEGORY_MAIN;
            categoryBridged = true;
            g_mainCategoryHit = true;
            if ( redirectScope )
                SetRedirectScopeDepth( scopeState + 1 );

            char message [ 192 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[redirect] I18N Load<GameObject> category Initial(1) -> Main(0), pathHash=%016llX%s",
                static_cast< unsigned long long >( pathHash ),
                targetHash ? "" : " (redirect scope)" );
            Log( message );
        }

        if ( categoryBridged && targetHash )
            RunMainResourceDiagnostics( );

        if ( !g_originalI18NLoadGameObject )
            return result;

        void * returned = g_originalI18NLoadGameObject( result, pathHash,
            effectiveCategory, method );

        if ( categoryBridged ) {
            uint64_t raw0 = 0;
            uint64_t raw1 = 0;
            uint32_t raw2 = 0;
            bool readable = false;
            __try {
                memcpy( &raw0, result, sizeof( raw0 ) );
                memcpy( &raw1, reinterpret_cast< const uint8_t * >( result ) + 8,
                    sizeof( raw1 ) );
                memcpy( &raw2, reinterpret_cast< const uint8_t * >( result ) + 16,
                    sizeof( raw2 ) );
                readable = true;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                readable = false;
            }

            if ( readable ) {
                bool nonZero = raw0 != 0 || raw1 != 0 || raw2 != 0;
                g_mainLoadReturnedHandle = nonZero;
                char message [ 224 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[redirect] Main load returned %s handle raw=%016llX:%016llX:%08X",
                    nonZero ? "non-zero" : "zero",
                    static_cast< unsigned long long >( raw0 ),
                    static_cast< unsigned long long >( raw1 ), raw2 );
                Log( message );
            }
            else {
                Log( "[redirect] Main load handle buffer was unreadable" );
            }
        }
        return returned;
    }

    static void * __fastcall CachedLoadGameObjectHook( void * result,
        void * loader, void * path, void * method ) {
        if ( !g_originalCachedLoadGameObject )
            return result;

        bool captureLoginLoad = g_redirectEnabled.load( ) &&
            RawStringEqualsAscii( path, LOGIN_SK_ACTOR_F );
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = !captureLoginLoad || SetRedirectScopeDepth(
            previousState | TLS_FLAG_CAPTURE_LOGIN_LOAD );
        if ( captureLoginLoad ) {
            BeginLoginReplacementSession( "fixed login actor load" );
            g_redirectHit = true;
            g_deferredLoadPending = true;
            Log( "[deferred-load] fixed login actor prefab observed; Aglina load queued until Main initialization" );
            if ( !scopeSet )
                Log( "[deferred-load] TLS context capture failed; original female actor will still load" );
        }

        void * returned = g_originalCachedLoadGameObject(
            result, loader, path, method );

        if ( captureLoginLoad && scopeSet )
            SetRedirectScopeDepth( previousState );
        return returned;
    }

    static void * FindImageByName( const char * imageName ) {
        if ( !api::get_domain || !api::get_assemblies || !api::assembly_get_image ||
            !api::image_get_name )
            return nullptr;

        void * domain = api::get_domain( );
        if ( !domain )
            return nullptr;

        size_t count = 0;
        void ** assemblies = api::get_assemblies( domain, &count );
        if ( !assemblies )
            return nullptr;

        for ( size_t i = 0; i < count; ++i ) {
            void * image = assemblies [ i ] ? api::assembly_get_image( assemblies [ i ] ) : nullptr;
            const char * name = image ? api::image_get_name( image ) : nullptr;
            if ( name && strcmp( name, imageName ) == 0 )
                return image;
        }
        return nullptr;
    }

    static void * FindClass( const char * namespaze, const char * name ) {
        if ( !api::get_domain || !api::get_assemblies || !api::class_from_name )
            return nullptr;

        if ( namespaze && strncmp( namespaze, "UnityEngine", 11 ) == 0 ) {
            const char * unityImages [ ] = {
                "UnityEngine.CoreModule.dll",
                "UnityEngine.AnimationModule.dll",
                "UnityEngine.GameObjectModule.dll",
                "UnityEngine.dll",
                nullptr
            };
            for ( int i = 0; unityImages [ i ]; ++i ) {
                void * image = FindImageByName( unityImages [ i ] );
                if ( !image )
                    continue;
                void * klass = api::class_from_name( image, namespaze, name );
                if ( klass ) {
                    Log( std::string( "[replace] resolved " ) + namespaze + "." +
                        name + " from " + unityImages [ i ] );
                    return klass;
                }
            }
        }

        void * domain = api::get_domain( );
        size_t count = 0;
        void ** assemblies = domain ? api::get_assemblies( domain, &count ) : nullptr;
        if ( !assemblies )
            return nullptr;

        for ( size_t i = 0; i < count; ++i ) {
            void * image = assemblies [ i ] ? api::assembly_get_image( assemblies [ i ] ) : nullptr;
            if ( !image )
                continue;
            void * klass = api::class_from_name( image, namespaze, name );
            if ( klass )
                return klass;
        }
        return nullptr;
    }

    static bool Contains( const char * text, const char * expected ) {
        return !expected || ( text && strstr( text, expected ) != nullptr );
    }

    static void * FindMethod( void * klass, const char * name, int argc,
        const char * firstParamContains = nullptr,
        const char * returnContains = nullptr,
        bool requireNonGeneric = false ) {
        if ( !klass || !api::class_get_methods || !api::method_get_name ||
            !api::method_get_param_count )
            return nullptr;

        void * iter = nullptr;
        while ( void * method = api::class_get_methods( klass, &iter ) ) {
            const char * methodName = api::method_get_name( method );
            if ( !methodName || strcmp( methodName, name ) != 0 )
                continue;
            if ( static_cast< int >( api::method_get_param_count( method ) ) != argc )
                continue;
            if ( requireNonGeneric && api::method_is_generic && api::method_is_generic( method ) )
                continue;

            if ( firstParamContains ) {
                const void * param = argc > 0 && api::method_get_param
                    ? api::method_get_param( method, 0 ) : nullptr;
                const char * paramName = param && api::type_get_name
                    ? api::type_get_name( const_cast< void * >( param ) ) : nullptr;
                if ( !Contains( paramName, firstParamContains ) )
                    continue;
            }

            if ( returnContains ) {
                void * ret = api::method_get_return_type
                    ? api::method_get_return_type( method ) : nullptr;
                const char * retName = ret && api::type_get_name
                    ? api::type_get_name( ret ) : nullptr;
                if ( !Contains( retName, returnContains ) )
                    continue;
            }
            return method;
        }
        return nullptr;
    }

    static void * Invoke( void * method, void * obj, void ** params,
        const char * label ) {
        if ( !method || !api::runtime_invoke ) {
            Log( std::string( "[replace] unresolved method: " ) + label );
            return nullptr;
        }

        void * exception = nullptr;
        void * result = nullptr;
        __try {
            result = api::runtime_invoke( method, obj, params, &exception );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( std::string( "[replace] native exception in " ) + label );
            return nullptr;
        }
        if ( exception ) {
            Log( std::string( "[replace] managed exception in " ) + label );
            return nullptr;
        }
        return result;
    }

    static bool InvokeVoid( void * method, void * obj, void ** params,
        const char * label ) {
        if ( !method || !api::runtime_invoke ) {
            Log( std::string( "[replace] unresolved method: " ) + label );
            return false;
        }

        void * exception = nullptr;
        __try {
            api::runtime_invoke( method, obj, params, &exception );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( std::string( "[replace] native exception in " ) + label );
            return false;
        }
        if ( exception ) {
            Log( std::string( "[replace] managed exception in " ) + label );
            return false;
        }
        return true;
    }

    static std::string ReadString( void * str ) {
        if ( !str || !api::string_length_fn || !api::string_chars )
            return "";
        int len = 0;
        wchar_t * chars = nullptr;
        __try {
            len = api::string_length_fn( str );
            chars = api::string_chars( str );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return "";
        }
        if ( len <= 0 || !chars )
            return "";

        int size = WideCharToMultiByte( CP_UTF8, 0, chars, len, nullptr, 0,
            nullptr, nullptr );
        if ( size <= 0 || size > ( 1 << 20 ) )
            return "";
        std::string out( static_cast< size_t >( size ), '\0' );
        WideCharToMultiByte( CP_UTF8, 0, chars, len, out.data( ), size,
            nullptr, nullptr );
        return out;
    }

    static std::string ObjectName( void * obj ) {
        return ReadString( Invoke( mObjectGetName, obj, nullptr, "Object.get_name" ) );
    }

    static std::string ObjectClassName( void * obj ) {
        if ( !obj || !api::object_get_class || !api::class_get_name )
            return "";
        void * klass = api::object_get_class( obj );
        const char * name = klass ? api::class_get_name( klass ) : nullptr;
        return name ? name : "";
    }

    template < typename T > static bool Unbox( void * boxed, T & value ) {
        if ( !boxed || !api::object_unbox )
            return false;
        __try {
            T * ptr = reinterpret_cast< T * >( api::object_unbox( boxed ) );
            if ( !ptr )
                return false;
            value = *ptr;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
        return true;
    }

    static int ArrayLength( void * array ) {
        if ( !array || !api::array_length )
            return 0;
        __try {
            return api::array_length( array );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return 0;
        }
    }

    static void * ArrayElement( void * array, int index ) {
        if ( !array )
            return nullptr;
        __try {
            void ** vector = reinterpret_cast< void ** >(
                reinterpret_cast< char * >( array ) + 32 );
            return vector [ index ];
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    static void SetArrayElement( void * array, int index, void * value ) {
        void ** vector = reinterpret_cast< void ** >(
            reinterpret_cast< char * >( array ) + 32 );
        void ** slot = &vector [ index ];
        if ( api::gc_wbarrier_set_field )
            api::gc_wbarrier_set_field( array, slot, value );
        else
            *slot = value;
    }

    static void * TypeObject( void * klass ) {
        if ( !klass || !api::class_get_type || !api::type_get_object )
            return nullptr;
        void * type = api::class_get_type( klass );
        return type ? api::type_get_object( type ) : nullptr;
    }

    static void * GetComponentsInChildren( void * root, void * componentClass ) {
        void * typeObject = TypeObject( componentClass );
        if ( !root || !typeObject || !g_gameObjectGetComponentsInChildren )
            return nullptr;

        void * components = nullptr;
        __try {
            components = g_gameObjectGetComponentsInChildren(
                root, typeObject, true, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "GameObject.GetComponentsInChildren" );
            return nullptr;
        }
        if ( !components )
            Log( "[replace] direct GameObject.GetComponentsInChildren returned null" );
        return components;
    }

    static void * FindTransformByName( void * root, const char * name ) {
        if ( !root || !name )
            return nullptr;
        void * transforms = GetComponentsInChildren( root, clsTransform );
        int count = ArrayLength( transforms );
        for ( int i = 0; i < count; ++i ) {
            void * transform = ArrayElement( transforms, i );
            if ( transform && ObjectName( transform ) == name )
                return transform;
        }
        return nullptr;
    }

    static bool AlignReplacementAnchor(
        LoginActorReplacementState & state, bool snapVertical,
        float deltaTime ) {
        if ( !state.replacementActive || !state.replacementTransform ||
            !state.originalAnchor || !state.replacementAnchor ||
            !mGetPosition || !mSetPosition )
            return false;

        Vector3 originalPosition { };
        Vector3 replacementPosition { };
        Vector3 rootPosition { };
        if ( !Unbox( Invoke( mGetPosition, state.originalAnchor, nullptr,
                "Transform.get_position(original anchor)" ), originalPosition ) ||
            !Unbox( Invoke( mGetPosition, state.replacementAnchor, nullptr,
                "Transform.get_position(replacement anchor)" ), replacementPosition ) ||
            !Unbox( Invoke( mGetPosition, state.replacementTransform, nullptr,
                "Transform.get_position(replacement root)" ), rootPosition ) )
            return false;

        float verticalError = originalPosition.y - replacementPosition.y;
        float verticalCorrection = verticalError;
        if ( !snapVertical ) {
            float safeDeltaTime = std::clamp( deltaTime, 0.0f, 0.1f );
            float response = REPLACEMENT_VERTICAL_FOLLOW_RESPONSE_SECONDS;
            float alpha = response > 0.0f
                ? 1.0f - std::exp( -safeDeltaTime / response ) : 1.0f;
            verticalCorrection *= alpha;
        }

        Vector3 alignedPosition {
            rootPosition.x + originalPosition.x - replacementPosition.x,
            rootPosition.y + verticalCorrection,
            rootPosition.z + originalPosition.z - replacementPosition.z
        };
        void * positionParams [ 1 ] = { &alignedPosition };
        if ( !InvokeVoid( mSetPosition, state.replacementTransform,
                positionParams, "Transform.set_position(replacement root)" ) )
            return false;

        if ( snapVertical && !state.anchorAlignmentLogged ) {
            state.anchorAlignmentLogged = true;
            char message [ 352 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[anchor-sync] one-shot aligned Bip001_Pelvis; "
                "exact XZ and smoothed Y tracking enabled "
                "delta=(%.3f,%.3f,%.3f) "
                "replacementRoot=(%.3f,%.3f,%.3f)",
                originalPosition.x - replacementPosition.x,
                originalPosition.y - replacementPosition.y,
                originalPosition.z - replacementPosition.z,
                alignedPosition.x, alignedPosition.y, alignedPosition.z );
            Log( message );
        }
        return true;
    }

    static bool SetReplacementLocalYaw(
        LoginActorReplacementState & state, float yawDegrees ) {
        if ( !state.replacementTransform || !mSetLocalRotation )
            return false;

        constexpr float PI = 3.14159265358979323846f;
        float halfRadians = yawDegrees * PI / 360.0f;
        Quaternion rotation { 0.0f, std::sin( halfRadians ), 0.0f,
            std::cos( halfRadians ) };
        void * rotationParams [ 1 ] = { &rotation };
        return InvokeVoid( mSetLocalRotation, state.replacementTransform,
            rotationParams, "Transform.set_localRotation(phase facing)" );
    }

    static bool ApplyReplacementPhaseFacing(
        LoginActorReplacementState & state, ReplacementPhase phase ) {
        // The replacement sit clips need a -120 degree basis correction, while
        // the final locomotion clip is authored at zero. Start the transition
        // at -120 and blend it out before the final phase begins.
        float yawDegrees = phase == ReplacementPhase::WalkLong
            ? 0.0f : LOGIN_SIT_START_YAW_DEGREES;
        if ( !SetReplacementLocalYaw( state, yawDegrees ) )
            return false;

        const ReplacementClipSpec & spec = REPLACEMENT_CLIPS [
            static_cast< size_t >( phase ) ];
        if ( phase == ReplacementPhase::SitToWalk ) {
            char message [ 256 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-facing] phase=%s localYaw=%.1f -> 0.0 "
                "smoothDuration=%.3fs",
                spec.label, LOGIN_SIT_START_YAW_DEGREES,
                LOGIN_CAMERA_TURN_DURATION_SECONDS );
            Log( message );
        }
        else {
            char message [ 192 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-facing] phase=%s localYaw=%.1f",
                spec.label, yawDegrees );
            Log( message );
        }
        return true;
    }

    static bool UpdateReplacementPhaseFacing(
        LoginActorReplacementState & state ) {
        if ( state.activePhase !=
            static_cast< int >( ReplacementPhase::SitToWalk ) )
            return true;

        double normalized = std::clamp(
            state.activeClipElapsed / LOGIN_CAMERA_TURN_DURATION_SECONDS,
            0.0, 1.0 );
        // Smoothstep avoids an angular-velocity jump at either endpoint.
        double eased = normalized * normalized * ( 3.0 - 2.0 * normalized );
        float yawDegrees = static_cast< float >(
            LOGIN_SIT_START_YAW_DEGREES * ( 1.0 - eased ) );
        if ( !SetReplacementLocalYaw( state, yawDegrees ) )
            return false;
        return true;
    }

    static bool LoadOriginalLoginPrefabs( ) {
        static constexpr std::array< uint64_t, MAX_LOGIN_ACTOR_SLOTS > hashes { {
            LOGIN_SK_ACTOR_F_HASH
        } };
        static constexpr std::array< const char *, MAX_LOGIN_ACTOR_SLOTS > names { {
            "SK_actor_female"
        } };

        void * typeObject = TypeObject( clsGameObject );
        if ( !typeObject || !g_resourceManagerLoadHashType ||
            !g_assetProxyHandleGet ) {
            Log( "[login-prefab] GameObject type or resource APIs are unavailable" );
            return false;
        }

        bool allLoaded = true;
        for ( size_t i = 0; i < hashes.size( ); ++i ) {
            if ( g_originalLoginPrefabs [ i ].load( ) )
                continue;

            alignas( 16 ) uint8_t handleBuffer [ 24 ] = { 0 };
            bool called = false;
            __try {
                g_resourceManagerLoadHashType( handleBuffer, hashes [ i ],
                    typeObject, ROOT_CATEGORY_INITIAL, 10, nullptr );
                called = true;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                called = false;
            }

            uint64_t raw0 = 0;
            uint64_t raw1 = 0;
            uint32_t raw2 = 0;
            bool readable = called && ReadRawHandle(
                handleBuffer, raw0, raw1, raw2 );
            bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
            void * prefab = nullptr;
            if ( nonZero ) {
                __try {
                    prefab = g_assetProxyHandleGet( handleBuffer, nullptr );
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    prefab = nullptr;
                }
            }

            if ( prefab ) {
                g_originalLoginPrefabs [ i ] = prefab;
                if ( api::gchandle_new )
                    g_originalLoginPrefabGcHandles [ i ] =
                        api::gchandle_new( prefab, false );
            }
            else {
                allLoaded = false;
            }

            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[login-prefab] source=%zu name=%s hash=%016llX "
                "handle=%s prefab=%p gcHandle=%u",
                i, names [ i ],
                static_cast< unsigned long long >( hashes [ i ] ),
                nonZero ? "non-zero" : "zero", prefab,
                g_originalLoginPrefabGcHandles [ i ] );
            Log( message );
        }
        return allLoaded;
    }

    static void ReleaseOriginalLoginPrefabRoots( ) {
        if ( api::gchandle_free ) {
            for ( uint32_t & handle : g_originalLoginPrefabGcHandles ) {
                if ( handle )
                    api::gchandle_free( handle );
                handle = 0;
            }
        }
        for ( auto & prefab : g_originalLoginPrefabs )
            prefab = nullptr;
    }

    static bool LoadReplacementClip( size_t index ) {
        if ( index >= REPLACEMENT_CLIPS.size( ) )
            return false;
        if ( g_replacementClips [ index ].load( ) )
            return true;

        const ReplacementClipSpec & spec = REPLACEMENT_CLIPS [ index ];
        void * typeObject = TypeObject( clsAnimationClip );
        if ( !typeObject || !g_resourceManagerLoadHashType ||
            !g_assetProxyHandleGet ) {
            Log( "[sequence-load] AnimationClip type or resource APIs are unavailable" );
            return false;
        }

        alignas( 16 ) uint8_t handleBuffer [ 24 ] = { 0 };
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = SetRedirectScopeDepth(
            previousState | TLS_FLAG_TARGET_INTERNAL );
        bool called = false;
        __try {
            g_resourceManagerLoadHashType( handleBuffer,
                spec.pathHash, typeObject,
                ROOT_CATEGORY_MAIN, 10, nullptr );
            called = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            called = false;
        }
        if ( scopeSet )
            SetRedirectScopeDepth( previousState );

        uint64_t raw0 = 0;
        uint64_t raw1 = 0;
        uint32_t raw2 = 0;
        bool readable = called && ReadRawHandle(
            handleBuffer, raw0, raw1, raw2 );
        bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
        char loadMessage [ 384 ] = { 0 };
        snprintf( loadMessage, sizeof( loadMessage ),
            "[sequence-load] phase=%s called=%s handle=%s "
            "hash=%016llX raw=%016llX:%016llX:%08X",
            spec.label, called ? "true" : "false",
            nonZero ? "non-zero" : "zero",
            static_cast< unsigned long long >( spec.pathHash ),
            static_cast< unsigned long long >( raw0 ),
            static_cast< unsigned long long >( raw1 ), raw2 );
        Log( loadMessage );
        if ( !nonZero )
            return false;

        void * clip = nullptr;
        bool getCalled = false;
        __try {
            clip = g_assetProxyHandleGet( handleBuffer, nullptr );
            getCalled = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            getCalled = false;
            clip = nullptr;
        }
        if ( !getCalled || !clip ) {
            Log( std::string( "[sequence-load] phase=" ) + spec.label +
                " FAssetProxyHandle.Get returned no AnimationClip" );
            return false;
        }

        g_replacementClips [ index ] = clip;
        if ( api::gchandle_new )
            g_replacementClipGcHandles [ index ] =
                api::gchandle_new( clip, false );
        char clipMessage [ 512 ] = { 0 };
        snprintf( clipMessage, sizeof( clipMessage ),
            "[sequence-load] phase=%s clip=%p gcHandle=%u speed=%.2f "
            "loop=%s path=%s",
            spec.label, clip, g_replacementClipGcHandles [ index ],
            spec.speed, spec.loop ? "true" : "false", spec.path );
        Log( clipMessage );
        return true;
    }

    static bool LoadReplacementClips( ) {
        if ( g_walkClipResolved.load( ) )
            return true;
        if ( g_walkClipLoadAttempted.exchange( true ) )
            return false;

        for ( size_t i = 0; i < REPLACEMENT_CLIPS.size( ); ++i ) {
            if ( !LoadReplacementClip( i ) ) {
                Log( "[sequence-load] incomplete sequence; original actor will be retained" );
                ReleaseReplacementClipRoots( );
                for ( auto & clip : g_replacementClips )
                    clip = nullptr;
                return false;
            }
        }
        if ( !ValidateReplacementClips( ) ) {
            Log( "[sequence-load] clip wrappers are present but their native "
                "assets are not ready; playback remains suspended" );
            ReleaseReplacementClipRoots( );
            for ( auto & clip : g_replacementClips )
                clip = nullptr;
            return false;
        }
        g_walkClipResolved = true;
        Log( "[sequence-load] all four replacement sequence clips resolved" );
        return true;
    }

    static bool ValidateReplacementClips( ) {
        if ( !mClipGetLength || !mClipGetEmpty || !mClipGetHumanMotion )
            return false;

        for ( size_t i = 0; i < REPLACEMENT_CLIPS.size( ); ++i ) {
            void * clip = g_replacementClips [ i ].load( );
            float length = -1.0f;
            bool empty = true;
            bool humanMotion = false;
            bool lengthReadable = clip && Unbox( Invoke( mClipGetLength,
                clip, nullptr, "AnimationClip.get_length" ), length );
            bool emptyReadable = clip && Unbox( Invoke( mClipGetEmpty,
                clip, nullptr, "AnimationClip.get_empty" ), empty );
            bool humanReadable = clip && Unbox( Invoke( mClipGetHumanMotion,
                clip, nullptr, "AnimationClip.get_humanMotion" ), humanMotion );
            if ( lengthReadable && emptyReadable && humanReadable &&
                length > 0.0f && !empty && humanMotion )
                continue;

            char message [ 288 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-load] phase=%s native-ready=false "
                "lengthReadable=%s length=%.3f emptyReadable=%s empty=%s "
                "humanReadable=%s humanMotion=%s",
                REPLACEMENT_CLIPS [ i ].label,
                lengthReadable ? "true" : "false", length,
                emptyReadable ? "true" : "false", empty ? "true" : "false",
                humanReadable ? "true" : "false",
                humanMotion ? "true" : "false" );
            Log( message );
            return false;
        }
        return true;
    }

    static void * GetTransform( void * gameObject ) {
        return Invoke( mGameObjectGetTransform, gameObject, nullptr,
            "GameObject.get_transform" );
    }

    static void * GetParent( void * transform ) {
        return Invoke( mTransformGetParent, transform, nullptr,
            "Transform.get_parent" );
    }

    static std::string RelativePath( void * transform, void * root ) {
        if ( !transform )
            return "";
        if ( transform == root )
            return "";

        std::vector< std::string > parts;
        void * current = transform;
        for ( int depth = 0; current && depth < 256; ++depth ) {
            if ( current == root )
                break;
            parts.push_back( ObjectName( current ) );
            current = GetParent( current );
        }
        if ( current != root )
            return ObjectName( transform );

        std::reverse( parts.begin( ), parts.end( ) );
        std::string result;
        for ( size_t i = 0; i < parts.size( ); ++i ) {
            if ( i )
                result += '/';
            result += parts [ i ];
        }
        return result;
    }

    static bool EndsWith( const std::string & value, const std::string & suffix ) {
        return suffix.size( ) <= value.size( ) &&
            value.compare( value.size( ) - suffix.size( ), suffix.size( ), suffix ) == 0;
    }

    static bool SetEnabled( void * renderer, bool enabled ) {
        if ( !renderer || !g_rendererSetEnabled )
            return false;
        __try {
            g_rendererSetEnabled( renderer, enabled, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct Renderer.set_enabled" );
            return false;
        }
        return true;
    }

    static void RestoreOriginalRendererHierarchy( ) {
        for ( void * renderer : g_disabledRenderers )
            SetEnabled( renderer, true );
        g_disabledRenderers.clear( );
        g_originalRenderersDisabled = false;
    }

    static void RestoreActorRendererHierarchy(
        LoginActorReplacementState & state ) {
        for ( void * renderer : state.disabledRenderers )
            SetEnabled( renderer, true );
        state.disabledRenderers.clear( );
    }

    static void KeepOriginalAnimatorsUpdating( void * targetRoot ) {
        void * animators = GetComponentsInChildren( targetRoot, clsAnimator );
        int animatorCount = ArrayLength( animators );
        int alwaysAnimate = 0;
        void * cullingParams [ 1 ] = { &alwaysAnimate };
        int configured = 0;
        for ( int i = 0; i < animatorCount; ++i ) {
            void * animator = ArrayElement( animators, i );
            if ( animator && InvokeVoid( mAnimatorSetCullingMode, animator,
                cullingParams, "Animator.set_cullingMode" ) )
                ++configured;
        }
        Log( "[complete-replace] original animators forced to AlwaysAnimate "
            "for login state synchronization (configured=" +
            std::to_string( configured ) +
            "/" + std::to_string( animatorCount ) + ")" );
    }

    static bool DisableOriginalRendererHierarchy(
        LoginActorReplacementState & state, void * targetRoot,
        void * excludedRoot = nullptr ) {
        KeepOriginalAnimatorsUpdating( targetRoot );
        void * renderers = GetComponentsInChildren( targetRoot, clsRenderer );
        if ( !renderers )
            return false;

        state.disabledRenderers.clear( );
        void * excludedTransform = GetTransform( excludedRoot );
        int rendererCount = ArrayLength( renderers );
        for ( int i = 0; i < rendererCount; ++i ) {
            void * renderer = ArrayElement( renderers, i );
            if ( !renderer )
                continue;

            bool belongsToExcludedRoot = false;
            if ( excludedTransform ) {
                void * rendererObject = Invoke( mComponentGetGameObject,
                    renderer, nullptr, "Component.get_gameObject" );
                void * current = GetTransform( rendererObject );
                for ( int depth = 0; current && depth < 256; ++depth ) {
                    if ( current == excludedTransform ) {
                        belongsToExcludedRoot = true;
                        break;
                    }
                    current = Invoke( mTransformGetParent, current, nullptr,
                        "Transform.get_parent" );
                }
            }
            if ( belongsToExcludedRoot )
                continue;

            if ( !SetEnabled( renderer, false ) ) {
                RestoreActorRendererHierarchy( state );
                return false;
            }
            state.disabledRenderers.push_back( renderer );
        }

        g_originalRenderersDisabled = true;
        Log( "[complete-replace] original renderer hierarchy hidden; "
            "login root and animation controller remain active (count=" +
            std::to_string( state.disabledRenderers.size( ) ) + ")" );
        return true;
    }

    static bool SetBones( void * renderer, void * bones ) {
        if ( !renderer || !bones || !g_skinnedMeshRendererSetBones )
            return false;
        __try {
            g_skinnedMeshRendererSetBones( renderer, bones, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "SkinnedMeshRenderer.set_bones" );
            return false;
        }
        return true;
    }

    static bool SetRootBone( void * renderer, void * rootBone ) {
        if ( !renderer || !g_skinnedMeshRendererSetRootBone )
            return false;
        __try {
            g_skinnedMeshRendererSetRootBone( renderer, rootBone, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "SkinnedMeshRenderer.set_rootBone" );
            return false;
        }
        return true;
    }

    static bool SetActive( void * gameObject, bool active ) {
        if ( !gameObject || !g_gameObjectSetActive )
            return false;
        __try {
            g_gameObjectSetActive( gameObject, active, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct GameObject.SetActive" );
            return false;
        }
        return true;
    }

    static void DestroyWalkPlayable( ) {
        if ( g_walkGraphScript ) {
            if ( mNpcGraphDestroy )
                InvokeVoid( mNpcGraphDestroy, g_walkGraphScript, nullptr,
                    "NPCCPUAnimationGraphScript_SimpleOneState.Destory" );
            if ( g_walkGraphScriptGcHandle != 0 && api::gchandle_free )
                api::gchandle_free( g_walkGraphScriptGcHandle );
            g_walkGraphScriptGcHandle = 0;
            g_walkGraphScript = nullptr;
        }
        if ( g_walkPlayableGraph.handle && g_playableGraphDestroy ) {
            __try {
                g_playableGraphDestroy( &g_walkPlayableGraph, nullptr );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                Log( "[walk-playable] native exception while destroying graph" );
            }
        }
        g_walkPlayableGraph = { };
        g_activeReplacementPlayable = { };
        g_walkPlayableStarted = false;
    }

    static void DestroyActorPlayable( LoginActorReplacementState & state ) {
        if ( state.graphScript ) {
            if ( mNpcGraphDestroy )
                InvokeVoid( mNpcGraphDestroy, state.graphScript, nullptr,
                    "NPCCPUAnimationGraphScript_SimpleOneState.Destory" );
            if ( state.graphScriptGcHandle != 0 && api::gchandle_free )
                api::gchandle_free( state.graphScriptGcHandle );
            state.graphScriptGcHandle = 0;
            state.graphScript = nullptr;
        }
        if ( state.playableGraph.handle && g_playableGraphDestroy ) {
            __try {
                g_playableGraphDestroy( &state.playableGraph, nullptr );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                Log( "[sequence-playable] native exception while destroying actor graph" );
            }
        }
        state.playableGraph = { };
        state.activePlayable = { };
        state.walkMixer = { };
        state.walkPlayables = { };
        state.walkActiveInput = 0;
        state.walkBlendActive = false;
        state.walkMixerConfigured = false;
        state.walkMixerFaultLogged = false;
        state.walkBlendCount = 0;
    }

    static void LogAnimatorAndClipState( const char * phase,
        void * replacementRoot, void * animator, void * clip ) {
        bool enabled = false;
        bool activeAndEnabled = false;
        bool initialized = false;
        bool animatorHuman = false;
        bool boundPlayables = false;
        bool clipEmpty = false;
        bool clipLegacy = false;
        bool clipHuman = false;
        float clipLength = -1.0f;
        int cullingMode = -1;
        int updateMode = -1;

        Unbox( Invoke( mBehaviourGetEnabled, animator, nullptr,
            "Behaviour.get_enabled" ), enabled );
        Unbox( Invoke( mBehaviourGetIsActiveAndEnabled, animator, nullptr,
            "Behaviour.get_isActiveAndEnabled" ), activeAndEnabled );
        Unbox( Invoke( mAnimatorGetIsInitialized, animator, nullptr,
            "Animator.get_isInitialized" ), initialized );
        Unbox( Invoke( mAnimatorGetIsHuman, animator, nullptr,
            "Animator.get_isHuman" ), animatorHuman );
        Unbox( Invoke( mAnimatorGetHasBoundPlayables, animator, nullptr,
            "Animator.get_hasBoundPlayables" ), boundPlayables );
        void * avatar = Invoke( mAnimatorGetAvatar, animator, nullptr,
            "Animator.get_avatar" );
        Unbox( Invoke( mAnimatorGetCullingMode, animator, nullptr,
            "Animator.get_cullingMode" ), cullingMode );
        Unbox( Invoke( mAnimatorGetUpdateMode, animator, nullptr,
            "Animator.get_updateMode" ), updateMode );
        Unbox( Invoke( mClipGetLength, clip, nullptr,
            "AnimationClip.get_length" ), clipLength );
        Unbox( Invoke( mClipGetEmpty, clip, nullptr,
            "AnimationClip.get_empty" ), clipEmpty );
        Unbox( Invoke( mClipGetLegacy, clip, nullptr,
            "AnimationClip.get_legacy" ), clipLegacy );
        Unbox( Invoke( mClipGetHumanMotion, clip, nullptr,
            "AnimationClip.get_humanMotion" ), clipHuman );

        void * animatorGo = Invoke( mComponentGetGameObject, animator, nullptr,
            "Component.get_gameObject" );
        std::string animatorName = ObjectName( animatorGo );
        std::string animatorPath;
        void * animatorTransform = GetTransform( animatorGo );
        void * replacementTransform = GetTransform( replacementRoot );
        if ( animatorTransform && replacementTransform )
            animatorPath = RelativePath( animatorTransform, replacementTransform );

        char message [ 768 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[walk-diag] phase=%s animator=%p name=\"%s\" path=\"%s\" "
            "enabled=%s activeAndEnabled=%s initialized=%s isHuman=%s "
            "hasBoundPlayables=%s avatar=%p cullingMode=%d updateMode=%d "
            "clip=%p length=%.3f empty=%s legacy=%s humanMotion=%s",
            phase ? phase : "unknown", animator,
            animatorName.c_str( ), animatorPath.c_str( ),
            enabled ? "true" : "false",
            activeAndEnabled ? "true" : "false",
            initialized ? "true" : "false",
            animatorHuman ? "true" : "false",
            boundPlayables ? "true" : "false", avatar,
            cullingMode, updateMode, clip, clipLength,
            clipEmpty ? "true" : "false",
            clipLegacy ? "true" : "false",
            clipHuman ? "true" : "false" );
        Log( message );
    }

    static bool ConfigureAnimatorForWalk( void * replacementRoot,
        void * animator, void * clip ) {
        LogAnimatorAndClipState( "before-config", replacementRoot,
            animator, clip );

        bool enabled = true;
        int alwaysAnimate = 0;
        int unscaledTime = 2;
        bool writeDefaultValues = true;
        void * enabledParams [ 1 ] = { &enabled };
        void * cullingParams [ 1 ] = { &alwaysAnimate };
        void * updateModeParams [ 1 ] = { &unscaledTime };
        void * rebindParams [ 1 ] = { &writeDefaultValues };
        bool configured =
            InvokeVoid( mBehaviourSetEnabled, animator, enabledParams,
                "Behaviour.set_enabled" ) &&
            InvokeVoid( mAnimatorSetCullingMode, animator, cullingParams,
                "Animator.set_cullingMode" ) &&
            InvokeVoid( mAnimatorSetUpdateMode, animator, updateModeParams,
                "Animator.set_updateMode" ) &&
            InvokeVoid( mAnimatorRebind, animator, rebindParams,
                "Animator.Rebind(bool)" );
        LogAnimatorAndClipState( "after-config", replacementRoot,
            animator, clip );
        return configured;
    }

    static bool StartGameWalkPlayable( void * replacementRoot,
        void * animator, void * clip, int animatorCount,
        bool animatorAdded ) {
        if ( !clsNpcSimpleOneStateGraph || !api::object_new )
            return false;

        void * script = api::object_new( clsNpcSimpleOneStateGraph );
        if ( !script ) {
            Log( "[walk-gamegraph] failed to allocate SimpleOneState graph" );
            return false;
        }
        g_walkGraphScript = script;
        if ( api::gchandle_new )
            g_walkGraphScriptGcHandle = api::gchandle_new( script, false );

        int updateMode = DIRECTOR_UPDATE_MODE_UNSCALED_GAME_TIME;
        float speed = static_cast< float >( REPLACEMENT_WALK_SPEED );
        void * initParams [ 2 ] = { &animator, &updateMode };
        void * clipParams [ 1 ] = { &clip };
        void * speedParams [ 1 ] = { &speed };
        bool configured =
            InvokeVoid( mNpcGraphCtor, script, nullptr,
                "NPCCPUAnimationGraphScript_SimpleOneState.ctor" ) &&
            InvokeVoid( mNpcGraphInit, script, initParams,
                "NPCCPUAnimationGraphScript_SimpleOneState.Init" ) &&
            InvokeVoid( mNpcGraphSetMainAnimClip, script, clipParams,
                "NPCCPUAnimationGraphScript_SimpleOneState.SetMainAnimClip" ) &&
            InvokeVoid( mNpcGraphSetMainClipSpeed, script, speedParams,
                "NPCCPUAnimationGraphScript_SimpleOneState.SetMainClipSpeed" ) &&
            InvokeVoid( mNpcGraphPlay, script, nullptr,
                "NPCCPUAnimationGraphScript_SimpleOneState.Play" );
        if ( !configured ) {
            Log( "[walk-gamegraph] game-native graph setup failed" );
            DestroyWalkPlayable( );
            return false;
        }

        g_walkPlayableStarted = true;
        LogAnimatorAndClipState( "after-gamegraph-play", replacementRoot,
            animator, clip );
        char success [ 320 ] = { 0 };
        snprintf( success, sizeof( success ),
            "[walk-gamegraph] SimpleOneState started script=%p gcHandle=%u "
            "animator=%p existingAnimators=%d animatorAdded=%s "
            "updateMode=UnscaledGameTime speed=%.2f",
            script, g_walkGraphScriptGcHandle, animator, animatorCount,
            animatorAdded ? "true" : "false", REPLACEMENT_WALK_SPEED );
        Log( success );
        return true;
    }

    static bool StartRawAnimationPlayable( LoginActorReplacementState & state,
        void * replacementRoot, void * clip, ReplacementPhase phase ) {
        int phaseIndex = static_cast< int >( phase );
        if ( !replacementRoot || !clip || phaseIndex < 0 ||
            phaseIndex >= static_cast< int >( REPLACEMENT_CLIPS.size( ) ) )
            return false;
        if ( !ValidateReplacementClips( ) ) {
            g_walkClipResolved = false;
            Log( "[sequence-playable] clip native validation failed; graph creation refused" );
            return false;
        }
        const ReplacementClipSpec & spec =
            REPLACEMENT_CLIPS [ static_cast< size_t >( phaseIndex ) ];
        bool holdForwardLeanPose = phase == ReplacementPhase::SitLoop;
        // Crossfade is itself a manual looping mode. It must not depend on the
        // clip's imported LoopTime flag or on the separate native-loop toggle.
        bool useWalkMixer = phase == ReplacementPhase::WalkLong &&
            REPLACEMENT_USE_WALK_CROSSFADE;
        double sampleTime =
            phase == ReplacementPhase::SitLoop ||
                phase == ReplacementPhase::SitToWalk
            ? FORWARD_LEAN_POSE_SAMPLE_SECONDS : 0.0;
        double playbackSpeed = holdForwardLeanPose ? 0.0 : spec.speed;
        // Native LoopTime is honored only when requested. Force loop overrides
        // a non-looping import; the dual-playable mixer takes precedence over
        // both modes.
        bool finalPlayableLoop = phase == ReplacementPhase::WalkLong &&
            ( REPLACEMENT_FORCE_FINAL_LOOP ||
                ( spec.loop && REPLACEMENT_FINAL_NATIVE_LOOP ) );
        bool playbackLoop = holdForwardLeanPose ? false :
            ( !useWalkMixer && ( phase == ReplacementPhase::WalkLong
                ? finalPlayableLoop : spec.loop ) );

        void * animators = GetComponentsInChildren(
            replacementRoot, clsAnimator );
        int animatorCount = ArrayLength( animators );
        void * animator = animatorCount > 0
            ? ArrayElement( animators, 0 ) : nullptr;
        bool animatorAdded = false;
        if ( !animator ) {
            void * animatorType = TypeObject( clsAnimator );
            if ( !animatorType || !g_gameObjectAddComponent )
                return false;
            __try {
                animator = g_gameObjectAddComponent(
                    replacementRoot, animatorType, nullptr );
                animatorAdded = animator != nullptr;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                animator = nullptr;
            }
        }
        if ( !animator ) {
            Log( "[sequence-playable] no Animator was found or added" );
            return false;
        }
        if ( !ConfigureAnimatorForWalk( replacementRoot, animator, clip ) ) {
            Log( "[sequence-playable] Animator preparation failed; graph creation refused" );
            return false;
        }

        bool animatorInitialized = false;
        bool animatorHuman = false;
        bool initializedReadable = Unbox( Invoke( mAnimatorGetIsInitialized,
            animator, nullptr, "Animator.get_isInitialized" ),
            animatorInitialized );
        bool humanReadable = Unbox( Invoke( mAnimatorGetIsHuman,
            animator, nullptr, "Animator.get_isHuman" ), animatorHuman );
        void * avatar = Invoke( mAnimatorGetAvatar, animator, nullptr,
            "Animator.get_avatar" );
        if ( !initializedReadable || !humanReadable || !animatorInitialized ||
            !animatorHuman || !avatar ) {
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-playable] Animator native-ready=false "
                "initializedReadable=%s initialized=%s humanReadable=%s "
                "isHuman=%s avatar=%p; graph creation refused",
                initializedReadable ? "true" : "false",
                animatorInitialized ? "true" : "false",
                humanReadable ? "true" : "false",
                animatorHuman ? "true" : "false", avatar );
            Log( message );
            return false;
        }

        PlayableGraphData graph { };
        PlayableHandleData playable { };
        PlayableHandleData secondaryPlayable { };
        PlayableHandleData mixer { };
        PlayableOutputHandleData output { };
        void * graphName = api::string_new
            ? api::string_new( "EF Aglina Login Sequence" ) : nullptr;
        void * outputName = api::string_new
            ? api::string_new( "EF Aglina Login Output" ) : nullptr;
        bool playableCreated = false;
        bool secondaryPlayableCreated = false;
        bool mixerCreated = false;
        bool mixerInputsConnected = false;
        bool outputCreated = false;
        bool configured = false;
        double sampledPlayableTime = -1.0;
        __try {
            g_playableGraphCreateInjected( graphName, &graph, nullptr );
            playableCreated = graph.handle &&
                g_animationClipPlayableCreateHandle(
                    &graph, clip, &playable, nullptr );
            if ( playableCreated ) {
                // AnimationClipPlayable has its own clip-sampling loop flags.
                // TimeWrapMode alone does not override a non-looping import.
                g_animationClipPlayableSetOverrideLoopTime(
                    &playable, true, nullptr );
                g_animationClipPlayableSetLoopTime(
                    &playable, playbackLoop, nullptr );
            }
            PlayableHandleData * graphSource = &playable;
            if ( playableCreated && useWalkMixer ) {
                secondaryPlayableCreated =
                    g_animationClipPlayableCreateHandle(
                        &graph, clip, &secondaryPlayable, nullptr );
                if ( secondaryPlayableCreated ) {
                    g_animationClipPlayableSetOverrideLoopTime(
                        &secondaryPlayable, true, nullptr );
                    g_animationClipPlayableSetLoopTime(
                        &secondaryPlayable, false, nullptr );
                }
                mixerCreated = secondaryPlayableCreated &&
                    g_animationMixerPlayableCreateHandle(
                        &graph, &mixer, nullptr );
                if ( mixerCreated ) {
                    g_playableHandleSetInputCount( &mixer, 2, nullptr );
                    bool primaryConnected = g_playableGraphConnectInternal(
                        &graph, &playable, 0, &mixer, 0, nullptr );
                    bool secondaryConnected = g_playableGraphConnectInternal(
                        &graph, &secondaryPlayable, 0, &mixer, 1, nullptr );
                    mixerInputsConnected =
                        primaryConnected && secondaryConnected;
                    if ( mixerInputsConnected ) {
                        g_playableHandleSetInputWeight(
                            &mixer, 0, 1.0f, nullptr );
                        g_playableHandleSetInputWeight(
                            &mixer, 1, 0.0f, nullptr );
                        graphSource = &mixer;
                    }
                }
            }
            bool sourceReady = playableCreated &&
                ( !useWalkMixer || mixerInputsConnected );
            outputCreated = sourceReady &&
                g_createAnimationOutput(
                    &graph, outputName, &output, nullptr );
            if ( outputCreated ) {
                // Keep the walk graph advancing even if the login scene pauses
                // scaled game time. DirectorUpdateMode.UnscaledGameTime = 2.
                g_playableGraphSetUpdateMode(
                    &graph, DIRECTOR_UPDATE_MODE_UNSCALED_GAME_TIME, nullptr );
                g_animationOutputSetTarget( &output, animator, nullptr );
                g_playableOutputSetSource( &output, graphSource, 0, nullptr );
                g_playableHandleSetWrapMode( &playable,
                    playbackLoop ? DIRECTOR_WRAP_MODE_LOOP :
                        DIRECTOR_WRAP_MODE_HOLD,
                    nullptr );
                g_playableHandleSetSpeed(
                    &playable, playbackSpeed, nullptr );
                g_playableHandleSetTime( &playable, sampleTime, nullptr );
                if ( useWalkMixer ) {
                    g_playableHandleSetWrapMode( &secondaryPlayable,
                        DIRECTOR_WRAP_MODE_HOLD, nullptr );
                    g_playableHandleSetSpeed(
                        &secondaryPlayable, 0.0, nullptr );
                    g_playableHandleSetTime(
                        &secondaryPlayable, 0.0, nullptr );
                }
                g_playableGraphPlay( &graph, nullptr );
                // PlayableGraph.Play starts scheduling the graph but does not
                // synchronously write its selected pose. Evaluate now so the
                // prefab's standing bind/controller pose is never rendered.
                g_playableGraphEvaluate( &graph, 0.0f, nullptr );
                sampledPlayableTime = g_playableHandleGetTime(
                    &playable, nullptr );
                configured = true;
            }
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            configured = false;
        }

        if ( !configured ) {
            if ( graph.handle && g_playableGraphDestroy ) {
                __try {
                    g_playableGraphDestroy( &graph, nullptr );
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                }
            }
            char failure [ 384 ] = { 0 };
            snprintf( failure, sizeof( failure ),
                "[sequence-playable] phase=%s graph setup failed "
                "animatorCount=%d animatorAdded=%s playable=%s "
                "secondary=%s mixer=%s connected=%s output=%s",
                spec.label, animatorCount, animatorAdded ? "true" : "false",
                playableCreated ? "true" : "false",
                secondaryPlayableCreated ? "true" : "false",
                mixerCreated ? "true" : "false",
                mixerInputsConnected ? "true" : "false",
                outputCreated ? "true" : "false" );
            Log( failure );
            return false;
        }

        DestroyActorPlayable( state );
        state.playableGraph = graph;
        state.activePlayable = useWalkMixer ? mixer : playable;
        if ( useWalkMixer ) {
            state.walkMixer = mixer;
            state.walkPlayables [ 0 ] = playable;
            state.walkPlayables [ 1 ] = secondaryPlayable;
            state.walkActiveInput = 0;
            state.walkBlendActive = false;
            state.walkMixerConfigured = true;
            state.walkMixerFaultLogged = false;
            state.walkBlendCount = 0;
        }
        g_walkPlayableStarted = true;
        char success [ 448 ] = { 0 };
        snprintf( success, sizeof( success ),
            "[sequence-playable] phase=%s graph started animator=%p "
            "existingAnimators=%d animatorAdded=%s graph=%p version=%u "
            "updateMode=UnscaledGameTime speed=%.2f loop=%s "
            "requestedSampleTime=%.3f actualSampleTime=%.3f "
            "holdForwardLean=%s dualPlayableMixer=%s "
            "clipLoopOverride=true initialPoseSampled=true",
            spec.label, animator, animatorCount,
            animatorAdded ? "true" : "false", graph.handle, graph.version,
            playbackSpeed, playbackLoop ? "true" : "false", sampleTime,
            sampledPlayableTime,
            holdForwardLeanPose ? "true" : "false",
            useWalkMixer ? "true" : "false" );
        Log( success );
        return true;
    }

    static bool StartReplacementPhaseOnRoot(
        LoginActorReplacementState & state, void * replacementRoot,
        ReplacementPhase phase, const char * source ) {
        if ( g_loginReloadSuspended.load( ) ||
            !g_walkClipResolved.load( ) )
            return false;
        int phaseIndex = static_cast< int >( phase );
        if ( phaseIndex < 0 ||
            phaseIndex >= static_cast< int >( REPLACEMENT_CLIPS.size( ) ) )
            return false;
        const ReplacementClipSpec & spec =
            REPLACEMENT_CLIPS [ static_cast< size_t >( phaseIndex ) ];
        size_t clipIndex = phase == ReplacementPhase::SitLoop
            ? static_cast< size_t >( ReplacementPhase::SitToWalk )
            : static_cast< size_t >( phaseIndex );
        void * clip = g_replacementClips [ clipIndex ].load( );
        if ( !replacementRoot || !clip )
            return false;

        DestroyActorPlayable( state );
        if ( !StartRawAnimationPlayable(
            state, replacementRoot, clip, phase ) )
            return false;

        float clipLength = 0.0f;
        Unbox( Invoke( mClipGetLength, clip, nullptr,
            "AnimationClip.get_length" ), clipLength );
        state.activePhase = phaseIndex;
        if ( phase == ReplacementPhase::SitLoop )
            state.activeClipLength = 0.0f;
        else if ( phase == ReplacementPhase::SitToWalk )
            state.activeClipLength = ( std::max )( 0.0f,
                clipLength - static_cast< float >(
                    FORWARD_LEAN_POSE_SAMPLE_SECONDS ) );
        else if ( phase == ReplacementPhase::WalkLong &&
            REPLACEMENT_USE_WALK_CROSSFADE )
            state.activeClipLength = static_cast< float >(
                ( std::min )( static_cast< double >( clipLength ),
                    REPLACEMENT_WALK_LOOP_END_SECONDS ) );
        else
            state.activeClipLength = clipLength;
        state.activeClipElapsed = 0.0;
        if ( !ApplyReplacementPhaseFacing( state, phase ) )
            Log( "[sequence-facing] warning: failed to apply phase-facing offset" );

        size_t slotIndex = static_cast< size_t >(
            &state - g_loginActorSlots.data( ) );
        char message [ 384 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[sequence] actor slot=%zu phase=%s source=%s clip=%p "
            "sourceClip=%s length=%.3f effectiveLength=%.3f "
            "speed=%.2f loop=%s",
            slotIndex, spec.label, source ? source : "unknown", clip,
            REPLACEMENT_CLIPS [ clipIndex ].label, clipLength,
            state.activeClipLength,
            spec.speed, spec.loop ? "true" : "false" );
        Log( message );
        return true;
    }

    static bool StartReplacementPhase( ReplacementPhase phase,
        const char * source ) {
        for ( auto & state : g_loginActorSlots ) {
            if ( state.replacementActive && state.replacementRoot )
                return StartActorReplacementPhase( state, phase, source );
        }
        return false;
    }

    static bool StartActorReplacementPhase(
        LoginActorReplacementState & state, ReplacementPhase phase,
        const char * source ) {
        if ( g_loginReloadSuspended.load( ) ||
            !g_walkClipResolved.load( ) ||
            !state.replacementActive || !state.replacementRoot )
            return false;
        return StartReplacementPhaseOnRoot(
            state, state.replacementRoot, phase, source );
    }

    static void QueueReplacementPhase( void * controller,
        ReplacementPhase phase, const char * source ) {
        if ( g_loginReloadSuspended.load( ) )
            return;

        int phaseIndex = static_cast< int >( phase );
        if ( phaseIndex < 0 ||
            phaseIndex >= static_cast< int >( REPLACEMENT_CLIPS.size( ) ) )
            return;

        LoginActorReplacementState * state = FindActorSlot( controller, true );
        if ( !state )
            return;
        void * expectedDriver = nullptr;
        g_sequenceDriverController.compare_exchange_strong(
            expectedDriver, controller );
        g_broadcastReplacementPhase = phaseIndex;

        if ( phase == ReplacementPhase::SitLoop &&
            !state->replacementActive && !state->gateRequested ) {
            state->gateRequested = true;
            state->gateStartTick = GetTickCount64( );
            state->gateTimedOut = false;
        }

        for ( size_t i = 0; i < g_loginActorSlots.size( ); ++i ) {
            auto & targetState = g_loginActorSlots [ i ];
            if ( !targetState.controller && !targetState.targetRoot )
                continue;
            int previousRequested = targetState.lastRequestedPhase;
            targetState.lastRequestedPhase = phaseIndex;
            if ( previousRequested == phaseIndex )
                continue;

            targetState.pendingPhase = phaseIndex;
            TryEnsureActorReplacement( targetState );
            char message [ 352 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-sync] broadcast actor slot=%zu phase=%s "
                "source=%s active=%s",
                i,
                REPLACEMENT_CLIPS [ static_cast< size_t >( phaseIndex ) ].label,
                source ? source : "unknown",
                targetState.replacementActive ? "true" : "false" );
            Log( message );
        }
    }

    static bool TickReplacementWalkMixer(
        LoginActorReplacementState & state ) {
        if ( !state.walkMixerConfigured || !state.walkMixer.handle ||
            !state.walkPlayables [ 0 ].handle ||
            !state.walkPlayables [ 1 ].handle ||
            !g_playableHandleGetTime || !g_playableHandleSetTime ||
            !g_playableHandleSetSpeed || !g_playableHandleSetInputWeight )
            return false;

        int currentInput = state.walkActiveInput == 1 ? 1 : 0;
        int nextInput = 1 - currentInput;
        PlayableHandleData & current = state.walkPlayables [ currentInput ];
        PlayableHandleData & next = state.walkPlayables [ nextInput ];
        double currentTime = -1.0;
        __try {
            currentTime = g_playableHandleGetTime( &current, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
        if ( currentTime < 0.0 )
            return false;
        state.activeClipElapsed = currentTime;

        const double loopEnd = ( std::min )(
            REPLACEMENT_WALK_LOOP_END_SECONDS,
            static_cast< double >( state.activeClipLength ) );
        const double loopStart = REPLACEMENT_WALK_LOOP_START_SECONDS < loopEnd
            ? REPLACEMENT_WALK_LOOP_START_SECONDS : 0.0;
        const double loopLength = loopEnd - loopStart;
        const double fadeClipDuration = ( std::min )(
            REPLACEMENT_WALK_CROSSFADE_SECONDS * REPLACEMENT_WALK_SPEED,
            loopLength * 0.5 );
        const double transitionStart = loopEnd - fadeClipDuration;
        if ( loopLength <= 0.0 || fadeClipDuration <= 0.0 )
            return false;

        bool blendStartedNow = false;
        double nextTime = -1.0;
        if ( !state.walkBlendActive ) {
            if ( currentTime + 0.0005 < transitionStart )
                return true;

            // Blend the outgoing tail against the beginning of the configured
            // interval. The former currentTime-loopLength calculation sampled
            // before loopStart and made many one-shot clips appear unchanged.
            nextTime = ( std::min )(
                loopStart + ( currentTime - transitionStart ),
                loopStart + fadeClipDuration );
            __try {
                g_playableHandleSetInputWeight(
                    &state.walkMixer, currentInput, 1.0f, nullptr );
                g_playableHandleSetInputWeight(
                    &state.walkMixer, nextInput, 0.0f, nullptr );
                g_playableHandleSetTime( &next, nextTime, nullptr );
                g_playableHandleSetSpeed(
                    &next, REPLACEMENT_WALK_SPEED, nullptr );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                return false;
            }
            state.walkBlendActive = true;
            blendStartedNow = true;
        }

        double normalized = std::clamp(
            ( currentTime - transitionStart ) / fadeClipDuration,
            0.0, 1.0 );
        double eased = normalized * normalized * ( 3.0 - 2.0 * normalized );
        __try {
            g_playableHandleSetInputWeight( &state.walkMixer, currentInput,
                static_cast< float >( 1.0 - eased ), nullptr );
            g_playableHandleSetInputWeight( &state.walkMixer, nextInput,
                static_cast< float >( eased ), nullptr );
            if ( blendStartedNow && state.playableGraph.handle &&
                g_playableGraphEvaluate )
                g_playableGraphEvaluate( &state.playableGraph, 0.0f, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }

        size_t slotIndex = static_cast< size_t >(
            &state - g_loginActorSlots.data( ) );
        if ( blendStartedNow && state.walkBlendCount < 4 ) {
            char message [ 352 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[walk-crossfade] actor slot=%zu cycle=%u begin "
                "fromInput=%d toInput=%d sourceTime=%.3f targetTime=%.3f "
                "fadeReal=%.3f",
                slotIndex, state.walkBlendCount + 1, currentInput, nextInput,
                currentTime, nextTime, REPLACEMENT_WALK_CROSSFADE_SECONDS );
            Log( message );
        }

        if ( normalized + 0.0005 < 1.0 )
            return true;

        double completedTime = loopStart + fadeClipDuration;
        __try {
            g_playableHandleSetInputWeight(
                &state.walkMixer, currentInput, 0.0f, nullptr );
            g_playableHandleSetInputWeight(
                &state.walkMixer, nextInput, 1.0f, nullptr );
            g_playableHandleSetSpeed( &current, 0.0, nullptr );
            completedTime = g_playableHandleGetTime( &next, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
        state.walkActiveInput = nextInput;
        state.walkBlendActive = false;
        state.activeClipElapsed = completedTime;
        ++state.walkBlendCount;

        if ( state.walkBlendCount <= 4 ||
            state.walkBlendCount % 30 == 0 ) {
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[walk-crossfade] actor slot=%zu cycle=%u complete "
                "activeInput=%d sourceTime=%.3f activeTime=%.3f",
                slotIndex, state.walkBlendCount, state.walkActiveInput,
                currentTime, completedTime );
            Log( message );
        }
        return true;
    }

    static void TickReplacementSequence(
        LoginActorReplacementState & state, float deltaTime ) {
        if ( g_loginReloadSuspended.load( ) ||
            !g_walkClipResolved.load( ) ||
            !state.replacementActive || !state.replacementRoot )
            return;

        int pending = state.pendingPhase;
        state.pendingPhase = static_cast< int >( ReplacementPhase::None );
        if ( pending >= 0 &&
            pending < static_cast< int >( REPLACEMENT_CLIPS.size( ) ) ) {
            StartActorReplacementPhase( state,
                static_cast< ReplacementPhase >( pending ), "login-hook" );
        }

        int active = state.activePhase;
        if ( active < 0 ||
            active >= static_cast< int >( REPLACEMENT_CLIPS.size( ) ) )
            return;
        const ReplacementClipSpec & spec =
            REPLACEMENT_CLIPS [ static_cast< size_t >( active ) ];
        if ( state.activeClipLength <= 0.0f )
            return;

        if ( deltaTime > 0.0f )
            state.activeClipElapsed +=
                static_cast< double >( deltaTime ) * spec.speed;
        UpdateReplacementPhaseFacing( state );
        if ( active == static_cast< int >( ReplacementPhase::WalkLong ) &&
            REPLACEMENT_USE_WALK_CROSSFADE ) {
            if ( ( !state.walkMixerConfigured ||
                !TickReplacementWalkMixer( state ) ) &&
                !state.walkMixerFaultLogged ) {
                Log( "[walk-crossfade] warning: mixer tick failed; "
                    "walk held without a visible time reset" );
                state.walkMixerFaultLogged = true;
            }
            return;
        }
        if ( state.activeClipElapsed + 0.001 <
            static_cast< double >( state.activeClipLength ) )
            return;

        bool phaseLoops = spec.loop;
        if ( active == static_cast< int >( ReplacementPhase::WalkLong ) )
            phaseLoops = REPLACEMENT_FORCE_FINAL_LOOP ||
                ( spec.loop && REPLACEMENT_FINAL_NATIVE_LOOP );
        if ( phaseLoops ) {
            double playableTime = -1.0;
            bool timeRead = false;
            if ( state.activePlayable.handle && g_playableHandleGetTime ) {
                __try {
                    playableTime = g_playableHandleGetTime(
                        &state.activePlayable, nullptr );
                    timeRead = true;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    timeRead = false;
                }
            }

            // Native loops (such as SitLoop when it is not pose-held) keep
            // advancing without an explicit time jump.
            while ( state.activeClipElapsed >=
                static_cast< double >( state.activeClipLength ) ) {
                state.activeClipElapsed -=
                    static_cast< double >( state.activeClipLength );
            }
            size_t slotIndex = static_cast< size_t >(
                &state - g_loginActorSlots.data( ) );
            char message [ 288 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[sequence-loop] actor slot=%zu phase=%s native wrap=true timeRead=%s "
                "playableTime=%.3f length=%.3f",
                slotIndex, spec.label, timeRead ? "true" : "false", playableTime,
                state.activeClipLength );
            Log( message );
            return;
        }

        if ( spec.automaticNext == ReplacementPhase::None )
            return;

        StartActorReplacementPhase(
            state, spec.automaticNext, "clip-complete" );
    }

    static bool ApplyReplacementScale( void * replacementRoot ) {
        void * replacementTransform = GetTransform( replacementRoot );
        Vector3 scale { 1.0f, 1.0f, 1.0f };
        if ( !replacementTransform || !Unbox( Invoke( mGetLocalScale,
            replacementTransform, nullptr, "Transform.get_localScale" ), scale ) )
            return false;
        scale.x *= REPLACEMENT_SCALE_MULTIPLIER;
        scale.y *= REPLACEMENT_SCALE_MULTIPLIER;
        scale.z *= REPLACEMENT_SCALE_MULTIPLIER;
        void * scaleParams [ 1 ] = { &scale };
        Invoke( mSetLocalScale, replacementTransform, scaleParams,
            "Transform.set_localScale" );

        char message [ 320 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[complete-replace] fixed scale factor=%.3f "
            "final=(%.3f,%.3f,%.3f)",
            REPLACEMENT_SCALE_MULTIPLIER,
            scale.x, scale.y, scale.z );
        Log( message );
        return true;
    }

    static bool CopyLayerToHierarchy( void * targetRoot, void * replacementRoot ) {
        int layer = 0;
        if ( !Unbox( Invoke( mGameObjectGetLayer, targetRoot, nullptr,
            "GameObject.get_layer" ), layer ) )
            return false;

        void * transforms = GetComponentsInChildren( replacementRoot, clsTransform );
        int count = ArrayLength( transforms );
        for ( int i = 0; i < count; ++i ) {
            void * transform = ArrayElement( transforms, i );
            void * gameObject = Invoke( mComponentGetGameObject, transform, nullptr,
                "Component.get_gameObject" );
            if ( !gameObject )
                continue;
            void * params [ 1 ] = { &layer };
            Invoke( mGameObjectSetLayer, gameObject, params, "GameObject.set_layer" );
        }

        Log( "[replace] copied silhouette layer " + std::to_string( layer ) +
            " to " + std::to_string( count ) + " replacement objects" );
        return true;
    }

    static void DestroyObject( void * obj ) {
        if ( !obj )
            return;
        void * params [ 1 ] = { &obj };
        Invoke( mObjectDestroy, nullptr, params, "Object.Destroy" );
    }

    static void SuspendLoginReplacementForReload( const char * source ) {
        if ( g_loginSessionCleanupInProgress.exchange( true ) )
            return;

        // InitInitPathHash invalidates Unity asset wrappers from the previous
        // resource generation. Freeze every replacement entry point before
        // touching the graph so no login callback can bind a stale clip again.
        g_loginReloadSuspended = true;

        size_t graphCount = 0;
        for ( auto & slot : g_loginActorSlots ) {
            if ( slot.playableGraph.handle || slot.graphScript )
                ++graphCount;
            DestroyActorPlayable( slot );
            RestoreActorRendererHierarchy( slot );
            SetActive( slot.replacementRoot, false );
            slot.replacementActive = false;
            slot.pendingPhase = static_cast< int >(
                ReplacementPhase::None );
            slot.activePhase = static_cast< int >( ReplacementPhase::None );
            slot.lastRequestedPhase = static_cast< int >(
                ReplacementPhase::None );
            slot.activeClipLength = 0.0f;
            slot.activeClipElapsed = 0.0;
            slot.gateRequested = false;
            slot.tickHeld = false;
            slot.gateTimedOut = false;
            slot.gateStartTick = 0;
        }
        DestroyWalkPlayable( );
        RestoreOriginalRendererHierarchy( );
        g_originalRenderersDisabled = false;

        // GC handles keep managed wrappers alive, but do not keep the native
        // AnimationClip/Prefab assets valid across a ResourceManager hash
        // rebuild. Release and reload all asset roots in the new generation.
        ReleasePelicaPrefabRoot( );
        ReleaseOriginalLoginPrefabRoots( );
        ReleaseReplacementClipRoots( );
        g_pelicaPrefab = nullptr;
        g_pelicaPrefabResolved = false;
        for ( auto & clip : g_replacementClips )
            clip = nullptr;
        g_walkClipResolved = false;
        g_walkClipLoadAttempted = false;
        g_deferredLoadPending = true;
        g_deferredLoadAttempted = false;
        g_mainLoadReturnedHandle = false;
        g_initialPathHashEntered = false;
        g_initialPathHashCompleted = false;
        g_initialPathHashCompletedTick = 0;
        // InitInitPathHash returning does not mean Animator/resource teardown
        // jobs have finished. Keep a quiet window before the first validation
        // attempt so the retry scene can settle on the main thread.
        g_loginReloadResumeNextTick = GetTickCount64( ) + 2000;

        g_sequenceDriverController = nullptr;
        g_pendingReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_activeReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_lastRequestedReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_broadcastReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_walkPlayableStarted = false;
        g_loginAnimationGateRequested = false;
        g_loginAnimationTickHeld = false;
        g_loginAnimationGateTimedOut = false;
        g_loginAnimationGateStartTick = 0;
        g_replacementActive = false;

        char message [ 384 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[login-session] reload suspend complete generation=%u "
            "source=%s graphs=%zu; instance retained hidden, stale resource "
            "roots invalidated",
            g_loginSessionGeneration.load( ),
            source ? source : "unknown", graphCount );
        Log( message );
        g_loginSessionCleanupInProgress = false;
    }

    static bool TryResumeLoginReplacementAfterReload( ) {
        if ( !g_loginReloadSuspended.load( ) )
            return true;
        if ( g_loginSessionCleanupInProgress.load( ) ||
            !g_initialPathHashCompleted.load( ) ||
            !g_mainPathHashCompleted.load( ) )
            return false;

        uint64_t now = GetTickCount64( );
        uint64_t nextAttempt = g_loginReloadResumeNextTick.load( );
        if ( nextAttempt != 0 && now < nextAttempt )
            return false;
        g_loginReloadResumeNextTick = now + 500;

        if ( !g_pelicaPrefabResolved.load( ) || !g_pelicaPrefab.load( ) ) {
            g_deferredLoadPending = true;
            g_deferredLoadAttempted = false;
            TryDeferredMainLoad( );
        }
        else if ( !g_walkClipResolved.load( ) ) {
            g_walkClipLoadAttempted = false;
            LoadReplacementClips( );
        }

        if ( !g_pelicaPrefabResolved.load( ) || !g_pelicaPrefab.load( ) ||
            !g_walkClipResolved.load( ) )
            return false;

        size_t resumedCount = 0;
        // No hook callback can interleave here on the captured login thread.
        // Temporarily allow the phase builder, but restore suspension if the
        // retained Animator cannot be rebound and sampled safely.
        g_loginReloadSuspended = false;
        for ( auto & slot : g_loginActorSlots ) {
            if ( !slot.replacementRoot || !slot.targetRoot )
                continue;

            // The retained instance was hidden before the hash rebuild. The
            // original actor stays visible until both the new clips and the
            // replacement GameObject have been validated on the game thread.
            if ( !SetActive( slot.replacementRoot, false ) )
                continue;
            RestoreActorRendererHierarchy( slot );
            if ( !SetActive( slot.replacementRoot, true ) ) {
                continue;
            }

            slot.replacementActive = true;
            slot.pendingPhase = static_cast< int >( ReplacementPhase::None );
            slot.activePhase = static_cast< int >( ReplacementPhase::None );
            slot.lastRequestedPhase = static_cast< int >(
                ReplacementPhase::SitLoop );
            slot.activeClipLength = 0.0f;
            slot.activeClipElapsed = 0.0;
            slot.anchorAlignmentLogged = false;

            // Sample the held sit pose while the original is still visible.
            // Only hide the original after a fully validated graph exists.
            if ( !StartActorReplacementPhase( slot,
                ReplacementPhase::SitLoop, "reload-resume" ) ) {
                slot.replacementActive = false;
                SetActive( slot.replacementRoot, false );
                continue;
            }
            if ( !DisableOriginalRendererHierarchy( slot, slot.targetRoot,
                slot.replacementRoot ) ) {
                DestroyActorPlayable( slot );
                slot.replacementActive = false;
                SetActive( slot.replacementRoot, false );
                RestoreActorRendererHierarchy( slot );
                continue;
            }
            if ( !AlignReplacementAnchor( slot, true ) )
                Log( "[anchor-sync] reload one-shot alignment failed; "
                    "replacement kept at copied root pose" );
            ++resumedCount;
        }

        if ( resumedCount == 0 ) {
            g_loginReloadSuspended = true;
            g_replacementActive = false;
            return false;
        }

        g_pendingReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_activeReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_lastRequestedReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_broadcastReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_sequenceDriverController = nullptr;
        g_walkPlayableStarted = false;
        g_replacementActive = true;
        g_loginReloadResumeNextTick = 0;

        char message [ 320 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[login-session] reload resume complete generation=%u "
            "actors=%zu; native assets validated and sit graph sampled",
            g_loginSessionGeneration.load( ), resumedCount );
        Log( message );
        return true;
    }

    static void CleanupLoginReplacementSession( const char * source,
        bool releaseReusableAssets ) {
        if ( g_loginSessionCleanupInProgress.exchange( true ) )
            return;

        bool hadSession = g_loginSessionActive.exchange( false );
        bool reusableAssetsReady = g_pelicaPrefabResolved.load( ) &&
            g_walkClipResolved.load( ) && g_pelicaPrefab.load( ) &&
            g_loginInstantiateMethod.load( );
        bool hasState = hadSession || g_loginInstantiateCaptured.load( ) ||
            g_replacementActive.load( );
        if ( releaseReusableAssets ) {
            hasState = hasState || g_pelicaPrefabResolved.load( ) ||
                g_loader || g_loaderGcHandle != 0;
        }
        size_t graphCount = 0;
        for ( const auto & slot : g_loginActorSlots ) {
            hasState = hasState || slot.controller || slot.targetRoot ||
                slot.replacementRoot || slot.playableGraph.handle ||
                slot.graphScript;
            if ( slot.playableGraph.handle || slot.graphScript )
                ++graphCount;
        }
        if ( !hasState ) {
            g_loginSessionCleanupInProgress = false;
            return;
        }

        uint32_t generation = g_loginSessionGeneration.load( );
        char beginMessage [ 320 ] = { 0 };
        snprintf( beginMessage, sizeof( beginMessage ),
            "[login-session] cleanup begin generation=%u source=%s "
            "graphs=%zu thread=%lu",
            generation, source ? source : "unknown", graphCount,
            static_cast< unsigned long >( GetCurrentThreadId( ) ) );
        Log( beginMessage );

        // Graphs must be destroyed before the game's scene release destroys
        // their target Animators.  Leaving an unscaled graph bound to a dead
        // Animator lets Unity's animation Job.Worker evaluate stale bindings.
        for ( auto & slot : g_loginActorSlots )
            DestroyActorPlayable( slot );
        DestroyWalkPlayable( );

        for ( auto & slot : g_loginActorSlots )
            RestoreActorRendererHierarchy( slot );
        RestoreOriginalRendererHierarchy( );

        std::array< void *, MAX_LOGIN_ACTOR_SLOTS + 2 > replacements { };
        size_t replacementCount = 0;
        auto rememberReplacement = [&]( void * replacement ) {
            if ( !replacement )
                return;
            auto begin = replacements.begin( );
            auto end = begin + replacementCount;
            if ( std::find( begin, end, replacement ) != end )
                return;
            if ( replacementCount < replacements.size( ) )
                replacements [ replacementCount++ ] = replacement;
        };
        for ( const auto & slot : g_loginActorSlots )
            rememberReplacement( slot.replacementRoot );
        rememberReplacement( g_replacementRoot );
        rememberReplacement( g_pelicaInstance.load( ) );
        for ( size_t i = 0; i < replacementCount; ++i ) {
            SetActive( replacements [ i ], false );
            if ( mObjectDestroy )
                DestroyObject( replacements [ i ] );
        }

        if ( releaseReusableAssets ) {
            ReleasePelicaPrefabRoot( );
            ReleaseOriginalLoginPrefabRoots( );
            ReleaseReplacementClipRoots( );
            if ( g_loaderGcHandle != 0 && api::gchandle_free )
                api::gchandle_free( g_loaderGcHandle );
            g_loaderGcHandle = 0;
            g_loader = nullptr;
        }

        g_replacementActive = false;
        g_redirectHit = false;
        g_mainCategoryHit = false;
        g_mainLoadReturnedHandle = false;
        g_initialPathHashEntered = false;
        g_initialPathHashCompleted = false;
        g_mainPathHashEntered = false;
        g_mainPathHashCompleted = false;
        g_mainDiagnosticsStarted = false;
        g_mainDiagnosticsCompleted = false;
        g_resourceManagerInitialized = false;
        g_resourceManagerAsyncIniting = false;
        g_resourceManagerUsingBundle = false;
        g_stringCheckExists = false;
        g_hashCheckExists = false;
        g_rawHashCheckExists = false;
        g_initialPathHashCompletedTick = 0;
        g_mainPathHashCompletedTick = 0;
        g_targetResolvedHash = 0;
        g_loadAssetInternalReturnedProxy = false;
        g_bundleLoaderReturnedProxy = false;
        g_bundleLoaderProxyHasError = false;
        g_loadUntrackedReturnedHandle = false;
        g_loadUntrackedDirectIsValid = false;
        g_untrackedHandleGatePassed = false;
        g_tryGetAssetInfoLoadObserved = false;
        g_loadAssetInternalObserved = false;
        g_bundleLoaderObserved = false;
        g_loadUntrackedObserved = false;
        g_untrackedHandleGateObserved = false;
        g_assetProxyFinishErrorObserved = false;
        g_mainBundlePathObserved = false;
        g_bundleFileLoadCount = 0;
        g_bundleFileFailureCount = 0;
        g_bundleSyncCount = 0;
        g_bundleSyncFailureCount = 0;
        g_deferredLoadPending = false;
        g_deferredLoadAttempted = releaseReusableAssets
            ? false : reusableAssetsReady;
        g_loginLoadContextCaptured = false;
        if ( releaseReusableAssets ) {
            g_i18nLoadGameObjectMethod = nullptr;
            g_loginLoadThreadId = 0;
        }
        g_deferredHandleRaw0 = 0;
        g_deferredHandleRaw1 = 0;
        g_deferredHandleRaw2 = 0;
        g_loginBindObserved = false;
        g_loginInstantiateCaptured = false;
        g_loginReloadSuspended = false;
        g_loginReloadResumeNextTick = 0;
        if ( releaseReusableAssets )
            g_pelicaPrefabResolved = false;
        g_pelicaInstantiateAttempted = false;
        g_pelicaInstantiateSucceeded = false;
        g_originalLoginActorDisabled = false;
        g_skeletonBindAttempted = false;
        g_skeletonBindSucceeded = false;
        g_originalRenderersDisabled = false;
        if ( releaseReusableAssets ) {
            g_walkClipLoadAttempted = false;
            g_walkClipResolved = false;
        }
        g_walkPlayableStarted = false;
        g_pendingReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_activeReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_lastRequestedReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_loginAnimationGateRequested = false;
        g_loginAnimationTickHeld = false;
        g_loginAnimationGateTimedOut = false;
        g_loginAnimationGateStartTick = 0;
        g_activeReplacementClipLength = 0.0f;
        g_activeReplacementClipElapsed = 0.0;
        g_loginActorPrefab = nullptr;
        g_loginActorParent = nullptr;
        g_loginActorInstance = nullptr;
        if ( releaseReusableAssets ) {
            g_loginInstantiateMethod = nullptr;
            g_pelicaPrefab = nullptr;
        }
        g_pelicaInstance = nullptr;
        if ( releaseReusableAssets ) {
            for ( auto & clip : g_replacementClips )
                clip = nullptr;
            g_pelicaPrefabGcHandle = 0;
            g_replacementClipGcHandles.fill( 0 );
        }
        g_broadcastReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_sequenceDriverController = nullptr;
        g_targetRoot = nullptr;
        g_replacementRoot = nullptr;
        g_disabledRenderers.clear( );
        for ( auto & slot : g_loginActorSlots )
            slot = LoginActorReplacementState { };

        char completeMessage [ 256 ] = { 0 };
        snprintf( completeMessage, sizeof( completeMessage ),
            "[login-session] cleanup complete generation=%u "
            "replacementObjects=%zu reusableAssets=%s; hooks remain armed",
            generation, replacementCount,
            releaseReusableAssets ? "released" : "cached" );
        Log( completeMessage );
        g_loginSessionCleanupInProgress = false;
    }

    static bool CopyLocalTransform( void * source, void * target ) {
        if ( !source || !target )
            return false;

        Vector3 position { };
        Vector3 scale { 1.0f, 1.0f, 1.0f };
        Quaternion rotation { 0.0f, 0.0f, 0.0f, 1.0f };
        if ( !Unbox( Invoke( mGetLocalPosition, target, nullptr,
            "Transform.get_localPosition" ), position ) )
            return false;
        if ( !Unbox( Invoke( mGetLocalRotation, target, nullptr,
            "Transform.get_localRotation" ), rotation ) )
            return false;
        if ( !Unbox( Invoke( mGetLocalScale, target, nullptr,
            "Transform.get_localScale" ), scale ) )
            return false;

        void * parent = GetParent( target );
        if ( !g_transformSetParent )
            return false;
        __try {
            g_transformSetParent( source, parent, false, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct Transform.SetParent" );
            return false;
        }

        void * positionParams [ 1 ] = { &position };
        void * rotationParams [ 1 ] = { &rotation };
        void * scaleParams [ 1 ] = { &scale };
        Invoke( mSetLocalPosition, source, positionParams, "Transform.set_localPosition" );
        Invoke( mSetLocalRotation, source, rotationParams, "Transform.set_localRotation" );
        Invoke( mSetLocalScale, source, scaleParams, "Transform.set_localScale" );
        return true;
    }

    static bool AttachReplacementBesideLoginRoot( void * replacementTransform,
        void * targetTransform ) {
        // A child would inherit the original actor's animated root motion on
        // top of the replacement clip. Keep both actors under the same stable
        // scene parent and start from the original root's local transform.
        return CopyLocalTransform( replacementTransform, targetTransform );
    }

    static bool PrepareCompleteReplacement(
        LoginActorReplacementState & state, void * targetRoot,
        void * replacementRoot ) {
        void * targetTransform = GetTransform( targetRoot );
        void * replacementTransform = GetTransform( replacementRoot );
        if ( !targetTransform || !replacementTransform )
            return false;

        // Resolve both bones before moving the replacement. Its sampled sit
        // pose is aligned once in XYZ; afterwards the original staging drives
        // XZ exactly and Y through a low-pass correction so the replacement
        // keeps the framing without inheriting per-frame vertical jitter.
        state.replacementTransform = replacementTransform;
        state.originalAnchor = FindTransformByName(
            targetRoot, "Bip001_Pelvis" );
        state.replacementAnchor = FindTransformByName(
            replacementRoot, "Bip001_Pelvis" );
        state.anchorAlignmentLogged = false;
        if ( !state.originalAnchor || !state.replacementAnchor )
            Log( "[anchor-sync] Bip001_Pelvis anchor missing; replacement will "
                "keep the copied sibling root pose" );

        // Hide only the original visuals. Attach the replacement afterwards so
        // it is not included in the renderer list above.
        if ( !DisableOriginalRendererHierarchy( state, targetRoot ) )
            return false;
        if ( !AttachReplacementBesideLoginRoot(
            replacementTransform, targetTransform ) ) {
            RestoreActorRendererHierarchy( state );
            return false;
        }
        if ( !CopyLayerToHierarchy( targetRoot, replacementRoot ) )
            Log( "[complete-replace] warning: failed to copy silhouette layer" );
        if ( !ApplyReplacementScale( replacementRoot ) )
            Log( "[complete-replace] warning: failed to adjust model scale" );
        if ( !SetActive( replacementRoot, true ) ) {
            RestoreActorRendererHierarchy( state );
            return false;
        }

        ReplacementPhase initialPhase = ReplacementPhase::SitLoop;
        int pending = state.pendingPhase;
        state.pendingPhase = static_cast< int >( ReplacementPhase::None );
        if ( pending >= 0 &&
            pending < static_cast< int >( REPLACEMENT_CLIPS.size( ) ) )
            initialPhase = static_cast< ReplacementPhase >( pending );
        if ( !StartReplacementPhaseOnRoot(
            state, replacementRoot, initialPhase, "replacement-ready" ) ) {
            RestoreActorRendererHierarchy( state );
            return false;
        }
        return true;
    }

    static void * FindTargetRoot( ) {
        if ( mGameObjectFind && api::string_new ) {
            void * targetName = api::string_new( "SK_actor_female(Clone)" );
            void * findParams [ 1 ] = { &targetName };
            void * direct = Invoke( mGameObjectFind, nullptr, findParams,
                "GameObject.Find" );
            if ( direct ) {
                Log( "[replace] target found through GameObject.Find" );
                return direct;
            }
        }

        Log( "[replace] direct target lookup missed; trying global enumeration" );
        void * typeObject = TypeObject( clsGameObject );
        void * params [ 1 ] = { &typeObject };
        void * objects = Invoke( mFindObjectsOfTypeAll, nullptr, params,
            "Resources.FindObjectsOfTypeAll" );
        int count = ArrayLength( objects );
        for ( int i = 0; i < count; ++i ) {
            void * obj = ArrayElement( objects, i );
            std::string name = ObjectName( obj );
            if ( name == "SK_actor_female(Clone)" )
                return obj;
        }
        return nullptr;
    }

    static bool CheckResourceExists( const char * path ) {
        if ( !mResourceCheckExists || !api::string_new )
            return true;
        void * pathString = api::string_new( path );
        uint8_t categoryMain = 0;
        void * params [ 2 ] = { &pathString, &categoryMain };
        bool exists = false;
        return Unbox( Invoke( mResourceCheckExists, nullptr, params,
            "ResourceManager.CheckExists" ), exists ) && exists;
    }

    static bool EnsureLoader( ) {
        if ( g_loader )
            return true;
        if ( !api::object_new )
            return false;

        g_loader = api::object_new( clsSimpleAssetLoader );
        if ( !g_loader )
            return false;

        uint8_t channelRes = 1;
        uint8_t categoryMain = 0;
        uint32_t priorityDefault = 2;
        void * params [ 3 ] = { &channelRes, &categoryMain, &priorityDefault };
        Invoke( mLoaderCtor, g_loader, params, "SimpleAssetLoader.ctor" );
        if ( api::gchandle_new )
            g_loaderGcHandle = api::gchandle_new( g_loader, false );
        return true;
    }

    static void * InstantiateTargetModel( ) {
        if ( !EnsureLoader( ) || !api::string_new )
            return nullptr;

        void * pathString = api::string_new( TARGET_MODEL_PATH );
        void * hashParams [ 1 ] = { &pathString };
        void * hashBox = Invoke( mCreateStringPathHash, nullptr, hashParams,
            "StringPathHash.CreateStringPathHash" );
        int64_t pathHash = 0;
        if ( !Unbox( hashBox, pathHash ) ) {
            Log( "[replace] failed to create StringPathHash" );
            return nullptr;
        }

        bool useBeyondInstantiate = true;
        void * instantiateParams [ 2 ] = { &pathHash, &useBeyondInstantiate };
        void * handleBox = Invoke( mInstantiatePrefab, g_loader, instantiateParams,
            "SimpleAssetLoader.InstantiatePrefab" );
        if ( !handleBox )
            return nullptr;

        Invoke( mHandleLoadImmediate, handleBox, nullptr,
            "FAssetProxyLoaderHandle.LoadImmediate" );

        bool hasError = false;
        if ( Unbox( Invoke( mHandleGetHasError, handleBox, nullptr,
            "FAssetProxyLoaderHandle.get_hasError" ), hasError ) && hasError ) {
            std::string error = ReadString( Invoke( mHandleGetErrorCode, handleBox,
                nullptr, "FAssetProxyLoaderHandle.get_errorCode" ) );
            Log( "[replace] asset load error: " + error );
            return nullptr;
        }

        return Invoke( mHandleGet, handleBox, nullptr,
            "FAssetProxyLoaderHandle.Get" );
    }

    static void * MapBone( void * sourceBone, void * sourceRoot,
        const std::unordered_map< std::string, void * > & targetByPath,
        const std::unordered_map< std::string, std::vector< void * > > & targetByName ) {
        if ( !sourceBone )
            return nullptr;

        std::string sourcePath = RelativePath( sourceBone, sourceRoot );
        auto exact = targetByPath.find( sourcePath );
        if ( exact != targetByPath.end( ) )
            return exact->second;

        std::string sourceName = ObjectName( sourceBone );
        auto byName = targetByName.find( sourceName );
        if ( byName != targetByName.end( ) && byName->second.size( ) == 1 )
            return byName->second [ 0 ];

        for ( const auto & entry : targetByPath ) {
            if ( EndsWith( entry.first, "/" + sourcePath ) ||
                EndsWith( sourcePath, "/" + entry.first ) )
                return entry.second;
        }
        return nullptr;
    }

    static bool BindReplacement( void * targetRoot, void * replacementRoot ) {
        void * targetTransform = GetTransform( targetRoot );
        void * replacementTransform = GetTransform( replacementRoot );
        if ( !targetTransform || !replacementTransform )
            return false;

        if ( !CopyLocalTransform( replacementTransform, targetTransform ) )
            return false;
        if ( !CopyLayerToHierarchy( targetRoot, replacementRoot ) )
            Log( "[replace] warning: failed to copy silhouette layer" );

        void * targetTransforms = GetComponentsInChildren( targetRoot, clsTransform );
        void * sourceTransforms = GetComponentsInChildren( replacementRoot, clsTransform );
        void * sourceRenderers = GetComponentsInChildren(
            replacementRoot, clsSkinnedMeshRenderer );
        if ( !targetTransforms || !sourceTransforms || !sourceRenderers )
            return false;

        std::unordered_map< std::string, void * > targetByPath;
        std::unordered_map< std::string, std::vector< void * > > targetByName;
        int targetTransformCount = ArrayLength( targetTransforms );
        for ( int i = 0; i < targetTransformCount; ++i ) {
            void * transform = ArrayElement( targetTransforms, i );
            if ( !transform )
                continue;
            targetByPath [ RelativePath( transform, targetTransform ) ] = transform;
            targetByName [ ObjectName( transform ) ].push_back( transform );
        }

        std::vector< RendererBinding > bindings;
        std::unordered_set< void * > mappedSourceBones;
        std::unordered_set< void * > mappedTargetBones;
        int totalBones = 0;
        int mappedBones = 0;
        int fullyMappedRenderers = 0;
        int partiallyMappedRenderers = 0;
        int unmappedRenderers = 0;
        int mappedRootBones = 0;
        int rendererCount = ArrayLength( sourceRenderers );
        for ( int i = 0; i < rendererCount; ++i ) {
            void * renderer = ArrayElement( sourceRenderers, i );
            void * bonesArray = Invoke( mSkinnedGetBones, renderer, nullptr,
                "SkinnedMeshRenderer.get_bones" );
            int boneCount = ArrayLength( bonesArray );
            if ( boneCount <= 0 )
                continue;

            RendererBinding binding;
            binding.renderer = renderer;
            binding.bones.reserve( static_cast< size_t >( boneCount ) );
            for ( int boneIndex = 0; boneIndex < boneCount; ++boneIndex ) {
                void * sourceBone = ArrayElement( bonesArray, boneIndex );
                void * mapped = MapBone( sourceBone, replacementTransform,
                    targetByPath, targetByName );
                if ( mapped ) {
                    ++binding.mapped;
                    ++mappedBones;
                    mappedSourceBones.insert( sourceBone );
                    mappedTargetBones.insert( mapped );
                    binding.bones.push_back( mapped );
                }
                else {
                    binding.bones.push_back( sourceBone );
                }
                ++totalBones;
            }

            void * sourceRootBone = Invoke( mSkinnedGetRootBone, renderer, nullptr,
                "SkinnedMeshRenderer.get_rootBone" );
            binding.rootBone = MapBone( sourceRootBone, replacementTransform,
                targetByPath, targetByName );
            if ( binding.rootBone )
                ++mappedRootBones;
            else
                binding.rootBone = sourceRootBone;

            if ( binding.mapped == boneCount )
                ++fullyMappedRenderers;
            else if ( binding.mapped > 0 )
                ++partiallyMappedRenderers;
            else
                ++unmappedRenderers;
            bindings.push_back( std::move( binding ) );
        }

        double coverage = totalBones > 0
            ? static_cast< double >( mappedBones ) / totalBones : 0.0;
        char coverageLog [ 192 ];
        sprintf_s( coverageLog,
            "[replace] skeleton mapping: %d/%d bones (%.1f%%), %zu renderers",
            mappedBones, totalBones, coverage * 100.0, bindings.size( ) );
        Log( coverageLog );

        char rendererLog [ 256 ];
        sprintf_s( rendererLog,
            "[replace] renderer mapping: full=%d partial=%d zero=%d, "
            "rootBones=%d/%zu, uniqueBones=%zu source -> %zu target",
            fullyMappedRenderers, partiallyMappedRenderers, unmappedRenderers,
            mappedRootBones, bindings.size( ), mappedSourceBones.size( ),
            mappedTargetBones.size( ) );
        Log( rendererLog );

        if ( mappedTargetBones.size( ) < MIN_UNIQUE_MAPPED_BONES ||
            coverage < MIN_SKELETON_MAPPING_COVERAGE ) {
            char rejection [ 256 ];
            sprintf_s( rejection,
                "[replace] mapping coverage too low (requires %.0f%% and %zu "
                "unique target bones); original model retained",
                MIN_SKELETON_MAPPING_COVERAGE * 100.0,
                MIN_UNIQUE_MAPPED_BONES );
            Log( rejection );
            return false;
        }

        for ( auto & binding : bindings ) {
            void * newBones = api::array_new( clsTransform, binding.bones.size( ) );
            if ( !newBones )
                return false;
            for ( int i = 0; i < static_cast< int >( binding.bones.size( ) ); ++i )
                SetArrayElement( newBones, i, binding.bones [ i ] );

            if ( !SetBones( binding.renderer, newBones ) )
                return false;

            if ( !SetRootBone( binding.renderer, binding.rootBone ) ||
                !SetEnabled( binding.renderer, true ) )
                return false;
        }

        void * targetRenderers = GetComponentsInChildren(
            targetRoot, clsSkinnedMeshRenderer );
        int targetRendererCount = ArrayLength( targetRenderers );
        g_disabledRenderers.clear( );
        for ( int i = 0; i < targetRendererCount; ++i ) {
            void * renderer = ArrayElement( targetRenderers, i );
            if ( !renderer )
                continue;
            if ( !SetEnabled( renderer, false ) ) {
                for ( void * disabled : g_disabledRenderers )
                    SetEnabled( disabled, true );
                g_disabledRenderers.clear( );
                return false;
            }
            g_disabledRenderers.push_back( renderer );
        }

        if ( !SetActive( replacementRoot, true ) ) {
            for ( void * disabled : g_disabledRenderers )
                SetEnabled( disabled, true );
            g_disabledRenderers.clear( );
            return false;
        }
        return true;
    }

    static bool ResolveCompleteReplacementApis( ) {
        if ( !api::initialized )
            api::init( );
        if ( !api::initialized )
            return false;

        clsObject = FindClass( "UnityEngine", "Object" );
        clsGameObject = FindClass( "UnityEngine", "GameObject" );
        clsComponent = FindClass( "UnityEngine", "Component" );
        clsTransform = FindClass( "UnityEngine", "Transform" );
        clsRenderer = FindClass( "UnityEngine", "Renderer" );
        clsSkinnedMeshRenderer = FindClass( "UnityEngine", "SkinnedMeshRenderer" );
        clsBehaviour = FindClass( "UnityEngine", "Behaviour" );
        clsAnimator = FindClass( "UnityEngine", "Animator" );
        clsAnimationClip = FindClass( "UnityEngine", "AnimationClip" );
        clsNpcSimpleOneStateGraph = FindClass(
            "Beyond.NPC.AnimationGraphScript",
            "NPCCPUAnimationGraphScript_SimpleOneState" );
        if ( !clsObject || !clsGameObject || !clsComponent || !clsTransform ||
            !clsRenderer || !clsSkinnedMeshRenderer || !clsBehaviour ||
            !clsAnimator || !clsAnimationClip || !clsNpcSimpleOneStateGraph )
            return false;

        mObjectGetName = FindMethod( clsObject, "get_name", 0 );
        mGameObjectGetTransform = FindMethod( clsGameObject, "get_transform", 0 );
        mGameObjectGetLayer = FindMethod( clsGameObject, "get_layer", 0 );
        mGameObjectSetLayer = FindMethod( clsGameObject, "set_layer", 1 );
        mGameObjectSetActive = FindMethod( clsGameObject, "SetActive", 1 );
        mComponentGetGameObject = FindMethod( clsComponent, "get_gameObject", 0 );
        mGetComponentsInChildren = FindMethod( clsGameObject,
            "GetComponentsInChildren", 2, "System.Type" );
        mTransformGetParent = FindMethod( clsTransform, "get_parent", 0 );
        mTransformSetParent = FindMethod( clsTransform, "SetParent", 2 );
        mGetPosition = FindMethod( clsTransform, "get_position", 0 );
        mSetPosition = FindMethod( clsTransform, "set_position", 1 );
        mGetLocalPosition = FindMethod( clsTransform, "get_localPosition", 0 );
        mSetLocalPosition = FindMethod( clsTransform, "set_localPosition", 1 );
        mGetLocalRotation = FindMethod( clsTransform, "get_localRotation", 0 );
        mSetLocalRotation = FindMethod( clsTransform, "set_localRotation", 1 );
        mGetLocalScale = FindMethod( clsTransform, "get_localScale", 0 );
        mSetLocalScale = FindMethod( clsTransform, "set_localScale", 1 );
        mRendererSetEnabled = FindMethod( clsRenderer, "set_enabled", 1 );
        mSkinnedGetBones = FindMethod( clsSkinnedMeshRenderer, "get_bones", 0 );
        mSkinnedSetBones = FindMethod( clsSkinnedMeshRenderer, "set_bones", 1 );
        mSkinnedGetRootBone = FindMethod( clsSkinnedMeshRenderer, "get_rootBone", 0 );
        mSkinnedSetRootBone = FindMethod( clsSkinnedMeshRenderer, "set_rootBone", 1 );
        mBehaviourGetEnabled = FindMethod( clsBehaviour, "get_enabled", 0 );
        mBehaviourSetEnabled = FindMethod( clsBehaviour, "set_enabled", 1 );
        mBehaviourGetIsActiveAndEnabled = FindMethod(
            clsBehaviour, "get_isActiveAndEnabled", 0 );
        mAnimatorGetIsHuman = FindMethod( clsAnimator, "get_isHuman", 0 );
        mAnimatorGetIsInitialized = FindMethod(
            clsAnimator, "get_isInitialized", 0 );
        mAnimatorGetHasBoundPlayables = FindMethod(
            clsAnimator, "get_hasBoundPlayables", 0 );
        mAnimatorGetAvatar = FindMethod( clsAnimator, "get_avatar", 0 );
        mAnimatorGetCullingMode = FindMethod(
            clsAnimator, "get_cullingMode", 0 );
        mAnimatorSetCullingMode = FindMethod(
            clsAnimator, "set_cullingMode", 1 );
        mAnimatorGetUpdateMode = FindMethod(
            clsAnimator, "get_updateMode", 0 );
        mAnimatorSetUpdateMode = FindMethod(
            clsAnimator, "set_updateMode", 1 );
        mAnimatorRebind = FindMethod( clsAnimator, "Rebind", 1 );
        mClipGetLength = FindMethod( clsAnimationClip, "get_length", 0 );
        mClipGetEmpty = FindMethod( clsAnimationClip, "get_empty", 0 );
        mClipGetLegacy = FindMethod( clsAnimationClip, "get_legacy", 0 );
        mClipGetHumanMotion = FindMethod(
            clsAnimationClip, "get_humanMotion", 0 );
        mNpcGraphCtor = FindMethod( clsNpcSimpleOneStateGraph, ".ctor", 0 );
        mNpcGraphInit = FindMethod( clsNpcSimpleOneStateGraph, "Init", 2,
            "UnityEngine.Animator" );
        mNpcGraphSetMainAnimClip = FindMethod( clsNpcSimpleOneStateGraph,
            "SetMainAnimClip", 1, "UnityEngine.AnimationClip" );
        mNpcGraphSetMainClipSpeed = FindMethod( clsNpcSimpleOneStateGraph,
            "SetMainClipSpeed", 1, "System.Single" );
        mNpcGraphPlay = FindMethod( clsNpcSimpleOneStateGraph, "Play", 0 );
        mNpcGraphDestroy = FindMethod(
            clsNpcSimpleOneStateGraph, "Destory", 0 );

        bool ready = mObjectGetName && mGameObjectGetTransform &&
            mGameObjectGetLayer && mGameObjectSetLayer && mGameObjectSetActive &&
            mComponentGetGameObject && mGetComponentsInChildren &&
            mTransformGetParent && mTransformSetParent && mGetPosition &&
            mSetPosition && mGetLocalPosition &&
            mSetLocalPosition && mGetLocalRotation && mSetLocalRotation &&
            mGetLocalScale && mSetLocalScale && mRendererSetEnabled &&
            mSkinnedGetBones && mSkinnedSetBones && mSkinnedGetRootBone &&
            mSkinnedSetRootBone && mBehaviourGetEnabled &&
            mBehaviourSetEnabled && mBehaviourGetIsActiveAndEnabled &&
            mAnimatorGetIsHuman && mAnimatorGetIsInitialized &&
            mAnimatorGetHasBoundPlayables && mAnimatorGetAvatar &&
            mAnimatorGetCullingMode && mAnimatorSetCullingMode &&
            mAnimatorGetUpdateMode && mAnimatorSetUpdateMode &&
            mAnimatorRebind && mClipGetLength && mClipGetEmpty &&
            mClipGetLegacy && mClipGetHumanMotion && mNpcGraphCtor &&
            mNpcGraphInit && mNpcGraphSetMainAnimClip &&
            mNpcGraphSetMainClipSpeed && mNpcGraphPlay &&
            mNpcGraphDestroy && api::runtime_invoke && api::class_get_type &&
            api::type_get_object && api::object_unbox && api::array_length &&
            api::array_new && api::object_new && api::string_length_fn &&
            api::string_chars &&
            g_resourceManagerLoadHashType && g_gameObjectAddComponent &&
            g_playableGraphCreateInjected &&
            g_animationClipPlayableCreateHandle &&
            g_animationClipPlayableSetOverrideLoopTime &&
            g_animationClipPlayableSetLoopTime &&
            g_animationMixerPlayableCreateHandle &&
            g_playableHandleSetInputCount &&
            g_playableGraphConnectInternal &&
            g_playableHandleSetInputWeight && g_createAnimationOutput &&
            g_animationOutputSetTarget && g_playableOutputSetSource &&
            g_playableHandleSetWrapMode && g_playableHandleSetSpeed &&
            g_playableHandleGetTime && g_playableHandleSetTime &&
            g_playableGraphSetUpdateMode &&
            g_playableGraphPlay && g_playableGraphEvaluate &&
            g_playableGraphDestroy;
        Log( ready
            ? "[complete-replace] Unity model and game-native animation graph APIs resolved on the captured game thread"
            : "[complete-replace] one or more Unity model/animation APIs are unavailable" );
        return ready;
    }

    static bool ResolveApis( ) {
        clsObject = FindClass( "UnityEngine", "Object" );
        clsResources = FindClass( "UnityEngine", "Resources" );
        clsGameObject = FindClass( "UnityEngine", "GameObject" );
        clsComponent = FindClass( "UnityEngine", "Component" );
        clsTransform = FindClass( "UnityEngine", "Transform" );
        clsRenderer = FindClass( "UnityEngine", "Renderer" );
        clsSkinnedMeshRenderer = FindClass( "UnityEngine", "SkinnedMeshRenderer" );
        clsResourceManager = FindClass( "Beyond.Resource", "ResourceManager" );
        clsSimpleAssetLoader = FindClass( "Beyond.Resource", "SimpleAssetLoader" );
        clsStringPathHash = FindClass( "Beyond.Resource", "StringPathHash" );
        clsAssetLoaderHandle = FindClass( "Beyond.Resource", "FAssetProxyLoaderHandle" );

        if ( !clsObject || !clsResources || !clsGameObject || !clsComponent ||
            !clsTransform ||
            !clsRenderer || !clsSkinnedMeshRenderer || !clsResourceManager ||
            !clsSimpleAssetLoader || !clsStringPathHash || !clsAssetLoaderHandle )
            return false;

        mFindObjectsOfTypeAll = FindMethod( clsResources,
            "FindObjectsOfTypeAll", 1, "System.Type" );
        mObjectGetName = FindMethod( clsObject, "get_name", 0 );
        mObjectDestroy = FindMethod( clsObject, "Destroy", 1, "UnityEngine.Object" );
        mGameObjectFind = FindMethod( clsGameObject, "Find", 1, "System.String" );
        mGameObjectGetTransform = FindMethod( clsGameObject, "get_transform", 0 );
        mGameObjectGetLayer = FindMethod( clsGameObject, "get_layer", 0 );
        mGameObjectSetLayer = FindMethod( clsGameObject, "set_layer", 1 );
        mGameObjectSetActive = FindMethod( clsGameObject, "SetActive", 1 );
        mComponentGetGameObject = FindMethod( clsComponent, "get_gameObject", 0 );
        mGetComponentsInChildren = FindMethod( clsGameObject,
            "GetComponentsInChildren", 2, "System.Type" );
        mTransformGetParent = FindMethod( clsTransform, "get_parent", 0 );
        mTransformSetParent = FindMethod( clsTransform, "SetParent", 2 );
        mGetPosition = FindMethod( clsTransform, "get_position", 0 );
        mSetPosition = FindMethod( clsTransform, "set_position", 1 );
        mGetLocalPosition = FindMethod( clsTransform, "get_localPosition", 0 );
        mSetLocalPosition = FindMethod( clsTransform, "set_localPosition", 1 );
        mGetLocalRotation = FindMethod( clsTransform, "get_localRotation", 0 );
        mSetLocalRotation = FindMethod( clsTransform, "set_localRotation", 1 );
        mGetLocalScale = FindMethod( clsTransform, "get_localScale", 0 );
        mSetLocalScale = FindMethod( clsTransform, "set_localScale", 1 );
        mRendererSetEnabled = FindMethod( clsRenderer, "set_enabled", 1 );
        mSkinnedGetBones = FindMethod( clsSkinnedMeshRenderer, "get_bones", 0 );
        mSkinnedSetBones = FindMethod( clsSkinnedMeshRenderer, "set_bones", 1 );
        mSkinnedGetRootBone = FindMethod( clsSkinnedMeshRenderer, "get_rootBone", 0 );
        mSkinnedSetRootBone = FindMethod( clsSkinnedMeshRenderer, "set_rootBone", 1 );
        mResourceCheckExists = FindMethod( clsResourceManager, "CheckExists", 2,
            "System.String" );
        mLoaderCtor = FindMethod( clsSimpleAssetLoader, ".ctor", 3 );
        mCreateStringPathHash = FindMethod( clsStringPathHash,
            "CreateStringPathHash", 1, "System.String" );
        mInstantiatePrefab = FindMethod( clsSimpleAssetLoader,
            "InstantiatePrefab", 2, "Beyond.Resource.StringPathHash" );
        mHandleLoadImmediate = FindMethod( clsAssetLoaderHandle,
            "LoadImmediate", 0 );
        mHandleGet = FindMethod( clsAssetLoaderHandle, "Get", 0, nullptr,
            "UnityEngine.Object", true );
        mHandleGetHasError = FindMethod( clsAssetLoaderHandle, "get_hasError", 0 );
        mHandleGetErrorCode = FindMethod( clsAssetLoaderHandle, "get_errorCode", 0 );

        return mFindObjectsOfTypeAll && mObjectGetName && mObjectDestroy &&
            mGameObjectFind &&
            mGameObjectGetTransform && mGameObjectGetLayer &&
            mGameObjectSetLayer && mGameObjectSetActive &&
            mComponentGetGameObject &&
            mGetComponentsInChildren && mTransformGetParent && mTransformSetParent &&
            mGetPosition && mSetPosition &&
            mGetLocalPosition && mSetLocalPosition && mGetLocalRotation &&
            mSetLocalRotation && mGetLocalScale && mSetLocalScale &&
            mRendererSetEnabled && mSkinnedGetBones && mSkinnedSetBones &&
            mSkinnedGetRootBone && mSkinnedSetRootBone && mLoaderCtor &&
            mCreateStringPathHash && mInstantiatePrefab && mHandleLoadImmediate &&
            mHandleGet && api::object_new && api::string_new && api::array_new;
    }

    static void ReplaceNow( ) {
        if ( g_replacementRoot ) {
            Log( "[replace] replacement is already active; press F10 to restore" );
            return;
        }

        Log( "[replace] starting Aglina replacement on Unity main thread" );
        if ( !ResolveApis( ) ) {
            Log( "[replace] required classes or methods were not resolved" );
            return;
        }

        void * targetRoot = FindTargetRoot( );
        if ( !targetRoot ) {
            Log( "[replace] SK_actor_female(Clone) not found; trigger F9 during silhouette" );
            return;
        }
        Log( "[replace] target=" + ObjectName( targetRoot ) );

        if ( !CheckResourceExists( TARGET_MODEL_PATH ) ) {
            Log( std::string( "[replace] resource does not exist: " ) +
                TARGET_MODEL_PATH );
            return;
        }
        Log( std::string( "[replace] loading " ) + TARGET_MODEL_PATH );

        void * replacementRoot = InstantiateTargetModel( );
        if ( !replacementRoot ) {
            Log( "[replace] prefab instantiate returned null" );
            return;
        }

        Log( "[replace] loaded object class=" + ObjectClassName( replacementRoot ) +
            " name=" + ObjectName( replacementRoot ) );
        SetActive( replacementRoot, false );

        if ( !BindReplacement( targetRoot, replacementRoot ) ) {
            DestroyObject( replacementRoot );
            Log( "[replace] replacement aborted and temporary object destroyed" );
            return;
        }

        g_targetRoot = targetRoot;
        g_replacementRoot = replacementRoot;
        g_replacementActive = true;
        Log( "[replace] Aglina renderers are active; original animation remains in control" );
    }

    static void RestoreNow( ) {
        if ( !g_replacementRoot ) {
            Log( "[replace] no active replacement" );
            return;
        }

        RestoreOriginalRendererHierarchy( );

        DestroyWalkPlayable( );
        if ( g_originalLoginActorDisabled.load( ) && g_targetRoot )
            SetActive( g_targetRoot, true );

        SetActive( g_replacementRoot, false );
        DestroyObject( g_replacementRoot );
        g_replacementRoot = nullptr;
        g_targetRoot = nullptr;
        g_replacementActive = false;
        g_originalLoginActorDisabled = false;
        g_originalRenderersDisabled = false;
        g_walkPlayableStarted = false;
        Log( "[replace] original silhouette model restored" );
    }

} // namespace

namespace ModelReplacer {

    bool LoadConfiguration( const std::string & configPath ) {
        return LoadRuntimeConfiguration( configPath );
    }

    bool Initialize( HMODULE module ) {
        ( void ) module;
        if ( !g_modelReplacementConfiguredEnabled ) {
            g_redirectEnabled = false;
            Log( "[redirect] model replacement disabled by configuration; hooks not installed" );
            return true;
        }
        if ( g_cachedLoadGameObjectTarget && g_i18nLoadGameObjectTarget &&
            g_resolveI18NPathHashTarget && g_initInitialPathHashTarget &&
            g_initMainPathHashTarget && g_loadUntrackedHashTarget &&
            g_loadAssetInternalHashTarget && g_bundleLoaderLoadAssetTarget &&
             g_tryGetAssetInfoHashTarget && g_untrackedHandleGateTarget &&
             g_assetProxyFinishErrorTarget && g_bundleProxyLoadSyncTarget &&
             g_bundleProxyGetFilePathTarget && g_bundleProxyLoadFileTarget &&
             g_loginManagerReleaseSceneTarget &&
             g_loginSceneRootOnBindTarget &&
             g_loginAnimChangeStateTarget && g_loginAnimResetA1Target &&
             g_loginAnimPlayA1sTarget && g_loginAnimPlayA1A2Target &&
             g_loginAnimTickTarget && g_loginAnimReleaseTarget &&
             g_instantiateGameObjectParentTarget )
            return true;

        HMODULE gameAssembly = GetModuleHandleA( "GameAssembly.dll" );
        if ( !gameAssembly ) {
            Log( "[redirect] GameAssembly.dll is not loaded" );
            return false;
        }
        g_gameAssemblyBase = reinterpret_cast< uintptr_t >( gameAssembly );
        g_redirectHit = false;
        g_mainCategoryHit = false;
        g_mainLoadReturnedHandle = false;
        g_initialPathHashEntered = false;
        g_initialPathHashCompleted = false;
        g_mainPathHashEntered = false;
        g_mainPathHashCompleted = false;
        g_mainDiagnosticsStarted = false;
        g_mainDiagnosticsCompleted = false;
        g_resourceManagerInitialized = false;
        g_resourceManagerAsyncIniting = false;
        g_resourceManagerUsingBundle = false;
        g_stringCheckExists = false;
        g_hashCheckExists = false;
        g_rawHashCheckExists = false;
        g_initialPathHashCompletedTick = 0;
        g_mainPathHashCompletedTick = 0;
        g_targetResolvedHash = 0;
        g_loadAssetInternalReturnedProxy = false;
        g_bundleLoaderReturnedProxy = false;
        g_bundleLoaderProxyHasError = false;
        g_loadUntrackedReturnedHandle = false;
        g_loadUntrackedDirectIsValid = false;
        g_untrackedHandleGatePassed = false;
        g_tryGetAssetInfoLoadObserved = false;
        g_loadAssetInternalObserved = false;
        g_bundleLoaderObserved = false;
        g_loadUntrackedObserved = false;
        g_untrackedHandleGateObserved = false;
        g_assetProxyFinishErrorObserved = false;
        g_mainBundlePathObserved = false;
        g_bundleFileLoadCount = 0;
        g_bundleFileFailureCount = 0;
        g_bundleSyncCount = 0;
        g_bundleSyncFailureCount = 0;
        g_deferredLoadPending = false;
        g_deferredLoadAttempted = false;
        g_loginLoadContextCaptured = false;
        g_i18nLoadGameObjectMethod = nullptr;
        g_loginLoadThreadId = 0;
        g_deferredHandleRaw0 = 0;
        g_deferredHandleRaw1 = 0;
        g_deferredHandleRaw2 = 0;
        g_loginBindObserved = false;
        g_loginInstantiateCaptured = false;
        g_loginSessionActive = false;
        g_loginSessionCleanupInProgress = false;
        g_loginReloadSuspended = false;
        g_loginReloadResumeNextTick = 0;
        g_loginSessionGeneration = 0;
        g_pelicaPrefabResolved = false;
        g_pelicaInstantiateAttempted = false;
        g_pelicaInstantiateSucceeded = false;
        g_originalLoginActorDisabled = false;
        g_skeletonBindAttempted = false;
        g_skeletonBindSucceeded = false;
        g_originalRenderersDisabled = false;
        g_walkClipLoadAttempted = false;
        g_walkClipResolved = false;
        g_walkPlayableStarted = false;
        g_pendingReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_activeReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_lastRequestedReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_loginAnimationGateRequested = false;
        g_loginAnimationTickHeld = false;
        g_loginAnimationGateTimedOut = false;
        g_loginAnimationGateStartTick = 0;
        g_activeReplacementClipLength = 0.0f;
        g_activeReplacementClipElapsed = 0.0;
        g_loginActorPrefab = nullptr;
        g_loginActorParent = nullptr;
        g_loginActorInstance = nullptr;
        g_loginInstantiateMethod = nullptr;
        g_pelicaPrefab = nullptr;
        g_pelicaInstance = nullptr;
        for ( auto & clip : g_replacementClips )
            clip = nullptr;
        g_pelicaPrefabGcHandle = 0;
        g_replacementClipGcHandles.fill( 0 );
        for ( auto & prefab : g_originalLoginPrefabs )
            prefab = nullptr;
        g_originalLoginPrefabGcHandles.fill( 0 );
        g_broadcastReplacementPhase =
            static_cast< int >( ReplacementPhase::SitLoop );
        g_sequenceDriverController = nullptr;
        g_walkPlayableGraph = { };
        g_activeReplacementPlayable = { };
        g_walkGraphScript = nullptr;
        g_walkGraphScriptGcHandle = 0;
        for ( auto & slot : g_loginActorSlots )
            slot = LoginActorReplacementState { };
        g_redirectEnabled = true;
        g_il2cppThreadCurrent = reinterpret_cast< Il2CppThreadCurrentFn >(
            GetProcAddress( gameAssembly, "il2cpp_thread_current" ) );
        g_il2cppStringNew = reinterpret_cast< Il2CppStringNewFn >(
            GetProcAddress( gameAssembly, "il2cpp_string_new" ) );
        if ( !g_il2cppThreadCurrent || !g_il2cppStringNew ) {
            Log( "[redirect] required guarded IL2CPP exports are unavailable" );
            g_gameAssemblyBase = 0;
            return false;
        }
        g_cachedLoadGameObjectTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_CACHED_LOAD_GAME_OBJECT );
        g_i18nLoadGameObjectTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_I18N_LOAD_GAME_OBJECT );
        g_resolveI18NPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_I18N_RESOLVE_PATH_HASH );
        g_initInitialPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INIT_INITIAL_PATH_HASH );
        g_initMainPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INIT_MAIN_PATH_HASH );
        g_resourceManagerIsInitialized = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_IS_INITIALIZED );
        g_resourceManagerIsAsyncIniting = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_IS_ASYNC_INITING );
        g_resourceManagerGetUsingBundle = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_USING_BUNDLE );
        g_resourceManagerCheckString = reinterpret_cast< CheckStringFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_CHECK_STRING );
        g_resourceManagerCheckHash = reinterpret_cast< CheckHashFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_CHECK_HASH );
        g_resourceManagerRawCheckHash = reinterpret_cast< CheckHashFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_RAW_CHECK_HASH );
        g_loadUntrackedHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_LOAD_UNTRACKED_HASH );
        g_loadAssetInternalHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOAD_ASSET_INTERNAL_HASH );
        g_bundleLoaderLoadAssetTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_LOADER_LOAD_ASSET );
        g_tryGetAssetInfoHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_TRY_GET_ASSET_INFO_HASH );
        g_untrackedHandleGateTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_UNTRACKED_HANDLE_GATE );
        g_assetProxyFinishErrorTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_FINISH_ERROR );
        g_bundleProxyLoadSyncTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_LOAD_SYNC );
        g_bundleProxyGetFilePathTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_GET_FILE_PATH );
        g_bundleProxyLoadFileTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_LOAD_FILE );
        g_loginManagerReleaseSceneTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_MANAGER_RELEASE_SCENE );
        g_loginSceneRootOnBindTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_SCENE_ROOT_ON_BIND );
        g_loginAnimChangeStateTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_CHANGE_STATE );
        g_loginAnimResetA1Target = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_RESET_A1 );
        g_loginAnimPlayA1sTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_PLAY_A1S );
        g_loginAnimPlayA1A2Target = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_PLAY_A1A2 );
        g_loginAnimTickTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_TICK );
        g_loginAnimReleaseTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_ANIM_RELEASE );
        g_instantiateGameObjectParentTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INSTANTIATE_GAME_OBJECT_PARENT );
        g_assetProxyHandleGet = reinterpret_cast< AssetProxyHandleGetFn >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_HANDLE_GET );
        g_gameObjectSetActive = reinterpret_cast< GameObjectSetActiveFn >(
            g_gameAssemblyBase + RVA_GAME_OBJECT_SET_ACTIVE );
        g_transformSetParent = reinterpret_cast< TransformSetParentFn >(
            g_gameAssemblyBase + RVA_TRANSFORM_SET_PARENT );
        g_gameObjectGetComponentsInChildren =
            reinterpret_cast< GameObjectGetComponentsInChildrenFn >(
                g_gameAssemblyBase + RVA_GAME_OBJECT_GET_COMPONENTS_IN_CHILDREN );
        g_rendererSetEnabled = reinterpret_cast< RendererSetEnabledFn >(
            g_gameAssemblyBase + RVA_RENDERER_SET_ENABLED );
        g_skinnedMeshRendererSetBones =
            reinterpret_cast< SkinnedMeshRendererSetBonesFn >(
                g_gameAssemblyBase + RVA_SKINNED_MESH_RENDERER_SET_BONES );
        g_skinnedMeshRendererSetRootBone =
            reinterpret_cast< SkinnedMeshRendererSetRootBoneFn >(
                g_gameAssemblyBase + RVA_SKINNED_MESH_RENDERER_SET_ROOT_BONE );
        g_resourceManagerLoadHashType =
            reinterpret_cast< ResourceManagerLoadHashTypeFn >(
                g_gameAssemblyBase + RVA_RESOURCE_MANAGER_LOAD_HASH_TYPE );
        g_gameObjectAddComponent = reinterpret_cast< GameObjectAddComponentFn >(
            g_gameAssemblyBase + RVA_GAME_OBJECT_ADD_COMPONENT );
        g_playableGraphCreateInjected =
            reinterpret_cast< PlayableGraphCreateInjectedFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_CREATE_INJECTED );
        g_animationClipPlayableCreateHandle =
            reinterpret_cast< AnimationClipPlayableCreateHandleFn >(
                g_gameAssemblyBase + RVA_ANIMATION_CLIP_PLAYABLE_CREATE_HANDLE );
        g_animationClipPlayableSetOverrideLoopTime =
            reinterpret_cast< AnimationClipPlayableSetLoopFlagFn >(
                g_gameAssemblyBase +
                RVA_ANIMATION_CLIP_PLAYABLE_SET_OVERRIDE_LOOP_TIME );
        g_animationClipPlayableSetLoopTime =
            reinterpret_cast< AnimationClipPlayableSetLoopFlagFn >(
                g_gameAssemblyBase +
                RVA_ANIMATION_CLIP_PLAYABLE_SET_LOOP_TIME );
        g_animationMixerPlayableCreateHandle =
            reinterpret_cast< AnimationMixerPlayableCreateHandleFn >(
                g_gameAssemblyBase +
                RVA_ANIMATION_MIXER_PLAYABLE_CREATE_HANDLE );
        g_playableHandleSetInputCount =
            reinterpret_cast< PlayableHandleSetInputCountFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_SET_INPUT_COUNT );
        g_playableGraphConnectInternal =
            reinterpret_cast< PlayableGraphConnectInternalFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_CONNECT_INTERNAL );
        g_playableHandleSetInputWeight =
            reinterpret_cast< PlayableHandleSetInputWeightFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_SET_INPUT_WEIGHT );
        g_createAnimationOutput = reinterpret_cast< CreateAnimationOutputFn >(
            g_gameAssemblyBase + RVA_CREATE_ANIMATION_OUTPUT );
        g_animationOutputSetTarget =
            reinterpret_cast< AnimationOutputSetTargetFn >(
                g_gameAssemblyBase + RVA_ANIMATION_OUTPUT_SET_TARGET );
        g_playableOutputSetSource =
            reinterpret_cast< PlayableOutputSetSourceFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_OUTPUT_SET_SOURCE );
        g_playableHandleSetWrapMode =
            reinterpret_cast< PlayableHandleSetWrapModeFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_SET_WRAP_MODE );
        g_playableHandleSetSpeed =
            reinterpret_cast< PlayableHandleSetSpeedFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_SET_SPEED );
        g_playableHandleGetTime =
            reinterpret_cast< PlayableHandleGetTimeFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_GET_TIME );
        g_playableHandleSetTime =
            reinterpret_cast< PlayableHandleSetTimeFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_HANDLE_SET_TIME );
        g_playableGraphSetUpdateMode =
            reinterpret_cast< PlayableGraphSetUpdateModeFn >(
                g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_SET_UPDATE_MODE );
        g_playableGraphPlay = reinterpret_cast< PlayableGraphActionFn >(
            g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_PLAY );
        g_playableGraphEvaluate = reinterpret_cast< PlayableGraphEvaluateFn >(
            g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_EVALUATE );
        g_playableGraphDestroy = reinterpret_cast< PlayableGraphActionFn >(
            g_gameAssemblyBase + RVA_PLAYABLE_GRAPH_DESTROY );
        g_untrackedHandleIsValid = reinterpret_cast< HandleGateFn >(
            g_gameAssemblyBase + RVA_UNTRACKED_HANDLE_IS_VALID );
        g_assetProxyHasError = reinterpret_cast< AssetProxyHasErrorFn >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_HAS_ERROR );
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            g_redirectTlsIndex = TlsAlloc( );
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            Log( "[redirect] TlsAlloc failed; target path hash fallback remains active" );

        const uint8_t expectedPrologue [ ] = {
            0x48, 0x89, 0x5C, 0x24, 0x08,
            0x48, 0x89, 0x6C, 0x24, 0x10,
            0x48, 0x89, 0x74, 0x24, 0x18,
            0x57
        };
        bool cachedPrologueMatches = false;
        bool i18nPrologueMatches = false;
        bool resolvePrologueMatches = false;
        bool initialHashPrologueMatches = false;
        bool mainHashPrologueMatches = false;
        bool deepLoadProloguesMatch = false;
        bool loginAttachProloguesMatch = false;
        bool loginAnimationProloguesMatch = false;
        bool loginLifecycleProloguesMatch = false;
        __try {
            cachedPrologueMatches = CheckPrologue( "CachedLoad<GameObject>",
                g_cachedLoadGameObjectTarget, expectedPrologue,
                sizeof( expectedPrologue ) );
            i18nPrologueMatches = CheckPrologue( "I18N Load<GameObject>",
                g_i18nLoadGameObjectTarget, expectedPrologue,
                sizeof( expectedPrologue ) );
            const uint8_t resolvePrologue [ ] = {
                0x48, 0x89, 0x4C, 0x24, 0x08, 0x53, 0x56
            };
            const uint8_t initPrologue [ ] = { 0x48, 0x83, 0xEC, 0x28 };
            resolvePrologueMatches = CheckPrologue( "I18N resolve path hash",
                g_resolveI18NPathHashTarget, resolvePrologue,
                sizeof( resolvePrologue ) );
            initialHashPrologueMatches = CheckPrologue( "InitInitPathHash",
                g_initInitialPathHashTarget, initPrologue,
                sizeof( initPrologue ) );
            mainHashPrologueMatches = CheckPrologue( "InitMainPathHash",
                g_initMainPathHashTarget, initPrologue,
                sizeof( initPrologue ) );
            const uint8_t loadUntrackedPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x10, 0x4C, 0x89, 0x44
            };
            const uint8_t loadInternalPrologue [ ] = {
                0x48, 0x8B, 0xC4, 0x48, 0x89, 0x58, 0x08
            };
            const uint8_t bundleLoadPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x18, 0x55, 0x57
            };
            const uint8_t assetInfoPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x20, 0x48, 0x89, 0x4C
            };
            const uint8_t handleGatePrologue [ ] = {
                0x48, 0x89, 0x54, 0x24, 0x10, 0x48, 0x89, 0x4C
            };
            const uint8_t finishErrorPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            const uint8_t bundleSyncOrFilePrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t bundlePathPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            deepLoadProloguesMatch =
                CheckPrologue( "BundleResourceManager.LoadUntracked(hash)",
                    g_loadUntrackedHashTarget, loadUntrackedPrologue,
                    sizeof( loadUntrackedPrologue ) ) &
                CheckPrologue( "BundleResourceManager._LoadAssetInternal(hash)",
                    g_loadAssetInternalHashTarget, loadInternalPrologue,
                    sizeof( loadInternalPrologue ) ) &
                CheckPrologue( "BundleLoader.Manager.LoadAsset",
                    g_bundleLoaderLoadAssetTarget, bundleLoadPrologue,
                    sizeof( bundleLoadPrologue ) ) &
                CheckPrologue( "TryGetAssetInfoFromPath(hash)",
                    g_tryGetAssetInfoHashTarget, assetInfoPrologue,
                    sizeof( assetInfoPrologue ) ) &
                CheckPrologue( "FAssetProxyUntrackedHandle gate",
                    g_untrackedHandleGateTarget, handleGatePrologue,
                    sizeof( handleGatePrologue ) ) &
                CheckPrologue( "AssetProxy._FinishWithError",
                    g_assetProxyFinishErrorTarget, finishErrorPrologue,
                    sizeof( finishErrorPrologue ) ) &
                CheckPrologue( "BundleProxy.LoadSync",
                    g_bundleProxyLoadSyncTarget, bundleSyncOrFilePrologue,
                    sizeof( bundleSyncOrFilePrologue ) ) &
                CheckPrologue( "BundleProxy._GetBundleFileFullPath",
                    g_bundleProxyGetFilePathTarget, bundlePathPrologue,
                    sizeof( bundlePathPrologue ) ) &
                CheckPrologue( "BundleProxy._LoadAssetBundle",
                    g_bundleProxyLoadFileTarget, bundleSyncOrFilePrologue,
                    sizeof( bundleSyncOrFilePrologue ) );
            const uint8_t loginOnBindPrologue [ ] = {
                0x40, 0x57, 0x48, 0x83, 0xEC, 0x50
            };
            const uint8_t loginManagerReleasePrologue [ ] = {
                0x40, 0x57, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t loginAnimReleasePrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t loginChangeStatePrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x57, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t loginResetA1Prologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x60
            };
            const uint8_t loginPlayA1sPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x10,
                0x55, 0x48, 0x8D, 0x6C, 0x24, 0xA9
            };
            const uint8_t loginPlayA1A2Prologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x18,
                0x55, 0x48, 0x8D, 0x6C, 0x24, 0xA9
            };
            const uint8_t loginTickPrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x30,
                0x48, 0x8B, 0xD9
            };
            const uint8_t handleGetPrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t setActivePrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            const uint8_t transformSetParentPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x48, 0x89, 0x74, 0x24, 0x10, 0x57
            };
            const uint8_t getComponentsInChildrenPrologue [ ] = {
                0x48, 0x83, 0xEC, 0x48, 0x33, 0xC0, 0x41, 0xB1, 0x01
            };
            const uint8_t rendererSetterPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x57, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t resourceLoadHashTypePrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x10,
                0x48, 0x89, 0x6C, 0x24, 0x18,
                0x48, 0x89, 0x7C, 0x24, 0x20, 0x41
            };
            const uint8_t addComponentPrologue [ ] = {
                0x66, 0x90, 0x45, 0x33, 0xC0, 0xE9
            };
            const uint8_t playableCommonPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x57, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t playableWidePrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x48, 0x89, 0x74, 0x24, 0x10, 0x57
            };
            const uint8_t playableActionPrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t playableSetSpeedPrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x30,
                0x48, 0x8B, 0x05
            };
            const uint8_t playableWeightPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x57, 0x48, 0x83, 0xEC, 0x30
            };
            loginAttachProloguesMatch =
                CheckPrologue( "LoginSceneRoot.OnBindToManager",
                    g_loginSceneRootOnBindTarget, loginOnBindPrologue,
                    sizeof( loginOnBindPrologue ) ) &
                CheckPrologue( "Instantiate<GameObject>(original,parent,false)",
                    g_instantiateGameObjectParentTarget, expectedPrologue,
                    sizeof( expectedPrologue ) ) &
                CheckPrologue( "FAssetProxyHandle.Get",
                    reinterpret_cast< void * >( g_assetProxyHandleGet ),
                    handleGetPrologue, sizeof( handleGetPrologue ) ) &
                CheckPrologue( "GameObject.SetActive",
                    reinterpret_cast< void * >( g_gameObjectSetActive ),
                    setActivePrologue, sizeof( setActivePrologue ) ) &
                CheckPrologue( "Transform.SetParent(Transform,bool)",
                    reinterpret_cast< void * >( g_transformSetParent ),
                    transformSetParentPrologue,
                    sizeof( transformSetParentPrologue ) ) &
                CheckPrologue( "GameObject.GetComponentsInChildren(Type,bool)",
                    reinterpret_cast< void * >(
                        g_gameObjectGetComponentsInChildren ),
                    getComponentsInChildrenPrologue,
                    sizeof( getComponentsInChildrenPrologue ) ) &
                CheckPrologue( "Renderer.set_enabled",
                    reinterpret_cast< void * >( g_rendererSetEnabled ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) ) &
                CheckPrologue( "SkinnedMeshRenderer.set_bones",
                    reinterpret_cast< void * >(
                        g_skinnedMeshRendererSetBones ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) ) &
                CheckPrologue( "SkinnedMeshRenderer.set_rootBone",
                    reinterpret_cast< void * >(
                        g_skinnedMeshRendererSetRootBone ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) ) &
                CheckPrologue( "ResourceManager.Load(hash,Type)",
                    reinterpret_cast< void * >( g_resourceManagerLoadHashType ),
                    resourceLoadHashTypePrologue,
                    sizeof( resourceLoadHashTypePrologue ) ) &
                CheckPrologue( "GameObject.AddComponent(Type)",
                    reinterpret_cast< void * >( g_gameObjectAddComponent ),
                    addComponentPrologue, sizeof( addComponentPrologue ) ) &
                CheckPrologue( "PlayableGraph.Create_Injected",
                    reinterpret_cast< void * >(
                        g_playableGraphCreateInjected ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "AnimationClipPlayable.CreateHandle",
                    reinterpret_cast< void * >(
                        g_animationClipPlayableCreateHandle ),
                    playableWidePrologue,
                    sizeof( playableWidePrologue ) ) &
                CheckPrologue(
                    "AnimationMixerPlayable.CreateHandleInternal_Injected",
                    reinterpret_cast< void * >(
                        g_animationMixerPlayableCreateHandle ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "PlayableHandle.SetInputCount_Injected",
                    reinterpret_cast< void * >(
                        g_playableHandleSetInputCount ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "PlayableGraph.ConnectInternal_Injected",
                    reinterpret_cast< void * >(
                        g_playableGraphConnectInternal ),
                    expectedPrologue, sizeof( expectedPrologue ) ) &
                CheckPrologue(
                    "PlayableHandle.SetInputWeightFromIndex_Injected",
                    reinterpret_cast< void * >(
                        g_playableHandleSetInputWeight ),
                    playableWeightPrologue,
                    sizeof( playableWeightPrologue ) ) &
                CheckPrologue( "CreateAnimationOutput",
                    reinterpret_cast< void * >( g_createAnimationOutput ),
                    playableWidePrologue,
                    sizeof( playableWidePrologue ) ) &
                CheckPrologue( "AnimationOutput.SetTarget",
                    reinterpret_cast< void * >( g_animationOutputSetTarget ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "PlayableOutput.SetSource",
                    reinterpret_cast< void * >( g_playableOutputSetSource ),
                    playableWidePrologue,
                    sizeof( playableWidePrologue ) ) &
                CheckPrologue( "PlayableHandle.SetWrapMode",
                    reinterpret_cast< void * >( g_playableHandleSetWrapMode ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "PlayableHandle.SetSpeed_Injected",
                    reinterpret_cast< void * >( g_playableHandleSetSpeed ),
                    playableSetSpeedPrologue,
                    sizeof( playableSetSpeedPrologue ) ) &
                CheckPrologue( "PlayableHandle.GetTime_Injected",
                    reinterpret_cast< void * >( g_playableHandleGetTime ),
                    playableActionPrologue,
                    sizeof( playableActionPrologue ) ) &
                CheckPrologue( "PlayableHandle.SetTime_Injected",
                    reinterpret_cast< void * >( g_playableHandleSetTime ),
                    playableSetSpeedPrologue,
                    sizeof( playableSetSpeedPrologue ) ) &
                CheckPrologue( "PlayableGraph.SetUpdateMode",
                    reinterpret_cast< void * >(
                        g_playableGraphSetUpdateMode ),
                    playableCommonPrologue,
                    sizeof( playableCommonPrologue ) ) &
                CheckPrologue( "PlayableGraph.Play",
                    reinterpret_cast< void * >( g_playableGraphPlay ),
                    playableActionPrologue,
                    sizeof( playableActionPrologue ) ) &
                CheckPrologue( "PlayableGraph.Destroy",
                    reinterpret_cast< void * >( g_playableGraphDestroy ),
                    playableActionPrologue,
                    sizeof( playableActionPrologue ) );
            loginAnimationProloguesMatch =
                CheckPrologue( "LoginSceneAnimCtrl._ChangeToState",
                    g_loginAnimChangeStateTarget,
                    loginChangeStatePrologue,
                    sizeof( loginChangeStatePrologue ) ) &
                CheckPrologue( "LoginSceneAnimCtrl._ResetToA1",
                    g_loginAnimResetA1Target,
                    loginResetA1Prologue,
                    sizeof( loginResetA1Prologue ) ) &
                CheckPrologue(
                    "LoginSceneAnimCtrl._PlayA1sAndTriggerNext",
                    g_loginAnimPlayA1sTarget,
                    loginPlayA1sPrologue,
                    sizeof( loginPlayA1sPrologue ) ) &
                CheckPrologue( "LoginSceneAnimCtrl._PlayA1A2Impl",
                    g_loginAnimPlayA1A2Target,
                    loginPlayA1A2Prologue,
                    sizeof( loginPlayA1A2Prologue ) ) &
                CheckPrologue( "LoginSceneAnimCtrl.Tick",
                    g_loginAnimTickTarget,
                    loginTickPrologue,
                    sizeof( loginTickPrologue ) );
            loginLifecycleProloguesMatch =
                CheckPrologue( "LoginManager._ReleaseLoginScene",
                    g_loginManagerReleaseSceneTarget,
                    loginManagerReleasePrologue,
                    sizeof( loginManagerReleasePrologue ) ) &
                CheckPrologue( "LoginSceneAnimCtrl.OnRelease",
                    g_loginAnimReleaseTarget,
                    loginAnimReleasePrologue,
                    sizeof( loginAnimReleasePrologue ) );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            cachedPrologueMatches = false;
            i18nPrologueMatches = false;
            resolvePrologueMatches = false;
            initialHashPrologueMatches = false;
            mainHashPrologueMatches = false;
            deepLoadProloguesMatch = false;
            loginAttachProloguesMatch = false;
            loginAnimationProloguesMatch = false;
            loginLifecycleProloguesMatch = false;
        }
        if ( !cachedPrologueMatches || !i18nPrologueMatches ||
             !resolvePrologueMatches || !initialHashPrologueMatches ||
             !mainHashPrologueMatches || !deepLoadProloguesMatch ||
             !loginAttachProloguesMatch ||
             !loginAnimationProloguesMatch ||
             !loginLifecycleProloguesMatch ) {
            Log( "[redirect] login/Main diagnostic signature mismatch; refusing hook" );
            g_cachedLoadGameObjectTarget = nullptr;
            g_i18nLoadGameObjectTarget = nullptr;
            g_resolveI18NPathHashTarget = nullptr;
            g_initInitialPathHashTarget = nullptr;
            g_initMainPathHashTarget = nullptr;
            g_loadUntrackedHashTarget = nullptr;
            g_loadAssetInternalHashTarget = nullptr;
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_tryGetAssetInfoHashTarget = nullptr;
            g_untrackedHandleGateTarget = nullptr;
            g_assetProxyFinishErrorTarget = nullptr;
            g_bundleProxyLoadSyncTarget = nullptr;
            g_bundleProxyGetFilePathTarget = nullptr;
            g_bundleProxyLoadFileTarget = nullptr;
            g_loginManagerReleaseSceneTarget = nullptr;
            g_loginSceneRootOnBindTarget = nullptr;
            g_loginAnimChangeStateTarget = nullptr;
            g_loginAnimResetA1Target = nullptr;
            g_loginAnimPlayA1sTarget = nullptr;
            g_loginAnimPlayA1A2Target = nullptr;
            g_loginAnimTickTarget = nullptr;
            g_loginAnimReleaseTarget = nullptr;
            g_instantiateGameObjectParentTarget = nullptr;
            g_assetProxyHandleGet = nullptr;
            g_gameObjectSetActive = nullptr;
            g_transformSetParent = nullptr;
            g_gameObjectGetComponentsInChildren = nullptr;
            g_rendererSetEnabled = nullptr;
            g_skinnedMeshRendererSetBones = nullptr;
            g_skinnedMeshRendererSetRootBone = nullptr;
            g_resourceManagerLoadHashType = nullptr;
            g_gameObjectAddComponent = nullptr;
            g_playableGraphCreateInjected = nullptr;
            g_animationClipPlayableCreateHandle = nullptr;
            g_animationClipPlayableSetOverrideLoopTime = nullptr;
            g_animationClipPlayableSetLoopTime = nullptr;
            g_animationMixerPlayableCreateHandle = nullptr;
            g_playableHandleSetInputCount = nullptr;
            g_playableGraphConnectInternal = nullptr;
            g_playableHandleSetInputWeight = nullptr;
            g_createAnimationOutput = nullptr;
            g_animationOutputSetTarget = nullptr;
            g_playableOutputSetSource = nullptr;
            g_playableHandleSetWrapMode = nullptr;
            g_playableHandleSetSpeed = nullptr;
            g_playableHandleGetTime = nullptr;
            g_playableHandleSetTime = nullptr;
            g_playableGraphSetUpdateMode = nullptr;
            g_playableGraphPlay = nullptr;
            g_playableGraphEvaluate = nullptr;
            g_playableGraphDestroy = nullptr;
            if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
                TlsFree( g_redirectTlsIndex );
                g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
            }
            g_gameAssemblyBase = 0;
            return false;
        }

        MH_STATUS status = MH_Initialize( );
        if ( status != MH_OK && status != MH_ERROR_ALREADY_INITIALIZED ) {
            Log( "[redirect] MH_Initialize failed: " +
                std::to_string( static_cast< int >( status ) ) );
            g_cachedLoadGameObjectTarget = nullptr;
            g_i18nLoadGameObjectTarget = nullptr;
            g_resolveI18NPathHashTarget = nullptr;
            g_initInitialPathHashTarget = nullptr;
            g_initMainPathHashTarget = nullptr;
            g_loadUntrackedHashTarget = nullptr;
            g_loadAssetInternalHashTarget = nullptr;
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_tryGetAssetInfoHashTarget = nullptr;
            g_untrackedHandleGateTarget = nullptr;
            g_assetProxyFinishErrorTarget = nullptr;
            g_bundleProxyLoadSyncTarget = nullptr;
            g_bundleProxyGetFilePathTarget = nullptr;
            g_bundleProxyLoadFileTarget = nullptr;
            g_loginManagerReleaseSceneTarget = nullptr;
            g_loginSceneRootOnBindTarget = nullptr;
            g_loginAnimChangeStateTarget = nullptr;
            g_loginAnimResetA1Target = nullptr;
            g_loginAnimPlayA1sTarget = nullptr;
            g_loginAnimPlayA1A2Target = nullptr;
            g_loginAnimTickTarget = nullptr;
            g_loginAnimReleaseTarget = nullptr;
            g_instantiateGameObjectParentTarget = nullptr;
            g_assetProxyHandleGet = nullptr;
            g_gameObjectSetActive = nullptr;
            g_transformSetParent = nullptr;
            g_gameObjectGetComponentsInChildren = nullptr;
            g_rendererSetEnabled = nullptr;
            g_skinnedMeshRendererSetBones = nullptr;
            g_skinnedMeshRendererSetRootBone = nullptr;
            g_resourceManagerLoadHashType = nullptr;
            g_gameObjectAddComponent = nullptr;
            g_playableGraphCreateInjected = nullptr;
            g_animationClipPlayableCreateHandle = nullptr;
            g_animationClipPlayableSetOverrideLoopTime = nullptr;
            g_animationClipPlayableSetLoopTime = nullptr;
            g_animationMixerPlayableCreateHandle = nullptr;
            g_playableHandleSetInputCount = nullptr;
            g_playableGraphConnectInternal = nullptr;
            g_playableHandleSetInputWeight = nullptr;
            g_createAnimationOutput = nullptr;
            g_animationOutputSetTarget = nullptr;
            g_playableOutputSetSource = nullptr;
            g_playableHandleSetWrapMode = nullptr;
            g_playableHandleSetSpeed = nullptr;
            g_playableHandleGetTime = nullptr;
            g_playableHandleSetTime = nullptr;
            g_playableGraphSetUpdateMode = nullptr;
            g_playableGraphPlay = nullptr;
            g_playableGraphEvaluate = nullptr;
            g_playableGraphDestroy = nullptr;
            if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
                TlsFree( g_redirectTlsIndex );
                g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
            }
            return false;
        }
        g_minHookInitialized = true;

        status = MH_CreateHook( g_instantiateGameObjectParentTarget,
            reinterpret_cast< void * >( &InstantiateGameObjectParentHook ),
            reinterpret_cast< void ** >(
                &g_originalInstantiateGameObjectParent ) );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_CreateHook(Instantiate<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginManagerReleaseSceneTarget,
            reinterpret_cast< void * >( &LoginManagerReleaseSceneHook ),
            reinterpret_cast< void ** >(
                &g_originalLoginManagerReleaseScene ) );
        if ( status != MH_OK ) {
            Log( "[login-session] MH_CreateHook(LoginManager._ReleaseLoginScene) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginSceneRootOnBindTarget,
            reinterpret_cast< void * >( &LoginSceneRootOnBindHook ),
            reinterpret_cast< void ** >( &g_originalLoginSceneRootOnBind ) );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_CreateHook(LoginSceneRoot.OnBindToManager) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimChangeStateTarget,
            reinterpret_cast< void * >( &LoginAnimChangeStateHook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimChangeState ) );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_CreateHook(_ChangeToState) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimResetA1Target,
            reinterpret_cast< void * >( &LoginAnimResetA1Hook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimResetA1 ) );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_CreateHook(_ResetToA1) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimPlayA1sTarget,
            reinterpret_cast< void * >( &LoginAnimPlayA1sHook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimPlayA1s ) );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_CreateHook(_PlayA1sAndTriggerNext) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimPlayA1A2Target,
            reinterpret_cast< void * >( &LoginAnimPlayA1A2Hook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimPlayA1A2 ) );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_CreateHook(_PlayA1A2Impl) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimTickTarget,
            reinterpret_cast< void * >( &LoginAnimTickHook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimTick ) );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_CreateHook(Tick) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginAnimReleaseTarget,
            reinterpret_cast< void * >( &LoginAnimReleaseHook ),
            reinterpret_cast< void ** >( &g_originalLoginAnimRelease ) );
        if ( status != MH_OK ) {
            Log( "[login-session] MH_CreateHook(LoginSceneAnimCtrl.OnRelease) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_initInitialPathHashTarget,
            reinterpret_cast< void * >( &InitInitialPathHashHook ),
            reinterpret_cast< void ** >( &g_originalInitInitialPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(InitInitPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_initMainPathHashTarget,
            reinterpret_cast< void * >( &InitMainPathHashHook ),
            reinterpret_cast< void ** >( &g_originalInitMainPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(InitMainPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_resolveI18NPathHashTarget,
            reinterpret_cast< void * >( &ResolveI18NPathHashHook ),
            reinterpret_cast< void ** >( &g_originalResolveI18NPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(I18N resolve path hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_tryGetAssetInfoHashTarget,
            reinterpret_cast< void * >( &TryGetAssetInfoHashHook ),
            reinterpret_cast< void ** >( &g_originalTryGetAssetInfoHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(TryGetAssetInfoFromPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_assetProxyFinishErrorTarget,
            reinterpret_cast< void * >( &AssetProxyFinishErrorHook ),
            reinterpret_cast< void ** >( &g_originalAssetProxyFinishError ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(AssetProxy._FinishWithError) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyLoadSyncTarget,
            reinterpret_cast< void * >( &BundleProxyLoadSyncHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyLoadSync ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy.LoadSync) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyGetFilePathTarget,
            reinterpret_cast< void * >( &BundleProxyGetFilePathHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyGetFilePath ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy._GetBundleFileFullPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyLoadFileTarget,
            reinterpret_cast< void * >( &BundleProxyLoadFileHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyLoadFile ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy._LoadAssetBundle) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleLoaderLoadAssetTarget,
            reinterpret_cast< void * >( &BundleLoaderLoadAssetHook ),
            reinterpret_cast< void ** >( &g_originalBundleLoaderLoadAsset ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(BundleLoader.LoadAsset) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loadAssetInternalHashTarget,
            reinterpret_cast< void * >( &LoadAssetInternalHashHook ),
            reinterpret_cast< void ** >( &g_originalLoadAssetInternalHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(_LoadAssetInternal hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loadUntrackedHashTarget,
            reinterpret_cast< void * >( &LoadUntrackedHashHook ),
            reinterpret_cast< void ** >( &g_originalLoadUntrackedHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(LoadUntracked hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_untrackedHandleGateTarget,
            reinterpret_cast< void * >( &UntrackedHandleGateHook ),
            reinterpret_cast< void ** >( &g_originalUntrackedHandleGate ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(untracked-handle gate) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_i18nLoadGameObjectTarget,
            reinterpret_cast< void * >( &I18NLoadGameObjectHook ),
            reinterpret_cast< void ** >( &g_originalI18NLoadGameObject ) );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_CreateHook(I18N Load<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_cachedLoadGameObjectTarget,
            reinterpret_cast< void * >( &CachedLoadGameObjectHook ),
            reinterpret_cast< void ** >( &g_originalCachedLoadGameObject ) );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_CreateHook(CachedLoad<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_EnableHook( g_instantiateGameObjectParentTarget );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_EnableHook(Instantiate<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginManagerReleaseSceneTarget );
        if ( status != MH_OK ) {
            Log( "[login-session] MH_EnableHook(LoginManager._ReleaseLoginScene) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginSceneRootOnBindTarget );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_EnableHook(LoginSceneRoot.OnBindToManager) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimChangeStateTarget );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_EnableHook(_ChangeToState) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimResetA1Target );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_EnableHook(_ResetToA1) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimPlayA1sTarget );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_EnableHook(_PlayA1sAndTriggerNext) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimPlayA1A2Target );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_EnableHook(_PlayA1A2Impl) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimTickTarget );
        if ( status != MH_OK ) {
            Log( "[sequence-sync] MH_EnableHook(Tick) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginAnimReleaseTarget );
        if ( status != MH_OK ) {
            Log( "[login-session] MH_EnableHook(LoginSceneAnimCtrl.OnRelease) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_initInitialPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(InitInitPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_initMainPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(InitMainPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_resolveI18NPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(I18N resolve path hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_tryGetAssetInfoHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(TryGetAssetInfoFromPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_assetProxyFinishErrorTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(AssetProxy._FinishWithError) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyLoadSyncTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy.LoadSync) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyGetFilePathTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy._GetBundleFileFullPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyLoadFileTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy._LoadAssetBundle) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleLoaderLoadAssetTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(BundleLoader.LoadAsset) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loadAssetInternalHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(_LoadAssetInternal hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loadUntrackedHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(LoadUntracked hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_untrackedHandleGateTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(untracked-handle gate) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_i18nLoadGameObjectTarget );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_EnableHook(I18N Load<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_cachedLoadGameObjectTarget );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_EnableHook(CachedLoad<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        Log( "[deferred-load] login actor capture armed; Aglina will attach after Main initialization" );
        return true;
    }

    void QueueReplace( ) {
        if ( !g_cachedLoadGameObjectTarget ) {
            Log( "[redirect] CachedLoad<GameObject> hook is not ready" );
            return;
        }
        Log( g_redirectHit.load( )
            ? "[deferred-load] status: female login actor was preserved and observed"
            : "[deferred-load] status: armed, female login actor load has not been observed" );
        Log( g_mainCategoryHit.load( )
            ? "[deferred-load] status: deferred Aglina Main load was issued"
            : "[deferred-load] status: deferred Aglina Main load was not issued" );
        char deferredMessage [ 384 ] = { 0 };
        snprintf( deferredMessage, sizeof( deferredMessage ),
            "[deferred-load] status: contextCaptured=%s pending=%s attempted=%s "
            "handle=%s raw=%016llX:%016llX:%08X",
            g_loginLoadContextCaptured.load( ) ? "true" : "false",
            g_deferredLoadPending.load( ) ? "true" : "false",
            g_deferredLoadAttempted.load( ) ? "true" : "false",
            g_mainLoadReturnedHandle.load( ) ? "non-zero" : "zero",
            static_cast< unsigned long long >(
                g_deferredHandleRaw0.load( ) ),
            static_cast< unsigned long long >(
                g_deferredHandleRaw1.load( ) ),
            g_deferredHandleRaw2.load( ) );
        Log( deferredMessage );
        char attachMessage [ 768 ] = { 0 };
        snprintf( attachMessage, sizeof( attachMessage ),
            "[replace-live] status: bind=%s instantiateCapture=%s prefab=%s clip=%s "
            "instantiateAttempted=%s instantiateSucceeded=%s "
            "walkLoadAttempted=%s walkPlayable=%s "
            "originalRootDisabled=%s originalRenderersDisabled=%s "
            "originalPrefab=%p parent=%p originalInstance=%p replacementPrefab=%p "
            "replacementInstance=%p walkClip=%p",
            g_loginBindObserved.load( ) ? "observed" : "not-observed",
            g_loginInstantiateCaptured.load( ) ? "captured" : "not-captured",
            g_pelicaPrefabResolved.load( ) ? "resolved" : "not-resolved",
            g_walkClipResolved.load( ) ? "resolved" : "not-resolved",
            g_pelicaInstantiateAttempted.load( ) ? "true" : "false",
            g_pelicaInstantiateSucceeded.load( ) ? "true" : "false",
            g_walkClipLoadAttempted.load( ) ? "true" : "false",
            g_walkPlayableStarted.load( ) ? "true" : "false",
            g_originalLoginActorDisabled.load( ) ? "true" : "false",
            g_originalRenderersDisabled.load( ) ? "true" : "false",
            g_loginActorPrefab.load( ), g_loginActorParent.load( ),
            g_loginActorInstance.load( ), g_pelicaPrefab.load( ),
            g_pelicaInstance.load( ),
            g_replacementClips [ static_cast< size_t >(
                ReplacementPhase::WalkLong ) ].load( ) );
        Log( attachMessage );
        for ( size_t i = 0; i < g_loginActorSlots.size( ); ++i ) {
            const auto & slot = g_loginActorSlots [ i ];
            char slotMessage [ 384 ] = { 0 };
            snprintf( slotMessage, sizeof( slotMessage ),
                "[replace-live] actor slot=%zu controller=%p target=%p "
                "replacement=%p active=%s phase=%d pending=%d",
                i, slot.controller, slot.targetRoot, slot.replacementRoot,
                slot.replacementActive ? "true" : "false",
                slot.activePhase, slot.pendingPhase );
            Log( slotMessage );
        }
        Log( g_initialPathHashCompleted.load( )
            ? "[main-diag] status: Initial path hash initialization completed"
            : "[main-diag] status: Initial path hash initialization was not observed" );
        Log( g_mainPathHashCompleted.load( )
            ? "[main-diag] status: Main path hash initialization completed"
            : "[main-diag] status: Main path hash initialization was not observed" );
        if ( g_mainDiagnosticsCompleted.load( ) ) {
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[main-diag] status: manager initialized=%s asyncIniting=%s usingBundle=%s; "
                "CheckExists string/hash/rawHash=%s/%s/%s; resolvedHash=%016llX",
                g_resourceManagerInitialized.load( ) ? "true" : "false",
                g_resourceManagerAsyncIniting.load( ) ? "true" : "false",
                g_resourceManagerUsingBundle.load( ) ? "true" : "false",
                g_stringCheckExists.load( ) ? "true" : "false",
                g_hashCheckExists.load( ) ? "true" : "false",
                g_rawHashCheckExists.load( ) ? "true" : "false",
                static_cast< unsigned long long >(
                    g_targetResolvedHash.load( ) ) );
            Log( message );
        }
        else {
            Log( "[main-diag] status: target Main diagnostic has not completed" );
        }
        if ( g_mainCategoryHit.load( ) ) {
            char message [ 384 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] status: assetInfo(load)=%s internalProxy=%s "
                "bundleProxy=%s bundleProxyError=%s untrackedHandle=%s "
                "directIsValid=%s outerGate=%s",
                g_tryGetAssetInfoLoadObserved.load( ) ? "observed" : "not-observed",
                BoolResult( g_loadAssetInternalObserved.load( ),
                    g_loadAssetInternalReturnedProxy.load( ) ),
                BoolResult( g_bundleLoaderObserved.load( ),
                    g_bundleLoaderReturnedProxy.load( ) ),
                BoolResult( g_bundleLoaderObserved.load( ),
                    g_bundleLoaderProxyHasError.load( ) ),
                BoolResult( g_loadUntrackedObserved.load( ),
                    g_loadUntrackedReturnedHandle.load( ) ),
                BoolResult( g_loadUntrackedObserved.load( ),
                    g_loadUntrackedDirectIsValid.load( ) ),
                BoolResult( g_untrackedHandleGateObserved.load( ),
                    g_untrackedHandleGatePassed.load( ) ) );
            Log( message );

            char bundleMessage [ 384 ] = { 0 };
            snprintf( bundleMessage, sizeof( bundleMessage ),
                "[bundle-diag] status: finishError=%s targetPath=%s "
                "fileLoads=%u fileFailures=%u syncLoads=%u syncFailures=%u",
                g_assetProxyFinishErrorObserved.load( ) ? "observed" : "not-observed",
                g_mainBundlePathObserved.load( ) ? "observed" : "not-observed",
                g_bundleFileLoadCount.load( ), g_bundleFileFailureCount.load( ),
                g_bundleSyncCount.load( ), g_bundleSyncFailureCount.load( ) );
            Log( bundleMessage );
        }
    }

    void QueueRestore( ) {
        g_redirectEnabled = false;
        g_deferredLoadPending = false;
        Log( "[deferred-load] disabled for future loads; current scene objects were not changed by F10" );
    }

    void Shutdown( ) {
        g_redirectEnabled = false;
        CleanupLoginReplacementSession( "module shutdown", true );
        g_pendingReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_activeReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_lastRequestedReplacementPhase =
            static_cast< int >( ReplacementPhase::None );
        g_loginAnimationGateRequested = false;
        g_loginAnimationTickHeld = false;
        g_loginAnimationGateTimedOut = false;
        g_loginAnimationGateStartTick = 0;
        if ( g_loginAnimReleaseTarget ) {
            MH_DisableHook( g_loginAnimReleaseTarget );
            MH_RemoveHook( g_loginAnimReleaseTarget );
            g_loginAnimReleaseTarget = nullptr;
            g_originalLoginAnimRelease = nullptr;
        }
        if ( g_loginAnimTickTarget ) {
            MH_DisableHook( g_loginAnimTickTarget );
            MH_RemoveHook( g_loginAnimTickTarget );
            g_loginAnimTickTarget = nullptr;
            g_originalLoginAnimTick = nullptr;
        }
        if ( g_loginAnimPlayA1A2Target ) {
            MH_DisableHook( g_loginAnimPlayA1A2Target );
            MH_RemoveHook( g_loginAnimPlayA1A2Target );
            g_loginAnimPlayA1A2Target = nullptr;
            g_originalLoginAnimPlayA1A2 = nullptr;
        }
        if ( g_loginAnimPlayA1sTarget ) {
            MH_DisableHook( g_loginAnimPlayA1sTarget );
            MH_RemoveHook( g_loginAnimPlayA1sTarget );
            g_loginAnimPlayA1sTarget = nullptr;
            g_originalLoginAnimPlayA1s = nullptr;
        }
        if ( g_loginAnimResetA1Target ) {
            MH_DisableHook( g_loginAnimResetA1Target );
            MH_RemoveHook( g_loginAnimResetA1Target );
            g_loginAnimResetA1Target = nullptr;
            g_originalLoginAnimResetA1 = nullptr;
        }
        if ( g_loginAnimChangeStateTarget ) {
            MH_DisableHook( g_loginAnimChangeStateTarget );
            MH_RemoveHook( g_loginAnimChangeStateTarget );
            g_loginAnimChangeStateTarget = nullptr;
            g_originalLoginAnimChangeState = nullptr;
        }
        if ( g_loginManagerReleaseSceneTarget ) {
            MH_DisableHook( g_loginManagerReleaseSceneTarget );
            MH_RemoveHook( g_loginManagerReleaseSceneTarget );
            g_loginManagerReleaseSceneTarget = nullptr;
            g_originalLoginManagerReleaseScene = nullptr;
        }
        if ( g_loginSceneRootOnBindTarget ) {
            MH_DisableHook( g_loginSceneRootOnBindTarget );
            MH_RemoveHook( g_loginSceneRootOnBindTarget );
            g_loginSceneRootOnBindTarget = nullptr;
            g_originalLoginSceneRootOnBind = nullptr;
        }
        if ( g_instantiateGameObjectParentTarget ) {
            MH_DisableHook( g_instantiateGameObjectParentTarget );
            MH_RemoveHook( g_instantiateGameObjectParentTarget );
            g_instantiateGameObjectParentTarget = nullptr;
            g_originalInstantiateGameObjectParent = nullptr;
        }
        if ( g_cachedLoadGameObjectTarget ) {
            MH_DisableHook( g_cachedLoadGameObjectTarget );
            MH_RemoveHook( g_cachedLoadGameObjectTarget );
            g_cachedLoadGameObjectTarget = nullptr;
            g_originalCachedLoadGameObject = nullptr;
        }
        if ( g_i18nLoadGameObjectTarget ) {
            MH_DisableHook( g_i18nLoadGameObjectTarget );
            MH_RemoveHook( g_i18nLoadGameObjectTarget );
            g_i18nLoadGameObjectTarget = nullptr;
            g_originalI18NLoadGameObject = nullptr;
        }
        if ( g_bundleProxyLoadFileTarget ) {
            MH_DisableHook( g_bundleProxyLoadFileTarget );
            MH_RemoveHook( g_bundleProxyLoadFileTarget );
            g_bundleProxyLoadFileTarget = nullptr;
            g_originalBundleProxyLoadFile = nullptr;
        }
        if ( g_bundleProxyGetFilePathTarget ) {
            MH_DisableHook( g_bundleProxyGetFilePathTarget );
            MH_RemoveHook( g_bundleProxyGetFilePathTarget );
            g_bundleProxyGetFilePathTarget = nullptr;
            g_originalBundleProxyGetFilePath = nullptr;
        }
        if ( g_bundleProxyLoadSyncTarget ) {
            MH_DisableHook( g_bundleProxyLoadSyncTarget );
            MH_RemoveHook( g_bundleProxyLoadSyncTarget );
            g_bundleProxyLoadSyncTarget = nullptr;
            g_originalBundleProxyLoadSync = nullptr;
        }
        if ( g_assetProxyFinishErrorTarget ) {
            MH_DisableHook( g_assetProxyFinishErrorTarget );
            MH_RemoveHook( g_assetProxyFinishErrorTarget );
            g_assetProxyFinishErrorTarget = nullptr;
            g_originalAssetProxyFinishError = nullptr;
        }
        if ( g_untrackedHandleGateTarget ) {
            MH_DisableHook( g_untrackedHandleGateTarget );
            MH_RemoveHook( g_untrackedHandleGateTarget );
            g_untrackedHandleGateTarget = nullptr;
            g_originalUntrackedHandleGate = nullptr;
        }
        if ( g_loadUntrackedHashTarget ) {
            MH_DisableHook( g_loadUntrackedHashTarget );
            MH_RemoveHook( g_loadUntrackedHashTarget );
            g_loadUntrackedHashTarget = nullptr;
            g_originalLoadUntrackedHash = nullptr;
        }
        if ( g_loadAssetInternalHashTarget ) {
            MH_DisableHook( g_loadAssetInternalHashTarget );
            MH_RemoveHook( g_loadAssetInternalHashTarget );
            g_loadAssetInternalHashTarget = nullptr;
            g_originalLoadAssetInternalHash = nullptr;
        }
        if ( g_bundleLoaderLoadAssetTarget ) {
            MH_DisableHook( g_bundleLoaderLoadAssetTarget );
            MH_RemoveHook( g_bundleLoaderLoadAssetTarget );
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_originalBundleLoaderLoadAsset = nullptr;
        }
        if ( g_tryGetAssetInfoHashTarget ) {
            MH_DisableHook( g_tryGetAssetInfoHashTarget );
            MH_RemoveHook( g_tryGetAssetInfoHashTarget );
            g_tryGetAssetInfoHashTarget = nullptr;
            g_originalTryGetAssetInfoHash = nullptr;
        }
        if ( g_resolveI18NPathHashTarget ) {
            MH_DisableHook( g_resolveI18NPathHashTarget );
            MH_RemoveHook( g_resolveI18NPathHashTarget );
            g_resolveI18NPathHashTarget = nullptr;
            g_originalResolveI18NPathHash = nullptr;
        }
        if ( g_initMainPathHashTarget ) {
            MH_DisableHook( g_initMainPathHashTarget );
            MH_RemoveHook( g_initMainPathHashTarget );
            g_initMainPathHashTarget = nullptr;
            g_originalInitMainPathHash = nullptr;
        }
        if ( g_initInitialPathHashTarget ) {
            MH_DisableHook( g_initInitialPathHashTarget );
            MH_RemoveHook( g_initInitialPathHashTarget );
            g_initInitialPathHashTarget = nullptr;
            g_originalInitInitialPathHash = nullptr;
        }
        if ( g_minHookInitialized ) {
            MH_Uninitialize( );
            g_minHookInitialized = false;
        }
        if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
            TlsFree( g_redirectTlsIndex );
            g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
        }
        g_gameAssemblyBase = 0;
        g_il2cppThreadCurrent = nullptr;
        g_il2cppStringNew = nullptr;
        g_resourceManagerIsInitialized = nullptr;
        g_resourceManagerIsAsyncIniting = nullptr;
        g_resourceManagerGetUsingBundle = nullptr;
        g_resourceManagerCheckString = nullptr;
        g_resourceManagerCheckHash = nullptr;
        g_resourceManagerRawCheckHash = nullptr;
        g_untrackedHandleIsValid = nullptr;
        g_assetProxyHasError = nullptr;
        g_assetProxyHandleGet = nullptr;
        g_gameObjectSetActive = nullptr;
        g_transformSetParent = nullptr;
        g_gameObjectGetComponentsInChildren = nullptr;
        g_rendererSetEnabled = nullptr;
        g_skinnedMeshRendererSetBones = nullptr;
        g_skinnedMeshRendererSetRootBone = nullptr;
        g_resourceManagerLoadHashType = nullptr;
        g_gameObjectAddComponent = nullptr;
        g_playableGraphCreateInjected = nullptr;
        g_animationClipPlayableCreateHandle = nullptr;
        g_animationClipPlayableSetOverrideLoopTime = nullptr;
        g_animationClipPlayableSetLoopTime = nullptr;
        g_animationMixerPlayableCreateHandle = nullptr;
        g_playableHandleSetInputCount = nullptr;
        g_playableGraphConnectInternal = nullptr;
        g_playableHandleSetInputWeight = nullptr;
        g_createAnimationOutput = nullptr;
        g_animationOutputSetTarget = nullptr;
        g_playableOutputSetSource = nullptr;
        g_playableHandleSetWrapMode = nullptr;
        g_playableHandleSetSpeed = nullptr;
        g_playableHandleGetTime = nullptr;
        g_playableHandleSetTime = nullptr;
        g_playableGraphSetUpdateMode = nullptr;
        g_playableGraphPlay = nullptr;
        g_playableGraphEvaluate = nullptr;
        g_playableGraphDestroy = nullptr;
        g_pelicaPrefabGcHandle = 0;
        g_replacementClipGcHandles.fill( 0 );
        g_walkPlayableGraph = { };
        g_activeReplacementPlayable = { };
        g_walkGraphScript = nullptr;
        g_walkGraphScriptGcHandle = 0;
    }

} // namespace ModelReplacer
