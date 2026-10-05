# `m_BonesPerVertex`：当前实现与 Android 适配结论

状态：调查结论，未改变运行时代码。

## 当前 Windows 实现

生产版 `BetterEndfield.CustomModel` 的处理顺序如下：

1. 通过 `il2cpp_resolve_icall` 解析 `UnityEngine.Mesh` 的顶点缓冲、索引缓冲、子网格和上传方法。
2. 从 `UnityPlayer.dll` 的映射 PE 中查找字符串 `m_BonesPerVertex`，再追踪引用它的序列化函数。
3. 只有至少两个序列化函数得到同一个字段位移、且候选不歧义时，才接受该位移。
4. 创建新 Mesh 后，在上传顶点缓冲、索引、子网格和 bindposes 之前，从原 Mesh 读取该原生字段，限制为 `1/2/4`，并写入新 Mesh。
5. 上传完成后再次读取新 Mesh，值不一致就放弃替换。

因此，当前生产实现已经不再直接依赖固定的 `+0x1C8`。旧研究文档中的 `+0x1C8` 是某个客户端的观测值和历史实现记录，不应再被理解为正式版的解析策略。

## `0x1C8` 到底是什么

它是 native `Mesh` 对象布局中的**字节位移**：如果某个 Mesh 对象的起始地址是 `mesh_native`，那么该字段地址是

```text
mesh_native + 0x1C8
```

在已采集的序列化函数中，指令类似 `lea rdx, [rsi+0x1C8]` 或 `lea rdx, [rdi+0x1C8]`。`rsi/rdi` 是当前 Mesh 对象，指令把字段地址算出来，再把这个地址传给序列化辅助函数；另一个寄存器传入字符串 `m_BonesPerVertex`。因此：

- `0x1C8` 不是函数 RVA；
- 不是字段的数值；
- 不是独立的“BonesPerVertex 参数”；
- 它只是当前客户端 native 对象布局中的字段地址位移。

字段里的值才是项目关心的影响数类别，目前有效值为 `1/2/4`。HG 路径会读取该字段并把它限制到 4 后写入自己的绘制记录，所以新 Mesh 必须在引擎消费前初始化为对应源 Mesh 的值。

## Mesh 构造函数是否能传入这个值

当前 IL2CPP 资料中的 `UnityEngine.Mesh` 构造函数是无参数的 `.ctor()`；它只接收已经分配的托管 Mesh 对象，随后进入 `Mesh.Internal_Create(Mesh mono)`。当前构建路径也是先 `NewAsset`，再调用无参数 `mesh.ctor`。没有发现可在构造调用中传入 `bonesPerVertex` 的参数位。

已有原生快照还显示，构造函数会初始化若干相邻字段，但正常路径不会写入 `+0x1C8`。这正好解释了新 Mesh 继承分配块残留值的随机现象：问题不是“构造参数传错”，而是构造完成后没有经过会写入该字段的路径。

因此初始化位置应保持为：

```text
Mesh 构造完成
→ 写入/建立 m_BonesPerVertex
→ SetVertexBufferParams / 顶点与索引上传 / bindposes
→ UploadMeshData
```

不能把这个量塞进现有 `.ctor()` 调用，也不能等 `UploadMeshData` 之后才补写。

## 2026-09-20 静态追踪：原版资源是在哪里写入的

在保留的 `UnityPlayer.dll` 静态分析样本中，已经把两个字段引用点继续追到了通用归档读取分支：

```text
Mesh 归档函数候选 0x1706F04
  → lea rdx, [rdi + 0x1C8]
  → lea r8,  "m_BonesPerVertex"
  → call 0x10BB6D0
  → call 0x127F310       （判断当前归档字段的读写模式）
  → 读模式返回正值
  → call 0x10BDE1C
  → 从归档游标读取 4 字节
  → mov dword ptr [rdx], eax
```

`0x10BDE1C` 的目标参数 `rdx` 就是前面计算出的 `Mesh + 0x1C8`，其核心指令为：

```asm
mov eax, dword ptr [archive_cursor]
mov dword ptr [rdx], eax
```

