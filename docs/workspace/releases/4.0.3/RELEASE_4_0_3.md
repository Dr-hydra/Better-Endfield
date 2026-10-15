# Better Endfield Next 4.0.3

本次面向终末地 **Windows 1.6.3 / Android 1.6.4**。Windows 与 Android 版本为 4.0.3，Android versionCode 40003，BEM Tools 更新为 1.5.3。安装身份与签名沿用 Next，4.0.x 可覆盖更新。公开发布正文见 [GitHub Release 说明](GITHUB_RELEASE.md)。

## 更新

- **新版启动器适配**：BE 的 XInput、“保存并启动”、Injector、快捷方式和 Steam 国服替换命令接入统一启动流程。利用本机官方签名启动文件满足新版检查，无需打开官方启动器界面；不修改游戏的校验 DLL。仍需保留有效的官方启动器安装文件。
- **保留加载方式选择**：XInput 只由现有“安装或更新”按钮部署；启动、创建快捷方式和应用 Steam ACF 不会自动安装。Steam 按当前模式生成 Injector 或 Launcher 命令，切换模式或升级后需重新粘贴启动选项。
- **Steam 正式包修复**：明确持久化 JSON 字段和无参构造，修复混淆后元数据导入、缓存、安装与权限记录读取失败；预览窗口在挂载前注册首次资料导入。
- **持续冲刺**：针对 1.6.3 中相关方法被内联的变化，调整到仍被主流程调用的中断命令与退出入口，保留角色归属和自然结束条件。
- **战斗统计**：更新新版 Proto 字段与集合布局，属性范围扩至 108，修正类型映射，补充减免来源归因和物理异常倍率。
- **MMD 地面检测**：正确验证 `FindFloorResult` 内嵌的 `RaycastHit`，避免将有效布局误判为不支持。
- **资源增量更新**：沿现有软件工具链刷新模型/动作、四语短语音和战斗字典，目前覆盖 37 个角色及变体条目、4,719 条动作，补充祀和明河相关条目；更新 Buff 来源关联并补齐可取得的技能、装备图片。资源缺失项按实际情况保留，不以其他内容代替。
- **BEM Tools 1.5.3**：正式目录新增祀 15 个配对部件、14 个索引身份和 58 个纹理身份；其中 8 个部件布局符合现有来源转换规则，7 个保持明确不支持。附带祀完整世界/详情、cloth_04 和明河三种世界模型的 5 套显式工程；这些工程为 Windows-only、keep-only 创作起点，未伪造 Android donor 或完整自动转换/渲染验证。独立 ZIP 与主程序内置工具同步分发。

## 本地附件

产物位于 `releases/4.0.3/`：

| 文件 | 用途 |
| --- | --- |
| [BetterEndfieldNext-4.0.3-Setup.exe](../../../../releases/4.0.3/BetterEndfieldNext-4.0.3-Setup.exe) | Windows x64 安装包 |
| [BetterEndfieldNext-4.0.3-win-x64.zip](../../../../releases/4.0.3/BetterEndfieldNext-4.0.3-win-x64.zip) | Windows x64 ZIP 包 |
| [BetterEndfieldNext-4.0.3-Android-arm64.apk](../../../../releases/4.0.3/BetterEndfieldNext-4.0.3-Android-arm64.apk) | Android arm64，已在连接手机覆盖安装 |
| [BEM-Tools-1.5.3-win-x64.zip](../../../../releases/4.0.3/BEM-Tools-1.5.3-win-x64.zip) | 独立创作者 GUI、CLI 与新增角色资料 |

公开附件为 Setup、Android APK 与 BEM Tools ZIP；Windows 主程序便携 ZIP 只留本地。本次未生成 AAB，没有额外计算产物哈希。

## 当前验证范围

- 独立研究启动入口已启动当前 CN Windows 1.6.3 游戏，并由游戏自身配置返回启动检查成功。隔离原始 DLL 探针也已通过；探针使用重建的 fixture，与完整游戏的真实配置验证分别记录。
- Windows Release 完整构建、Obfuscar 混淆、原有 Next 签名与 Inno Setup 打包通过；新 Launcher 已进入 ZIP 与安装包。Native 启动器检查 20 项、Steam 服务及可选加载方式检查 82 项通过。额外短审查修复 Injector 初始化异常时遗留暂停游戏的清理缺口。
- 最终发布单文件程序实际打开隔离 Steam 页面，标准和小窗口预览通过；对该程序实际解出的混淆程序集，元数据解析与 metadata/cache/install/compatibility 五类检查全部通过。没有写真实 Steam 配置、安装 XInput 或改变用户加载模式。
- BEM Tools 1.5.3 正式 ZIP 在异工作目录中检查通过：独立/内置 CLI 与 GUI 版本一致，祀目录和 24 个新资料文件两处分发一致，5 个新工程均 pack/validate 通过；独立 GUI 实际完成中文目录打开、导出 BEM 1.4、资源校验和帮助打开。
- Steam 覆盖层、计时及完整 Steam 客户端启动流程尚未实测，不能仅由启动检查成功推导这些功能可用。
- Android Release（R8 与原有签名）构建通过，已覆盖安装 4.0.3 并保留数据。手机游戏实际为 1.6.4 / 61；只提取 ARM64 库和元数据，确认冲刺两个新挂点均被调用，相关委托、字段和枚举匹配。MMD 实际走 Android 专用 `runtime_invoke`，参数和字段合同匹配，无需照搬 Windows 原生 ABI。
- 本轮手机日志确认相机合同就绪、四个挂钩安装及初始化成功；持续冲刺未启用，MMD 未播放，不计作行为验证。随后系统记录 `USER REQUESTED / FORCE STOP / SwipeUpClean`，不能把用户划掉应用误报为崩溃。未下载语言包，未建立全量反编译库。
- 语音仍有具名事件缺少对应 bank，新角色头像来源亦有缺项；本次资源更新没有把这些缺项标为完成。BEM 新角色正式资料的部分转换边界详见 [1.6.3 角色资料](../../../../tools/CustomModel/profiles/endfield-1.6.3/README.md)，不将采集完成等同于全部来源转换或实机替换已验收。

## 依据

- [1.6.3 挂点与资源更新](../../../host/research/1.6.3/hook-update/GAME_UPDATE_1_6_3.md)
- [新版启动检查与启动桥验证](../../../host/research/1.6.3/launcher-check/LAUNCHER_CHECK.md)
- [Android 1.6.4 定向挂点与本轮设备记录](../../../../research/android/1.6.4/Android/hook-update/ANDROID_1_6_4_HOOK_CHECK.md)

构建与精简验证结果见同目录 `ASSETS.json`。普通日志位于 `build/release-4.0.3/logs/`，发布包保存于不可清理的 `releases/4.0.3/`。
