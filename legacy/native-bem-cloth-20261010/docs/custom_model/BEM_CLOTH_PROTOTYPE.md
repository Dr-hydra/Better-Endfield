# BEM 衣物物理原型：开发记录

## 当前交付：2026-10-10 / 测试 002

开发分支已快进到 main 的 `c2f8eafc`，保留现有物理、UI 模型来源追踪修改。手机上的 001 请求在建骨阶段崩溃，重启后拒绝重放；因此此前画面不能证明新 BoneCloth 运行过。对应定位报告为 `artifacts/native-bem-tohsaka-aglina-20261008/reports/body-collision-diagnosis/README.md`。

002 修正 GameObject / Component 的 receiver 类型错误，为新团队配置 Edge 身体碰撞，保留六个原生碰撞体并增加 42 个拟合腿部球形碰撞体。新增 Process colliderList、teamIdSet、活动角色归属、启用状态、尺寸/缩放和实际碰撞模式的读回检查；记录关节相对骨盆的位移。拟合形状属于测试候选，不修改原生共享身体形状。面部 1.2.2 改为只对连通皮肤壳生成连续法线。

Android release 编译及 APK 签名校验通过；NPC roots、instance lineage、生产 BEM 解析、骨骼换绑静止不变量与离线原生契约检查通过。APK、BEM 1.2.2、authoring 和请求 `rin-bonecloth-002` 已打包到 `artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-Next-4.0.0-fix-002.zip`。**本版未部署、未完成游戏内碰撞或脸部效果验收，层序保护仍未实现。** 不应把注册和位移读数当成显示面碰撞已解决的证据。

物理仍需旁置 authoring / request；APK 或普通 BEM 单独安装不会启用测试。现行 BEM 为 1.0–1.4，没有发布物理扩展。

## 以下为历史阶段记录

状态：**Android 接口与配置复制已实测；双层模拟、碰撞注册、显示回写尚未实现。** 本文不是 BEM 1.5 正式规范。现行 BEM 仍为 1.0–1.4，不能把原型 JSON 放进 `.bem` 后声称它具有物理能力。

开发分支：`feat/better-endfield-next`，基于 `bde0adfb`。检查功能直接进入 Next 的内置 custom_model 模块，不依赖第三方模块入口，不改变 BEM/MMD 的安装身份。

## 已取得的 Android 证据

2026-10-09，在已连接设备上安装 Next 检查构建，启用游戏作用域、导入凛 1.1.2，读取运行时加载的 aglina 场景与 UI 资源。

- `BeyondBoneCloth` 的 `BuildAndRun`、`get_Process`、`get_SerializeData`、`ResetCloth`、`SetParameterChange`、`SetSkipWriting` 方法按完整签名解析成功。
- 原生 `SelfCollisionMode.None`、`FullMesh` 枚举按名字解析成功，没有使用硬编码数值。
- 调用了独立新建 `ClothSerializeData` 的构造与 `Import(original, true)`。检查的七个引用字段均与原配置对象不同，原配置的序列化结果在操作前后完全一致。
- 场景资源读取到 `MC_coat`、头发、尾巴、飘带等五个布料组件。`MC_coat` 的自碰撞、配对碰撞均为 0，原生身体碰撞体为六个。UI 资源没有读取到相同 `BeyondBoneCloth` 组件，不能假设两条路径相同。

这些结果证明方法可解析、配置可复制。**没有调用 BuildAndRun，没有创建求解团队，也没有证明 FullMesh 已经注册或正在执行。** 当前记录是已加载资源的配置证据，不能替代活动角色实例、求解器状态和碰撞效果的检查。七个字段的对象隔离也不是全部嵌套数据都无共享引用的证明。

## 制作端试验数据

`tools/CustomModel/cloth_authoring.py` 校验 `kind=bem-cloth-authoring-prototype` 的独立 JSON。该工具不修改 BEM 解析器、不输出带有新物理能力的 BEM，也不会把拓扑校验等同于原生模拟验证。

凛的最终显示网格作为对应关系来源，而不是使用 FBX 导出前的顶点编号：

| 区域 | 代理点 | 代理三角面 | 腰部固定点 | 对应显示顶点 |
| --- | ---: | ---: | ---: | ---: |
| 内裙 | 372 | 624 | 42 | 481 |
| 外裙 | 528 | 899 | 54 | 773 |

