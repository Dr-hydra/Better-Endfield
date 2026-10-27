package dev.betterendfield.next;

import java.util.HashMap;
import java.util.Locale;
import java.util.Map;

/**
 * English text for interface strings that are written in Java.
 *
 * <p>The module builds almost every view in code, and the in-game overlay runs
 * inside the game process where the module's own {@code R} table is not
 * available. Text that already lives in {@code res/values} is localized through
 * {@code values-en}; this class covers the literals that are assembled in code,
 * using the Chinese source text as the key (the same model Qt's {@code tr()}
 * uses). When the system language is not English, or a key has no translation,
 * the original text is returned unchanged, so behavior for existing users is
 * identical.</p>
 */
final class Text {
    private Text() {}

    private static final Map<String, String> ENGLISH = new HashMap<>(1024);

    static {
        overlay();
        install();
        mmd();
        app();
        characters();
        composer();
    }

    private static void composer() {
        put("外观组合", "Appearance composer");
        put("选择来源", "Select sources");
        put("选择至少两个已安装的包", "Select at least two installed packages");
        put("请选择 2 至 16 个来源包", "Select 2 to 16 source packages");
        put("组合名称", "Composition name");
        put("组合名称不能为空", "Composition name is required");
        put("我的外观组合", "My appearance composition");
        put("分析组合", "Analyze composition");
        put("正在分析组合…", "Analyzing composition\u2026");
        put("分析完成", "Analysis complete");
        put("重新分析", "Analyze again");
        put("预览与分区修正", "Preview and edit regions");
        put("保留原版 / 不加入", "Original / exclude");
        put("自定义分段", "Custom segments");
        put("展开细分", "Expand parts");
        put("收起细分", "Collapse parts");
        put("启用所选", "Enable selected");
        put("停用所选", "Disable selected");
        put("高亮所选", "Highlight selected");
        put("共用选项：", "Shared options: ");
        put("固定部分", "Fixed parts");
        put("展开绘制记录", "Expand draws");
        put("收起绘制记录", "Collapse draws");
        put("绘制段 ", "Draw ");
        put("启用", "Enable");
        put("停用", "Disable");
        put("已启用", "Enabled");
        put("已停用", "Disabled");
        put("部分启用", "Partially enabled");
        put("未提供", "Unavailable");
        put("未分类", "Unclassified");
        put(" 个三角形", " triangles");
        put("请先选择绘制段", "Select draw segments first");
        put("所选段的顶点布局不兼容，请分别查看", "Selected segments have incompatible vertex layouts. Inspect them separately.");
        put("不启用", "Disabled");
        put("切换选项", "Source options");
        put("重叠部分会同时绘制；不会自动裁剪或消除穿模。", "Overlapping parts render together. Clipping and intersections are not repaired automatically.");
        put("生成并安装组合", "Generate and install composition");
        put("请先开启保留本地副本", "Enable Keep local copies first");
        put("颈部", "Neck");
        put("头部", "Head");
        put("躯干", "Torso");
        put("骨盆", "Pelvis");
        put("左臂", "Left arm");
        put("右臂", "Right arm");
        put("左腿", "Left leg");
        put("右腿", "Right leg");
        put("返回组合", "Back to composer");
        put("预览包", "Preview package");
        put("预览包 ", "Preview package ");
        put("点面修正", "Edit faces");
        put("结束修正", "Finish editing");
        put("撤销", "Undo");
        put("自动分区", "Automatic regions");
        put("已应用到组合", "Applied to composition");
        put("选择", "Select ");
        put("正在生成组合…", "Generating composition\u2026");
        put("组合已安装", "Composition installed");
        put("组合安装未完成：", "Composition installation incomplete: ");
    }

    /** Source text in, display text out. */
    static String of(String chinese) {
        if (chinese == null || chinese.isEmpty()) return chinese;
        if (!Locale.getDefault().getLanguage().equals("en")) return chinese;
        String english = ENGLISH.get(chinese);
        return english == null ? chinese : english;
    }

    private static void put(String chinese, String english) { ENGLISH.put(chinese, english); }