同一批分析还找到另一个包含完整 Mesh 字段名的归档函数候选 `0x1705DA0`，它通过 `0x354410` / `0x3554D0` 注册或写出同名字段。两个函数共同说明：原版 AssetBundle Mesh 的值来自 Mesh 归档数据，而不是构造函数参数。

这条链目前可以称为“已找到原版写入机制”，但不能直接拿来初始化新 Mesh：`0x10BDE1C` 依赖正在读取的 AssetBundle 归档上下文、游标和字段描述。脱离该上下文调用会读错数据或破坏归档状态。

另外，已检查的 `UnityEngine.Mesh::SetBoneWeightsImpl`（`0xF99BD4`）和 `InternalSetBoneWeights`（`0xF990C4`）会更新 Unity 的骨骼权重存储，但函数本身以及其已展开的内部路径没有直接写入 `Mesh + 0x1C8` 的指令。因此它们目前不能被视为这个鹰角字段的正式 setter。

当前结论：

- 原版值的直接来源：Mesh 资源归档数据；
- 原版写入动作：通用归档读取器把四字节值写到字段地址；
- 可复用的公开 Mesh setter：静态证据不足；
- 新 Mesh 的可靠处理：构造完成后，在上传前由平台适配器写入匹配源 Mesh 的值。

## 方法级接入候选

### 候选 A：用 `Object.Instantiate(sourceMesh)` 代替空 Mesh 构造（当前客户端已判定不安全）

Unity 的 `Object.Internal_CloneSingle` 会进入 native 对象的类型克隆路径，而不是调用 `Mesh..ctor()`。如果 Mesh 的 native 克隆实现复制完整序列化状态，那么 `m_BonesPerVertex` 会随源 Mesh 一起复制，之后只需在克隆体上覆盖顶点、索引、子网格和 bindposes。

这个路线理论上可以绕开私有字段 setter，但仓库的旧版 `module_twin_mesh_diag.inc` 已记录：对当前修改版客户端的 live source Mesh 调用 `UnityEngine.Object.Internal_CloneSingle(Object)` 会崩溃，因此 Clone/Instantiate 探针被故意移除。它不能作为当前 Android 的默认方案，也不能因为 Unity API 名称稳定就重新启用生产调用。

只有在独立、可丢弃的诊断构建中确认该崩溃已被解释并且目标客户端允许克隆 Mesh 后，才有必要做下面的 A/B：

```text
同一个源 Mesh
→ new Mesh() 与 Object.Instantiate(sourceMesh) 各创建一个对象
→ 记录创建后 m_BonesPerVertex
→ 不上传任何新数据前比较两者
```

若克隆体稳定等于源值，再比较覆盖几何后该值是否仍保持。只有两步都通过，才适合把 CustomModel 的建 Mesh 路径改为克隆路径。

### 候选 B：Hook `Mesh.Internal_Create`

当前托管入口 `Mesh.Internal_Create(Mesh mono)` 的 native icall 为 `UnityEngine.Mesh::Internal_Create`，其后进入 native Mesh 创建函数。可以在方法边界安装一次性 Hook：

```text
BuildMeshFromComponent 设置线程局部 pendingInfluence
→ 调用 Mesh..ctor()
→ Internal_Create Hook 调用原函数
→ 从 mono 取得 native Mesh 指针
→ 写入 pendingInfluence
→ 返回 Mesh..ctor()
```

这是真正可按项目约定解析的“方法级接入点”，Android 可以通过 IL2CPP/Host adapter 解析该方法并用 Dobby 安装 Hook。它解决了初始化时序和平台入口统一问题，但如果 Unity 没有公开 setter，Hook 内部仍需要平台适配器提供 native 字段地址；不能把 Hook 误解为自动消除布局问题。

`UploadMeshDataImpl` 也可以作为兜底 Hook，但它发生得更晚，且某些构造/资源路径可能在最后一次显式 Upload 之前已经消费 Mesh，不作为首选。

根据当前实机结论，`SetBoneWeights` / `InternalSetBoneWeights` 不再作为这个字段的 setter 候选：它们没有写入 HG 消费的 `m_BonesPerVertex`，并且还可能与 packed skin stream 形成两套相互覆盖的蒙皮表示。Clone/Instantiate 因当前客户端已有崩溃证据而排除生产路线。`Internal_Create` 只能作为生命周期 Hook；如果要写独立新 Mesh，仍须使用经过验证的平台字段适配器，或改走已证明安全的原版 Mesh 复用方案。

