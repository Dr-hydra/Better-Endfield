cmake_minimum_required(VERSION 3.25)

project(BetterEndfieldNative VERSION 3.1.1 LANGUAGES C CXX)

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

set(BETTER_ENDFIELD_STAGE_DIR "${CMAKE_BINARY_DIR}/stage/$<CONFIG>")
set(BETTER_ENDFIELD_INCLUDE_DIR "${CMAKE_CURRENT_SOURCE_DIR}/shared/include")

add_executable(BetterEndfield.FirstPersonMeshTests EXCLUDE_FROM_ALL tests/first_person_mesh_tests.cpp)

add_subdirectory(
    "${CMAKE_CURRENT_SOURCE_DIR}/shared/third_party/minhook"
    "${CMAKE_BINARY_DIR}/minhook"
)

function(better_endfield_target_defaults target)
    target_include_directories(${target} PRIVATE "${BETTER_ENDFIELD_INCLUDE_DIR}")
    target_compile_definitions(${target} PRIVATE UNICODE _UNICODE WIN32_LEAN_AND_MEAN NOMINMAX)

    if(MSVC)
        target_compile_options(${target} PRIVATE /W4 /permissive- /utf-8)
    endif()
endfunction()

add_library(BetterEndfield.Host SHARED
    shared/host/bootstrap.cpp
    shared/host/pose_lease.cpp
    shared/host/dynamic_resolver.cpp
    shared/host/hook_broker.cpp
    shared/host/host_runtime.cpp
    shared/host/logging.cpp
    shared/host/module_manager.cpp
    shared/host/settings_store.cpp
    shared/third_party_modules/third_party_host.cpp
)
better_endfield_target_defaults(BetterEndfield.Host)
target_link_libraries(BetterEndfield.Host PRIVATE minhook ws2_32)
add_executable(BetterEndfield.ThirdPartyHostTests EXCLUDE_FROM_ALL tests/third_party_host_test.cpp shared/third_party_modules/third_party_host.cpp)
better_endfield_target_defaults(BetterEndfield.ThirdPartyHostTests)
target_link_libraries(BetterEndfield.ThirdPartyHostTests PRIVATE ws2_32)
add_library(BetterEndfield.ThirdPartyEcho SHARED EXCLUDE_FROM_ALL ../tools/ThirdPartyModules/echo/native/echo.cpp)
better_endfield_target_defaults(BetterEndfield.ThirdPartyEcho)
set_target_properties(BetterEndfield.ThirdPartyEcho PROPERTIES OUTPUT_NAME "example.echo")
set_target_properties(BetterEndfield.Host PROPERTIES
    OUTPUT_NAME "BetterEndfield.Host"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/runtime"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/runtime"
)

