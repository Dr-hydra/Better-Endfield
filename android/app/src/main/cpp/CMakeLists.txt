cmake_minimum_required(VERSION 3.22.1)
project(BetterEndfieldAndroid LANGUAGES C CXX)

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
add_subdirectory("${BETTER_ENDFIELD_ROOT}/native/modules/custom_model"
    "${CMAKE_CURRENT_BINARY_DIR}/custom_model")

# Import-time codec only. This library never starts the injected game runtime.
set(ASTCENC_CLI OFF CACHE BOOL "" FORCE)
set(ASTCENC_ISA_NEON ON CACHE BOOL "" FORCE)
set(ASTCENC_ISA_NATIVE OFF CACHE BOOL "" FORCE)
set(ASTCENC_UNITTEST OFF CACHE BOOL "" FORCE)
add_subdirectory("${BETTER_ENDFIELD_ROOT}/native/shared/third_party/astcenc"
    "${CMAKE_CURRENT_BINARY_DIR}/astcenc")
set_target_properties(astcenc-neon-static PROPERTIES POSITION_INDEPENDENT_CODE ON)
# ASTC's search loops need optimization even in a debuggable APK. Keep symbols.
target_compile_options(astcenc-neon-static PRIVATE -O3)
add_library(betterendfield_installer SHARED installer/install_jni.cpp installer/texture_install.cpp)
target_compile_options(betterendfield_installer PRIVATE -O2)
target_compile_features(betterendfield_installer PRIVATE cxx_std_20)
target_include_directories(betterendfield_installer PRIVATE "${BETTER_ENDFIELD_ROOT}/native/shared/third_party/bcdec")
target_link_libraries(betterendfield_installer PRIVATE BetterEndfield.CustomModelCore astcenc-neon-static c++_static c++abi)
target_link_options(betterendfield_installer PRIVATE -Wl,-z,max-page-size=16384 -Wl,-z,common-page-size=16384)

# The UI, camera and sustained-dash features are the desktop translation units,
# compiled for ARM64 without a second implementation. Everything platform
# specific is in native/shared/android_compat: a <Windows.h> stand-in that the
# desktop sources reach through their existing include, and an Android
# implementation of the desktop mouse-to-touch helper. The include directory that
# provides <Windows.h> is deliberately scoped to this target so the Android
# runtime sources can never pick it up.
add_library(betterendfield_desktop_features STATIC
    "${BETTER_ENDFIELD_ROOT}/native/shared/android_compat/android_win32.cpp"
    "${BETTER_ENDFIELD_ROOT}/native/shared/android_compat/touch_input_android.cpp"
    "${BETTER_ENDFIELD_ROOT}/native/modules/ui/module.cpp"
    "${BETTER_ENDFIELD_ROOT}/native/modules/camera/module.cpp"
    "${BETTER_ENDFIELD_ROOT}/native/modules/actions/module.cpp")

# Each desktop module exports the same BetterEndfield_GetModuleApiV1 entry point
# because on Windows each one is its own DLL. Renaming per translation unit keeps
# all three in one shared library, the way the custom-model source already is.
set_source_files_properties(
    "${BETTER_ENDFIELD_ROOT}/native/modules/ui/module.cpp"
    PROPERTIES COMPILE_DEFINITIONS
    "BetterEndfield_GetModuleApiV1=BetterEndfield_GetUiModuleApiV1")
set_source_files_properties(
    "${BETTER_ENDFIELD_ROOT}/native/modules/camera/module.cpp"
    PROPERTIES COMPILE_DEFINITIONS
    "BetterEndfield_GetModuleApiV1=BetterEndfield_GetCameraModuleApiV1")
set_source_files_properties(
    "${BETTER_ENDFIELD_ROOT}/native/modules/actions/module.cpp"
    PROPERTIES COMPILE_DEFINITIONS
    "BetterEndfield_GetModuleApiV1=BetterEndfield_GetActionsModuleApiV1")

target_include_directories(betterendfield_desktop_features PRIVATE
    "${BETTER_ENDFIELD_ROOT}/native/shared/android_compat/include"
    "${BETTER_ENDFIELD_ROOT}/native/shared/android_compat"
    "${BETTER_ENDFIELD_ROOT}/native/shared/include"
    "${BETTER_ENDFIELD_ROOT}/native/modules/ui")
target_compile_features(betterendfield_desktop_features PRIVATE cxx_std_20)
# Shared desktop sources are not compiled with -Wpedantic on Windows either;
# holding them to it here would only produce noise this port cannot act on.
target_compile_options(betterendfield_desktop_features PRIVATE -Wall -O2)
set_target_properties(betterendfield_desktop_features
    PROPERTIES POSITION_INDEPENDENT_CODE ON)

add_library(betterendfield_android SHARED
    native_bridge.cpp
    core/hook_broker.cpp
    core/log.cpp
    core/runtime.cpp
    core/command_pump.cpp
    modules/custom_model/resource_probe.cpp
    modules/custom_model/mesh_layout_probe.cpp
    modules/custom_model/mesh_skin_metadata_adapter.cpp
    modules/custom_model/custom_model_module.cpp
    modules/custom_model/android_mesh_builder.cpp
    "${BETTER_ENDFIELD_ROOT}/native/modules/custom_model/module.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/modules/custom_model/native_mesh_layout_android.cpp"
    modules/character_voice/character_voice_module.cpp
    modules/desktop/desktop_module.cpp
    modules/login_model/login_model_module.cpp
    "${BETTER_ENDFIELD_ROOT}/native/modules/model/module.cpp")

# The Android runtime currently uses its own Module interface. Compile the
# shared PC custom-model translation unit as a private implementation target so
# its exported desktop module symbol cannot collide with the login-model module.
set_source_files_properties(
    "${BETTER_ENDFIELD_ROOT}/native/modules/custom_model/module.cpp"
    PROPERTIES COMPILE_DEFINITIONS
    "BetterEndfield_GetModuleApiV1=BetterEndfield_GetCustomModelModuleApiV1")

target_include_directories(betterendfield_android PRIVATE
    .
    "${BETTER_ENDFIELD_ROOT}/native/shared/android_compat"
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
    BetterEndfield.CustomModelCore
    betterendfield_desktop_features
    ${log_library}
    ${dl_library})
