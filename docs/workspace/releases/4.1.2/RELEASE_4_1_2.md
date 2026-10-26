# Better Endfield Next 4.1.2

本版为 **Windows 4.1.2 与 Android 4.1.2（40102）** 的开发记录，BEM Tools 维持 **1.6.3**。
开发进行中，各项完成后逐条追加；构建与验证结果在候选包产出后补写。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)；产物清单见同目录 [ASSETS.json](ASSETS.json)。

## 更新

<!-- 每完成一项就往这里追加一条，格式与 4.1.1 发布记录保持一致。 -->

- **桌面端角色外观新增「打开模型文件夹」快捷入口**：
  - 「角色外观」管理页顶部的快捷按钮明确更名为「打开模型文件夹」；
  - 点击后直接打开实际存放 `.bem` 文件的目录（`<程序安装目录>/models`，若无写权限则自动回退 AppData 目录），方便直接查看、备份与管理模型包文件。

- **安卓端界面布局优化：移除顶部横幅，底部导航四等分**：
  - 移除主界面顶部大横幅，减少纵向空间占用；
  - 将赞助按钮移至底部导航栏并取消特殊高亮色，统一采用标签页视觉样式；
  - 底部导航栏重构为「主页 / 增强功能 / 关于 / 赞助」四键并列，采用等分宽度（`weight=1`）与文字动态自适应缩放，确保在不同分辨率与屏幕比例下恒定保持四等分对称排布。

- **优化桌面端持续冲刺界面层级排布**：
  - 将「隐藏机甲与光效」选项与说明调整至「梨诺」设置项正下方并缩进展示，修正此前「祀」插入在梨诺与其子选项之间导致的排布与归属混乱；
  - 机甲光效子选项与梨诺启用状态联动，未启用梨诺时自动禁用子项，层级关系一目了然。

- **修复桌面端发布遗漏管理员外观模块动态库**：
  - 修复 `native/CMakeLists.txt` 中 `BetterEndfieldNext.Layout` 目标遗漏 `BetterEndfieldNext.WeaponAppearance` 依赖项的问题，确保 Windows 完整构建时自动编译该 DLL 并部署到 `modules/` 目录；
  - 在 `BuildBetterEndfieldNext.ps1` 发布前完整性检查清单中补齐 `BetterEndfieldNext.WeaponAppearance.dll` 与 `betterendfieldnext.weapon_appearance.module.ini` 等模块文件，避免构建与打包漏发。

- **管理员外观功能规范化：双端统一默认关闭**：
  - 桌面端 `WeaponAppearanceConfiguration` 默认启用状态、ini 读取缺省回退值以及 `betterendfieldnext.weapon_appearance.module.ini` 清单全部调整为 `enabled=false`；
  - 双端共享原生模块 `weapon_appearance/module.cpp` 的默认运行配置结构体同步调整为 `enabled = false`；
  - 与安卓端 `ModuleSettings.isAppearanceEnabled`（默认 `false`）完全对齐，消除未配置时自动覆写外观与特效的行为，双端统一遵循“默认保持原版、用户手动选择开启”的行为规范。

- **BEM Tools 新增 Android CLI 产物（1.6.3）**：新增 `BEM-Tools-1.6.3-android-arm64.tar.gz`，让手机上的 agent 自己完成 `inspect` / `convert` / `validate` / `pack`，不必把源包传回 Windows。
  - 不是 APK，也不是 GUI：解压后是源码形态的 `app/` 加一个 mksh 兼容的 `bem-tools` 入口；入口解析自身路径（含符号链接）、把 `TMPDIR` 指向 `/data/local/tmp`、注入 `PYTHONPATH` 与 UTF-8、透传 exit 0/2，并转发 `doctor` / `install-wheel` / `repair`；
  - 源格式只保留目录与 ZIP：RAR/7z 需要 7-Zip 后端，发行包不带，命中时按 `ARCHIVE_BACKEND` 拒绝而不是缺可执行文件；
  - 打包用 AST 可达性分析从入口算白名单，模块从 39 降到 24；成品 2.0 MB 压缩 / 11.5 MB 解压；
  - 默认装到 `/data/local/tmp/bem-cli`：`/sdcard` 挂载为 `noexec`，Android 10 起应用私有目录也被 SELinux 拒绝 `execve`。构建入口 `scripts/BuildBemCliAndroid.ps1`。

