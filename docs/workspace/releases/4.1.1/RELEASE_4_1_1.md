# Better Endfield Next 4.1.1

本版为 **Windows 4.1.1 与 Android 4.1.1（40101）** 的开发记录，BEM Tools 版本维持 **1.6.2**。
开发进行中，各项完成后逐条追加；构建与验证结果在候选包产出后补写。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)；产物清单见同目录 [ASSETS.json](ASSETS.json)。

## 更新

<!-- 每完成一项就往这里追加一条，格式与 4.1.0 发布记录保持一致。 -->

- **修复昼雪（chr_0014_aurora）旧模型包在 4.1.0 或免校验模式下的左手骨骼错位问题**：
  - 修复 SelectCompatibleModelDonor 在开启「关闭模型校验」（skip_validation）或普通名称匹配时，因条件短路导致昼雪 1.6.3 定点兼容与骨骼重排逻辑被完全跳过的缺陷。
  - 强制判定昼雪旧目标（87120 索引 C1），无论是否跳过索引校验均无条件重定向至 1.6.3 现行 87288 索引与标准接收器路径，确保 urora_legacy_163 恒定生效并执行 10 个骨骼槽位（Slot 37~44、64/65）定点重排，彻底解决人物自身左手大拇指、食指严重扭曲及中指变形异常。
  - 补齐 BetterEndfieldNext.CustomModelBindingTests 因 4.1.0 动画驱动引入导致的缺失依赖库与 BlendShape 反射桩函数，并在 urora_runtime_checks.inc 补强 skip_validation=true 全链路自动化回归断言。

- **安卓界面重构：主页改为「启用功能一览」**：
  - 主页只列出当前处于开启状态的功能，并显示实时状态：替换角色与动作、登录主题色、自定义语言的角色数、已开启的界面项、已开启的相机项、冲刺角色、悬浮窗透明度、当前 MMD 作品；点击任意一项直达对应设置页。
  - 未启用的功能不再出现在主页，入口集中在「增强功能」页；第三方模型没有开关状态，单独在主页保留一个入口。从设置页返回时列表按存储重建，不会显示过期状态。
  - 顶部导航由「开屏模型 / 第三方模型 / 角色配音 / 增强功能 / 关于」收敛为「主页 / 增强功能 / 关于」。
  - 开屏模型、第三方模型、角色配音收进「增强功能」二级页面，该页按「模型与外观 / 游戏内增强 / 内容与工具」分组；二级页面顶部提供「‹ 返回增强功能」，系统返回键在任意子页面先回主页，不再直接退出程序。
- **安卓端接入桌面端「管理员外观」模块**（`betterendfieldnext.weapon_appearance`，与桌面端 WeaponAppearancePage 能力一一对齐）：
  - 设置页提供与桌面端相同的四段结构：总开关「启用外观覆写」；武器（替换宏愿外观开关 + 样式选择「宏愿 · 剧情水墨流光 / 原版默认外观」）；管理员角色（替换开关 + 形态三选「剧情水墨专属服装 / 剧情摘面具形态 / 原版默认形态」）；战斗与剑光特效开关。
  - 配置串键位与桌面端 `WeaponAppearanceConfiguration.ToIni()` 完全一致（`replace_weapon` / `weapon_mode` / `replace_character` / `character_mode` / `replace_effects` / `effects_mode`），原生模块按桌面源码原样进 NDK 构建，各区块可独立启停，不再强制全部开启。
  - 主页「启用功能一览」按细粒度开关动态汇总（水墨武器 / 水墨服装 / 摘面具形态 / 水墨剑光），全部保留原版时显示「已启用（保留原版外观）」。
- **持续冲刺支持「祀」（双端对齐，默认关闭）**：
  - 基于 Issue #32（zhuran24）逆向研究成果精简实现。采用轻量原生 Animator 状态回绕（0.315 → 0.126）与坐骑墨兽（`TmpObjectInst`）同帧 CrossFade 协同驱动，伴随墨兽时间窗自适应回绕，确保骑乘奔跑不脱节。
  - 首次回绕后自动冻结墨兽材质消散时钟，抑制收尾踩水花粒子，使墨兽全程奔跑不溶解。
  - 不引入沉重的外部姿态包（无需 BEPOSE12 或 1.7MB 的 BEACC001 附属件数据），不落盘自学习接缝文件，无额外底层钩子负担。
  - Windows 与 Android 启动器增强页均新增「祀」独立开关，默认保持关闭。
- **安卓界面英文本地化**（系统语言为英文时整界面切换英文，中文环境零变化）：
  - 新增 `Text` 翻译层（Qt `tr()` 式，以中文源文为键）。选它而不是纯资源文件，是因为游戏内悬浮窗运行在游戏进程、拿不到模块自身的 `R` 表，且大量文案是运行时拼接的片段；非英文语言或查不到词条时原样返回中文，因此中文路径的字符串比较、日志与状态机行为完全不变。
  - 新增 `res/values-en/strings.xml`，213 个 key 与中文侧一一对应（占位符 `%1$s` / `%1$d` / `%%` 全部对齐）；`layout/view_bem_install.xml` 与 `AndroidManifest.xml` 中的硬编码中文改为 `@string` 引用。
  - 把依赖中文字面量做 `contains` 判断的两处状态栏显隐改为结构化标志（`BemInstaller.IDLE_STATUS`、`MmdInstaller.failed`）。否则英文环境下判断恒为 false，操作失败时错误条会静默消失。
  - 角色名统一取桌面端 `ui/BetterEndfieldNext.UI/Assets/model/character-names-en.json` 的官方英文名，不再从内部 id 推断（洁尔佩塔 Gilberta、梨诺 Reno、祀 Si）。角色配音列表与开屏模型的角色名来自数据文件，在读取处套翻译，`characterId()` / `id()` 仍是存盘值，不影响比较逻辑。
