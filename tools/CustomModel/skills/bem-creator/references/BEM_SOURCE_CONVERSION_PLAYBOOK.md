# BEM 源转换执行手册

这份文档是**执行者的作业指导**：拿到一个 EFMI / 3DMigoto 源 Mod，怎么一步步读到事实、
怎么判断、判断不了的时候停在哪里。`bem_tool` 负责"格式是否合法"，这份负责"源数据能不能用、
身份能不能定、哪些损失可以接受"。两者不重叠，也不互相替代。

转换不是一条能自动跑完的流水线。`convert` 会把能机械前推的部分推完，剩下每一关都需要
读证据、下判断、留记录。**本文的每个"出口条件"都是硬门槛：不满足就不许进入下一关。**

**输入**：源 Mod 目录（含活动 INI、DDS、可能还有作者自带的 HLSL 与配置）。
**输出**：一个 `.bem` 包，加上一份说明每处证据级别与损失的转换记录。

产物的生成走工具自己的路：先写出可编辑工程（`project.json` + `payloads/`），再由
`pack` / `build` 出包，最后 `validate`。**本文不重复这些命令**，只覆盖"内容从哪来、凭什么认为它对"
这一段——也就是命令之前的所有判断。命令用法见[创作者指南](BEM_CREATOR_GUIDE.md)。

**不覆盖**：格式字段定义（见 [格式规范](BEM_FORMAT_SPEC.md)）、CLI 用法、自动化分级与配方
（见[源 Mod 转换](BEM_SOURCE_MOD_CONVERSION.md)）。

### 0.0.1 作业循环

每一关都按同一个循环走，不要跳步：

| 步 | 动作 | 产出 |
| --- | --- | --- |
| 取证 | 跑对应脚本 / 读 INI / 查原生快照 | 事实，不是结论 |
| 判定 | 拿事实对判据表 | 一条可复核的结论（含证据级别 L1–L4） |
| 落位 | 结论写进 manifest 的具体字段，或写进转换记录 | 字段值 / 排除声明 / 失败记录 |
| 过关 | 出口条件全部满足 | 进入下一关；否则停在原地补证据 |

**任何时候都不许用"看起来对"替代取证**。取证不到的东西，按 L4 处理：不入账、不猜、
保留失败记录。这不算失败，这是正确结果。


---

## 0. 三条元规则

后面所有章节都是这三条的展开。如果只记三句话，记这三句。

### 0.1 能读 ≠ 正确

包能被解析、能通过双端离线校验，只证明**结构合法**，不证明**视觉正确**。

有大量通过校验但必须撤回的情形：

- 材质 donor 整份借过来，未列出的参数也被一起替换，画面变了但格式合法
- 同一张贴图被绑到语义相反的属性上（源的 `Lightmap` 落到原生的 `_BumpMap`）
- 同一份 DDS 放在了正确的槽里，但源里那个槽根本是另一个用途

**判据**：包通过校验后，还要能回答"每个替换 draw 的每个材质属性，凭什么说它对应源里那一项"。答不出来就是未完成，不是成功。

### 0.2 推断必须分级，且必须可追溯

没有任何身份能 100% 精确时，转换靠推断。推断本身不是罪，**不标注、不区分强度**才是。

| 级别 | 依据 | 处置 |
| --- | --- | --- |
| **L1 精确** | 源与目标逐字节相等：IB 字节、索引数、顶点位置三角、DDS surface | 可直接入账，无需声明 |
| **L2 计算锚定** | 由原生快照重算得到唯一命中：VB0 默认描述符、mip CRC32C + DX11 描述符、原 IB 完整 CRC | 可入账，标注"计算锚定" |
| **L3 语义推断** | 同角色剩余排除、同索引数、材质语义、源命名、原作者注释 | 可入账，**必须逐条写明推断依据与不确定点** |
| **L4 假设** | 只有"应该就是它" | 不得入账。保留失败记录，等证据 |

L3 是可接受的终点，但有个前提：**推断链上每一环都要能被第三方复核**。写"依据源角色、色彩空间与原生唯一材质槽推断"是可复核的；写"应该是身体法线"不是。

### 0.3 个案推断不沉淀为全局规则

同角色、同系列、同作者的包之间可以复用推断，但复用的方式有限制：

- 在**同一次转换任务内**复用已验证的局部映射，可以（例如同一角色的多个服装包共享同一套旧组件身份）
- 把它写进**全局别名库**，不行（除非该别名本身达到 L1/L2）

原因很实在：一次案例里的"剩余唯一目标"结论，换个包就不唯一了；一个包里 `ps-t15` 是法线，另一个包同一个槽可能是光照图。**把个案升级成规则，等于把一次猜测复制成一百次。**

---

## 1. 转换的四个阶段

一次转换必须按顺序过四关，**不能跳关**。跳过任何一关，后面做的都是白工。

### 阶段零：机械体检 —— "这个包有没有硬伤"

出口条件：机械阻断点全部有结论。这一步花五分钟，能省掉几小时的无效人工适配。

按参考语料的实测分布，失败最大的一类（占 48 个失败包的 46%）是**源 DDS 物理缺失**——活动 INI 声明了 `this = Resource_X`，对应文件在源包和同角色全部同类包中都不存在。这个条件完全机械可判，却曾被逐个手工发现 22 次。

体检要回答四件事：

| 问题 | 判据 | 结论 |
| --- | --- | --- |
| 有没有声明了但文件不存在的资源？ | 逐个 `[Resource*]` 的 `filename` 落盘检查 | 见下面的三级分类 |
| 有不存在的资源，但**没人用**吗？ | 该名字是否出现在 `ps-tN` / `this` / `vbN` / `ib` 等 Draw 路径上 | 无人用 = 模板残留，无害 |
| 索引绘制超出缓冲容量吗？ | `max(first_index + count) × stride` vs 文件字节数 | 报事实，可否修看 §3.1 四判据 |
| 这个入口属于哪一族？ | `TextureOverride_(EntryPoint_)?ComponentN` / `..._HASH_N_N` / `LODN.HASH_N_N` | 决定后续能否走标准流水线 |

**"缺失"必须分三级，混为一谈会误杀能做的包**：

1. **Draw 路径引用** → 真阻断。视觉输出直接丢失。
2. **仅框架命名空间引用**（`Resource\RabbitFx\Diffuse = ref ...`）→ 三步定级，顺序不能颠倒：
   - **名字自带身份吗**？`Resource-<hash>-<n>-<slot>` 这种写法，且同包有 `hash = <hash>` 的
     TextureOverride，说明它按名引用游戏原生贴图。全量语料 250 个此类缺失里有 142 个属于这种情况，
     与成功转换共存——**不要去兄弟包里找 donor，按身份锚定流程核实原生贴图即可**。
   - **否则看槽位名**（表见附 A.3）：`Diffuse`/`Normalmap`/`Lightmap` 是基础数据，缺了就是材质丢基础图，
     按阻断处理；`GlowMap`/`FXMap`/`Rainmap`/`DiscardMap` 属于附加通道，只有转换明确放弃该通道时
     才能降为警告。
   - **槽位名也读不出时**，只能退到文件名里的角色词（`-NormalMap.dds` 之类）。这是弱证据，
     必须在记录里注明"角色来自文件名而非槽位"。
   参考语料里有个成功包带着 40 个此类缺失照常完成——但那是槽位被放弃、或身份可从名字推得的结果，
   不是"框架槽位天然无害"的证明。
3. **无人引用** → 无害。SSMT 生成的导出常留下几十个模板资源块，文件从来没写过。

同理两条容易误判的：

- **`TextureOverride_TextureN` 不是网格入口**。全库 3388 个，比 Component 族（2310 + 1747）还多。它是贴图槽覆盖，当成入口会把整个包的结构判错。
- **`Resource_..._IB` 不一定叫 `_index`**。索引缓冲的判据是"被 `ib = ref` 绑定"，不是命名。

**CrossIB 会让 IB 检查失效**：section 里出现 `CustomShader_ExtractCB1` / `RecordBones` / `RedirectCB1` 时，后续 draw 读的是从别处重定向来的索引，`ib = ref X` 不再约束它们。不排除就会把一个 2178 索引的缓冲判成"需要 240756 索引"。

工具：`bem_triage.py <包目录>`（见工具链附录）。

### 阶段一：定身份 —— "这个源数据对应游戏里哪个东西"

出口条件：每个源入口（Component、hash、旧 IB）都落到一个具体的原生组件/网格/材质槽上，且能说出凭什么。

常见误判：
- 按 `ComponentN` 的数字猜身份。数字是作者随手编的，与游戏内组件编号无关。
- 按网格名字猜。源里叫 `body` 的不一定是原生 body。
- 按"看起来像"猜。噪声图不因为是噪声就是特效贴图。

### 阶段二：定范围 —— "哪些部分该进来，哪些该声明剔除"

出口条件：每个源功能都有归属：承载 / 隐藏 / 剔除（附理由）/ 失败（附缺口）。

常见误判：
- 只做默认状态。外层目录里每一个可达状态都要覆盖，或明确声明折叠理由。
- 用"剔除权限"消化看不懂的东西。权限只覆盖明确列出的事项，不覆盖"我没搞明白"。
- 把必需数据当特效删掉（见第 8 章）。

### 阶段三：定表达 —— "用哪个版本、哪种机制装进去"

出口条件：选定 BEM 版本与机制，状态数在上限内，且不触发互斥约束。

常见误判：
- 一上来就展开成 N 份独立外观，而不是用条件绘制。
- 用形态参数装动画，又同时给它绑手动滑条（两者会互相覆盖）。
- 需要的能力版本没声明，包在老解析器上直接拒收。

---

## 2. 资源身份锚定

### 2.1 证据的获取顺序

按成本从低到高，能停就停：

1. **查已有目录**：角色 catalog、已批准别名库、原生 mip 身份扫描结果。有精确条目直接复用，不要重做大规模几何比对。
2. **比字节**：源 IB 与原生 IB、源 DDS surface 与原生 mip、源 VB 流与原生流。逐字节相等是最高证据，一次比对就够。
3. **比几何**：位置三角重合数、独特位置数、索引范围。注意要比**位置集合**而不是顶点顺序——作者常重排顶点。
4. **算描述符**：用原生快照重算 VB0/IB 默认描述符，或按 mip CRC32C + DX11 描述符重算贴图身份。命中的前提是**唯一且无冲突**。
5. **跨包复用**：同角色其他包里已有 L1/L2 证据的旧入口，可以借过来。跨代映射（旧版网格 → 现版网格）尤其依赖这条。

### 2.2 什么时候允许"推断入账"

三个条件同时满足：

- 推断方向唯一（不存在第二个合理目标）
- 推断链可复核（写得出依据）
- 该结论不会被当作通用规则扩散

缺一个就降级：保留候选、记失败、写清恢复条件（需要什么样的证据才能解开）。

### 2.3 禁止的匹配方式

| 禁止 | 为什么 |
| --- | --- |
| 按 `ComponentN` 数字对应 | 与游戏内编号无关 |
| 按资源文件名/材质名对应 | 同名跨组件常见，且作者会改名、写错字 |
| 只按索引数对应 | 相同索引数的不同网格大量存在 |
| 按"剩余位置"兜底 | 剩余的那个不一定是；常见两个源入口对应同一组件的双子网格（眉毛/睫毛），剩余法会漏掉真实目标 |
| 按图像相似度断言身份 | 同字节只证明"替换图相同"，不证明"旧触发资源相同" |
| 按相邻文件或复用像素补缺 | 缺的源文件不能拿相似图顶替 |

### 2.4 一个反复出现的陷阱：同名不等于同物

同名材质跨组件绑定、同路径文件不同字节、同字节图不同用途，三种都在真实数据里出现过。判据一律回到**消费者**：这个属性最终被哪个 draw、哪个材质槽采样。查不到消费者，就没有身份。

---

## 3. 源数据完整性

这是最容易误判的一章。源 Mod 的 IB/VB 越界非常常见，但**不是所有越界都一样**，处置也完全不同。

### 3.1 越界四判据

拿到"绘制请求数 > 实际 IB 长度"时，逐条核这四个问题：

| # | 判据 | 含义 |
| --- | --- | --- |
| 1 | **缺口是否恰在缓冲末尾** | 缺口在中间 = 数据错位，不能修；在末尾 = 可能只是绘制数写多了 |
| 2 | **剩余数据是否为完整三角形** | 实际 IB 长度必须能被 3 整除，且末三角索引都在顶点范围内 |
| 3 | **末尾是否闭合** | 统计边界边（只被一个三角使用的边）。闭合网格的边界边应很少；出现上百条 = 确实被截断过 |
| 4 | **是否存在成对缺口** | 一个组件的 IB 多出 N 个索引、另一个恰好少 N 个 = 分段数写串了，可以互相修正 |

### 3.2 处置树