两层分别建立拓扑，声明一对 surface-surface 碰撞关系。外裙两个未连接腰部的小装饰块不作为独立模拟岛；其 72 个显示顶点通过代理三角形、重心坐标与三角形局部 TBN 偏移跟随布料，保留静止位置。固定顶点从腰部区域选取，不能为了通过检查给悬空部件随意添加骨盆固定点。

显示对应关系包括三条顶点流和索引流按顺序连接后的 SHA-256，避免在模型重导出后误用旧映射。单位为米，坐标为导出的 serialized mesh local；Android 的实际接收器坐标转换仍须由运行时适配并验证。

校验覆盖：有限坐标和预算、三角形范围与退化/重复、每个模拟岛的固定点、显示顶点唯一写入者、表面绑定重心坐标与偏移、碰撞配对。报告明确输出 `wire_format_supported=false`、`native_solver_verified=false`。

## 格式定稿时需要的数据

拟议的物理扩展应基于显式资源目标，最终版本号和能力名称在执行链验证后确定：

1. `surfaces`：模拟代理的点、三角面、连接关系与固定区域。大数组放进有明确长度和布局的 payload。
2. `bindings`：代理到所选显示 mesh 的顶点/三角形映射、静止偏移与原始几何身份；材质缝连续，内外裙独立。
3. `anchors`：资源内骨骼绑定、局部坐标和约束。若后端需要辅助骨骼，显式描述并由加载器拥有其生命周期。
4. `colliders`、`contacts`：已有身体碰撞体引用、必要的模型专用碰撞体，以及层间/自碰撞关系和接触厚度。
5. `profiles`：原生后端与参数来源，明确单位、范围、平台和资源；不存进程地址、Unity 对象指针或跨平台直接复制的 Team ID。
6. 能力声明和失败策略：需要物理的包遇到不支持的加载器应明确拒绝；若作者提供静态降级外观，须显式选择。不能静默丢弃物理配置。

现有 manifest 数值约束为 UInt32。浮点位置、矩阵和曲线要设计有版本的二进制 payload，或使用明确单位的定点参数，不能直接把本试验 JSON 当成正式 manifest。

## 后续执行链的验收门槛

- 在活动角色实例上确认原生网格布料/骨骼布料的创建路线、选点数据和构建结果；优先复用原生求解器。
- 为两层建立独立的候选对象，逐项检查可变数据隔离；失败只销毁候选对象。
- 检查团队实际注册、代理点/面数量、配对关系以及有效碰撞参数。仅有 FullMesh 枚举或 BuildAndRun 返回值不算碰撞通过。
- 接入动画、模拟与显示回写顺序，保证一个显示顶点只有一个最终驱动者；处理显示法线/切线与边界。
- 将物理加入现有模型提交/恢复事务，覆盖换装、热切换、切人、传送、LOD、场景退出和构建失败；禁止两个实例共享可变求解器状态。
- 最后接入 BEM 打包/解析/选择/导入链路，并分别验证 Android 和 Windows。当前 MMD 布料状态控制不是这一执行链的替代品。

## 内裙静止轮廓检查

独立还原此前加在内裙上的间隙修正后，低处裙摆横向宽度从约 0.4129 米变为 0.3892 米，差约 23.7 毫米；最大单点修正约 21.3 毫米。两个静止版本均未检出内外裙三角面相交。

这支持“内裙被先前的间隙修正撑宽”的判断，但不是动作验证。该还原已写入正式包 1.1.3 并安装到 Next，外裙没有改动。物理代理已据此重新导出，再结合腿部与裙层动态碰撞检查。

## 检查构建的使用范围

`android_cloth_probe.cpp` 在已有 Unity 线程模型维护边界执行。只有游戏私有 cache 中存在 `bem-cloth-probe.request.json` 才工作，限制为 aglina、最多八个资源根、每根最多六十四个布料组件。`inspect` 仅读取；`clone-configuration` 只操作新建的配置对象。结果写入同目录 `bem-cloth-probe-response.json`。没有新增界面或第三方模块路由。

普通 BEM 1.1.3 仍为蒙皮包，不应标注“物理版已完成”。

## 双表面构建实验

实验只创建私有、不可见的原生 MeshCloth；不接管角色显示网格。内裙 354 点 / 624 面 / 42 固定点，外裙 528 点 / 899 面 / 54 固定点。手动选区使用命名枚举及运行时字段验证。

