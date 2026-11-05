# 模型兼容修复层方案

状态：阶段 0、1 已实施（2026-11-05），阶段 2 未开始。契约与实施记录见正文、第 12 节与第 13 节。
日期：2026-11-05。工作位置：`F:\Better Endfield`。

## 1. 目标与边界

官方每次重打包都可能让"已经发出去的 BEM 包"在新客户端上对不上：网格改名、网格被去重、索引数变化、骨骼重排。现在的做法是每遇到一个就在 `SelectCompatibleModelDonor` 里加一个特例，特例按平台用 `#if` 切开。目标是把这个位置正式化成**可注册的修复记录**，让第 N 个角色的接入成本从"改核心文件 + 加平台分支"降到"加一条记录"。

三条边界必须先说清：

1. **修复不是放宽。** 修复把一个**确定的期望值 A** 改写成**另一个确定值 B**，匹配仍然走 `SelectUnique` 的全等比较，命中数不等于 1 仍然 `Ambiguous`。不存在"接受候选集"。
2. **修复不减少对应项。** 进来 46 个组件，出去还是 46 个。这条由上层 BEM 匹配保证（`module.cpp:4430`），修复层不参与也不破坏它——它没有权力"跳过"任何组件。
3. **修复不自动推断。** 只处理已经登记、已经被人工确认过的变更。不按名字前缀、相似度、哈希距离去猜。

## 2. 现状：两个特例的真实形态

`SelectCompatibleModelDonor`（`native/modules/custom_model/module.cpp:4355`）是全部兼容逻辑的唯一入口，现在长这样：

```
#if defined(_WIN32)
    ... 昼雪 aurora legacy：改写 indices + 钉死 receiver_path + 二次 SelectUnique + 87 骨骼硬校验
#elif defined(__ANDROID__)
    ... sibling LOD：遍历候选，按 SiblingLodPathMatches 直接挑一个
#else
#endif
```

| 特例 | 触发条件 | 修复动作 | 校验 | 绑定期 |
| --- | --- | --- | --- | --- |
| 昼雪 `chr_0014_aurora` | 正常匹配 `Missing` 且 `IsLegacyTarget` 成立 | `indices` 87120→87288、钉死 `Mesh_all/lod0/S_actor_aurora_cloth_01_lod0` | 材质名 + 87 骨骼名/路径 + 87×16 bindpose ≤1e-5 | `RemapBone` 置换 36/37、64/65 |
| 安卓 sibling LOD | `Missing` 且 `explicit_resource` | 不改请求，按路径挑同级 LOD 候选 | 仅 `candidate_valid` | 无 |

这两个已经是同一条流程（失败 → 产出新期望 → 重选 → 校验），只是被平台 `#if` 切开、各自写死。第 3 个角色只能再切一个分支，这是要做这一层的直接原因。

**但 `SelectCompatibleModelDonor` 不是唯一的入口。** 安卓还有一条完全独立的 legacy 路径 `HasCompatibleAndroidLegacyWorld`（`model_job_runtime.inc:877`），它自己调两次 `SelectUnique`，两次都不经过这个函数。只在一个地方挂修复，安卓的 world 侧不会有任何效果。详见 5.4——这是核对代码后对原方案最重要的修正。

## 3. 设计：修复记录（Repair Record）

### 3.1 一条记录描述什么

一条记录是**一次已确认的期望改写**，由四段组成：

| 段 | 作用 | 是否参与匹配 |
| --- | --- | --- |
| `expected` | 包声明的是什么（必须当前请求精确等于它才触发） | 是，且必须全等 |
| `repaired` | 客户端现在实际是什么（改写后的单值） | 是，改写后仍全等 |
| `proof` | 凭什么认为 A 和 B 是同一个东西 | 运行时实测，不信数据 |
| `bind` | 绑定期怎么处理（共用网格、骨骼映射） | 否 |

### 3.2 数据记录的形态（默认实现，不是强制契约）

下面这套字段是**最省事的一种写法**：当一类问题能纯粹用数据描述时，填几行就完事，不用写代码。层本身不要求任何修复必须长成这样——见 3.4 的第四类。

```json
{
  "schema": 1,
  "id": "jsspsi-vfxpart-dedup-1.6.5",
  "character": "chr_0036_jsspsi",
  "resources": ["world", "ui"],
  "components": [16, 19, 39, 42],
  "observed_at": "2954fa80-23c1-1579-2b22-4ecfd6d70418",
  "expected": { "mesh_name": "S_actor_jsspsi_vfxpart_01_lod0", "indices": 4680 },
  "repaired": { "mesh_name": "S_actor_jsspsi_cloth_06_lod0",    "indices": 4680 },
  "proof": {
    "receiver_path_leaf": "S_actor_jsspsi_vfxpart_01_lod0",
    "require_indices_equal": true,
    "require_vertices_equal": true,
    "require_bone_sequence_equal": false
  },
  "bind": { "geometry_source": "S_actor_jsspsi_cloth_06_lod0" }
}
```