```
绘制请求 > 实际 IB
├─ 判据 1 不成立（缺口在中间）
│   └─ 无法修。数据本身错位或缺失，记失败
├─ 判据 2 不成立（长度非 3 的倍数 / 索引越界）
│   └─ 无法修。IB 已损坏
├─ 判据 3 不成立（末尾开放，边界边上百）
│   └─ 无法修。真实文件被截断，缺的三角形不可恢复
├─ 判据 4 成立（另有一处反向多出等量）
│   └─ 可修：把两条 draw 各自改成完整现存 IB。必须确认两处缺口数量精确相等
└─ 四条都通过（末尾、完整、闭合、无对偶）
    └─ 可修：只把绘制数限制为实际 IB 长度，保留全部现存几何
        · 缺失的三角形仍不可恢复，报告必须写明"原越界指令不等价"
        · 此判据**只适用于本包数据**，不能因为"缓冲长度够不够"就批量改数
```

**为什么要把"可修"和"不可修"分这么细**：可修的那类，修正后能得到一个视觉上完整的模型（丢几个三角看不见）；不可修的那类，硬修会得到破洞、拉丝、锥刺，比失败更难排查。

### 3.3 源自身的错误

源 INI/脚本里存在大量作者失误，识别出来属于正常转换工作，不算"猜"：

| 错误类型 | 识别方法 | 修复原则 |
| --- | --- | --- |
| 哈希笔误 | 该 hash 的索引数/几何与声明的组件不符，而同角色另一入口完全吻合 | 按几何证据改指向，保留原作者意图可解释 |
| 分段数写错 | 请求数恰好等于前面某一段的长度（常见是重复计入了一段） | 只改内存绘制数，不动文件 |
| 资源名拼错 | 引用了不存在的资源名，但同一文件里有唯一相近的已定义名 | 按磁盘实际文件修复，报告注明 |
| 关键词拼错 | 例如把 `skip` 写成 `ski`，导致"隐藏"变成了"先自绘一次再继续画" | **不要修成 skip**。真实行为是两次绘制，要照实还原 |
| 漏声明资源 | 调用了 IB/VB 资源却没在文件里声明，而同包存在唯一具名的对应缓冲 | 从同包读取，报告注明这是恢复作者意图、不断言源命令原本可运行 |
| 注入广告 | 源文件里有下载页/广告行 | 只在适配副本里净化，不运行来源脚本 |

**关键词拼错这条要特别小心**：直觉上会想"这明显是笔误，改成 skip 就好了"，但那样会丢掉一次真实绘制，得到少一层外观的包。**照实还原错误行为，比替作者修正更接近源意**。

### 3.4 什么时候该判"缺源文件"

源快照里根本不存在所需的 DDS/网格时，属于不可修。判断要点：

- 资源路径指向的文件不存在（而不是解析失败）
- 同角色其他包也没有该文件
- 该文件对应的原生属性已经定位，只是没有作者版本

这类失败点要写成"缺源文件 + 已定位的消费属性"，而不是继续写"身份不明"——前者告诉作者去补文件，后者让人以为还要再查。

---

## 4. 顶点流与网格结构对齐

### 4.1 三种基数关系

| 关系 | 典型场景 | 做法 |
| --- | --- | --- |
| **一对一** | 一个源网格换一个原生组件 | 直接映射，核流布局与索引范围 |
| **一对多** | 一个源 draw 要分给多个原生材质槽 | 按三角面逐个分类到目标槽，不能整块塞给一个槽 |
| **多对一** | 多个源入口指向同一原生组件 | 常见于双子网格（眉毛/睫毛、毛发分段）。先确认各段索引范围不重叠，再决定保留/替换哪段 |

**一对多最容易错**：单次 draw 覆盖两个材质时，凭材质名猜分割点必然出错。正确做法是把每个三角面按**顶点位置**归属到其中一个原生子网格，得到零歧义的分类。分类有歧义时（某个面同时落在两者），说明映射方向错了，回头查身份。

### 4.2 合并网格的拆分

源作者常把多个部件合并成一个网格，原生是分开的。拆分判据：

- 用索引数与三角数比对两侧之和是否吻合
- 用位置集合判断哪些顶点属于哪块
- 拆分后的每一块都要能对上原生子网格的边界

拆分完成后**必须逐块核骨骼权重**——合并网格的权重是跨部件混在一起的，拆错边界会得到错绑的部件。

### 4.3 同 hash 多段

两个源段写同一个 hash、又没有 `match_index_count` 区分时，不能按段的先后顺序分配。判据：

- 两段各自的索引数 → 对应原生子网格的哪一段
- 段名后缀**不是**运行时匹配条件，不能当依据
- 两段各自的源条件（哪段隐藏、哪段绘制）必须一并还原

### 4.4 流布局

- 流数、每流属性、stride 必须与目标原生布局逐项一致；不一致要转换而不是硬塞
- 蒙皮权重格式要转换：原生的 `Float32x4`/`UInt32x4` → BEM 的 `UNORM16x4`/`UInt8x4`，转换后核最大误差
- `vb3 = vb0` 这类别名写法，只有 vb0 已绑定时才成立，且三路流仍要单独校验
- 缺 UV1 之类的属性时，**不要编造**。写清缺失与视觉影响未验证

---

## 5. 骨骼与蒙皮

### 5.1 调色板容量

每个网格的骨骼调色板是 `UInt8` 索引，实际可用上限约 256，工程上按 **≤120 槽**留余量更安全。合并骨架压缩时先算每个组件需要的槽数，超了要拆组件，不能截断索引。

### 5.2 合并骨架的组偏移

源用一套全局骨骼编号，原生是每组件独立调色板。转换方式：

1. 找到源提供的组大小与组偏移（或从权重引用反推）
2. 按组把全局编号映射到组内编号
3. 把组内编号映射到原生骨名
4. 逐顶点核带权引用总数是否与源一致（这是一个很强的校验：总数对不上说明映射漏了组）

源没有给骨名顺序时，组内顺序属于 L3 推断，必须标注。

### 5.3 零权重槽

源顶点在第 4 槽有骨骼索引、但该槽权重严格为 0 的情况很常见。判据：

- 权重严格为 0 → 该索引无效，可归零
- 权重非 0 → 这个索引是真实绑定，不能动

**只改无效槽，不改任何非零权重。** 这条和"猜测骨骼对应"是两件事，不要混。

### 5.4 跨版本骨骼表断位

游戏在某次更新里往某个角色的骨骼表中段插入了一根副骨时，基于旧版提取的 Mod 里所有 **≥ 插入位置**的骨骼编号会整体错位一位。这不是作者的错，是断代。

识别特征：
- 脚趾/头部等末端骨骼被"拉"向躯干（错绑到手臂/脚趾）
- 头发、角、配饰在空中横飞
- 源 INI 里有"某编号以上 +1"之类的注释（说明作者在 PC 端用运行期 Compute Shader 动态补位，脱离后就错）

修复：对错位区间做**自顶向下**的重映射（高位先搬，避免覆盖）。例：插入点是 60，区间 [60, 66] 的映射要从 66→67 开始往下做。

**这类问题只出现在特定角色的特定骨骼表上**，不能套用到其他角色。目前已知受影响的是**伊冯**的外衣骨骼表（`cloth_01`，第 60 位插入了副骨 `Bip001_R_UpperArm_tz_minus`），导致所有 ≥60 的编号整体前移一位：左脚趾被错绑到右上臂、头部角与发箍被错绑到右脚趾、右小腿与脚掌连带错位。其他角色是否受影响，要单独比对骨骼数量与源版本，不能因为"看着像错位"就套用同一套重映射。

### 5.5 运行期骨骼重定向不是特效

源里用 Compute Shader 提取 CB1、记录骨骼、再按部件偏移改指向（常见于跨 IB 绘制）的，属于**基础绘制链路**。删掉它不会"少一个特效"，而是网格蒙皮直接错乱或消失。

判据：这段 Shader 是否向后续 draw 提供骨骼/矩阵。提供 → 不能删。不提供、只改外观 → 可能属于可剔除的特效。

---

## 6. 材质路由

这是错误密度最高的一章。绝大多数"包能读但画面不对"都出在这里。

### 6.1 六条铁律

1. **donor 前先查目标材质自己的纹理表。** 目标原生材质本身可能已经持有同名纹理和槽位。此时借别人的整份材质，会把未列出的参数一起换掉。
2. **donor 是整份复制，不是逐项覆盖。** 用别组件的材质时，那一整份材质（包括你没打算改的参数）都会生效。要么接受，要么逐项补齐。
3. **同名材质跨组件时必须按 draw 绑定，不能按材质名全局替换。** 两个组件共用同一个材质名但需要不同贴图时，全局替换会串色。
4. **`keep` 的组件要逐个查贴图消费者。** 只替换主网格、让旁边的组件保持 `keep`，而那个组件消费的正是被改写的贴图 → 效果漏掉。要么把它一起替换并附同一贴图，要么明确记录未覆盖。
5. **同字节 ≠ 同语义。** 两张图字节相同，不代表它们在该材质里承担同一角色。判据是属性名与 draw 绑定，不是文件内容。
6. **槽位残留要显式压制。** 原生材质自带发光图、清漆遮罩等槽位时，替换 draw 只绑 D/N/P 会让渲染器回退采样这些残留，出现莫名其妙的青绿反光/自发光。此时要么用关键字或权重关掉通道，要么绑 1×1 纯黑占位贴图。
   典型情形是**祀的外衣**（`M_actor_jsspsi_cloth_01`）：该材质自带 `_EmissionMap`（`T_actor_jsspsi_cloth_01_E`）与 `_ClearCoatMask`（`T_actor_jsspsi_cloth_03_M`），PC 端用 `ps-t13 = null` / `ps-t20 = null` 显式置空；转 BEM 后这两个槽会被回退采样，导致外衣与袖子在强光或特定角度泛出刺眼的青绿色金属反光。压制后即恢复正常。

### 6.2 错误模式库

| 症状 | 根因 | 排查 |
| --- | --- | --- |
| 某部位颜色变了但没换贴图 | donor 整份替换带入额外参数 | 查 donor 材质的所有属性，与目标逐项比 |
| 两个部件串色 | 按材质名全局替换 | 改成按 draw 分别绑定 |
| 替换了脸但眉毛还是旧贴图 | `keep` 组件漏消费者 | 查该贴图被哪些 draw 采样 |
| 法线/光照完全不对 | 属性语义错配（`Lightmap` ↔ `_BumpMap`） | 逐个核源 shader 槽与原生属性，不能按文件名 |
| 强光下泛金属/青绿反光 | 原生清漆/发光槽残留 | 关通道或纯黑占位 |
| 半透明变成实心 | 混合因子没表达 | 用 base color alpha + 混合关键字 + 深度写入 |

### 6.3 `ps-t*` 到原生属性的映射边界

源用 `ps-t13`、`ps-t18` 之类的寄存器槽写贴图，BEM 用的是具名材质属性。这个映射**没有通用规则**：

- 同一角色的不同包，同一个槽可能指向不同属性
- 同一系列的不同作者，可能把法线和光照图写反
- 没有编译 Shader 反射或实机捕获时，只能靠"源命名 + 同角色已证包 + 原生唯一材质槽"三重支撑做 L3 推断

**因此：这类映射永远逐包推断、逐包标注，不得升级为全局规则。**

**一个例外，但它只解决一半**：槽位由框架规定时，映射是确定的——`Resource\RabbitFX\Diffuse`
这类槽位名本身就是属性声明（见附 A.3），不需要推断。但两级不能混：框架说"`t70` 装的是 Diffuse"，
**不等于**"BEM 的原生材质里有叫 Diffuse 的属性"。前者是源的词汇，后者仍要按 donor 的原生槽去核。

另外两类要单独识别：

- **`ps-tN = null`（显式清空槽）**：BEM 的材质覆盖没有"清空纹理槽"的能力。遇到这类，要么用关键字/权重达到等价效果，要么如实记失败（这不是能靠换算绕过的问题）。
- **`ps-t` 直写像素但无具名属性**：说明源在改一个原生材质没有对应名字的槽，需要先找到消费者，找不到就是失败。

### 6.4 多材质单子网格

原生一个子网格挂两个材质、源只有一个 draw 段时，BEM 要生成两段 draw 重播同一索引段。这个 pass 次数通常没有实机捕获可证，属于必须记录的限制。不要因为"格式能表达"就宣称材质等价。

---

## 7. 状态空间

### 7.1 可达状态要算准

三个坑，一个比一个隐蔽：

1. **持久键的初值可能不在循环枚举里。** 源 `ini` 保存的是上次的值，初值可能是循环里没有的那一档。漏算会少一整片状态。
2. **多个持久键共用一个物理按键时，要按保存值全覆盖，不能按"按一次切一次"推。** 保守做法是把所有组合都打进包。
3. **条件绘制用符号节点压缩时，组合数是乘积。** 五组 3/5/6/9/5 就是 4050 种，不是 28 种。

### 7.2 可以折叠的情况