## 它解决了什么，仍缺什么

这套实现解决了 Windows x64 客户端变化时的固定偏移问题，并且在字段无法解析、值不在 `1/2/4`、写入失败或上传后被覆盖时会拒绝替换。它仍然依赖以下 Windows 专属条件：PE 结构、x64 指令解码、`UnityPlayer.dll` 映射、SEH 保护和原生 Mesh 私有对象布局。

它不是方法级 Hook。字段本身没有被 Hook；代码是在 Mesh 构造阶段直接读写 Unity 私有原生对象内存。`HasBoneWeights()` 也只能表示是否有蒙皮，不能给出 `1/2/4` 的具体影响数，因此不能作为替代方案。

## 与项目 Android 约定的对照

项目 Android 模块的通用路线是：从 IL2CPP 元数据按程序集、命名空间、类名、方法名、参数和返回类型解析稳定方法，再通过 Host adapter/Dobby 安装一次性方法 Hook；不使用固定 RVA、固定私有对象偏移，也不在 Vulkan/D3D DrawCall 层拦截。

当前 `android/app/src/main/cpp/CMakeLists.txt` 只编译 `native/modules/model/module.cpp`，没有编译 `native/modules/custom_model/module.cpp` 或 `native_mesh_layout.cpp`。因此 Android 目前没有 BEM CustomModel 运行时实现，Windows 的字段扫描不能直接移植过去。

结论：

- “运行时发现而不是写死偏移”：Windows 当前符合；Android 尚未实现。
- “按方法解析/方法级 Hook”：当前 `m_BonesPerVertex` 路径不符合；它是字段读写，不是 Hook。
- “停留在 Unity Mesh 对象层”：符合项目架构；不需要改成 Vulkan/D3D DrawCall Hook。
- “跨平台保持同一 BEM 语义”：可以做到，但需要平台适配层隔离实现细节。

## 推荐的正式接口

将 `module.cpp` 中的字段细节抽成平台无关接口：

```text
MeshSkinMetadataAdapter
  ReadInfluenceCount(source_mesh)
  InitializeInfluenceCount(new_mesh, count)
  VerifyInfluenceCount(new_mesh, count)
```

Windows 实现暂时封装现有 PE 序列化上下文解析。Android 实现可从实际 IL2CPP dump 中确认 `GetBonesPerVertex*` 等诊断方法和资源归档入口；`SetBoneWeights*` 已排除为目标字段 setter。若公开托管路径不能保留该语义，就解析一个有稳定原生特征的 Mesh 序列化方法，在 Unity Mesh 对象层安装一次性 ARM64 Hook，通过适配器完成观察、布局关联和验证。

现有 Android/IL2CPP 资料中虽然能看到 `Mesh.SetBoneWeights(NativeArray<byte>, NativeArray<BoneWeight1>)`、`Mesh.GetBonesPerVertex()` 和 `Mesh.GetBonesPerVertexValue()`，但用户的实机结论已经确认：`InternalSetBoneWeights` 以前调用时并不会写入 HG 消费的 `m_BonesPerVertex`。历史 PoC 还显示它会与自定义 packed vertex stream 形成两套蒙皮表示，后续 `SetVertexBufferParams` 可能覆盖其中一套。因此这些方法最多用于观察 Unity 自己的骨骼权重存储，不能作为目标字段的正式 setter。

更具体地说，方法级路线不能再把 `SetBoneWeights` 当作目标字段的写入 API。它可以保留为诊断入口，用来确认 Unity 的另一套骨骼权重状态，但不应据此初始化 `m_BonesPerVertex`。`Internal_Create` 仍可作为创建时序 Hook；真正的字段写入如果继续采用独立新 Mesh，只能由平台适配器在真实归档观察得到的布局上完成，或者改用已经证明安全的原版 Mesh 复用路线。