- **Android CLI 在 Python 3.14 上零依赖**：Termux 当前 `pkg install python` 装的是 3.14.8，自带 PEP 784 的 `compression.zstd`，设备不必再装 `zstandard`。
  - 新增 `zstd_compat.py`：扩展缺失时把标准库实现挂成 `zstandard`，扩展存在时完全不接管，Windows 行为不变；`doctor` 的 `deps.zstd_backend` 报 `stdlib` 或 `cext`；
  - 两条后端输出逐字节相同（都是 zstd 1.5.7），手机产出的包与 Windows 产出的 digest 一致；用真实 `TohsakaRin-aglina-by-Dr-hydra.bem` 在 3.13（扩展）与 3.14（标准库）上校验，除源路径外输出完全一致；
  - 回归：`audit/test_bem_repair.py` 141 例、`test_bem_v1` 9 例、`test_bem_projects` 10 例、`test_bem_v11` 13 例（1 skip）在两套后端上全绿。

- **随包提供 bionic arm64 的 zstandard wheel**：Termux 没有 zstandard 包，PyPI 只有 glibc/musl 轮子，`pip install zstandard` 会在设备上拉 clang 现编。
  - 用 NDK 27 clang 加 Termux `python` 包内的 3.14 头文件交叉编译 `zstandard 0.25.0` 的 C 扩展，产出 `zstandard-0.25.0-cp314-cp314-android_24_arm64_v8a.whl`（301 KB；strip 后 604 KB 的 ELF64/AArch64 共享对象，只依赖 `libc`/`libm`/`libdl`，只导出 `PyInit_backend_c`）；
  - `bem-tools install-wheel` 不走 pip 而是解包复制：Termux 的 Python 报 `sys.platform == 'android'`，pip 认 `android_<api>_arm64_v8a` 标签，Chaquopy 一类 Linux 形态解释器认 `linux_aarch64`，一个 wheel 文件名只能带一个平台标签；安装器按本机 `EXT_SUFFIX` 落盘，两种宿主都适用且不需要编译器；
  - 构建脚本 `tools/CustomModel/android/build_zstandard_wheel.py`，Termux 换 Python 版本后重跑即可。

- **创作者技能补 Android 运行规范，SKILL.md 增加宿主分叉**：新增 `references/ANDROID_RUNTIME.md` 与 `.en.md`，覆盖入口差异、能力差异表、路径与权限三条硬约束、输出契约、错误码与安装升级；`SKILL.md` 原先把入口写死成 `.exe` 或 `python bem_tool.py`，改为按宿主分叉的三行表并写明第一步是 `doctor`；`bem_tool.py` 的源格式提示改为按 7-Zip 后端是否可用动态拼装，Windows 文案逐字未变。

