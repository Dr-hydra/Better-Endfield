// Compare three ways to write N log lines into a growing file.
// A: open/append/close per line (current Logger::Write)
// B: one persistent ofstream, flush per line
// C: one persistent ofstream, flush every 64 lines
// D: one persistent ofstream, never flush until close (buffer only)
#include <chrono>
#include <cstdio>
#include <fstream>
#include <string>
#include <vector>

static double Ms() {
    using namespace std::chrono;
    return duration<double, std::milli>(steady_clock::now().time_since_epoch()).count();
}
static const char* kLine =
    "2026-10-25 19:00:00 [custom_model]   built t=T_actor_body_d WxH mips=10 BC7 sRGB graphicsFormat=25\n";

int main() {
    const int N = 256;
    const char* p = "bench_log_target.log";
    std::vector<std::string> lines;
    for (int i = 0; i < 2000; ++i) lines.push_back(kLine);   // pre-grown ~230 KB tail

    // A
    std::remove(p);
    { std::ofstream st(p, std::ios::out | std::ios::app); for (auto& l : lines) st << l; }
    double s = Ms();
    for (int i = 0; i < N; ++i) { std::ofstream st(p, std::ios::out | std::ios::app); st << kLine; }
    double e = Ms();
    std::printf("A open/append/close per line : %8.1f ms  (%6.1f us/line)\n", e - s, (e - s) * 1000 / N);

    // B
    std::remove(p);
    { std::ofstream st(p, std::ios::out | std::ios::app); for (auto& l : lines) st << l; }
    s = Ms();
    { std::ofstream st(p, std::ios::out | std::ios::app);
      for (int i = 0; i < N; ++i) { st << kLine; st.flush(); } }
    e = Ms();
    std::printf("B persistent + flush per line: %8.1f ms  (%6.1f us/line)\n", e - s, (e - s) * 1000 / N);

    // C
    std::remove(p);
    { std::ofstream st(p, std::ios::out | std::ios::app); for (auto& l : lines) st << l; }
    s = Ms();
    { std::ofstream st(p, std::ios::out | std::ios::app);
      for (int i = 0; i < N; ++i) { st << kLine; if ((i & 63) == 63) st.flush(); } }
    e = Ms();
    std::printf("C persistent + flush every 64: %8.1f ms  (%6.1f us/line)\n", e - s, (e - s) * 1000 / N);

    // D
    std::remove(p);
    { std::ofstream st(p, std::ios::out | std::ios::app); for (auto& l : lines) st << l; }
    s = Ms();
    { std::ofstream st(p, std::ios::out | std::ios::app); for (int i = 0; i < N; ++i) st << kLine; }
    e = Ms();
    std::printf("D persistent, flush on close   : %8.1f ms  (%6.1f us/line)\n", e - s, (e - s) * 1000 / N);

    std::remove(p);
    return 0;
}
