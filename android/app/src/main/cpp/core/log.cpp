#include "log.h"

#include <android/log.h>

#include <cstdio>
#include <cstdlib>

namespace betterendfield {
namespace {

constexpr char kLogTag[] = "BetterEndfield";

void Write(int priority, const char* component, const char* message) {
    __android_log_print(priority, kLogTag, "[%s] %s", component, message);
    const char* diagnostics = std::getenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH");
    if (diagnostics != nullptr && *diagnostics != '\0') {
        if (FILE* file = std::fopen(diagnostics, "a")) {
            std::fprintf(file, "[%s] %s\n", component, message);
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