- **DSH 插件补齐工具面与预览面**：`dsh-bem-creator` 从「只带指令」变成「skill + 工具 + 预览页」，会话里可以直接看成品模型。
  - **`bem` 工具**：`inspect` / `validate` / `preview` / `convert` / `doctor` 五个动作。`inspect` 只读 manifest，`validate` 查的是会让渲染出错的问题（索引越界、载荷长度不足、网格引用越界），`preview` 建场景并返回预览 id，`convert` 只在插件旁边放了 CLI 时才跑，`doctor` 报本机三段能力是否齐备。工具用裸 JSON Schema 自注册，不依赖 `@deepseek-ai/dsh-tools`，profile 里解析不到该包也能装；
  - **预览页**：注册 `conversation.view` 槽位，标签「预览」。轨道相机（拖拽旋转、滚轮/双指缩放、重置视角）、外观 chips、选项组 chips、形态参数滑条、部件勾选、贴图/素模切换、统计与解码状态；
  - **1.0–1.2 与 1.3+ 两种 draws 布局都吃**：老包是网格共享一份索引、draw 用 `start`/`count` 切片，新包是每个 draw 自带索引与 `when` 条件。`validate` 与场景构建按同一套判据分叉，真实 1.0 包 4 网格 / 16 draw / 15 贴图全过；
  - **形态滑条不是假的**：读 `parameters` 的 0–1000 刻度与 `mesh_deformations` 的稀疏帧（`u32 顶点 + 3×f32 增量`），客户端做相邻帧插值后 `bufferSubData` 回传，滑条动网格就动；
  - **贴图走 worker 池**：BC1/BC3/BC4/BC5/BC7 在 1–4 个 module worker 里解码，解完立刻转移所有权并丢掉 CPU 侧副本（五张 4096×2048 的 RGBA 是 160 MB，留在主线程会把手机 WebView 撑爆）；BC7 解码器从 `@bis-toolkit/bcn` 1.0.2 逐字 vendored（GPL-3.0-or-later，AGPL §13 允许合并），已用真实包贴图验证（相邻像素平均差 2.38，无效块 0）；
  - **不动 `/api/*`**：connection 插件占着那张路由表，插件用自己的 `/dsh-bem/` 前缀发 `api/latest`、`api/scene/<id>`、`api/blob/<scene>/<blob>` 与两个解码器资产，几何与贴图走 `application/octet-stream`，10 MB 的包不必 base64 塞进 JSON；
  - **明确不解码 ASTC**：安卓目标包里的 ASTC 贴图在浏览器侧标记为跳过并在角标里说明，JS 版 ASTC 解一张 4096² 基础层慢到没有意义（BC7 已经要 4.7 s/张）；
  - **CLI 改成随包携带源码**：原先 `convert` 只认 `BEM_CLI` 与 `lib/bin/*` 两个位置，等于把「脱离 bemtools」的负担转给了用户。现在 `lib/bin/cli/` 里放整套 Python 源码树（35 个模块 + `catalog/` 39 个角色 + `profiles/` 48 份，解压 11.2 MB、压缩后 1.6 MB），Windows 与安卓共用同一份——`python` / `python3` 由宿主提供。**不带**的是 CPython 运行时（PyInstaller 那份 33 MB 几乎全是解释器）、`zstandard` wheel（ABI 绑定，覆盖 3.10–3.14 要五份）和 7-Zip 后端（只有 zip/rar/7z 源才用）。插件因此从 385 KB 涨到 2.0 MB，换来「机器上有 Python 就能 convert」；
  - **探测改成「跑一次才算数」**：候选按 `BEM_CLI` → 包内源码树 → `PATH` → 约定目录（`/data/local/tmp/bem-cli`、`%LOCALAPPDATA%\BEM Tools`、`~/.local/bin`）的顺序试，每个都真的执行 `--version`，第一个成功的才采用——「文件在」和「能跑」不再混为一谈。解释器按 `python` → `py` → `python3`（Windows）/ `python3` → `python`（POSIX）依次试，`BEM_PYTHON` 可覆盖。全部失败时 `doctor` 报出前四条失败原文与可执行的补救命令（实测缺 `zstandard` 时给出 `python -m pip install zstandard`，装完即返回 `BEM Tools 1.6.3`）；
  - **`catalog/` 与 `profiles/` 不裁**：`component_auto.catalogs()` 是按入口点所在目录 glob `catalog/*.json` 的，裁了返回空列表，组件匹配类转换直接失败；`profiles/` 则是 recipe 里 `"profile": "profiles/verified-native.json"` 的落点。两者合计压缩后 1.3 MB，裁掉的收益远小于代价；
  - **三条测试**：`test/smoke.mjs`（skill 注册与 frontmatter 长度）、`test/host.test.mjs`（工具、路由、场景 JSON、二进制 blob、1.0/1.5 两种包面）、`test/client.test.mjs`（用假 React 与假 WebGL2 上下文真渲染一遍：面板出得来、贴图解得出、滑条改了位置缓冲、部件隐藏后不画、场景过期给出提示、卸载回收 worker）。`BuildDshPlugin.py` 在暂存目录里跑完三条再 `npm pack`，并校验 `exports["./client"]`、`dsh.client.platform`、`lib/client.js` 的模块封装——`dsh-client-modules` 在这三处任一缺失时是直接抛错，不是降级。
  - **联调修掉一个启动级 bug**：`apply` 原本在插件自己的 ctx 上按属性探 `ctx.webServer` / `ctx.tools`。Cordis 4 的 ctx 是服务表上的 Proxy，名字不在本 fiber 的 `inject` 集合里时是直接抛 `cannot get property "webServer" without inject`，不返回 `undefined`，`source?.name` 也挡不住——桌面 profile 必然在 `apply` 同步栈里把 fiber 打成 `failed`，skill 一起回滚（`fiberPhase: failed`、`Config.status: absent`、`/dsh-bem/*` 全 404），而客户端半的标签页照常出现，看起来像一个永远空白的预览页。改为 `mountService`：`ctx.get(name)` 非严格读做同步快路径（服务已就绪时不额外建 fiber），服务缺席时才用 `ctx.inject([name], cb)` 起子 fiber 声明依赖兜底晚到，生命周期统一交给 `ctx.effect`。`doctor` 顺带多报 `surface.routes` / `surface.tool`；
  - **测试替身也跟着修了**：原来的 stub 是普通对象，读未声明的服务只是 `undefined`，正好把这个 bug 藏住。新增 `test/cordis.mjs` 用 Proxy 复刻真实语义，`host.test.mjs` 反过来断言它必须抛；新增 `test/real-cordis.mjs`，在装有 dsh 的机器上用真实 Cordis 4.0.5 复验桌面全服务、headless 只有 skills、服务晚到、fiber 内直接读属性必抛四种情形，全绿且 `fiber.state` 均为 2（active）；
  - **前缀路由不能带尾斜杠**：`kind: 'prefix'` 注册 `/dsh-bem/` 时，`dsh-host-webserver` 的 matcher 会拼成 `pathname.startsWith('/dsh-bem//')`，于是只有 `/dsh-bem/` 本身和 `/dsh-bem//…` 能命中，正常的 `/dsh-bem/api/latest` 全部落到 SPA fallback。注册成功、handler 活着、`doctor.surface.routes` 还是 `true`——最容易被当成「端口不对」或「重启没生效」的一种失败。改为注册 `/dsh-bem`，handler 按 `ROUTE_BASE` 切片并去掉前导斜杠；测试也补上了分派层：`host.test.mjs` 先过一遍照抄的 `match()` 再调 handler，`real-cordis.mjs` 用真实 `WebServer.prototype.match` 交叉验证六种路径。
  - **预览画面的三处硬伤（联调第二轮）**：一是朝向——包里的顶点是 Z-up（1.67 m 的角色 Z 跨度 1.668、Y 只有 0.58），而相机是 Y-up，模型横躺；宿主改在 `buildScene` 里按并集包围盒判定（Z 跨度 > Y 跨度 1.05 倍）后把位置与形变增量一起做 (x, y, z) → (x, z, −y)，并在场景 JSON 写明 `orientation`。实测 40 个真实包（1.0/1.4/1.5）全是 Z-up，扁平的合成 fixture Z 跨度为 0 不会被误转。二是亮度——贴图按 `SRGB8_ALPHA8` 采样得到的是线性值，却被直接写进非 sRGB 的 drawing buffer，中间调整体暗一档；再叠上 `0.42 + 0.58 * key` 的大环境光，模型几乎只有一个明度。改为线性空间里 key/fill/半球三灯、末尾 `pow(color, 1/2.2)` 编码一次，环境光降到 0.20、key 提到 0.80，素模平均亮度 0.27 → 0.52（sRGB 0–1）。三是法线方向——网格沿 UV 缝拆点、绕序混杂（质心法只有约 64% 的三角形朝外），`gl_FrontFacing` 不能当「这面朝外」用，改为按视点翻（`dot(n, view) < 0` 取反）并给零长法线兜底。顺带把 `resetView` 的 `radius * 2.4` 改成 `radius / sin(fov/2) * 1.06`——原距离在 38° 垂直视场下装不下包围球，头脚会被裁掉。
  - **ASTC 不再假装可解码**：宿主原先给 ASTC 也发 blob，客户端于是逐张送进 worker，换来 6 条 `no decoder for astc` 失败。现在 `TEXTURE_FAMILY` 带 `decodable` 标志，格式 48/49/50 只给 `blob: null` + `skipped: 'ASTC blocks are not decoded in the browser build'`，`stats.previewableTextures` 自然把它们排除，客户端对空 blob 直接跳过（不算失败），角标写「N 张 ASTC 贴图不在浏览器内解码」。几何照常显示，这些 draw 走素模。
  - **补齐两处空断言**：`host.test.mjs` 原先把 ASTC 断言写在对 1.0 包的检查里，而那个包一张 ASTC 都没有，`astc.every(...)` 恒真——新增 ASTC 包（22 张贴图全 ASTC）与 Y-up 朝向两节；`client.test.mjs` 的 fixture 换成绕序朝内的四面体，新增「宿主跳过的贴图不算失败」「着色器不依赖 `gl_FrontFacing`」「末尾有 gamma 编码」三条；`BuildDshPlugin.py` 的冒烟测试补上 `BEM_TEST_PACKAGE_ASTC`。

