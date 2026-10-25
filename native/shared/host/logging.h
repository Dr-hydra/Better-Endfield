#pragma once

#include <filesystem>
#include <fstream>
#include <mutex>
#include <string>

namespace BetterEndfieldNext::Host {

// The log handle is opened once and kept for the process lifetime. Re-opening
// the file per line costs milliseconds as soon as a real-time scanner watches
// the write (a ~120 KB log measured 13 ms per line on Windows with the default
// Defender configuration), and the cost grows with the file. A held handle plus
// a flush per line keeps the tail readable after a crash for ~80 us instead.
// Measurement and method: worktree/audit/bem15-performance-2026-10-25.md
// section 11.
class Logger {
public:
    void Initialize(const std::filesystem::path& root);
    void Write(const std::string& source, const std::string& message);

private:
    std::filesystem::path file_path_;
    std::ofstream stream_;
    std::mutex mutex_;
};

} // namespace BetterEndfieldNext::Host
