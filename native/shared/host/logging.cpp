#include "logging.h"

#include <chrono>
#include <iomanip>
#include <sstream>

namespace BetterEndfieldNext::Host {

void Logger::Initialize(const std::filesystem::path& root) {
    std::error_code error;
    std::filesystem::create_directories(root, error);
    file_path_ = root / "BetterEndfieldNext.log";
    std::lock_guard lock(mutex_);
    stream_.open(file_path_, std::ios::out | std::ios::app);
}

void Logger::Write(const std::string& source, const std::string& message) {
    std::lock_guard lock(mutex_);

    // Initialize may have failed (another process holding the file, a read-only
    // volume). Retry there rather than on every line, so a permanently broken
    // path costs one failed open per write instead of a scan per write.
    if (!stream_.is_open()) {
        stream_.clear();
        stream_.open(file_path_, std::ios::out | std::ios::app);
        if (!stream_.is_open()) {
            return;
        }
    }

    const auto now = std::chrono::system_clock::now();
    const auto local = std::chrono::system_clock::to_time_t(now);
    std::tm time_info{};
    localtime_s(&time_info, &local);
    stream_ << std::put_time(&time_info, "%Y-%m-%d %H:%M:%S")
            << " [" << source << "] " << message << '\n';
    // Flush per line: the log is a crash-diagnosis artefact, so the tail has to
    // survive a crash. With the handle already open this is a plain write.
    stream_.flush();
}

} // namespace BetterEndfieldNext::Host
