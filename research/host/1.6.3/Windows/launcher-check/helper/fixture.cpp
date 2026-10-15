#include <windows.h>
#include <tlhelp32.h>

#include <filesystem>
#include <fstream>
#include <string>
#include <vector>

std::string Utf8(const std::wstring& value) {
    const int size = WideCharToMultiByte(CP_UTF8, WC_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), nullptr, 0, nullptr, nullptr);
    std::string result(static_cast<size_t>(size), '\0');
    if (size) WideCharToMultiByte(CP_UTF8, WC_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), result.data(), size, nullptr, nullptr);
    return result;
}

std::string Json(const std::wstring& value) {
    std::string result = "\"";
    const char* hex = "0123456789abcdef";
    for (const unsigned char character : Utf8(value)) {
        if (character == '\\' || character == '"') { result += '\\'; result += static_cast<char>(character); }
        else if (character < 32) { result += "\\u00"; result += hex[character >> 4]; result += hex[character & 15]; }
        else result += static_cast<char>(character);
    }
    return result + '"';
}

int wmain(int argc, wchar_t* argv[]) {
    if (argc < 5 || std::wstring(argv[1]) != L"--report" || std::wstring(argv[3]) != L"--exit") return 2;
    const DWORD code = static_cast<DWORD>(std::stoull(argv[4]));
    const DWORD current_id = GetCurrentProcessId();
    DWORD parent_id = 0;
    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    PROCESSENTRY32W entry{};
    entry.dwSize = sizeof(entry);
    if (snapshot != INVALID_HANDLE_VALUE) {
        if (Process32FirstW(snapshot, &entry)) do {
            if (entry.th32ProcessID == current_id) { parent_id = entry.th32ParentProcessID; break; }
        } while (Process32NextW(snapshot, &entry));
        CloseHandle(snapshot);
    }
    std::wstring parent_image;
    bool parent_alive = false;
    HANDLE parent = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION | SYNCHRONIZE, FALSE, parent_id);
    if (parent) {
        parent_alive = WaitForSingleObject(parent, 0) == WAIT_TIMEOUT;
        std::vector<wchar_t> name(32768);
        DWORD length = static_cast<DWORD>(name.size());
        if (QueryFullProcessImageNameW(parent, 0, name.data(), &length)) parent_image.assign(name.data(), length);
        CloseHandle(parent);
    }
    std::vector<wchar_t> environment(32768);
    const DWORD length = GetEnvironmentVariableW(L"BE_LAUNCHBRIDGE_TEST_VALUE", environment.data(), static_cast<DWORD>(environment.size()));
    std::ofstream report(std::filesystem::path(argv[2]), std::ios::binary);
    report << "{\n  \"pid\":" << current_id << ",\n  \"parentPid\":" << parent_id
        << ",\n  \"parentAlive\":" << (parent_alive ? "true" : "false")
        << ",\n  \"parentImage\":" << Json(parent_image)
        << ",\n  \"cwd\":" << Json(std::filesystem::current_path().wstring())
        << ",\n  \"environment\":" << Json(length < environment.size() ? std::wstring(environment.data(), length) : L"")
        << ",\n  \"arguments\":[";
    for (int index = 5; index < argc; ++index) {
        if (index != 5) report << ',';
        report << Json(argv[index]);
    }
    report << "]\n}\n";
    report.close();
    Sleep(1200);
    ExitProcess(code);
}
