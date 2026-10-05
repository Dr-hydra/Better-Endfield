# 工作区测试规范与入口

维护入口为 `config/test-suites.json`、`scripts/run_workspace_tests.py` 和
`scripts/Test-Workspace.ps1`。清单登记套件/脚本入口；断言仍由原有 unittest、
CTest 和平台测试发现。`native/tests`、`tools/CustomModel/test_*.py`、Android
测试源码没有为统一目录而搬动或复制。

本轮只核对清单、计划和纯模拟 registry 边界。没有构建、运行现存大测试、
启动游戏/手机、刷新真实资源、安装/覆盖 `E:\Better Endfield` 或推送。
`F:\Better Endfield_legacy` 保持完整回退；`F:\zmd` 不参与。

## 使用入口

在 `F:\Better Endfield` 执行，或从任意目录使用入口的绝对路径：

```powershell
python -B scripts/run_workspace_tests.py --list
python -B scripts/run_workspace_tests.py --module custom_model --plan
python -B scripts/run_workspace_tests.py --platform Android --kind simulation --plan
python -B scripts/run_workspace_tests.py --game-version 1.5.3 --plan
./scripts/Test-Workspace.ps1 -List
./scripts/Test-Workspace.ps1 -Module creator -Kind offline -Plan
```

`--module/--platform/--kind/--suite/--game-version` 可重复：同一字段取并集，
不同字段取交集。`any` 适用于任意目标平台/游戏版本。`platform` 是行为适用平台，
`host_platform` 是当前适配入口可执行的宿主平台；Android 的 host/JVM 检查
不等于设备测试。目前 native 产物布局适配 Windows Release，清单保留原源码
的可移植性；其它宿主需要确认产物布局和适配后启用。

`--list` 只列清单，所有结果为 `not_run`。`--plan` 核对前提、路径和已存在的
构建凭据，打印命令；可执行的套件仍是 `not_run`，缺依赖是 `blocked`，
宿主不适用/已退役是 `skipped`。二者均不启动套件、编译器、ADB 或 CTest。
配置/source revision 查询会调用只读 Git；JSON 写入配置 build。

不带 `--list/--plan` 才会执行所选 ready 套件；没有筛选时选择整个清单。
runner 始终不编译、下载、部署或自动修复依赖。后续获得授权的纯模拟检查
可以使用以下入口；这不是运行全量测试的命令：

```powershell
python -B scripts/run_workspace_tests.py --suite workspace.registry
```

本轮直接运行同一 `test_suite_registry.py` 的 unittest 发现，只使用合成目录、
小型假凭据和 mock 进程，未执行它所登记的原有测试。

## 配置与输出

入口使用共同 API `load_workspace(config_path=None, repo_root=None)`。
默认配置为 `config/workspace.defaults.json`，本机覆盖为忽略跟踪的
`config/workspace.local.json`。选择顺序为显式 `--workspace-config`、环境
`BE_WORKSPACE_CONFIG`、本机覆盖。PowerShell 对应 `-WorkspaceConfig`，通过
dot-source `Workspace.ps1` 的 `Get-BEWorkspace` 解析工具/路径。

子进程使用 `ws.command('tools.python')` 和 `ws.env()`；每个套件的 cwd 来自
清单，CustomModel 在 `tools/CustomModel`，原 native Python 测试在其原目录。
Python 加 `-B` 并设置 `PYTHONDONTWRITEBYTECODE`。临时文件在配置
`paths.temp/workspace-tests/<run>/<suite>`，日志、case JSON、总结果在
`paths.build/tests/runs/<run>`。运行一次不会产生源码目录的临时副本。

`--output` / `-Output` 指定总结果 JSON；相对路径按工作区根解析，只允许
配置 build 内。输出根先解析符号链接/junction，拒绝与源码、inputs、research、
toolchains、releases、生成资源、安装目录或 legacy 重叠，也拒绝 build/temp
互相包含。list/plan 不创建 temp。已接入的脚本只写 build/temp；保留的未接入
入口是 `catalog_only`，即使原脚本存在也不启动。