| 情况 | 判据 | 处置 |
| --- | --- | --- |
| 某个键对选定范围无视觉差异 | 逐态比对绘制签名，完全相同 | 折叠，并在报告写明折叠理由 |
| 某个键只影响已被剔除的内容 | 该键的所有影响都落在剔除项里 | 折叠，不虚构剔除项 |
| 某个键只改变"后续按键是否可用"（锁键） | 两个保存值绘制相同 | 折叠 |

**折叠必须有比对证据**，不能因为"看起来没区别"就折叠。

### 7.3 条件绘制而不是展开

上限抬升后，绝大多数组合数都不需要展开成独立外观：

- 每组选项 64 项、组数 64、候选 4096
- 用 `when` 控制 draw，用空绘制表达隐藏（隐藏优先用组件级 `hide`）
- 一个包里表达几万到几千万种组合是正常的，不是异常

**判断该不该展开**：如果两种状态只差"某组件显不显示"，用条件；如果差的是完全不同的几何，才考虑独立外观。

---

## 8. 剔除边界

这一章是"能不能删"的判定。删错的代价比不删高得多——删掉基础绘制会得到一个破模型，而不删只是多带了一点用不上的内容。

### 8.1 判定树

```
这段源内容可以删吗？
├─ 它是否是选定角色的基础外观的一部分？
│   ├─ 是 → 不能删
│   │   （基础网格、基础 D/P/N/E 贴图、活动骨骼/材质程序、DrawMesh 承担的手绘）
│   └─ 否 → 继续
├─ 它是否向基础绘制提供数据？
│   ├─ 是 → 不能删
│   │   （CrossIB 骨骼、CB1 重定向、骨骼调色板、语义是 Diffuse 的 RabbitFX 输入）
│   └─ 否 → 继续
├─ 它是否属于已明确授权的可剔除类别？
│   ├─ 低模 LOD（当前运行链路锁 LOD0）→ 可删，报告写范围
│   ├─ 全局雨雪覆盖（跨包共享）→ 可删，但必须双字段声明
│   ├─ 纯实时 Shader 执行（不影响静态外观）→ 可删
│   ├─ 作者 UI/通知/广告 → 可删
│   └─ 都不是 → 保留，或记失败
└─ 无法判断它是否为基础外观
    └─ 先查消费者。查不到 → 记失败，不要删
```

### 8.2 三个"看起来像特效但不能删"

| 内容 | 为什么不能删 |
| --- | --- |
| `DrawMesh` 类调用 | 经常承担基础手绘/基础网格绘制，不是附加特效 |
| RabbitFX 的 `Diffuse` | 这是**基础色**。把它当发光特效删掉，模型会变成白模 |
| 局部 mip 覆盖 | 只替换某几级 mip 的写法，说明作者在意采样结果，不是可忽略细节 |

### 8.3 动画：没有原生通道，但可以近似

这一条在前几版工具里被记成"新增动画系统，可剔除"，那已经过时了。现在的情况是**能做，但会掉质量**，判断的不是"能不能"而是"值不值"。

**近似的原理**：把动画逐帧解算成每个顶点的位置偏移，烘焙成形变帧，再用时钟波形扫过这些帧。

**三个决定质量的硬数字**：

| 规格 | 数值 | 后果 |
| --- | --- | --- |
| 每形变通道帧数 | 2..64 | 原始动画超 64 帧必须降采样 |
| 帧间求值 | 线性插值 | 帧越稀，两条相邻帧之间的"直线"离真实弧线越远 |
| 波形 | 5 种闭集 | 时间曲线（缓入缓出、停顿、变速）只能挑最接近的 |

**六项损失，按严重程度**：

1. **体积塌陷/关节折叠** —— 旋转本应走圆弧，顶点直线插值走弦。大角度摆臂、肘膝弯曲时肢体变细、腋下与膝窝自穿。
2. **帧数被砍** —— 快节奏动作出现跳帧感，细节动作消失。
3. **光照不跟随** —— 规范明确规定：形态驱动只走顶点位置，不触碰法线/切线/UV。所以转身后高光与阴影停在原朝向，暗部"贴"在模型上。**这不是实现缺陷，是设计边界**，无法通过参数绕过。
4. **骨骼实际不动** —— 依附骨骼的东西全失效：武器挂点、飘带物理、碰撞、脚底贴地。
5. **时间曲线走样** —— "停顿 3 帧再爆发"这类节拍一律丢失。缓入缓出用正弦类波形最接近。
6. **多段动画互相挤占** —— 每包驱动数、每驱动绑定数都有上限，一段动作占一个驱动。

**判据**：

| 源动画特征 | 结论 |
| --- | --- |
| 幅度小、≤64 帧、以材质/形态变化为主、不依赖骨骼挂点 | 近似后基本看不出，值得做 |
| 材质参数随时间变（发光呼吸、颜色渐变、贴图偏移） | 原生支持，直接做，不算近似 |
| 幅度大、依赖关节旋转、时长长、靠骨骼带动物理或挂点 | 近似后一眼假，如实剔除并声明，不要交付"能动但难看"的包 |
| 只有动画、没有任何静态内容 | 记失败。不要造一个全 `keep` 的空包冒充成功 |

**一个连带约束**：动画用的形态参数，不能再被手动滑条控制同一批顶点。运行期每帧改写与加载期整条上传会互相覆盖，表现为原位残影、闪烁、表面发虚。需要"滑条定基准 + 动画往复"时，拆成两个参数、绑互不重叠的顶点区间。

**另一个容易忽略的状态语义**：驱动器有四态（停止 / 播放 / 暂停 / 播完），**只有"停止"会释放参数**。暂停和"播完"都继续每帧投递冻结相位或末帧采样，属性和顶点保持不变。所以一次性的单程动画播完后，模型会**停在末帧姿势**而不是回到原状——如果设计意图是"推出去再弹回来"，那是往复波形的事，不是单程波形。

还有一条只影响关键字的坑：关键字没有静态覆盖时，运行时一律按关闭处理。若目标 Shader 原本开着同一关键字，驱动停止后会和作者预期不一致。需要语义保证时，同时在材质覆盖里显式声明该关键字。

---

## 9. 验收与披露

### 9.1 双端离线校验证明什么

| 能证明 | 不能证明 |
| --- | --- |
| 包结构合法、字段自洽 | 画面与源 Mod 一致 |
| 按所选状态能读出几何与材质 | 材质属性映射正确（可能全对但语义反了） |
| 索引/顶点/贴图在范围内 | 骨骼动画视觉正确 |
| 能力声明与版本匹配 | 透明、混合、Shader 效果的观感 |

所以**离线通过是必要条件**，不是完成条件。完成条件还包括 0.1 那一问："凭什么说这个属性对应源里那一项"。

### 9.2 必须披露的内容

每包报告至少要有：

- `render_verified` = false（未做任何实机验证时）
- `full_source_behavior_complete` = false（有未承载的源行为时）
- `excluded_features`：逐项列出实际剔除的源功能，**没有就不写，不许虚构**
- 每条 L3 推断的推理链与不确定点
- 全局雨雪/跨角色效果未承载时的独立标记字段
- 范围外内容（备用 LOD、其他角色的影响、未验证的 pass 次数）

**一个制度性的教训**：字段覆盖率不取决于"要求写"，取决于"不写会怎样"。参考语料里，被登记闸门检查的字段覆盖率是 100%，闸门不管的 `excluded_features` / `limitations` 只有 84–89%，一次性审计就查出 135 处缺失。**凡是"应该写"的东西，都要变成"不写就登记不进去"。**

### 9.3 写包：原子替换 + 逐字节回读

写产物这一步有三个动作，缺一个就会在中断时留下半成品或在落盘时悄悄改内容：

```
写临时文件 → os.replace 原子替换 → 读回 → 校验读回的结构与几何
```

**先说清楚哪些是现成的、哪些要自己做**，别重复实现，也别以为已经有了：

| 环节 | 现状 | 证据 |
| --- | --- | --- |
| 原子写入 | **已内建**，不用自己写 | `bem_v1.atomic_write()`（`bem_v1.py:42-54`）用 `mkstemp` + `os.replace`，`write_package()` 在 `bem_v1.py:236` 调它 |
| 载荷去重 + 索引重映射 | **已内建，且会改清单** | `write_package()` 在 `bem_v1.py:187-217` 把相同字节的 payload 合并，并把 `draw['indices']`、`stream['payload']`、`texture['payload']`、形变帧的 `payload` 全部按 `mapping` 重写 |
| 写盘后回读校验 | **没有内建**，`pack` / `convert` 都不做 | 需要自己调 `bem_projects.validated()` 或 `bem_v1.read_package()` |

**这里有个陷阱**：因为写盘时会去重并重映射，`read_package(输出)` 得到的清单**不等于**你内存里的 `builder.m`。直接用 `assert m == builder.m` 会在一堆包里假失败（只要有两个 payload 字节相同）。同理 `builder.payloads` 是去重前的列表，读回的是去重后的。

正确的回读校验写成"重新校验"而不是"字节相等"：

```python
from pathlib import Path
from bem_projects import validated      # 内部走 read_package + check_geometry

def write_and_verify(builder, output: Path):
    """builder 是 bem_v1.Builder：清单在 builder.m，载荷在 builder.payloads。

    属性名别记错：清单是 .m，不是 .manifest；bem_merge.MergeResult 没有
    write()，它只算"哪些段生效"，不是产物对象。
    """
    builder.write(output)              # 内部已原子替换
    m, p = validated(output)           # 真的把文件读回来，重跑结构与几何校验

    # 语义层面的对照：数量必须对得上（去重只会减少，不会增加）
    assert len(m['meshes']) == len(builder.m['meshes']), 'READBACK: 网格数变化'
    assert len(m['textures']) == len(builder.m['textures']), 'READBACK: 贴图数变化'
    assert len(p) <= len(builder.payloads), 'READBACK: 载荷数变多，去重逻辑异常'

    # 幂等性检查：对读回的模型再写一次，字节必须完全相同。
    # 这是比"与内存模型相等"更强的检查——它同时验证了去重与重映射是稳定的。
    import hashlib, tempfile, os
    fd, again = tempfile.mkstemp(suffix='.bem'); os.close(fd)
    try:
        from bem_v1 import write_package
        write_package(again, m, p)
        assert Path(again).read_bytes() == output.read_bytes(), 'READBACK: 重写字节不一致'
    finally:
        os.unlink(again)
    return m, p
```

为什么不能"写到位再检查"：写到一半断电，磁盘上就有一个头部声明完整但 payload 缺失的文件，而所有"文件存在"的判断都会通过。`os.replace` 在同一分区上是原子的，读者要么看到旧文件，要么看到新文件。这一层 `atomic_write` 已经做了，你不用管；你要管的是**回读**。

### 9.4 账本即真相，报告不能自证

如果一批转换要持续几个月、几百个包，必须有一个**唯一权威状态**，而且**它不能相信任何自报数字**。

参考语料的两条设计值得照搬：

1. **登记完成时重新读包，不信报告**。报告说外观数 16777216，就重新按 `option_groups[*].choices` 连乘算一遍，不等就拒绝登记。
2. **撤回只能移动，不能删除**。撤回时把正式产物移到归档区、保留旧报告路径，不允许 `rm`。

同时要承认它的局限：**账本管得住"登记那一刻"，管不住"之后"**。参考语料里有一个"成品"在登记后被换成了 50 MB 的 Windows PE 可执行文件，账本仍记着成功——登记时是对的，之后再没人回来看。

所以**权威状态 + 周期性独立审计**是配套的，不是二选一。审计要查的是账本不查的维度：文件是不是 BEM 容器、包 ID 对不对、外观数是否失控、披露字段在不在。

### 9.5 撤回

发现已入账的包存在结构性错误（donor 误借、属性语义反了、基础内容被当特效删了）时，**撤回而不是打补丁**：

- 包移出成品目录，台账改失败
- 保留旧包与旧报告，写明撤回原因
- 重新生成候选后走同一套校验

补丁式修正会让"这个包为什么是这样"变得无法追溯，比失败更难处理。

---

## 10. 失败也要分类

失败不是一种状态。四类失败，恢复条件完全不同，写错类别会导致后来的人做无效工作。

| 类型 | 特征 | 恢复条件 |
| --- | --- | --- |
| **缺源文件** | 资源路径指向的文件不存在，原生属性已定位 | 作者提供文件即可；不需要再查身份 |
| **身份未证** | 有候选，但达不到 L1/L2，且存在多个可能 | 需要原生捕获（DX11 描述符、Shader 反射、实机帧） |
| **能力缺失** | 源行为清楚，但 BEM 表达不了（清空纹理槽、骨骼通道、自定义算法） | 等格式升级；或改用有损降级并声明 |
| **质量不可交付** | 技术上能做，但降级后效果明显劣化 | 不是等技术，是取舍。要么接受降级并声明，要么不做 |

### 10.1 真实分布：48 个失败包的实际构成

