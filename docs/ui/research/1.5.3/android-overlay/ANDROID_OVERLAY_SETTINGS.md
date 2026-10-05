# 安卓悬浮窗外观设置（2026-10-03）

基线：`main dd7131b4`。已读取 `android/AGENTS.md`，直接修改共享工作区；未提交、推送、创建分支、安装或启动游戏，未执行目录清理或全量复制。

## 最终行为

- 悬浮窗设置页新增“透明度”滑条（0–80%，默认 0）和“自动贴边”开关（默认关闭），只有控件标题与数值，无新增说明文字。沿用即时保存，分别写入 `overlay_transparency`（float 百分比）和 `overlay_auto_snap`（boolean），不重写其他配置。
- 透明度作用于整个悬浮窗宿主，包括图标与展开面板；面板展开动画仍使用自己的相对 alpha。保留至少 20% 不透明度，避免图标完全不可见。
- 自动贴边启用后的首次吸附，以及每次拖动结束，均收起面板并将 handle 变成 **24×40dp 窄标签**，图标缩为约 16dp。选择最近左右边，居中固定选右边；标签真正贴住 Insets 安全边，横向没有额外 8dp 留白。
- 点击窄标签恢复 **48×48dp 普通图标**并展开管理面板；开始实际拖动时也恢复普通尺寸，松手后再次吸附收小。手动收起展开面板时，自动贴边仍会恢复窄标签。
- 关闭自动贴边后恢复普通图标，当前位置按原安全区限制，可自由拖动；不会自动打开面板。归一化纵向位置继续保留。
- 展开面板和普通图标沿用原 8dp 边距、宽高上限；窄标签仅去掉横向边距。旋转、分屏、刘海、系统栏或输入法变化仍限制在安全区，极小/零尺寸窗口也不越界。旋转保留已展开面板，若发生于拖动中，则释放无效输入并吸附收小。
- 新字符串仅位于 `res/values/overlay_settings.xml` 及 `values-en`、`values-ja`、`values-ko`、`values-zh-rTW` 的同名文件，未改 `strings.xml`。

## 代码证据与整合点

- `ModuleSettings.java`：读取时和写入时限制透明度范围，NaN/无穷值回落到 0；旧安装没有新增 key 时保持原外观与自由拖动。
- `EnhancementSettingsActivity.java`：每次滑条/开关事件保存后直接调用现有预览实例的 `refreshAppearance()`。关闭的预览不会被刷新重新创建。
- `GameOverlay.java`：可见期间原有 500ms 刷新同时读取外观；透明度变化不重建面板。`docked` 控制窄标签/普通图标，吸附只隐藏面板、释放按住输入，不改变相机/冻结开关和进程级 `sessionDismissed`。点击展开不会被后续定时刷新重新收起。
- 必要的额外接线仅在 `XposedEntry.java` 的 `GameOverlay.install` 调用：原来供应 `OverlayFeatures`，现在供应可重新获取的 `SharedPreferences`。否则负责文件无法获取游戏侧新增外观配置。框架发布仍沿用 `FrameworkSettings`，未修改模型页或模型配置逻辑。
- `OverlayGeometry.java`：支持独立 handle 宽高、窄标签横向零边距及独立的 Insets 可用区域。`GameOverlay` 用实际可用区域判断屏幕变化，恢复普通样式不会误触发拖动取消。
- 本次行为澄清后的追加修改仅为 `GameOverlay.java`、`OverlayGeometry.java`、`OverlayGeometryTest.java` 和本文；保留透明度设置和已有接线，未修改任何模型 Java。

## 已执行验证与边界

- JDK 19 `javac --release 17` / `java`：100,007 个自由位置 + 100,007 个吸附位置 + 100,007 个窄标签位置，共 **301,021 个几何案例**通过。另验证左右边/居中/NaN、旋转、分屏、1–4 倍密度、极小/零窗口、实际 Insets 边零间隙及恢复普通样式后与原几何完全一致。
- SDK build-tools 36.0.0 `aapt2`：上述 5 份资源 XML 独立编译、链接通过。
- 最新 `GameOverlay.java` / `OverlayGeometry.java` 与当前 `ModuleSettings.java` 依赖用 SDK 37 和现有 debug 类目录完成局部 `javac` 编译，无生产构建。
- 所有验证产物写入 C 盘临时目录，未改动共享构建输出。目标文件 `git diff --check` 通过。
- 统一 APK 构建、重新安装与实机验收由主代理执行；本代理未等待设备或安装/启动游戏。共享配置传播、窄标签实际触摸、点击展开/拖动收小、横竖屏切换及“本次关闭”目前是代码证据，不能视为实机效果证明。