单独 Hook 一个已解析的 Mesh 原生方法也不能凭空补出这个字段：Hook `SetBoneWeights` 只会重新进入已经被证实无效的语义路径，Hook 上传方法又发生得太晚。这个 Hook 必须由平台适配器按方法元数据/原生签名安装，不能把序列化字符串扫描结果当成 Android 的固定地址；也不能在 DrawCall 层 Hook。

单独 Hook“包含 `m_BonesPerVertex` 字符串的序列化函数”只能继续获得字段地址或观察存档，不是跨平台 setter。它仍依赖 native 函数签名和寄存器约定，适合作为 Windows 诊断/布局发现，不适合作为 Android 正式写入合同。

不能用以下方式代替：

- 把 Windows 的 PE 扫描器改成 Android 固定偏移；
- 只调用 `HasBoneWeights()`；
- 从顶点 stride 猜测影响数；
- 在渲染器或 Vulkan/D3D DrawCall 层补字段。

在 Android 适配器完成前，BEM 的 `1/2/4` 影响数仍应作为转换和运行时校验条件保留；无法建立源 Mesh 合同就拒绝替换，而不是猜测。

## 下一步验证顺序

1. 保留 Windows 当前扫描器，补一个 `MeshSkinMetadataAdapter` 接口，不改变现有行为。
2. 对 Android 的实际资源链安装观察探针：外层记录资源身份，内层定位 Mesh 归档字段分派/标量读取器，确认真实 cursor 下的源字段地址和值，并处理同步与异步关联。
3. 只有多个 `1/2/4` 源 Mesh 得到一致的运行时相对布局，或原版 Mesh 复用通过共享关系验证后，才把 `CustomModel`/BEM 加入 Android CMake；随后为缺方法、关联失败、值不一致分别增加失败闭合测试。`SetBoneWeights` 只保留诊断，不作为通过条件。

## 2026-09-20 方法名核对与 Android 候选排序

### `Internal_Create` 不是归档写入链的同名函数

这里容易把两个不同层次的名字混在一起：

- `UnityEngine.Mesh.Internal_Create(Mesh mono)` 是 Unity 托管绑定中的创建入口，当前 Unity C# reference source 将它绑定到 `MeshScripting::CreateMesh`；`Mesh..ctor()` 只调用这个入口创建 native Mesh。
- 归档链中的 `0x10BB6D0`、`0x127F310` 和 `0x10BDE1C` 是 UnityPlayer 内部的无托管名字辅助函数。它们接收归档描述、游标和字段地址，`0x10BDE1C` 才执行从归档游标取值并写入 `Mesh + offset`。
- 旧记录中的 `Internal_Create → 0x00464780 → 0x0019BD80` 是新对象建立/登记和上传相关路径，不是 `m_BonesPerVertex` 归档 setter。

因此，`Internal_Create` 与归档写入链没有“同名导致不可用”的冲突；但它本身也不是字段 setter，只是一个稳定的创建时机。

### 更可靠的跨平台候选

候选应按“能否不接触私有字段布局”排序，而不是按距离构造函数的远近排序。

1. **`Mesh.SetBoneWeights` / `InternalSetBoneWeights` 已排除为目标字段 setter。**

   Unity reference source 确实把公开的 `SetBoneWeights` 转发到 `MeshScripting::SetBoneWeights`，但这只说明它是 Unity 骨骼权重 API，不说明它会写入 HG 使用的 `m_BonesPerVertex`。现有实机结果已经否定了这个假设；保留它只会增加第二套蒙皮表示的覆盖风险。

2. **Clone/Instantiate 暂不作为生产回退。**

   Unity 的 `Object.Instantiate(Object)` 当前绑定到 `Internal_CloneSingle` / `CloneObject`，方法身份本身是稳定的；但本项目已有对 live source Mesh 调用该入口导致客户端崩溃的记录。它只能作为隔离诊断候选，不能作为 Android 正式实现的跨版本保证。

   如果将来重新打开诊断，仍必须分别验证“克隆后、覆盖几何后”两次字段/绘制状态；任何崩溃、字段不保留或绘制记录不一致都应永久关闭该路线。

