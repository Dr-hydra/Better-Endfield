#pragma once

#include <filesystem>
#include <mutex>
#include <string>

namespace BetterEndfield::Host {

class Logger {
public:
    void Initialize(const std::filesystem::path& root);
    void Write(const std::string& source, const std::string& message);

private:
    std::filesystem::path file_path_;
    std::mutex mutex_;
};

} // namespace BetterEndfield::Host