`tools.ctest` 可在本机配置选择。未配置时先找配置 CMake 同目录的 CTest，
再查 PATH。不新增默认工具链下载。native 产物期望路径见每项 `entry`：
独立 CTest 在 `paths.build/tests/native/<suite-directory>/Release`，主工程排除
目标沿用构建入口布局，在 `paths.build/windows/win-x64/Release/native/Release`。
其它已有 build 目录应由构建负责人统一
选择布局或更新本机/清单适配，不能把旧 `artifacts` EXE 当当前结果。

## 游戏版本与输入

已确认九月初更新后的游戏版本为 **1.5.3**；此前准确号未给出的旧客户端/
`GameAssembly-old` 对比资料标 **pre-1.5.3**。两版对比同时注明两者。
1.5.3 内部热更新用资源 snapshot ID 区分。BEM 1.0–1.3、BE 软件版本、EFMI
工具版本、工具提交与游戏版本分别记录，不互相推断。

当前资源路径从最新 workspace config 读取，例如 `resource_update.input_root`
指向 `inputs/1.5.3/...`；清单不用写死本机输入目录。版本相关采样、profile
证据和设备入口标 `game_version: ["1.5.3"]`、`status: version_limited`，
配置中的游戏版本不同则阻止运行。纯 BEM、EFMI、Hash-LOD、工具/parser、
模拟行为兼容回归标 `any`，不会因旧格式或旧日期被删。

每个 suite 的 `input` 指定 ID、类型和可选配置键/源码路径。合成输入记录
源码 commit/code revision；持久输入记录路径、配置 game.version 和 snapshot。
输入的原始 manifest/来源维护在持久输入区，不为检查清单遍历/哈希大型资产。
尚未取得来源 snapshot 的证据不得声称覆盖 1.5.3 的所有热更新。

## 游戏与设备入口

真实游戏与设备类型只能在显式 `--allow-game` / `-AllowGame` 后考虑执行。
PC 使用 `test.be_install_dir`；Android 还必须设置 `test.android_serial`，
不使用默认 ADB 设备。源码与回退目录不能充当安装目标。结果保存实际目标，
子进程环境提供 `BE_TEST_INSTALL_DIR`、`BE_TEST_ANDROID_SERIAL`。

现存 `android/Test-CustomModel.ps1`、`Test-ResourceProbe.ps1` 会启动游戏、
可能安装 APK，并写旧 `artifacts` 目录；清单保留它们，适配完成前始终
`blocked`。instrumentation、runtime probe arming 同样保留具体阻止原因。
`--allow-game` 不会绕过这些前提，也不授权刷新/部署。采集脚本正常退出或
生成 log 本身不代表行为测试通过。当前任务禁止执行这些入口。

## 构建凭据

旧二进制存在不能证明对应当前源码，尤其主工程 `EXCLUDE_FROM_ALL` 目标。
runner 不生成凭据，也不把已有 EXE 反向标成新编译。未来构建负责人应在
**实际成功构建对应目标后**写 `entry.receipt` 指定的 JSON。独立 async CTest
中的 capacity/morph 目标也必须单独构建，不能只构建默认目标。

凭据 schema 1 的字段：

```json
{
  "schema_version": 1,
  "suite_id": "camera.firstperson_mesh",
  "source_commit": "实际 git HEAD",
  "code_revision": "同一时间 source_record(repo_root) 的 code_revision",
  "build_succeeded": true,
  "configuration": "Release",
  "tool_revision": "实际编译器/工具链版本",
  "build_command": ["实际成功执行的构建命令及参数"],
  "built_at": "实际 UTC 完成时间",
  "artifacts": [
    {
      "path": "windows/win-x64/Release/native/Release/BetterEndfield.FirstPersonMeshTests.exe",
      "size": 123,
      "mtime_ns": 123456789
    }
  ]
}
```

