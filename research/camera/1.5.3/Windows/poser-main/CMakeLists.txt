cmake_minimum_required(VERSION 3.16)
project(endfield_poser CXX)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
option(POSER_ENABLE_LAYERED_OVERLAY "Compile the optional XXMI layered-window backend" OFF)
message(STATUS "Layered overlay: ${POSER_ENABLE_LAYERED_OVERLAY}")

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
    src/cloth/assets/cloth_asset_cache.h
    src/cloth/assets/cloth_asset_coat_calves.h
    src/cloth/assets/cloth_asset_coat_torso.h
    src/cloth/assets/cloth_asset_dense.h
    src/cloth/assets/cloth_asset_graph.h
    src/cloth/assets/cloth_asset_leg_coverage.h
    src/cloth/assets/cloth_asset_panel.h
    src/cloth/assets/cloth_asset_recipe.h
    src/cloth/assets/cloth_asset_worker.h
    src/cloth/bonecloth/cloth_bonecloth_auto.h
    src/cloth/bonecloth/cloth_bonecloth_binding.h
    src/cloth/bonecloth/cloth_bonecloth_body.h
    src/cloth/bonecloth/cloth_bonecloth_coat_policy.h
    src/cloth/bonecloth/cloth_bonecloth_coat_source.h
    src/cloth/bonecloth/cloth_bonecloth_contact.h
    src/cloth/bonecloth/cloth_bonecloth_contact_finish.h
    src/cloth/bonecloth/cloth_bonecloth_display.h
    src/cloth/bonecloth/cloth_bonecloth_elastic.h
    src/cloth/bonecloth/cloth_bonecloth_fitted_body.h
    src/cloth/bonecloth/cloth_bonecloth_input_anchor.h
    src/cloth/bonecloth/cloth_bonecloth_limits.h
    src/cloth/bonecloth/cloth_bonecloth_local.h
    src/cloth/bonecloth/cloth_bonecloth_local_state.h
    src/cloth/bonecloth/cloth_bonecloth_panel_restore.h
    src/cloth/bonecloth/cloth_bonecloth_prefetch.h
    src/cloth/bonecloth/cloth_bonecloth_profile.h
    src/cloth/bonecloth/cloth_bonecloth_recipe.h
    src/cloth/bonecloth/cloth_bonecloth_reference.h
    src/cloth/bonecloth/cloth_bonecloth_response_layer.h
    src/cloth/bonecloth/cloth_bonecloth_runtime.h
    src/cloth/bonecloth/cloth_bonecloth_support.h
    src/cloth/bonecloth/cloth_bonecloth_support_policy.h
    src/cloth/collision/cloth_contact_finish_install.h
    src/cloth/collision/cloth_contact_finish_state.h
    src/cloth/collision/cloth_display_install.h
    src/cloth/collision/cloth_display_state.h
    src/cloth/collision/cloth_layer_order_hook.h
    src/cloth/collision/cloth_layer_order_runtime.h
    src/cloth/collision/cloth_layer_order_state.h
    src/cloth/collision/cloth_response_frame.h
    src/cloth/core/cloth_actor_context.h
    src/cloth/core/cloth_anchor.h
    src/cloth/core/cloth_collision.h
    src/cloth/core/cloth_skin_guard.h
    src/cloth/core/cloth_writer.h
    src/cloth/core/cloth_writer_state.h
    src/cloth/diagnostics/cloth_body_trace.h
    src/cloth/diagnostics/cloth_bonecloth_contact_trace.h
    src/cloth/diagnostics/cloth_bonecloth_fit_trace.h
    src/cloth/diagnostics/cloth_bonecloth_trace.h
    src/cloth/diagnostics/cloth_collision_snapshot.h
    src/cloth/diagnostics/cloth_contact_trace.h
    src/cloth/diagnostics/cloth_input_trace.h
    src/cloth/diagnostics/cloth_ribbon_motion_trace.h
    src/cloth/diagnostics/cloth_shoulder_trace.h
    src/cloth/native/cloth_native_runtime.h
    src/cloth/shoulder/cloth_shoulder_drive.h
    src/core/mmd_countdown_hud.h
    src/core/web_server.h
    src/editor/panel_mmd_squad.h
    src/game/mmd_avatar.h
    src/game/mmd_camera_settings.h
    src/game/mmd_runtime_api.h
    src/game/mmd_squad.h
    src/game/mmd_terrain.h
    src/game/smc_actor_state.h
    src/game/squad.h
    src/math/cloth_playback_start.h
    src/math/cloth_preparation_budget.h
    src/math/eye_gaze_profile.h
    src/math/hotkey_state.h
    src/math/mmd_avatar.h
    src/math/mmd_calibration.h
    src/math/mmd_camera_profile.h
    src/math/mmd_countdown.h
    src/math/mmd_squad.h
    src/math/mmd_terrain.h
    src/poser.rc
    src/config.h
    src/core/user_agreement.h src/editor/panel_agreement.h
    src/math/mmd_motion.h src/math/mmd_rig.h src/math/mmd_retarget.h src/math/mmd_props.h src/math/mmd_adaptation.h src/math/mmd_amplitude.h
    src/game/mmd_player.h src/game/mmd_io.h src/game/frozen_grip.h src/editor/panel_mmd.h src/editor/panel_mmd_adaptation.h
    src/core/game_hooks.h
    src/core/gui_overlay.h src/core/build_features.h
    src/core/frame_driver.h src/math/frame_cadence.h
    src/core/runtime_bootstrap.h src/core/runtime_lifetime.h src/core/runtime_shutdown.h src/core/plugin_paths.h src/core/proxy_loader.h
    src/game/mmd_audio.h src/math/mmd_audio_sync.h
    src/game/mmd_camera.h src/math/mmd_camera.h src/game/eye_gaze.h src/math/eye_gaze.h
    src/math/cloth_state.h src/game/cloth.h src/game/cloth_init.h
    src/game/skeleton.h src/game/freeze.h src/game/accessory.h src/game/morph.h src/game/smc_morph.h src/game/smc_automation.h
    src/math/character_face.h src/math/face_geometry.h src/math/mmd_face_controls.h
    src/math/face_mixing.h
    src/game/char_state.h
    src/game/roster.h src/math/edit_selection.h
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
  if(POSER_ENABLE_LAYERED_OVERLAY)
    target_sources(poser PRIVATE src/core/gui_overlay_layered.h
      src/core/layered_readback.h src/core/overlay_device.h)
  endif()
  target_include_directories(poser PRIVATE
    deps deps/imgui deps/imguizmo deps/minhook_lib/include deps/json src ${CMAKE_CURRENT_SOURCE_DIR})
  target_compile_definitions(poser PRIVATE
    POSER_ENABLE_LAYERED_OVERLAY=$<BOOL:${POSER_ENABLE_LAYERED_OVERLAY}>
    APPLEPIE_PLUGIN_IMPL _CRT_SECURE_NO_WARNINGS WIN32_LEAN_AND_MEAN
    IMGUI_DEFINE_MATH_OPERATORS _DISABLE_CONSTEXPR_MUTEX_CONSTRUCTOR)
  # /EHa：让 __except 也能捕获 C++ 异常（IL2CPP 调用可能抛 0xe06d7363，
  # /EHsc 下这类异常会直接崩掉游戏进程）
  target_compile_options(poser PRIVATE /EHa)
  target_link_options(poser PRIVATE /NODEFAULTLIB:LIBCMT)
  target_link_libraries(poser PRIVATE
    cloth_decoder d3d11 dxgi d3dcompiler dwmapi ole32
    ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)

  # 代理 DLL：本地编译 src/core/proxy_d3dcompiler.cpp，不再依赖上游编译产物
  add_library(d3dcompiler_47 SHARED src/core/proxy_d3dcompiler.cpp)
  target_compile_definitions(d3dcompiler_47 PRIVATE _CRT_SECURE_NO_WARNINGS)
  set_target_properties(d3dcompiler_47 PROPERTIES OUTPUT_NAME "d3dcompiler_47")

  # Vulkan 代理 DLL（改写自上游，转发 System32\vulkan-1）
  add_library(vulkan-1 SHARED src/core/proxy_vulkan_full.cpp)
  target_compile_definitions(vulkan-1 PRIVATE _CRT_SECURE_NO_WARNINGS)
  set_target_properties(vulkan-1 PROPERTIES OUTPUT_NAME "vulkan-1")

  # 部署：把 poser.dll 与 d3dcompiler_47.dll 拷到 plugin/
  add_dependencies(poser d3dcompiler_47 vulkan-1 cloth_payloads)
  add_custom_command(TARGET poser POST_BUILD
    COMMAND $<TARGET_FILE:cloth_resources> --repo "${CMAKE_CURRENT_SOURCE_DIR}" --dll $<TARGET_FILE:poser>
    COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:cloth_resources>
            ${CMAKE_CURRENT_SOURCE_DIR}/build/cloth_resources.exe
    VERBATIM)
  add_custom_command(TARGET poser POST_BUILD
    COMMAND powershell -NoProfile -ExecutionPolicy Bypass -File
            ${CMAKE_CURRENT_SOURCE_DIR}/tools/copy_character_faces.ps1
            -Destination ${CMAKE_CURRENT_SOURCE_DIR}/plugin/mmd/character-faces
    VERBATIM)
  add_custom_command(TARGET poser POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E make_directory ${CMAKE_CURRENT_SOURCE_DIR}/plugin
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:poser>
            ${CMAKE_CURRENT_SOURCE_DIR}/plugin/poser.dll
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:d3dcompiler_47>
            ${CMAKE_CURRENT_SOURCE_DIR}/plugin/d3dcompiler_47.dll
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
            $<TARGET_FILE:vulkan-1>
            ${CMAKE_CURRENT_SOURCE_DIR}/plugin/vulkan-1.dll)