首次设备实验在选区 JSON 回读时触及 32 KiB 字符串限制，已改为托管数组及字段逐项检查。构建成功仍须分别证明配对碰撞注册、实际避碰和显示绑定，不能用 `BuildAndRun` 的返回值代替。成功、失败、超时、移除请求以及模型关闭均销毁实验对象并检查清理结果。

### Android 002 崩溃与后续限制

`rin-next-proxy-pair-002` 的两个 `BuildAndRun` 均返回 true，最后采样的 TeamId 均为 0，随后发生 SIGSEGV。CrashSight minidump 指向线程 23958 的 `SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob`。当前设备 Burst 二进制中，故障指令先从作业首字段取指针，再读取计数；寄存器 x8=0，故障地址 0。这与 Windows 研究代码 `cloth_contact_job_state.h` / `cloth_contact_job_install.h` 处理的跨帧计数器问题吻合，但 Windows 的 ABI、指令指纹及 hook 地址不可直接用于 Android。

目前 `build-proxy-pair` 返回 unsupported，不创建求解器。`build-independent-proxies` 明确将两层的 selfMode/syncMode 设为 None、syncPartner 设为空，并移除复制配置中的原始 colliderList/collisionBones。每个构建请求在执行前写入持久化 consumed 标记；同一个 token 在重启后不再执行。此模式只证明独立求解器的构建与销毁，不证明避碰。

配对模式恢复前需要 Android 运行时字段布局与调度调用证据、计数器所有权及生命周期、原任务依赖保持和撤销时的任务完成边界。禁止通过全局关闭跨帧任务绕过这项要求。当前 BEM 包没有新增物理格式声明。

### 005 独立求解器设备结果

`rin-next-proxy-pair-005` 使用 `build-independent-proxies`。Android 回读确认内裙 TeamId=12、外裙 TeamId=13，两者 IsValid/IsRunning 均为 true；随后 `cleanup_confirmed=true`。原始配置未改变，未借用原始碰撞体，也未绑定显示网格。请求文件已删除。本次结果在报告和 consumed 检查点中保存，重启不会覆盖成一次新的失败或重放构建。

同次元数据采样确认两种跨帧碰撞作业的字段偏移（托管装箱基准）：indexCount=16、nextPosArray=32、oldPosArray=48、对应 contactList=64。Execute 和 UpdateBroadPhase 均可按名称解析。该证据不是对 native 调度参数、UnsafeList 长度字段或调用边界的完整验证，因此 Android counter adapter 仍为未安装，配对碰撞继续禁用。

接续顺序：验证 Android 调度结构和计数器 lease → 配对碰撞注册及动态几何避碰 → 实例空间与模拟输出绑定 → 身体碰撞、换装/销毁事务 → BEM 格式及打包链集成。独立求解器成功不能跳过其中任一步。

## 006–015 方法 Hook 与基础设施修复（2026-10-09）

用户要求使用方法 Hook，不使用固定地址/偏移安装 Hook。新增 `android_cloth_contact_abi.inc` 仅读取当前元数据和函数代码证据；`android_cloth_contact_hooks.inc` 通过命名方法和完整签名安装 HookBroker Hook：`SelfCollisionConstraint.UpdateBroadPhase(JobHandle)` 与 `IJobExtensions.ScheduleManagedCrossFrameJob<T>(T, JobQueuePriority, JobHandle)` 的两个闭合泛型版本。闭合方法通过命名泛型定义、`RuntimeMethodInfo.MakeGenericMethod` 和 IL2CPP reflection API 取得。

已确认游戏直接调用的 CrossFrameJobUtils AOT 入口与反射取得的入口不同，但两者调用相同的上述终端调度方法。调用图解析只用于验证，不用于选择一个裸地址安装 Hook。结构尺寸/字段通过当前 IL2CPP 元数据验证，计数器绑定现有列表的实时长度，保留任务依赖、优先级和原始数组。Windows 和 Android 独立计数器回归均通过。

014 游戏测试仍然崩溃。本次位于 UnityMain 的 `DobbyCommit -> CodePatch -> memcpy`，故障地址位于下一内存页开头；Hook 目标处在当前页末尾前 12 字节，写入 16 字节，而 Dobby 1.0.5 只保护第一页。**本次尚未执行碰撞 Hook，因此不能据此宣称碰撞计数器修复失败或通过。** 报告、CrashSight stack/minidump 已保存到工作区 `reports/contact-hook-014*`。