- **产物版本对齐 1.6.3**：`BEM-Tools-1.6.3-win-x64.zip`、`BEM-Tools-1.6.3-android-arm64.tar.gz` 与 `dsh-bem-creator-1.6.3.tgz` 三件产物同版本发布。DSH 插件版本与 `bem_tool.py` 的 `TOOL_VERSION` 由构建脚本强校验，不一致会直接构建失败；本轮双方均为 1.6.3，版本号本身未变，只把发布记录补齐。

## 附件范围

候选包产出后填写。

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| BetterEndfieldNext-4.1.2-Setup.exe | 待定 | Windows x64 安装包 |
| BetterEndfieldNext-4.1.2-Android-arm64.apk | 待定 | Android arm64 安装包 |
| BEM-Tools-1.6.3-win-x64.zip | 待定 | 独立创作者 GUI、CLI 和全量角色资料 |
| BEM-Tools-1.6.3-android-arm64.tar.gz | 待定 | 安卓端 BEM CLI（源码形态，供 agent 在 Termux / root shell 直接调用；无 GUI，只吃目录与 ZIP） |
| dsh-bem-creator-1.6.3.tgz | 待定 | DeepSeek Harness 插件（2.0 MB）：创作者 skill + `bem` 工具 + 会话内成品预览 + BEM Tools 源码树；`convert` 只需宿主机有 Python 3.10+（3.14+ 免依赖，更早版本 `pip install zstandard`） |