对参考语料 551 个包的失败记录逐条归类（按根因，不按错误码字面）：

| 根因 | 包数 | 占比 | 关键判据 |
| --- | --- | --- | --- |
| **源 DDS 物理缺失** | 22 | **46%** | 声明了但文件不存在，同角色全部同类包也没有 |
| 身份未证 | 8 | 17% | hash 在原生索引零命中，不能按序号猜 |
| 源 IB 损坏/越界 | 4 | 8% | 缺口不在末尾 / 剩余非完整三角形 / 末尾开放 |
| 跨格式机制（CrossIB、CB1、Compute Shader） | 3 | 6% | 运行时骨骼劫持、多阶段重定向 |
| 非角色对象（prop / 武器 / 鞘） | 3 | 6% | 目标不在角色 catalog |
| 运行时生成材质 | 2 | 4% | 皮肤/妆容由 Shader 逐像素生成 |
| 多角色同包 | 1 | 2% | 单 target 表达不了 |

**这张表最该记住的一点**：最大的一类（46%）判据最简，却最难自动化之外的"最难"在身份未证（17%）。所以投入产出比最高的是把"查文件缺失"做成开工第一步，而不是先攻身份。

### 10.2 一个反直觉的数字

同一批失败记录里，有 6 个包记的是"超出旧格式上限"。格式上限提高之后，它们的**阻点已经不存在，但没有人回头重做**：

| 包 | 原阻点 | 现上限 |
| --- | --- | --- |
| 单贴图 12800² 单级 DDS | 144 MiB > 64 | 256 MiB |
| 每选择 38 张贴图 | > 32 | 64 |
| 1551 条 draw | > 512 | 4096 |
| 40 / 48 个选项 | 每组 > 16 | 每组 64 |

**失败记录必须写"缺什么"，而"缺什么"会过期。** 一份没人回收的失败清单，会把"现在能做"的包永久锁死。所以失败记录要能按"阻断类型"检索——类型稳定的（缺源文件），一升级就该批量重跑；类型依赖版本的（超上限），要挂版本号。

### 10.3 报告要求

每份失败记录必须写清"缺什么才能解开"，且要具体到可以照着去找。写"身份不明"是无效失败记录；写"`ps-t15` 缺 Shader 反射确认其属性与绘制范围"才可执行。

另外：**必须写"这只是当前版本的判断"**。凡是受格式能力限制的结论，都要带一个可复查的锚点（哪个上限、哪条规范），否则半年后没人知道该不该重做。

---

## 11. 落位：判断结果写进哪里

判断必须落到具体字段，否则等于没判。这张表把每一关的结论对应到 manifest 的位置，
字段定义本身见[格式规范](BEM_FORMAT_SPEC.md)，这里只讲"哪条结论该写在哪"。

| 结论 | 落位 | 说明 |
| --- | --- | --- |
| 这个源入口对应目标的哪个部件 | `meshes[].component` | 由身份锚定决定，不由源的 `ComponentN` 数字决定 |
| 这段源几何对应目标哪套骨骼 | `meshes[].bones[]` | `component` + `index` + `name` 三项必须同时对上 |
| 每个绘制段的索引范围 | `meshes[].draws[]` 的 `indices` payload + `count` | 1.1+ 每条 draw 自带 payload，`count` 为 3 的倍数 |
| 这段绘制用哪个材质 | `draws[].material_component` + `material_slot` + `material_name` | 三者必须同时指向 donor 的同一个原生材质槽 |
| 这段绘制用哪几张贴图 | `draws[].textures` → 顶层贴图表 | 同一 draw 不能重复引用同一项 |
| 贴图身份锚定的结果 | 顶层贴图表的 `original_name` + `format` + `srgb` | `original_name` 写 donor 上被替换的原 Texture 对象全名 |
| 状态 → 可选项 | `option_groups[]` + `draws[].when` | 条件绘制优于展开成 N 份外观 |
| 形态键 → 连续参数 | `parameters[]` + `mesh_deformations` | 位置增量是绝对偏移，按 `tick` 线性插值 |
| 材质覆写 | `draws[].material_overrides` + `required_capabilities` | 声明 `material-overrides-v1`，属性名以 `_` 开头 |
| 需要的能力版本 | `required_capabilities[]` | 漏声明 = 老解析器直接拒收，不是降级 |
| 某一项为什么没做 | 转换记录（不进 manifest） | 必须写"缺什么才能解开"，写到可以照着去找 |
| 剔除 / 隐藏的声明 | 转换记录（不进 manifest） | 只覆盖明确列出的事项，不覆盖"没搞明白" |

**最后一条最容易做错**：manifest 表达不了"我为什么没做某件事"。这类结论必须进转换记录，
随包一起交付。包里没有这个字段，不代表这件事不需要交代。

## 12. 必须停下报告的情形

下列情形通常意味着源数据存在严重缺陷。
- 在**协作模式**下：遇到下列情形必须停下，将证据整理清楚后交给用户决策。
- 在**全托管模式（面向小白）**下：Agent 授权作为逆向工程师自主介入。对于存在明确修复可能的问题（如符合四判据的 IB 越界、可在同级包中找到同名同哈希 donor 的缺失贴图、超大贴图降级等），由 Agent 编写并执行专有装配脚本进行自愈（详见第 13 节），无需中途向小白打扰提问；**仅当核心主体资产（主 Diffuse、基础网格、目标角色 catalog）彻底缺失且完全不可恢复时，才终止并向用户报告**。

| 情形 | 为什么必须停 | 报告要带什么 |
| --- | --- | --- |
| 身份只到 L4（"应该就是它"） | 绑错的贴图能建包、能加载、渲染的是错的，比缺贴图更糟 | 候选清单 + 每个候选的排除理由 |
| 缺失 DDS 在同角色全部同类包里都找不到 | 无中生有不可能；换 donor 需要授权 | 缺失清单 + 已检索范围（哪些包、哪些目录） |
| IB 越界，且四条可修条件都不成立 | 重试不能恢复不存在的三角形 | 容量、需求、缺口索引数，逐字给出 |
| 骨骼表超过 256 且无法离线重映射 | 规范要求拒绝，不截断、不自动分片 | 源骨骼总数 + 重映射后的表长 |
| 状态空间超出枚举预算 | 少算的状态是静默少算，报告会显得完整 | 键域 + 已枚举数 + 预算上限 |
| 材质槽语义无法确认 | donor 是整份替换，未列出的参数也会一起换掉 | 槽位 + 该槽上所有已知属性 + 缺哪个证据 |
| 源依赖运行期才成立的行为 | 静态 INI 看不到结果，任何结论都是猜 | 具体是哪一类行为、在哪个段里 |

### 12.1 最后一类要特别说明

有一类源行为**在静态文本里根本看不见结果**：它要么由框架在运行期计算，要么由着色器在 GPU 上判定。
这类行为的共同特征是——源里只有"调用"和"参数"，没有"结果"。

判断方法是看**被调用的那段在哪里定义**：

- 定义在源包内部 → 当普通命令列表读，能读到底（`bem_merge.py` 会算出来）
- 定义在 loader 自带库里 → 按库的语义读，语义是确定的（`references/efmi/` 里有原件）
- 定义在第三方框架里，且框架本体可公开取得 → 按接口事实读（附 A.3）：槽位与委托 draw 都能展开，
  但"框架的槽位名"不等于"原生材质属性名"
- **连本体都拿不到** → 这条通道的实际效果不可知。此时唯一正确的结论是
  "绑定未知，逐 draw 取值读不出"，并如实披露；不能假设"大概是把槽位映射到某属性"。

四类都不要试图用"同名同义"去补：命名相似不构成证据。

---



---

## 13. Agent 专有装配脚本编写指南与自愈模式库

当源 Mod 为非规范包（多状态组合键、包含伴随 INI、DDS 缺失、IB 末尾越界、包含自定义 Shader）导致 CLI 的 `bem_tool convert` 抛错中断时，**在全托管模式下，Agent 严禁停下向小白用户求助，必须作为逆向工程师在工作区编写专有 Python 脚本（如 `workspace/build_mod.py`）完成自主修复与装配**。

### 13.1 专有脚本的标准骨架（Standard Assembly Skeleton）

专有脚本在工作区运行，复用底层 `bem_v1`、`component_auto`、`bem_merge`，通过内存中的 `Overlay` 动态净化与修复。

**先弄清为什么需要 Overlay。** `component_auto.prepare()` 第一行是 `sections(src.read_exact(ini).decode('utf-8-sig'))`（`component_auto.py:170`），而 `efmi_source.sections()` 遇到重复段直接 `raise ValueError(f"duplicate INI section: {name}")`（`efmi_source.py:20-21`）。也就是说：**只要入口 INI 里有重复段，`prepare` 连解析都过不去**，与内容是否正确无关。`bem_merge` 会按加载器语义（首次出现生效、include 展开、命名空间重写、水印剥离）算出真正生效的段，压成一个无重复的字典再喂给 `prepare`，就绕过了这个纯语法门槛。这是 Overlay 存在的唯一理由，不是"绕过校验"。

**这不是理论问题，四个真实包的对照**（`prepare` 用的是"入口文件单文件视图"，Overlay 给的是"加载器合并视图"）：

| 包 | 文件数 | 存活段 | 唯一名 | 跨命名空间冲突 | 单文件视图 | 合并视图 |
| --- | --- | --- | --- | --- | --- | --- |
| 伊冯-围裙 | 1 | 134 | 134 | 0 | **解析即抛** `duplicate INI section: Constants` | 命中 `chr_0017_yvonne`，11 个入口 |
| 伊冯-26W | 3 | 477 | 293 | **182** | **解析即抛** `duplicate INI section: Constants` | 过了解析，但报 `SOURCE_IDENTITY`（没有 ComponentN 基础入口） |
| 弧光-02 | 1 | 99 | 99 | 0 | 解析通过 | 解析通过，两者都报 `CHARACTER_CATALOG`（资料不覆盖） |
| 梨诺-颜色切换 | 1 | 274 | 274 | 0 | 解析通过 | 解析通过，两者都报 `CHARACTER_CATALOG` |

读法：**"伊冯-围裙"这一类包，不做 Overlay 就完全无法自动转换**——它在单文件视图下连段都解析不出来，而在合并视图下能正常命中角色资料并产出 11 个部件入口。反过来，弧光-02 和梨诺-颜色切换是 `manual_only`，Overlay 帮不上忙，报的是资料缺失而不是语法问题——**这两种失败必须分开报**，否则会把"资料不覆盖"误诊成"格式不规范"。伊冯-26W 则是第三类：语法过了、身份不成立，需要按 §12 的停止条件处理。

**两条硬约束，写脚本时必须遵守**：

1. **路径要相对定位，不能抄本机绝对路径。** 脚本要能在任何人的机器上跑，`F:/…` 这类路径只能出现在你自己的临时调试里。用 `Path(__file__)` 往上找到 `tools/CustomModel`，找不到就报错退出，不要静默降级。
2. **`standard_worker.normalize_entries` 不存在。** 早期文档里的这个符号在本树中没有任何定义（`grep -rn "def normalize_entries" tools/CustomModel/*.py` 为空）。段归一化现在由 `bem_merge` 承担。照抄旧骨架会在 import 行就 `ImportError`，且看起来像"环境问题"，很容易被误判。

