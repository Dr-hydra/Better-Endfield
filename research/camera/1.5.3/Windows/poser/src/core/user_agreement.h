#pragma once
#include <windows.h>
#include <atomic>
#include <cstring>
#include <string>
#include "plugin_paths.h"

namespace poser_agreement {
// This revision changes only when the agreement changes, not on every release.
inline constexpr int kRevision = 2;
inline constexpr wchar_t kFileName[] = L"poser_agreement.txt";
static std::string Receipt() {
  return "Endfield Poser agreement\nrevision=" + std::to_string(kRevision) + "\naccepted=1\n";
}

class Consent {
  std::atomic<bool> accepted_{false};
public:
  bool allowed() const { return accepted_.load(); }
  void load(const std::wstring &path) {
    accepted_.store(false);
    HANDLE file = CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ,
                              nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file == INVALID_HANDLE_VALUE) return;
    const std::string receipt = Receipt();
    char bytes[256] = {};
    DWORD count = 0;
    // A bounded read larger than any valid receipt also detects extra/corrupt data.
    const bool ok = ReadFile(file, bytes, sizeof(bytes), &count, nullptr) != FALSE;
    CloseHandle(file);
    accepted_.store(ok && count == receipt.size() &&
                    memcmp(bytes, receipt.data(), receipt.size()) == 0);
  }
  // Called only by the explicit agreement button. A failed write must not unlock
  // the plugin or damage a previous receipt. There is no remote accept endpoint.
  bool accept(const std::wstring &path, DWORD &error) {
    error = ERROR_SUCCESS;
    const auto temporary = path + L".tmp-" + std::to_wstring(GetCurrentProcessId()) +
                           L"-" + std::to_wstring(GetCurrentThreadId());
    HANDLE file = CreateFileW(temporary.c_str(), GENERIC_WRITE, 0, nullptr,
                              CREATE_NEW, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file == INVALID_HANDLE_VALUE) { error = GetLastError(); return false; }
    const std::string receipt = Receipt();
    DWORD count = 0;
    bool ok = WriteFile(file, receipt.data(), (DWORD)receipt.size(), &count, nullptr) != FALSE;
    if (!ok) error = GetLastError();
    else if (count != receipt.size()) { ok = false; error = ERROR_WRITE_FAULT; }
    if (ok && !FlushFileBuffers(file)) { ok = false; error = GetLastError(); }
    CloseHandle(file);
    if (ok && !MoveFileExW(temporary.c_str(), path.c_str(),
                           MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH)) {
      ok = false; error = GetLastError();
    }
    if (!ok) { DeleteFileW(temporary.c_str()); return false; }
    accepted_.store(true);
    return true;
  }
};
static Consent state;
static bool Allowed() { return state.allowed(); }
} // namespace poser_agreement
