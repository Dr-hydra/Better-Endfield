cmake_minimum_required(VERSION 3.22.1)
project(BetterEndfieldAndroid LANGUAGES CXX)

add_library(betterendfield_android SHARED native_bridge.cpp)

target_compile_features(betterendfield_android PRIVATE cxx_std_20)
target_compile_options(betterendfield_android PRIVATE
    -Wall
    -Wextra
    -Wpedantic
    -fvisibility=hidden)

find_library(log_library log REQUIRED)
find_library(dl_library dl REQUIRED)

target_link_libraries(betterendfield_android PRIVATE
    ${log_library}
    ${dl_library})