3. **`Mesh.Internal_Create` 只作为生命周期 Hook，不作为写入方法。**

   Android 可以按 IL2CPP 元数据解析：

   ```text
   UnityEngine.CoreModule.dll / UnityEngine / Mesh
   Internal_Create(UnityEngine.Mesh) -> System.Void
   ```

   用 Dobby 在方法边界安装 Hook，`BuildMeshFromComponent` 通过线程局部状态标记“下一次创建属于本次 BEM 构建”，原 Hook 返回后得到新 Mesh 的 native 身份。这个 Hook 只负责生命周期和对象关联，不能凭空写入字段；在 Hook 内增加 ARM64 固定偏移会重新引入当前方案要消除的版本耦合。

4. **`UploadMeshDataImpl` 只保留为观察/最后一道验证。**

   它发生在 Mesh 数据已经建立之后，不能保证 HG 在此前没有消费该字段，也没有 `bonesPerVertex` 参数；不应作为首选写入点。

### Android 实现合同

Android 模块不应解析或调用 `0x10BDE1C`，也不应把 `0x1C8` 移植成 ARM64 偏移。正式适配层应解析并缓存以下方法身份，按能力逐项启用：

```text
已排除的目标字段写入：Mesh.InternalSetBoneWeights(IntPtr, Int32, IntPtr, Int32)
语义读回/诊断候选：Mesh.GetBonesPerVertex()、Mesh.GetAllBoneWeights()
可选创建时序 Hook：Mesh.Internal_Create(Mesh)
隔离诊断候选（当前禁用）：Object.Internal_CloneSingle(Object)
```

每个方法都用程序集、命名空间、类名、方法名、参数类型、返回类型和参数数量解析；解析失败、Hook ABI 不匹配、字段观察不一致或 HG 记录不一致时，整个组件保持原样，不进入部分替换。`Internal_Create` 只在需要覆盖游戏自身创建时序时安装；独立新 Mesh 的字段写入必须来自真实归档观察得到的平台适配器，或由经过隔离验证的原版 Mesh 复用路线承担。Clone/Instantiate 保留为禁用的隔离诊断路线。

