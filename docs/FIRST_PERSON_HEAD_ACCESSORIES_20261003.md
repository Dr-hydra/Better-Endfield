# 第一人称头饰遗漏：局部修复与证据边界

基线：`main dd7131b4`。先读 [BEM 阴影实现](BEM_HAIR_SHADOW_IMPLEMENTATION_20261002.md) 与 [骨骼分析](BEM_HAIR_BONE_ANALYSIS_20261002.md)，并检查仓库及所负责目录的祖先 `AGENTS.md`；这些目录没有适用文件。本次只改 Camera runtime include、头饰测试及本记录，没有新增 UI 文字、角色特例或跨模块契约。

## 已确认的代码遗漏与修复

| 条件 | 原行为 | 本次行为 |
| --- | --- | --- |
| `cloth` / `fur` / 帽檐等名称未命中提示词，且可选 `vertex_count` 读取失败 | `EnsureNeckCap` 在骨骼分类之前跳过；全 Head palette 也无法隐藏 | 仅跳过已确认零顶点的部件；未知数量仍进入现有骨骼分类，允许确定的独立 Head/tail 部件隐藏 |
| 同一 renderer、mesh、骨骼对象，父级从躯干/未挂接状态变为 Head | `observed_bones` 只比较对象数组；耗尽两次重试后不再处理 | 同时记录实际骨骼分类；Head/tail/Neck/身体分类变化会重新发放该 renderer 的重试预算 |
| 已隐藏部件的骨骼对象不变，但父级离开 Head | patch 不失效，部件可能继续隐藏 | patch 保存分类并在扫描时复核；恢复仍由 Camera 持有的可见/投影状态，再按新分类处理 |

拓扑 clone 赋值前也复核分类，避免捕获期间层级变化后使用过期裁剪结论。独立部件沿用 `ShadowsOnly` / 原投影 Off 的可恢复隐藏，混合部件沿用完整 shadow mesh；未扩大全局允许集合。上述缺陷由生产代码与模拟 Host 场景确认，**尚无当前游戏会话证据证明用户看到的每件遗漏头饰都由这两处引起**。

## 定点资源核对

本次重新读取已有 `artifacts/android-refactor/android-source-metadata.jsonl`：`android-13179`、女管理员 `chr_0003_endminf_postmodel`，40 个带骨骼的 Renderer。文件含布局、palette、路径与 bindposes，不含原始权重/索引 payload。

| Android 原版可见部件 | palette 证据 | 能证明的范围 |
| --- | --- | --- |
| `cloth_03_lod1` | 1/1 骨骼在 Head 子树；832 顶点 | 无需名称提示、CPU 桥或 GPU 回读即可走独立部件隐藏 |
| `iris_01_lod1` | 6/6 骨骼在 Head 子树 | 无显式 BlendWeights 不影响独立部件的 palette 判定 |
| `hair_01_lod1` | Head 子树 27 项 + Neck 1 项 | 不能证明 Neck 是否实际参与 draw；不能据 PC 顶点统计整块隐藏 Android 原版 |
| `cloth_01_lod1` | 156 项中只有 Head 1 项；另含 `maozi_a1_M/a2_M/a3_M` | 三条 `maozi` 骨骼实际挂在 Spine2 下，不在 Head 子树；名称不能证明帽子面的非零权重，更不能授权删除整块衣服 |

其中 `maozi_a1_M` 路径为 `…/Bip001_Spine2/maozi_a1_M`，a2、a3 是它的后代；Head 路径为 `…/Bip001_Spine2/Bip001_Neck/Bip001_Head`。这是明确的分类边界，是否对应用户看到的帽子还需对应 draw 权重或现场观察。

此前 PC 噗切娜 `fur_02`、`cloth_02` 全 Head，`fur_03` 含 Neck，以及提弗洛斯不同 BEM 的 `cloth_02` 头/身体比例，仍以 10 月 2 日骨骼分析的离线记录为依据；本次没有重新解码那些 PC 包，不将其当作新采样或 Android 原版证据。

## 仍保留的边界与整合建议

- 正常读到正顶点数时，现有 scanner 已收集未命名提示的 SkinnedMeshRenderer；因此仅给提示词增加 `hat` / `brim` / `cloth` 不能解决通用分类缺口。现有扫描上限为深度 16、4096 节点、每 GameObject 32 个 skinned renderer；这份 Android 记录的 Renderer 路径最大深度为 3，不能据此证明更深的模型也完整覆盖。
- 未确认 Head 子树、含任何非零 Neck/身体影响的三角形继续保留。BEM CPU 桥按实际 draw 的非零影响裁剪，unused palette 项不构成身体面；keep/仅换纹理或原版无 GPU 回读仍不能凭名称恢复缺失几何。
- 原版 GPU `FpDecode` 仍拒绝无权重的 UInt8×4；这份 Android 记录有 13 个此类声明，但 CPU 桥 stride4 已按 BEM 生产规则解码隐含首权重。不能仅凭原版声明将所有 GPU 格式推广成 BEM 语义；全 Head 的独立部件本来就不需要解码。
- scanner 当前只查询 SkinnedMeshRenderer。本轮没有普通 MeshRenderer 头饰的直接样本，不新增猜测支持。如果后续证实此类遗漏，再整合 Renderer 类型/Transform 身份、MeshFilter sharedMesh 与相应恢复/阴影契约。

**主代理当前无需修改 `camera/module.cpp`、新增字段或契约。** `first_person_mesh.h` 与 `first_person_retry.h` 无需修改：允许集合和每绑定两次的预算规则保持原样，分类变化后的续发由 runtime 负责。

## 局部验证

- 新增顶点数失败场景在旧 runtime 上失败；修复后 `camera_head_hide_tests` 通过 **114 项检查**，覆盖未命中名称的帽/帽檐、同 GameObject 多 renderer 保留混合躯干、挂接完成后的重试、改挂躯干后的 ShadowsOnly/Off 恢复，以及已有阴影/CPU 桥用例。
- MSVC 19.44 Release 编译两项生产翻译单元测试，CTest **2/2** 通过；`camera_first_person_tests` **313 项检查**通过。只有已有 shared_ptr 原子 API 弃用警告。
- 现有 `first_person_mesh_tests.cpp` 以本机 GCC C++17 编译运行通过，验证连通衣物保留、任何非零身体权重、Neck、UV 缝、封口、非法输入及重试生命周期。
- 局部产物：ignored 的 `artifacts/first-person-accessories-tests-20261003/`。PC/Android 生产编译交由主代理统一执行；未安装或启动游戏，没有本轮设备或视觉验收，不能据 Host 测试声称 HG 双端实际投影效果已确认。
