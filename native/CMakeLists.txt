cmake_minimum_required(VERSION 3.25)

project(BetterEndfieldNative VERSION 2.0.1 LANGUAGES C CXX)

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
    shared/host/dynamic_resolver.cpp
    shared/host/hook_broker.cpp
    shared/host/host_runtime.cpp
    shared/host/logging.cpp
    shared/host/module_manager.cpp
    shared/host/settings_store.cpp
)
better_endfield_target_defaults(BetterEndfield.Host)
target_link_libraries(BetterEndfield.Host PRIVATE minhook)
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
)
better_endfield_target_defaults(BetterEndfield.Music)
set_target_properties(BetterEndfield.Music PROPERTIES
    OUTPUT_NAME "BetterEndfield.Music"
    RUNTIME_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
    LIBRARY_OUTPUT_DIRECTORY "${BETTER_ENDFIELD_STAGE_DIR}/modules"
)

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
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/voice/betterendfield.voice.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.voice.module.ini"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        "${CMAKE_CURRENT_SOURCE_DIR}/modules/music/betterendfield.music.module.ini"
        "${BETTER_ENDFIELD_STAGE_DIR}/modules/betterendfield.music.module.ini"
    DEPENDS
        BetterEndfield.Host
        BetterEndfield.Model
        BetterEndfield.Voice
        BetterEndfield.Music
        BetterEndfield.Injector
        BetterEndfield.Bootstrap
        BetterEndfield.XInput14Proxy
)
