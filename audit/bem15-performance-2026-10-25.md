# BEM 1.5 能力的性能风险审计

日期：2026-10-25
范围：BEM 1.5（`minor = 5`）新增的四项能力 —— `material-overrides-v1`、`root-offset-v1`、`dynamic-drivers-v1`、ZIP 容器，以及它们实际依赖的 1.2/1.3 基础设施（形态通道、`base` 快照、预算上限）。
方法：读实现 + 对热路径做等价 C++ 微基准（源码见 `worktree/audit/bench/`）。**未在游戏内做帧时间采样**，因此所有数字都是「模块自身 CPU 时间」和「上传字节数」，不含 Unity 内部开销。

---

## 0. 结论

**格式本身没有性能问题，出问题的是「每帧都要重算并整条重传」这条实现路线。**

按严重度排序：7 项 P0/P1 需要改代码，4 项 P2 属于「能改就改、不改也能用」。

| 级别 | 问题 | 实测/估算代价 | 定位 |
| --- | --- | --- | --- |
| **P0-1** | 形态驱动每帧整条 Position 通道重传，Android 无区间写入路径 | 真实包 82 MiB/s 常驻上传；50 万顶点 343 MiB/s；100 万顶点 687 MiB/s | `module.cpp:1044-1071`、`3174-3181` |
| **P0-2** | 采样是 `O(D log V)`：每条 delta 都做一次 `lower_bound` | 100 万 delta 单帧 41 ms（仅采样），整帧 53.7 ms；预计算下标后 14.5 ms | `bem.cpp:1393` |
| **P0-3** | `once` 播完（`finished`）后永久每帧重算重传，姿势其实没变 | `sawtooth`/`step` 一次性的包会长期占用 P0-1 的全部带宽 | `animation_driver.cpp:350-356`、`440-515` |
| **P1-1** | 材质调制每帧每属性一次 managed 字符串 + GCHandle + 反射调用，无脏检查 | 512 条绑定时簿记 144 µs/帧（实测），叠加 512 次 `MethodInfo.Invoke` | `module.cpp:3309-3323`、`3381-3404` |
| **P1-2** | `Contract()` 是 135 项线性 `std::string` 比较，热路径每次调用都扫一遍 | 单次写入 0.28 µs 里约 0.2 µs 是这次扫描 | `module.cpp:524-530` |
| **P1-3** | `ApplyMaterialOverrides` 每处理一个材质打一条日志，而日志器每条 open/close 一次文件 | **实测 14 ms/条**（本机，write-open 触发实时扫描）；256 draw ≈ 3.6 s 加载停顿。见 §11。**已修复**：真实 `Logger` A/B 10.6 → 0.02 ms/行 | `module.cpp:2252`、`logging.cpp:19` |
| **P1-4** | 形态 `base` 是整条 stream 的完整副本（含 UV/骨骼字节） | 真实包 1.9 MB；100 万顶点 stride 64 时 64 MB，且不计入 1536 MiB 预算 | `bem.cpp:969` |
| P2-1 | ZIP 容器逐 64 KiB 块回调 + 每块一次 vector 分配 + 一次额外 memcpy | 300 MiB 载荷 ≈ 4800 次 64 KiB 堆分配、600 MiB 额外拷贝 | `bem.cpp:162-169` |
| P2-2 | `root_offset` 准备期按名字遍历子节点，每个子节点一次 managed 字符串拷贝 | 深度 × 8192 上限，热切换会重跑 | `module.cpp:2966-2987` |
| P2-3 | 1536 MiB 解码预算对 Android 同样生效 | 文档自述 7 张 8K BC7 = 448 MiB 上传量，多角色叠加 | `bem.cpp:33` |
| P2-4 | `skip_validation` 把材质覆盖配额放宽 4 倍 | 单 draw 最多 352 个属性，256 draw = 9 万次反射调用（加载期） | `bem.cpp:1226-1281` |

**不是问题的部分**（避免过度归因）：
- 波形时钟本身可忽略：16 控制器 × 32 绑定 = 512 次浮点运算 + 512 次 `std::function` 调用，单帧 < 10 µs。问题全在下游 sink。
- `root_offset` 只在 prepare/commit 时写一次 `localPosition`，不是逐帧。
- ZIP 不是「整包读进内存」：`mz_zip_reader_init` 走自定义读回调（`bem.cpp:253-255`），常驻内存只有中央目录。
- `drivers` 清单解析有 `LoadedPath()` 短路，不会每帧重开文件。
- 材质静态覆盖是加载期一次性应用，稳态零成本。