约定：

- `expected` / `repaired` 里的每个字段都是**单值**，没有数组、没有通配。
- `observed_at` 记录这条修复是在哪个 `BundleManifest.Version` 下观测到的。运行时对不上就打日志，提示记录可能过期，但**不因此改变匹配结果**。
- `bind.geometry_source` 用来解决共用网格：`vfxpart_01` 和 `cloth_06` 指向同一个 Mesh 对象，替换器按 Mesh 写顶点，必须声明谁是数据源、另一个只镜像。
- 不设 `platforms` 字段。理由见第 5 节。

### 3.3 层只做两件事

层的职责压到最小：**什么时候调、能给什么**。怎么修，一律由注册进来的函数自己决定。

```cpp
struct RepairContext {
    const CharacterAdapter& adapter;
    void* asset;
    std::span<const GenericMatching::Candidate> candidates;
    std::span<const GenericRendererCandidate> index;
    uint32_t component_id;
    const GenericMatching::Request& request;
    GenericMatching::Match failed;              // 失败状态与候选数
};

struct RepairResult {
    enum class Kind { NotApplied, RewriteRequest, PickCandidate };
    Kind kind = Kind::NotApplied;
    GenericMatching::Request request;           // RewriteRequest 时有效
    size_t candidate_index = 0;                 // PickCandidate 时有效
    const char* note = "";                      // 进日志
};

using RepairFn = RepairResult (*)(RepairContext&);

struct RepairEntry {
    const char* character;   // nullptr = 任意角色
    const char* resource;    // nullptr = 任意资源
    int component = -1;      // -1 = 任意组件
    RepairFn fn;
    const char* id;
};
```

框架执行体只有十几行：

```cpp
Match SelectRepaired(RepairContext ctx, Validate&& valid) {
    auto match = SelectUnique(ctx.candidates, ctx.request, valid);
    if (match.status != MatchStatus::Missing) return match;   // 正常路径不动

    for (const auto& e : g_repairs.For(ctx.adapter.id, ctx.request.resource, ctx.component_id)) {
        auto r = e.fn(ctx);
        if (r.kind == RepairResult::Kind::NotApplied) continue;
        if (r.kind == RepairResult::Kind::RewriteRequest) {
            auto retry = SelectUnique(ctx.candidates, r.request, valid);
            if (retry.status == MatchStatus::Matched) { Log(e.id, r.note); return retry; }
            continue;                                          // 不成，换下一条
        }
        Log(e.id, r.note);
        return {MatchStatus::Matched, r.candidate_index, 1};
    }
    return match;                                              // 仍是 Missing
}
```

`PickCandidate` 是给完全自定义留的口子：handler 自己读资产、自己判断该用哪个渲染器，层不干预。`RewriteRequest` 给数据记录和大多数情况用。

**层不规定**：必须单值（`PickCandidate` 就是直接指定）、必须走全等（handler 自己填 request，怎么填随它）、必须挂 component（`component = -1` 就是角色级）、一次只能一条（循环里可以连续尝试，默认命中即停）。这些是下面数据记录的自加约束，不是层的规矩。

### 3.3.1 数据记录的默认解释器

注册 I 类数据记录时，用一个内置函数把它变成 `RepairFn`：

```cpp
RepairFn MakeDataRecordRepair(const RepairRecord& rec) {
    return [](RepairContext& ctx) -> RepairResult {
        if (!rec.MatchesExpected(ctx.request)) return {};                   // 期望不精确相等就跳过
        if (!VerifyProof(rec.proof, ctx.index, ctx.candidates)) return {};  // 实测
        RepairResult r; r.kind = RepairResult::Kind::RewriteRequest;
        r.request = ctx.request;
        r.request.mesh_name = rec.repaired.mesh_name;
        r.request.indices   = rec.repaired.indices;
        r.note = rec.id;
        return r;
    };
}
```

数据记录自加三条约束，因为它靠"精确"换"零代码"，一模糊就没有价值了：`expected`/`repaired` 全字段单值；`MatchesExpected` 精确相等；改写后仍交给 `SelectUnique`，命中两个就是 `Ambiguous`。

这三条**不上升到层的不变式**。想绕过，直接写 `RepairFn`。

### 3.4 三类修复，一个调度

按"修复的是什么"分三类，不是按载体分：