已新增仓库内 `core/code_patch_posix.cpp`，由 CMake 明确替换 Dobby 的 POSIX 单页实现，不修改忽略目录下的工具链源文件。现在覆盖完整写入页范围、保留各页原权限、拒绝不可读/溢出范围、任一页开写失败时恢复已改权限并不写入；HookBroker 也会检查底层写入失败，不把 Dobby 返回成功直接当作安装成功。

`BetterEndfieldNext.CodePatchTests` 在独立 Android 进程通过：跨页代码写入及执行、Dobby prepare/commit/detour/原函数/恢复、混合页权限恢复、第二页写权限拒绝后的回滚、不可访问页/溢出/空参数拒绝。测试没有在游戏中执行。015 APK 已编译，尚未装入游戏复测。

**当前手机已回退 005，私有 cache 实验请求已删除。不要自动重放 014 或再次请求用户重复 UI 测试。** 当前正式 BEM 仍为 1.1.3，pair 模式仍禁用，裙摆显示没有新的物理效果。下一步是在已通过独立跨页 Hook 回归的基础上，验证空接触调度，再启用私有双层配对并验证原生注册；尚未完成配对碰撞、实际显示绑定或 BEM 物理格式。

## UI 重复加载修复记录

手机日志显示场景替换成功后，UI 再次加载因 `clone materials do not identify a completed generation` 被拒绝。正常替换先前只在开启热切换时保留 Original 来源记录，关闭热切换时还会在模板弱引用消失 10 秒后删除记录。现对每次有选择键的资源提交保留来源，回收使用有界的历史记录策略（每个角色最多四条已失去模板引用的修改记录）；隐藏部件也计算为修改。未替换网格只保存弱身份，生成网格仍按既有规则保留其原始网格；材质仍单独校验。

生产代码回归覆盖关闭热切换、模板弱引用消失、超出旧回收窗口和材质变化。003 APK 已通过用户实机复现步骤：详情页切其他角色、等待 15 秒、切回仍显示凛。首次打开成功不单独视为这项问题的验收。


## 017–022 实际进度（2026-10-09，接续记录）

以上 015 时点的“配对禁用/回退 005”已经过期。017、018 在手机成功构建两个独立团队，验证单向 syncTeamId、反向 parent team、三种碰撞标记和点/边/面块。两类碰撞任务持续修复计数器，退休时解除配对、先销毁消费者、确认生产者原语及父团队清空，再完成任务并释放 Hook。018 代理点到粒子的映射为双射且位置误差为零；内外层最大位移分别约 59.4 / 118.5 毫米。两个静态锚点实验采样都未检出三角面互穿。该结果不代替活动角色运动验收。

019 的新崩溃来自错误的 Normal 通道写入：Android 此方法按 float3 每顶点读 12 字节，即使传 dimension=1 仍如此。4 字节 packed TBN 不能传给它。020 仅写 float3 Position，通过未发布 Mesh 克隆的修改/还原/源网格不变/packed 布局不变检查。以后不得再次用 packed TBN 调用 Normal 通道 setter。

021 增加 `bind-live-pair` 实例显示桥：通过 Resources.FindObjectsOfTypeAll 和完整命名签名查找活动世界 renderer；读取实际骨骼和 bindposes，代理坐标经 pelvis bindpose 转到骨盆局部，私有代理固定点随活动骨盆变动。通过 HookBroker 的命名 ClothManager.OnAfterLateUpdate() Hook，在完成作业后读取 dispPosArray；利用加权骨骼矩阵逆变换回写实例所有的 Mesh 克隆。保留 packed TBN；未修改的顶点、材质、UV、蒙皮和索引保持原样。核心匹配只在来源身份比较中解引用物理克隆别名，实际回滚绑定保留真实 Mesh。

021 请求在加载卡顿反馈后撤销，采样未找到可绑定的活动角色，因此 **021 没有显示物理效果**。未见新 native crash/ANR；加载阶段仍有主线程维护日志，尚不能将加载延迟归因于某一单项。022 缓存命名方法解析，要求 renderer 对世界相机可见才启动，并增加首次实际显示网格写入的全字节回读检查及随骨盆/角色缩放变动的装饰偏移变换。

当前正式 BEM 仍为 1.1.3；没有声明 BEM 物理 wire format。显示驱动仍待实机验证，身体碰撞、动态法线、长期生命周期和正式格式集成仍待完成。实验请求采用持久化 token，失败不得在游戏重启后自动重放；显示写入探针也采用该保护。