```python
"""Dedicated assembly script for non-standard mod conversion."""
import sys, copy, json
from pathlib import Path

# --- 1. 相对定位工具根：从脚本位置向上找，找不到就明确失败 -------------------
def find_tools_root(start: Path) -> Path:
    for base in [start, *start.parents]:
        candidate = base / "CustomModel"
        if (candidate / "bem_v1.py").is_file():
            return candidate
        if (base / "bem_v1.py").is_file() and base.name == "CustomModel":
            return base
    raise SystemExit("TOOLS_ROOT_NOT_FOUND: 把本脚本放进 worktree 内，或用 --tools-root 指定")

TOOLS_ROOT = find_tools_root(Path(__file__).resolve().parent)
sys.path.insert(0, str(TOOLS_ROOT))
sys.path.insert(0, str(TOOLS_ROOT / "skills/bem-creator/scripts"))

import bem_v1 as bem
import component_auto
import bem_merge
from convert_efmi_poc import Source
from efmi_source import value
from bem_projects import validated


class Overlay(Source):
    """In-memory INI overlay that never touches source files on disk."""

    def __init__(self, src: Source, ini: str, sec_dict: dict[str, str]):
        self.source, self.path, self.names = src, src.path, src.names
        self.ini = ini
        self.raw = "\n\n".join("[" + n + "]\n" + b for n, b in sec_dict.items()).encode("utf-8")

    def read_exact(self, name: str) -> bytes:
        if name.replace("\\", "/") == self.ini.replace("\\", "/"):
            return self.raw
        return self.source.read_exact(name)


def flatten_live_sections(merged: bem_merge.MergeResult) -> dict[str, str]:
    """All surviving sections, keyed by written name, as raw bodies.

    Flattens every document (not just documents()[0]): entries frequently live in
    an included file, and prepare() looks them up by name in one flat dict.
    Sections that lost a duplicate are absent — that is the point.

    KNOWN LIMIT: this collapses namespaces. When the same plain name lives in two
    namespaces (measured: 182 such names in one real package), only the first
    survives here. The loader keeps both. prepare() only understands plain names,
    so this is a lossy compromise forced by the route, not a correct merge.
    If merged.collisions() is non-empty, check that the lost copies are not
    referenced by a namespaced `run = CommandList\\Name` you care about; if they
    are, this route is not safe for that package.
    """
    out: dict[str, str] = {}
    for _relative, sections in merged.documents():
        for name, section in sections.items():
            out.setdefault(name, section.body)
    return out


def build_package(source_dir: Path, output_bem: Path, character_id: str, package_name: str,
                  legacy_aliases: dict | None = None, clamp_draws: list | None = None,
                  root_offset_y: float | None = None, material_overrides: dict | None = None):
    src = Source(source_dir)
    merged = bem_merge.merge(source_dir)
    if not merged.entry:
        raise SystemExit("NO_ENTRY: bem_merge 没找到入口 INI，先跑 bem_triage.py 看路由")
    active_ini = merged.entry
    clean_sec = flatten_live_sections(merged)

    # 2. 净化全局雨雪等外部冲突贴图（哈希取自实际源包，不要凭记忆写）
    for section, body in list(clean_sec.items()):
        if section.startswith("TextureOverride_Texture") and value(body, "hash") in ("7d330dbd", "e69d7088"):
            del clean_sec[section]

    # 3. 匹配角色 Catalog（可注入历史哈希别名）
    db = copy.deepcopy(component_auto.catalogs())
    cat = next((c for c in db if c["character_id"] == character_id), None)
    if cat is None:
        raise SystemExit(f"CATALOG_MISS: catalog/ 里没有 {character_id}，不要手改 catalog 凑数")
    if legacy_aliases:
        cat["entries"].update(legacy_aliases)

    # 4. 剥离 3DMigoto 运行时触发器（BEM 里没有这个命令）
    for key in list(clean_sec):
        if key.startswith("CommandList_Draw_Component"):
            clean_sec[key] = clean_sec[key].replace("run = CommandListTriggerResourceOverrides", "")
    clean_sec.pop("CommandListTriggerResourceOverrides", None)

    # 5. 修复 IB 越界：改的是 draw 声明的 count，不是缓冲区本身。
    #    clamp_draws 的每一项必须是 (component_id, declared_count, safe_count, first)，
    #    safe_count 由 bem_repair.py ib-audit 给出，不要自己估。
    if clamp_draws:
        for cid, declared, safe, first in clamp_draws:
            draw_key = f"CommandList_Draw_Component{cid}"
            body = clean_sec.get(draw_key)
            if body is None:
                raise SystemExit(f"DRAW_SECTION_MISSING: {draw_key}")
            old = f"drawindexed = {declared}, {first}, 0"
            new = f"drawindexed = {safe}, {first}, 0"
            if old not in body:
                raise SystemExit(f"DRAW_PATTERN_MISS: 在 {draw_key} 里找不到 {old!r}，先 dump 原文核对")
            clean_sec[draw_key] = body.replace(old, new)

    # 6. 内存装配。prepare 内部已跑 validate_manifest + check_geometry。
    overlay = Overlay(src, active_ini, clean_sec)
    builder, info = component_auto.prepare(overlay, active_ini, database=[cat])
    builder.m["name"] = package_name

    # 7. 注入 BEM 1.5 特性。能力声明与字段必须同时出现，否则校验会拒。
    if root_offset_y is not None:
        caps = builder.m.setdefault("required_capabilities", [])
        if "root-offset-v1" not in caps:
            caps.append("root-offset-v1")
        builder.m["root_offset"] = {"path": "Bip001", "translation": [0.0, float(root_offset_y), 0.0]}

    if material_overrides:
        caps = builder.m.setdefault("required_capabilities", [])
        if "material-overrides-v1" not in caps:
            caps.append("material-overrides-v1")
        for mesh in builder.m.get("meshes", []):
            for draw in mesh.get("draws", []):
                draw.setdefault("material_overrides", {}).update(material_overrides)

    # 8. 原子写入 + 逐字节回读。只写不读回来，等于没验证。
    output_bem.parent.mkdir(parents=True, exist_ok=True)
    tmp = output_bem.with_suffix(output_bem.suffix + ".tmp")
    builder.write(tmp)
    m, p = validated(tmp)          # 真的把文件读回来重新校验
    tmp.replace(output_bem)
    print(f"built & read back: {output_bem} ({output_bem.stat().st_size} bytes)")
    return m, p, info
```

**这个骨架仍然做不到的事，别假装它做到了**：`prepare` 只处理"每个部件一条连续索引区间"的情形（`component_auto.py:218-224` 对 `bindings`、缓冲切换、非零 `base` 一律抛错）。多材质槽重排、跨缓冲 draw、需要重建顶点流的包，走 `bem_assemble.py`，或者按 §13.5 自己写 `Builder` 调用。骨架里没有 `bem_peer` 的调用：借用 donor 是独立决策，见 §13.3。

### 13.2 模式一：IB 索引越界安全裁剪（F3 根因自愈）

当 `bem_triage.py` 报出 `INDEX_BUFFER_OVERRUN` 时，先跑：

```bash
python scripts/bem_repair.py ib-audit <包根>/Meshes/Component0_IB.buf --first 0 --count 54018 --stride 2
```

它会给出 `available_from_first`、`short_by`、`overrun`、`fixable`、`clamped_count`、`dropped_triangles`。**先看数字再决定**，不要凭"越界了"就动手。

**关键纠正：要改的是 draw 声明的 `count`，不是索引缓冲区。** 早期文档给过一个 `clamp_index_buffer()`，把 `.buf` 按 `len//stride` 截断后另存。那个写法有三个致命问题：

1. **它完全无视 `first`。** 越界的是 `first + count > total`，而 `total` 是缓冲区长度。从 `first` 之后可用的索引数是 `total - first`，不是 `total`。`first > 0` 时截断到 `total` 等于没修。
2. **它把缓冲区写短了。** 缓冲区是源包资产，改短会改变 payload 的字节身份，并且同一个 `.buf` 常被多个 draw 共用——为一个 draw 截断，会顺手弄坏其他 draw。
3. **它不判断丢了多少三角形。** 裁剪必然删几何，删了多少必须能报出来，否则交付说明就是假的。

正确做法是只改 INI 里那一条绘制声明的 `count`（见 §13.1 骨架第 5 步），让 `count` 落在 `[3 的倍数, total - first]` 内，缓冲区一个字节都不动：

```bash
# 先算出安全值，再决定接受不接受。结果打到 stdout（不写文件）
python scripts/bem_repair.py ib-clamp <包根>/Meshes/Component0_IB.buf \
    --first 0 --count 54018 --stride 2
# 不接受几何损失时它会拒绝，并给出 IB_TRUNCATION_NOT_ACCEPTED
```

`ib-clamp` 只有在显式传 `--accept-truncation` 时才会给出裁剪结果，并且**永远不写回 `.buf`**——它输出的是"该把 count 改成多少"这份结论。把结论落到 INI 是你的脚本的事。

### 13.3 模式二：同名/同哈希 DDS 自动借用与注入（F1 根因自愈）

当 `bem_triage.py` 报出某个 DDS 缺失时，**"找到了同名文件"不等于"这是同一张贴图"**。同名不同内容的 DDS 在真实包里非常常见（同一角色不同皮肤共用文件名、作者复用旧版资源），盲抄会把错误的法线贴图贴到脸上，而校验器不会报错——它只看字节是否合法。

分两步，中间必须有人（或脚本）做判断：

```bash
# 第一步：找候选。输出分两档：strong（相对路径一致）/ weak（仅文件名一致）
# 结果打到 stdout，按字节大小分组，同名不同大小标 conflict
python scripts/bem_repair.py donor-find "Body_Diffuse.dds" --root ..
```

`donor-find` **不复制任何东西**。

```bash
# 第二步：带着证据复制。必须给 --expect-size 或 --expect-sha256
python scripts/bem_repair.py donor-copy "<donor-find 输出里的某一个候选路径>" \
    -o ./payloads/ --expect-sha256 3f2a...
```

`donor-copy` 的入参是 `donor-find` 列出的**某一个候选**（不是"缺失文件名 + 源目录"两个自由参数），输出目录用 `-o`。没有 `--expect-size` / `--expect-sha256` 时它拒绝（`DONOR_NO_EVIDENCE`），目标已存在时拒绝（`DONOR_WOULD_OVERWRITE`，需显式 `--force`）。这条约束是故意的：**证据等级必须写在命令里**，不能写在注释里。

证据从哪来，按可靠性排序：

| 证据 | 等级 | 说明 |
| --- | --- | --- |
| 源包 `hash=` 与 donor 包 `hash=` 相同 | exact | 最硬。同一 hash 就是同一资源 |
| 相对路径完全一致（含子目录） | computed | `strong` 档的含义。路径是作者的身份声明 |
| 仅文件名一致 + 字节大小一致 | inferred | `weak` 档。可以接受，但要在交付说明里写明 |
| 仅文件名一致，大小不同 | 不可用 | 这是冲突，不是候选 |

四个条件都不满足时，正确结论是**这个槽位没有 donor**，走原生材质槽（丢掉该槽位），不要造一张贴图填进去。

### 13.4 模式三：超大贴图（16K / >256MB）Mip0 自动剥离（规避硬上限）

当单张 DDS 超过 256 MiB 时引擎会拒绝加载。用工具，不要手写：

```bash
# 先看清结构：尺寸、格式、mip 数、array size、是否 cubemap
python scripts/bem_repair.py dds-audit <贴图>.dds

# 剥离 mip0 并重建成新文件（输入不动）
python scripts/bem_repair.py dds-mipdrop <贴图>.dds -o <输出>.dds
```

**为什么不能照抄早期那段 20 行的 `drop_mip0_dds()`**：它写死了 `mip0_bytes = blocks_w * blocks_h * 16`，而每个块的字节数是随格式变的——BC1/BC4 是 8，BC5/BC6H/BC7 是 16，未压缩格式连块都不是。它还只处理了一个 slice：对 `array_size > 1` 或 cubemap（`misc & 0x4`），mip0 在**每个** slice 里都有一份，只切第一份会让文件长度对不上，运行时直接拒收或读到错位数据。

`dds-mipdrop` 做对的几件事：按 DXGI 格式查每块字节数；按 `array_size` 逐 slice 重建；拒绝体积纹理（`DDS_VOLUME_UNSUPPORTED`）；重建后断言长度等于 `header_len + (per_slice - mip_sizes[0]) * array_size`（`DDS_REBUILD_MISMATCH`）；单 mip 时拒绝（`DDS_SINGLE_MIP`）；剥完仍超限时拒绝（`DDS_STILL_OVER_LIMIT`，除非显式 `--allow-still-over`）。

剥掉 mip0 是**降质**：贴图在近景会糊。这必须写进交付说明，不能当作无损修复。

### 13.5 模式四：多状态空间与 Option Groups 自动转译

当 `bem_state_probe.py` 探测出多个按键切换状态时，在 `project.json` 中配置 `option_groups` 与 `component_rules`。

**两个最容易写错的结构细节**（错了只会拿到 `Invalid component rule` / `Invalid condition node` 这种笼统报错，看着像数据问题，其实是格式问题）：

1. `component_rules` 是**每个部件目标一条、按目标顺序排列**，每条形如 `{"target": N, "candidates": [...]}`，**不是** `{"component": N, "rules": [...]}`。校验器要求 `len(component_rules) == len(target.components)` 且 `rule['target'] == 下标`（`bem_v11.py:742-746`）。
2. `when` **不是字符串表达式**。它是结构化节点——`true`、`{"eq": [组, 选项]}`、`{"all": [...]}`、`{"any": [...]}`、`{"not": ...}`——由 `_condition()` 校验（`bem_v11.py:123-142`）。写 `"glasses == 'off'"` 会直接判为不支持的条件。

```json
{
  "option_groups": [
    {
      "id": "glasses",
      "name": "眼镜开关",
      "default": "on",
      "choices": [
        {"id": "on", "name": "佩戴"},
        {"id": "off", "name": "摘下"}
      ]
    }
  ],
  "component_rules": [
    {"target": 0, "candidates": [{"when": true, "operation": "keep"}]},
    {"target": 1, "candidates": [{"when": true, "operation": "keep"}]},
    {"target": 2, "candidates": [{"when": true, "operation": "keep"}]},
    {
      "target": 3,
      "candidates": [
        {"when": {"eq": ["glasses", "off"]}, "operation": "hide"},
        {"when": {"eq": ["glasses", "on"]}, "operation": "replace", "mesh": 0}
      ]
    }
  ]
}
```