| 类型 | 修什么 | 作用范围 | 现有实现 | 例子 |
| --- | --- | --- | --- | --- |
| **N 命名映射** | 同一个部件在两个资源里的名字不同（安卓 world lod1 带变体后缀如 `_lod1_11`，UI lod0 没有） | **跨资源**：UI lod0 → world lod1 | `ExactLodRelation` + `android_lod_relations.generated.h`（已存在） | 现有 32 角色的 LOD 表 |
| **I 身份改写** | 同一个位置的网格资产名或索引数变了 | 单资源内 | 新增 `compat_records.inc` | 祀的 vfxpart 去重 |
| **C 契约处理** | 几何结构变了（骨骼重排、bindpose 变化），没法用数据描述 | 单资源内 | 新增 handler 类 | 昼雪 aurora |

三类共用同一次"失败 → 修 → 重选 → 校验"的调度，差别只在注册的是数据还是函数。

**还有第四种：直接写一个 `RepairFn`。** 不套任何记录形态、不归任何一类，逻辑想怎么写怎么写。三类只是常见形态的归纳，不是完备分类——正因为总会有归不了类的情况，层才只提供入口、不规定修法。

**N 类不是新东西。** `android_lod_relations.generated.h` 已经在做这件事，而且做得对：生成脚本 `generate_android_lod_relations.py` 第 10-13 行明确拒绝"给 UI 网格名追 `_lod1`"这种猜测，理由是那样会给带变体后缀的角色产出看着 verified、实际错误的行。这条原则和本方案给数据记录的精确性要求是同一条，应该继承而不是并列。

I / C 两类的记录结构（3.2 那一段）就是给它们用的。N 类保持现有 `ExactLodRelation` 的行格式，不改 schema，只把它纳入同一个 registry 统一调度。

C 类不需要专门接口。昼雪就是**一个 `RepairFn`**：

```cpp
RepairResult RepairAuroraLegacy(RepairContext& ctx) {
    if (!AuroraLegacy::IsLegacyTarget(ctx.adapter.id, ctx.request.resource, ctx.component_id,
            ctx.request.mesh_name, ctx.request.indices, ctx.adapter.explicit_resource)) return {};
    if (!ValidateAuroraLegacyCandidate(ctx.adapter, ctx.asset, ctx.component_id, ...)) return {};
    RepairResult r; r.kind = RepairResult::Kind::RewriteRequest;
    r.request = ctx.request;
    r.request.indices = AuroraLegacy::kCurrentIndices;
    r.request.verified_receiver_path = AuroraLegacy::kReceiverPath;
    r.note = "aurora-legacy";
    return r;
}
```

`aurora_legacy_compat.h` 一行不改，只是从"`#if` 分支里的调用"变成"注册表里的一项"。绑定期需要的 `RemapBone` 通过 `ctx` 带出的绑定对象处理，也不需要虚函数。

## 4. 只有三条是层的规矩

层要薄，规矩就得少。只有下面三条对**所有**修复成立：

1. **默认在 `MatchStatus::Missing` 之后调用。** `Ambiguous` 默认不修——多候选时挑一个是花屏的老来源。确实需要预处理某个角色的，可以显式声明在匹配前执行，但那是选择，不是默认。
2. **一次修复要么完整生效，要么完全不生效。** 不允许"改了一半、校验失败但绑定已经写了"。`SelectRepaired` 只在拿到 `Matched` 之后返回，中间任何一步失败都退回原 `match`。
3. **生效的修复必须能说出来。** 日志里至少有 `id` 和它改了什么。不求格式统一，求能查。

其余都是数据记录的自加约束（3.3.1），不是层的规矩：

| 约束 | 只对谁成立 |
| --- | --- |
| 期望值精确相等、单值、无通配 | 数据记录 |
| 改写后走全等 `SelectUnique` | 数据记录 |
| `proof` 运行时实测 | 数据记录 |
| 作用域 `(character, resource, component)` | 默认注册粒度；`nullptr` / `-1` 可放开 |
| 一次只命中一条 | 默认"命中即停"，不是禁止连续尝试 |

**"组件数不减少"不是修复层的规矩。** 它属于上层 BEM 匹配——`module.cpp:4430` 那条 `return false` 本来就在做这件事。放进修复层会让人误以为层有权决定"跳过某个组件"，而它没有。

现有 `DistinctReceivers`（`module.cpp:4439`）继续保留：它查的是 renderer 指针，拦的是"两个组件抢同一个渲染器"，与共用 Mesh 是两回事，两者都要有。

## 5. LOD 与资源边界

这一节决定修复层挂在哪、以及要不要平台分支。核对代码后的实际结构比"两个平台各走一条"复杂。

### 5.1 Region 有 8 个值，不是 2 个

