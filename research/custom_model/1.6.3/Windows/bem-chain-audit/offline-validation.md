# BEM 离线链路检查

日期：2026-10-15。Windows 客户端 1.6.3 / BE 4.0.3；手机导出样本来自 Android 1.6.4。本记录覆盖包与配置链路及生产绑定逻辑的模拟回归，不把主机模拟测试等同于游戏实测。

## 结果

| 检查 | 本轮实际结果 |
| --- | --- |
| Windows 管理器 `ManagerChecks --v14` | 通过：1.4 明确资源目标、平台过滤、资源冲突、启停持久化、畸形目标拒绝 |
| Windows 管理器完整导入路径 | 通过：严格验证暂存字节后导入、初始禁用、启用/禁用、同目标冲突、更新外观回退、删除、ZIP 部分成功、坏包不修改库 |
| `CreatorProjectChecks` | 通过：重开、编辑、包身份保留、另存路径、禁止输出覆盖输入及形变资料 |
| `BemV14ResourceTests` | 53 项通过：共享解析器、逐资源载荷/形变选择、静态网格、跨资源 donor 拒绝、LOD 矛盾拒绝、registry 与纹理改写保留目标身份 |
| `CustomModelBindingTests` 主套件 | 通过：A→B→A 热切换、选项身份、禁用恢复、失败回滚、原始 donor、材质/骨骼独立归属、静态 MeshFilter、场景克隆重新绑定、非 prefab 类型拒绝 |
| `--first-enable` | 通过：未曾替换过的世界/详情缓存模板、自然 clone、明确资源 static/ultimate、快速配置变更、首次扫描重试、禁用恢复 |
| `--clone-support` / `--npc-roots` | 通过：同步投递、私有材质、代际及骨骼隔离；同包队伍/NPC/clone、原始 prefab 交接、歧义及 donor 拒绝、禁用恢复 |
| `--android-discovery` | 通过：Android 明确 LOD1 static/skinned prefab+clone、Windows LOD0 边界、Original 禁用/重启用、模块清理；原有详情 LOD0→世界 LOD1 配对路径 |
| `AndroidWorldBindingTests` | 213 项通过：实际 Android adapter 的配对发布/回滚、当前 prefab 名、共享纹理、活体骨骼及影子路径，运行于主机模拟对象 |
| Java `OverlayWritePolicyTest` / `BemHotSwitchUpdateTest` | 通过：安装器共用选择写入策略、参数/选项保留、排他启用；A↔B、重试、过期候选拒绝、全禁用与参数单独更新 |

全部 native 目标均在本轮新建的 `build/bem-chain-audit-4.0.3/native` 中从当前源码编译。`custom_model_binding_tests.cpp` 直接包含生产 `module.cpp`，共享核心为当前 `bem.cpp/mod_registry.cpp`；Android world 测试包含生产 Android adapter。C# 测试直接链接当前生产服务；Java 测试本轮用 JDK 21 / `--release 17` 编译生产 `BemOptions/BemParameters/OverlayWritePolicy/BemHotSwitchUpdate`，没有使用旧 class 假通过。

构建保留两条已有 warning：`model_clone_lineage.inc` 局部变量隐藏、第三方 hde64 的可能未初始化局部变量；本轮未扩展修改这些代码。

## 修复及输入边界

发现 `ManagerChecks.csproj` 未链接后来新增的 `BemToolService`，当前源码原本报两处 CS0103。仅补充实际生产服务文件链接，未用假的校验器，也未改生产逻辑。

多资源示例的 `weapon` 原本为 LOD1，适合格式解析测试，不适合作为 Windows 首启成功样本。直接拿它跑首启，weapon 被既有 Windows LOD0 规则拒绝；改用同一生成器创建、仅将该资源声明改成 LOD0 的合成派生包后通过。Android discovery 另用三资源均显式 LOD1、`renderer_path=Mesh_all/lod1/<mesh_name>`、包含 `android-arm64` 的合成派生包。没有放宽运行时边界来让测试通过。