    /** In-game overlay deck and the panel it embeds. */
    private static void overlay() {
        put("快捷控制，点按展开，拖动移动", "Quick controls. Tap to expand, drag to move");
        put("界面", "UI");
        put("自由镜头", "Free camera");
        put("冻结时间", "Freeze time");
        put("游戏视野", "Game FOV");
        put("模型管理", "Models");
        put("媒体屏", "Media screen");
        put("拖动标题移动悬浮窗", "Drag the title to move the overlay");
        put("完成", "Done");
        put("收起", "Collapse");
        put("本次关闭", "Hide for now");
        put("本次关闭悬浮窗，保留功能状态，游戏重启后可再次出现",
                "Hides the overlay for this session; feature state is kept and it returns after the game restarts");
        put("隐藏 / 恢复 HUD", "Hide / restore HUD");
        put("启用 / 退出自由镜头", "Enter / exit free camera");
        put("移动速度", "Movement Speed");
        put("自由镜头 FOV", "Free camera FOV");
        put("冻结 / 恢复时间", "Freeze / resume time");
        put("冻结时间可独立使用。关闭悬浮窗会保留冻结状态；请先点按恢复，或在设置中关闭后重启游戏。",
                "Time freeze works on its own. Closing the overlay keeps the frozen state; tap to resume first, or disable it in settings and restart the game.");
        put("尚未配置功能。请在增强设置中选择。", "No feature configured. Choose one in enhancement settings.");
        put("增强设置", "Enhancement settings");
        put("状态", "Status");
        put("界面控制", "UI control");
        put("MMD 控制台", "MMD console");
        put("会客室媒体屏", "Guest Room Media Screen");
        put("快捷控制", "Quick controls");
        put("，对应热键 ", ", hotkey ");
        put("等待 MMD 运行状态", "Waiting for MMD status");
        put("作品库", "Library");
        put("不选作品", "No work");
        put("MMD 播放进度", "MMD playback position");
        put("−5 秒", "−5 s");
        put("播放", "Play");
        put("+5 秒", "+5 s");
        put("停止", "Stop");
        put("循环", "Loop");
        put("镜头模式", "Camera mode");
        put("自由", "Free");
        put("游戏", "Game");
        put("机位 · 0 个", "Camera keys · 0");
        put("记录", "Record");
        put("清空", "Clear");
        put("保存", "Save");
        put("读取", "Load");
        put("运镜", "Camera move");
        put("自由视角", "Free Camera");
        put("暂停", "Pause");
        put("退出自由视角", "Exit free camera");
        put("MMD 指令被拒绝或队列已满", "MMD command rejected or queue full");
        put("MMD 控制桥不可用", "MMD control bridge unavailable");
        put("加载中", "Loading");
        put("播放中", "Playing");
        put("已暂停", "Paused");
        put("空闲", "Idle");
        put("预览模式", "Preview mode");
        put("选择作品 ", "Select work ");
        put("预览不读取作品库。", "Preview does not read the library.");
        put("还没有作品。请在管理器的 MMD 作品库中导入。",
                "No works yet. Import one in the manager's MMD library.");
        put("连接后显示已安装作品。", "Installed works appear once connected.");
        put("机位 · ", "Camera keys · ");
        put(" 个", "");
        put("\n地形跟随未启用 · ", "\nTerrain follow off · ");
        put("接口不可用，继续普通动作播放", "interface unavailable, continuing normal playback");
        put("正在加载", "Loading");
        put("开始播放", "Playback started");
        put("已停止", "Stopped");
        put("播放完成", "Playback finished");
        put("请先选择作品", "Select a work first");
        put("作品配置无法读取", "Work configuration unreadable");
        put("没有可播放的内容，请检查动作 / 镜头 / 音乐开关",
                "Nothing to play; check the motion / camera / music switches");
        put("动作加载失败", "Motion load failed");
        put("镜头加载失败", "Camera load failed");
        put("音乐加载失败", "Music load failed");
        put("音乐模块未加载", "Music module not loaded");
        put("自由镜头未启用", "Free camera not enabled");
        put("角色动作未启用", "Character motion not enabled");
        put("已切换镜头模式", "Camera mode switched");
        put("已选择作品", "Work selected");
        put("已切换循环", "Loop toggled");
        put("升", "Up");
        put("降", "Down");
        put("慢", "Slow");
        put("快", "Fast");
        put("划动转向\nLOOK", "Swipe to look\nLOOK");
        put("左滚", "Roll left");
        put("复位", "Reset view");
        put("右滚", "Roll right");
        put("收起关键帧", "Hide keyframes");
        put("关键帧热键", "Keyframe hotkeys");
        put("添加帧", "Add key");
        put("播放帧", "Play keys");
        put("清空帧", "Clear keys");
        put("保存机位", "Save keyframes");
        put("读取机位", "Load keyframes");
        put("停", "Stop");
        put("，按住生效", ", hold to apply");
        put("预览模式 · 不发送任何游戏指令", "Preview mode · no game commands are sent");
        put("控制桥绑定失败 · 请查看日志", "Control bridge binding failed · check the log");
        put("请先启用自由镜头功能", "Enable the free camera feature first");
        put("镜头指令未接受，请查看运行状态", "Camera command not accepted; check runtime status");
        put("开始 / 恢复原画", "Start / resume original");
        put("暂停 / 继续", "Pause / resume");
        put("媒体屏模块未就绪", "Media screen module not ready");
        put("媒体屏控制不可用", "Media screen control unavailable");
        put("跳转秒数", "Seek seconds");
        put("跳转", "Seek");
        put("跳转未执行", "Seek not performed");
        put("请输入有效秒数", "Enter a valid number of seconds");
        put("播放结束", "Playback ended");
        put("操作失败：", "Failed: ");
        put("控制不可用", "Control unavailable");
        put("输入队列已满或键位无效", "Input queue full or invalid key");
        put("控制桥绑定失败，请查看运行日志", "Control bridge binding failed; check the runtime log");
        put("预览模式，不读取游戏运行状态。", "Preview mode: game runtime state is not read.");
        put("\n设置只表示配置意图。修改模块开关后需要重新启动游戏。",
                "\nSettings only express intent. Restart the game after changing module switches.");
        put("运行状态", "Runtime status");
        put("关闭", "Close");
        put("无法打开增强设置", "Unable to open enhancement settings");
        put("热切换：已启用", "Hot switch: on");
        put("热切换：未启用", "Hot switch: off");
        put("设置桥不可用", "Settings bridge unavailable");
        put("设置未保存", "Settings not saved");
        put("全局 FOV", "Global FOV");
        put("全局 FOV  ", "Global FOV  ");
        put("全局 FOV（预览）", "Global FOV (preview)");
        put("全局 FOV（运行时未加载）", "Global FOV (runtime not loaded)");
        put("全局 FOV（重启后生效）", "Global FOV (applies after restart)");
        put("全部模型 ▾", "All models ▾");
        put("全部模型", "All models");
        put("角色或武器筛选", "Filter by character or weapon");
        put("关闭全部", "Disable all");
        put("无已安装模型", "No models installed");
        put("模型列表格式不可用", "Model list format unusable");
        put("重试读取", "Retry");
        put("启用（热切换）", "Enabled (hot switch)");
        put("启用（重启后生效）", "Enabled (after restart)");
        put("收起详细选项 ▴", "Hide options ▴");
        put("详细选项 ▾", "Options ▾");
        put("外观", "Appearance");
        put("作者默认值", "Author defaults");
        put("动效", "Animation");
        put("重播", "Replay");
        put("开始", "Start");
        put("继续", "Resume");
        put("速度  ", "Speed  ");
        put(" 速度", " speed");
        put("已停止 / 结束", "Stopped / ended");
        put("模型列表读取失败：", "Failed to read the model list: ");
        put("正在读取模型列表", "Reading the model list");
        put("设置桥返回的数据不完整", "Incomplete data from the settings bridge");
        put("模型列表超出桥接大小限制", "Model list exceeds the bridge size limit");
        put("设置已被更新，请重新选择", "Settings were updated; choose again");
        put("无效的设置请求", "Invalid settings request");
        put("无效的设置字段", "Invalid settings field");
        put("缺少设置字段", "Missing settings field");
        put("FOV 超出范围", "FOV out of range");
        put("模型包列表已更新，请重新选择", "The model package list was updated; choose again");
        put("选项类型与模型包不匹配", "Option type does not match the model package");
        put("等待运行时连接", "Waiting for the runtime connection");
        put("部分模块启动失败 · 长按查看", "Some modules failed to start · long-press for details");
        put("模块准备中 · 长按查看", "Modules are starting · long-press for details");
        put("时间已冻结 · 再次点按恢复", "Time frozen · tap again to resume");
        put("自由镜头 · 按住移动，划动转向", "Free camera · hold to move, swipe to look");
        put("已连接 · 点按切换，按住持续", "Connected · tap to toggle, hold to sustain");
        put("等待游戏首帧", "Waiting for the game's first frame");
        put("Unity 首帧入口不可用", "Unity first-frame entry point unavailable");
        put("运行时载入失败：", "Runtime load failed: ");
        put("等待框架服务连接已中断", "Waiting for the framework service was interrupted");
        put("设置桥授权检查未通过，请重试", "Settings bridge authorization check failed; try again");
    }