上例是字段示意，不能保存为有效凭据。`artifacts` 必须与该 suite 清单完全
一致，路径相对配置 build；记录真实 stat size/mtime_ns。检查不哈希大型
native 二进制，但会拒绝文件变化、缺失、配置不符、错误 suite、旧 commit
或不同 dirty code revision。凭据的小文件内容指纹也保存在结果中。

`source_record` 记录 HEAD，并对相关源码范围中变更的代码/配置小文件计算
revision，包括新增/删除代码。显式纳入曾被 `scripts/*` 忽略的工作区 helper，
只检查指定源码目录；不扫描输入树、不哈希历史大文件。变更代码超过 2 MiB
时不自动哈希，revision 不完整就阻止 native 运行。已有 receipt 在启动前再次
核对，避免计划后源码变化。源码可能被其他代理并行修改，最终执行应使用
完成后的 revision。CTest 真执行前还只读发现测试命令，要求发现的可执行文件
恰好等于凭据集合，拒绝旧 CTest 元数据指向其它二进制。

Python 原测试可通过 `BEM_VALIDATOR` 调 native。runner 清除外部继承值，
`creator.bem.format/tasks` 执行 Python 部分并保留实际 skip 计数；只有显式选择
且凭据有效的 `creator.bem.native_validation` 才设置生产 validator。复用相同
测试模块的两个执行模式覆盖 Python/native 两种前提，不复制第二套断言。
archive integration 的旧 backend 路径尚待接入，因此整组先阻止而非自动使用
未确认版本的 7-Zip。

## 结果与退出码

总结果 JSON 包含源码 commit/code revision、小文件变更指纹、配置来源文件
指纹/effective key、registry 指纹、输入 ID/路径/snapshot、命令、cwd、凭据、
执行时长、日志位置及 Python case 统计。不会复制完整本机配置/私有参数。

| 结果状态 | 含义 |
| --- | --- |
| `passed` | 实际套件执行通过；Python 可能有部分 skip，需看 case 统计 |
| `failed` | 实际断言/进程失败、超时或结果格式错误 |
| `skipped` | 平台不适用、已替代/退役，或全部 Python case 被跳过 |
| `blocked` | 缺依赖/输入/凭据、无适配、配置不安全或 unittest 零 case |
| `not_run` | 只列清单/生成计划；没有执行测试 |

unittest expected failures、unexpected successes 和实际 skips 分别计数；不会
把零 case 或全跳过标成通过。退出码：0 表示成功列清单/生成计划或执行无
失败/阻止；1 表示实际 suite 失败；2 表示执行受阻、空选择或入口配置错误。
list/plan 中依赖缺失可正常生成报告并退出 0，不代表这些 suite 已通过。

## 套件目录与临时验证收口

| 模块 | 正式 suite ID / 原有维护来源 |
| --- | --- |
| workspace / resource | `workspace.registry/config`、`resource.workspace/character_presets`；三个 workspace 测试文件分别由测试、parent、资源负责人维护 |
| host | `host.hooks` → `native/tests/hook_chain`；`android.host_rebuild` 保留 Linux 编译模拟入口，`android.host_java` 保留原连接时序/overlay geometry Java main 检查 |
| camera / firstperson | `camera.playback/firstperson_profiles/firstperson_mesh/profile_evidence` → 原 camera CTest、profile CTest、mesh 排除目标和 `test_generation.py` |
| custom_model | `custom_model.core/matcher/loadstate/binding/android_loadstate/desktop_loadstate/android_stream` → 原 core、matcher、scheduler、binding、桌面/Java state 与 stream 入口 |
| actions / combat | `actions.policy/effects/external/pose`、`combat.semantics` → 原排除目标，不依赖真实游戏 |
| creator 格式与工程 | `creator.bem.format/projects/tasks/native_validation` → 原 BEM 各代模块、项目/任务模块和生产 validator |
| creator 来源与转换 | `creator.efmi/hash_lod/native_catalog/source_archive` → EFMI、Hash-LOD、native 图/材质证据、RAR/7z 原模块 |
| creator 记录与纹理 | `creator.runtime_records/textures/android_mesh_api` → 原 request/sweep 聚合、DDS、离线 ELF/stub 检查 |
| third_party | `third_party.host/bridge/navigation/sdk/ui/materializer` → 原 Python、JS、C#/Java 脚本；需构建或无配置适配的入口保留但阻止 |
| AndroidMMD | `androidmmd.audio/import` → 原 `native/tests/android_mmd_*/Run.ps1`；需独立 JDK 编译/本地 jars，无自动下载 |
| game / device | `android.bem_manifest/bem_instrumentation/custom_model_device/resource_probe`、`custom_model.runtime_probe`；manifest 是 static，其余真实采集/设备入口被门控 |

