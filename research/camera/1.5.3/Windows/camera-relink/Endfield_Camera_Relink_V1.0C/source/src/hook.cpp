#include "hook.h"
#include <MinHook.h>

namespace ecl {

namespace {
int g_lastStatus = 0;  // MH_OK
}

bool InstallHook(void* target, void* detour, void** original) {
    g_lastStatus = 0;
    if (!target || !detour) {
        g_lastStatus = -1;
        return false;
    }
    // 关键修复: MH_Initialize 在第二次调用时返回 MH_ERROR_ALREADY_INITIALIZED,
    // 早期版本把它当成失败直接 return, 导致"只能挂上第一个钩子"。
    // 另外失败分支不能调用 MH_Uninitialize() —— 那会把已装好的钩子一起拆掉。
    const MH_STATUS init = MH_Initialize();
    if (init != MH_OK && init != MH_ERROR_ALREADY_INITIALIZED) {
        g_lastStatus = static_cast<int>(init);
        return false;
    }
    MH_STATUS s = MH_CreateHook(target, detour, original);
    if (s != MH_OK) {
        g_lastStatus = static_cast<int>(s);
        return false;
    }
    s = MH_EnableHook(target);
    if (s != MH_OK) {
        g_lastStatus = static_cast<int>(s);
        MH_RemoveHook(target);
        return false;
    }
    return true;
}

bool RemoveHook(void* target) {
    if (MH_DisableHook(target) != MH_OK) return false;
    if (MH_RemoveHook(target) != MH_OK) return false;
    return true;
}

int LastHookStatus() { return g_lastStatus; }

}  // namespace ecl