`Region { Unknown, Lod0, Lod1, Lod2, Lod3, MobileProxy, DesktopProxy, Explicit }`（`generic_model_matcher.h:11`）。`module.cpp:4425` 只决定用 `Explicit` 还是 `Lod0`；其余取值由 `ClassifyReceiver`（`:17`）从渲染器路径前缀解析。

### 5.2 安卓有两条不同的匹配路径

`HasCompatibleModelTarget`（`model_job_runtime.inc:951`）按平台和资源类型分流：

| 条件 | 走哪 | 是否经过 `SelectCompatibleModelDonor` |
| --- | --- | --- |
| Android 且 `!explicit_resource` 且当前 asset 是 world 资源 | `HasCompatibleAndroidLegacyWorld`（`:877`） | **否** |
| 其余全部（PC；Android explicit；Android legacy 但当前 asset 是 UI 资源） | `:964` 起的通用组件循环 | 是 |

### 5.3 Android legacy 的流程

`HasCompatibleAndroidLegacyWorld` 做的事就是你说的"用 UI 的 lod0 替换 world 的 lod1"：

1. 加载 UI 资源作 donor（`route.load_ui(adapter.ui_resource,...)`，`:886`）
2. 在 UI 里按 `Region::Lod0` + `identity.name` + `identity.indices` 做 `SelectUnique`（`:902-909`）
3. 找出对应的 world lod1 位置，三条来源按优先级：
   - `FindExactAndroidLod1Relation`（`:912`）：查 `ExactLodRelation` 表，要求 `reference_confirmed && mesh_names_confirmed`
   - `SelectBoundedAndroidLod1`（`:917`）：**仅 `mod.skip_validation` 时启用**，去掉 `_8/_20` 后缀后比较 LOD 叶子
   - `AndroidLod1Counterpart`（`:930`，定义在 `generic_model_matcher.h:217`）：纯名字变换 `_lod0`→`_lod1`，不查表
4. 用 `Region::Lod1` + `relation.target_mesh` + `target_indices` 在 world 里 `SelectUnique`（`:933-936`）

第 2 步和第 4 步是两次独立的 `SelectUnique`，都不经过 `SelectCompatibleModelDonor`。

### 5.4 `ExactLodRelation` 表本身是一种修复

这张表解决的正是**安卓特有的命名差异**：world lod1 的部件名可能和 UI lod0 的不一样（`s_actor_purrchena_fur_01_lod1_11` 这类变体后缀），不能靠名字替换推出来。

生成脚本 `generate_android_lod_relations.py:10-13` 把这条讲得很明确：

> There is deliberately no fallback that appends `_lod1` to the UI mesh stem: that guess is what produced verified looking rows for characters whose Android fur meshes carry a variant suffix.

所以它是**第 3.4 节里的 N 类记录**，不是"定位机制"。第 3 步的另外两个来源（`SelectBoundedAndroidLod1`、`AndroidLod1Counterpart`）恰恰就是被这张表否掉的那种猜测，只作为开发/兜底路径存在，`skip_validation` 之外的路径用不到。

含义：兼容层不需要为"命名差异"新造一套，把现有表纳入统一 registry 即可。I / C 两类才是需要新增的。

### 5.5 修复入口是三个

| # | 入口 | 覆盖 |
| --- | --- | --- |
| 1 | `SelectCompatibleModelDonor`（`module.cpp:4355`） | PC 全部；Android explicit；Android legacy 下 asset 为 UI 资源的那一次 |
| 2 | `HasCompatibleAndroidLegacyWorld` 的 ui_request（`model_job_runtime.inc:904`） | Android legacy 的 UI lod0 donor 查找 |
| 3 | `HasCompatibleAndroidLegacyWorld` 的 world_request（`:935`） | Android legacy 的 world lod1 receiver 查找 |

只挂在第 1 处，安卓 legacy 的 world 侧修复不会生效。实现上不复制三份逻辑，抽一个 `SelectRepaired(...)`，三处都调它。

### 5.6 安卓侧的两个现有薄弱点

- `android_lod_relations.generated.h` 里 `target_indices_known` 基本为 `false`，所以第 4 步的 `world_request` 默认 `check_indices=false`。**网格被去重时，名字是唯一判据**——祀这种情况在安卓上失败的机制就在这里。
- 该表的 snapshot 仍是 `2954fa80-...`。`ANDROID_BEM_CHAIN_AUDIT.md`（2026-10-15）第 88、94 行已明确记录：这张表**没有按 1.6.4 重新采集**，且"若实际 target path、骨骼、材质或 donor 身份改变，运行时校验仍可能拒绝替换"。这正是兼容层要处理的问题。

### 5.7 仍然不需要 platforms 字段

平台差异已经由 `ExactLodRelation.scope.platform` 和 5.2 的分流条件表达。修复记录只描述网格身份，再加 `platforms` 就是重复表达，还会让同一类变更在不同平台写两份。