    /** BEM installer, its page and the archive reader. */
    private static void install() {
        put("原样导入 BEM 包；贴图异常时，可在对应模型包下手动转换手机纹理。",
                "Imports BEM packages as-is. If textures look wrong, convert the mobile texture format from the model package card.");
        put("正在转换第 ", "Converting texture ");
        put(" 张纹理 · mip ", " · mip ");
        put("正在取消，保留原安装版本…", "Cancelling; keeping the previously installed version…");
        put("已取消 ZIP 导入，原有模型包已保留。", "ZIP import cancelled; existing model packages kept.");
        put("已取消安装", "Installation cancelled");
        put("安装索引迁移保存失败", "Failed to save the migrated install index");
        put("安装索引损坏", "Install index corrupted");
        put("正在读取 BEM 或 ZIP…", "Reading BEM or ZIP…");
        put("无法创建安装目录", "Unable to create the install directory");
        put("无法创建临时目录", "Unable to create a temporary directory");
        put("无法打开包", "Unable to open the package");
        put("ZIP 中没有可导入的 BEM。\n", "No importable BEM was found in the ZIP.\n");
        put("ZIP 中没有通过校验的 BEM。\n", "No BEM in the ZIP passed validation.\n");
        put("ZIP 已校验，等待选择要导入的 ", "ZIP verified. Select the ");
        put(" 个模型包。", " model package(s) to import.");
        put("已原样导入：", "Imported as-is: ");
        put("。重启游戏后生效。", ". Restart the game to apply.");
        put("导入未完成：", "Import incomplete: ");
        put("无法启动导入任务，请重试。", "Unable to start the import task; try again.");
        put("正在校验 ", "Verifying ");
        put("无法启动 ZIP 导入，请重试。", "Unable to start the ZIP import; try again.");
        put("未选择模型包，已取消 ZIP 导入。", "No model package selected; ZIP import cancelled.");
        put("正在导入 ", "Importing ");
        put("已取消 ZIP 导入", "ZIP import cancelled");
        put("ZIP 导入未完成", "ZIP import incomplete");
        put("ZIP 导入完成", "ZIP import complete");
        put("：成功 ", ": imported ");
        put(" 个，未导入 ", ", skipped ");
        put("，无效或失败项 ", ", invalid or failed ");
        put(" 个。\n", ".\n");
        put("\n重启游戏后生效。", "\nRestart the game to apply.");
        put("正在等待框架服务连接…", "Waiting for the framework service…");
        put("安装结果发布失败", "Failed to publish the install result");
        put("安装索引保存失败", "Failed to save the install index");
        put("读取授权已失效，请重新选择文件或从文件管理器打开。",
                "Read permission expired; select the file again or open it from a file manager.");
        put("正在准备纹理转换…", "Preparing texture conversion…");
        put("正在读取并校验包（不转换纹理）…", "Reading and validating the package (no texture conversion)…");
        put("无效的模型包版本", "Invalid model package version");
        put("模型包已被替换，请刷新后重试", "The model package was replaced; refresh and try again");
        put("正在转换 ASTC 纹理；大包可能需要数分钟…",
                "Converting ASTC textures; large packages may take minutes…");
        put("设备未报告 ASTC，正在生成 RGBA32 资源…",
                "The device did not report ASTC; generating RGBA32 assets…");
        put("正在发布给游戏…", "Publishing to the game…");
        put("已转换：", "Converted: ");
        put("没有读取权限或临时授权已失效，请从文件管理器重新打开，或使用应用内文件选择器。",
                "No read permission or the temporary grant expired. Reopen from a file manager or use the in-app picker.");
        put("此包缺少已确认的法线贴图编码信息，暂时无法转换；可继续使用原包。",
                "This package lacks confirmed normal-map encoding, so conversion is unavailable; the original package still works.");
        put("转换未完成，原包和启用状态已保留：",
                "Conversion incomplete; the original package and its enabled state were kept: ");
        put("模型包已被替换，原配置保持不变", "The model package was replaced; configuration unchanged");
        put("同一个包 ID 不能更新为另一目标", "A package ID cannot be updated to a different target");
        put("转换结果的目标不匹配", "Conversion result target mismatch");
        put("转换结果的资源目标不匹配", "Conversion result resource target mismatch");
        put("模型包版本重复", "Duplicate model package version");
        put("安装索引校验失败", "Install index validation failed");
        put("安装索引磁盘校验失败", "Install index disk verification failed");
        put("请等待当前操作完成，或取消后再移除", "Wait for the current operation or cancel it before removing");
        put("模型包已被移除或更新，请刷新后重试",
                "The model package was removed or updated; refresh and try again");
        put("移除配置保存失败", "Failed to save the removal configuration");
        put("已从列表移除，正在清理安装文件…", "Removed from the list; cleaning up installed files…");
        put("已移除：", "Removed: ");
        put("。其他包已保留，重启游戏后生效。",
                ". Other packages were kept; restart the game to apply.");
        put("部分残留文件未能清理，但该包已停用。",
                "Some leftover files could not be cleaned up, but the package is disabled.");
        put("模型包已移除；部分文件清理失败：",
                "Model package removed; some files could not be cleaned up: ");
        put("无效的安装目录", "Invalid install directory");
        put("正在清理未使用数据…", "Cleaning up unused data…");
        put("已清理 ", "Freed ");
        put("。游戏目录中未启用的模型会在下次启动游戏时清理。",
                ". Disabled models in the game directory are cleaned up on the next launch.");
        put("清理未完成：", "Cleanup incomplete: ");
        put("无法启动清理任务，请重试。", "Unable to start the cleanup task; try again.");
        put("正在恢复本地副本…", "Restoring local copies…");
        put("正在移除本地副本…", "Removing local copies…");
        put("本地副本写入失败", "Failed to write the local copy");
        put("已恢复本地副本 ", "Restored local copies ");
        put("已移除本地副本 ", "Removed local copies ");
        put("框架中缺少文件，需重新导入：", "Files are missing from the framework; re-import required: ");
        put("框架中缺少文件，已保留本地副本：", "Files are missing from the framework; local copies kept: ");
        put("恢复本地副本未完成：", "Restoring local copies incomplete: ");
        put("移除本地副本未完成：", "Removing local copies incomplete: ");
        put("无法启动任务，请重试。", "Unable to start the task; try again.");
        put("文件超过大小限制", "File exceeds the size limit");
        put("正在准备转换为 ASTC 格式…", "Preparing the ASTC conversion…");
        put("正在转换为 ASTC 格式并流压缩…", "Converting to ASTC with stream compression…");
        put("保存 ASTC 转换索引失败", "Failed to save the ASTC conversion index");
        put("」已成功转换为 ASTC 格式。重启游戏后生效。",
                "\" was converted to ASTC. Restart the game to apply.");
        put("无法替换转换后的模型包", "Unable to replace the converted model package");
        put("ASTC 转换未完成：", "ASTC conversion incomplete: ");
        put("正在扫描已安装模型包…", "Scanning installed model packages…");
        put("正在检查并修复法线 (", "Checking and repairing normals (");
        put("保存法线修复索引失败", "Failed to save the normal repair index");
        put("法线修复完成：共扫描 ", "Normal repair complete: scanned ");
        put(" 个模型包，更新了 ", " model package(s), updated ");
        put(" 个包。重启游戏后生效。", " package(s). Restart the game to apply.");
        put("法线修复未完成：", "Normal repair incomplete: ");
        put("请等待当前操作完成后修改", "Wait for the current operation to finish");
        put("保存失败", "Save failed");
        put("正在处理", "Working");
        put("当前进度：", "Progress: ");
        put(" · 已用 ", " · elapsed ");
        put("分", "m ");
        put("秒", "s");
        put("存储选项保存失败", "Failed to save the storage option");
        put("实验选项保存失败", "Failed to save the experimental option");
        put("修复所有法线", "Repair all normals");
        put("无法打开模型链接", "Unable to open the model link");
        put("确定", "OK");
        put("暂无模型包", "No model packages yet");
        put("武器 · ", "Weapon · ");
        put("组件 / 形态滑条选项 ▾", "Component / shape options ▾");
        put("详细组件 / 外观选项 ▾", "Detailed component / appearance options ▾");
        put("启动外观", "Startup appearance");
        put("已转为 ASTC", "ASTC applied");
        put("转为 ASTC 格式", "Convert to ASTC");
        put("将「", "Convert \"");
        put("」转为 ASTC？", "\" to ASTC?");
        put("将扫描模型包内所有纹理并重编码为移动端原生 ASTC 格式（自动补全法线 Z 轴并启用流压缩）。用于解决部分机型贴图黑屏、粉屏或显存过大问题。",
                "Re-encodes every texture in the package to the mobile-native ASTC format (normals get a reconstructed Z axis and stream compression is enabled). Use it when some devices show black or pink textures or run out of video memory.");
        put("开始转换", "Start conversion");
        put("预览", "Preview");
        put("模型预览", "Model preview");
        put("无法打开预览：", "Could not open preview: ");
        put("模型包已移除，请关闭预览。", "This package was removed. Close the preview.");
        put("源包已更新，请重新打开预览。", "The source package changed. Reopen the preview.");
        put("设置已保存", "Settings saved");
        put("预览进程已退出，请重新打开。", "The preview process exited. Reopen the preview.");
        put("移除模型包", "Remove model package");
        put("移除「", "Remove \"");
        put("」？", "\"?");
        put("将删除此模型包的安装文件和配置。下载目录中的原始 BEM 文件会保留，可重新导入。游戏内已加载的模型需重启游戏后恢复。",
                "Deletes this package's installed files and configuration. The original BEM in your download folder is kept and can be imported again. Models already loaded in the game return after a restart.");
        put("移除", "Remove");
        put("安装列表读取失败：", "Failed to read the install list: ");
        put("设置已保存；已启用实验热切换的游戏将在下次切换配队或重新打开详情时更新。首次开启需重启游戏。",
                "Settings saved. With the experimental hot switch on, the game updates on the next team switch or detail reopen. The first enable requires a game restart.");
        put("设置已保存，重启游戏后生效。资源目标重叠的其他包已自动停用。",
                "Settings saved; restart the game to apply. Other packages that share the same resource target were disabled automatically.");
        put("实验设置已保存，重启游戏后生效。",
                "Experimental settings saved; restart the game to apply.");
        put("设置未保存：", "Settings not saved: ");
        put("：", ": ");
        put("（", " (");
        put("，步长 ", ", step ");
        put("，默认 ", ", default ");
        put("，原形 ", ", neutral ");
        put("）", ")");
        put("滑条读取失败", "Failed to read the slider");
        put("调整后点击应用。隐藏的滑条按原形生效，并保留已保存数值。开启实验热切换后，下次切换配队或打开详情时更新；否则重启游戏生效。",
                "Adjust the sliders, then tap apply. Hidden sliders use their neutral value and keep the saved number. With the experimental hot switch on, changes apply on the next team switch or detail reopen; otherwise restart the game.");
        put("应用滑条", "Apply sliders");
        put("无法打开此文件，请重新选择 BEM 包。",
                "Unable to open this file; select the BEM package again.");
        put("已有导入、转换或移除任务正在进行。此文件尚未导入；任务结束后可点下方按钮继续。再次打开文件会替换这条待处理请求。",
                "An import, conversion or removal is already running. This file has not been imported yet; use the button below when the task finishes. Opening another file replaces this pending request.");
        put("无法开始导入，请重新选择 BEM 包。",
                "Unable to start the import; select the BEM package again.");
        put("以下项未通过校验：\n", "These items failed validation:\n");
        put("导入 ZIP 中的模型包", "Import model packages from the ZIP");
        put("导入所选", "Import selected");
        put("取消", "Cancel");
        put("无法开始 ZIP 导入：", "Unable to start the ZIP import: ");
        put("选择 BEM 或 ZIP", "Select a BEM or ZIP");
        put("请选择 BEM 文件或包含 BEM 的 ZIP；修改扩展名不能转换格式。",
                "Select a BEM file or a ZIP that contains BEM packages; renaming a file does not convert it.");
        put("正在读取 ZIP 合集…", "Reading the ZIP archive…");
        put("ZIP 超过 8 GiB 大小上限。", "The ZIP exceeds the 8 GiB limit.");
        put("ZIP 文件数超过 4096。", "The ZIP has more than 4096 files.");
        put("ZIP 中的 BEM 数量超过 ", "The ZIP contains more than ");
        put(" 个。", " BEM packages.");
        put("ZIP 中存在重复文件路径。", "The ZIP contains duplicate file paths.");
        put("不支持此 ZIP 条目的压缩方式，请使用存储或 Deflate。",
                "This ZIP entry uses an unsupported compression method; use stored or Deflate.");
        put("ZIP 内 BEM 超过 2 GiB 大小上限。", "A BEM inside the ZIP exceeds the 2 GiB limit.");
        put("ZIP 内 BEM 的总大小超过导入上限。",
                "The total size of the BEM packages in the ZIP exceeds the import limit.");
        put("正在读取：", "Reading: ");
        put("ZIP 条目长度或 CRC 校验失败。", "A ZIP entry failed its length or CRC check.");
        put("ZIP 目录记录数量不匹配。", "The ZIP directory entry count does not match.");
        put("ZIP 中没有找到 BEM 文件。", "No BEM file was found in the ZIP.");
        put("正在校验 ZIP 单模型包…", "Validating the single-package ZIP…");
        put("ZIP 中的 manifest.json 不是 BEM 模型清单。",
                "The manifest.json in the ZIP is not a BEM manifest.");
        put("ZIP 单模型包缺少 payload ", "The single-package ZIP is missing payload ");
        put("ZIP 内 payload 超过 2 GiB 大小上限。", "The payload inside the ZIP exceeds the 2 GiB limit.");
        put("ZIP 内 payload 的总大小超过导入上限。",
                "The total payload size in the ZIP exceeds the import limit.");
        put("正在写入：", "Writing: ");
        put("ZIP 条目超过大小上限。", "A ZIP entry exceeds the size limit.");
        put("manifest.json 超过 4 MiB 上限。", "manifest.json exceeds the 4 MiB limit.");
        put("manifest.json 不是合法 JSON。", "manifest.json is not valid JSON.");
        put("无法创建模型包临时目录。", "Unable to create the model package staging directory.");
        put("ZIP 条目路径不合法。", "A ZIP entry path is invalid.");
        put("ZIP 目录层级超过 32。", "ZIP directory depth exceeds 32.");
        put("ZIP 条目文件名不合法。", "A ZIP entry file name is invalid.");
        put("ZIP 文件损坏、加密或编码不支持。", "The ZIP is damaged, encrypted, or uses an unsupported encoding.");
        put("ZIP 长度不合法。", "Invalid ZIP length.");
        put("ZIP 目录缺失或不完整。", "The ZIP directory is missing or incomplete.");
        put("不支持分卷 ZIP。", "Multi-volume ZIPs are not supported.");
        put("ZIP64 目录缺失。", "The ZIP64 directory is missing.");
        put("ZIP64 目录不合法。", "Invalid ZIP64 directory.");
        put("ZIP 文件数超过 4096 或目录不合法。", "The ZIP has more than 4096 files or an invalid directory.");
        put("ZIP 目录长度不合法。", "Invalid ZIP directory length.");
        put("ZIP 目录信息超过大小上限。", "ZIP directory information exceeds the size limit.");
        put("ZIP 目录记录不完整。", "The ZIP directory record is incomplete.");
        put("ZIP 目录记录不合法。", "Invalid ZIP directory record.");
        put("ZIP 目录记录长度不合法。", "Invalid ZIP directory record length.");
    }

