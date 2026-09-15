# Mesh 生成与 HG 蒙皮路径定位（2026-09-14）

## 结论边界

前半轮只读分析项目代码、最新日志、当前 dump 和游戏 UnityPlayer.dll。随后用户要求动态取证，并明确要求切回 F12 路径，因此追加了下述 0.0.42 诊断实现；没有启动游戏。

用户观察：每次生成出错的部件不固定，但某个生成后的 Mesh 的错误会固定；早期资源替换与后期替换都能复现，防 GC 没有改善。这使调查优先级转向生成/上传时的状态、派生数据和缓存关联。随机性本身不能证明竞争，也不能排除由资源状态触发的确定性分支。

**还未确认根因。** 本轮找到了此前诊断未覆盖的原生状态，核实了两项具体疑问，并缩小了可观测边界。

## 使用的材料及方法

- 游戏：`E:\Endfield Game\UnityPlayer.dll`，文件版本 `2021.3.34.0`，大小 33,069,624 字节。
- dump：主目录 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal`。
- 工作树当前 `module_poc2_part_02.inc`：直接写入原始 skin stream，已不调用 `InitializeMeshSkin`；不能沿用更早版本“两次设置布局”的假设。
- 15:30:51 的最近一组构建日志：8 个可见部件均报告 `layoutOnly=1 active=1 sourceInfluences=4 builtInfluences=4`，随后 early-full 提交成功。这不是逐顶点/GPU 数据正确的证明，也没有与每个部件的视觉好坏对应起来。
- 从内置 icall 注册表得到 UnityPlayer 原生地址，以运行日志中布局、上传入口的地址差交叉验证。下述地址均为 **UnityPlayer RVA**，不是 GameAssembly RVA，不能混用。
- Capstone 仅解码选定函数及其附近显式分支；不自动递归展开 call。IDA 禁用全局自动分析，仅对四个选定函数尝试反编译。
- 冷分支、间接调用和部分 helper 的类型仍不完整。反编译出现的 `STACK`、缺失参数或“未初始化局部变量”不能当成程序缺陷证据。以指令和已验证入口为准。
- `mesh-1c8-store-candidates.json` 只是同偏移写指令候选，含其他类型及可能误匹配；**不能据此声称这些都是 Mesh 写入点**。早期从 `308c40` 等函数中间开始的探索输出不用于结论；已用 `308bf0` 完整入口纠正。
- Headroom 当前没有可调用工具；原始结果保存在研究目录，后续按函数读取。

研究产物在 `research/postmodel-focused/`：`inspect-mesh-native.py`、`mesh-native-*.json`、`decompile-mesh-native.py`、`mesh-player.i64`。不要把反编译中人工赋予的名称当作原始符号。

## 1. InternalSetVertexBufferData 的参数单位已核实

代码实际调用：

```cpp
InternalSetVertexBufferData(mesh, stream, data,
                           0, 0, byte_count, 1, 0);
```

签名依次为 mesh、stream、data、dataStart、meshBufferStart、count、elementSize、flags。

实际路径：

```text
icall 0x00F993B0
  -> 0x0119A034（处理源偏移和各参数）
  -> 0x01709F94（范围检查、CPU 拷贝、标脏）
```

`0x01709F94` 的正常路径可归纳为：

```text
copyBytes = count * elementSize
destinationOffset = meshBufferStart * elementSize
capacity = vertexCount * streamStride
检查 destinationOffset + copyBytes <= capacity
Mesh 写时复制/可写状态处理：0x001C3C00
memcpy(vertexData + streamOffset + destinationOffset,
       data + dataStart * elementSize,
       copyBytes)
