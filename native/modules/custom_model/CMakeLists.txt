# Platform-neutral package decoding and resource selection, shared by PC/Android.
# Engine calls, process memory access and native layout discovery stay in adapters.
file(GLOB BEM_ZSTD_COMMON CONFIGURE_DEPENDS ../../shared/third_party/zstd/lib/common/*.c)
file(GLOB BEM_ZSTD_DECODE CONFIGURE_DEPENDS ../../shared/third_party/zstd/lib/decompress/*.c)
file(GLOB BEM_ZSTD_COMPRESS CONFIGURE_DEPENDS ../../shared/third_party/zstd/lib/compress/*.c)
add_library(BetterEndfieldNext.BemZstd STATIC ${BEM_ZSTD_COMMON} ${BEM_ZSTD_DECODE} ${BEM_ZSTD_COMPRESS})
target_compile_definitions(BetterEndfieldNext.BemZstd PRIVATE ZSTD_DISABLE_ASM)
set_target_properties(BetterEndfieldNext.BemZstd PROPERTIES POSITION_INDEPENDENT_CODE ON)

# ZIP container support (format specification section 14). miniz is vendored as the
# amalgamated upstream single-file pair; the directory also holds three unresolvable
# corrupt leftovers from a filesystem fault, so this is an explicit source list.
add_library(BetterEndfieldNext.BemMiniz STATIC ../../shared/third_party/miniz/miniz.c)
target_include_directories(BetterEndfieldNext.BemMiniz PUBLIC "${CMAKE_CURRENT_SOURCE_DIR}/../../shared/third_party/miniz")
if(NOT MSVC)
    target_compile_options(BetterEndfieldNext.BemMiniz PRIVATE -w)
endif()
set_target_properties(BetterEndfieldNext.BemMiniz PROPERTIES POSITION_INDEPENDENT_CODE ON)

add_library(BetterEndfieldNext.CustomModelCore STATIC bem.cpp mod_registry.cpp)
target_compile_features(BetterEndfieldNext.CustomModelCore PUBLIC cxx_std_20)
target_include_directories(BetterEndfieldNext.CustomModelCore PUBLIC "${CMAKE_CURRENT_SOURCE_DIR}")
target_link_libraries(BetterEndfieldNext.CustomModelCore PUBLIC BetterEndfieldNext.BemZstd BetterEndfieldNext.BemMiniz)
set_target_properties(BetterEndfieldNext.CustomModelCore PROPERTIES POSITION_INDEPENDENT_CODE ON)

add_executable(BetterEndfieldNext.BemValidate EXCLUDE_FROM_ALL ../../tests/bem_v1_validate.cpp)
target_link_libraries(BetterEndfieldNext.BemValidate PRIVATE BetterEndfieldNext.CustomModelCore)
add_executable(BetterEndfieldNext.BemV11CapacityTests EXCLUDE_FROM_ALL ../../tests/bem_v11_capacity_tests.cpp)
add_executable(BetterEndfieldNext.BemV13MorphTests EXCLUDE_FROM_ALL ../../tests/bem_v13_morph_tests.cpp)
add_executable(BetterEndfieldNext.BemV14ResourceTests EXCLUDE_FROM_ALL ../../tests/bem_v14_resource_tests.cpp)
target_link_libraries(BetterEndfieldNext.BemV14ResourceTests PRIVATE BetterEndfieldNext.CustomModelCore)
target_link_libraries(BetterEndfieldNext.BemV13MorphTests PRIVATE BetterEndfieldNext.CustomModelCore)
add_executable(BetterEndfieldNext.BemKeepTextureValidate EXCLUDE_FROM_ALL ../../tests/bem_keep_texture_validate.cpp)
target_link_libraries(BetterEndfieldNext.BemKeepTextureValidate PRIVATE BetterEndfieldNext.CustomModelCore)
target_link_libraries(BetterEndfieldNext.BemV11CapacityTests PRIVATE BetterEndfieldNext.CustomModelCore)
if(ANDROID)
    target_link_libraries(BetterEndfieldNext.BemValidate PRIVATE c++_static c++abi)
    target_link_libraries(BetterEndfieldNext.BemV11CapacityTests PRIVATE c++_static c++abi)
    target_link_libraries(BetterEndfieldNext.BemV13MorphTests PRIVATE c++_static c++abi)
endif()