    /** MMD library, import and VMD handling. */
    private static void mmd() {
        put("作品索引损坏；未覆盖已有作品", "The work index is corrupted; existing works were left untouched");
        put("导入失败：", "Import failed: ");
        put("本地作品文件缺失或已变化：", "A local work file is missing or changed: ");
        put("正在发布 ", "Publishing ");
        put("框架服务未连接或发布失败，请启用模块后重试",
                "The framework service is not connected or publishing failed; enable the module and try again");
        put("作品已重新发布", "Work republished");
        put("作品索引保存失败", "Failed to save the work index");
        put("作品已移除", "Work removed");
        put("部分文件暂未清理，但该作品已停止发布。",
                "Some files could not be cleaned up yet, but the work is no longer published.");
        put("正在准备作品文件…", "Preparing work files…");
        put("操作未完成：", "Operation incomplete: ");
        put("目录读取授权失效，请重新选择。", "Directory access expired; select it again.");
        put("作品库最多 512 个作品", "The library holds at most 512 works");
        put("无法创建作品库目录", "Unable to create the library directory");
        put("无法创建临时作品目录", "Unable to create the temporary work directory");
        put("复制：", "Copying: ");
        put("资料长度异常：", "Asset length mismatch: ");
        put("VMD 类型不符", "VMD type mismatch");
        put("正在发布…", "Publishing…");
        put("无法发布本地作品目录", "Unable to publish the local work directory");
        put("发布：", "Publishing: ");
        put("框架服务未连接或发布失败", "The framework service is not connected or publishing failed");
        put("正在完成…", "Finishing…");
        put("已导入：", "Imported: ");
        put("资源必须是作品目录内的单层文件名",
                "Assets must be single-level file names inside the work directory");
        put("不支持子目录或绝对路径：", "Subdirectories and absolute paths are not supported: ");
        put("作品文件超过大小限制", "A work file exceeds the size limit");
        put("作品已被移除，请刷新后重试", "The work was removed; refresh and try again");
        put("无效的作品目录编号", "Invalid work directory id");
        put("作品目录超出应用存储范围", "The work directory is outside the app storage");
        put("文件数超过 4096", "More than 4096 files");
        put("重复路径：", "Duplicate path: ");
        put("缺少文件名", "Missing file name");
        put("无效资源路径", "Invalid asset path");
        put("目录层级超过 16", "Directory depth exceeds 16");
        put("无效资源路径：", "Invalid asset path: ");
        put("无效文件名", "Invalid file name");
        put("资源路径越界", "Asset path escapes the archive root");
        put("不支持 RAR，请使用 ZIP 或 7z", "RAR is not supported; use ZIP or 7z");
        put("请选择 ZIP 或 7z 压缩包", "Select a ZIP or 7z archive");
        put("解压：", "Extracting: ");
        put("不支持加密压缩包", "Encrypted archives are not supported");
        put("不支持 Zstandard ZIP 条目：", "Unsupported Zstandard ZIP entry: ");
        put("不支持的 ZIP 条目：", "Unsupported ZIP entry: ");
        put("目录条目长度异常", "Directory entry length mismatch");
        put("ZIP 校验失败：", "ZIP validation failed: ");
        put("ZIP 长度异常", "ZIP length mismatch");
        put("ZIP 目录缺失", "ZIP directory missing");
        put("ZIP64 长度异常", "ZIP64 length mismatch");
        put("ZIP64 目录异常", "ZIP64 directory invalid");
        put("ZIP 目录长度或文件数超限", "ZIP directory length or file count exceeds the limit");
        put("ZIP 目录截断", "ZIP directory truncated");
        put("ZIP 签名长度异常", "ZIP signature length mismatch");
        put("ZIP 目录条目异常或过多", "ZIP directory entries are invalid or too many");
        put("ZIP 条目长度异常", "ZIP entry length mismatch");
        put("ZIP 文件数声明异常", "ZIP file count declaration is invalid");
        put("不支持 7z 删除条目", "7z deletion entries are not supported");
        put("不支持符号链接", "Symbolic links are not supported");
        put("资源长度超限：", "Asset exceeds the length limit: ");
        put("无法创建缓存目录", "Unable to create the cache directory");
        put("资料缓存失效，请重新选择", "The asset cache expired; select again");
        put("已取消", "Cancelled");
        put("请选择文件或目录", "Select a file or directory");
        put("正在准备…", "Preparing…");
        put("正在识别…", "Inspecting…");
        put("所选文件不含该槽需要的数据", "The selected file has no data for this slot");
        put("无法保存资料缓存", "Unable to save the asset cache");
        put("无法替换资料缓存", "Unable to replace the asset cache");
        put("准备失败：", "Preparation failed: ");
        put("读取授权失效，请重新选择", "Read permission expired; select again");
        put("目录层级异常", "Directory depth invalid");
        put("无法列出目录", "Unable to list the directory");
        put("正在读取目录…", "Reading the directory…");
        put("无效目录文件名", "Invalid directory file name");
        put("无法读取文件", "Unable to read the file");
        put("资源长度异常", "Asset length mismatch");
        put("选择缓存写入失败", "Failed to write the selection cache");
        put("无法写入选择缓存", "Unable to write the selection cache");
        put("选择缓存过大", "The selection cache is too large");
        put("选择缓存异常", "The selection cache is invalid");
        put(" · 音乐", " · Music");
        put(" 引用缺失或类型不符：", " reference missing or wrong type: ");
        put("识别作品超过 512 个", "More than 512 works detected");
        put("请输入作品名（最多 120 字）", "Enter a work name (up to 120 characters)");
        put("资源类型不符：", "Asset type mismatch: ");
        put("资源缺失或长度异常：", "Asset missing or length mismatch: ");
        put("作品超过 1 GiB", "The work exceeds 1 GiB");
        put("set.ini 过大", "set.ini is too large");
        put("仅支持动作/表情 1–4", "Only motion/expression slots 1–4 are supported");
        put("重复字段：", "Duplicate field: ");
        put("请选择动作或镜头", "Select a motion or camera file");
        put("缺少配置值", "Missing configuration value");
        put("配置值包含控制字符", "The configuration value contains control characters");
        put("目录层级过深", "Directory depth too deep");
        put("无法读取缓存资料", "Unable to read the cached asset");
        put("资料长度超限：", "Asset exceeds the length limit: ");
        put("VMD 超过 64 MiB：", "VMD exceeds 64 MiB: ");
        put("无效 VMD：", "Invalid VMD: ");
        put("VMD IK 长度异常", "VMD IK length mismatch");
        put("VMD 尾部长度异常", "VMD trailer length mismatch");
        put("VMD 没有动作、表情或镜头", "The VMD has no motion, expression or camera data");
        put("VMD 节长度异常", "VMD section length mismatch");
        put("动作 ", "Motion ");
        put("表情 ", "Face ");
        put("‹ 返回相机设置", "‹ Back to camera settings");
        put("作品结束后重新播放。", "Replay the work when it ends.");
        put("作品需在 set.ini 中配置 music 文件。音频会叠加游戏背景音乐，建议先关闭游戏 BGM。",
                "The work needs a music file configured in set.ini. Audio is mixed on top of the game soundtrack, so turn the game BGM down first.");
        put("Orbit · 环绕", "Orbit");
        put("DollyZoom · 滑动变焦", "Dolly zoom");
        put("Crane · 升降", "Crane");
        put("Truck · 平移", "Truck");
        put("导入与管理", "Import and manage");
        put("范围 ", "Range ");
        put("；保留上次有效值", "; keeping the last valid value");
        put("请等待当前作品操作完成", "Wait for the current work operation to finish");
        put("暂无作品", "No works yet");
        put("无法读取作品库：", "Unable to read the library: ");
        put("操作失败", "Operation failed");
        put("导入 MMD", "Import MMD");
        put("选择 ZIP / 7z", "Select ZIP / 7z");
        put("选择目录", "Select directory");
        put("作品", "Work");
        put("作品名", "Work name");
        put("选择文件", "Select file");
        put("导入", "Import");
        put("请等待当前操作完成", "Wait for the current operation to finish");
        put("文件选择器不可用", "File picker unavailable");
        put("选择外部文件", "Select an external file");
        put("请重新选择文件", "Select the file again");
        put("镜头", "Camera");
        put("音乐", "Music");
        put("动作", "Motion");
        put("表情", "Face");
        put("导入 MMD 作品", "Import MMD work");
        put("：成功 ", ": imported ");
    }