- **赞助面板新增 Ko-fi**：
  - 桌面端与安卓端赞助面板在微信赞赏、爱发电、PayPal 之外新增 Ko-fi（`https://ko-fi.com/hydra405`），两端顺序一致。
- **修复材质包加载明显卡顿：日志器改为持久句柄 + 加载期日志汇总**（起因是「不装动画包、只用不带控制器的材质包也掉性能，这一版更明显」）：
  - 根因不在驱动路径：无 `drivers` 时 `EnsureAnimationDriverLoaded` / `ApplyDriverMorph` / `ReleaseDriverMaterials` / `SyncDriverSounds` / `PublishDriverStateIfChanged` 五处全部早退，逐帧成本约等于 0。真正的成本在加载路径上逐条打日志，而 `Logger::Write` 每条都用 `ofstream` 打开再析构一次文件——Windows 的实时防护会在 write-open 时扫描整份文件，实测本机每条「打开-追加-关闭」**10.6 ms**，且随日志文件变大继续变慢。
  - `Logger` 改为持有 `std::ofstream` 成员：`Initialize` 时打开一次，`Write` 直接写入并每行 `flush()`，打开失败时在 `Write` 内清 failbit 重试（不会每行重新扫文件）。每行 flush 保住崩溃后日志尾部的可读性。
  - 两处逐条日志改为汇总：`Applied BEM 1.5 material overrides` 由「每条带覆盖的 draw 一条」改为「每个组件一条」（`C<n> draws=N properties=M`，保留材质路径为 `slots=N properties=M`）；贴图 `built t=` 明细改为每次上传事务只打第一张（结尾标 `(first of this transaction)`），其余进格式直方图，由事务汇总行以 `textureFormats=BC7 sRGB x8,RGBA32 linear x2` 输出。
  - 实测每行成本 **10.6 ms → 0.02 ms（450–600×）**。上百 draw 的材质包原先要白等数秒，现在日志开销可忽略。诊断信息没有减少：首张贴图的格式/尺寸、覆盖条数与属性总数、本次事务的贴图格式分布都保留在汇总行里。
  - 行为变化：日志文件在程序运行期间保持打开。运行中删除或重命名日志文件后，后续日志仍写入原句柄（旧实现会重建文件）；需要清空日志请先退出程序。

## 附件范围

候选包产出后填写。

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| BetterEndfieldNext-4.1.1-Setup.exe | 待定 | Windows x64 安装包 |
| BetterEndfieldNext-4.1.1-Android-arm64.apk | 待定 | Android arm64 安装包 |
| BEM-Tools-1.6.2-win-x64.zip | 待定 | 独立创作者 GUI、CLI 和全量角色资料 |

## 验证边界

整体仍未产出候选包，安装包与发行验证待补。已完成的验证：

- **安卓英文本地化**：`assembleDebug` 通过；REDMI K90 Pro Max（25102RKBEC，Android 17）实机走查主页、增强功能、关于、悬浮窗、管理员外观、持续冲刺、第三方模型、角色配音八个页面，均为英文且无截断；清除应用级语言覆盖后中文界面与文案与改动前一致。
- **赞助面板 Ko-fi**：安卓实机确认微信赞赏 / 爱发电 / PayPal / Ko-fi 四个渠道齐全且无换行截断；桌面端 `dotnet build` 通过，未实机点击。
- **日志器与加载日志汇总**：`logging.cpp`、`host_runtime.cpp`、`module_manager.cpp`、`hook_broker.cpp`、`module.cpp` 用 MSVC 19.44（`-std:c++20 -EHsc -utf-8`）单独编译通过。真实 `Logger` 探针（`worktree/audit/bench/logger_probe.cpp`，直接链接 `logging.cpp`）实测三项：改前/改后各 200 行的每行成本为 10.6 / 10.1 / 11.6 ms 对 0.023 / 0.017 / 0.026 ms；`Logger` 存活期间用另一 `ifstream` 能读到最新行（`hook_chain_tests.cpp:195` 的同文件读取不受影响）；写完末行直接 `std::_Exit(0)` 不跑析构，文件尾部仍完整。**未做完整 CMake 构建与实机加载验证**（`build/native_build` 的 CMake 缓存指向已不存在的 `C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools`），实测数字来自微基准而非实机加载计时。
- 未覆盖：`values-ja` / `values-ko` / `values-zh-rTW` 仍只有两个文件，其余回落中文；BEM 包内的角色名、选项名、动效名属数据，未翻译；`提弗洛斯`（chr_0034_typhoea）桌面端尚无英文名，暂时回落中文。
