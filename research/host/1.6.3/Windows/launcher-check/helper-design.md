# 独立启动 helper 的最小实现准备

2026-10-15。本记录仅梳理构建工具、现有入口与实现约束，未编写 helper、未改生产入口、未启动游戏。父进程检查的具体接受条件仍由局部分析确定；不能把“helper 改名”当成已验证方案。

## 位置与构建

- 源码建议放 `research/host/1.6.3/Windows/launcher-check/helper/`，单独 `CMakeLists.txt + main.cpp`；产物放 `build/launcher-helper-1.6.3`。
- 本机已确认存在 `C:\Program Files\CMake\bin\cmake.exe`。
- 现 native 工程 generator 为 `Visual Studio 17 2022`，实例为 `C:/Program Files (x86)/Microsoft Visual Studio/2022/BuildTools`。
- 已确认编译器 `C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Tools\MSVC\14.44.35207\bin\Hostx64\x64\cl.exe`、`Common7\Tools\VsDevCmd.bat`、`MSBuild\Current\Bin\MSBuild.exe` 都存在。`cl` 不在当前普通 PowerShell PATH；单独 CMake 的 VS generator 可直接选该实例，无需修改环境。
- 备选是 `C:\Program Files\dotnet\dotnet.exe` / SDK `9.0.314`，本轮元数据 console 已实际编译通过。独立 native console 更适合需要精确保留 Win32 进程行为的最小实验，不增加 .NET 运行时依赖。
- C++ 仅需 Windows API、`wmain`，使用 `/MT`、Unicode、C++17 即够。不链接 Host/Injector，也不构建整个 native 项目。

## 必须保留的启动行为

1. 显式传入绝对 `Endfield.exe` 路径作为 `CreateProcessW.lpApplicationName`；命令行单独以带引号的 exe 路径开头，UTF-16 可写缓冲区。
2. 按 Windows 参数规则重建每个参数，正确处理空参数、引号、引号前反斜杠、尾部反斜杠。现 Injector 的简单转义代码不宜直接复制。
3. `lpCurrentDirectory` 使用游戏 exe 所在目录，不使用 helper 或 Steam 所在目录。
4. `lpEnvironment=nullptr`，原样继承 Steam/调用方环境；`bInheritHandles=FALSE`；基础创建 flags 为 `0`。无需 suspended、注入或 ShellExecute 中转。
5. 关闭线程句柄后保留进程句柄；`WaitForSingleObject(process, INFINITE)` 等待真正的游戏子进程结束，再用 `GetExitCodeProcess` 读取结果并用 `ExitProcess(DWORD)` 原样返回。创建/等待失败时分别报告 Win32 错误；不要返回“成功”掩盖游戏未启动。
6. 不调用 `TerminateProcess`，不设置会在 helper 关闭时杀死游戏的 job 标志。helper 应只观察直接子进程生命周期。
7. 如普通权限创建返回 `ERROR_ELEVATION_REQUIRED`，明确报告该结果，不偷偷换成 ShellExecute/runas，因为那会改变本次欲验证的直接父子创建关系。helper 运行权限须与最终实验方案一起确定。

helper 存活可避免启动器提前退出造成的生命周期断裂，但 Steam 状态、时长、覆盖层的最终表现仍需实机验证，不能仅由 wait 推断通过。

## 参数契约与 Steam 接入

研究工具可用 `--game <absolute exe> [--game-arg <one argument>]... [--steam-command <remaining Steam command>]`。每个 `--game-arg` 只传一个实际游戏参数；遇到 `--steam-command` 后停止解析并不向国服转发尾部。这样在未来 Steam 启动选项中追加 `%command%` 时，展开的国际服/占位 exe 不会意外成为国服参数。首次本地实验不需要 `%command%`。

不要把官方/Steam 登录令牌、账号参数猜测性地拼接到游戏参数。本轮只需保留用户已有的普通游戏参数，例如 `-force-d3d11`。

## 现有入口及后续接点

- `native/loaders/injector/main.cpp:516`：`--game` + `--` 游戏参数；`:577` 使用 `CreateProcessW(CREATE_SUSPENDED)`，随后加载 Bootstrap/Host，成功后关闭进程句柄立即返回。因此现 Injector 不适合直接作为 Steam 等待 helper，且它会拒绝游戏目录已有 XInput 代理。
- `ui/BetterEndfieldNext.UI/MainWindow.xaml.cs:1966`：XInput 模式安装代理后仍 `Process.Start(Endfield.exe)`；Injector 分支以 `runas` 启动 Injector。未来确定正常启动路径后才应改这些生产入口。
- `native/loaders/xinput/input_proxy.cpp:176`：DllMain 启动工作线程，读取 `%LOCALAPPDATA%\BetterEndfieldNext\BetterEndfieldNext.ini` 的 `Loader.install_root/load_host`，加载 Host。helper 本身不需实现注入；已按既有工具安装的 XInput 可独立承担模块加载。
- `ui/BetterEndfieldNext.UI/Services/SteamIntegrationService.cs:77`：目前生成 `"国服Endfield.exe" 参数 %command%`。后续若 helper 实验通过，应在此增加明确的 helper 路径/参数构建，并相应调整 UI 启动命令展示与测试；ACF 元数据本身不受 helper 选择影响。
- `ui/BetterEndfieldNext.UI/Views/SteamIntegrationPage.xaml.cs:268`：复制上述命令；启动按钮经 `SteamEnvironment.Launch` 调用 `steam.exe -applaunch 4732690`。当前不修改。

最小验证顺序是先用独立无副作用子进程确认参数、工作目录、等待和退出码转发；确认局部分析的校验条件后再决定是否值得做一次用户授权的游戏启动实验。本记录没有宣布任何绕过方式已成功。
