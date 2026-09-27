#pragma once
#include "camera_path.h"
#include <atomic>
#include <condition_variable>
#include <filesystem>
#include <fstream>
#include <mutex>
#include <optional>
#include <thread>
#if defined(_WIN32)
#include <Windows.h>
#else
#include <fcntl.h>
#include <unistd.h>
#endif

namespace BetterEndfield::CameraFiles {
enum class Kind { LoadVmd, LoadPath, SavePath, LoadMotion };
struct Job {
    Kind kind = Kind::LoadVmd;
    std::string path; // UTF-8, never interpreted as shell commands
    uint64_t generation = 0;
    CameraPath::Path snapshot;
};
struct Result {
    Kind kind = Kind::LoadVmd;
    uint64_t generation = 0;
    std::string error;
    std::vector<Vmd::CameraKey> cameras;
    Vmd::Motion motion;
    CameraPath::Path path;
};
inline bool ReadBounded(const std::filesystem::path& path, size_t limit,
    std::vector<uint8_t>& bytes, std::string& error) {
    std::error_code ec;
    if (!std::filesystem::is_regular_file(path,ec)) { error = "input must be a regular file"; return false; }
    std::ifstream file(path,std::ios::binary|std::ios::ate);
    if (!file) { error = "cannot open input file"; return false; }
    const auto size = file.tellg();
    if (size <= 0 || uint64_t(size) > limit) { error = "empty/oversize input file"; return false; }
    std::vector<uint8_t> next(static_cast<size_t>(size));
    file.seekg(0);
    if (!file.read(reinterpret_cast<char*>(next.data()),std::streamsize(next.size())) ||
        file.peek() != std::char_traits<char>::eof() || file.bad()) {
        error = "input changed or could not be read completely"; return false;
    }
    bytes = std::move(next);
    return true;
}
inline bool SaveAtomic(const std::filesystem::path& path, const std::string& text, std::string& error) {
    static std::atomic_uint64_t serial{0};
    std::filesystem::path temporary;
#if defined(_WIN32)
    HANDLE file = INVALID_HANDLE_VALUE;
#else
    int file = -1;
#endif
    for (int attempt = 0; attempt < 16; ++attempt) {
        temporary = path;
        temporary += ".be-tmp-" + std::to_string(serial.fetch_add(1));
#if defined(_WIN32)
        file = CreateFileW(temporary.c_str(),GENERIC_WRITE,0,nullptr,CREATE_NEW,FILE_ATTRIBUTE_NORMAL,nullptr);
        if (file != INVALID_HANDLE_VALUE) break;
        if (GetLastError() != ERROR_FILE_EXISTS && GetLastError() != ERROR_ALREADY_EXISTS) break;
#else
        file = ::open(temporary.c_str(),O_WRONLY|O_CREAT|O_EXCL|O_NOFOLLOW,0600);
        if (file >= 0 || errno != EEXIST) break;
#endif
    }
#if defined(_WIN32)
    if (file == INVALID_HANDLE_VALUE) { error = "cannot create adjacent temporary file"; return false; }
    DWORD written = 0;
    bool ok = WriteFile(file,text.data(),DWORD(text.size()),&written,nullptr) && written == text.size();
    if (ok) ok = FlushFileBuffers(file) != 0;
    if (!CloseHandle(file)) ok = false;
    if (ok) ok = MoveFileExW(temporary.c_str(),path.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH) != 0;
#else
    if (file < 0) { error = "cannot create adjacent temporary file"; return false; }
    bool ok = true;
    size_t offset = 0;
    while (offset < text.size()) {
        auto n = ::write(file,text.data()+offset,text.size()-offset);
        if (n < 0 && errno == EINTR) continue;
        if (n <= 0) { ok = false; break; }
        offset += size_t(n);
    }
    if (ok) ok = ::fsync(file) == 0;
    if (::close(file) != 0) ok = false;
    if (ok) ok = ::rename(temporary.c_str(),path.c_str()) == 0;
#endif
    if (!ok) {
        std::error_code ignored;
        std::filesystem::remove(temporary,ignored);
        error = "save/replace failed; destination was not replaced successfully";
    }
    return ok;
}
inline Result Process(const Job& job) {
    Result result;
    result.kind = job.kind; result.generation = job.generation;
    try {
        if (job.path.empty() || job.path.find('\0') != std::string::npos) {
            result.error = "configure a nonempty UTF-8 file path first"; return result;
        }
        auto path = std::filesystem::path(std::u8string(job.path.begin(),job.path.end()));
        if (job.kind == Kind::SavePath) {
            std::string text;
            if (CameraPath::Encode(job.snapshot,text,result.error)) SaveAtomic(path,text,result.error);
            return result;
        }
        std::vector<uint8_t> bytes;
        const size_t limit = job.kind == Kind::LoadPath ? CameraPath::MaxFileBytes : Vmd::Limits{}.file_bytes;
        if (!ReadBounded(path,limit,bytes,result.error)) return result;
        if (job.kind == Kind::LoadPath) {
            CameraPath::Decode({reinterpret_cast<const char*>(bytes.data()),bytes.size()},result.path,result.error);
        } else {
            Vmd::Motion motion;
            if (!Vmd::Parse(bytes,motion,result.error)) return result;
            if (job.kind == Kind::LoadMotion) {
                if (motion.bones.empty() && motion.morphs.empty()) result.error = "VMD contains no bone or morph keys";
                else result.motion = std::move(motion);
                return result;
            }
            if (motion.cameras.empty()) result.error = "VMD contains no camera keys";
            else if (std::any_of(motion.cameras.begin(),motion.cameras.end(),
                [](const auto& k) { return k.orthographic; }))
                result.error = "orthographic VMD camera is not supported by the current Unity adapter";
            else result.cameras = std::move(motion.cameras);
        }
    } catch (const std::exception& e) { result.error = e.what(); }
    catch (...) { result.error = "unknown camera file error"; }
    return result;
}

// One owned thread and one in-flight job/result. No fire-and-forget threads,
// no engine/JNI calls, and no unbounded queue of 64 MiB file parses.
class Worker final {
public:
    Worker() = default;
    Worker(const Worker&) = delete;
    Worker& operator=(const Worker&) = delete;
    ~Worker() { Stop(); }
    void Start() {
        std::lock_guard lock(mutex_);
        if (thread_.joinable()) return;
        stopping_ = false;
        thread_ = std::thread([this] { Run(); });
    }
    bool Submit(Job job) {
        std::lock_guard lock(mutex_);
        if (!thread_.joinable() || stopping_ || busy_) return false;
        pending_ = std::move(job); busy_ = true; condition_.notify_one();
        return true;
    }
    std::optional<Result> Poll() {
        std::lock_guard lock(mutex_);
        if (!completed_) return std::nullopt;
        auto result = std::move(completed_);
        completed_.reset(); busy_ = false;
        return result;
    }
    bool Busy() {
        std::lock_guard lock(mutex_);
        return busy_;
    }
    void Stop() {
        {
            std::lock_guard lock(mutex_);
            stopping_ = true; pending_.reset(); condition_.notify_all();
        }
        if (thread_.joinable()) thread_.join();
        std::lock_guard lock(mutex_);
        completed_.reset(); busy_ = false;
    }
private:
    void Run() {
        for (;;) {
            Job job;
            {
                std::unique_lock lock(mutex_);
                condition_.wait(lock,[&] { return stopping_ || pending_.has_value(); });
                if (stopping_) return;
                job = std::move(*pending_); pending_.reset();
            }
            Result result = Process(job);
            std::lock_guard lock(mutex_);
            if (stopping_) return;
            completed_ = std::move(result);
        }
    }
    std::thread thread_;
    std::mutex mutex_;
    std::condition_variable condition_;
    bool stopping_ = false, busy_ = false;
    std::optional<Job> pending_;
    std::optional<Result> completed_;
};
} // namespace BetterEndfield::CameraFiles