---

## 1. 测量方法

三个微基准直接复制了出货代码（不是重写），只把 Unity 调用换成空实现，因此测的是**模块自己花掉的时间**。

```
D:/work/MSYS2/mingw64/bin/g++.exe -O2 -std=c++2a <file>.cpp -o <file>.exe
```

- `bench_morph.cpp` —— 逐字复制 `bem.cpp:1361-1401` 的 `SampleMorphChannel` 与 `module.cpp:1044-1071` 的 `UploadMeshPositions` CPU 半段（去掉 `set_native_channel` / `upload_mesh_data`）。
- `bench_morph2.cpp` —— 同输入四种实现对比（A 出货 / B 预计算下标 / C 持久 positions 增量修补）。
- `bench_material.cpp` —— `Contract()` 线性扫描 + FNV key + `unordered_map` 查找 + 字符串分配，不含 `MethodInfo.Invoke`。
- `scan_bem15.py` / `detail15.py` —— 直接读 BEM 头与 manifest，统计语料里的真实规模。

**语料实测**：仓库内 98 个可解析 `.bem` 中只有 4 个是 1.5（`temp/bemcheck/pkg.bem`、`temp/v4-check.bem` 字节相同，另两个是同结构的变体），规模完全一致。它正好是一个完整用例：9 个网格、4 条形态通道 / 9609 条 delta、4 个驱动、12 条 draw 带材质覆盖。下文的「真实包」指的就是它。

需要说清楚的是：**1.5 的真实使用样本只有这一份**。下面所有「真实包」结论都基于它，不能外推成「线上包的典型形态」；病态场景的结论来自格式上限，不是来自语料。

| 网格 | 顶点数 | stride | 形态通道（delta 数） |
| --- | --- | --- | --- |
| mesh0 | 17,517 | 16 | `vag_morph` 1637、`anal_morph` 180 |
| mesh1 | 119,860 | 16 | `dildo_depth` 6358、`anal_plug_depth` 1434 |

驱动：`driver_dildo`(pingpong)、`driver_anal_plug`(pingpong)、`driver_body_morph`(sine, 2 绑定)、`driver_body_gloss`(sine, `material_float _Smoothness`)。

---

## 2. P0-1：每帧整条 Position 通道重传

**代码路径**：`ApplyDriverMorph` → `UploadMeshPositions`（`module.cpp:1044`）。

每一帧、每一个被驱动的网格，都做三件事：

1. `accumulated.assign(V*3, 0)` 清空整张累加表（`module.cpp:3145`）；
2. 从 `set.base` 逐顶点 `memcpy` 12 字节，重建一条**紧凑的** positions 数组（`module.cpp:1054-1055`）；
3. `set_native_channel(mesh, 0, ..., vertex_count, ...)` 把**整条通道**交回 Unity，再 `upload_mesh_data`（`module.cpp:1066-1069`）。

只有 6358 个顶点真的动了，但交出去的是 119,860 个顶点的 1.37 MiB。

桌面端有 `byte_range` 路径（`InternalSetVertexBufferData` 只写变动区间），但：

- 默认值是 `channel`（`module.cpp:205`）；
- **Android 没有这条路径**（`module.cpp:3173` 注释、`3183-3185`），Unity 未导出该 icall，所以移动端只能整通道。

实测（单帧，不含 Unity 侧开销）：

| 场景 | 触及顶点 | CPU 合计 | 每帧上传 | 60 fps 带宽 |
| --- | --- | --- | --- | --- |
| 真实 mesh0（17,517 v） | 1,801 | 0.074 ms | 0.20 MiB | 12 MiB/s |
| 真实 mesh1（119,860 v） | 7,725 | 0.587 ms | 1.37 MiB | **82 MiB/s** |
| 50 万顶点 / 2 万 delta | 20,000 | 2.595 ms | 5.72 MiB | **343 MiB/s** |
| 100 万顶点 / 100 万 delta | 1,000,000 | 53.726 ms | 11.44 MiB | **687 MiB/s** |

真实包两个网格合计 ≈ 0.66 ms/帧 + 1.57 MiB/帧。桌面上这是 16.6 ms 预算的 4%，可以接受；但：

- 手机 CPU 单核性能约为桌面的 1/3，`memcpy` 与 cache 压力更大，同一份包在 Android 上大约 2–3 ms/帧；
- 687 MiB/s 的持续上传会直接吃掉移动端的内存带宽，而这份带宽本来要留给渲染；
- 更糟的是 P0-3 —— 有些情况下这份开销**永远不会停**。