BEM 1.4 的顶层 `target.platform` 沿用格式规定的 `windows-x64`，**实际多平台适配由各 `target.resources[].platforms` 决定**。不能把顶层字符串直接改为 `android-arm64`；当前生成器会拒绝。

## Windows / Android 目标身份一致性

| 层 | 使用的身份及约束 |
| --- | --- |
| Windows `BemPackageService` | `TargetKind:TargetId`；1.4 使用 `windows-x64:<resource>` 的交集判冲突，不让 Android-only 包导入或启用 |
| JNI `installer/install_jni.cpp` | 调用同一 native `ReadBemPackageInfo/LoadBem` 验证载荷，输出 `target_kind/target_id/resource_keys/bem_minor` 给 Android；直接导入保持包原始字节 |
| Android `BemOptions/BemInstaller` | 同一 owner key；校验 resource key 平台及规范名；只按 `android-arm64:<resource>` 冲突；拒绝无 Android 目标，转换更新保留 owner、resource keys、选择及启用状态 |
| native `mod_registry` / receiver | 按资源和平台生成 adapter；逐组件 renderer kind/path、mesh/index/material/bone 与 LOD 合同仍必须成立，`skip_validation` 不绕过身份矛盾 |

不同前端的默认启用交互没有被误认为格式矛盾：Windows 新导入默认禁用，Android 新安装记录默认启用；两边明确保存的 `enabled` 均进入 native 配置。首启、热切换、停用恢复采用该保存状态。

## 真实凛包导入与实机准备

源文件：`inputs/1.6.4/Android/bem-validation/tohsaka-rin-aglina-1.2.6-device.bem`，10,077,562 字节，BEM header **1.0**。未重新打包。

用当前发布构建内实际 `tools/BemConverter/BetterEndfieldNext.BemConverter.exe` 做严格暂存校验，再由当前生产 `BemPackageService.ImportAsync` 导入。初始禁用检查通过，然后明确启用并保存热切换。新的 native parser 同样接受该原包：10 个组件、15 张纹理。

- 包 ID：`native.hiragara.tohsaka-rin.aglina.prototype`
- target：`character:chr_0013_aglina`，支持当前 Windows 平台。
- 隔离 Root：`build/bem-chain-audit-4.0.3/live/local/BetterEndfieldNext/catalog/custom-model`
- 包：上述 Root 下 `packages/native.hiragara.tohsaka-rin.aglina.prototype.bem`，导入后仍为 10,077,562 字节。
- 配置：上述 Root 下 `runtime.ini`；`enabled=true`、`appearance=rin`、`hot_switch=true`、`skip_validation=false`，`fast_loading=false`、`clone_support=false`。

导入入口保留为 `ManagerChecks --import-real <isolated settings root> <converter install root> <source.bem>`。它只接受当前工作区 `build/bem-chain-audit-4.0.3` 内的空隔离库，避免写真实模型库。测试不会清理这份特意留给实机验证的库。

本代理未启动游戏、未安装 XInput、未改用户真实库、未修改发布包。Windows 实机加载/停用恢复由根代理另行记录；Android 因手机尚未下载语言资源，本轮仅静态及离线验证。

## 复现入口

统一构建目录：`build/bem-chain-audit-4.0.3`。native CMake targets：`BetterEndfieldNext.CustomModelBindingTests`、`BetterEndfieldNext.BemV14ResourceTests`、`BetterEndfieldNext.BemValidate`、`BetterEndfieldNext.AndroidWorldBindingTests`。

合成 BEM 1.0 来自 `tools/CustomModel/test_bem_v1.py:fixture()`，1.4 来自 `examples/multi-resource/create_project.py --native-fixtures`，派生输入保留在 `build/bem-chain-audit-4.0.3/fixtures`。C# 项目运行时传 `-p:BEWorkspaceBuildRoot=G:\Better Endfield\build\bem-chain-audit-4.0.3`。Java 主机输出在该 build 根的 `android-host`，JSON 依赖是工作区已有 `toolchains/testing/android/json-20250517.jar`。