祀已实测：两平台 74 个共同渲染器的 meshId 与骨骼数完全一致，`vfxpart_01/04` 与 `cloth_06/08` 共用同一 Mesh。昼雪在 prefab 层实测一致（同一 meshId、87 骨骼）。

## 6. 载体与生命周期

| 载体 | 救谁 | 说明 |
| --- | --- | --- |
| 运行时内置表（编译进模块） | **已经发布的包** | 立即生效，用户不用重下。祀必须走这条 |
| BEM 1.5 组件字段 | 以后出的包 | 同一份 schema；1.4 不能带（`bem_v14.py:98` 已禁止同类字段） |

内置表**先手写** `.inc`，与 `bem_targets.inc` 同级，不写进 `module.cpp`。现在只有 4 条，写生成器不划算；等累积到十几个角色、手写开始出错时再说。加载期做严格校验：角色 id 必须存在于当前 catalog、component 必须在范围内、`expected` 与 `repaired` 不得相等、`repaired` 至少有一项与 `expected` 不同。任何一条不满足就拒绝整个表并打日志（照抄 `bem.cpp:475-490` 对 `bone_name_aliases` 的校验风格）。

这套校验**只针对数据表**。直接注册的 `RepairFn` 不受约束，正确性由它自己负责——毕竟它就是为那些"数据说不清"的情况准备的。

## 7. 落点与改动清单

规划阶段不动代码。实施时按下表逐项来：

| # | 文件 | 位置 | 改动 |
| --- | --- | --- | --- |
| 1 | `native/modules/custom_model/compat_registry.h` | 新增 | `RepairContext`、`RepairResult`、`RepairFn`、`RepairEntry`、注册表、`SelectRepaired()`、`MakeDataRecordRepair()` |
| 2 | `native/modules/custom_model/compat_records.inc` | 新增（生成） | I 类（身份改写）记录表 |
| 3 | `native/modules/custom_model/module.cpp` | `4355-4401` | `SelectCompatibleModelDonor` 加"失败后遍历 registry"框架；两个 `#if` 分支阶段 1 再删 |
| 4 | `native/modules/custom_model/model_job_runtime.inc` | `902-909` | `ui_request` 的 `SelectUnique` 换成 `SelectRepaired`（入口 2） |
| 5 | `native/modules/custom_model/model_job_runtime.inc` | `933-936` | `world_request` 的 `SelectUnique` 换成 `SelectRepaired`（入口 3） |
| 6 | `native/modules/custom_model/android_lod_relations.generated.h` | 不动 | N 类表内容不变，只在 registry 里注册，让三类走同一调度 |
| 7 | `native/modules/custom_model/module.cpp` | `4444-4453` | 绑定期从 `legacy_aurora` 布尔改为 `RepairOutcome`，由记录的 `Apply` 决定 |
| 8 | `native/modules/custom_model/aurora_legacy_compat.h` | 全文件保留 | 内容不变，被 `AuroraLegacyHandler` 引用 |
| 9 | `native/modules/custom_model/aurora_legacy_handler.h` | 新增 | 一个 `RepairFn`，内部调用第 8 项；不改 `aurora_legacy_compat.h` |
| 10 | `native/modules/custom_model/generic_model_matcher.h` | `88-94` | `Request` 不动（保持单值 `indices`），不引入 `accepted_indices` |

第 6 项要特别说明：N 类表**不改格式、不改生成器**，只是让它在调度上和 I / C 两类平级。如果为了"统一"去重构 `ExactLodRelation` 的字段，等于把已经在生产里跑对的东西重写一遍，风险大于收益。

明确不做：

- 不把 `Request.indices` 改成集合。这是上一版方案里最危险的一条，撤回。
- 不动 `SelectUnique` 的命中条件。
- 不在 BEM 1.4 里加字段。
- 不把昼雪的 87 组 bindpose 从 `.h` 挪进数据——可选项，不是前提。

## 8. 分阶段实施

### 阶段 0：骨架 + 祀的记录（不改昼雪）

- 新增 registry 头文件、`SelectRepaired()`、空的 `compat_records.inc`
- **三个入口同时挂上**：`SelectCompatibleModelDonor`、`HasCompatibleAndroidLegacyWorld` 的 ui_request（`:904`）与 world_request（`:935`）
- registry 里只有祀那 4 条（component 16 / 19 / 39 / 42）
- 昼雪分支原地不动，继续走 `#if defined(_WIN32)`
- 验收：祀在 PC 与新安卓上都能替换。**安卓必须在 legacy 路径上验一次**——只在入口 1 挂的话，安卓 world 侧根本走不到修复层，等于没验到入口 2/3。没有记录的角色的行为要与改动前逐字节一致