标脏/通知：0x001C3C90
```

所以当前 `count=byte_count, elementSize=1` 没有把字节数误当顶点数；零偏移也没有单位歧义。源缓冲会在此函数返回前复制进原生 Mesh 数据，**这一段**没有把 C++ vector 的临时地址留给后续 GPU 异步读取。后续引擎自己管理的缓冲生命周期是另一层问题。

`SafeSetVertexBufferData` 仅以“未触发异常”为成功；原生函数某些参数错误分支只记录错误后返回。当前仍缺少逐字节 CPU 回读，不能用函数返回成功代替数据一致性验证。

Unity 2021.3 官方绑定签名也一致：
https://raw.githubusercontent.com/Unity-Technologies/UnityCsReference/2021.3/Runtime/Export/Graphics/Mesh.bindings.cs

## 2. SubMeshDescriptor 的初始零 Bounds 会被重算

`SubMeshDescriptorRaw` 为 48 字节：Bounds 24 字节，随后 topology/indexStart/indexCount/baseVertex/firstVertex/vertexCount 六个 Int32。实际原生代码读取的偏移与定义一致。

路径：

```text
SetSubMesh_Injected 0x00F99F9C
  -> 0x0119A69C
     -> 检查 flags & 8（DontRecalculateBounds）
     -> 未设置时：0x002BA8B0 -> 0x002BA810（索引对应的顶点范围）
     -> 0x002BA780 -> 0x002BA930（子网格 Bounds）
```

当前 flags=0，因此会进入计算路径。`0x002BA930` 对有效索引逐个读取 position 的 xyz，计算 min/max，再写回子网格的 center/extents。传入的全零 Bounds 并不意味着存储到 Mesh 中的最终 Bounds 仍是零。

这是当前游戏二进制里的实际分支，不仅是标准 Unity 文档的假设。官方行为说明也相同：
https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mesh.SetSubMesh.html

仍应区分三处：submesh.bounds、Mesh.bounds、SkinnedMeshRenderer.localBounds。不能用 Mesh 总 Bounds 合格替代另外两项。顶点格式不支持、实际顶点内容异常或后续缓存错误仍需独立判断；本轮未取得异常部件的最终 Bounds 回读值。零 Bounds 尚不能解释静止在地面的默认姿态。

## 3. 当前 skin 日志检查的只是有限元数据

`HasBoneWeights` icall `0x00F9901C` -> `0x004179E0`：检查通道 13（BlendIndices）存在，或 Mesh 数据对象 `+0x180` 的附加指针存在。没有扫描全部权重、骨骼索引或蒙皮矩阵。

`GetBonesPerVertexValue` icall `0x00545490` -> `0x0019D2D0`：从 Mesh `+0x38` 指向的数据对象读 `+0x180` 和通道描述字节 `+0x3F` 的低四位，转换为 0/1/2/4/255 类别。**这不是逐顶点权重回读，也不是 HG 实际打包值的回读。**

因此日志中 `layoutOnly=1` 是可解释的：只建立带 BlendIndices 的布局就足以让检查通过。不能据此断言 skin 数据已正确初始化。

## 4. UploadMeshData(false) 是未被现有校验覆盖的边界

```text
UploadMeshDataImpl 0x0019BCC0
  -> 0x0019BD80
     -> 0x001C3DF0（清理若干派生数组/缓存）
     -> 0x0019BEB0（获取目标通道布局并调用 0x000EA440 整理顶点数据）
     -> 0x001670B0
        -> dirty flags 非零时进入 0x000D4D60