## 023–025 活动角色与已确认的互穿

022 扫描确认活动根实际名称为 `chr_0013_aglina_postmodel(Clone)#36`。早期显示桥只接受没有池编号的名称，因此未创建候选团队。023 已复用核心 `ResourceBaseName` 消除该错误；随后在活动演员上确认两个私有团队、双射粒子映射（约 0.203 毫米误差）、每帧 1254 个显示顶点回写，以及首次全字节网格回读。025 前尚无解决互穿的验收结果。

024 输出实际 renderer 的可见状态、持有的私有 Mesh、粒子索引映射和最终显示顶点世界坐标；提供显式 `show_rest_mesh` 比较开关，并保留解除时的所有权检查。分析工具 `analyze_live_display.py` 对代理与真实 BEM 显示三角面分别检测。帧 2490 确认 **代理与显示均有 5 处互穿**；显示到代理的最大对应误差约 0.031 毫米。内裙 624 个显示面全部接管，外裙 971 个完整显示面及 9 个混合边界面（当前分析排除后者）。不能以早先两个无交叉采样或团队注册成功宣称避碰通过。

025 在私有配置中试验每层 8 毫米接触厚度及两层均为 0.5 的接触质量，核对原配置不变、Curve 对象未与原求解器共享，采集实际有效参数和两类接触列表长度；控制错误进入撤销流程。所有 hook 仍是命名方法/完整签名，不使用 RVA。当前 025 已安装，手机停在通知栏，尚待前台场景采样。该厚度和质量设置是待验证方案，不能在正式包说明中标注完成。

后续先对比真实显示面相交与原生接触数量/有效参数；需要证明当前 solver 的接触确实消除已复现互穿，而不是继续用注册或回写计数替代效果。身体碰撞目前关闭，用户当前反馈的重点是两层裙子，不应把它误当作腿部穿透。


## 2026-10-10 转向：BoneCloth 关节驱动（参照 EIEM）

用户实机确认：026/027 的 MeshCloth + CPU 顶点回写**画面完全无变化**（求解器运行、每帧写 1254 顶点、最大偏移约 12 cm）。对照 Windows EIEM 衣物增强（`research/camera/1.5.3/Windows/eiem-git/src/cloth/`）：EIEM 从不写顶点，而是新建 Transform 关节链、生成带额外 bindpose/权重的网格、`set_bones`+`set_sharedMesh` 换绑，再由原生 BoneCloth 驱动关节，普通蒙皮显示结果；层间碰撞为两 BoneCloth 团队的 FullMesh sync（内层 syncPartner=外层，clothMass 0.5）＋计数器修复（Android 已移植并实机验证）＋层序补丁（Burst 钩子，暂未移植）。

新模式 `bind-live-bonecloth`（`android_cloth_bone.inc`，纯计算 `native/modules/custom_model/cloth_bone_binding.h`，测试 `cloth_bone_binding_tests.cpp`）：
- 制作端 `scripts/bonecloth_authoring.py`（由 `build_native.py` 调用）重建凛源模型自带的裙摆骨骼：内裙 8×3（SequentialLoopMesh）、外裙分叉拆成 10 条 5 节链（SequentialNonLoopMesh，前开口），共 74 关节；1135 个裙摆顶点沿用源作者权重，非裙摆影响按骨骼名引用。输出 `reports/bonecloth-authoring.json`（含 stream0 SHA-256 与逐顶点静止位置）。
- 运行时：在活动骨盆下以单位旋转建关节，bindpose = T(−p_pelvis_local)·BP_pelvis（静止时与骨盆蒙皮完全一致）；经加载器 `BuildMeshFromComponent`（导出为 `BuildModelMeshForCloth`）生成追加 bindpose 的网格，`set_bones`→`set_sharedMesh`；两层 BeyondBoneCloth 复制 MC_coat 配置并沿用其 6 个腿部碰撞体，boneAttributeDict 通过托管反射 Add（根 Fixed、其余 Move）；宿主 GameObject 挂在角色根下（求解器惯性中心）。监视网格/骨骼被替换或角色失活即恢复并清理。
- 未在实机验证。待测：换绑后静止无跳变、裙摆可见摆动、FullMesh 注册与计数器修复在 BoneCloth 团队上有效、腿部碰撞。层序补丁未移植，层间仍可能翻面。