### 阶段 1：迁昼雪

- 一个 `RepairFn` 落地，逻辑照搬
- 删掉 `#if defined(_WIN32)` 分支，`legacy_aurora` 布尔由 `RepairResult` 带出
- 验收：昼雪的 87 骨骼校验仍然全部生效；故意构造一个 bindpose 不符的 donor，必须被拒

### 阶段 2：N 类入册与清掉 `#if`

- 把 `android_lod_relations.generated.h` 注册进 registry，作为 N 类记录参与同一调度；**表内容与生成器不动**
- 复核 `module.cpp:4383-4396` 的 `#elif defined(__ANDROID__)` 分支：先确认它在 `explicit_resource` 下的实际触发面，能由 N 类记录覆盖就删，不能就保留并补注释说明为什么留
- 目的是让三类修复在同一处可查、可审计，不是省代码
- 验收：安卓 `explicit_resource` 路径行为不变；`AndroidWorldBindingTests` 的 213 项全过

### 阶段 3：暂不做

不做通用检查工具，也不写记录生成器。

理由：现在只有 4 条记录，一次性核对就能保证正确；做一个扫全角色的体检器，工作量远大于它当前能省下的。等记录数量上去、或者这类变更变成每个版本都来的常态，再考虑。

这一阶段从本期范围里拿掉后，方案只依赖「框架 + 手写表 + 三处入口」，范围收敛很多。

## 9. 测试

| 套件 | 新增用例 |
| --- | --- |
| `generic_model_matching_tests` | 负例：`expected` 不精确相等时**不得**触发修复；改写后出现两个候选必须是 `Ambiguous` |
| `custom_model_binding_tests` | 共用 Mesh 只写一次；`geometry_source` 缺失时拒绝而不是默认取第一个 |
| 新增 `compat_registry_tests` | 空注册表时行为与改动前逐字节一致；数据记录加载期校验（角色不存在 / component 越界 / expected 等于 repaired / 重复记录）；`observed_at` 过期只告警；`PickCandidate` 直接指定候选的路径 |
| `AuroraLegacy` 相关 | 迁移前后回归必须逐项一致：材质名、87 骨骼名、87 路径、87×16 bindpose |

回归范围按项目惯例可以放宽，但要包含 `BemV13MorphTests` 与 `AnimationDriverTests`，因为共用 Mesh 的处理会碰到绑定阶段。

## 10. 风险

1. **记录会过期。** 官方再改一次，`repaired` 就不再成立，表现和现在一样是"无法替换"，但至少日志能指出是哪条记录失效。`observed_at` 就是为这个准备的。
2. **共用 Mesh 的写入冲突。** 祀这次两处数据相同所以看不出问题；换一个数据不同的共用网格就是互相覆盖。用数据记录时必须声明 `geometry_source`，不能给默认值。
3. **框架被用歪的两个方向。** 一是把数据记录当成唯一形态，硬把本该写函数的逻辑塞进 `expected → repaired`；二是反过来，每个 case 都写函数，数据表形同虚设。判据很简单：能用一句话说清"原来是什么、现在是什么"的就填数据，说不清的就写函数，不要为了统一而扭曲。
4. **`SelectUnique` 是全局的。** 改动集中在 `SelectCompatibleModelDonor`，但它的调用方 `CaptureGenericResourceBindings`（`module.cpp:4422`）也在同一路径上，回归要覆盖。

## 11. 未决问题

1. 安卓侧昼雪 `cloth_01_lod0` 的 **index_count** 尚未实测。需要 53 个闭包 bundle 所在的 6.71 GB chunk，本次停在传输前。现有证据是 meshId 与 87 骨骼在 prefab 层完全一致，属高度可信推断。是否补测取决于这条记录要不要写死 `indices`。
2. `android_lod_relations.generated.h` 的 snapshot 仍是 `2954fa80`，没有按 1.6.4 重新采集。这是 N 类的存量缺口，与本次方案无关但同一个病根。重新采集需要跑 `build_android_lod1_index.py` 逐角色读安卓 prefab，本次已经证明这条路可行（祀、昼雪都解出来了），但只覆盖了 2 个角色。
3. 祀的 postmodel 闭包在 PC 是 146 个 bundle、安卓 118 个，118 个同名，PC 多 28 个。多出来的部分未逐个查明是否影响替换，倾向于是 Windows 侧额外依赖。
4. I 类和 N 类在语义上有重叠（都改名字）。分界：N 类改的是"哪个位置对应哪个位置"（跨资源），I 类改的是"那个位置上的网格身份"（单资源内）。如果实际接入时发现某个 case 两边都说得通，以"是否需要跨资源"为准判定，不要两处都写。
5. 官方这类改名/去重/重排的出现频率未知。一个版本一次则这套框架是刚需；一两年一次则阶段 0 + 1 就够，阶段 2 也可以缓。