```

`0x000D4D60` 根据 `Mesh+0x80` 的顶点/索引 dirty bits，构造最多四个 stream 的描述，选择更新原有 GPU buffer 或分配/提交新的缓冲。正常尾部清除低两位 dirty bits。它还保留 Mesh 数据对象的引用供后续引擎流程使用。

这里只能证明存在这些操作和分支，**不能断言本样本每次上传都会改变布局，也不能把正常的引用保留当作竞争证据**。

项目的 `MatchesDeclaration`、bindpose 字节回读、skin 状态日志都在 `SafeUploadMeshData` 之前。上传之后只打印 `uploaded=1`，没有再次核对实际布局、VB 字节、Bounds 或 HG 状态。这是明确的诊断缺口。

另一个需要保留的时序事实：`Mesh.Internal_Create` -> `0x00464780` 的新对象建立路径，本身就可能执行 `0x0019BD80`。不能假设我们最后一次显式 Upload 是该对象第一次经过引擎上传/登记流程。

## 5. HG 确实读取另一组原生字段

`0x00C7B750` 在更新渲染对象数据时：

1. 把 renderer `+0x374/+0x370` 写到渲染对象数据 `+0x50/+0x54`。
2. 用 `0x000EF200` 决定蒙皮分支；返回 `0x200000` 的路径继续读 renderer `+0x298` 指向的 Mesh。
3. 读取该 **原生 Mesh 的 `+0x1C8`**，限制到 4，再 OR `0x10` 等位，写入渲染对象数据 `+0x4C`。

`0x000EF200` 受一个全局配置值影响，并由 `0x000EF230` 检查 Mesh 数据 `+0x190` 的附加结构。dump 的 HGObjectFlags 明确存在 CpuSkinning/VertexSkinning，但不要仅靠名字给所有未知结构定型。

`Mesh+0x1C8` 与上一节 GetBonesPerVertexValue 的读取位置不同。其消费者已定位；**写入链、字段正式语义、正常/异常运行值尚未确认**。这不是认定“固定参数错了”，而是一个可比较的派生状态候选。若两次生成它始终一致，应降低该字段的优先级。

## 6. HG 矩阵缓冲与默认姿态现象存在可验证的联系

- `0x00308BF0` 创建并命名缓冲 `HGSkinnedMeshResourceSystem_GfxBuffer`。
- `0x00E1ED20` 的系统建立路径准备 512 组 3×4 单位矩阵：常量分别是 `(1,0,0,0)`、`(0,1,0,0)`、`(0,0,1,0)`。
- `0x00309AC0` 预留 `0x600` 个 float4，即 `0x6000` 字节，把上述矩阵放进缓冲前缀。
- `0x000EEBF0` 按矩阵数乘 3 请求 float4 空间；`0x000EEC40` 正常返回“分配位置 + 0x600”和对应 CPU 指针，失败返回偏移 0、指针 null。
- renderer 的 `0x000EEB00` 路径根据 Mesh bindpose 数量申请空间，将结果写入 `+0x374/+0x388`，之后调用 `0x00C7B750` 更新渲染对象数据。存在按系统序号和原子操作跳过重复更新的逻辑。

这给出了一个具体机制：如果绘制使用了默认矩阵区域，而没有取得/使用当前动画矩阵，就可能保持默认姿态。但**尚未证明异常绘制使用偏移 0**，更未证明分配失败。分配/更新存在逐序号执行的特征，单次临时失败未必能解释永久固化；不要直接改缓冲容量或强制刷新。

## 接下来的最小取证

以用户标识的正常/异常部件为依据，保留同一份输入与生成序号，比较：

1. `SetVertexBufferData` 后、最终 Upload 前、Upload 后：实际布局与 stream 0/1/2 字节是否一致，特别是 skin stream；同时记录 bindpose、Mesh/submesh Bounds。
2. 同一新 Mesh 的原生 `+0x1C8`、数据对象 `+0x180/+0x190` 及 dirty flags 是否在生成阶段已经分叉。
3. 首次 HG 消费时：实际蒙皮分支、传给渲染对象的数据位、矩阵偏移与指针。将其与上面的 Mesh 身份关联，不能只按 renderer 名称归类。

CPU 数据首次出现差异，继续查生成；CPU 一致而上传后分叉，查布局转换和 GPU 更新；上述都一致而 HG 参数分叉，再查 HG 注册/消费。没有这些对应证据前，不写裸偏移补丁、不强制重初始化，也不把反编译局部变量的异常显示当成真实未初始化问题。

## 0.0.42 动态诊断交付

按用户要求关闭 EarlyDeliveryC9 自动调用，恢复 F12 apply/refresh 与 F11 rollback。沿用当前 BuildMeshFromComponent 的生成算法，本版只增加观测，不调整布局、权重、bindpose 或初始化顺序。

- 每次接受生成记录 `BEGIN G<n>`；同一次生成里每个实际新建 Mesh 另有 B 编号及 C 部件编号。
- 新 Mesh 最终 Upload 前后记录 `CPU` / `UPLOAD`，保存源 Mesh 和两阶段的原生头、数据头、每条 VB 的完整字节及 submesh0 Bounds。逐字节比较 BEM 输入，记录 `diff` 和首个不同字节位置，不计算哈希。若 Upload 合法改变了布局，字节差异只能表示需要解读，不能直接判坏。
- `ATTACHED` 与手动数字 0 的 `KEY0` 记录 renderer 名称、原生地址与 Mesh 编号对应，并再次读取当前 CPU 顶点数据。
- 每次 F12/0 打开六秒观察窗口。HG `0x00C7B750` detour 在原调用之后，只读 renderer 和 Mesh 字段，记录 `HG-PUSH`。不在渲染线程调用托管方法、分配内存或写日志；使用固定队列、try-lock 和节流，主线程消费日志；队列溢出/锁争用记 dropped 数量。
- 原生偏移观测之前核对六个独立 icall RVA，并通过元数据解析 m_CachedPtr 偏移。版本不匹配则禁用原生诊断，保留 F12 路径。
- HG hook 安装失败时保留 CPU 快照并明确日志提示。hook 的结果没有自动视觉标签，必须由测试者指出哪次 G/KEY0 是异常。
- 文件目录：`%LOCALAPPDATA%\BetterEndfield\logs\mesh-trace\<pid>-<tick>\`；主要日志仍为 BetterEndfield.log。
- 旧模型探测/BakeMesh 采样不再由 0 触发；不会自动修复、重绑或重建被采样的 Mesh。

注意：落盘与回读会增加主线程耗时，可能影响复现概率；GPU buffer 内容没有回读。本版首先定位 CPU/上传边界与 HG 提交参数差异，不能把它称为完整 GPU 抓帧。HG 矩阵指针有值不等于动画矩阵已写完，故不在该 hook 中将矩阵内容判为有效/无效。

验证：本工作树 Release 原生模块编译通过。运行时 hook、原生布局快照及视觉对应由用户实机验证；尚未宣称修复缺件。

## 17:31 实测与进一步静态定位

用户标识 G1/KEY0-S2（17:31:29）异常，G2/KEY0-S4（17:31:35）正常，随后确认异常区域就是手部，与 C5 对应。

- 两次 C0–C9 的三条 VB 均逐字节相同，所有 submesh0 Bounds 也逐字节相同。各阶段 VB 对 BEM 的 diff 均为 0。
- C5 原版 `+0x1C8=4`；G1 为 0，G2 为 27。各自从 CPU、UPLOAD 到 KEY0 都保持不变。其他新建 Mesh 上也有不合理的大整数，原版为 1 或 4。
- 同一原生地址 `44c4f2ab20` 在 G1 用于 C1、在 G2 用于 C5，两个对象 ID 不同，字段值均为 27：支持分配块残留的解释，但不单独构成因果证明。
- 已采集的 HG-PUSH 事件矩阵偏移/指针没有为零，但其 result 全部为 -1。按静态分支，这批事件不能证明 ECS 渲染对象的数据已更新，不能宣称已观测到实际绘制使用 0 或 4。

进一步根据 SOURCE/CPU 快照中的相同原生 vtable（RVA `0x019C8230`）找到 Mesh 构造函数 `0x000D9E70`。它安装该 vtable，初始化 `+0x1C0`、`+0x1CC/+0x1D0/+0x1D4` 等，正常完整路径未写入 `+0x1C8`。以实际快照为佐证，新建后的字段确实保留不合理值。`+0x80` 也是按位初始化，故其高位的随机值不应被错误解读为 dirty bits 异常。

**字段正式名称已确认，不再只是偏移猜测：**

```text
0x017062B7 lea rdx, [rsi+0x1C8]
0x017062C1 lea r8, "m_BonesPerVertex" (string RVA 0x019C8200)
0x017062CB call serialization helper 0x00354410

