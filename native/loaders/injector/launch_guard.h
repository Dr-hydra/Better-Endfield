#pragma once
#include <filesystem>

namespace better_endfield {
inline bool HasLocalXInputProxy(const std::filesystem::path& game_path) {
    std::error_code error;
    const bool exists = std::filesystem::exists(game_path.parent_path() / L"xinput1_4.dll", error);
    return exists || static_cast<bool>(error);
}
}