注意目标 0–2 的 `keep` 占位：每个部件至少要有一条候选，且**在任意合法组合下恰好有一条成立**（见 `BEM_FORMAT_SPEC` §7）。没被改动的部件不能省略。

写完之后用 `bem_repair.py option-audit work/project.json` 自检：它会把每个合法组合都跑一遍，
指出"哪个状态有 0 条或 2 条候选成立"，以及最坏状态下的 draw 数。注意 **256 是按单个网格判的，
不是各部件求和**（`bem_v11.py:392-394` 对每个被选中的 mesh 单独求 `logic.maximum`），
把各网格 draw 数相加会误报。详见 §13.15。

### 13.6 模式五：BEM 1.5 专有特效与姿态注入

遇到原包的 Shader 特效与高跟悬空，直接写入 BEM 1.5 字段：

```python
# 1. 材质与着色器覆盖 (material_overrides)
draw_entry["material_overrides"] = {
    "floats": {
        "_TransparentDepthWrite": 1.0,
        "_Cutoff": 0.5,
        "_Smoothness": 0.3
    },
    "colors": {
        "_BaseColor": [1.0, 1.0, 1.0, 1.0],
        "_EmissionColor": [0.2, 0.2, 0.2, 1.0]
    },
    "disable_keywords": ["_ANISOTROPY_SPECULAR_ON"],
    "enable_keywords": ["_ALPHAPREMULTIPLY_ON"]
}

# 2. 根骨偏移 (root_offset 修复高跟插地)
manifest["required_capabilities"].append("root-offset-v1")
manifest["root_offset"] = {
    "path": "Bip001",
    "translation": [0.0, 0.04, 0.0]
}
```

### 13.7 模式六：入口 INI 重复段归一化（`ini-audit` / `ini-normalize`）

`component_auto.prepare()` 用 `efmi_source.sections()` 读入口 INI，该函数在**任何**重名段上抛
`duplicate INI section`（`efmi_source.py:20-21`）。这是纯语法门，与重复是否有害无关。

加载器自己对重复段有两条**相反**的规则，搞反了就会改行为：

| 段名类别 | 例子 | 重复时的实际语义 |
| --- | --- | --- |
| 无前缀"全局"段 | `Constants`、`Present`、`Rendering` | 按源顺序**追加**，同一变量后写覆盖前写 |
| 带前缀段 | `TextureOverride*`、`CommandList*`、`Resource*`、`Pool*`、`Key*` | **只取首次出现**，后续副本被解析后丢弃 |

所以"保留第一个"对全局段是错的，"全部合并"对带前缀段也是错的。

```bash
python bem_repair.py ini-audit F:/zmd/伊冯/伊冯-围裙/mod.ini
python bem_repair.py ini-normalize F:/zmd/.../mod.ini -o work/mod.norm.ini
```

`ini-normalize` 在写盘前做两件事，任何一件不成立就拒绝：

1. **投影不变**：把源文本和结果文本各自投影成 `(各段有效语句向量, 键查找表)`，两者必须完全相等；
   带前缀段取首次赋值，全局段取末次赋值。不相等抛 `INI_PROJECTION_CHANGED` 并列出差异段。
2. **幂等**：对结果再跑一遍归一化，必须无变化，否则抛 `INI_NOT_IDEMPOTENT`。

它还会拒绝两种无法凭推断处理的情形：`namespace =`（显式空命名空间会改变全局追加语义）和
缩进段头（`efmi_source.sections()` 的正则要求 `[` 在行首，人类读者却会算作段——两个读者对
"文件里有什么"意见不一致，不猜）。

**实测**（`伊冯-沁夏霓光`，三个活动 INI）：

| 文件 | 源 | 归一化后 |
| --- | --- | --- |
| `065f9a34….ini` | `efmi_source.sections()` 抛 `duplicate INI section: Constants` | 202 段，通过 |
| `ce1deef2….ini` | 5 段，通过 | 5 段，通过（无改动） |
| `f38d483e….ini` | 抛 `duplicate INI section: Constants` | 6 段，通过 |

`065f9a34` 的 `[Constants]×3` 与 `[Present]×3` 均为追加语义，语句数分别是 `[27, 7, 43]` 和
`[1, 9, 54]`——三段都带内容，丢掉任意一段都会改变结果。

### 13.8 模式七：条件贴图覆盖收敛为静态选择（`texture-normalize`）

`component_auto.texture_table` 只接受一种形状：

```ini
[TextureOverride_X]
hash = <hex>
if $object_detected
this = ResourceY
endif
```

源包常写成 `if $swapvar_body==0 / this = ResourceA / else / this = ResourceB / endif`。这**不是缺陷**，
是状态选择。`texture-normalize` 按给定状态解析分支、改写成上面的形状，再走一遍自己的输出证明
选中的资源没变（变了抛 `TEXTURE_SELECTION_CHANGED`）。

```bash
python bem_repair.py texture-normalize work/mod.norm.ini -o work/mod.tex.ini
python bem_repair.py texture-normalize in.ini -o out.ini --set '$swapvar_body=1'
```

环境取值的顺序是有讲究的，写反会静默丢掉全部覆盖：`Constants` 声明 → 内置假设
（`$object_detected=1`、`$mod_enabled=1`、`$lod_level=0`、`$lod_detected=0`）→ `--set`。
`Constants` 里声明的是**检测前**的值（常见 `$object_detected = 0`），而转换态就是"检测到、模组启用"，
所以假设必须覆盖声明。实测 `伊冯-围裙`：顺序写反时 21 条覆盖全部被丢，改正后 21 条全部选中。

逐 draw 的 `ps-tN =` 覆盖不动、原样保留并记入 `left_untouched`——收敛它等于改变某个 draw 绑哪张图。

还有两个会让"条件解不出来"的坑，都不是包的错：

- **变量名大小写**。3Dmigoto 的变量名不区分大小写，`Constants` 里写 `$SocksColor`、条件里写
  `$sockscolor` 是同一个变量。用精确匹配的环境查表会把每条这类条件都判成 unknown。实测
  `伊冯-沁夏霓光`：改正前 5 条贴图段被拒，改正后 22 条全部选中。
- **倒卖水印**。同上，用 `bem_merge` 的规则在结构分析前剔除，否则横幅行会被当成"不支持的语句"。

### 13.9 模式八：入口程序降级与模板代差（`entry-normalize`）

`component_auto.validate_entry` 只接受一种分发程序（`component_auto.py:106-111`）。失败时先分清是
哪一种，三种处理完全不同：

| 现象 | 判断 | 处理 |
| --- | --- | --- |
| 旧式内联 `if $mod_enabled && DRAW_TYPE == 4 / handling = skip / run = CommandListX / endif` | 老模板 | **可降级**：改写成固定 LOD0 回调 + 合成分发器 + 声明 `$component_count` |
| 分发器**缺**模板里的语句 | 包比模板老 | **可升级**：整段替换成规范九行，报告补了哪几条 |
| 分发器**多**出模板不认识的语句 | 包比转换器**新** | **拒绝** `ENTRY_TEMPLATE_STALE`，列出多余语句；该改的是转换器模板，不是包 |
| 组件声明 `$\EFMIv1\gpu_posed = 0` | 该 draw 留在原骨架 | **拒绝** `ENTRY_CPU_POSED_COMPONENT`，BEM 无法重新指向它 |

第三种是本轮实测里最重要的一条。`伊冯-围裙` 是复杂集里**判为成功**的包，它的 11 个入口里有 10 个
被 `ENTRY_PROGRAM: 分发流程超出已支持的 ComponentN 模板` 卡住，而真实差异只有 4 条**新增**语句：

```
$\EFMIv1\bones_count = $bones_count
$\EFMIv1\instance_count = $max_instance_count
$\EFMIv1\custom_mesh_scale = 1.00
CommandList\EFMIv1\Callback_MergedSkeleton_ConnectComponent = ref CommandList_MergedSkeleton_ConnectComponent
```

把这 4 条删掉"修好"这个包，会同时删掉合并骨架连线、实例计数和自定义网格缩放。所以正确结论是
**转换器模板落后于 EFMIv1 运行时**，需要补模板，而不是改包。第 11 个入口（`Component10`）声明
`gpu_posed = 0`，属于另一类，同样不能改。

反向的例子是 `伊冯-沁夏霓光`：10 个入口全是旧式内联（`handling = skip` 在守卫内），降级后 10/10
通过 `validate_entry`，分发器与 `$component_count = 10` 自动补齐，其中 4 个入口原本只抑制不绘制，
按 `standard_worker.normalize_entries` 的同一约定合成了空的 `CommandListBEMReviewedHideN` 回调。

两个容易踩的细节：

- 倒卖水印（`【防倒卖：…】_702728`）会被塞在 `endif` 之后。它不是语句，用 `bem_merge` 的同一条
  规则（无 `=` 且有方括号 CJK 横幅）剔除后再做结构分析；否则 10 个入口会误判 2 个。
- 降级后的文件会被重新解析，并对每个入口再跑一次真实的 `component_auto.validate_entry`；任何
  一个不过就抛 `ENTRY_STILL_INVALID`，绝不写出未验证的结果。这条检查需要 `component_auto`，
  所以在发行包里该子命令会拒绝（`ENTRY_TEMPLATE_UNAVAILABLE`），其余子命令不受影响。

### 13.10 模式九：蒙皮调色板与顶点压缩（`skin-audit` / `skin-fix` / `vertex-compact`）

这三条只对**已解包工程**（`project.json` + `payloads/`）生效。打包后的 `.bem` 里 payload 是压缩的，
"改一个顶点"等于重写整个容器。

规则来自校验器，不来自口味：调色板索引是 UINT8、至多 256 项；每个带权顶点的权重和是 65535
（UNORM16）或 1.0（float）；索引**只有在该槽权重为 0 时**才允许越出调色板。

```bash
python bem_repair.py skin-audit work/project.json
python bem_repair.py skin-fix  work/project.json -o work/fixed
python bem_repair.py vertex-compact work/project.json --mesh 0 -o work/compact
```

`skin-fix` 只把**零权重**槽上的越界索引清零；带权重的越界一律拒绝
（`SKIN_BONE_OUT_OF_PALETTE`），因为清零会让那块几何掉到根骨上。`--zero-weight-policy first` 才会
处理"四个权重全 0"的顶点（给它整份权重压到骨骼 0），这是可见改动，必须显式开启。没有任何顶点
需要改时不写文件，返回 `written: false`——复制几十兆去证明"没事可做"不是修复。

`vertex-compact` 有三条约束，缺一条就会做出更糟的包：

1. **共享缓冲必须整组压缩**。同一份顶点缓冲被多个 mesh 读时，只压一个会让旧缓冲必须留下，
   包反而变大。实测 `洁尔佩塔-侦探`：mesh 0 单独压得到 66477/203284，看着省了 2/3；但 mesh 4
   共用同一份流，整组压完是 203284/203284——一个顶点都不多余。工具因此按组判断并拒绝
   （`VERTEX_COMPACT_NOOP`），而不是交出一个更大的包。
2. **写前逐行校验**。每条 draw 的每个索引，新缓冲对应行的字节必须和旧缓冲完全相同；这是比
   "三角形数没变"强得多的证明，顶点位置、权重、骨骼任一变化都会被抓到。
3. **清理孤儿载荷**。压缩会新增一份顶点缓冲，旧的不清掉包只会变大。`prune_payloads` 遍历整个清单
   收集 `payload` / `indices` / `preview` 引用（包括动画帧、音频、预览图），重编号后再写。

### 13.11 模式十：合并骨架蒙皮改写（`skin-convert`）

EFMI 里一旦出现 `Callback_MergedSkeleton_ConnectComponent`，蒙皮流写的就是**全局骨骼 id**，
而 BEM 的 mesh 只能引用自己的调色板。两种源布局，都必须先转成网格本地调色板：

| 源 stride | 形态 | 内容 |
| --- | --- | --- |
| 8 | 刚性 | 4 × UINT16 全局骨骼 id |
| 16 | 带权 | 4 × UNORM16 权重（偏移 0）+ 4 × UINT16 全局 id（偏移 8） |

目标布局只有两种，和原生网格一致：stride 12（2×U16 权重 + U8×4 索引）、stride 32
（4×F32 权重 + U32×4 索引）。

```bash
python bem_repair.py skin-convert work/project.json --mesh 0 -o work/native
python bem_repair.py skin-convert work/project.json --mesh 0 --target-stride 32 -o work/native
python bem_repair.py skin-convert work/project.json --mesh 0 --palette palette.json -o work/native
```

调色板**按首次使用顺序**生成（不是按骨骼表顺序），写前逐顶点核对
`(权重, 骨骼名)` 二元组，写后重读校验。五个拒绝码：`SKIN_MERGED_PALETTE_TOO_LARGE`
（>256 根骨骼——这时只能拆网格）、`SKIN_BONE_OUT_OF_TABLE`、`SKIN_ALREADY_NATIVE`、
`SKIN_NO_BONE_TABLE`（既没给 `--palette`，目标部件也没有骨骼表）、`SKIN_LAYOUT_UNKNOWN`。
多个 mesh 共用同一份蒙皮缓冲时整组只重写一次，和 `vertex-compact` 同一套 `_stream_group` 逻辑。

