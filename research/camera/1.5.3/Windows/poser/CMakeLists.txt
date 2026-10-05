cmake_minimum_required(VERSION 3.16)
project(endfield_poser CXX)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
option(POSER_ENABLE_XXMI_BRIDGE "Build the optional XXMI mod bridge (excluded from release packages)" OFF)

# MSVC：源码含 UTF-8 中文注释/字符串，缺 /utf-8 时 GBK 误读会把
# 行尾字节解析成反斜杠续行符，导致下一行被吞（C2046 等假错误）。
if(MSVC)
  add_compile_options(/utf-8 /bigobj)
  # The game ships MSVCP140 14.29; newer constexpr mutex initialization is
  # incompatible with that private runtime. Keep the legacy initialization ABI.
  add_compile_definitions(_DISABLE_CONSTEXPR_MUTEX_CONSTRUCTOR)
endif()

# 输出目录统一到 plugin/（与 build.bat 的部署布局一致）
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# ---- 插件 DLL + 代理 DLL（仅 Windows/MSVC；沙箱 Linux 跳过）----
if(WIN32)
  enable_language(C)
  file(GLOB CLOTH_DECODER_SOURCES CONFIGURE_DEPENDS
    deps/brotli/c/common/*.c deps/brotli/c/dec/*.c)
  add_library(cloth_decoder STATIC ${CLOTH_DECODER_SOURCES})
  target_include_directories(cloth_decoder PUBLIC deps/brotli/c/include)
  target_compile_definitions(cloth_decoder PUBLIC BROTLI_STATIC)
  add_executable(cloth_resources src/build/cloth_resources.cpp)
  target_compile_definitions(cloth_resources PRIVATE _CRT_SECURE_NO_WARNINGS WIN32_LEAN_AND_MEAN)
  file(GLOB_RECURSE CLOTH_RESOURCE_INPUTS CONFIGURE_DEPENDS
    assets/cloth/*.bin assets/cloth/*.bundle src/cloth/generated/*.h
    src/cloth/resources/*.h src/build/cloth_resources.h)
  set(CLOTH_RESOURCE_OUTPUTS)
  foreach(id RANGE 4303 4314)
    list(APPEND CLOTH_RESOURCE_OUTPUTS "${CMAKE_CURRENT_SOURCE_DIR}/build/cloth/resources/${id}.bin")
  endforeach()
  add_custom_command(OUTPUT ${CLOTH_RESOURCE_OUTPUTS}
    COMMAND $<TARGET_FILE:cloth_resources> --repo "${CMAKE_CURRENT_SOURCE_DIR}" --pack
    DEPENDS cloth_resources ${CLOTH_RESOURCE_INPUTS} src/poser.rc
    WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}" VERBATIM)
  add_custom_target(cloth_payloads DEPENDS ${CLOTH_RESOURCE_OUTPUTS})
  set_source_files_properties(src/poser.rc PROPERTIES OBJECT_DEPENDS "${CLOTH_RESOURCE_OUTPUTS}")

  # 主插件 DLL
  add_library(poser SHARED
    src/poser.cpp
    src/poser.rc
    src/config.h
    src/core/user_agreement.h src/editor/panel_agreement.h
    src/math/mmd_motion.h src/math/mmd_rig.h src/math/mmd_retarget.h src/math/mmd_props.h src/math/mmd_adaptation.h src/math/mmd_amplitude.h
    src/game/mmd_squad.h src/game/squad.h src/game/smc_actor_state.h src/math/mmd_squad.h src/editor/panel_mmd_squad.h
    src/game/mmd_player.h src/game/mmd_io.h src/game/frozen_grip.h src/editor/panel_mmd.h src/editor/panel_mmd_adaptation.h
    src/core/build_features.h
    src/core/game_hooks.h
    src/core/gui_overlay.h src/core/layered_readback.h src/core/overlay_device.h
    src/core/frame_driver.h src/math/frame_cadence.h src/math/hotkey_state.h
    src/core/runtime_bootstrap.h src/core/runtime_lifetime.h src/core/runtime_shutdown.h src/core/plugin_paths.h src/core/proxy_loader.h
    src/game/mmd_audio.h src/math/mmd_audio_sync.h
    src/game/mmd_camera.h src/math/mmd_camera.h src/game/eye_gaze.h src/math/eye_gaze.h src/math/eye_gaze_profile.h
    src/game/mmd_camera_settings.h src/math/mmd_camera_profile.h src/math/mmd_calibration.h
    src/game/mmd_runtime_api.h src/game/mmd_avatar.h src/math/mmd_avatar.h
    src/game/mmd_terrain.h src/math/mmd_terrain.h
    src/math/cloth_state.h src/game/cloth.h src/game/cloth_anchor.h src/cloth/core/cloth_actor_context.h
    src/math/cloth_playback_start.h src/cloth/bonecloth/cloth_bonecloth_limits.h
    src/cloth/bonecloth/cloth_bonecloth_prefetch.h src/cloth/bonecloth/cloth_bonecloth_display.h
    src/cloth/bonecloth/cloth_bonecloth_contact_finish.h src/cloth/collision/cloth_display_state.h
    src/cloth/collision/cloth_display_install.h src/cloth/collision/cloth_contact_finish_state.h
    src/cloth/collision/cloth_contact_finish_install.h src/cloth/diagnostics/cloth_ribbon_motion_trace.h
    src/cloth/assets/cloth_asset_coat_calves.h
    src/cloth/assets/cloth_asset_coat_torso.h
    src/cloth/assets/cloth_asset_coat_waist_panels.h
    src/game/skeleton.h src/game/freeze.h src/game/accessory.h src/game/morph.h src/game/smc_morph.h src/game/smc_automation.h
    src/math/character_face.h src/math/face_geometry.h src/math/mmd_face_controls.h
    src/math/face_mixing.h
    src/game/char_state.h
    src/editor/selection.h src/editor/panel_library.h src/editor/panel_morph.h
    src/editor/panel_bones.h
    src/editor/ik_control.h
    deps/imgui/imgui.cpp
    deps/imgui/imgui_draw.cpp
    deps/imgui/imgui_tables.cpp
    deps/imgui/imgui_widgets.cpp
    deps/imgui/imgui_impl_dx11.cpp
    deps/imgui/imgui_impl_win32.cpp
    deps/imguizmo/ImGuizmo.cpp
  )
  if(POSER_ENABLE_XXMI_BRIDGE)
    target_sources(poser PRIVATE src/math/mod_bridge.h src/game/mod_bridge.h src/editor/panel_mod_bridge.h)
    set(POSER_PLUGIN_OUTPUT "${CMAKE_CURRENT_SOURCE_DIR}/build/xxmi/plugin")
  else()
    set(POSER_PLUGIN_OUTPUT "${CMAKE_CURRENT_SOURCE_DIR}/plugin")
  endif()
  target_compile_definitions(poser PRIVATE POSER_ENABLE_XXMI_BRIDGE=$<BOOL:${POSER_ENABLE_XXMI_BRIDGE}>)
  target_include_directories(poser PRIVATE
    deps deps/imgui deps/imguizmo deps/minhook_lib/include deps/json src ${CMAKE_CURRENT_SOURCE_DIR})
  target_compile_definitions(poser PRIVATE
    APPLEPIE_PLUGIN_IMPL _CRT_SECURE_NO_WARNINGS WIN32_LEAN_AND_MEAN
    IMGUI_DEFINE_MATH_OPERATORS)
  # /EHa：让 __except 也能捕获 C++ 异常（IL2CPP 调用可能抛 0xe06d7363，
  # /EHsc 下这类异常会直接崩掉游戏进程）
  target_compile_options(poser PRIVATE /EHa)
  target_link_options(poser PRIVATE /NODEFAULTLIB:LIBCMT)
  target_link_libraries(poser PRIVATE
    cloth_decoder d3d11 dxgi d3dcompiler dwmapi ole32
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)

  # 代理 DLL：本地编译 src/core/proxy_d3dcompiler.cpp，不再依赖 EIEM 编译产物
  add_library(d3dcompiler_47 SHARED src/core/proxy_d3dcompiler.cpp)
  target_compile_definitions(d3dcompiler_47 PRIVATE _CRT_SECURE_NO_WARNINGS)
  set_target_properties(d3dcompiler_47 PROPERTIES OUTPUT_NAME "d3dcompiler_47")

  # Vulkan 代理 DLL（摘自 EIEM，转发 System32\vulkan-1）
  add_library(vulkan-1 SHARED src/core/proxy_vulkan_full.cpp)
  target_compile_definitions(vulkan-1 PRIVATE _CRT_SECURE_NO_WARNINGS)
  set_target_properties(vulkan-1 PROPERTIES OUTPUT_NAME "vulkan-1")

  add_dependencies(poser d3dcompiler_47 vulkan-1 cloth_payloads)
  add_custom_command(TARGET poser POST_BUILD
    COMMAND $<TARGET_FILE:cloth_resources> --repo "${CMAKE_CURRENT_SOURCE_DIR}" --dll $<TARGET_FILE:poser>
    VERBATIM)

  # 部署：把 poser.dll 与 d3dcompiler_47.dll 拷到 plugin/
  add_custom_command(TARGET poser POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E make_directory "${POSER_PLUGIN_OUTPUT}"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:poser>
            "${POSER_PLUGIN_OUTPUT}/poser.dll"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:d3dcompiler_47>
            "${POSER_PLUGIN_OUTPUT}/d3dcompiler_47.dll"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:vulkan-1>
            "${POSER_PLUGIN_OUTPUT}/vulkan-1.dll")
endif()

# Private local tests are optional; public checkouts build without them.
option(POSER_BUILD_TESTS "Build private local tests" OFF)
if(POSER_BUILD_TESTS)
enable_testing()

# ---- 数学单测（跨平台，Linux g++ 也可编；每个测试独立可执行）----
function(add_poser_test name src)
  if(NOT EXISTS "${CMAKE_CURRENT_SOURCE_DIR}/${src}")
    message(FATAL_ERROR "POSER_BUILD_TESTS requires local test sources: ${src}")
  endif()
  add_executable(${name} ${src} src/math/quat_math.h src/math/ik_two_bone.h src/math/pose_file.h)
  target_include_directories(${name} PRIVATE src deps/json)
  if(WIN32)
    target_link_libraries(${name} PRIVATE cloth_decoder)
  endif()
  add_test(NAME ${name} COMMAND ${name})
endfunction()

add_poser_test(test_quat tests/test_quat.cpp)
add_poser_test(test_ik tests/test_ik.cpp)
add_poser_test(test_native_cloth tests/test_native_cloth.cpp)
add_poser_test(test_pose_file tests/test_pose_file.cpp)
add_poser_test(test_mmd tests/test_mmd.cpp)
add_poser_test(test_mmd_calibration tests/test_mmd_calibration.cpp)
add_poser_test(test_mmd_avatar tests/test_mmd_avatar.cpp)
add_poser_test(test_mmd_terrain tests/test_mmd_terrain.cpp)
add_poser_test(test_mmd_camera tests/test_mmd_camera.cpp)
add_poser_test(test_mmd_transport tests/test_mmd_transport.cpp)
add_poser_test(test_mod_bridge tests/test_mod_bridge.cpp)
add_poser_test(test_character_faces tests/test_character_faces.cpp)
add_poser_test(test_face_mixing tests/test_face_mixing.cpp)
add_poser_test(test_eye_gaze tests/test_eye_gaze.cpp)
set_tests_properties(test_character_faces PROPERTIES WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}")
if(MSVC)
  add_poser_test(test_user_agreement tests/test_user_agreement.cpp)
  add_poser_test(test_mmd_large tests/test_mmd_large.cpp)
  add_poser_test(test_smc_ownership tests/test_smc_ownership.cpp)
  add_poser_test(test_agreement_ui tests/test_agreement_ui.cpp)
  target_include_directories(test_agreement_ui PRIVATE deps/imgui)
  add_poser_test(test_mmd_camera_runtime tests/test_mmd_camera_runtime.cpp)
  add_poser_test(test_mmd_camera_settings tests/test_mmd_camera_settings.cpp)
  target_include_directories(test_mmd_camera_runtime PRIVATE deps/minhook_lib/include)
  target_compile_options(test_mmd_camera_runtime PRIVATE /EHa)
  target_link_libraries(test_mmd_camera_runtime PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_runtime_shutdown tests/test_runtime_shutdown.cpp)
  target_include_directories(test_runtime_shutdown PRIVATE deps/minhook_lib/include)
  target_compile_options(test_runtime_shutdown PRIVATE /EHa)
  target_link_libraries(test_runtime_shutdown PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_runtime_bootstrap tests/test_runtime_bootstrap.cpp)
  target_include_directories(test_runtime_bootstrap PRIVATE deps/minhook_lib/include)
  target_compile_options(test_runtime_bootstrap PRIVATE /EHa)
  target_link_libraries(test_runtime_bootstrap PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_plugin_paths tests/test_plugin_paths.cpp)
  add_poser_test(test_mod_bridge_runtime tests/test_mod_bridge_runtime.cpp)
  target_compile_definitions(test_mod_bridge_runtime PRIVATE POSER_ENABLE_XXMI_BRIDGE=1)
  add_poser_test(test_layered_readback tests/test_layered_readback.cpp)
  target_link_libraries(test_layered_readback PRIVATE d3d11)
  add_poser_test(test_overlay_device tests/test_overlay_device.cpp)
  target_link_libraries(test_overlay_device PRIVATE d3d11)
  add_poser_test(test_mmd_audio tests/test_mmd_audio.cpp)
  target_compile_options(test_mmd_audio PRIVATE /EHa)
  add_poser_test(test_cloth_runtime tests/test_cloth_runtime.cpp)
  add_poser_test(test_cloth_enhancement tests/test_cloth_enhancement.cpp)
  target_include_directories(test_cloth_enhancement PRIVATE deps/minhook_lib/include)
  target_compile_options(test_cloth_enhancement PRIVATE /EHa)
  target_link_libraries(test_cloth_enhancement PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  set_tests_properties(test_cloth_enhancement PROPERTIES WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}")
  target_include_directories(test_cloth_runtime PRIVATE deps/minhook_lib/include)
  target_compile_options(test_cloth_runtime PRIVATE /EHa)
  target_link_libraries(test_cloth_runtime PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_frame_driver tests/test_frame_driver.cpp)
  target_include_directories(test_frame_driver PRIVATE deps/minhook_lib/include)
  target_compile_options(test_frame_driver PRIVATE /EHa)
  target_link_libraries(test_frame_driver PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_mmd_runtime tests/test_mmd_runtime.cpp)
  add_poser_test(test_mmd_environment_runtime tests/test_mmd_environment_runtime.cpp)
  target_include_directories(test_mmd_environment_runtime PRIVATE deps/minhook_lib/include)
  target_compile_options(test_mmd_environment_runtime PRIVATE /EHa)
  target_link_libraries(test_mmd_environment_runtime PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  target_compile_options(test_mmd_runtime PRIVATE /EHa)
  add_poser_test(test_character_capture tests/test_character_capture.cpp)
  add_poser_test(test_component_query tests/test_component_query.cpp)
  target_include_directories(test_component_query PRIVATE deps/minhook_lib/include)
  target_compile_options(test_component_query PRIVATE /EHa)
  target_link_libraries(test_component_query PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  target_include_directories(test_character_capture PRIVATE deps/minhook_lib/include)
  target_compile_options(test_character_capture PRIVATE /EHa)
  target_link_libraries(test_character_capture PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_smc_switch tests/test_smc_switch.cpp)
  target_include_directories(test_smc_switch PRIVATE deps/minhook_lib/include)
  target_compile_options(test_smc_switch PRIVATE /EHa)
  target_link_libraries(test_smc_switch PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_smc_abi tests/test_smc_abi.cpp)
  add_poser_test(test_smc_automation tests/test_smc_automation.cpp)
  target_include_directories(test_smc_automation PRIVATE deps/minhook_lib/include)
  target_compile_options(test_smc_automation PRIVATE /EHa)
  target_link_libraries(test_smc_automation PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_face_runtime tests/test_face_runtime.cpp)
  add_poser_test(test_eye_gaze_runtime tests/test_eye_gaze_runtime.cpp)
  target_include_directories(test_eye_gaze_runtime PRIVATE deps/minhook_lib/include)
  target_compile_options(test_eye_gaze_runtime PRIVATE /EHa)
  target_link_libraries(test_eye_gaze_runtime PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  add_poser_test(test_mmd_face_controls tests/test_mmd_face_controls.cpp)
  target_include_directories(test_face_runtime PRIVATE deps/minhook_lib/include)
  target_compile_options(test_face_runtime PRIVATE /EHa)
  target_link_libraries(test_face_runtime PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
  target_include_directories(test_smc_abi PRIVATE deps/minhook_lib/include)
  target_compile_options(test_smc_abi PRIVATE /EHa)
  target_link_libraries(test_smc_abi PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
endif()

endif()