**建议**

1. 桌面端把默认上传模式改成 `byte_range`（`runs > 32` 时才退回整通道），并把 A/B 开关保留。
2. Android 端至少做**脏检查**：采样权重与上一帧相同就跳过上传。这一步不需要新 icall，收益覆盖 P0-3 全部场景。
3. `positions` 改成持久缓冲 + 增量修补（见 §3 的 C 方案），可以顺手去掉每帧 1.44 MB 的 gather。

---

## 3. P0-2：采样是 `O(D log V)`

**代码路径**：`SampleMorphChannel`，`bem.cpp:1393`：

```cpp
const auto local = std::lower_bound(set.vertices.begin(),set.vertices.end(),delta.vertex);
```

`set.vertices` 是该网格所有通道 delta 顶点的**升序并集**（`bem.cpp:965-968`），`delta.vertex` 本来就来自同一批顶点。每条 delta 都重新二分一次，是纯粹的重复劳动：并集在加载期已经建好了，映射关系可以一次算完。

实测（同一份输入，三种实现）：

| 场景 | A 出货 | B 预计算下标 | C B + 持久 positions |
| --- | --- | --- | --- |
| 真实 mesh1（119.9k v / 6358+1434） | 0.550 ms | 0.212 ms | **0.085 ms** |
| 50 万 v / 2 万 delta | 2.374 ms | 1.660 ms | **0.291 ms** |
| 100 万 v / 100 万 delta | 50.921 ms | 14.496 ms | **13.637 ms** |

真实包快 **6.5 倍**，50 万顶点快 **8 倍**。100 万 delta 那一行剩下的 13.6 ms 是 `O(D)` 的加法本身，已经到算法下限。

注意 100 万顶点 / 100 万 delta 是**格式允许的**（单通道 `count ≤ 顶点数`，顶点上限 1,048,576）。也就是说一个完全合规的包可以让模块单帧花掉 53.7 ms —— 18 fps 封顶，而且是在桌面上。

**建议**

- `CaptureMorph`（`bem.cpp:930-938`）在读 delta 时顺手填 `delta.slot = index into morph->vertices`，运行期直接索引，去掉 `lower_bound`。
- `morph->vertices` 的构造现在是 `std::set<uint32_t>`（`bem.cpp:965-967`）逐条 insert，100 万条时是 100 万次红黑树插入 + 约 48 MB 临时节点。改成 `vector` + `sort` + `unique` 可以同时省加载时间和内存。

---

## 4. P0-3：`once` 播完后永久重传

`HasActiveDrivers()`（`animation_driver.cpp:350-356`）把 `Paused` 和 `Finished` 都算作「活跃」，因为按第 13 节语义它们要每帧投递冻结/末帧值来保持姿势。这条语义本身没错，但下游没有做「值没变就不干活」的优化：

- `Tick` 对 `Finished` 驱动每帧算一次 `Evaluate(waveform, 1-1e-9)`（`animation_driver.cpp:473-476`）；
- `sawtooth`/`step` 在 `p→1` 时输出 1，于是 `ApplyDriverMorph` 每帧都得到非零 `touched`；
- `g_driver_dirty_meshes` 一直为真，**整条通道每帧重传，永远**。

而文档恰恰推荐这种用法：`once` 要「推出去停在最远端」就写 `sawtooth` 或 `step`（规范 §13「末帧在哪由波形决定」）。换句话说，**按推荐写法做的单程动画，播完以后会以 60 fps 持续上传一份永不变化的数据**，直到用户手动停止或换包。

`Paused` 同理：暂停意味着姿势冻结，但每帧仍完整重算 + 重传。文档说暂停是常用操作（悬浮窗三个按钮之一）。

**建议**

在 `ApplyDriverMorph` 里加一层权重缓存：为每个 `(mesh, parameter)` 记住上一帧的 `weight`，全部未变化且 `g_driver_dirty_meshes` 不含该网格时直接返回。材质通道同理（见 P1-1）。注意保留「停止」那一帧的强制上传 —— 现有 `was_dirty` 逻辑已经处理了这一点，只需把「值未变」也当作可跳过。

---

## 5. P1-1 / P1-2：材质调制的每帧反射成本

**代码路径**：`WriteDriverMaterial`（`module.cpp:3381`）→ `SetDriverMaterialProperty`（`3309`）。

每帧、每条绑定的每一次写入都要付：