## 验证边界

- **桌面端构建与依赖检验**：CMake 编译目标 `BetterEndfieldNext.WeaponAppearance` 成功生成 `BetterEndfieldNext.WeaponAppearance.dll`；`BetterEndfieldNext.Layout` 成功包含该依赖；
- **配置与默认行为验证**：`WeaponAppearanceConfiguration` 实例与 ini 序列化确认 `enabled=false`；原生模块默认 `Configuration.enabled=false`；安卓端默认 `appearance_enabled=false`。
- **DSH 插件**：`BuildDshPlugin.py` 在暂存目录里依次跑 `test/smoke.mjs`、`test/host.test.mjs`、`test/client.test.mjs`，三条全绿才 `npm pack`；归档再校验一遍 `lib/client.js`、`lib/vendor/bcn.js`、`lib/assets/decode-worker.js` 是否在内，以及 `exports["./client"]` 与 `dsh.client.platform` 是否写对。宿主半跑真实包：1.0 包 4 网格 / 47667 顶点 / 16 draw / 15 贴图，`validate ok`；1.5 包 9 网格 / 145138 顶点 / 47 draw / 10 贴图 / 17 选项组 / 16 条组件规则 / 4 参数 / 4 形变通道，`validate ok`，建场景 77 ms。
- **预览边界的诚实交代**：客户端半的真实渲染只在假 React + 假 WebGL2 上下文里验证过（面板、解码、形变、隐藏、过期提示、卸载回收），**着色器能否在具体驱动上编译仍只能等 dsh 里联调**；画面本身另用同一套相机/光照公式做了一次离线软件光栅（720×900，素模）：1.0 包覆盖 14.8%、包围盒 371×702（高宽比 1.89，站直）、亮度 mean 0.52 / p10 0.40 / p90 0.60，1.5 包 mean 0.51，与修复前 mean 0.27 相比约翻倍；ASTC 贴图不解码也不再发 blob，1.0 包的基础层贴图按块直接喂 GPU，mip 链由 GPU 生成。
