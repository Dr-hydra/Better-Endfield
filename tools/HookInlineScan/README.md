# HookInlineScan

检查被 hook 的托管方法在当前 `GameAssembly.dll` 里是否还会经过它的入口。

按名字解析只能证明方法存在，不能证明游戏还调用这份代码。MSVC 会把小方法或只有一个调用方的方法内联进调用方，hook 装在原入口上也看不到这些调用。2026-09-03 PC 版 `HashStringPathProcessor.InitMainPathHash` 被内联进 `GameInitState._ReloadResourceIndexes`，登录模型替换因此失效（见 `docs/host/research/1.5.3/hook-update/GAME_UPDATE_20260903_HOOK_DIFF.md` 勘误）。

游戏更新后先跑一遍这个工具，再决定哪些 hook 需要改挂。

## 原理

对每个被 hook 的方法：

- 统计全程序指向它入口的 `call rel32` / `jmp rel32`；
- 判断它是否尾跳到另一个方法（薄包装）；
- **用 IFix 找内联副本**。这个游戏的每个插桩方法入口都会检查自己的补丁 ID：`mov ecx, id; call IsPatched/GetPatch`，或者在 `IsPatched` 被内联后写成 `cmp dword [reg+0x18], id; jg 冷路径`，冷路径里再调用 `GetPatch(id)`。`IsPatched`/`GetPatch` 每个程序集各有一份，所以"程序集 + ID"唯一对应一个方法。方法被内联到哪里，这段检查就会被复制到哪里。副本所在的函数还要调用原方法大部分有特征的被调函数，才算确认（输出里的 `callees n/m`）。

## 用法

```powershell
python -m pip install -r tools/HookInlineScan/requirements.txt

python tools/HookInlineScan/hook_inline_scan.py `
  --game "D:\Arknights Endfield" `
  --dump research/il2cpp-dumps/20260903-pc-current
```

`--dump` 必须是**同一版本**客户端的 IL2CPP dump，要包含 `IL2CPP_Dump_AI`，`IL2CPP_Dump_Normal` 可选，用来识别 `virtual` 等修饰符。

hook 目标有三种来源：

| 参数 | 来源 |
|---|---|
| 默认（`--hooks hooked_methods.json`） | 从源码整理的 hook 描述符清单（2026-10-03 快照，共 137 个），按名字和签名在 dump 里匹配 |
| `--log %LOCALAPPDATA%\BetterEndfield\logs\BetterEndfield.log` | 新版 Host 运行一次后，日志中 `Hook installed ... at GameAssembly.dll+0x...` 的行，就是实际装上的目标 |
| `--rva 0x472E3D0 ...` | 直接指定 RVA |

模块新增或修改 hook 后，`hooked_methods.json` 会过时，这时优先用 `--log`。

其他参数：`--all` 同时列出没有发现问题的 hook；`--json out.json` 输出完整结果。索引按 `GameAssembly.dll` 的时间戳缓存在 `.cache/`，同一版本第二次运行会快很多。

## 结论分类

| 分类 | 含义 | 处理 |
|---|---|---|
| `never` | 没有直接调用点，且找到了确认的内联副本 | hook 不会触发，改挂副本所在的方法或它调用的方法 |
| `partial` | 有直接调用，但部分调用方使用内联副本 | 看副本那条路径对功能是否重要 |
| `no-direct-callers` | 没有直接调用点，也没有 IFix 副本 | 可能是委托、Unity 消息、反射/Lua，也可能是没有 IFix 插桩的方法被内联了。结合运行时日志判断 |
| `wrapper` | 尾跳到另一个方法 | 当前没问题，但这类薄包装最容易在下个版本被内联，可以考虑直接挂它转发的那个方法 |
| `unresolved` | dump 里找不到这个描述符 | 方法改名或已删除 |

## 与运行时诊断配合

Host 会在日志里记录每个 hook 的安装地址、首次调用时间，以及装好 3、15、60 分钟后仍未被调用的 hook。`no-direct-callers` 这类静态分析无法下结论的情况，以运行时日志里是否出现 `First call observed` 为准。

## 局限

- 只有带 IFix 插桩的程序集才能定位内联副本。`UnityEngine.*`、Wwise、RootMotion 等程序集只能给出调用点数量和尾跳信息。
- 方法如果在入口处先内联了别的方法，它的第一个 IFix 检查会是被内联方法的。这种情况输出中标为 `entry check also starts the smaller ...`，对应的副本结论不可靠。
- 只分析 Windows x64 的 `GameAssembly.dll`。
