#include "logging.h"

#include <chrono>
#include <fstream>
#include <iomanip>
#include <sstream>

namespace BetterEndfield::Host {

void Logger::Initialize(const std::filesystem::path& root) {
    std::error_code error;
    std::filesystem::create_directories(root, error);
    file_path_ = root / "BetterEndfield.log";
}

void Logger::Write(const std::string& source, const std::string& message) {
    std::lock_guard lock(mutex_);

    std::ofstream stream(file_path_, std::ios::out | std::ios::app);
    if (!stream) {
        return;
    }

    const auto now = std::chrono::system_clock::now();
    const auto local = std::chrono::system_clock::to_time_t(now);
    std::tm time_info{};
    localtime_s(&time_info, &local);
    stream << std::put_time(&time_info, "%Y-%m-%d %H:%M:%S")
           << " [" << source << "] " << message << '\n';
}

} // namespace BetterEndfield::Host