    /** Main window, enhancement pages, media screen and about page. */
    private static void app() {
        put("：", ": ");
        put("。", ". ");
        put("、", ", ");
        put("；", "; ");
        put("（", " (");
        put("）", ")");
        put(")：", "): ");
        put("「", "\"");
        put("」", "\"");
        put("」？", "\"?");
        put("启用「", "Enable \"");
        put("无法打开创意工坊", "Unable to open the workshop");
        put("范围 0.05–20", "Range 0.05–20");
        put("模型缩放应为 0.05–20；保留上次有效值",
                "Model scale must be between 0.05 and 20; keeping the last valid value");
        put("循环区间或混合时长无效；保留上次有效值",
                "Invalid loop range or crossfade duration; keeping the last valid values");
        put("主题色应为 #RRGGBB；保留上次有效值",
                "Theme color must be #RRGGBB; keeping the last valid value");
        put("ZIP64 目录缺失", "ZIP64 directory missing");
        put("不支持分卷 ZIP", "Multi-volume ZIPs are not supported");
        put("资源长度异常：", "Asset length mismatch: ");
        put("持续冲刺", "Sustained Dash");
        put("创意工坊", "Workshop");
        put("会客室媒体屏", "Guest Room Media Screen");
        put("水墨武器", "Ink weapon");
        put("水墨服装", "Ink outfit");
        put("摘面具形态", "Unmasked form");
        put("水墨剑光", "Ink sword light");
        put("发现新版本", "New Version Available");
        put("Better Endfield Next 有可用更新：\n\n当前版本：",
                "A Better Endfield Next update is available:\n\nCurrent version: ");
        put("\n最新版本：", "\nLatest version: ");
        put("稍后再说", "Later");
        put("跟随游戏", "Follow Game");
        put("中文", "Chinese");
        put("日本語", "Japanese");
        put("한국어", "Korean");
        put("、", ", ");
        put("‹ 返回", "‹ Back");
        put("功能状态", "Status");
        put("启用媒体屏", "Enable media screen");
        put("在帝江号会客室替换屏幕显示自选媒体内容",
                "Replace the reception room screens with your own media");
        put("媒体内容", "Media content");
        put("媒体来源类型", "Media source");
        put("单张图片", "Single image");
        put("图片轮播", "Image slideshow");
        put("视频文件", "Video file");
        put("导入媒体文件", "Import media file");
        put("未选择文件", "No file selected");
        put("播放视频声音", "Play video audio");
        put("启用视频内置音频流", "Use the video's built-in audio track");
        put("循环播放", "Loop playback");
        put("视频结束或图片轮播到末尾时自动重新循环",
                "Restart automatically when the video ends or the slideshow reaches the last image");
        put("轮播间隔（秒）", "Slide interval (seconds)");
        put("音量（0.0 - 2.0）", "Volume (0.0 - 2.0)");
        put("投屏与适配", "Screens and fitting");
        put("大屏投屏（墙面轮播屏）", "Wall screen (large slideshow display)");
        put("接管会客室主墙壁大显示屏", "Take over the main wall display in the reception room");
        put("小屏投屏（桌面电脑）", "Desk screen (desktop computer)");
        put("接管会客室桌面电脑显示屏", "Take over the desktop computer display in the reception room");
        put("小屏画面适配", "Desk screen fitting");
        put("完整显示（保持比例，留黑边）", "Fit (keep aspect ratio, letterbox)");
        put("裁剪填满（保持比例）", "Crop to fill (keep aspect ratio)");
        put("拉伸填满", "Stretch to fill");
        put("屏幕对象路径", "Screen object path");
        put("分辨率与控制", "Resolution and control");
        put("输出分辨率", "Output resolution");
        put("自定义", "Custom");
        put("画布宽度", "Canvas width");
        put("画布高度", "Canvas height");
        put("启停快捷键", "Toggle hotkey");
        put("暂停／继续快捷键", "Pause / resume hotkey");
        put("保存设置", "Save settings");
        put("媒体屏设置已保存，冷重启游戏后生效",
                "Media screen settings saved; they apply after a cold restart of the game");
        put("注意：修改设置后需彻底强制停止并重新启动游戏。",
                "Note: force-stop the game completely and restart it after changing settings.");
        put("设置值无效", "Invalid setting value");
        put("设置保存失败", "Failed to save settings");
        put("已选择 ", "Selected ");
        put(" 张轮播图片", " slideshow image(s)");
        put("未选择轮播图片", "No slideshow images selected");
        put("未选择媒体文件", "No media file selected");
        put("已就绪：", "Ready: ");
        put("快捷键无效", "Invalid hotkey");
        put("导入中...", "Importing...");
        put("媒体文件数量已达上限", "The media file limit has been reached");
        put("无法创建媒体目录", "Unable to create the media directory");
        put("请选择 PNG、JPEG 或 MP4 文件", "Select a PNG, JPEG or MP4 file");
        put("无法读取所选文件", "Unable to read the selected file");
        put("文件过大", "File too large");
        put("文件为空", "File is empty");
        put("媒体索引保存失败", "Failed to save the media index");
        put("导入完成", "Import complete");
        put("媒体导入完成并已自动保存", "Media imported and saved automatically");
        put("本软件会将本机代码注入游戏进程，并在运行时修改模型、动画和语音资源选择。\n\n",
                "This software injects native code into the game process and modifies model, animation and voice asset selection at runtime.\n\n");
        put("可能的风险包括游戏崩溃、存档或配置异常、更新后失效，以及被游戏安全或反作弊系统识别。使用在线账号可能产生账号限制风险。\n\n",
                "Risks include game crashes, corrupted saves or configuration, breakage after updates, and detection by the game's security or anti-cheat systems. Using an online account may risk account restrictions.\n\n");
        put("本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关，也不提供任何形式的担保。请自行备份重要数据，遵守游戏服务条款，并自行承担使用后果。\n\n",
                "This is an unofficial experimental tool, unaffiliated with Hypergryph, Mountain Contour Studio or GRYPHLINE, and comes with no warranty of any kind. Back up important data, follow the game's terms of service, and accept the consequences of use.\n\n");
        put("本软件不负责停用或绕过反作弊组件；游戏更新后如签名不匹配，相关 Hook 应停止使用，等待适配。",
                "This software does not disable or bypass anti-cheat components. If a game update changes signatures, stop using the affected hooks until they are adapted.");
        put("终末地登录场景模型与角色配音控制器",
                "Login scene model and character voice controller for Endfield");
        put("版本 ", "Version ");
        put("软件更新", "Software Updates");
        put("启动时自动检查更新", "Check for updates at startup");
        put("进入应用时自动检查 GitHub Releases 并提示新版本",
                "Check GitHub Releases on launch and prompt when a new version exists");
        put("检查更新", "Check for Updates");
        put("打开下载页", "Open Release Page");
        put("项目", "Project");
        put("GitHub 仓库", "GitHub repository");
        put("全部版本", "All releases");
        put("GNU AGPL v3.0 开源许可", "GNU AGPL v3.0 license");
        put("作者与交流", "Author & Community");
        put("打开 B站主页", "Bilibili Space");
        put("打开小黑盒主页", "Heybox Profile");
        put("QQ群  ", "QQ Group  ");
        put("复制", "Copy");
        put("QQ群", "QQ Group");
        put("Discord 社区", "Discord community");
        put("风险与免责声明", "Risk & Disclaimer");
        put("本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关。注入和运行时修改可能造成游戏崩溃、版本不兼容或账号风险。",
                "This is an unofficial experimental tool, unaffiliated with Hypergryph, Mountain Contour Studio or GRYPHLINE. Injection and runtime modification may cause game crashes, version incompatibilities or account risk.");
        put("查看完整说明", "View Full Disclaimer");
        put("无法打开链接", "Unable to open the link");
        put("正在检查…", "Checking…");
        put("检查更新失败 · ", "Update check failed · ");
        put("更新数据过大", "Update data is too large");
        put("暂无 Android 发布版本", "No Android release yet");
        put("发现新版本 ", "Update available: ");
        put("已是最新版本 · ", "Up to date · ");
        put("桌面端语气词对照表版本不受支持",
                "The desktop voice-filler table version is not supported");
        put("其他角色（默认规则）", "All Other Characters (Default Rule)");
        put("桌面模型预设表版本不受支持", "The desktop model preset table version is not supported");
        put("桌面模型预设表为空", "The desktop model preset table is empty");
        put("FOV 保存失败", "Failed to save FOV");
        put("无效的作品编号", "Invalid work id");
        put("重复的选项组", "Duplicate option group");
        put("无效的选项组", "Invalid option group");
        put("重复的组件选项", "Duplicate component option");
        put("选项已从包中移除：", "The option was removed from the package: ");
        put("此选项组合在包内不可达", "This option combination is unreachable in the package");
        put("无效的选项标识", "Invalid option id");
        put("无效的外观选项", "Invalid appearance option");
        put("无效的模型目标类别", "Invalid model target kind");
        put("模型包缺少目标标识", "The model package is missing its target id");
        put("模型包缺少资源目标", "The model package is missing its resource target");
        put("无效的模型资源目标", "Invalid model resource target");
        put("此模型包没有 Android 资源目标，无法在当前平台导入或启用",
                "This package has no Android resource target, so it cannot be imported or enabled here");
        put("重复的模型包版本", "Duplicate model package version");
        put("无效的启用状态", "Invalid enabled state");
        put("未知选项条件", "Unknown option condition");
        put("无效的滑条目录", "Invalid slider catalog");
        put("滑条数值必须是整数", "Slider values must be integers");
        put("滑条数值必须在 0–1000 范围内", "Slider values must be within 0–1000");
        put("无效的滑条设置", "Invalid slider setting");
        put("无效或重复的滑条设置", "Invalid or duplicate slider setting");
        put("滑条数值不符合包内范围或步长：",
                "Slider value is outside the package's range or step: ");
        put("滑条已从包中移除", "The slider was removed from the package");
        put("框架服务未连接；请确认已在框架中启用模块后重试",
                "The framework service is not connected; enable the module in the framework and try again");
        put("框架中缺少模型包文件，请重新导入",
                "The model package files are missing from the framework; import again");
        put("模型包读取失败：", "Failed to read the model package: ");
        put("框架服务已断开，请重试", "The framework service disconnected; try again");
        put("无效的模型包发布文件", "Invalid model package publish file");
        put("模型包发布失败：", "Failed to publish the model package: ");
        put("无效的媒体文件", "Invalid media file");
        put("框架服务未连接", "The framework service is not connected");
        put("媒体文件过大", "Media file too large");
        put("作品文件过大", "Work file too large");
        put("请一次打开或分享一个 BEM 或 ZIP 文件。", "Open or share one BEM or ZIP file at a time.");
        put("分享内容不是文件，请使用文件管理器的「打开方式」。",
                "The shared content is not a file; use Open with in your file manager.");
        put("请一次选择一个 BEM 或 ZIP 文件，不支持文本或批量分享。",
                "Select one BEM or ZIP file at a time; text and batch shares are not supported.");
        put("无法读取文件打开请求，请从文件管理器重新打开。",
                "Unable to read the open request; open the file again from a file manager.");
        put("请通过文件管理器分享本地 BEM 或 ZIP 文件；不接受文件路径或网络链接。",
                "Share a local BEM or ZIP file from a file manager; file paths and web links are not accepted.");
        put("文件打开请求包含不同的文件地址，请一次选择一个文件。",
                "The open request contains different file locations; select one file at a time.");
        put("文件为空或不完整，不是有效的 BEM 包。",
                "The file is empty or incomplete and is not a valid BEM package.");
        put("不是有效的 BEM 文件；修改扩展名不能转换文件格式。",
                "Not a valid BEM file; renaming does not convert the format.");
        put("BEM 文件超过大小限制（最大 2 GiB）。", "The BEM file exceeds the size limit (2 GiB).");
        put("内置语气词对照表版本不受支持", "The built-in voice-filler table version is not supported");
        put(": 旧表中没有该目录", ": the legacy table has no such directory");
        put("游戏外部数据目录不可用", "The game's external data directory is unavailable");
        put("无法创建 catalog 目录：", "Unable to create the catalog directory: ");
        put(": 旧表缺少语言包分区", ": the legacy table has no language-pack section");
        put("内置语气词对照表损坏：", "The built-in voice-filler table is corrupted: ");
        put("Media ID 越界：", "Media ID out of range: ");
        put("Media 路由冲突：", "Media routing conflict: ");
        put("未下载对应语言包（", "Language pack not downloaded (");
        put(" 缺少 ", " missing ");
        put(" 个 Media", " media entries");
        put("当前设备语言包与旧表不匹配；", "The device language pack does not match the legacy table; ");
        put("不是 Endfield PCK", "Not an Endfield PCK");
        put("PCK header size 不受支持", "Unsupported PCK header size");
        put("PCK header 解密失败", "PCK header decryption failed");
        put("PCK Media sector 未对齐", "PCK media sector is not aligned");
        put("PCK Media entry size 不受支持：", "Unsupported PCK media entry size: ");
        put("语言包缺少 Media ID ", "The language pack is missing media ID ");
        put(" 越界", " is out of range");
        put("文件被截断", "The file is truncated");
        put("PCK header sector 越界", "PCK header sector out of range");
        put("PCK header sector 过大", "PCK header sector too large");
        put("PCK header 被截断", "PCK header truncated");
        put("PCK 64-bit 字段超出 Android 支持范围",
                "PCK 64-bit fields exceed what Android supports");
    }

