# 任务 D：逐角色第一人称资料与 helper

基线 `main dd7131b4`，直接叠加现有工作区。职责限定为本记录及下列新增文件；camera runtime 接入、Android MeshData 适配和最终构建/安装由主代理负责。

## 接入接口

`native/modules/camera/first_person_profiles.h`，命名空间 `BetterEndfield::FirstPersonProfiles`：

Android 同步需要一起包含 `first_person_profiles.generated.inc`；两者没有平台 SDK 依赖。

- `LookupProfile(modelId)`：严格匹配真实角色 ID 或已经观测的 postmodel/uimodel 根，未知模型返回空。
- `MatchBonePath(profile, path, revision={})`：接受 actor-relative 路径或该角色的完整根路径。返回 `semantic` 0=保留、1=隐藏、2=Neck，以及匹配规则和资料来源。没有匹配规则时继续现有通用判定；外来根不匹配。
- `BoneRule::region`：Body / Head / Neck / Tail / HeadAccessory。局部规则的 `head_weight` 计 Head 和 HeadAccessory，**tail 计入 other_weight**。
- `FindLocalRule(profile, rendererPath, lod=-1, revision={})`：严格匹配已有部件路径和实际 LOD，范围包含 face/hair/head/fur，以及 palette 明确引用已录入帽链的局部头饰部件。没有 `hide_all` 接口。
- `ShouldHideLocalVertex/Triangle(rule, evidence)`：读取当前实际非零权重。要求头颈占比 ≥0.5；fur/头饰范围还要求真实 Head/头饰贡献。每个三角形的三个顶点都必须通过。

空间量使用同一 Mesh 坐标系内的 Neck→Head 帧：`along = dot(position-neck, unit_axis)/length`，`radius = distance_to_axis/length`。必须有有效帧，边界为 `along∈[-0.5,4]`、`radius≤4`。这是按用户“FP 视线优先、允许多裁局部衣领”的授权制定的政策阈值，不是全角色逐点测得的尺寸。无帧时不启用局部例外，纯 Head/tail 仍走已有规则。

原版和 BEM 均依据**当前** palette、draw、weights、位置；原始 Mesh 名仅作来源显示，不用于整块删除 BEM。主代理保留完整源投影、恢复及状态所有权；helper 不修改 Transform 或 BoneScale。

## 覆盖与证据