0x0170787A lea rdx, [rdi+0x1C8]
0x01707884 lea r8, "m_BonesPerVertex"
0x0170788B call serialization helper 0x010BB6D0
```

资源序列化覆盖字段，而当前 `new Mesh` + 原始 VB 上传没有走相同路径。它与公开 `GetBonesPerVertexValue` 计算的通道类别是两个不同来源。

除 ECS 路径外，又确认两处消费者：

- `0x0041EDB0` 的 HG 网格处理路径在 `0x0041F3D6` 读取 Mesh `+0x1C8`，无符号 clamp 到 4；后续参与该网格处理参数。
- `0x0119F794` 的 renderer 绘制数据构建路径：this 是 renderer 的 `+0x38` 子对象，第二参数是输出列表，第三参数是上下文；返回有效记录索引或 -1。每条记录 `0x218` 字节。在 `0x0119F88E` 读取该 Mesh 字段，并将 `min(value,4)` 打包到记录 `+0x160` 的 bits 1–3。该赋值仍受蒙皮分支条件控制，必须用实际记录验证。

## 0.0.43 同一 C5 Mesh 的可逆因果实验

F12/F11/0 沿用，新增数字 9。**没有自动修正生成结果**，先允许用户复现异常。

数字 9 在主线程针对当前记录、当前实际绑定的 C5 自定义 Mesh：核对实例 ID、原生 vtable、原版 C5 字段值确实是 4；保存旧值后，仅写入该 Mesh 的 32 位 `m_BonesPerVertex` 并回读。再次按 9 恢复保存的旧值。F12 新一代会清空旧实验记录。源 Mesh 不作为写入目标。

不调用 setter 重绑、不上传、不 MarkModified、不重建；这些动作会引入额外变量。每次写入记录身份、前后值并自动生成 C5-TEST 快照。若存在缓存导致单字段写入暂时不被绘制消费，视觉不变不能单独否定假设。

将原来 `0x00C7B750` 的观测 hook 换为 `0x0119F794`。原调用返回有效索引后只读真正的输出记录，新增 `DRAW drawInfluences=` 日志；原来无效入口的 -1 不再充当绘制证据。除六个 icall RVA 检查外，新 hook 安装前核对目标函数短前导指令字节；没有计算哈希。丢事件计数改为五秒汇总，避免刷屏。

实验判据：**同一 G/B/C5、相同 VB、无重建情况下，旧值→4 时手部恢复，4→旧值时异常重现，并与 DRAW 参数变化一致。** 未完成实机往返前仍称强假设，不宣称最终修复。

## 0.0.44 任意部件字段对照与跨帧重绑

用户无法固定复现 C5，且提出字段可能已被缓存。数字 9 扩展到当前实际绑定的所有已登记自定义 Mesh，分别使用对应原版的 `m_BonesPerVertex`（校验为 1/2/4），再次按 9 恢复各 Mesh 保存的旧值。仍不自动修正生成结果，不写源资产，不上传顶点数据。

新增顶排数字 8：保存当前 renderer / Mesh 实例身份，将 renderer 切到各自原版 Mesh，等待两帧后重新挂回**同一个**自定义 Mesh。既有 replacement handles / retained assets 保留对象；不 Destroy、不 new Mesh。保持材质和 enabled 状态，不将 sharedMesh 清空。该实验触发 sharedMesh setter 的绑定转换，但不保证清除所有 HG/GPU 缓存，更不等于完整重新生成 Mesh。

跨帧间隔中主线程 pump 暂停自动 reconciliation，并延后处理热键。F11/F12 在重绑结束后处理，且清除尚未执行的 8/9 请求，防止误作用于下一代 Mesh。回挂时重新校验原生地址与实例 ID；若游戏已另换 Mesh 或对象失效则跳过，不覆盖外部变化。失败记录保留在日志，正常 reconciliation 随后恢复。

8 自动记录 REBIND-BEFORE / REBIND-DETACH / REBIND-ATTACH / REBIND-AFTER；9 自动记录 FIELD-TEST。快照保留 G/B/C 身份、字段与 VB，DRAW 窗口持续六秒。无需每次另按 0。

建议先在任意部件异常时只按 8，观察单独重绑是否有效；再按 9 观察单独改值，然后按 8 观察改值后重绑；最后再按 9、8 检查恢复旧值后故障是否回来。各步留几秒，期间保持同一界面且不按 F12。若单独重绑无效、改值加重绑可往返复现，才会增强字段与缓存之间的因果判断。

验证：Release 模块编译通过；未运行游戏或增加测试套件，等待用户实机验证。

## 0.0.44 实机反馈与外部调查报告核对

用户明确反馈：**只按 9 即有效**。因此当前故障可通过修改现有 Mesh 的 `m_BonesPerVertex` 消除，无需在改值之后重绑或重建。这为字段故障提供了干预证据；尚未收到恢复旧值再次出现故障的反馈，不能把单次恢复扩大为所有随机故障均已排除。

最新会话 `mesh-trace/28164-23493015`，18:04:56，G24 的 FIELD-TEST-S27：

- C0–C9 的字段写入与回读均成功，目标使用各自原版的 1 或 4。C7 为 `0 → 1`，C5 为 `4251986372 → 4`。本次不能继续将 C5 当作唯一异常部件。
- FIELD-TEST-S27 与紧邻之前 REBIND-AFTER-S26 的三条 VB、submesh0 Bounds 逐字节一致；Mesh 实例身份不变。原生对象前 0x1E0 字节内，变化仅在 `+0x1C8..+0x1CB`。既有快照保存 0x200 字节，C3 的 0x1E9..0x1EB 也变化，处于报告所述对象分配尺寸之外，不用它推断 Mesh 字段变化。
- 本会话没有 DRAW 事件，故不能声称日志直接证明了最终绘制记录从 0 变成 1；当前依据是实际字段改动、几何不变与用户视觉反馈。
- 日志中 8 的操作先于 9，且全部 REBIND-ATTACH `wasSource=0`，说明计划回挂前已被重新绑定，不能将它当作有效的完整两帧隔离对照。主 pump 之外还存在 AI coverage 的重用绑定入口，若以后继续用 8 实验，需统一抑制这些入口。用户确认只需 9，因此当前修复方向不依赖这项重绑实验。

用户提供的外部报告：`C:/Users/28377/.codex/attachments/a07942f2-f01a-49c5-bd15-39c804e32209/pasted-text.txt`。其结尾提出两项根因，核对结果如下：

1. **字段未初始化：与证据一致。** 原生构造与序列化路径、消费者读取及随机残留快照已经相互支持，本次只改值即可恢复进一步增强因果解释。但不能把 `new Mesh` 概括为必然发生可见故障：消费者对大于 4 的值会 clamp 到 4，随机残留并不都产生不同的蒙皮模式；也没有 Shader 级证据支持“必定塌到世界原点”或进程“必定崩溃”。
2. **帧序号/CAS 竞争：尚未证明为另一根因。** `0xEEB00` 的同帧跳过与 CAS 分支确实存在，竞争失败也可能代表另一线程已经负责分配，不能单靠该分支推导矩阵丢失。之前 HG-PUSH 样本的矩阵偏移/指针均非零，且返回 -1；未记录到报告所声称的错误绘制实际读取偏移 0。只影响同一帧的缓存还不足以解释故障在此后多帧持续固化。该方向保留为独立假设，不据此直接改写 renderer 序号或锁字段。

正式修复应优先落在共享 Mesh 构建路径：新建后尽早、在首次提交自定义几何及绑定 renderer 之前，从匹配的源 Mesh 初始化该字段，保留版本/类型/数值检查，覆盖 F12 与早期资源替换两条调用路径。初始化能力不应依赖诊断是否开启。不把跨帧重绑或强制清理渲染缓存作为已证实必要步骤。本次仅补充调查记录，未替换 0.0.44 测试模块。
