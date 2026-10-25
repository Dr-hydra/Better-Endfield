// Verifies the two behavioural claims of the held-handle Logger change:
//  1. the log is readable from another handle while the Logger is alive and
//     still writing (native/tests/hook_chain/hook_chain_tests.cpp:195 does this);
//  2. the tail survives a hard exit with no destructors (flush per line).
// Build: see worktree/audit/bench/build_log_probe.sh
#include "logging.h"

#include <chrono>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <ctime>
#include <filesystem>
#include <fstream>
#include <sstream>
#include <string>

namespace fs = std::filesystem;
using BetterEndfieldNext::Host::Logger;

// The pre-change implementation, kept here as the A side of the A/B. Opens the
// file, writes one line, closes it, exactly as logging.cpp did before.
static void WriteReopening(const fs::path& file, const std::string& source, const std::string& message) {
    std::ofstream stream(file, std::ios::out | std::ios::app);
    if (!stream) return;
    const auto now = std::chrono::system_clock::now();
    const auto local = std::chrono::system_clock::to_time_t(now);
    std::tm info{};
    localtime_s(&info, &local);
    stream << std::put_time(&info, "%Y-%m-%d %H:%M:%S") << " [" << source << "] " << message << '\n';
}

static double BenchReopening(const fs::path& file, int lines) {
    const auto start = std::chrono::steady_clock::now();
    for (int i = 0; i < lines; ++i)
        WriteReopening(file, "bench", "Model upload transaction resource=packed.assets mode=synchronous published=true");
    const auto end = std::chrono::steady_clock::now();
    return std::chrono::duration<double, std::milli>(end - start).count() / lines;
}

static double BenchHeld(const fs::path& root, int lines) {
    Logger logger;
    logger.Initialize(root);
    const auto start = std::chrono::steady_clock::now();
    for (int i = 0; i < lines; ++i)
        logger.Write("bench", "Model upload transaction resource=packed.assets mode=synchronous published=true");
    const auto end = std::chrono::steady_clock::now();
    return std::chrono::duration<double, std::milli>(end - start).count() / lines;
}

static std::string Slurp(const fs::path& p) {
    std::ifstream in(p, std::ios::binary);
    std::stringstream ss;
    ss << in.rdbuf();
    return ss.str();
}

int main(int argc, char** argv) {
    const bool crash_mode = argc > 1 && std::strcmp(argv[1], "crash") == 0;
    const bool bench_mode = argc > 1 && std::strcmp(argv[1], "bench") == 0;
    const fs::path root = fs::path(argv[crash_mode ? 2 : 1]);

    if (bench_mode) {
        const int lines = argc > 3 ? std::atoi(argv[3]) : 200;
        const fs::path a = root / "reopen";
        const fs::path b = root / "held";
        std::error_code ec;
        fs::create_directories(a, ec);
        fs::create_directories(b, ec);
        // Warm up so the first-run file creation does not bias either side.
        BenchReopening(a / "BetterEndfieldNext.log", 5);
        BenchHeld(b, 5);
        const double reopen = BenchReopening(a / "BetterEndfieldNext.log", lines);
        const double held = BenchHeld(b, lines);
        std::printf("lines=%d\n", lines);
        std::printf("reopen_ms_per_line=%.3f\n", reopen);
        std::printf("held_ms_per_line=%.3f\n", held);
        std::printf("speedup=%.1fx\n", reopen / held);
        return 0;
    }

    const fs::path file = root / "BetterEndfieldNext.log";

    Logger logger;
    logger.Initialize(root);
    logger.Write("probe", "line one");
    logger.Write("probe", "line two");

    // Claim 1: a second handle sees the flushed tail immediately.
    const std::string mid = Slurp(file);
    const bool saw_one = mid.find("line one") != std::string::npos;
    const bool saw_two = mid.find("line two") != std::string::npos;

    logger.Write("probe", "line three");
    const std::string late = Slurp(file);
    const bool saw_three = late.find("line three") != std::string::npos;

    if (crash_mode) {
        logger.Write("probe", "tail before hard exit");
        std::fflush(nullptr);
        std::_Exit(0); // no destructors: the tail must already be on disk
    }

    std::printf("readable_while_alive=%s\n", (saw_one && saw_two) ? "yes" : "no");
    std::printf("sees_new_line_while_alive=%s\n", saw_three ? "yes" : "no");
    std::printf("line_count=%d\n", [&] {
        int n = 0;
        std::istringstream in(late);
        for (std::string l; std::getline(in, l);) ++n;
        return n;
    }());
    return (saw_one && saw_two && saw_three) ? 0 : 1;
}