完整精确 ID、kind、平台和状态以 JSON 为准；上表斜线是同前缀分组简写。
`prerequisite` 支持 tool、配置 path/value、源码 fixture 和人工前提；未知条件
记录阻止原因。AndroidMMD 是模块名称，不能把它或 BEM/EFMI 的版本当游戏版本。

清单 `validation_catalog` 记录当前入口替代关系：

| 既有验证资料/命令 | 现行归属与保留边界 |
| --- | --- |
| `FIRST_PERSON_HEAD_ACCESSORIES_20261003.md` 中局部 artifacts 编译/运行命令 | `camera.playback`、`camera.firstperson_mesh` 统一其 host 回归入口；保留原历史结果和视觉/设备限制 |
| `FIRST_PERSON_PROFILES_20261003.md` 中 profile 测试命令 | `camera.firstperson_profiles`、`camera.profile_evidence` 统一已覆盖回归；geometry helper、实际来源 audit 仍活跃 |
| `native/tests/async_loading/README.md` 的旧 artifacts CTest 命令 | `custom_model.core/loadstate` 使用配置 build + receipt；保留全部 concurrency、容量、morph 兼容边界 |
| CustomModel README 的原全目录 unittest discover | 仍是同一实现维护来源；清单按组选择，native/archive 前提单列，未删除原发现入口 |
| Android probes、第一人称几何/ABI 研究与实机采样 | 1.5.3/snapshot 相关，仍活跃；离线模拟不能替代运行时/视觉证据，旧客户端对比标 pre-1.5.3 |

这里“已替代”只指临时执行入口被正式 suite 收口，不能把旧实机结论改写成
本轮通过。历史资料路径以清单所记源码提交为证据；迁移后的定位可查
`config/documents.json`。没有确认行为、输入域和预期完全等价时，不删除诊断
脚本、旧版本回归或独有用例。此轮没有删除/归档原测试。

新增清单项须保持 ID 稳定，填齐 `module/kind/platform/game_version/input/
prerequisite/entry/status/replaced_by` 和 purpose；退役写原因，已替代项填可达
的 successor ID，禁止环。实现改变或出现新用例时再作相关验证，不反复跑
已通过的无关大测试。

当前仓库曾用 `scripts/*` 忽略非发布脚本。parent 应在其 `.gitignore` 写范围
补充 `scripts/run_workspace_tests.py`、`scripts/Test-Workspace.ps1`、
`scripts/tests/` 及目录下源文件的跟踪例外；测试负责人不越界修改它。

2026-10-05 本轮核对：47 个 suite 已登记；仅 `workspace.registry` 的 17 项
纯模拟检查实际执行并通过。list/plan、PowerShell 筛选转发/显式 config 和
Android opt-in 后仍要求 serial 的计划核对通过。报告在配置 build 的
`tests/inspection/`，其中 `registry-simulation.json` 是模拟执行结果，
`catalog-list.json` / `catalog-plan.json` 是清单/计划，不能解释成全量测试通过。
