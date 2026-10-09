cmake_minimum_required(VERSION 3.25)

project(BetterEndfieldNextNative VERSION 3.1.1 LANGUAGES C CXX)

if(NOT WIN32)
    message(FATAL_ERROR "Better Endfield native runtime targets Windows only.")
endif()

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

# Injected modules must not bind to the game's local MSVC CRT. The client
# ships its own MSVCP140/VCRUNTIME140 versions, so /MD would make the Host and
# its modules share an ABI/runtime that is outside our control.
if(MSVC)
    set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")
endif()

set(BETTER_ENDFIELD_NEXT_STAGE_DIR "${CMAKE_BINARY_DIR}/stage/$<CONFIG>")
set(BETTER_ENDFIELD_NEXT_INCLUDE_DIR "${CMAKE_CURRENT_SOURCE_DIR}/shared/include")

add_executable(BetterEndfieldNext.InjectorLaunchGuardTests EXCLUDE_FROM_ALL tests/injector_launch_guard_tests.cpp)

add_subdirectory(
    "${CMAKE_CURRENT_SOURCE_DIR}/shared/third_party/minhook"
    "${CMAKE_BINARY_DIR}/minhook"
)

function(better_endfield_target_defaults target)
    target_include_directories(${target} PRIVATE "${BETTER_ENDFIELD_NEXT_INCLUDE_DIR}")
    target_compile_definitions(${target} PRIVATE UNICODE _UNICODE WIN32_LEAN_AND_MEAN NOMINMAX)

    if(MSVC)
        target_compile_options(${target} PRIVATE /W4 /permissive- /utf-8)
        target_compile_options(${target} PRIVATE $<$<CONFIG:Release>:/Gy> $<$<CONFIG:Release>:/Gw>)
        target_link_options(${target} PRIVATE $<$<CONFIG:Release>:/OPT:REF> $<$<CONFIG:Release>:/OPT:ICF>)
        set_property(TARGET ${target} PROPERTY INTERPROCEDURAL_OPTIMIZATION_RELEASE TRUE)
    endif()
endfunction()

add_library(BetterEndfieldNext.Host SHARED
    shared/host/bootstrap.cpp
    shared/host/pose_lease.cpp
    shared/host/dynamic_resolver.cpp
    shared/host/hook_broker.cpp
    shared/host/hook_diagnostics.cpp
    shared/host/host_runtime.cpp
    shared/host/logging.cpp
    shared/host/module_manager.cpp
    shared/host/settings_store.cpp
    shared/third_party_modules/third_party_host.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.Host)
target_link_libraries(BetterEndfieldNext.Host PRIVATE minhook ws2_32)
add_executable(BetterEndfieldNext.ThirdPartyHostTests EXCLUDE_FROM_ALL tests/third_party_host_test.cpp shared/third_party_modules/third_party_host.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ThirdPartyHostTests)
target_link_libraries(BetterEndfieldNext.ThirdPartyHostTests PRIVATE ws2_32)
add_library(BetterEndfieldNext.ThirdPartyEcho SHARED EXCLUDE_FROM_ALL ../tools/ThirdPartyModules/echo/native/echo.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ThirdPartyEcho)
set_target_properties(BetterEndfieldNext.ThirdPartyEcho PROPERTIES OUTPUT_NAME "example.echo")
set_target_properties(BetterEndfieldNext.Host PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Host"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/runtime"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/runtime"
)