endif()

# Existing public math tests remain available in a clean checkout. Additional
# local regression fixtures are optional and are not distributed.
option(POSER_BUILD_TESTS "Build available regression tests" ON)
if(POSER_BUILD_TESTS)
  enable_testing()
  file(GLOB POSER_TEST_SOURCES CONFIGURE_DEPENDS tests/test_*.cpp)
  foreach(test_source IN LISTS POSER_TEST_SOURCES)
    get_filename_component(test_name "${test_source}" NAME_WE)
    if(NOT WIN32 AND NOT test_name MATCHES "^test_(quat|ik|pose_file|mmd|mmd_camera|mmd_transport|character_faces|face_mixing|eye_gaze|edit_selection)$")
      continue()
    endif()
    add_executable(${test_name} "${test_source}")
    target_include_directories(${test_name} PRIVATE src deps/json deps/imgui deps/minhook_lib/include)
    if(MSVC)
      target_compile_options(${test_name} PRIVATE /EHa)
      target_link_libraries(${test_name} PRIVATE cloth_decoder d3d11
        ${CMAKE_CURRENT_SOURCE_DIR}/deps/minhook_lib/lib/libMinHook.x64.lib)
    endif()
    add_test(NAME ${test_name} COMMAND ${test_name})
    set_tests_properties(${test_name} PROPERTIES WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}")
  endforeach()
endif()
