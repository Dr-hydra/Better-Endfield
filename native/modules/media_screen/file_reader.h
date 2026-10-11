#pragma once
#include "image_header.h"
#include <algorithm>
#include <cstdint>
#include <condition_variable>
#include <filesystem>
#include <fstream>
#include <mutex>
#include <optional>
#include <thread>
#include <vector>
#include <string>

namespace BetterEndfieldNext::MediaScreen {
inline std::filesystem::path Utf8Path(const std::string& text) {
    return std::filesystem::path(std::u8string(reinterpret_cast<const char8_t*>(text.data()), text.size()));
}
// Disk I/O never runs in the Unity frame callback. A superseded read is discarded.
class FileReader {
public:
    struct Result { uint64_t generation; std::vector<unsigned char> bytes; std::string error; };
    void Start() {
        { std::lock_guard lock(mutex_); stopping_ = false; pending_ = false; result_.reset(); }
        thread_ = std::thread([this] { Run(); });
    }
    uint64_t Read(const std::string& path) {
        std::lock_guard lock(mutex_); ++generation_; path_ = path; pending_ = true; result_.reset();
        cv_.notify_one(); return generation_;
    }
    void Cancel() { std::lock_guard lock(mutex_); ++generation_; pending_ = false; result_.reset(); }
    std::optional<Result> Take() { std::lock_guard lock(mutex_); auto out = std::move(result_); result_.reset(); return out; }
    void Stop() {
        { std::lock_guard lock(mutex_); stopping_ = true; ++generation_; cv_.notify_one(); }
        if (thread_.joinable()) thread_.join();
    }
    ~FileReader() { Stop(); }
private:
    void Run() {
        for (;;) {
            std::string path; uint64_t generation;
            { std::unique_lock lock(mutex_); cv_.wait(lock, [&] { return stopping_ || pending_; });
              if (stopping_) return;
              path = path_; generation = generation_; pending_ = false; }
            Result out{generation, {}, {}};
            try {
                std::ifstream stream(Utf8Path(path), std::ios::binary | std::ios::ate);
                const auto size = stream ? stream.tellg() : std::streampos(-1);
                if (size <= 0 || size > 32 * 1024 * 1024) out.error = "Image missing, empty or larger than 32 MiB";
                else {
                    out.bytes.resize(static_cast<size_t>(size)); stream.seekg(0);
                    for (size_t offset = 0; offset < out.bytes.size();) {
                        { std::lock_guard lock(mutex_); if (stopping_ || generation != generation_) break; }
                        auto count = std::min<size_t>(256 * 1024, out.bytes.size() - offset);
                        stream.read(reinterpret_cast<char*>(out.bytes.data() + offset), static_cast<std::streamsize>(count));
                        if (!stream) { out.error = "Cannot read image"; break; } offset += count;
                    }
                    if (out.error.empty() && !SupportedImage(out.bytes)) out.error = "Unsupported PNG/JPEG or image dimensions";
                }
            } catch (const std::exception& error) { out.error = error.what(); }
            { std::lock_guard lock(mutex_); if (!stopping_ && generation == generation_) result_ = std::move(out); }
        }
    }
    std::mutex mutex_; std::condition_variable cv_; std::thread thread_;
    std::string path_; uint64_t generation_ = 0;
    bool pending_ = false, stopping_ = false;
    std::optional<Result> result_;
};
}