## 12. 实施记录（2026-11-05，阶段 0）

### 12.1 落地的文件

| 文件 | 状态 | 内容 |
| --- | --- | --- |
| `native/modules/custom_model/compat_registry.h` | 新增 | `RepairContext` / `RepairResult` / `RepairFn` / `DataRepairRecord` / `FunctionRepair` / `RepairFromDataRecord()` / `SelectRepaired()` |
| `native/modules/custom_model/compat_records.inc` | 新增（手写） | 祀的 4 条 I 类记录 + 空的函数修复表 |
| `native/modules/custom_model/module.cpp` | 改 | 头部引入两个新文件；`SelectCompatibleModelDonor` 在 `Missing` 后调用 `SelectRepaired`，命中即返回 |
| `native/modules/custom_model/model_job_runtime.inc` | 改 | `HasCompatibleAndroidLegacyWorld` 的 ui_request（入口 2）、world_request（入口 3）由 `SelectUnique` 换成 `SelectRepaired`；循环改为带下标以传 `component_id` |
| `native/tests/compat_repair_tests.cpp` + `native/tests/compat_repair/CMakeLists.txt` | 新增 | 40 项契约测试 |

`aurora_legacy_compat.h`、`generic_model_matcher.h`、`android_lod_relations.generated.h` 三个文件**未改动**，符合第 7 节第 6、8、10 项。

### 12.2 数据记录实际长什么样

```cpp
{"jsspsi-vfxpart01-cloth06-world",
    "chr_0036_jsspsi","chr_0036_jsspsi_postmodel",-1,
    "S_actor_jsspsi_vfxpart_01_lod0",4680,true,
    "S_actor_jsspsi_cloth_06_lod0",0,false,
    "2954fa80-23c1-1579-2b22-4ecfd6d70418"},
```

字段依次是 `id / character / resource / component / expected_mesh / expected_indices / expected_indices_known / repaired_mesh / repaired_indices / repaired_indices_known / observed_at`。

`component` 取 `-1`：本地 component 下标由包内资源选择顺序决定，而网格身份在每个资源里唯一，按身份打靶不会误伤。

证明的四项在运行时实测，任一不成立就放弃：槽位叶子等于 `expected_mesh`、实际网格名等于 `repaired_mesh`、索引数等于 `expected_indices`、且**有且只有一个**候选满足以上三条。改写后把 `verified_receiver_path` 钉到被证明的那个槽位，避免去重后的共享网格让重选变成 `Ambiguous`。

### 12.3 实施中确认的两件事

**1. 资源级包（BEM 1.4）天然能区分共享网格。** `mod_registry.cpp:178,186` 在包带 `resources` 时把 `component_paths` 填进 `ComponentIdentity.receiver_path` 并把 `explicit_resource` 置真，请求因此带 `verified_receiver_path`，`SelectUnique` 按路径过滤——去重后两个槽位共用同一网格也不会互相干扰。祀这类新包走这条，修复层只处理"名字变了"。

**2. 无路径的旧包（BEM 1.3）在共享槽位上会 `Ambiguous`。** 这类包不携带 `receiver_path`，`SelectUnique` 只按网格名比较，`cloth_06` 的组件会同时命中 `vfxpart_01` 槽位和 `cloth_06` 槽位。修复层按设计不处理 `Ambiguous`，因此**只修好了被改名的槽位，修不了共用网格的另一侧**。这条已写进测试 `LegacyResourceChecks()` 固定行为。要覆盖它需要"在匹配前按资产自身层级确定唯一槽位"的预处理，属于阶段 2 以后的事，且需要包提供路径，旧包本身给不出。

### 12.4 验证结果

| 套件 | 结果 |
| --- | --- |
| `CompatRepairTests`（新增） | PASS 40 |
| `BetterEndfieldNext.CustomModelBindingTests` | 通过 |
| `BetterEndfieldNext.AndroidWorldBindingTests` | PASS 213 |
| `BetterEndfieldNext.AndroidModelLodTests` | PASS 68 |
| `AuroraLegacyCompatibilityTests` | PASS 228 |
| `BetterEndfieldNext.CustomModel`（模块本体） | 编译通过，仅既有告警 |

新增测试覆盖：记录表自检（每条形如"改名"且带观测版本）、已满足的契约根本不进注册表、按声明槽位命中、第二条记录独立命中、索引数不符/未实测/请求指向别的槽位/索引数不在记录里/未登记的角色与网格/跨角色作用域六种拒绝路径、拒绝后请求不被改写、以及无路径旧包的行为。

