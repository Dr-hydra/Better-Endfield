#include "command_pump.h"

#include <atomic>
#include <mutex>
#include <string_view>

namespace betterendfield {
namespace {
std::mutex g_mutex;
std::string g_pending;
std::string g_command;
std::string g_status = "BE_STATUS_V1\n0\nidle\n\n";
uint64_t g_generation = 0;
}

bool SubmitRuntimeCommand(const char* payload, size_t size) {
    if (!payload || size == 0 || size > 4096) return false;
    std::string value(payload, size);
    if (!value.starts_with("BE_COMMAND_V1\n")) return false;
    const size_t first = value.find('\n', 14);
    const size_t second = first == std::string::npos ? std::string::npos : value.find('\n', first + 1);
    const size_t third = second == std::string::npos ? std::string::npos : value.find('\n', second + 1);
    if (first == std::string::npos || second == std::string::npos || third == std::string::npos) return false;
    uint64_t generation = 0;
    try { generation = std::stoull(value.substr(14, first - 14)); }
    catch (...) { return false; }
    const std::string command = value.substr(first + 1, second - first - 1);
    if (command.empty() || command.size() > 32 || command.find_first_not_of("abcdefghijklmnopqrstuvwxyz0123456789_") != std::string::npos) return false;
    std::lock_guard lock(g_mutex);
    if (generation <= g_generation) return false;
    g_generation = generation;
    g_command = command;
    g_status = "BE_STATUS_V1\n" + std::to_string(generation) + "\naccepted\n" + command + "\n";
    g_pending = std::move(value);
    return true;
}

bool ConsumeRuntimeCommand(std::string& payload) {
    std::lock_guard lock(g_mutex);
    if (g_pending.empty()) return false;
    payload = std::move(g_pending);
    g_pending.clear();
    return true;
}

void AcknowledgeRuntimeCommand(const char* status) {
    if (!status || !*status) return;
    const std::string_view value(status);
    if (value != "applied" && value != "rejected" && value != "unsupported") return;
    std::lock_guard lock(g_mutex);
    g_status = "BE_STATUS_V1\n" + std::to_string(g_generation) + "\n" +
        std::string(value) + "\n" + g_command + "\n";
}

std::string CopyRuntimeCommandStatus() {
    std::lock_guard lock(g_mutex);
    return g_status;
}
}
