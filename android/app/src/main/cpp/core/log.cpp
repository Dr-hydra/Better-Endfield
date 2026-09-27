#include "log.h"

#include <android/log.h>

#include <cstdio>
#include <cstdlib>
#include <mutex>
#include <unistd.h>

namespace betterendfield {
namespace {

constexpr char kLogTag[] = "BetterEndfield";

void Write(int priority, const char* component, const char* message) {
    __android_log_print(priority, kLogTag, "[%s] %s", component, message);
    const char* diagnostics = std::getenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH");
    if (diagnostics != nullptr && *diagnostics != '\0') {
        static std::mutex mutex;
        std::lock_guard lock(mutex);
        if (FILE* file = std::fopen(diagnostics, "a")) {
            if (std::fseek(file, 0, SEEK_END) == 0 && std::ftell(file) > 1024 * 1024) {
                std::fclose(file);
                file = std::fopen(diagnostics, "w");
                if (!file) return;
            }
            std::fprintf(file, "[tid=%d][%s] %s\n", static_cast<int>(gettid()), component, message);
            std::fclose(file);
        }
    }
}

}  // namespace

void LogInfo(const char* component, const char* message) {
    // MIUI suppresses injected native INFO messages for this game process.
    // Keep alpha diagnostics visible without using fatal/error severity.
    Write(ANDROID_LOG_WARN, component, message);
}

void LogError(const char* component, const char* message) {
    Write(ANDROID_LOG_ERROR, component, message);
}

}  // namespace betterendfield