Unity 参考绑定：[Mesh.bindings.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Graphics/Mesh.bindings.cs)、[UnityEngineObject.bindings.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Scripting/UnityEngineObject.bindings.cs)；API 版本交叉证据：[Mesh.SetBoneWeights 2019.2](https://docs.unity.cn/2019.2/Documentation/ScriptReference/Mesh.SetBoneWeights.html)、[2022.3](https://docs.unity.cn/ScriptReference/Mesh.SetBoneWeights.html)、[Unity 6](https://docs.unity3d.com/jp/current/ScriptReference/Mesh.SetBoneWeights.html)。

## 归档上下文能否直接拿来写新 Mesh

结论是：**可以利用真实归档调用栈做观察或让原版 Mesh 完成正常反序列化，但不能在当前资源交付 Hook 中直接调用归档读取器给新 Mesh 写字段。**

当前 `_FinishWithAsset` Hook 拿到的是已经完成加载的 Unity 对象；归档游标、字段描述、读写模式和临时上下文已经在更早的 native 资源加载栈中消费或销毁。此时直接调用 `0x10BDE1C` 会缺少它需要的游标和归档状态，不能把它当成 `Mesh* + value` 的 setter。

真正进入上下文只有两种方式：

1. **Hook Mesh 归档函数本身，让原函数继续运行。** 这可以在真实读模式下观察 `m_BonesPerVertex` 的目标地址和值，或者验证动态布局；不能把 BEM 数据临时塞进它的归档游标，因为 BEM 不是 Unity AssetBundle 的字段流，字段数量、对象引用和游标推进都不匹配。
2. **让原版归档器先生成一个正常 Mesh，然后直接复用这个 Mesh 的原生状态。** 这意味着在资源交付阶段修改已经反序列化完成的源 Mesh，而不是创建一个空 Mesh 再复制字段。它能避开字段写入，但会改变共享资源：现有证据显示 postmodel/uimodel 和自然克隆可能共享 Mesh 指针，因此必须先证明资源身份唯一、所有使用者都应该看到同一替换，且不会影响其他角色或 LOD。当前正式设计明确禁止未经隔离验证的原 Mesh 原地修改。

第一种方式在项目里的具体形状应是“外层资源方法做关联，内层 native 归档函数做观察”，而不是在外层方法里手动补调归档读取器：

```text
I18NAssetLoader.Load / 更早的资源入口
→ 记录资源 hash、线程/任务关联和 probe 状态
→ 调用原方法
   → native Mesh 归档函数在真实 cursor/context 下运行
      → 调用原函数
      → 记录目标地址和值，或在返回后读取已写入的字段
→ 结束关联并验证 source Mesh 与资源身份
```

如果整个反序列化是同步嵌套调用，线程局部状态就足够；如果 `Load` 只创建异步请求，归档函数在其他线程执行，就不能只靠 TLS，必须用资源 hash、AssetProxy/对象身份或 loader 自己的请求上下文做关联。外层方法 Hook 本身看不到归档 cursor；真正需要 ARM64/ELF 版本适配的是内层 native 归档函数的定位和 ABI。

还有一个可以单独做实验的“上下文内复制”变体：在带有字段名的分派函数或标量读取器处识别 `m_BonesPerVertex`，保留原调用让它正常推进 cursor 并写回源 Mesh，同时把同一个四字节结果复制到一个已经建立关联的新 Mesh。复制目标可以通过“本次调用的源字段地址 − 源 Mesh native 地址”得到运行时相对偏移，再加到新 Mesh native 地址上，从而不把 `0x1C8` 写死。这个方案仍然需要稳定的 ARM64 调用约定、字段名识别、源/新 Mesh 关联以及异步资源关联；它是动态布局探针，不是可以脱离版本验证的通用 setter。

如果新 Mesh 在归档字段被读取时还不存在，就只能先记录 `(resource identity, source Mesh, observed field address/value)`，等 BEM 创建新 Mesh 后再由适配器应用；这时归档上下文负责提供可信值和布局，最终写入仍发生在新 Mesh 生命周期内。只有新 Mesh 已经在同一归档事务中注册并且对象引用关系经过验证，才可以尝试直接重定向目标地址；默认不应改变原归档对象的注册关系。

因此“正常通过上下文调用归档读取器”适合作为**动态布局发现/原版 Mesh 复用实验**，不适合作为 BEM 新 Mesh 的通用 setter。安卓版优先应做：在真实 Mesh 归档过程中观察并缓存字段地址，或验证原地修改方案；如果仍需独立新 Mesh，就必须有经过 A/B 的平台字段适配器，不能把归档读取器脱栈重入。

### 资源入口修正与两条路线的取舍

当前证据不应再把 `I18NAssetLoader` 作为默认资源入口。IL2CPP dump 中更接近实际 AssetBundle 交付的是 `BundleLoader.AssetProxy.LoadAsync/LoadSync`、`BundleLoader.BundleProxy.LoadAsset/LoadAssetAsync` 以及 `BundleResourceManager._LoadAssetInternal`；`BundleLoader.AssetProxy._FinishWithAsset` 是已经拿到 Unity 对象后的交付边界。具体 Android 版本仍需用实际调用记录确认，不能只按 dump 名字安装 Hook。

如果目标是**找到 `m_BonesPerVertex` 的运行时布局并给独立新 Mesh 初始化**，优先复现 PC 的动态扫描思想：把 PE/x64 扫描器改成 ELF/AArch64 版本，继续用字段字符串、引用点、函数边界、两处以上一致的字段位移和失败闭合。资源入口 Hook 只负责把源 Mesh、资源 hash/AssetProxy 和归档观察结果关联起来。

如果目标只是**确认归档上下文、资源身份和对象生命周期**，才把窗口沿真实 `BundleLoader` 路径上提。单独上提资源 Hook 不能产生字段地址，也不能替代 native 归档函数观察；它的价值是解决异步关联和时序问题。

所以推荐顺序是：先做 Android 动态扫描的只读探针，再补真实 `BundleLoader` 入口作为关联层；只有扫描器确认字段函数存在但无法把源 Mesh 与资源对应起来时，才继续向更上游移动。这样即使 Android 的 native 布局与 PC 不同，也会在扫描失败处停止，而不会误把一个“看起来稳定”的资源方法当成字段 setter。