| 步骤 | 位置 | 代价 |
| --- | --- | --- |
| `DriverMaterialKey`：FNV-1a 哈希属性名 | `3286-3295` | 名字长度次乘加 |
| `g_driver_material_writes` 查找/插入 | `3386` | 哈希桶 + 节点 |
| `ForEachDriverMaterial` 遍历所有已完成资源 × 所有 binding | `3296-3307` | 线性，含 `IsNativeObjectAlive` |
| `NewString(name)`：新建 managed 字符串 | `3311` | **managed 分配 + `gchandle_new` + `rooted_objects` 哈希插入** |
| `Contract("material.set_float")`：**135 项线性字符串比较** | `524-530` | 实测 ≈0.2 µs |
| `InvokeVoid` → `MethodInfo.Invoke` | `658-670` | 通常 0.5–2 µs（未在本环境实测） |

实测簿记部分（不含 Unity Invoke）：512 次写入 = **143.7 µs/帧**，折合 0.28 µs/次。加上 Invoke，最坏情况（16 控制器 × 32 绑定）单帧 **0.4–1.2 ms** 只为材质调制。

`NewString` 在 `ResourcePump` 里会落到 `ConstructionScope::Root`（`module.cpp:581-590`），每次调用都 `gchandle_new` 一次、并插入 `rooted_objects`；作用域析构时再逐个 free。512 次写入 = 每帧 512 个 GCHandle 的申请/释放，纯 GC 压力。

真实包只有 1 条 `material_float` 绑定，代价可以忽略；问题在**配额上限允许的规模**上。

**建议**

1. 属性名在加载期转成 managed 字符串并缓存（`DriverBinding` 里存一个 handle），运行期复用 —— 去掉每帧的分配与 GCHandle。
2. `Contract()` 换成 `unordered_map<string_view, MethodContract*>`，或在加载期把 `MethodContract*` 直接存进 `DriverBinding`。135 项线性扫描在热路径上是纯粹的浪费。
3. 加脏检查：`value` 与上一帧相同则不写（顺带解决 P0-3 的材质部分）。
4. `ForEachDriverMaterial` 的 `IsNativeObjectAlive` 可以按帧缓存结果。

---

## 6. P1-3：日志器每条都 open/close 文件

`Logger::Write`（`logging.cpp:16-30`）：

```cpp
std::lock_guard lock(mutex_);
std::ofstream stream(file_path_, std::ios::out | std::ios::app);   // 每条都打开
...
stream << ... << '\n';                                             // 析构时关闭
```

而 `ApplyMaterialOverrides`（`module.cpp:2250-2254`）在**每次被调用时**都打一条：

```cpp
Log("Applied BEM 1.5 material overrides: floats=... colors=... disable_keywords=...");
```

`PrepareDrawMaterials`（`2547-2564`）对每条 draw 调用一次，`CopyMaterials`（`2292-2312`）对每个材质槽调用一次，所以**日志条数 = 带覆盖的 draw 数**。

**这一条后来被实测证实是本报告里最贵的单项**（见 §11）：本机每条「打开-追加-关闭」约 **14 ms**，因为 `Logger::Write` 每次都用 `ofstream` 打开再析构，而 Windows 的实时防护会在 write-open 时扫描整个文件。256 条 draw 各带覆盖时是 256 次 open/close，约 **3.6 秒**的加载停顿。

**建议**：删掉这条日志，或降到 debug 级并汇总成一条（在 `PrepareDrawMaterials` 结束时统计总数）。日志器本身必须改成持有句柄、析构时 flush —— 这一条同时决定 §11 里贴图日志的开销。

> **已修复**（2026-10-25）：日志器改为持久句柄，两条逐材质/逐贴图日志改为按组件、按事务汇总。落地内容与实测见 §11.6。

---

## 7. P1-4：`base` 是整条 stream 的副本

`bem.cpp:969`：

```cpp
morph->base = source;   // source = component.streams[stream]，整条顶点流
```

`source` 是 `vertex_count × stride` 的**交错**数据（含法线、UV、骨骼索引）。保留整条是为了 `byte_range` 路径能按区间原样回写 —— 但 `channel` 路径（默认，且 Android 唯一）根本不需要 stride 里的其余字节，它只要紧凑的 positions。

代价：

| 场景 | base 副本 |
| --- | --- |
| 真实 mesh1（119,860 × 16） | 1.9 MB |
| 100 万顶点 × stride 16 | 16 MB |
| 100 万顶点 × stride 64（允许上限） | **64 MB** |

