// EndfieldCamLink 注入器 v2
//
// 模式1(推荐): 挂起启动游戏 -> 注入 -> 恢复
//   本方案属于社区通行做法, 非本项目独创 —— 面对"要求管理员权限 + 带反作弊"的游戏,
//   运行中注入会被拒(OpenProcess err=5), 挂起期注入是唯一可行路径,
//   同类工具因此普遍采用同一思路。本项目采用该思路, 代码为独立实现。
//   injector.exe --launch "游戏Endfield.exe路径" "DLL绝对路径" [游戏参数...]
// 模式2(附加运行中进程):
//   injector.exe <PID|进程名> <DLL绝对路径>
#include <windows.h>
#include <tlhelp32.h>
#include <cstdio>
#include <cstring>
#include <string>

static DWORD FindPidByName(const char* name) {
    HANDLE snap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (snap == INVALID_HANDLE_VALUE) return 0;
    PROCESSENTRY32 pe{};
    pe.dwSize = sizeof(pe);
    DWORD pid = 0;
    if (Process32First(snap, &pe)) {
        do {
            if (_stricmp(pe.szExeFile, name) == 0) { pid = pe.th32ProcessID; break; }
        } while (Process32Next(snap, &pe));
    }
    CloseHandle(snap);
    return pid;
}

// 对已打开(拥有权限)的进程做 LoadLibrary 注入
static bool InjectDll(HANDLE proc, const std::string& dllPath) {
    char full[MAX_PATH];
    if (GetFullPathNameA(dllPath.c_str(), MAX_PATH, full, nullptr) == 0) {
        fprintf(stderr, "路径无效: %s\n", dllPath.c_str());
        return false;
    }
    const size_t len = strlen(full) + 1;
    void* remote = VirtualAllocEx(proc, nullptr, len, MEM_COMMIT | MEM_RESERVE,
                                  PAGE_READWRITE);
    if (!remote) {
        fprintf(stderr, "VirtualAllocEx 失败 err=%lu\n", GetLastError());
        return false;
    }
    if (!WriteProcessMemory(proc, remote, full, len, nullptr)) {
        fprintf(stderr, "WriteProcessMemory 失败 err=%lu\n", GetLastError());
        VirtualFreeEx(proc, remote, 0, MEM_RELEASE);
        return false;
    }
    HMODULE kernel32 = GetModuleHandleA("kernel32.dll");
    FARPROC loadLib = GetProcAddress(kernel32, "LoadLibraryA");
    HANDLE thread = CreateRemoteThread(proc, nullptr, 0,
        reinterpret_cast<LPTHREAD_START_ROUTINE>(loadLib), remote, 0, nullptr);
    if (!thread) {
        fprintf(stderr, "CreateRemoteThread 失败 err=%lu\n", GetLastError());
        VirtualFreeEx(proc, remote, 0, MEM_RELEASE);
        return false;
    }
    WaitForSingleObject(thread, 15000);
    DWORD exitCode = 0;
    GetExitCodeThread(thread, &exitCode);
    CloseHandle(thread);
    VirtualFreeEx(proc, remote, 0, MEM_RELEASE);
    printf("注入完成, LoadLibrary 返回模块句柄: 0x%08lX (0=失败)\n", exitCode);
    return exitCode != 0;
}

// 模式1: 挂起创建游戏 -> 注入 -> 恢复 (在 ACE/保护武装前完成, 游戏目录零写入)
static int LaunchAndInject(const std::string& gamePath, const std::string& dllPath,
                           const std::string& gameArgs) {
    std::string dir = gamePath;
    const size_t slash = dir.find_last_of("\\/");
    dir = slash == std::string::npos ? "." : dir.substr(0, slash);

    std::string cmdline = "\"" + gamePath + "\"";
    if (!gameArgs.empty()) cmdline += " " + gameArgs;

    STARTUPINFOA si{};
    si.cb = sizeof(si);
    PROCESS_INFORMATION pi{};
    char* cmd = new char[cmdline.size() + 1];
    strcpy(cmd, cmdline.c_str());
    if (!CreateProcessA(nullptr, cmd, nullptr, nullptr, FALSE,
                        CREATE_SUSPENDED, nullptr, dir.c_str(), &si, &pi)) {
        const DWORD err = GetLastError();
        fprintf(stderr,
                "CreateProcess 失败 err=%lu (740=需要管理员权限运行注入器)\n", err);
        delete[] cmd;
        return 8;
    }
    delete[] cmd;
    printf("游戏进程已挂起创建 (PID %lu), 开始注入...\n", pi.dwProcessId);
    if (!InjectDll(pi.hProcess, dllPath)) {
        TerminateProcess(pi.hProcess, 1);
        CloseHandle(pi.hThread);
        CloseHandle(pi.hProcess);
        return 9;
    }
    ResumeThread(pi.hThread);
    printf("已恢复游戏运行 (PID %lu)。注入成功, 模块日志见 EndfieldCamLink.log。\n",
           pi.dwProcessId);

    // 观察 60 秒: 若游戏秒退, 打印退出码帮助定位
    const DWORD waited = WaitForSingleObject(pi.hProcess, 60000);
    if (waited == WAIT_OBJECT_0) {
        DWORD code = 0;
        GetExitCodeProcess(pi.hProcess, &code);
        printf("游戏在 60 秒内退出, 退出码 0x%08lX\n", code);
        CloseHandle(pi.hThread);
        CloseHandle(pi.hProcess);
        return 10;
    }
    CloseHandle(pi.hThread);
    CloseHandle(pi.hProcess);
    return 0;
}

int main(int argc, char** argv) {
    if (argc >= 4 && strcmp(argv[1], "--launch") == 0) {
        std::string args;
        for (int i = 4; i < argc; ++i) {
            if (i > 4) args += " ";
            args += argv[i];
        }
        return LaunchAndInject(argv[2], argv[3], args);
    }
    if (argc >= 3) {
        DWORD pid = strtoul(argv[1], nullptr, 10);
        if (pid == 0) pid = FindPidByName(argv[1]);
        if (pid == 0) { fprintf(stderr, "找不到目标进程\n"); return 2; }
        HANDLE proc = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION |
                                      PROCESS_VM_OPERATION | PROCESS_VM_WRITE |
                                      PROCESS_VM_READ,
                                  FALSE, pid);
        if (!proc) {
            fprintf(stderr,
                    "OpenProcess 失败 (err=%lu)。若游戏以管理员运行, 请以管理员运行注入器; "
                    "运行中的游戏受保护时请改用 --launch 模式。\n",
                    GetLastError());
            return 4;
        }
        const bool ok = InjectDll(proc, argv[2]);
        CloseHandle(proc);
        return ok ? 0 : 5;
    }
    fprintf(stderr,
            "用法:\n"
            "  启动即注入: injector.exe --launch \"游戏Endfield.exe路径\" \"DLL绝对路径\" [游戏参数...]\n"
            "  附加注入:   injector.exe <PID|进程名> <DLL绝对路径>\n");
    return 1;
}