- 从 `F:/zmd_bem/research/identities/roles/*/native.json` 生成 32 个角色、64 个 world/UI 主骨架来源。实际来源均为 Windows，manifest 字段为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`；它不保证不同 payload 相同。
- Head/Neck、tail 根及祖先路径均从真实 Transform→GameObject 图解析。保留整个基本骨架为默认值，只覆盖精确 Head/tail/头饰根；不会放大到 Spine2。生成数据没有固定 palette index。
- 6 个角色的 8 条 Head 外帽链已按最新授权启用：男女管理员、佩丽卡、弧光、汤汤、决。帽链语义标记为 `NamePathInferred`，没有冒称名称本身是完整几何验证。
- 共 420 个来源范围记录，world/UI 重复来源分别保留，只录入已有可验证 LOD。外来依赖根、士兵/武器嵌套骨架、shadow proxy 都排除。
- 噗切娜单列为第 33 个**补充范围 profile**，由已有 catalog 的直接 world/UI LOD0 路径提供，未伪造完整骨架图。fur03 使用既有 PC 权重证据：26,100 个纯 Head 顶点、2,500 个 Head/Neck 混合顶点，无纯身体顶点；仍执行当前几何的局部判定。renderer 路径是 `...fur_03_lod0`，Mesh 名才含 `_20`。
- 安塔尔、洛茜、庄方宜、决的 native 文件保留既有读取警告；本表验证当前实际解析出的主骨架和部件引用，没有宣称所有 bundle/class 解析无缺口。同名 collider 不算语义根。
- Android 可在实际 live ancestry/path 确认后复用结构提示；这些 Windows 权重和 LOD 记录不成为 Android 原版权重实测。传入未知 revision 时没有 profile 例外；缺版本元数据时只有具名路径提示。新头像仍回退通用规则。

## 新增行为的局部验证

独立回归只读既有 raw VB/IB，按原始 UNorm16 等声明解码权重与实际 submesh draw，不用重建 m_Skin。逐 palette 路径分类和局部顶点裁剪均调用实际 C++ helper；不导出原版几何。8 个角色的 25 个 world LOD0 Mesh 完成校验，0 个 unsupported；其中 12 个样本具备所需 Head/Neck bindpose 帧。

| 角色 | 新增纯帽链面 | 新增局部边界面 | 样本中保留面 |
| --- | ---: | ---: | ---: |
| 管理员（男） | 767 | 0 | 22,298 |
| 管理员（女） | 601 | 480 | 33,216 |
| 佩丽卡 | 1,467 | 128 | 24,375 |
| 弧光 | 208 | 512 | 25,444 |
| 洁尔佩塔 | 0 | 120 | 0 |
| 汤汤 | 3 | 108 | 6,493 |
| 决 | 159 | 1,235 | 18,336 |
| 提弗洛斯 | 0 | 242 | 0 |

这些是本次政策在指定原版 PC 样本上的实际 draw 分类计数，不能代替 Android、BEM 所有外观或游戏画面验收。提弗洛斯 face 新增 118 面、hair 新增 124 面；校园/逆兔包的 96 个混合顶点和 124/248 个提交面仍按既有文档作为包证据区分。女管理员 cloth 的新增 181 面、弧光 cloth 的 492 面、决 cloth 的 1,120 面是局部裁剪，三者仍分别保留 33,216 / 24,339 / 16,348 个衣装面。

独立 CMake 测试通过 2,163 项 C++ 检查；Python 5 项资料安全回归通过，覆盖未知模型/版本、Head/Neck/Spine2 边界、外来根与嵌套骨架、实际路径与 Mesh 名差异、身体保留、损坏图拒绝。生成一致性及 raw geometry regression 的 `--check` 也用于核对现有资料。没有游戏启动、GPU 性能实测或 APK 全量构建结论。

## 新增文件清单与复跑

- `native/modules/camera/first_person_profiles.h`
- `native/modules/camera/first_person_profiles.generated.inc`
- `native/tests/first_person_profiles/CMakeLists.txt`
- `native/tests/first_person_profiles/profiles_tests.cpp`
- `native/tests/first_person_profiles/test_generation.py`
- `tools/FirstPersonProfiles/generate_profiles.py`
- `tools/FirstPersonProfiles/profiles.generated.json`
- `tools/FirstPersonProfiles/audit_local_geometry.py`
- `tools/FirstPersonProfiles/geometry_regression.generated.json`
- `docs/FIRST_PERSON_PROFILES_20261003.md`

局部构建目录为 `artifacts/first-person-profiles-tests-20261003`；未 clean 或删除目录。复跑使用用户指定 Python/CMake：

```powershell
$fpPython = 'C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.13_3.13.3824.0_x64__qbz5n2kfra8p0\python3.13.exe'
$fpCmake = 'D:\work\Visual Studio\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe'
& $fpPython -B tools/FirstPersonProfiles/generate_profiles.py --check
& $fpCmake --build artifacts/first-person-profiles-tests-20261003
& artifacts/first-person-profiles-tests-20261003/first_person_profiles_tests.exe
& $fpPython -B tools/FirstPersonProfiles/audit_local_geometry.py --check
& $fpPython -B native/tests/first_person_profiles/test_generation.py
```

首次配置仅针对独立测试目录，可使用 MSVC/Ninja 或现有 GNU 编译器；与现有 camera_tests 无共享源码修改。所有基础表和统计都是可再生成的数据，不把 32 角色名单或候选名称等同于 32 角色实机优化完成。

## 追加（同日）：安卓提弗洛斯残留头饰与无几何回退

### 手机日志结论

`F:/zmd_bem/tmp/android_logs_1003/diag.log`：4998 行起的会话只解析了契约，没有进入第一人称；5378 行起的会话依次绑定洁尔佩塔、提弗洛斯（generation=2，5824 行）、噗切娜。本次 BEM 替换失效，提弗洛斯是**原版模型**。

- 该会话 `clone/index bindings unavailable, GPU readback unavailable`（5741 行）：`Mesh.GetSubMesh` 在安卓 IL2CPP 中缺失（`fp.mesh.submesh` 契约 not found），托管后备因此整体不可用；原版 Mesh 不可读、GPU 回读 icall 缺失，混合部件没有任何几何来源。
- 提弗洛斯被整块隐藏（全部 Shadow 模式为 Off 的独立部件）：brow、cloth_04（尾部石块）、cloth_05、eyeshadow、face_01、hair_01、iris，lod1–lod3。没有任何裁剪记录。
- 未处理且**没有日志**的部件：`cloth_02`、`cloth_01`、`body_01`（名称不含头部提示词，`!engine_ready` 分支只给 matched 部件打日志）以及 `hairshadow_01`（名称过滤直接跳过）。日志中的 `First person mesh scan: visited=537 ...` 因计数初值总是只输出 `...`，看不到 Renderer 列表。
- 噗切娜 `face_01_lod1/2/3` 同理落入 “clone bindings unavailable; retaining uncertain mixed geometry”。

### 残留部件（离线真实网格复现，world lod1）

| 部件 | 骨骼 | 当前规则可隐藏面 | 原因 |
| --- | --- | ---: | --- |
| cloth_02 | 98 身体 + 27 个 Head 子树骨骼：`Bip001_Head` 直绑头饰、`hair_L/R_base_a → hair_L/R_bowknot_a_*`（蝴蝶结）、`hair_L/R_side_a_*` | 1,358 | 与身体同一 Renderer、单一 submesh；骨骼语义早已是 Hide，安卓无几何不能裁剪 |
| cloth_01 | 110 身体 + Neck + 9 个 Head 子树（`hair_L/R_side_a_01..04` 侧发饰） | 112 | 同上 |
| hairshadow_01 | 17 个 Head 子树，纯头部 | 110 | 名称过滤把它当影子代理跳过 |
| body_01 | Head 1 + Neck 1 + 身体 | 8 | 颈部顶端，同上 |

因此不是骨骼表/帽链漏录、权重阈值或 Neck 边界问题，而是**通用执行缺口**：任何角色在安卓原版下，混合 Renderer 中的头发/头饰都无法处理；表格中其它 7 个角色同样存在（见下方回归）。

### 改动

1. **通用无几何回退：调色板塌缩**（`native/modules/camera/first_person_runtime.inc` `FpRedirectRoot` / `FpCollapseAnchor` / `FpTryPaletteRedirect` / `FpRedirectCurrent`）。当混合 Renderer 无法取得或裁剪几何（`!engine_ready`，或 `FpBuildPatch` 报告 `no_geometry`），且该 Renderer **自身不投影**（ShadowCastingMode=Off，HG 由独立 Shadow_Proxy 投影）时：复制其 `bones` 数组，只把实时 Head 子树、尾链（最高的 tail 祖先）、角色表 HeadAccessory 根下的条目指向该根下的私有零缩放子节点 `BetterEndfield_FpCollapse`，`set_bones` 后读回校验。骨架、动画、其它 Renderer、Shadow_Proxy 均不改；不缩放任何原有骨骼。完全由这些骨骼驱动的三角形塌成一点；与 Neck/身体混合的顶点按权重向根移动（观感优先授权）。自身投影的混合部件不塌缩，并记录 `shadow-casting mixed part without readable geometry retained`。
   - 恢复：仅当 Renderer 当前 palette 仍是我们写入的数组时写回原数组；BEM/游戏写入的新 palette、mesh 变化、投影模式变为非 Off、锚点缩放被改、原骨骼父级变化都会释放该 patch。锚点按名称复用且不主动销毁，随模型销毁，避免被外部捕获的 palette 指向已销毁 Transform。
2. **托管后备不再依赖 `Mesh.GetSubMesh`**（`FpReadManagedSubmesh`；`module.cpp` 新增可选契约 `fp.mesh.index_start/index_count/base_vertex`）：依次用 GetSubMesh → GetIndexStart/Count/BaseVertex → `GetIndices(submesh,true)` 长度的连续区间；之后仍逐值比较索引。BEM（可读）网格在安卓可重新走“裁剪副本 + 完整源投影”的精确路径。
3. **hairshadow**：只有 `shadowproxy` 视为影子代理；`hairshadow` 在 palette 全为 Head/tail 时按独立部件隐藏，混合时不裁剪、不塌缩（`FpShadowProxy` / `FpHairShadow`）。
4. **诊断**：Renderer 树每个绑定代次完整记录一次（最多 128 项），不再每 30 帧输出一行 `...`。
5. `module.cpp`：新增可选契约 `fp.renderer.bones.set`、`fp.array.clone`、`fp.array.set_value`、`fp.game_object.ctor`、`fp.transform.set_parent`、`fp.transform.local_position.set`、`fp.transform.local_rotation.set`、`fp.transform.local_scale.get/set` 和 `GameObject` 类；任一缺失时回退不启用，行为与旧版相同。

角色表与生成数据未改：提弗洛斯的蝴蝶结/侧发/头饰骨骼本来就在 Head 子树中，补骨骼路径没有意义。未录入角色走同一通用回退（Head 子树与 tail 名称不依赖角色表）。

### 离线回归

`tools/FirstPersonProfiles/audit_android_fallback.py`（结果 `android_fallback_regression.generated.json`，`--check` 校验，并断言提弗洛斯残留下降）用 8 个角色的 Windows raw VB/IB 按安卓无几何策略重放 world lod1。“残留”为三个顶点完全属于 Head/头饰/尾部语义、仍在绘制的三角形；“移动”为部分塌缩的三角形。

| 角色 | 残留 前→后 | 移动面 |
| --- | ---: | ---: |
| 提弗洛斯 | 1,588 → 0 | 328 |
| 管理员（男/女） | 1,015 → 0 / 352 → 0 | 1,277 / 1,505 |
| 佩丽卡 | 1,702 → 0 | 1,482 |
| 弧光 | 16,442 → 111 | 1,432 |
| 洁尔佩塔 | 390 → 0 | 114 |
| 汤汤 | 4,688 → 0 | 1,235 |
| 决 | 1,506 → 0 | 1,139 |

提弗洛斯明细：cloth_02 1,358 面全部塌缩、身体面 0 移动；cloth_01 112 面塌缩、172 个衣领面移动（位移 p90 3 mm）；body_01 8 面塌缩、156 个颈部面向 Head 收拢（p90 7.5 cm，最大 16.5 cm，相当于收口）；hairshadow 110 面改为整块隐藏。弧光剩余 111 面横跨多个帽根，塌成锚点间的小三角形。帽链/尾链混合顶点位移最大约 14–15 cm（兜帽、尾根附近）。前提：混合 Mesh_all 部件在安卓不自身投影（日志中提弗洛斯全部被处理部件均为 noncasting）；运行时逐个核对，不满足则保留。

### 测试与构建

- `artifacts/first-person-typhoea-residual-20261003`（MSVC，camera_playback）：CTest 12/12 通过；`camera_head_hide_tests` 530 项检查，新增塌缩/锚点复用/外部 palette 接管/投影模式变化/锚点缩放被改/父级变化/尾根与帽根/契约缺失/hairshadow 用例；`camera_portable_mesh_tests` 40 项，新增无 GetSubMesh 的两级回退。
- `generate_profiles.py --check`、`audit_android_fallback.py --check`、`test_generation.py` 通过。`audit_local_geometry.py --check` 的既有 helper（`artifacts/first-person-profiles-tests-20261003/first_person_profiles_tests.exe`，GNU 构建）缺运行库 DLL 无法启动（0xC0000135），与本次改动无关，未重建。
- Windows `BetterEndfield.Camera`（D:/work/BetterEndfield-checks/main-merge-20261001，Release）编译通过；Android arm64 Release `:app:assembleRelease --offline --no-daemon` 成功（日志 `android-assembleRelease.log`，包含当时工作区中其它代理的改动）。未安装、未启动游戏。

### 需实机确认

- 日志应出现 `head palette collapsed on non-casting mixed part S_actor_typhoea_cloth_02_lod1 ...`（cloth_01/body_01 同理）以及 hairshadow 的 standalone 隐藏；若出现 `shadow-casting mixed part ... retained`，说明安卓该部件自身投影，需要另行处理。
- 视觉：蝴蝶结/头饰/侧发是否消失；衣领与颈部收拢、兜帽/尾根附近是否有可见拉伸；人物投影（含头饰）是否保持完整。
- BEM 恢复后：托管裁剪路径是否成功（`BEM CPU geometry, clone layout verified`），失败时会回落到塌缩。第一人称期间热切换 BEM 包时，若 BEM 恰好捕获了被塌缩的 palette，第三人称可能残留塌缩，需要观察。