这份内存**不计入 1536 MiB 预算**（预算是解码期峰值，`base` 是常驻），而且是每个带形态的部件一份。

**建议**：主存紧凑 positions（`V × 12`）；只有在 `byte_range` 模式被选中时才额外保留完整 stride 副本。Android 上 stride 20 的部件可省 40%。

顺带一个设计问题值得确认：`CaptureMorph` 在 `ApplyMorph` **之前**快照（`bem.cpp:1165` 注释「Capture before morphing so `base` is the authored position」），所以每帧必须把 `settled`（加载期滑条值）也重放一遍才能还原「当前姿势」。如果 `base` 改成快照**加载期形变之后**的位置，运行期只需要 `base + 被驱动通道的 delta`，`settled` 整套逻辑和每帧的额外 `SampleMorphChannel` 调用都能省掉。语义上「停止 → 交还加载时的值」不变，但需要确认与「滑条与驱动共存」那条限制的交互。

---

## 8. P2 级

### P2-1 ZIP 容器的加载路径

`ZipReadFunc`（`bem.cpp:162-169`）每次回调都：

```cpp
auto bytes = (*read_fn)(file_ofs, n);   // 分配一个 n 字节 vector
std::memcpy(pBuf, bytes.data(), bytes.size());   // 再拷一遍
```

miniz 的 `MZ_ZIP_MAX_IO_BUF_SIZE` 是 64 KiB（`miniz.h:1073`），所以一个 300 MiB 的载荷要 **4800 次 64 KiB 堆分配 + 600 MiB 的额外拷贝**，每次还伴随一次 `seekg`。相比之下 BEM 二进制路径是「一个 vector + 一次 read」。

再叠加解码器差异：DEFLATE 单线程约 250–400 MB/s，zstd 约 1.5 GB/s。同一份内容，ZIP 容器加载比二进制包慢一个量级（**估算，未实测**）。

规范第 14 节写「内存级流式直读，零临时落盘」—— 不落盘是真的，但不是零拷贝。

**建议**：给 miniz 加大 IO buffer（`MZ_ZIP_MAX_IO_BUF_SIZE` 编译期可改）或让 `ZipReadFunc` 直接读进 `pBuf`（省掉中间 vector）。文档上把 ZIP 定位成「中小包的便利容器」，不要暗示它与二进制包等价。

### P2-2 `root_offset` 的准备期遍历

`PrepareVisualRoot`（`module.cpp:2966-2987`）按 `/` 切段，每段遍历一次子节点：

```cpp
for(int32_t i=0;i<count;++i) { ... if(child && ObjectName(child)==part) ... }
```

`ObjectName` 每次是一次 `object.get_name` 反射 + 一次 managed 字符串拷贝（`module.cpp:681-685`）。深度 × 子节点数上限是 5 × 8192，最坏 4 万次 managed 字符串拷贝（约 40 ms），发生在每次 prepare（含热切换重跑）。

只在带 `root_offset` 的包上触发，且是一次性开销，所以定 P2。**建议**：一次遍历把 `名字 → 子节点` 建成 map，避免按段重复扫。

### P2-3 1536 MiB 预算对 Android 同样生效

`LimitsFor`（`bem.cpp:33`）对 `minor >= 2` 一律返回 1536 MiB，不分平台。文档 §7 自己算了：7 张 8K BC7 = 448 MiB 上传量，编队多角色叠加。手机上这个上限没有实际保护作用。

**建议**：安装期对 Android 加一条「解码后总量」预估与告警，或在读取器里按平台给更低的默认预算。

### P2-4 `skip_validation` 的 4 倍配额

`bem.cpp:1226-1281`：关闭校验后单 draw 最多 128 floats + 64 colors + 32 vectors + 64 + 64 keywords = **352 个属性**。256 条 draw = 9 万次 `ApplyMaterialOverrides` 反射调用，全在加载期。

这一项本身不算缺陷（用户主动选的），但它是 P1-3 那条日志的放大器：9 万条日志 = 9 万次文件打开。

---

## 9. 与现有文档的差异

`docs/custom_model/BEM_RUNTIME_COMPATIBILITY.md:311` 写着：

> 多驱动并发、热切换中途的相位连续性、以及大量顶点通道下的帧耗尚未系统测量。

