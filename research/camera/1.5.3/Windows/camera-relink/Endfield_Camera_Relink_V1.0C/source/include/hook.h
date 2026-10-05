#pragma once
namespace ecl {

// 复用 MinHook 安装/卸载 inline hook。
// MinHook 1.3.3, 许可: BSD-2-Clause (见 source/third_party/minhook/LICENSE.txt)
// 返回 false 表示失败; original 可空(不调用原函数)。
// 注意: 支持多次调用(可同时挂多个钩子)。
bool InstallHook(void* target, void* detour, void** original);
bool RemoveHook(void* target);

// 最近一次安装失败的 MinHook 状态码(诊断用; 0 = MH_OK)
int LastHookStatus();

}  // namespace ecl
