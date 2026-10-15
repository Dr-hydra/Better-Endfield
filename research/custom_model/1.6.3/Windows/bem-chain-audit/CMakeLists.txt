cmake_minimum_required(VERSION 3.22)
project(BemCurrentPlayerAudit LANGUAGES C CXX)
get_filename_component(REPO "${CMAKE_CURRENT_SOURCE_DIR}/../../../../.." ABSOLUTE)
add_executable(bem-current-player-layout
    current_player_layout.cpp
    "${REPO}/native/modules/custom_model/native_mesh_layout.cpp"
    "${REPO}/native/shared/third_party/minhook/src/hde/hde64.c")
target_compile_features(bem-current-player-layout PRIVATE cxx_std_20)
target_compile_definitions(bem-current-player-layout PRIVATE NOMINMAX WIN32_LEAN_AND_MEAN)
target_include_directories(bem-current-player-layout PRIVATE "${REPO}/native/modules/custom_model")
