# Steam 启动选项自动配置

## 问题与证据

2026-10-15，本机 Steam 在 11:50:33、11:50:41、11:50:48 三次直接执行 `steamapps/common/Arknights Endfield/Endfield.exe`，返回 `OS Error 193`，界面显示“平台无效”。该文件是 BE 管理的 0 字节占位文件，当前账号的 `LaunchOptions` 缺失。启动链没有进入 BE helper。

原实现应用 ACF 后即启用启动按钮，仅生成并验证预期命令，没有核对 Steam 保存的命令。因此首次配置遗漏粘贴、加载模式改变或安装路径变化，都可能让启动按钮发出无效请求。

## 当前行为

- “应用 / 更新配置”先检查账号和命令，再应用 ACF 并自动填写启动选项。
- “从 Steam 启动国服”检查预期命令；Steam 已退出时自动补写或更新，命令正确时允许使用运行中的 Steam。需要更新且 Steam 仍运行时，提示完整退出后再点击启动。
- 优先使用运行中 Steam 的 ActiveUser；退出后读取 `loginusers.vdf` 的唯一 MostRecent，缺失时使用唯一最新合法 Timestamp。本机当前文件没有 MostRecent，因此覆盖了 Timestamp 路径。账户不明确时要求先正常登录，不向所有 userdata 写入。
- 只编辑 `UserLocalConfigStore/Software/Valve/Steam/apps/4732690/LaunchOptions`。保留其他字段、文本、编码、注释和 BOM；不改其他游戏或账号。
- 原选项和写入状态保存在 BE 配置目录 `steam/launch-options.json`。写入前记录归属，使用原子替换；写前检查 Steam 状态和文件是否改变。移除配置时按账号恢复原值，独立修改过的启动选项不会被静默覆盖。
- 保留“复制启动命令（备用）”，供手动处理已被外部修改的配置。
- 普通更新允许指定位置的小型 `steam_appid.txt`，内容必须匹配 `4732690`；兼容 BOM、首尾空白和 NUL，保留原字节。正常移除 BE 配置也保留该非 BE 创建的辅助文件及非空目录。
- 用户显式应用配置遇到其他文件冲突时，显示完整 Steam 目录、ACF 路径、文件数量及大小，默认选择取消。只有点击“删除并重新配置”才执行确认范围内的清理。执行前复核路径和内容；拒绝国服/BE/配置目录重叠、目录链接和确认后变化。自动元数据更新不会进入强制清理流程。

`steam_appid.txt` 为 Steamworks SDK 提供 AppID 提示，常用于开发和外部启动；从 Steam 启动时客户端已提供 AppID，不应将它描述为所有 Steam 游戏必然生成的文件。本机文件的具体写入进程未被直接跟踪。参见 [Valve Steamworks API 文档](https://partner.steamgames.com/doc/sdk/api?language=english)。

## 控制台窗口

XInput 模式的 Launcher 负责创建游戏、等待游戏退出和清理启动辅助进程，本身不负责注入。Injector 模式由 Injector 执行注入，完成后同样等待游戏退出。

两个 helper 识别已有的 `--steam-command` 标记和新的 `--hide-console` 开关。BE 的统一启动参数自动添加 `--hide-console`，因此 BE 自身的 Injector/XInput 启动、生成的快捷方式和 Steam 入口均隐藏常驻控制台。保留 helper 进程、等待逻辑及退出状态；隐藏模式失败通过弹窗报告。普通命令行不传隐藏开关时保留控制台诊断。由于仍使用 console 子系统，启动瞬间可能短暂闪窗；修复目标是消除常驻可见窗口。

## 验证边界

定向检查覆盖隔离 Steam 配置写入/恢复、账号选择、运行状态和文件变更、未知字段保留、命令转义及中断恢复。发布态另验证混淆后的 JSON 收据和隔离页面。

2026-10-15 最终验证结果：SteamIntegration 204 项通过；OfficialLauncherTests 34 项通过，包含真实隔离子进程的控制台脱离、CLI 控制台保留、等待完成和退出码验证。主程序 Release 混淆发布通过；实际单文件解压程序集的 8 类 JSON 检查通过；标准和 760×700 窗口的页面检查通过，清理确认默认取消、取消后目录保持不变。针对本机真实 Steam 文件仅调用只读检查，账号解析、配置预检以及实际含 9 字节 `steam_appid.txt` 目录的归属验证成功。

混淆脚本的“输出确已转换”检查改用逐字节比较；仍校验程序集发生变化，本次没有额外计算产物哈希。

本次不将服务测试或 UI 预览等同于真实 Steam 覆盖层、时长和完整国服启动验收。测试结果及补丁文件保存在 `build/steam-auto-launch` 与 `build/steam-console-fix`；正式 4.0.3 发布附件不覆盖。