### 13.12 模式十一：贴图覆盖分类与重复身份（`texture-audit`）

`component_auto.texture_table` 只认 `hash =` + `if $object_detected` + `this = Resource`。
`texture-audit` 把每个 `TextureOverride*` 段分成四类，并**复现自动路径会抛出的那条错误**：

| kind | 含义 | 自动路径的结论 |
| --- | --- | --- |
| `static` | 上述标准形状 | 可转换 |
| `per_draw` | `ps-tN = ResourceX` 逐 draw 绑槽 | `TEXTURE_STATE: 不支持的贴图语句` |
| `empty` | 只有 hash，没有语句 | `TEXTURE_STATE: 不完整的贴图覆盖` |
| `unsupported` | 其它语句（如 `$active = 1` 标记段） | `TEXTURE_STATE: 不支持的贴图语句` |

只有**重复 hash 是真阻断**（`blockers`）：两个段抢同一个身份，源本身对"哪张贴图生效"是歧义的，
重写解决不了，必须问人。其余进 `manual_review`，应当原样带过去而不是丢掉。
`ps-tN = null` 单独收进 `cleared_slots`——BEM 1.5 用 `texture_slots` 表达"这个槽什么都不绑"，
和"没写这个槽"不是一回事。

实测 `伊冯-围裙`：11 个入口、21 个静态覆盖、0 重复；`诀-庆典`（M0488）两个 `TextureOverride.Active.Mark`
段带 `$active = 1`，进 `manual_review` 但不阻断。

### 13.13 模式十二：角色资料缺口定位（`catalog-gap`）

`component_auto.match_catalog` 要求**所有** `ComponentN` 身份同时出现在**同一个**角色的
`catalog/*.json` 里，缺一个就整包拒绝，而且错误信息不说是哪一个。`catalog-gap` 回答这个问题：

```bash
python bem_repair.py catalog-gap work/mod.ini
python bem_repair.py catalog-gap work/mod.ini --catalog-dir <tool root>/catalog
```

`verdict` 有五种，处理方式完全不同：

- `matched`：恰好一个资料全覆盖 → 可以自动转换。
- `ambiguous-profile`：多个资料都覆盖 → `match_catalog` 会以"匹配不唯一"拒绝，先查资料是否重复。
- `mixed-characters`：缺口身份**在别的角色资料里** → 这个包混用了多个角色的部件，任何单一资料都
  满足不了，需要拆包或手工映射。`identities_present_elsewhere` 直接给出归属。
- `unknown-identity`：缺口身份**谁都不认识** → 缺的是"原资源 ↔ EFMI hash"的对应本身，先补资料。
- `partial`：两者都有。

它同时识别四种入口族，这是判断"能不能转换"的前提：

| 入口族 | 形态 | 有读取器 |
| --- | --- | --- |
| `component-n` | `TextureOverride_(EntryPoint_)?ComponentN` | 是（`component_auto.BASE`） |
| `hash-lod` | `TextureOverride_(EntryPoint_)?LODn.<hash>_<count>_<first>` | 是（`efmi_lod_source.ENTRY`） |
| `bare-hash` | `TextureOverride_<hash>_<count>_<first>` | **否** |
| `ib-component` | `TextureOverride_IB_<hash>_<label>_ComponentN` | **否**（只有 `bem_triage` 认它是 CrossIB 重定向） |

后两种会被报成 `ENTRY_FAMILY` 阻断：身份齐全也降低不了。

**一个容易搞错的地方**：`ib-component` 段名里的 8 位十六进制**不是**绘制身份，而是作者导出时
给顶点/索引缓冲的资源组标签；真正的身份是段体里的 `hash =`。实测
`TextureOverride_IB_b21b22ac_衣服_Component1` 的段体是 `hash = 586b4574`——按段名取身份会得到
一个谁都不认识的假 hash，还会被误判成"段名与段体不一致"。工具因此把段名那一段单独记在
`resource_tag` 里。同理，`ib-component` 的 `ComponentN` 会按部件重复（cloth/body/skirt 各一个
`Component1`），所以不能像 `component-n` 那样按组件下标查重。

以 hash 命名的入口（`bare-hash`）自带 index count，降低流程根本不查角色资料，所以那种情况
`catalog_required: false`，缺口只是参考信息。

实测复杂集 15 个包：3 个 `matched`、10 个 `unknown-identity`、2 个 `not-required`（入口族没有
读取器，但也不查资料）。也就是说本语料里"资料缺身份"占 2/3，不是偶发。

### 13.14 模式十三：形态参数与增量载荷（`deformation-audit`）

`bem_v13.validate_manifest` 遇错即抛，一个坏帧只会报一条。`deformation-audit` 把规则走完再汇总，
并且**真的解一遍增量载荷**：

```bash
python bem_repair.py deformation-audit work/project.json --minor 3
```

它核对的是规格第 10 节那些不靠读代码看不出来的点：

- 帧值从 `min` 起按 `step` 对齐、严格递增、必须**同时覆盖** `min` 和 `max`；
- 恰好一个 `{"value":neutral,"neutral":true}` 中性帧，且它不带 payload；
- 非中性帧字段恰好是 `value/payload/count/encoding`，`count` 可以为 0 但不超过顶点数；
- payload 长度**恰好** `4 + J + 16N`，`J` 在 1..4096，头部 JSON 是
  `{"count":N,"encoding":"sparse-position-f32"}`，索引在范围内且同帧唯一，增量是有限值；
- 形变网格必须有 Float32 XYZ 位置属性（semantic 0、format 0、dimension 3）。

`DEFORM_PAYLOAD_SIZE` 与 `DEFORM_PAYLOAD_HEADER` 经常一起出现：帧上写了 `count=3`、载荷里只有 2 条
记录时，两个都不对——这正是手工改过帧表的典型痕迹。

### 13.15 模式十四：选项空间枚举与 256 上限（`option-audit`）

`bem_v11.selection_plan` 用符号决策图证明"每个部件恰好一个候选"，需要构建器模块（因而需要
zstandard）。`option-audit` 改成**枚举**合法组合，好处是能说出**是哪个状态**坏了：

```bash
python bem_repair.py option-audit work/project.json
```

- 枚举时 `available_when` 不可用的组**没有取值**，针对它的 `eq` 一律为假——所以
  "组隐藏"本身也是一个状态，不能只按笛卡尔积算。
- `selection_constraints` 全部成立才算合法组合；默认组合也必须合法（`OPTION_DEFAULT_ILLEGAL`）。
- 每个状态下逐条规则求值：0 个候选 → `OPTION_RULE_UNCOVERED`，≥2 个 → `OPTION_RULE_AMBIGUOUS`，
  两者都带第一个违规状态。
- **256 是按单个网格判的，不是各部件求和**。`bem_v11` 对每个被选中的 mesh 单独做
  `logic.maximum(active, ...) <= 256`。两个各 200 draw 的网格永不同时出现，就不算超限；
  把它们的 draw 数相加会误报。
- 组合数超过 250000 时置 `truncated: true` 并在 `note` 里说明，不假装穷尽。

### 13.16 模式十五：CrossIB 几何分组（`ib-groups`）

CrossIB 的源侧审计做不了——重定向 shader 一出现，draw 的区间就不再对 `ib = ref X` 所声明的缓冲区
成立（`bem_triage` 因此主动跳过，见 `CROSS_IB_SECTION_SKIPPED`）。转成 BEM 之后这个问题重新变得可回答：
按网格**实际索引的字节**做指纹分组。

```bash
python bem_repair.py ib-groups work/project.json
```

- `shared-topology`（同一份索引字节、顶点不同）就是 CrossIB 的正常形态：一个拓扑、多套蒙皮。
- `exact_duplicates` 指出逐字节相同的网格；打包器本来就会去重载荷，所以这只是信息。
- `same_count_variants`（长度相同、字节不同）是拓扑变体，不是缺陷。
- `index_size_conflicts` 是唯一的真错误：同一份索引缓冲不可能同时按两种宽度读。

### 13.17 模式十六：形态键转滑条（`shape-params`）

源包的 ShapeKey 数据不是"一个姿势"，而是"一整条可调范围"。缓冲区 `ShapeKeyVertexOffsets` 存的是
**权重 1 的完整顶点偏移**，Unity 混合按权重线性叠加；`[Constants]` 里那个值只是源作者选的**起始点**。
所以正确做法是输出两帧覆盖 0..1000，而不是把源默认值烘进顶点后当静态模型交付。

```bash
python bem_repair.py shape-params "mod/伊冯" work/project.json --output work/body-morphs.json
```

产出的就是 `deformation-audit` 和构建器本来就要的 `kind: bem-position-morphs` 配置：

```json
{"schema":1,"kind":"bem-position-morphs",
 "parameters":[{"id":"shape_0_0","name":"Component 0 shape 0","min":0,"max":1000,
                "neutral":0,"default":500,"step":1}],
 "mesh_deformations":[{"mesh":0,"parameter":"shape_0_0","frames":[
    {"value":0,"neutral":true},
    {"value":1000,"efmi":{"source":"../../mod/伊冯","ini":"main.ini","component":0,
                          "shape_key":0,"vertex_order":"exported"}}]}]}
```

- 帧只写 `efmi` 绑定，不写死偏移：真正的解码由 `efmi_shapes.decode_binding` 在构建时做，偏移永远
  跟着源缓冲走，不会因为手工抄写而漂移。
- `default` 取源常量，滑条初始位置与源一致；`neutral` 固定 0。
- 部件 → 网格的映射走**默认外观**：`appearance.components[].target` 对应 `target.components[].id`，
  `.mesh` 就是目标网格下标。默认外观没替换的部件会被跳过并列在 `skipped`。
- 拒绝/阻断码：`SHAPE_DATA_ABSENT`（既没有资源声明也没有 setter 调用）、
  `SHAPE_NOT_DRIVEN`（缓冲区声明并初始化了，但**没有任何调用方真正执行 setter**，源里滑条是死的、
  权重恒 0，没有默认值可转 —— M0265 提弗洛斯-旗袍 就是这种：`[Constants]` 里只 `run =
  CommandListInitializeShapeKeys`，`$shapekey_id`/`$shapekey_value` 全是 0，`CommandListSetShapeKey`
  只在注释里出现）、`SHAPE_SOURCE_ARCHIVE`（rar/7z 需外部后端，先解压）、`SHAPE_RESOURCE_MISSING`、
  `SHAPE_VERTEX_MAP_REQUIRED`（源顶点号超出导出网格，必须显式 `vertex_map`）、
  `SHAPE_KEY_OUT_OF_RANGE`（键号超出 `batch*127`）、`SHAPE_POSITION_ATTRIBUTE`（缺 Float32 XYZ
  位置属性）、`SHAPE_NO_DRIVEN_KEY`（该部件全部权重为 0）。
- 写完配置后仍要走 `deformation-audit` 复核，再交给构建器：`shape-params` 只负责"该有哪些参数、
  绑哪个源"，不代替格式校验。

### 13.18 自我修改与调试闭环（Self-Correction Loop）

Agent 编写专有脚本后，必须在工作区执行并校验，绝不把半成品交给用户：
1. 运行专有脚本：`python workspace/build_mod.py`
2. 运行双端校验：`python tools/CustomModel/bem_tool.py validate output.bem --report report.json`
3. 检查校验报告：
   - 若 `success == true`：校验通过，进入交付流程。
   - 若校验失败：读取 `issues` 错误码（如 `UNBOUND_MATERIAL`、`BONE_PALETTE_OVERFLOW`），Agent 自行修改 `build_mod.py` 调整绑定，再次运行并校验，直至通过。

### 13.19 面向小白的通俗交付清单规范

完成制作后，Agent 需生成一份通俗易懂的说明，禁止充斥十六进制 hash，模板如下：

```markdown
### 转换交付完成

- **产物文件**：`F:\...\package.bem`（已通过离线数据校验，兼容当前游戏版本）
- **造型与切换说明**：
  - [默认外观]：包含完整基础服装与发型。
  - [部件开关]：已适配按键切换（如：按键切换外套穿脱、发型长短）。
- **自动修复与优化**：
  - [贴图补全]：原包漏带的 1 张装饰贴图，已自动从同角色资源库借用补齐。
  - [模型修整]：原模型末尾存在 6 个异常超出边界的索引点，已自动平滑截断，无破面。
  - [姿态优化]：已为高跟鞋添加 0.04 米站姿抬升微调，防止游戏中鞋跟陷入地面。
  - [安全过滤]：已自动安全过滤原作者自带的外部 2D 鼠标悬浮菜单脚本。
```


---

## 附 A：源侧框架参考