### 12.5 仍然没做的

- 阶段 1：昼雪迁移。`#if defined(_WIN32)` 分支原样保留。（已于同日完成，见第 13 节）
- 阶段 2：N 类表入册、清 `#elif defined(__ANDROID__)`。
- 安卓 world lod1 的记录：没有实测过 `S_actor_jsspsi_vfxpart_01_lod1` 这一侧的网格名，按"不猜"原则不写。入口 3 已挂上，测到之后加一行即可。
- `PickCandidate` 分支目前没有真实用例，测试也未覆盖（函数修复表为空）。（阶段 1 已补齐真实用例与测试，见 13.3）

## 13. 实施记录（2026-11-05，阶段 1：昼雪入册）

### 13.1 改了什么

| 文件 | 改动 |
| --- | --- |
| `native/modules/custom_model/compat_handlers.inc` | 新增。`RepairAuroraLegacy`（普通函数，非虚接口）+ 函数修复表 |
| `native/modules/custom_model/compat_registry.h` | `FunctionRepair` 增加 `also_when_unchecked`；`SelectRepaired` 拆出 `SelectRepairedFrom`（接受已算好的 ordinary match）并新增 `applied` 出参；`PickCandidate` 现在也要过调用方的 `validate` |
| `native/modules/custom_model/module.cpp` | 删掉整段 `#if defined(_WIN32)` 的 aurora 分支；`SelectCompatibleModelDonor` 去掉 `windows_compat` 形参；改为 consul 注册表后按 `applied` 判定 `legacy_aurora` |
| `native/modules/custom_model/compat_records.inc` | 只保留数据表，函数表移到 `compat_handlers.inc` |
| `native/tests/compat_repair_tests.cpp` | 新增 `HandWrittenRepairChecks()`，覆盖 `also_when_unchecked`、`PickCandidate`、以及 pick 未过 `validate` 时必须回退 |

`aurora_legacy_compat.h` 仍然一行未改；绑定阶段的 `ValidateAuroraLegacyDonor` 与 `RemapBone` 也没动，只是"这次是 aurora"这个事实由修复 id 带出来。

### 13.2 两个必须记下来的细节

**1. `#if defined(_WIN32)` 没有消失，只是从流程变成了注册。** aurora 处理器依赖的 `ValidateAuroraLegacyCandidate` 本身是 Windows-only 的运行时观察，所以 `compat_handlers.inc` 里仍有 `#if defined(_WIN32)`——但它只决定"这个平台上存在哪些修复"，不再把匹配流程切成两半。安卓侧注册为空表。

**2. `also_when_unchecked` 是这条迁移唯一新增的层能力。** 旧分支的触发条件是 `Missing || !check_indices`：包用 `skip_validation` 时，按名字匹配成功并不代表几何对得上，aurora 仍要重选并强制 87 骨骼契约。层默认只在 `Missing` 后咨询，所以这一个字段就是让处理器显式声明"我也要看未校验的匹配"。它按条目声明，不是全局开关。

### 13.3 验证

| 套件 | 结果 |
| --- | --- |
| `CustomModelBindingTests --aurora-legacy` | **PASS 674**（UI/world、keep/hide、palette、discovery、Job、refusal） |
| `CustomModelBindingTests`（默认） | 通过 |
| `CompatRepairTests` | PASS 52 |
| `AndroidWorldBindingTests` | PASS 213 |
| `AndroidModelLodTests` | PASS 68 |
| `AuroraLegacyCompatibilityTests` | PASS 228 |
| `BetterEndfieldNext.CustomModel` | 编译通过 |

`--aurora-legacy` 里第 83-87 行就是关键一条：`skip_validation=true` 时仍要求 `bindings[1].aurora_legacy_163` 为真（原注释写着 "CRITICAL BUG ... if bypassed"），迁移后依旧通过——说明 `also_when_unchecked` 承接的是原来那条分支，而不是把它丢了。

### 13.4 顺带修掉的一个隐患

`PickCandidate` 原先直接返回 `Matched`，不跑调用方的 `validate`。那等于给手写修复开了一个绕过契约校验的后门，而 `candidate_valid` 里含 `GenericCandidateContractError`（renderer 种类、材质/骨骼数量）。现在 pick 出来的候选同样要过 `validate`，测试里有一条专门验证"pick 的候选被 validate 拒绝时必须回退到原结果"。

### 13.5 仍然没做的

- 阶段 2：`android_lod_relations.generated.h` 入册、清 `#elif defined(__ANDROID__)`。
- 安卓 world lod1 的记录（同上，无实测数据）。
- `DataRepairRecord` 与 `FunctionRepair` 之外的第三种形态目前没有需求，不加。
