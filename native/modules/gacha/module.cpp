#include <BetterEndfield/ModuleApi.h>

#include <Windows.h>
#include <sddl.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <chrono>
#include <cstdint>
#include <string>
#include <string_view>
#include <thread>

namespace BetterEndfield::Gacha {
namespace {

constexpr char kModuleId[] = "betterendfield.gacha";
constexpr DWORD kMaximumResponseBytes = 32 * 1024;

using Il2CppDomainGetFn = void* (*)();
using Il2CppThreadAttachFn = void* (*)(void* domain);
using Il2CppThreadDetachFn = void (*)(void* thread);

struct MethodContract {
    const char* name;
    BE_MethodDescriptorV1 descriptor;
    const void* method_info = nullptr;
};

MethodContract g_methods[] = {
    {"token", {"Common.Beyond.dll", "Beyond.SDK", "SDKAccountUtils",
        "GetU8SessionToken", nullptr, "System.String", 0}},
    {"uid", {"Common.Beyond.dll", "Beyond.SDK", "SDKAccountUtils",
        "GetCurrentUID", nullptr, "System.String", 0}},
    {"channel", {"Common.Beyond.dll", "Beyond.SDK", "SDKAccountUtils",
        "GetSubChannel", nullptr, "System.String", 0}},
    {"region", {"Common.Beyond.dll", "Beyond.SDK", "SDKAccountUtils",
        "GetRegion", nullptr, "System.String", 0}},
};

const BE_HostApiV1* g_host = nullptr;
std::jthread g_worker;
std::atomic<bool> g_enabled{true};
Il2CppDomainGetFn g_domain_get = nullptr;
Il2CppThreadAttachFn g_thread_attach = nullptr;
Il2CppThreadDetachFn g_thread_detach = nullptr;

void Log(const char* message) {
    if (g_host && g_host->log) g_host->log(g_host->context, kModuleId, message);
}

void SecureClear(std::string& value) {
    if (!value.empty()) SecureZeroMemory(value.data(), value.size());
    value.clear();
    value.shrink_to_fit();
}

std::string EscapeJson(std::string_view value) {
    std::string result;
    result.reserve(value.size() + 8);
    for (const unsigned char c : value) {
        switch (c) {
        case '\\': result += "\\\\"; break;
        case '"': result += "\\\""; break;
        case '\b': result += "\\b"; break;
        case '\f': result += "\\f"; break;
        case '\n': result += "\\n"; break;
        case '\r': result += "\\r"; break;
        case '\t': result += "\\t"; break;
        default:
            if (c >= 0x20) result.push_back(static_cast<char>(c));
            break;
        }
    }
    return result;
}

std::string InvokeString(const MethodContract& method) {
    if (!g_host || !method.method_info) return {};
    void* exception = nullptr;
    void* managed = g_host->runtime_invoke(
        g_host->context, method.method_info, nullptr, nullptr, &exception);
    if (!managed || exception) return {};
    std::array<char, 8192> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, managed, buffer.data(), buffer.size());
    return copied > 0 ? std::string(buffer.data(), static_cast<size_t>(copied))
                      : std::string{};
}

bool WriteAll(HANDLE pipe, const void* data, DWORD size) {
    const auto* bytes = static_cast<const BYTE*>(data);
    DWORD offset = 0;
    while (offset < size) {
        DWORD written = 0;
        if (!WriteFile(pipe, bytes + offset, size - offset, &written, nullptr) || !written) {
            return false;
        }
        offset += written;
    }
    return true;
}

void ServeClient(HANDLE pipe, const std::string& token, const std::string& uid,
    const std::string& channel, const std::string& region) {
    char request[16]{};
    DWORD read = 0;
    if (!ReadFile(pipe, request, sizeof(request), &read, nullptr) ||
        read < 3 || std::string_view(request, read).find("GET") != 0) {
        return;
    }

    const std::string json = token.empty()
        ? "{\"ready\":false}"
        : "{\"ready\":true,\"token\":\"" + EscapeJson(token) +
          "\",\"uid\":\"" + EscapeJson(uid) + "\",\"channel\":\"" +
          EscapeJson(channel) + "\",\"region\":\"" + EscapeJson(region) + "\"}";
    const uint32_t length = static_cast<uint32_t>(json.size());
    if (length <= kMaximumResponseBytes) {
        WriteAll(pipe, &length, sizeof(length));
        WriteAll(pipe, json.data(), length);
        FlushFileBuffers(pipe);
    }
}

void WorkerMain(std::stop_token stop_token) {
    void* attached_thread = nullptr;
    for (int attempt = 0; attempt < 120 && !stop_token.stop_requested(); ++attempt) {
        if (g_domain_get && g_thread_attach) {
            if (void* domain = g_domain_get()) attached_thread = g_thread_attach(domain);
        }
        if (attached_thread) break;
        std::this_thread::sleep_for(std::chrono::milliseconds(250));
    }
    if (!attached_thread) {
        Log("Gacha session worker could not attach to IL2CPP after retries.");
        return;
    }

    std::string token;
    std::string uid;
    std::string channel;
    std::string region;
    while (token.empty() && !stop_token.stop_requested()) {
        token = InvokeString(g_methods[0]);
        if (!token.empty()) {
            uid = InvokeString(g_methods[1]);
            channel = InvokeString(g_methods[2]);
            region = InvokeString(g_methods[3]);
            break;
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(500));
    }
    if (g_thread_detach) {
        g_thread_detach(attached_thread);
        attached_thread = nullptr;
    }
    Log(token.empty() ? "Gacha session bridge stopped before session became available."
                      : "Gacha session bridge is ready; session captured once.");
    const std::wstring pipe_name = L"\\\\.\\pipe\\BetterEndfield.Gacha." +
        std::to_wstring(GetCurrentProcessId());
    PSECURITY_DESCRIPTOR descriptor = nullptr;
    SECURITY_ATTRIBUTES security_attributes{};
    security_attributes.nLength = sizeof(security_attributes);
    if (ConvertStringSecurityDescriptorToSecurityDescriptorW(
            L"D:P(A;;GA;;;AU)", SDDL_REVISION_1, &descriptor, nullptr)) {
        security_attributes.lpSecurityDescriptor = descriptor;
    }
    while (!stop_token.stop_requested()) {
        HANDLE pipe = CreateNamedPipeW(pipe_name.c_str(),
            PIPE_ACCESS_DUPLEX | FILE_FLAG_FIRST_PIPE_INSTANCE,
            PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_NOWAIT,
            1, kMaximumResponseBytes, 64, 1000,
            security_attributes.lpSecurityDescriptor ? &security_attributes : nullptr);
        if (pipe == INVALID_HANDLE_VALUE) {
            std::this_thread::sleep_for(std::chrono::milliseconds(250));
            continue;
        }
        while (!stop_token.stop_requested()) {
            if (ConnectNamedPipe(pipe, nullptr) || GetLastError() == ERROR_PIPE_CONNECTED) {
                DWORD mode = PIPE_READMODE_BYTE | PIPE_WAIT;
                SetNamedPipeHandleState(pipe, &mode, nullptr, nullptr);
                if (g_enabled.load(std::memory_order_acquire)) {
                    ServeClient(pipe, token, uid, channel, region);
                }
                break;
            }
            if (GetLastError() != ERROR_PIPE_LISTENING && GetLastError() != ERROR_NO_DATA) break;
            std::this_thread::sleep_for(std::chrono::milliseconds(100));
        }
        DisconnectNamedPipe(pipe);
        CloseHandle(pipe);
    }
    SecureClear(token);
    SecureClear(uid);
    SecureClear(channel);
    SecureClear(region);
    if (descriptor) LocalFree(descriptor);
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->runtime_invoke ||
        !host->copy_managed_string || !host->log) {
        return BE_Result_InvalidArgument;
    }
    g_host = host;
    for (auto& method : g_methods) {
        BE_ResolvedMethodV1 resolved{};
        if (host->resolve_method(host->context, &method.descriptor, &resolved) == BE_Result_Ok) {
            method.method_info = resolved.method_info;
        } else if (method.name == std::string_view("token")) {
            Log("Gacha token getter contract was not found.");
            g_host = nullptr;
            return BE_Result_ContractMismatch;
        }
    }
    HMODULE game_assembly = GetModuleHandleW(L"GameAssembly.dll");
    g_domain_get = reinterpret_cast<Il2CppDomainGetFn>(
        GetProcAddress(game_assembly, "il2cpp_domain_get"));
    g_thread_attach = reinterpret_cast<Il2CppThreadAttachFn>(
        GetProcAddress(game_assembly, "il2cpp_thread_attach"));
    g_thread_detach = reinterpret_cast<Il2CppThreadDetachFn>(
        GetProcAddress(game_assembly, "il2cpp_thread_detach"));
    if (!g_domain_get || !g_thread_attach || !g_thread_detach) return BE_Result_ContractMismatch;
    g_worker = std::jthread(WorkerMain);
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    g_enabled.store(true, std::memory_order_release);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    g_enabled.store(false, std::memory_order_release);
    if (g_worker.joinable()) {
        g_worker.request_stop();
        CancelSynchronousIo(g_worker.native_handle());
        g_worker.join();
    }
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Gacha Session Bridge", "1.0.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize, &ConfigurationChanged, &Shutdown};

} // namespace
} // namespace BetterEndfield::Gacha

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::Gacha::kApi;
}
