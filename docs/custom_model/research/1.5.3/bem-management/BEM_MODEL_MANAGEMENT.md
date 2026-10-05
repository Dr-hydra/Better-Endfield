# BEM 模型管理局部修正（2026-10-03）

基线：`main dd7131b4`。已读取 `android/AGENTS.md`。仅修改本任务负责文件，无提交、分支、全量复制、clean、目录删除或游戏启动。

## 修改与代码证据

- PC：`CustomModelPage.xaml(.cs)` 增加“按角色筛选”“关闭全部模型”。筛选选项来自完整 `Packages` 的角色 ID，名称复用 `PresetOptions.GetCharacterName`；切换只更新卡片 `Visibility`，不重建卡片、不保存配置、不改变启用状态，当前展开项及未应用滑条保留。刷新、导入和语言变化后更新角色列表；所选角色消失时回到全部角色。
- PC：`BemPackageService.DisableAllAsync()` 遍历完整 `Packages`，只设置 `Enabled=false`，一次调用现有 `SaveAsync()` 原子替换 `runtime.ini`。外观、组件、滑条、遗留滑条记忆、实验开关和独立 LOD 设置继续由原保存流程处理；失败时页面复用 `Reload()` 恢复磁盘快照。
- 安卓：`BemInstallPage.java` 在包列表前创建管理控件；角色名称复用现有 `character-names.json`，未知角色显示 ID。筛选只更新卡片可见性，保存于 Activity 的 Bundle，不写包索引；刷新后保留筛选，所选角色消失时回到全部角色。
- 安卓：`BemInstaller.disableAll()` 在同步锁内读取完整安装索引，为每个 generation 生成仅含 `enabled=false` 的变更，一次调用现有 `saveAll()`。继续使用 `commitIndex()` 的磁盘快照、SharedPreferences 提交及失败恢复；导入、转换、移除忙碌时拒绝修改。
- 两端“关闭全部”按钮的可用状态检查完整安装列表；即使筛选到全部已关闭的角色，其他角色存在启用包时仍可关闭全部。
- 热切换证据：PC 的 `native/modules/custom_model/module.cpp::ReloadRegistryAtDelivery()` 读取相同的 `runtime.ini`；安卓 `BemHotSwitchUpdater` 读取相同的 `BemInstaller.INDEX` 并走现有事务。因此无需修改热切换更新器，也未新增另一套生效流程。
- 本功能仅新增必要标题和操作结果。PC 字符串位于 `BemText.cs`（中英）；安卓新增独立 `values{,-en,-ja,-ko,-zh-rTW}/model_management.xml`，未修改 `overlay_settings.xml`、ModuleSettings、GameOverlay、EnhancementSettingsActivity。

## 已完成检查

- C 盘临时 .NET 9 控制台直接链接实际 `BemPackageService.cs`、`BemText.cs`，仅替代设置目录和语言服务。三个包、两个角色、BEM 1.0/1.1/1.3 元数据夹具通过：全部关闭前后 INI 除启用标志外逐字一致；重新加载全部停用；外观、组件、滑条及已移除滑条记忆保留；实验和独立 LOD 设置保留；重复关闭、空列表、文件被占用导致保存失败后的磁盘保留和 Reload 恢复通过。未调用导入或转换工具。
- Roslyn 检查修改的 C# 语法通过；XAML XML、筛选事件绑定和四条英文新增文案通过。该检查不代替 WinUI 生产编译。
- `aapt2 compile` 单独编译五份 `model_management.xml` 通过。
- `javac --release 17` 局部编译修改的两个 Java 文件及选项/参数依赖通过：Android API 37、原有其他类缓存；`FrameworkSettings` 和 `R` 使用临时编译声明。旧缓存缺少现有 `isConnected/awaitConnection`，故此检查只验证局部类型与语法，不等同生产依赖集成。
- 现有 `BemHotSwitchUpdateTest` 在主机 JVM 通过，覆盖配置变化、重试、被新选择覆盖的候选、全部关闭的空选择和滑条更新。
- 验证工具及小型夹具均位于 C 盘临时目录；未执行 PC/安卓全量构建。仓库要求 SDK 9.0.314，本机只有 9.0.308，因此临时检查从仓库外运行，未改 `global.json`。

## 实机边界

没有设备，不等待 ADB。两端控件实际排版、安卓批量持久化的设备行为，以及游戏内恢复原模型的具体时机未实机验证。热切换结论是现有代码链路证据；“点击后立刻改变当前已加载模型”不在已验证结论中。生产编译由主代理统一执行。