源 INI 里调用的东西不一定定义在源包里。四种来源要分开看，处理方式完全不同：

| 来源 | 识别特征 | 怎么读 | 结论 |
| --- | --- | --- | --- |
| **源包内部** | `[CommandList...]` 在本包内有定义 | 当普通命令列表读 | 能读到底，`bem_merge.py` 会算出来 |
| **loader 自带库** | `CommandList\EFMIv1\*`、`Resource\EFMIv1\*` | 读 `references/efmi/core/` 里的原件 | 语义确定，可按定义执行静态求值 |
| **第三方框架，本体可公开取得** | 包内无定义，框架有独立发布页 | 按附 A.3 的接口事实读；本体不随包分发 | 槽位与委托 draw 可展开；材质语义仍逐包核 |
| **哪里都取得不到** | 包内无定义，也没有可查的来源 | 不可知 | 逐 draw 取值读不出，必须如实披露 |

`references/efmi/` 是 EFMI 加载器随包装配的那套库（`namespace = EFMIv1`）与官方模板的
**未改动副本**，用于离线查证"这条语句到底做什么"。它是参考材料，不参与构建，也不会被加载。

### A.1 这份参考里有什么确定语义

| 想知道的事 | 查哪个文件 |
| --- | --- |
| 贴图覆盖是怎么触发的 | `core/API.ini`（`OverrideTextures` 与逐资源身份检查） |
| 实例怎么被逐个识别与配置 | `core/SpatialIdentification.ini`、`shaders/MergedSkeleton_InstanceConfigOverrider_*` |
| 骨骼实例的合并与偏移 | `core/MergedSkeleton.ini`、`shaders/MergedSkeleton_BoneData*` |
| 形态键的值怎么变成顶点位移 | `core/ShapeKeys.ini`、`shaders/ShapeKeyDequantizer.hlsl` |
| 一份标准源 INI 长什么样 | `mod.ini.j2`、`per_component.ini.j2` |

**读它的方式**：先看段名，再看每条命令改的是哪个槽/缓冲，最后看 `run` 链落到哪个 draw。
不要假设"名字像就是做这个"——`OverrideTextures` 这个名字听起来像在做材质，
实际它只是按资源身份触发一堆检查，本身不含任何材质定义。

### A.2 三种静态读不出结果的源行为

这三类在 BEM 里没有对应表达，且**源侧再完整也无法自动完成**：

1. **按实例分解**——同一个网格被逐实例处理，每个实例有自己的配置。
   BEM 的 draw 是整段索引流，没有"按实例"这一层。
2. **按实例的空间身份判定**——源侧靠实例在空间中的位置区分"玩家角色"与"路人"，
   只处理判定为玩家的那个。BEM 里没有运行期判定，也没有"这个实例是不是玩家"的输入。
3. **运行期探测的实例配置缓冲**——槽位号是逐个试出来的，不是固定值。

遇到这三类，结论是记录并停止（见 §12），不是"想办法模拟"。硬模拟出来的结果在
其他场景下会错，而且错得很安静。

### A.3 第三方框架：以 RabbitFX 为例

`RabbitFX` 是 caverabbit 的第三方框架，按 loader 逐个发行，**EFMI 版对应终末地**：
<https://gamebanana.com/mods/651557>。源包里通常只有"调用"，本体装在游戏 mod 目录，
所有包共用一份。

**不随包分发是许可问题，不是体积问题。** 本体合计约 47 KB（`RabbitFX.ini` 43 KB + 4 个 HLSL 2.3 KB + `deltaTime.ini` 0.5 KB），
带着完全不影响包体；但发布页标注 **CC BY-NC-ND 4.0**——ND 禁止分发演绎版本，NC 禁止商业用途，
而本项目是 AGPL-3.0，AGPL §10 不允许对作品附加额外限制。两者不能共存，所以这里给链接与事实，
不给文件。需要核对原文时按链接取，**把取到的版本记进转换记录**（接口会随版本变）。

以下每一条都影响"源里这句话在说什么"，属于事实而非代码。

**① 槽位名就是属性声明，不是约定俗成。** 框架本体第一行是 `namespace = RabbitFX`，
所以 `[ResourceDiffuse]`、`[ResourceGlowMap]` 这些空占位段，在引擎里的实际名字是
`Resource\RabbitFX\Diffuse`、`Resource\RabbitFX\GlowMap`。调用方在源包里写：

```
Resource\RabbitFX\Diffuse = ref Resource_Texture16
```

填的就是那个占位槽。**要判断"这张图是什么"，读槽位名即可**，不必猜：

| 槽位名 | 语义 | 寄存器 | 绑定所在 |
| --- | --- | --- | --- |
| `Diffuse` | 基础色 | `ps-t70` | `CommandList\RabbitFX\SetTextures` |
| `Lightmap` | 光照图 | `ps-t71` | 同上 |
| `Normalmap` | 法线 | `ps-t72` | 同上 |
| `DiscardMap` | 可丢弃遮罩 | `ps-t73` | 同上 |
| `Rainmap` | 雨淋遮罩 | `ps-t74` | 同上 |
| `GlowMap` | 自发光 | `ps-t60` | `CommandList\RabbitFX\Run` |
| `FXMap` | 特效遮罩 | `ps-t61` | 同上 |

两处的守卫都是 `ps == $RabbitFXMain || ps == $RabbitFXShadow` 且 `DRAW_TYPE == 4`。
`$RabbitFXMain = 1718.1`、`$RabbitFXShadow = 1718.2`、`$RabbitFXShadowFace = 1718.3`：
**像素着色器版本号就是通道开关**。

**② 委托 draw 的参数在源包里，动作在框架里，两半合起来才是完整绘制。**

```
源包：  $\RabbitFX\MeshStartIndex = 9762
        $\RabbitFX\MeshDrawAmount = 8256
        run = CommandList\RabbitFX\DrawMesh
框架：  if $MeshStartIndex >= 0 && $MeshDrawAmount > 0
            if vs == 1718.1
                drawindexedinstanced = $MeshDrawAmount, INSTANCE_COUNT, $MeshStartIndex, 0, FIRST_INSTANCE
```

即 `first = MeshStartIndex`、`count = MeshDrawAmount`；`vs == 1718.1` 是主体，`1718.2` 是阴影
（`DrawShadow` 段）。两边写法不同但指的是同一个变量：源包写 `$\RabbitFX\MeshStartIndex`，
框架里写 `$MeshStartIndex`，因为框架自己的 `namespace` 会补上前缀（变量跟段名同一套规则）。
框架执行完把两个参数置回 `-1`，**所以一次赋值只对紧随其后的那一次 draw 有效**。
看到 `DrawMesh` 就判"draw 不可知"是错的：它可展开，只是两边都要读。真正不可知的是**只有调用、
连本体都拿不到**的情形。

**③ 骨架缓冲是往返借用，不是新骨骼。** 调用方 `Resource\RabbitFX\T0 = ref ResourceT0` 交出缓冲，
框架写完骨骼，调用方 `ResourceT0 = copy Resource\RabbitFX\T0` 收回；`$componentOffset`、
`$boneOffset`、`$totalBones` 是调用方告知的布局参数。转换时只需要读回最终缓冲内容，
不需要复刻框架动作——复刻反而会把布局参数硬编码成猜测值。

**④ 它会和 `ShaderOverride` / `ShaderRegex` 互相顶掉。** 本体主体是 17 组 `ShaderRegex`，
直接替换游戏的阴影与发光着色器。同机别的包用 `ShaderOverride`/`ShaderRegex` 打同一个 pass 时，
两者争同一着色器，表现为用骨架系统的那个包整件不可见。这是源的装机环境问题，对转换只有一条结论：
本包不能自动处理，按 §12 记录。

**⑤ 框架的值不是颜色，是常量缓冲。** `Run` 把 `$h/$s/$v/$brightness/$interpolate` 写成
217 号常量缓冲的 `x/y/z/w` 分量（HSV 加插值），再叠加 172 号上的开关位。这些是运行期参数，
BEM 侧只能映射成参数滑条，不能当贴图，也不能当固定颜色。

**⑥ 只认一种拼写会漏。** 语料里同一命名空间存在 `RabbitFX`、`RabbitFx` 两种写法，
`Commandlist` 也有小写变体。匹配一律大小写无关。

**怎么用这套事实**：判断某张图是不是基础色 → 读槽位名（①）；算某个 draw 的索引范围 →
把源包参数与 ② 的公式合起来；判断某段是不是骨架扩展 → 看有没有 `T0`/`CB1` 的往返与
`componentOffset` 赋值（③）。三件事都不需要拿到框架本体。

---

## 附 B：一页流程卡

```
-1 算有效段（所有后续步骤的前提）
   bem_merge.py <包>
   同名段取首条、include 展开、命名空间改写、去水印行
   列出被丢弃的段及原因；不先算这步，后面全在重复计数
⓪ 机械体检（5 分钟，省几小时）
   bem_triage.py <包>
   缺失资源分三级：Draw 路径引用=阻断 / 框架通道=警告 / 无人引用=无害
   索引越界：报 first+count vs 容量；CrossIB section 跳过
   入口族：ComponentN / HASH_N_N / LODN.HASH_N_N；TextureN 不是入口
① 读入口
   外层目录 → 入口 INI → 可达状态清单（含持久键初值）
   bem_state_probe.py <包>  查每个状态画什么、用哪张图
   框架委托 draw（DrawMesh 之类）：源包参数 + 框架动作，两半合起来算（附 A.3）
② 定身份
   catalog/别名库 → 字节比对 → 几何比对 → 描述符重算 → 跨包复用
   bem_peer.py --target <包>   缺文件时找同字节 donor（候选≠同一性证明）
   每条落 L1/L2/L3，L4 不用
③ 核完整性
   IB/VB 四条判据 → 可修/不可修
   源自身错误（笔误/分段数/漏声明）单独识别
④ 对齐结构
   一对一 / 一对多（按三角面分类）/ 多对一（双子网格）
   合并网格拆分 + 权重逐块核
⑤ 映射骨骼
   调色板 ≤120 → 组偏移 → 骨名 → 带权引用总数校验
   跨代断位单独处理（自顶向下重映射）
⑥ 路由材质
   先查目标材质自己的表 → donor 整份替换的代价 → 按 draw 绑定
   keep 组件查消费者 → 槽位残留压制 → ps-t 逐包推断
⑦ 装状态
   算准可达状态 → 能折叠的折叠（要有比对证据）→ 条件绘制
⑧ 划剔除
   判定树过一遍；动画按"值不值"判，不按"能不能"判
⑨ 写包 + 校验 + 披露
   临时文件 → 读回 → 逐字节相等 → 原子替换 → 再读回
   双端离线 + 每个属性凭什么 + 四个必需字段
   登记完成时重新读包复算外观数，不信报告
   bem_audit.py 周期性核对账本/报告/实物
```

**最后一句**：这份文档里没有一条规则能替代"去查消费者"。所有判断最终都回到同一个问题——**这个数据，被哪个 draw、哪个材质槽、哪个骨骼真正用到了**。答得出，就能做；答不出，就该记失败，而不是靠相似度蒙一个。

---

## 附 C：工具链

六个脚本，在 `scripts/` 下，纯标准库，只读，不写源目录。`python scripts/<名字>.py <包目录>`。

| 脚本 | 用在哪一步 | 给什么 |
| --- | --- | --- |
| `bem_merge.py` | ⓪ 之前 | 按 loader 规则算出**最终生效的段**（同名段首条生效、include 展开、文件自带的 `namespace =` 改写、去水印行），并列出哪些段被丢弃及原因。第三方框架的槽位名就是靠这一步还原成 `Resource\RabbitFX\Diffuse` 这种可读形式 |
| `bem_triage.py` | ⓪ 体检 | 入口族、缺失资源三级分类（框架通道再按「名字身份 → 槽位名 → 文件名角色词」定级，并标出用的是哪一种）、索引越界、输出通道污染、框架定义是否在包内、判决与阻断点 |
| `bem_state_probe.py` | ① 读入口 | 键域、可达状态、每状态选中的贴图、零索引绘制、风险 |
| `bem_peer.py` | ② 缺文件时 | 按需在兄弟包里找同字节的 donor；名字对但字节不同的单列成冲突 |
| `bem_audit.py` | ⑨ 之后 | 账本/报告/实物三方不一致清单 |
| `bem_ini.py` | 被上面调用 | INI 解析与条件求值；未定义标识符返回 `None`，永不返回 0 |

用法上只有两条硬规则：

- **先跑 `bem_merge` 再跑别的**。后面三个都从它取有效段；不先算有效段，同名段会被重复计入。
- **`bem_peer` 的候选不是同一性证明**。它只回答"这个字节串在别的包里也存在吗"，换不换由身份锚定决定。

六者都遵循同一条原则：**报事实，不替人决定**。越界报容量与需求，修不修看四判据；
身份报零命中，不按序号兜底；缺失报引用位置，阻断还是警告由引用通道决定。