add_library(BetterEndfield.Model SHARED modules/model/module.cpp)
better_endfield_target_defaults(BetterEndfield.Model)
set_target_properties(BetterEndfield.Model PROPERTIES
    OUTPUT_NAME "BetterEndfield.Model"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_subdirectory(modules/custom_model)

add_library(BetterEndfield.CustomModel SHARED
    modules/custom_model/module.cpp
    modules/custom_model/native_mesh_layout.cpp
    shared/third_party/minhook/src/hde/hde64.c
)
better_endfield_target_defaults(BetterEndfield.CustomModel)
target_link_libraries(BetterEndfield.CustomModel PRIVATE BetterEndfield.CustomModelCore)
set_target_properties(BetterEndfield.CustomModel PROPERTIES
    OUTPUT_NAME "BetterEndfield.CustomModel"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.Voice SHARED modules/voice/module.cpp)
better_endfield_target_defaults(BetterEndfield.Voice)
set_target_properties(BetterEndfield.Voice PROPERTIES
    OUTPUT_NAME "BetterEndfield.Voice"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.Music SHARED
    modules/music/module.cpp
    modules/music/music_bank.h
    modules/music/omni_pcm_abi.h
    modules/music/local_track.inc
)
better_endfield_target_defaults(BetterEndfield.Music)
target_link_libraries(BetterEndfield.Music PRIVATE mfplat mfreadwrite mfuuid ole32)
set_target_properties(BetterEndfield.Music PROPERTIES
    OUTPUT_NAME "BetterEndfield.Music"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.UiModule SHARED
    modules/ui/module.cpp
    modules/ui/touch_input.cpp
    modules/ui/touch_input.h
)
better_endfield_target_defaults(BetterEndfield.UiModule)
set_target_properties(BetterEndfield.UiModule PROPERTIES
    OUTPUT_NAME "BetterEndfield.UiModule"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.Camera SHARED
    modules/camera/module.cpp
    modules/camera/free_camera_runtime.inc
    modules/camera/character_motion_runtime.inc
    modules/camera/mmd_director_runtime.inc
    modules/camera/mmd_library.h
    modules/camera/mmd_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfield.Camera)

# EIEM DirectVmd port (AGPL-3.0, see modules/camera/eiem/UPSTREAM.md). Built as
# its own object library with EIEM's compile model: narrow Win32 APIs, min/max
# macros, C++17-style u8 literals. One TU per dancing character (eiem_slot.h).
# Only eiem_body.h is visible to module.cpp.
add_library(BetterEndfield.CameraEiem OBJECT
    modules/camera/eiem/eiem_body.cpp
    modules/camera/eiem/eiem_body.h
    modules/camera/eiem/eiem_slot.h
    modules/camera/eiem/eiem_slot.inc
    modules/camera/eiem/eiem_slot0.cpp
    modules/camera/eiem/eiem_slot1.cpp
    modules/camera/eiem/eiem_slot2.cpp
    modules/camera/eiem/eiem_slot3.cpp
)
target_include_directories(BetterEndfield.CameraEiem PRIVATE
    "${CMAKE_CURRENT_SOURCE_DIR}/shared/third_party/minhook/include"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem/compat"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem/upstream"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/eiem"
    "${BETTER_ENDFIELD_INCLUDE_DIR}"
)
if(MSVC)
    target_compile_options(BetterEndfield.CameraEiem PRIVATE /W3 /utf-8 /EHsc /Zc:char8_t- /bigobj)
endif()
target_link_libraries(BetterEndfield.Camera PRIVATE BetterEndfield.CameraEiem winmm)
set_target_properties(BetterEndfield.Camera PROPERTIES
    OUTPUT_NAME "BetterEndfield.Camera"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_executable(BetterEndfield.MmdOverlay WIN32
    modules/camera/overlay/main.cpp
    modules/camera/overlay/mmd_overlay.rc
    modules/camera/mmd_library.h
    modules/camera/mmd_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfield.MmdOverlay)
target_link_libraries(BetterEndfield.MmdOverlay PRIVATE gdiplus shell32)
if(MSVC)
    target_link_options(BetterEndfield.MmdOverlay PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfield.MmdOverlay PROPERTIES
    OUTPUT_NAME "BetterEndfield.MmdOverlay"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.Actions SHARED modules/actions/module.cpp)
better_endfield_target_defaults(BetterEndfield.Actions)
set_target_properties(BetterEndfield.Actions PROPERTIES
    OUTPUT_NAME "BetterEndfield.Actions"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_executable(BetterEndfield.ActionsTests EXCLUDE_FROM_ALL modules/actions/dash_policy_tests.cpp)
better_endfield_target_defaults(BetterEndfield.ActionsTests)

add_executable(BetterEndfield.ActionsEffectsTests EXCLUDE_FROM_ALL modules/actions/effects_lifecycle_tests.cpp)
better_endfield_target_defaults(BetterEndfield.ActionsEffectsTests)

add_executable(BetterEndfield.ActionsExternalTests EXCLUDE_FROM_ALL modules/actions/external_loop_tests.cpp)
better_endfield_target_defaults(BetterEndfield.ActionsExternalTests)

add_executable(BetterEndfield.ActionsPoseTests EXCLUDE_FROM_ALL modules/actions/pose_overlay_tests.cpp)
better_endfield_target_defaults(BetterEndfield.ActionsPoseTests)

add_library(BetterEndfield.Gacha SHARED
    modules/gacha/module.cpp
)
better_endfield_target_defaults(BetterEndfield.Gacha)
target_link_libraries(BetterEndfield.Gacha PRIVATE advapi32)
set_target_properties(BetterEndfield.Gacha PROPERTIES
    OUTPUT_NAME "BetterEndfield.Gacha"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_library(BetterEndfield.CombatStats SHARED
    modules/combat_stats/module.cpp
    modules/combat_stats/combat_semantics.cpp
    modules/combat_stats/combat_semantics.h
    modules/combat_stats/rdps_math.h
    modules/combat_stats/combat_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfield.CombatStats)
target_link_libraries(BetterEndfield.CombatStats PRIVATE shell32)
set_target_properties(BetterEndfield.CombatStats PROPERTIES
    OUTPUT_NAME "BetterEndfield.CombatStats"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_executable(BetterEndfield.CombatOverlay WIN32
    modules/combat_stats/overlay/main.cpp
    modules/combat_stats/overlay/combat_overlay.rc
    modules/combat_stats/overlay/character_assets.generated.h
    modules/combat_stats/overlay/avatars.generated.rcinc
    modules/combat_stats/combat_overlay_protocol.h
)
better_endfield_target_defaults(BetterEndfield.CombatOverlay)
target_link_libraries(BetterEndfield.CombatOverlay PRIVATE gdiplus shlwapi shell32)
if(MSVC)
    target_link_options(BetterEndfield.CombatOverlay PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfield.CombatOverlay PROPERTIES
    OUTPUT_NAME "BetterEndfield.CombatOverlay"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

add_executable(BetterEndfield.CombatSemanticsTests EXCLUDE_FROM_ALL
    tests/combat_semantics_tests.cpp
    modules/combat_stats/combat_semantics.cpp
    modules/combat_stats/combat_semantics.h
    modules/combat_stats/rdps_math.h
)
better_endfield_target_defaults(BetterEndfield.CombatSemanticsTests)

option(BETTER_ENDFIELD_BUILD_LEGACY_CUSTOM_MODEL_RESEARCH
    "Expose archived CustomModel PoC lifecycle tests" OFF)
if(BETTER_ENDFIELD_BUILD_LEGACY_CUSTOM_MODEL_RESEARCH)
    add_subdirectory("${CMAKE_CURRENT_SOURCE_DIR}/../research/custom-model/legacy-runtime"
        "${CMAKE_CURRENT_BINARY_DIR}/research/custom-model-legacy")
endif()

add_executable(BetterEndfield.CustomModelBindingTests EXCLUDE_FROM_ALL
    tests/custom_model_binding_tests.cpp
    modules/custom_model/native_mesh_layout.cpp
    shared/third_party/minhook/src/hde/hde64.c
)
better_endfield_target_defaults(BetterEndfield.CustomModelBindingTests)
target_link_libraries(BetterEndfield.CustomModelBindingTests PRIVATE BetterEndfield.CustomModelCore)

better_endfield_target_defaults(BetterEndfield.BemValidate)

# Read-only research utility. It is deliberately excluded from the default
# build and release layout; build the target explicitly when runtime metadata
# signatures need to be refreshed.
add_library(BetterEndfield.MusicProbe SHARED EXCLUDE_FROM_ALL
    research/music_probe/module.cpp
)
better_endfield_target_defaults(BetterEndfield.MusicProbe)
set_target_properties(BetterEndfield.MusicProbe PROPERTIES
    OUTPUT_NAME "BetterEndfield.MusicProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
)

# Standalone research probe for the mobile-UI input path. Also excluded from
# the default build: it injects synthetic touch to answer whether the Unity
# backend surfaces WM_POINTER as a Touchscreen device, and is not shipped.
add_executable(BetterEndfield.TouchProbe EXCLUDE_FROM_ALL
    research/touch_probe/main.cpp
)
better_endfield_target_defaults(BetterEndfield.TouchProbe)
set_target_properties(BetterEndfield.TouchProbe PROPERTIES
    OUTPUT_NAME "BetterEndfield.TouchProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
)

# The probe must run at the game's integrity level or UIPI drops its injected
# input, so it is started from inside the game process rather than a shell.
add_library(BetterEndfield.TouchProbeLauncher SHARED EXCLUDE_FROM_ALL
    research/touch_probe/launcher_module.cpp
)
better_endfield_target_defaults(BetterEndfield.TouchProbeLauncher)
set_target_properties(BetterEndfield.TouchProbeLauncher PROPERTIES
    OUTPUT_NAME "BetterEndfield.TouchProbeLauncher"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
)

# Buff resource table probe. Excluded from the default build and the release
# layout: it is a local development tool that dumps every BuffData through the
# client's own loader, because the shipped BuffData files are MemoryPack blobs
# that carry no field names. The source lives under tools/ with the rest of the
# buff-table pipeline rather than here, since nothing in a release builds it.
add_library(BetterEndfield.BuffProbe SHARED EXCLUDE_FROM_ALL
    ../tools/BuffProbe/module.cpp
)
better_endfield_target_defaults(BetterEndfield.BuffProbe)
set_target_properties(BetterEndfield.BuffProbe PROPERTIES
    OUTPUT_NAME "BetterEndfield.BuffProbe"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/research"
)

add_executable(BetterEndfield.Injector
    loaders/injector/main.cpp
    loaders/injector/BetterEndfield.Injector.rc
)
better_endfield_target_defaults(BetterEndfield.Injector)
if(MSVC)
    target_link_options(BetterEndfield.Injector PRIVATE "/MANIFEST:NO")
endif()
set_target_properties(BetterEndfield.Injector PROPERTIES
    OUTPUT_NAME "BetterEndfield.Injector"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/loaders"
)

# XInput is the only game-directory proxy. It forwards the system API and
# starts the Better Endfield Host after IL2CPP is ready.
add_library(BetterEndfield.XInput14Proxy SHARED
    loaders/xinput/input_proxy.cpp
)
better_endfield_target_defaults(BetterEndfield.XInput14Proxy)
target_compile_definitions(BetterEndfield.XInput14Proxy PRIVATE
    BETTER_ENDFIELD_INPUT_PROXY_MARKER=L"BetterEndfield-xinput1_4-proxy.loaded"
    BETTER_ENDFIELD_INPUT_PROXY_STATUS=L"BetterEndfield-xinput1_4-host.status"
    BETTER_ENDFIELD_INPUT_PROXY_XINPUT14
    BETTER_ENDFIELD_INPUT_PROXY_LOAD_HOST
)
set_target_properties(BetterEndfield.XInput14Proxy PROPERTIES
    OUTPUT_NAME "xinput1_4"
    PREFIX ""
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/payloads"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/payloads"
)

add_library(BetterEndfield.Bootstrap SHARED
    loaders/xinput/input_proxy.cpp
)
better_endfield_target_defaults(BetterEndfield.Bootstrap)
target_compile_definitions(BetterEndfield.Bootstrap PRIVATE
    BETTER_ENDFIELD_INPUT_PROXY_MARKER=L"BetterEndfield-bootstrap.loaded"
    BETTER_ENDFIELD_INPUT_PROXY_STATUS=L"BetterEndfield-bootstrap-host.status"
    BETTER_ENDFIELD_INPUT_BOOTSTRAP
    BETTER_ENDFIELD_INPUT_PROXY_LOAD_HOST
)
set_target_properties(BetterEndfield.Bootstrap PROPERTIES
    OUTPUT_NAME "BetterEndfield.Bootstrap"
    PREFIX ""
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/loaders"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/loaders"
)

add_custom_target(BetterEndfield.Layout ALL
    COMMAND ${CMAKE_COMMAND} -E rm -rf "${BETTER_ENDFIELD_STAGE_DIR}/catalog"
    COMMAND ${CMAKE_COMMAND} -E rm -f
        "${BETTER_ENDFIELD_STAGE_DIR}/payloads/version.dll"
        "${BETTER_ENDFIELD_STAGE_DIR}/payloads/dinput8.dll"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/model/betterendfield.model.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.model.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/custom_model/betterendfield.custom_model.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.custom_model.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/voice/betterendfield.voice.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.voice.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/music/betterendfield.music.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.music.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/combat_stats/betterendfield.combat_stats.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.combat_stats.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/ui/betterendfield.ui.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.ui.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/camera/betterendfield.camera.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.camera.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/betterendfield.actions.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.actions.module.ini"
    COMMAND ${CMAKE_COMMAND} -E make_directory
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/actions"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/assets/pose_aglina.bin"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/actions/pose_aglina.bin"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/actions/assets/pose_liino.bin"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/actions/pose_liino.bin"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/gacha/betterendfield.gacha.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.gacha.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../manifests/combat/combat-semantics.besem"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/combat-semantics.besem"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/../manifests/combat/buff-sources.bemap"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/buff-sources.bemap"
    DEPENDS
        BetterEndfield.Host
        BetterEndfield.Model
        BetterEndfield.CustomModel
        BetterEndfield.Voice
        BetterEndfield.Music
        BetterEndfield.UiModule
        BetterEndfield.Camera
        BetterEndfield.Actions
        BetterEndfield.Gacha
        BetterEndfield.CombatStats
        BetterEndfield.CombatOverlay
        BetterEndfield.MmdOverlay
        BetterEndfield.Injector
        BetterEndfield.Bootstrap
        BetterEndfield.XInput14Proxy
)