add_library(BetterEndfieldNext.Model SHARED modules/model/module.cpp)
better_endfield_target_defaults(BetterEndfieldNext.Model)
set_target_properties(BetterEndfieldNext.Model PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Model"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_subdirectory(modules/custom_model)

add_library(BetterEndfieldNext.CustomModel SHARED
    modules/custom_model/module.cpp
    modules/custom_model/native_mesh_layout.cpp
    shared/third_party/minhook/src/hde/hde64.c
)
better_endfield_target_defaults(BetterEndfieldNext.CustomModel)
target_link_libraries(BetterEndfieldNext.CustomModel PRIVATE BetterEndfieldNext.CustomModelCore)
set_target_properties(BetterEndfieldNext.CustomModel PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.CustomModel"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_library(BetterEndfieldNext.Voice SHARED modules/voice/module.cpp)
better_endfield_target_defaults(BetterEndfieldNext.Voice)
set_target_properties(BetterEndfieldNext.Voice PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Voice"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_library(BetterEndfieldNext.Music SHARED
    modules/music/module.cpp
    modules/music/music_bank.h
    modules/music/omni_pcm_abi.h
    modules/music/local_track.inc
)
better_endfield_target_defaults(BetterEndfieldNext.Music)
target_link_libraries(BetterEndfieldNext.Music PRIVATE mfplat mfreadwrite mfuuid ole32)
set_target_properties(BetterEndfieldNext.Music PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Music"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_library(BetterEndfieldNext.UiModule SHARED
    modules/ui/module.cpp
    modules/ui/touch_input.cpp
    modules/ui/touch_input.h
)
better_endfield_target_defaults(BetterEndfieldNext.UiModule)
set_target_properties(BetterEndfieldNext.UiModule PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.UiModule"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_library(BetterEndfieldNext.Camera SHARED
    modules/camera/module.cpp
    modules/camera/free_camera_runtime.inc
    modules/camera/character_motion_runtime.inc
    modules/camera/mmd_director_runtime.inc
    modules/camera/mmd_library.h
    modules/camera/mmd_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfieldNext.Camera)

# EIEM DirectVmd port (AGPL-3.0, see modules/camera/eiem/UPSTREAM.md). Built as
# its own object library with EIEM's compile model: narrow Win32 APIs, min/max
# macros, C++17-style u8 literals. One TU per dancing character (eiem_slot.h).
# Only eiem_body.h is visible to module.cpp.
add_library(BetterEndfieldNext.CameraEiem OBJECT
    modules/camera/eiem/eiem_body.cpp
    modules/camera/eiem/eiem_body.h
    modules/camera/eiem/eiem_slot.h
    modules/camera/eiem/eiem_slot.inc
    modules/camera/eiem/eiem_slot0.cpp
    modules/camera/eiem/eiem_slot1.cpp
    modules/camera/eiem/eiem_slot2.cpp
    modules/camera/eiem/eiem_slot3.cpp
)
target_include_directories(BetterEndfieldNext.CameraEiem PRIVATE
    "${CMAKE_CURRENT_SOURCE_DIR}/shared/third_party/minhook/include"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem/compat"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem/upstream"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem"
    "${BETTER_ENDFIELD_NEXT_INCLUDE_DIR}"
)
if(MSVC)
    target_compile_options(BetterEndfieldNext.CameraEiem PRIVATE /W3 /utf-8 /EHsc /Zc:char8_t- /bigobj)
endif()
target_link_libraries(BetterEndfieldNext.Camera PRIVATE BetterEndfieldNext.CameraEiem winmm)
set_target_properties(BetterEndfieldNext.Camera PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Camera"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.MmdOverlay WIN32
    modules/camera/overlay/main.cpp
    modules/camera/overlay/mmd_overlay.rc
    modules/camera/mmd_library.h
    modules/camera/mmd_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfieldNext.MmdOverlay)
target_link_libraries(BetterEndfieldNext.MmdOverlay PRIVATE gdiplus shell32)
if(MSVC)
    target_link_options(BetterEndfieldNext.MmdOverlay PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfieldNext.MmdOverlay PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.MmdOverlay"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.ModelOverlay WIN32
    modules/custom_model/overlay/main.cpp
    modules/custom_model/overlay/model_overlay.rc
    modules/custom_model/overlay/model_library.h
    modules/custom_model/overlay/win32_overlay_window.h
)
better_endfield_target_defaults(BetterEndfieldNext.ModelOverlay)
target_link_libraries(BetterEndfieldNext.ModelOverlay PRIVATE BetterEndfieldNext.CustomModelCore gdiplus shell32)
if(MSVC)
    target_link_options(BetterEndfieldNext.ModelOverlay PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfieldNext.ModelOverlay PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.ModelOverlay"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.ModelOverlayTests EXCLUDE_FROM_ALL
    modules/custom_model/tests/model_overlay_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.ModelOverlayTests)
target_link_libraries(BetterEndfieldNext.ModelOverlayTests PRIVATE BetterEndfieldNext.CustomModelCore)

add_library(BetterEndfieldNext.Actions SHARED modules/actions/module.cpp)
better_endfield_target_defaults(BetterEndfieldNext.Actions)
set_target_properties(BetterEndfieldNext.Actions PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Actions"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.ActionsTests EXCLUDE_FROM_ALL modules/actions/dash_policy_tests.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ActionsTests)

add_executable(BetterEndfieldNext.ActionsEffectsTests EXCLUDE_FROM_ALL modules/actions/effects_lifecycle_tests.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ActionsEffectsTests)

add_executable(BetterEndfieldNext.ActionsExternalTests EXCLUDE_FROM_ALL modules/actions/external_loop_tests.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ActionsExternalTests)

add_executable(BetterEndfieldNext.ActionsPoseTests EXCLUDE_FROM_ALL modules/actions/pose_overlay_tests.cpp)
better_endfield_target_defaults(BetterEndfieldNext.ActionsPoseTests)

add_library(BetterEndfieldNext.Gacha SHARED
    modules/gacha/module.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.Gacha)
target_link_libraries(BetterEndfieldNext.Gacha PRIVATE advapi32)
set_target_properties(BetterEndfieldNext.Gacha PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Gacha"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_library(BetterEndfieldNext.CombatStats SHARED
    modules/combat_stats/module.cpp
    modules/combat_stats/combat_semantics.cpp
    modules/combat_stats/combat_semantics.h
    modules/combat_stats/rdps_math.h
    modules/combat_stats/combat_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfieldNext.CombatStats)
target_link_libraries(BetterEndfieldNext.CombatStats PRIVATE shell32)
set_target_properties(BetterEndfieldNext.CombatStats PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.CombatStats"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.CombatOverlay WIN32
    modules/combat_stats/overlay/main.cpp
    modules/combat_stats/overlay/combat_overlay.rc
    modules/combat_stats/overlay/character_assets.generated.h
    modules/combat_stats/overlay/avatars.generated.rcinc
    modules/combat_stats/combat_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfieldNext.CombatOverlay)
target_link_libraries(BetterEndfieldNext.CombatOverlay PRIVATE gdiplus shlwapi shell32)
if(MSVC)
    target_link_options(BetterEndfieldNext.CombatOverlay PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfieldNext.CombatOverlay PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.CombatOverlay"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules"
)

add_executable(BetterEndfieldNext.CombatSemanticsTests EXCLUDE_FROM_ALL
    tests/combat_semantics_tests.cpp
    modules/combat_stats/combat_semantics.cpp
    modules/combat_stats/combat_semantics.h
    modules/combat_stats/rdps_math.h
)
better_endfield_target_defaults(BetterEndfieldNext.CombatSemanticsTests)

option(BETTER_ENDFIELD_NEXT_BUILD_LEGACY_CUSTOM_MODEL_RESEARCH
    "Expose archived CustomModel PoC lifecycle tests" OFF)
if(BETTER_ENDFIELD_NEXT_BUILD_LEGACY_CUSTOM_MODEL_RESEARCH)
    add_subdirectory("${CMAKE_CURRENT_SOURCE_DIR}/../research/custom-model/legacy-runtime"
        "${CMAKE_CURRENT_BINARY_DIR}/research/custom-model-legacy")
endif()

add_executable(BetterEndfieldNext.CustomModelBindingTests EXCLUDE_FROM_ALL
    tests/custom_model_binding_tests.cpp
    modules/custom_model/native_mesh_layout.cpp
    shared/third_party/minhook/src/hde/hde64.c
)
better_endfield_target_defaults(BetterEndfieldNext.CustomModelBindingTests)
target_link_libraries(BetterEndfieldNext.CustomModelBindingTests PRIVATE BetterEndfieldNext.CustomModelCore)

add_executable(BetterEndfieldNext.AndroidWorldBindingTests EXCLUDE_FROM_ALL
    tests/android_world_binding_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidWorldBindingTests)

add_executable(BetterEndfieldNext.AndroidModelLodTests EXCLUDE_FROM_ALL
    tests/android_model_lod_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidModelLodTests)

add_executable(BetterEndfieldNext.AndroidTextureStreamingDiagnosticsTests EXCLUDE_FROM_ALL
    tests/android_texture_streaming_diagnostics_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidTextureStreamingDiagnosticsTests)

add_executable(BetterEndfieldNext.AndroidTextureBudgetPolicyTests EXCLUDE_FROM_ALL
    tests/android_texture_budget_policy_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidTextureBudgetPolicyTests)

add_executable(BetterEndfieldNext.AndroidTextureBudgetRuntimeTests EXCLUDE_FROM_ALL
    tests/android_texture_budget_runtime_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidTextureBudgetRuntimeTests)

add_executable(BetterEndfieldNext.AndroidLodStreamingObserverTests EXCLUDE_FROM_ALL
    tests/android_lod_streaming_observer_tests.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.AndroidLodStreamingObserverTests)

better_endfield_target_defaults(BetterEndfieldNext.BemValidate)

# Read-only research utility. It is deliberately excluded from the default
# build and release layout; build the target explicitly when runtime metadata
# signatures need to be refreshed.
add_library(BetterEndfieldNext.MusicProbe SHARED EXCLUDE_FROM_ALL
    research/music_probe/module.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.MusicProbe)
set_target_properties(BetterEndfieldNext.MusicProbe PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.MusicProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
)

# Standalone research probe for the mobile-UI input path. Also excluded from
# the default build: it injects synthetic touch to answer whether the Unity
# backend surfaces WM_POINTER as a Touchscreen device, and is not shipped.
add_executable(BetterEndfieldNext.TouchProbe EXCLUDE_FROM_ALL
    research/touch_probe/main.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.TouchProbe)
set_target_properties(BetterEndfieldNext.TouchProbe PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.TouchProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
)

# The probe must run at the game's integrity level or UIPI drops its injected
# input, so it is started from inside the game process rather than a shell.
add_library(BetterEndfieldNext.TouchProbeLauncher SHARED EXCLUDE_FROM_ALL
    research/touch_probe/launcher_module.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.TouchProbeLauncher)
set_target_properties(BetterEndfieldNext.TouchProbeLauncher PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.TouchProbeLauncher"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
)

# Buff resource table probe. Excluded from the default build and the release
# layout: it is a local development tool that dumps every BuffData through the
# client's own loader, because the shipped BuffData files are MemoryPack blobs
# that carry no field names. The source lives under tools/ with the rest of the
# buff-table pipeline rather than here, since nothing in a release builds it.
add_library(BetterEndfieldNext.BuffProbe SHARED EXCLUDE_FROM_ALL
    ../tools/BuffProbe/module.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.BuffProbe)
set_target_properties(BetterEndfieldNext.BuffProbe PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.BuffProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/research"
)

add_executable(BetterEndfieldNext.Injector
    loaders/injector/main.cpp
    loaders/injector/BetterEndfieldNext.Injector.rc
)
better_endfield_target_defaults(BetterEndfieldNext.Injector)
if(MSVC)
    target_link_options(BetterEndfieldNext.Injector PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfieldNext.Injector PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Injector"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/loaders"
)

# XInput is the only game-directory proxy. It forwards the system API and
# starts the Better Endfield Host after IL2CPP is ready.
add_library(BetterEndfieldNext.XInput14Proxy SHARED
    loaders/xinput/input_proxy.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.XInput14Proxy)
target_compile_definitions(BetterEndfieldNext.XInput14Proxy PRIVATE
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_MARKER=L"BetterEndfieldNext-xinput1_4-proxy.loaded"
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_STATUS=L"BetterEndfieldNext-xinput1_4-host.status"
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_XINPUT14
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_LOAD_HOST
)
set_target_properties(BetterEndfieldNext.XInput14Proxy PROPERTIES
    OUTPUT_NAME "xinput1_4"
    PREFIX ""
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/payloads"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/payloads"
)

add_library(BetterEndfieldNext.Bootstrap SHARED
    loaders/xinput/input_proxy.cpp
)
better_endfield_target_defaults(BetterEndfieldNext.Bootstrap)
target_compile_definitions(BetterEndfieldNext.Bootstrap PRIVATE
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_MARKER=L"BetterEndfieldNext-bootstrap.loaded"
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_STATUS=L"BetterEndfieldNext-bootstrap-host.status"
    BETTER_ENDFIELD_NEXT_INPUT_BOOTSTRAP
    BETTER_ENDFIELD_NEXT_INPUT_PROXY_LOAD_HOST
)
set_target_properties(BetterEndfieldNext.Bootstrap PROPERTIES
    OUTPUT_NAME "BetterEndfieldNext.Bootstrap"
    PREFIX ""
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/loaders"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/loaders"
)

add_custom_target(BetterEndfieldNext.Layout ALL
    COMMAND ${CMAKE_COMMAND} -E rm -rf "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/catalog"
    COMMAND ${CMAKE_COMMAND} -E rm -f
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/payloads/version.dll"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/payloads/dinput8.dll"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/model/betterendfieldnext.model.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.model.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/custom_model/betterendfieldnext.custom_model.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.custom_model.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../ui/BetterEndfieldNext.UI/Assets/model/character-names.json"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/model-character-names.json"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../ui/BetterEndfieldNext.UI/Assets/model/character-names-en.json"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/model-character-names-en.json"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/voice/betterendfieldnext.voice.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.voice.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/music/betterendfieldnext.music.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.music.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/combat_stats/betterendfieldnext.combat_stats.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.combat_stats.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/ui/betterendfieldnext.ui.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.ui.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/betterendfieldnext.camera.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.camera.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/betterendfieldnext.actions.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.actions.module.ini"
    COMMAND ${CMAKE_COMMAND} -E make_directory
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/actions"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/assets/pose_aglina.bin"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/actions/pose_aglina.bin"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/assets/pose_liino.bin"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/actions/pose_liino.bin"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/gacha/betterendfieldnext.gacha.module.ini"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/betterendfieldnext.gacha.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../manifests/combat/combat-semantics.besem"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/combat-semantics.besem"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../manifests/combat/buff-sources.bemap"
        "${BETTER_ENDFIELD_NEXT_STAGE_DIR}/modules/buff-sources.bemap"
    DEPENDS
        BetterEndfieldNext.Host
        BetterEndfieldNext.Model
        BetterEndfieldNext.CustomModel
        BetterEndfieldNext.Voice
        BetterEndfieldNext.Music
        BetterEndfieldNext.UiModule
        BetterEndfieldNext.Camera
        BetterEndfieldNext.Actions
        BetterEndfieldNext.Gacha
        BetterEndfieldNext.CombatStats
        BetterEndfieldNext.CombatOverlay
        BetterEndfieldNext.MmdOverlay
        BetterEndfieldNext.ModelOverlay
        BetterEndfieldNext.Injector
        BetterEndfieldNext.Bootstrap
        BetterEndfieldNext.XInput14Proxy
)