本文补上了第三项（§2–§4）。前两项仍未测 —— 多驱动并发的相位逻辑有单元测试覆盖（`native/tests/animation_driver_tests.cpp`），但没有帧时间数据；热切换中途的相位连续性依赖 `EnsureAnimationDriverLoaded` 的 `LoadedPath` 短路，逻辑上相位会随 `LoadFromManifest` 的 `drivers_.clear()` 归零，实际观感未验证。

另外两处文档表述建议修正：

- 规范 §14「内存级流式直读」→ 应说明是「读回调流式」，且逐 64 KiB 块有双拷贝。
- 运行时文档 §3 只写了「两条路径等价、Android 只有 channel」，没有写**整通道上传的带宽量级**。创作者需要知道「形态通道覆盖的顶点数」直接决定每帧上传量。

---

## 10. 修复优先级建议

如果只做三件事：

1. **`Logger` 改成持有句柄 + 删掉两条逐条日志**（§11）：改动最小、收益最大，直接决定「材质包加载卡不卡」。实测每条 14 ms，材质包动辄上百条。**已做**（§11.6）。
2. **权重脏检查**（§4）：一次性解决 `Paused`/`Finished` 的永久重传，Android 直接受益。
3. **预计算 delta 下标 + 持久 positions**（§3）：真实包 0.55 → 0.085 ms/帧，同时把 100 万 delta 的病态包从 53.7 ms 压到 13.6 ms。

材质属性名与 `MethodContract*` 缓存（§5）与 P1-4（base 副本）属于第二梯队。

---

## 11. 追加（同日）：不带控制器的材质包为什么也慢

用户反馈：**不装动画包、只用不带 `drivers` 的材质包，也感觉性能下降，而且这一版更明显。**
本节回答这一条，结论与 §1–§8 的逐帧分析无关。

### 11.1 先排除驱动路径

无 `drivers` 时，`ResourcePump` 里驱动那一段的每一步都有早退，逐帧成本约等于 0：

| 调用 | 早退条件 | 位置 |
| --- | --- | --- |
| `EnsureAnimationDriverLoaded` | `DriverCount()==0` → 慢路径；包路径命中 `s_driverless_packages` → `continue`（每帧只剩一次 `path::string()` + 一次哈希） | `4890-4952` |
| `PollAnimationDriverControls` | 共享内存轮询，无分配 | `4955-4970` |
| `ApplyDriverMorph({})` | `!g_driver_morph_active && values.empty()` → 直接 return | `3129` |
| `ReleaseDriverMaterials(nullptr)` | `g_driver_material_writes.empty()` → 直接 return | `3363` |
| `SyncDriverSounds` | `Summaries()`/`Sounds()` 返回空，两个空 `unordered_map` 不分配 | `3236-3238` |
| `PublishDriverStateIfChanged` | 空状态与上次相同 → return | `4818` |

`DriverAudioAvailable()` 只是原子读（`driver_audio_win.cpp:377`）。所以「材质包也慢」**不是驱动拖的**。

### 11.2 真正的原因：加载路径上逐条打日志，而日志器每条 open/close 一次文件

`Logger::Write`（`logging.cpp:16-30`）每写一行就 `std::ofstream(..., app)` 打开、写、析构关闭。实测本机（F: 卷，文件约 120 KB）：

| 写法 | 每条成本 |
| --- | --- |
| 打开-追加-关闭（= `Logger::Write` 现在的做法） | **14.1 ms** |
| 只读打开 + 关闭 | 0.61 ms |
| 一次打开 + 20 次写 | 0.80 ms |

成本几乎全在 **open-for-write**，而且**随文件变大而变慢**（同一台机器另一轮测试里，120 KB 文件到 36 ms/条）。这是 Windows 实时防护在 write-open 时扫描整份文件的典型表现；没有实时防护的机器上单次约 20–100 µs，仍比「保持句柄」贵两个数量级。

加载路径上有两处**逐条**日志，正好都是材质包的重灾区：

| 日志 | 频率 | 位置 |
| --- | --- | --- |
| `"Applied BEM 1.5 material overrides: ..."` | **每条带覆盖的 draw 一条**（1.5 新增） | `module.cpp:2252` |
| `"  built t=<名字> WxH mips=..."` | **每张构建出来的贴图一条** | `module.cpp:2042` |

真实 1.5 样例包（10 张贴图、12 条带覆盖的 draw）就是 22 条 → 约 **310 ms**。一个 draw 数上百、贴图二三十张的材质包，加载停顿在秒级：256 draws + 30 张贴图 ≈ **4 秒**，全部花在打开日志文件上。