    /**
     * Character display names.
     *
     * <p>These come from {@code character-names.json}, which ships Chinese only, so
     * the name reaches the screen as data rather than as a Java literal. The table
     * below is the desktop build's own {@code character-names-en.json}, which keeps
     * the two platforms naming characters identically. {@code 提弗洛斯} has no entry
     * on the desktop side yet and therefore falls back to Chinese.</p>
     */
    private static void characters() {
        put("管理员（男）", "Endministrator (Male)");
        put("管理员（女）", "Endministrator (Female)");
        put("佩丽卡", "Perlica");
        put("陈千语", "Chen Qianyu");
        put("狼卫", "Wulfgard");
        put("弧光", "Ikut");
        put("余烬", "Ember");
        put("赛希", "Xaihi");
        put("艾维文娜", "Avywenna");
        put("洁尔佩塔", "Gilberta");
        put("昼雪", "Snowshine");
        put("黎风", "Lifeng");
        put("莱万汀", "Laevatain");
        put("伊冯", "Yvonne");
        put("大潘", "Da Pan");
        put("秋栗", "Karin");
        put("卡契尔", "Catcher");
        put("埃特拉", "Aethel");
        put("萤石", "Fluorite");
        put("安塔尔", "Antal");
        put("阿列什", "Alesh");
        put("艾尔黛拉", "Ardelia");
        put("别礼", "Lastrite");
        put("汤汤", "Tangtang");
        put("洛茜", "Wulfa");
        put("骏卫", "Pograni");
        put("庄方宜", "Zhuang Fangyi");
        put("弭弗", "Mifu");
        put("决", "Li Zhiyan");
        put("卡缪", "Camille");
        put("梨诺", "Reno");
        put("祀", "Si");
        put("噗切娜", "Purrche");
        put("管理员", "Endministrator");
    }
}
