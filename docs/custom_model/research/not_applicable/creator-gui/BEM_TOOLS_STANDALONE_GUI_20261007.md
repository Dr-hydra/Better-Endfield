# BEM Tools 独立 GUI

工具版本：1.5.2；主程序版本保持 3.5.2；BEM 格式保持 1.0–1.4。

独立 ZIP 根目录新增 `BetterEndfield.BemTools.exe`，与已有 CLI 并列。
GUI 使用 self-contained WinUI 发布，不要求安装 Better Endfield、Python 或 .NET。
`ui/BetterEndfield.BemTools` 链接现有创作者窗口、报告呈现、工程与语言代码，
不启动主程序、加载游戏模块或读取主程序配置。

`BemToolService` 从游戏模型管理服务中拆出，同时识别主程序的
`tools/BemConverter/` 布局和独立 ZIP 的平铺布局。CLI 参数通过 ArgumentList
传递，工作目录固定到后端所在目录。独立程序可接受一个 `.bemproj.json` 路径。

`BuildBemTools.ps1` 构建 GUI 和冻结 CLI，再通过 `package_toolchain.py`
组合成同一个 ZIP。GUI 不复制回 CLI staging 目录，因此主程序构建不会再嵌入
一份独立 GUI。包中新增中英 README，中英文指南和 Skill 引用同步。

## 验证

- 独立工具完整构建成功；GUI 和 CLI 都为 1.5.2。
- 主程序 Release 编译成功，0 警告、0 错误。
- `test_bem14*.py`：11 项通过。
- 模型设置测试：28 项通过。旧测试夹具补全了 BEM 1.0 必需的 world/ui 资源身份；未修改运行时校验规则。
- 从最终 ZIP 解压到中文路径，使用 Windows UI Automation 启动真实 GUI：打开复制的导出工程，点击导出，成功生成 BEM 1.4，并通过 Android 武器资源的 CLI 校验。
- 实际打开随包帮助文档；从不同工作目录运行成功；最终包不依赖源码目录。

复现入口：`ui/tests/BemToolsPortable/Smoke.ps1`，使用 Windows PowerShell 执行，
传入解压后的 `ToolDirectory` 和独立 `WorkDirectory`。测试只关闭自己创建的 GUI 进程。

本地日志与结果位于 `build/bem-tools-gui/`。默认不计算产物哈希。