这解释了「这一版更明显」：`material_overrides` 与它那条日志是 1.5 才有的（`9e093a85` 引入），1.4 及以前的材质包不会走这条路径；而贴图那条日志本来就有，只是以前没人把「材质包 = 贴图多」和「加载卡」联系起来。

### 11.3 次要因素

- 每个属性一次 `NewString` + `Contract()` 135 项扫描 + `MethodInfo.Invoke`（§5）；`skip_validation` 下配额 ×4，单 draw 最多 352 个属性。
- 材质实例数 = draw 数（`PrepareDrawMaterials` 每条 draw 克隆一份材质，`module.cpp:2547-2554`）。材质包普遍 draw 多，实例多 → 合批变差，这一项与版本无关。
- 贴图体积：1.2+ 把单张上限从 64 MiB 提到 256 MiB、常驻从 768 MiB 提到 1536 MiB（§8 P2-3）。显存压力是持续的掉帧，不是卡顿。

### 11.4 怎么在用户机器上确认

1. 打开 `BetterEndfieldNext/logs/BetterEndfieldNext.log`，数一下 `Applied BEM 1.5 material overrides` 与 `built t=` 的行数；
2. 看这些行的时间戳间隔 —— 如果相邻两行差 10 ms 以上，就是本节这条；
3. 对照：把日志级别降下来（或临时删掉这两条 `Log`）后再加载一次，模型出现的时间应当明显缩短。

### 11.5 修复

按收益排序，都很小：

1. **`Logger` 改成持有 `ofstream` 成员**（`Initialize` 时打开，析构 flush）。这一条单独就能把每条日志从 14 ms 降到微秒级，并且顺带修掉所有其他日志的开销。
2. **删掉 `Applied BEM 1.5 material overrides`**，或改成在 `PrepareDrawMaterials` 结束时汇总一条（`draws=N overrides=M floats=... colors=...`）。
3. **`built t=` 降级为「仅首张 + 汇总」**，或只在尺寸/格式与预期不符时打（失败路径已经有单独的日志）。
4. 顺手加日志文件轮转（当前 append-only 无限增长，越玩越慢）。

### 11.6 落地记录（2026-10-25）

前三条已改，第四条未做。

| 项 | 改动 |
| --- | --- |
| Logger | `logging.h` 增加 `std::ofstream stream_`；`Initialize` 打开一次，`Write` 每行 `flush()`。打开失败时在 `Write` 里重试（清 failbit 再 open），不会每行都重新扫文件。 |
| 材质覆盖日志 | `ApplyMaterialOverrides` 里的 `Log` 删除，新增 `OverridePropertyCount()`；`PrepareDrawMaterials` 汇总成 `Applied BEM 1.5 material overrides: C<n> draws=N properties=M` 一条；`PrepareKeepMaterials`（`CopyMaterials` 那条路径）同样汇总 `slots=/properties=`。 |
| 贴图日志 | `CreateTextureFromBem` 只对本次事务的第一张打完整明细（结尾标 `(first of this transaction)`）；其余进 `ConstructionScope::texture_formats` 直方图，由 `Model upload transaction` 汇总行以 `textureFormats=BC7 sRGB x8,RGBA32 linear x2` 输出。没有 construction scope 时保持原样。 |

「第一张」用的是新加的 `ConstructionScope::texture_detailed` 计数，而不是 `texture_constructed`：后者在 `Texture2D` 构造成功时（`module.cpp:1951`）就自增，而明细行在整条上传链末尾，中间有 5 个失败出口。若用 `texture_constructed == 1` 判断，一张在 ctor 之后、上传之前失败的贴图会占掉"首张"名额，导致本事务里第一张**成功**的贴图没有明细行——恰好是排障最需要的那一行。`texture_detailed` 在明细行处自增，语义对齐。

实测对照：

| 写法 | 每行 | 来源 |
| --- | --- | --- |
| 改前：每条 open/append/close | 13.3 ms | `bench_log4.cpp`（模拟实现，256 行，目标文件约 230 KB） |
| 改后：持久句柄 + 每行 flush | 0.08 ms（166×） | 同上 |

上表是模拟实现的数。换成真实的 `Logger`（`logger_probe.cpp`，直接链接 `logging.cpp`，两侧各 200 行、同一目录、同机 Defender 开启）：

| 写法 | 每行（3 次） |
| --- | --- |
| 改前：每条 open/append/close | 10.6 / 10.1 / 11.6 ms |
| 改后：`Logger` 持久句柄 + 每行 flush | 0.023 / 0.017 / 0.026 ms |

