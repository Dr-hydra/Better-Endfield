cmake_minimum_required(VERSION 3.22.1)
project(BetterEndfieldAndroid LANGUAGES CXX)

get_filename_component(BETTER_ENDFIELD_ROOT
    "${CMAKE_CURRENT_SOURCE_DIR}/../../../../.." ABSOLUTE)
set(DOBBY_ROOT
    "${BETTER_ENDFIELD_ROOT}/tools/android-toolchain/dobby-1.0.5")
if(NOT EXISTS "${DOBBY_ROOT}/CMakeLists.txt")
    message(FATAL_ERROR "Dobby toolchain source is missing: ${DOBBY_ROOT}")
endif()

set(DOBBY_GENERATE_SHARED OFF CACHE BOOL "" FORCE)
set(DOBBY_DEBUG OFF CACHE BOOL "" FORCE)
set(DynamicBinaryInstrument OFF CACHE BOOL "" FORCE)
set(Plugin.SymbolResolver OFF CACHE BOOL "" FORCE)
set(Plugin.GlobalOffsetTableHook OFF CACHE BOOL "" FORCE)
set(Plugin.LinkerLoadCallback OFF CACHE BOOL "" FORCE)
set(Plugin.ApplicationEventMonitor OFF CACHE BOOL "" FORCE)
set(Plugin.Android.BionicLinkerRestriction OFF CACHE BOOL "" FORCE)
add_subdirectory("${DOBBY_ROOT}" "${CMAKE_CURRENT_BINARY_DIR}/dobby")

add_library(betterendfield_android SHARED
    native_bridge.cpp
    core/hook_broker.cpp
    core/log.cpp
    core/runtime.cpp
    modules/character_voice/character_voice_module.cpp
    modules/login_model/login_model_module.cpp
    "${BETTER_ENDFIELD_ROOT}/native/modules/model/module.cpp")

target_include_directories(betterendfield_android PRIVATE
    .
    "${BETTER_ENDFIELD_ROOT}/native/shared/include")

target_compile_features(betterendfield_android PRIVATE cxx_std_20)
target_compile_options(betterendfield_android PRIVATE
    -Wall
    -Wextra
    -Wpedantic
    -fvisibility=hidden)

target_link_options(betterendfield_android PRIVATE
    -Wl,-z,max-page-size=16384
    -Wl,-z,common-page-size=16384)

find_library(log_library log REQUIRED)
find_library(dl_library dl REQUIRED)

target_link_libraries(betterendfield_android PRIVATE
    c++_static
    c++abi
    dobby
    ${log_library}
    ${dl_library})