即 **450–600×**。真实实现比模拟版还快一点，因为模拟版每次都要重建 `std::tm` 的 `put_time` 开销而没走 `filebuf` 的缓冲路径。

同一探针还实测了 §11.6 下面两条「行为变化」，不是推理：

```
readable_while_alive=yes          # Logger 存活期间，另一个 ifstream 读到全部 3 行
sees_new_line_while_alive=yes     # 再写一行，同进程立刻读到
line_count=3
```

以及崩溃可读性：`crash` 模式写完最后一行后直接 `std::_Exit(0)`（不跑析构），文件尾部完整保留 `tail before hard exit` 那一行。所以「每行 flush」确实换来了"崩溃后尾行还在"，`hook_chain_tests.cpp:195` 那条同文件读取也不会被句柄挡住。

编译验证：`logging.cpp`、`host_runtime.cpp`、`module_manager.cpp`、`hook_broker.cpp`、`module.cpp` 全部用 MSVC 19.44（`-std:c++20 -EHsc -utf-8`）单独编译通过。**未做完整 CMake 构建与实机加载验证**：`build/native_build` 的 CMake 缓存指向已不存在的 `C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools`，重新配置属于另一件事。

行为变化（需要注意）：

- 日志文件在进程存活期间一直保持打开。`hook_chain_tests.cpp:195` 在 `Logger` 仍存活时读取同一文件；MSVC 的 `filebuf` 用 `_SH_DENYNO` 打开，读写并存没问题，且每行都 flush，读到的是最新内容（上面已实测）。
- 用户在运行期删除/重命名日志文件后，后续写入仍进入原句柄（旧实现会重新创建文件）。需要"清空日志"时必须先关游戏。
- `Initialize` 之前调用 `Write` 仍然安全（`stream_` 未 open，走 `Write` 里的重试分支，`file_path_` 为空则直接返回），行为与旧实现一致。

**行号基准**：本报告 §0–§10 的行号对应**改动前**的 `module.cpp` / `logging.cpp`。改动后 `module.cpp` 净增 58 行，§11.6 里引用的都是改后的新行号（`CreateTextureFromBem` 明细块在 `2054-2062`、`PrepareDrawMaterials` 在 `2579`、`PrepareKeepMaterials` 在 `2614`、`ConstructionScope` 在 `545-566`）。

---

## 附：复现

```bash
cd worktree/audit/bench
g++ -O2 -std=c++2a bench_morph.cpp   -o bench_morph.exe   && ./bench_morph.exe
g++ -O2 -std=c++2a bench_morph2.cpp  -o bench_morph2.exe  && ./bench_morph2.exe
g++ -O2 -std=c++2a bench_material.cpp -o bench_material.exe && ./bench_material.exe
g++ -O2 -std=c++2a bench_log3.cpp   -o bench_log3.exe   && ./bench_log3.exe   # §11 的 open/close 成本
g++ -O2 -std=c++2a bench_log4.cpp   -o bench_log4.exe   && ./bench_log4.exe   # 改前/改后四种写法对照
python scan_bem15.py      # 语料里 1.5 包的规模分布
python detail15.py        # 单个包的通道/驱动明细
```

§11.6 的真实 `Logger` 探针（链接仓库里的 `logging.cpp`，不是模拟实现；用 MSVC，因为 `logging.cpp` 依赖 `localtime_s`）：

```bash
# INCLUDE/LIB 指向 MSVC 14.44 + Windows SDK 10.0.26100
cl.exe -nologo -std:c++20 -EHsc -utf-8 -DNOMINMAX -DWIN32_LEAN_AND_MEAN \
  -I native/shared/include -I native/shared -I native/shared/host \
  worktree/audit/bench/logger_probe.cpp native/shared/host/logging.cpp \
  -Fe:logger_probe.exe -Fo:obj/

./logger_probe.exe <dir>             # 存活期可读性：应输出三个 yes / line_count=3
./logger_probe.exe crash <dir>       # 硬退出后尾部：日志末行应为 tail before hard exit
./logger_probe.exe bench <dir> 200   # 每行成本 A/B
```

环境：Windows 10/11，mingw-w64 g++ `-O2`，单线程，数据为 200 帧均值（前 5 帧预热不计）。基准里的数据是合成的（按真实包的顶点数与 delta 数生成），顶点索引随机分布 —— 这比真实情况**乐观**，因为真实 delta 往往集中在局部区域、cache 命中更好；但 `lower_bound` 的随机访问模式也更接近实测。
