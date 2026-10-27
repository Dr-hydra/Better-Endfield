# BEM 部件组合器双端实现规划

- 状态：方案稿（1.4 原子项设计已确认）
- 适用范围：Better Endfield Next PC / Android
- 依据：当前仓库 1.5.3 / 1.6.3 资源结构与现有实现
- 最后更新：2026-10-24

## PC 首轮实现审查（2026-10-27）

总体架构可行；实现时以下修正优先于下文原始伪代码：

- 2026-10-27 交互修订：PC 默认保留源包完整离散组合逻辑，当前选择仅初始化默认值；下文的默认快照模式已被替代。源选项、可用条件、组合约束、draw 和贴图槽条件统一重写命名空间；一个选项控制多个部件时共享状态。外层来源选择门控部件规则，未使用来源隐藏其选项并暂停其约束。
- 同一显式来源的普通模型和大招保留在同一输出，共享选项状态；独立大招根集合仍分别整体可选，重叠但不相同的根集合拒绝。界面显示「关联 N 个部件」，按需展开完整名称和资源路径，避免长组件名挤占常规布局。

- 组件预算按输出内唯一接收器及 donor 闭包计数，不是所有候选组件数量相加；同一接收器的多个候选共用一个组件 ID。
- donor-only 只指从未作为可选接收器的组件；某组件同时是槽位和 donor 时不得再追加无条件 keep，否则产生多个生效操作。
- Plan/Build 必须绑定平台、来源路径、文件大小、修改时间、来源选项和参数。Build 重新规划并核对，拒绝过期或被修改的计划。
- 构建 API 必须传递与规划一致的 options；候选选择用于设置输出默认值，不能删掉其他候选，否则丢失运行时自由切换能力。
- 1.0 的 draw 共用 indices 并含 start；升级为 1.1+ 时必须切分索引范围，不能只替换字段名。
- available_when、draw.when、texture_slots 必须使用现有核心的有效选项求值；贴图槽 null 表示不覆盖，不能写成索引 0。
- 参数默认值改为保存值不等于烘焙形变；驱动器音频、材质贴图和所有条件引用也需要重映射。未实现这些语义前必须显式拒绝，不能静默丢失。
- 不认识的资源、大招和武器整组作为可选原子项，默认不加入；相同根集合是候选，互不重叠的不同根集合是独立项。普通 world/UI 保持原有共用，不因混合 1.4 大招就强制迁移目标结构；角色与武器身份不同，由组合器统一生成并导入独立有效包。
- 构建先在同卷独立 stage 写入并完整加载所有候选，成功后发布新目录；输出已存在、取消或校验失败均不得覆盖来源或已发布包。

当前 PC 范围：共享 C++ 读取/组合核心、原生 plan/build/validate CLI、已安装包 WinUI 入口；支持 1.0–1.5 中不含尚未实现功能的来源，保留包内可切换候选；大招/未知资源和武器整体可选，不选则不进入输出。普通角色、大招、多个武器可作为一个项目生成包集合并连续导入，尚不额外封装 ZIP。包显示版本及 BEM 输出格式自动确定。参数/形变、1.5 材质覆盖/root offset/驱动器和 Android 接口仍未实现，须按具体功能明确报错。

已实现的使用方式、输出 1.2/1.4 的版本下限、JSON 接口及待验收项见 docs/custom_model/BEM_COMPOSER_PC.md；下文为完整路线，不代表首轮已实现全部阶段。

## 1. 结论

可以实现，且应做成**双端原生**：

- 组合引擎使用 C++，放入现有 `BetterEndfieldNext.CustomModelCore`。
- PC 端通过 CLI 和 WinUI 向导调用。
- Android 端通过 JNI 直接调用同一份 C++ 核心，Java 只负责界面、任务编排和安装。
- 手机端不依赖 PC，可以在手机上完成选包、选部件、生成、校验、启用全流程。

运行时仍然保持“一个角色同一时间只能启用一个包”。组合器不是让多个包同时生效，而是把多个已安装包离线合并成新的 BEM 包。

1.4 的处理方式已经确认：

- **普通角色**按细粒度部件槽位自由组合。
- **大招**作为原子可组合项，与普通角色同属 `character + 角色 ID`，可以放在同一个 1.4 角色包内。
- **武器**作为原子可组合项，属于 `weapon + 武器 ID`，单独输出 1.4 武器包。
- 最终仍输出 1.4 包；涉及武器时输出一组 1.4 包，以 ZIP bundle 交付。

## 2. 产品目标

### 2.1 必须实现

1. 从**已安装**的同角色包中选择两个或更多来源。
2. 按游戏原生部件建立槽位，而不是按 Mod 自己的文件命名。
3. 每个槽位支持保留原版、替换、隐藏、keep + 贴图覆盖。
4. 生成的包在游戏内可继续切换部件，不需要重新导入。
5. PC 与 Android 都能独立完成全部流程。
6. 源包只读，不修改、不删除。
7. 输出包通过现有 BEM 校验，并能在目标平台加载。

### 2.2 明确不做

- 不做运行时多包叠加。
- 不做跨角色组合。
- 不做跨资源 donor 借用。
- 不做自动骨骼重定向或自动补骨骼。
- 不做 Shader 合并。
- 不执行源 Mod 的 INI、脚本或 Shader。
- 不把不同游戏版本的部件强行拼接。

## 3. 现状与可复用基础

### 3.1 格式层已经具备的能力

BEM 已原生支持部件级组合：

- `component_rules` 为每个 target 部件声明 `keep` / `hide` / `replace`。
- `option_groups` + 条件表达式表达互斥选择。
- `selection_constraints` 限定合法组合。
- 骨骼 donor 与材质 donor 可以来自不同部件。
- 1.4 支持显式多资源目标、静态武器和大招 prefab。
- 1.5 支持材质覆盖与动态驱动器。
- ZIP 容器可直接承载 manifest 与 payload。

因此**不需要扩展 BEM 格式，也不需要修改游戏运行时**。

### 3.2 PC 端现有代码

| 能力 | 位置 |
| --- | --- |
| BEM 解析、校验、加载 | `native/modules/custom_model/bem.cpp` |
| 包元数据 | `BemPackageInfo`，`native/modules/custom_model/bem.h` |
| 已安装包列表 | `ui/BetterEndfieldNext.UI/Services/BemPackageService.cs` |
| 包管理页 | `ui/BetterEndfieldNext.UI/Views/CustomModelPage.xaml.cs` |
| 转换工具窗口 | `ui/BetterEndfieldNext.UI/Views/BemConverterWindow.cs` |
| 工具进程调用 | `ui/BetterEndfieldNext.UI/Services/BemToolService.cs` |
| Python 创作 CLI | `tools/CustomModel/bem_tool.py` |
| 打包 / 解包 / bundle | `tools/CustomModel/bem_projects.py` |

### 3.3 Android 端现有代码

| 能力 | 位置 |
| --- | --- |
| 第三方模型页 | `android/app/src/main/java/dev/betterendfield/next/BemInstallPage.java` |
| 安装索引与发布 | `BemInstaller.java` |
| 选项解析 | `BemOptions.java` |
| 参数解析 | `BemParameters.java` |
| 原生校验 JNI | `android/app/src/main/cpp/installer/install_jni.cpp` |
| BEM 重写 | `RewriteBemTextures`，`native/modules/custom_model/bem.cpp` |
| Android 原生构建 | `android/app/src/main/cpp/CMakeLists.txt` |

Android 安装器已经链接 `BetterEndfieldNext.CustomModelCore`，组合核心可以直接编入现有 native 库，不需要新增 ABI 或额外 `.so`。

## 4. 总体架构

```text
                    +-----------------------------+
                    | BetterEndfieldNext.CustomModelCore
                    | bem.cpp / bem_compose.cpp    |
                    | BemPackageReader             |
                    | PlanBemComposition           |
                    | BuildBemComposition          |
                    +--------------+--------------+
                                   |
              +--------------------+--------------------+
              |                    |                    |
      +-------v--------+   +-------v--------+   +-------v--------+
      | BemCompose.exe |   | compose_jni.cpp|   | bem_tool.py    |
      | PC CLI         |   | Android JNI    |   | compose 子命令 |
      +-------+--------+   +-------+--------+   +-------+--------+
              |                    |                    |
      +-------v--------+   +-------v--------+   +-------v--------+
      | WinUI 向导     |   | Java 组合页    |   | 创作者 CLI     |
      +----------------+   +----------------+   +----------------+
```

原则：

1. **格式逻辑只写一份**：C++ 核心。
2. **平台差异只在 I/O 和 UI**：PC 文件系统、Android framework remote file。
3. **计划与构建分离**：先出轻量计划给 UI，再执行重任务。
4. **流式处理**：不把多个包整体读入内存。

## 5. 核心数据模型

新增 `native/modules/custom_model/bem_compose.h`。

```cpp
namespace BetterEndfieldNext::CustomModel {

enum class BemComposePlatform { windows_x64, android_arm64 };

struct BemComposeSource {
    std::filesystem::path package;   // 已暂存的本地 BEM/ZIP
    std::string selection;           // 选项或外观；空表示默认
    std::string parameters;          // 滑条保存值
    std::string label;               // UI 显示名
};

struct BemComposeOptions {
    BemComposePlatform platform = BemComposePlatform::windows_x64;
    std::string project_id;
    std::string name;
    std::string author;
    std::string version;
    bool snapshot_mode = true;
    bool include_original_choice = true;
    bool atomic_snapshot_on_overflow = true;
    uint64_t memory_budget = 0;
};

struct BemComposeCandidate {
    uint32_t source_index = 0;
    std::string source_package_id;
    std::string operation;           // keep / hide / replace
    std::string mesh_name;
    std::string resource_id;
    std::string renderer_path;
    bool donor_only = false;
};

struct BemComposeSlot {
    std::string id;
    std::string display_name;
    std::string resource_id;
    std::string mesh_name;
    uint32_t original_index_count = 0;
    bool selectable = true;
    bool atomic = false;
    std::vector<BemComposeCandidate> candidates;
};

struct BemComposeAtomicItem {
    std::string id;
    std::string display_name;
    std::string target_kind;         // character / weapon
    std::string target_id;
    std::vector<std::string> resources;
    std::vector<BemComposeCandidate> candidates;
};

struct BemComposePlan {
    std::string project_id;
    std::string target_kind;
    std::string target_id;
    std::vector<std::string> resources;
    std::vector<BemComposeSlot> slots;
    std::vector<BemComposeAtomicItem> atomic_items;
    std::vector<std::string> output_packages;
    std::vector<std::string> warnings;
    std::vector<std::string> errors;
    uint32_t estimated_components = 0;
    uint32_t component_limit = 64;
    uint32_t estimated_groups = 0;
    uint32_t estimated_rules = 0;
    uint64_t estimated_output_bytes = 0;
    uint64_t worst_case_selected_bytes = 0;
    bool requires_texture_conversion = false;
    bool requires_atomic_snapshot = false;
};

struct BemComposeChoice {
    std::string slot_id;
    uint32_t candidate_index = 0;
};

struct BemComposeReport {
    std::string project_id;
    std::vector<std::string> outputs;
    uint64_t bytes = 0;
    uint32_t payloads = 0;
    uint32_t groups = 0;
    uint32_t rules = 0;
    std::vector<std::string> warnings;
};

bool PlanBemComposition(const std::vector<BemComposeSource>&,
                        const BemComposeOptions&,
                        BemComposePlan&, std::string& error);

bool BuildBemComposition(const std::vector<BemComposeSource>&,
                         const BemComposePlan&,
                         const std::vector<BemComposeChoice>&,
                         const std::filesystem::path& output_directory,
                         BemComposeReport&, std::string& error);

} // namespace
```

### 5.1 为什么计划与构建分离

- `Plan` 只读 header 与 manifest，速度快，可在 UI 中反复调用。
- `Build` 才读取 payload，耗时长，需要进度与取消。
- Android 可用计划 JSON 做旋转恢复与任务恢复。
- PC 可先展示冲突与预算，再让用户确认。

## 6. 关键前置重构

### 6.1 暴露只读包读取器

当前 `Container` 位于 `bem.cpp` 的匿名命名空间中，`bem_compose.cpp` 无法直接使用。

新增 `bem_package_reader.h/.cpp`：

```cpp
class BemPackageReader {
public:
    bool Open(const std::filesystem::path&, std::string& error);
    bool IsZip() const;
    uint16_t Minor() const;
    const nlohmann::json& Manifest() const;

    bool CopyPayload(uint32_t id, std::ostream& out,
                     uint32_t& codec, uint64_t& decoded,
                     const std::function<void()>& checkpoint,
                     std::string& error) const;
};
```

要求：

- 复用现有 BEM / ZIP 解码与目录校验。
- 不改变现有运行时加载路径。
- 只提供组合器需要的只读能力。
- 允许按 payload 流式复制，不整体加载。

### 6.2 不新增非标准 manifest 字段

MVP 不往 manifest 写自定义字段。来源信息写入：

- `target.snapshot`：不超过 256 字节的摘要；
- 输出旁的 `compose-report.json`：完整来源、选择、警告、大小。

这样不会影响现有严格校验器，也避免未来版本兼容问题。

### 6.3 版本策略

输出包版本取所需最低版本：

- 只有部件规则：1.1 或 1.2；
- 有贴图槽：至少 1.2；
- 有参数或形变：至少 1.3；
- 有多资源、静态 mesh、大招或武器：至少 1.4；
- 有材质覆盖或驱动器：至少 1.5。

不要无条件写 1.5，避免让旧运行时拒绝。

## 7. 组合算法

### 7.1 第一步：解析来源状态

对每个来源包：

1. 读取 manifest 与 `BemPackageInfo`。
2. 按目标平台过滤资源。
3. 解析当前选择：1.0 外观；1.1+ 选项组；1.3 允许空选项组；1.4/1.5 按资源与平台解析。
4. 求值每个 target 部件的 `component_rules`，得到唯一操作。
5. 解析 `texture_slots` 为固定贴图索引。
6. 记录每个部件的 `operation`、`mesh`、`material_overrides` 和实际贴图引用。

**快照模式**下，来源包内部的选项不再保留，只保留解析后的固定状态。这正好覆盖“包本身没有部件选项”的情况。

### 7.2 第二步：区分细粒度部件与原子项

组合器必须先把来源拆成两类：

| 类型 | 判定 | 组合粒度 |
| --- | --- | --- |
| 普通角色形态 | `character` 且资源根为普通 postmodel/uimodel | 细粒度部件槽位 |
| 大招 | `character` 且资源根为 ultimate/ability/mirror 等 | 原子项 |
| 武器 | `weapon` | 原子项 |

判定不能只看资源名，还应结合：

- 来源包里该资源的 `renderer_kind`；
- 资源根集合是否覆盖多个 prefab；
- 是否与普通形态共享组件；
- 1.4 来源的 `asset_path` 与 target id。

无法确定时，默认按原子项处理，并在计划中显示警告。宁可少拆，不要错误拆分。

### 7.3 第三步：建立细粒度槽位

槽位键：

- 1.0–1.3：`mesh_name`；
- 1.4+：`resource_id + mesh_name`。

槽位身份还必须校验：

- `original_index_count` 相同；
- `bone_names` 完全一致；
- `materials` 完全一致；
- 1.4 的 `renderer_kind` 与 `renderer_path` 一致。

任意一项不一致，直接报“来源包对应不同游戏资源版本”，不允许静默拼接。

### 7.4 第四步：收集 donor 闭包

这是最容易漏掉的一步。

一个替换网格可能借用其他部件的骨骼或材质。合并后必须把这些 donor 部件也放进新包的 `target.components`，否则 donor 索引会悬空。

算法：

1. 把用户选中的槽位部件和原子项组件加入集合。
2. 对每个选中的 mesh descriptor 加入所有 `bones[].component` 与 `draws[].material_component`。
3. 重复直到集合稳定。
4. donor-only 部件标记为不可选，生成无条件 `keep` 规则。
5. 1.4 下校验所有 donor 与目标 mesh 属于同一 resource。

### 7.5 第五步：生成细粒度选项组与规则

对每个普通角色槽位：

- 只有一个候选：生成无条件规则。
- 多个候选：生成一个选项组 `slot_<id>`。
  - choices：每个候选一个；
  - default：复现第一个来源的状态；
  - 每个候选规则带 `when: {"eq": ["slot_<id>", "<choice>"]}`。
- `include_original_choice` 为 true 时，额外加入 `keep` 候选。

这样每个槽位在任意组合下都恰好有一个候选成立。

上限：

- 选项组最多 64；
- 1.1 每组最多 16 个 choice，1.2+ 最多 64；
- 候选规则最多 512（1.1）或 4096（1.2+）。

### 7.6 第六步：重建 target 部件表

新包的 `target.components` 是 donor 闭包的并集。

- 全局 ID 从 0 连续编号。
- 排序必须确定：先按 resource 顺序，再按 `renderer_path`，最后按 `mesh_name`。
- 保留来源的 `mesh_name`、`original_index_count`、`bone_names`、`materials`。
- 1.4 保留 `resource`、`renderer_kind`、`renderer_path`。
- 资源 ID 重新分配为 `res0..resN`，并建立来源资源 ID 到新资源 ID 的映射。

### 7.7 第七步：重建 mesh 与贴图

每个来源 mesh 只复制一次，按 `(source_index, mesh_index)` 去重。

重映射：

- `streams[].payload`
- `draws[].indices`
- `bones[].component`
- `draws[].material_component`
- `draws[].material_overrides[].component`
- `mesh_deformations[].mesh`
- `mesh_deformations[].frames[].payload`

贴图处理：

- 固定贴图索引直接重映射。
- `{"slot":"id"}` 在快照模式下已解析为固定索引，合并后不保留 `texture_slots`。
- 相同 payload 先去 `(source, payload_id)` 身份，再按内容哈希 + 字节比对去重。
- 内容哈希只用于内部去重，不写入产物报告，不改变“默认不计算产物哈希”的约定。

### 7.8 第八步：参数、形变与驱动器

1.3+：

- 参数 ID 加来源前缀，例如 `s0_body`。
- 重写 `mesh_deformations[].parameter`。
- 重写 `drivers[].bindings[].target` 中的 `morph_parameter`。
- 参数总数最多 64。
- `available_when` 在快照模式下按来源当前选择求值；不可用的参数直接丢弃。

1.5：

- 驱动器 ID 加来源前缀。
- 重映射 `component`。
- 驱动器总数最多 16，超出则报错并列出冲突项。
- draw 级与 keep 级 `material_overrides` 原样复制，只重映射 component。
- 多个来源的 `root_offset` 必须一致；不一致时报错。

### 7.9 第九步：写入

推荐写**二进制 BEM**：

- 两端运行时与安装器都已支持；
- 不需要新增 ZIP 读取路径；
- 可以直接复用现有安装流程；
- 支持流式 spool 与取消。

写入流程：

1. 构建完整 manifest，分配 payload ID。
2. 流式复制 payload 到临时 spool 文件，记录 codec、stored、decoded。
3. 写 header、manifest、directory。
4. 追加 spool。
5. 删除 spool。
6. 调用现有校验器验证默认选择与抽样选择。

内存占用应为 O(单个 payload)，不是 O(总包大小)。

### 7.10 1.4 原子项与输出包集合

1.4 的 `target.kind` + `target.id` 是**包级身份**，不能在一个包内混合角色和武器。

| 组合对象 | 包级身份 | 组合粒度 |
| --- | --- | --- |
| 普通角色 | `character + 角色 ID` | 细粒度部件槽位 |
| 大招 | `character + 角色 ID` | 原子项，与普通角色同包 |
| 武器 | `weapon + 武器 ID` | 原子项，单独 1.4 包 |

**原子项定义**

```text
AtomicItemKey = target_kind + target_id + resource_root_set
```

- 大招通常包含 `ultimate`、`ability`、`mirror` 等资源根，这些资源必须作为一组处理。
- 武器通常只有一个资源根。
- 所有候选必须声明同一组资源根；根集合不同则不是同一个原子项，不能互相替换。
- 每个候选必须覆盖该组内的全部资源；缺失的资源用 `keep` 补齐，并在计划中显示警告。
- 原子项只生成一个选项组，组内每个 choice 对应一个候选，choice 同时控制该组内所有组件的规则。
- 原子项内部的组件不单独暴露给用户。
- donor-only 组件仍然无条件 `keep`，不参与原子项选择。

**输出包集合**

- 只选普通角色 + 大招：输出一个 1.4 `character` 包。
- 还选了武器：输出一个 1.4 `character` 包，以及每个武器一个 1.4 `weapon` 包。
- 多个武器属于不同 `weapon + 武器 ID`，各自独立成包，可以同时启用。
- 多包通过 ZIP bundle 交付。
- 每个输出包使用独立 `package_id`，例如 `<project>-character`、`<project>-weapon-<id>`。
- 组合项目本身有 `project_id`，只用于 UI 和报告，不写入 manifest。

**组件预算**

`target.components` 最多 64 项，是 1.4 原子项的主要硬限制：

```text
普通部件组件 + 所有原子项所有候选的组件之和 <= 64
```

超过时提供两种降级：

1. **原子项快照**：每个原子项只保留一个候选，牺牲该原子项的运行时切换，保留普通部件自由组合。
2. **拆分输出**：每个原子项候选生成独立完整包，用户安装其中一个。

默认采用原子项快照，并在计划中标记 `requires_atomic_snapshot=true`。

### 7.11 原子项的同一性校验

原子项不能只看资源根名字，必须逐项校验：

- 每个候选的资源根集合完全一致；
- 每个资源根的 `asset_path` 一致；
- 每个资源根的 `lod` 一致；
- 每个资源根下的组件表（`renderer_path`、`renderer_kind`、`mesh_name`、`original_index_count`）一致；
- 同一资源根内的 donor 表一致。

任意一项不一致，说明来源包对应不同的游戏资源版本或不同的大招形态，必须拒绝，不能退化成 keep 补齐。

keep 补齐只允许用于“资源根本身存在、但该候选没有替换该根内某个组件”的情况，不允许凭空补出一个不存在的资源根。

### 7.12 1.4 输出的来源前提

输出 1.4 包需要 1.4 必需的字段：

- `target.kind` + `target.id`
- `resources[].id / name / asset_path / platforms / lod`
- 组件的 `resource` / `renderer_kind` / `renderer_path`

旧版 1.0–1.3 包只有 `character_id` / `world_resource` / `ui_resource`，**没有** `asset_path` 和 `renderer_path`。因此：

- 来源全是 1.4 包：可以直接输出 1.4。
- 来源含 1.0–1.3 包：必须额外提供 1.4 target profile（资源根、asset_path、renderer_path、LOD），否则只能输出 1.1–1.3 角色包。
- 旧版来源永远不能凭空产生大招或武器资源，因为旧包不声明这些资源根。

UI 必须在选包阶段就提示这一点，避免用户选完才发现无法输出 1.4。

## 8. 平台策略

### 8.1 组合按平台进行

PC 与 Android 的资源 LOD、贴图格式和 donor 表可能不同。MVP 明确：

- `compose --platform windows-x64`
- `compose --platform android-arm64`

生成**平台专用 1.4 包**。

原因：

- 普通角色与大招在 Windows 和 Android 上通常使用不同 LOD 与 donor 合同；
- Android 需要 ASTC，PC 通常使用 BC；
- 强行双端合并容易出现“PC 正常、手机花屏”。

### 8.2 1.4 的平台与包级身份

1.4 的 `target.kind` + `target.id` 是**包级身份**，一个包只能属于一个角色或一个武器。因此：

- 普通角色与大招都属于 `character + 角色 ID`，可以放在同一个 1.4 角色包中。
- 武器属于 `weapon + 武器 ID`，**不能**与角色包合并，必须单独输出 1.4 武器包。
- 一次组合可以选择角色 + 大招 + 多个武器，但输出是**一组 1.4 包**，不是强行塞进一个文件。
- 多包使用 ZIP bundle 交付；PC 的 `bundle` 与 Android 的 ZIP 导入都已支持。

1.4 的 `resources[].platforms` 允许一个包声明多个平台，但组件数会按平台翻倍，并受 64 个组件上限约束。MVP 仍按平台分别生成。

### 8.3 Android 贴图

组合阶段**不做纹理转换**，避免重复解码和二次压缩。

- 组合完成后检查贴图格式；
- 目标为 Android 且存在 BC 格式时，标记 `requires_texture_conversion=true`；
- 由现有 `convertNative` 执行一次转换；
- 转换后再做最终校验与安装。

Android UI 默认在生成后自动转换。

## 9. PC 端实现

### 9.1 CLI

新增 `BetterEndfieldNext.BemCompose.exe`：

```text
BemCompose plan --platform windows-x64 \
  --source A.bem --selection "outfit:a" \
  --source B.bem --selection "" \
  --json plan.json

BemCompose build --plan plan.json --choices choices.json --output-dir out

BemCompose validate --package out/character.bem --platform windows-x64
```

`--json` 输出结构化结果，便于 WinUI 解析。

`bem_tool.py` 增加：

```text
python tools/CustomModel/bem_tool.py compose ...
```

内部委托给 `BemCompose.exe`，保持创作 CLI 入口一致。

### 9.2 WinUI

在 `CustomModelPage` 增加“组合已安装包”入口。

新增 `BemComposeWindow`，步骤：

1. 选择角色。
2. 选择来源包（只显示同角色、同平台兼容的包）。
3. 普通角色显示部件矩阵；大招和武器显示为原子项列表。
4. 部件矩阵：行是槽位，列是来源包，单元格为替换 / 保留 / 隐藏 / 贴图覆盖。
5. 原子项：每个原子项一个下拉框，选择完整来源候选。
6. 填写项目名、包名、版本、作者。
7. 预览：组件预算、组合数、最坏内存、预计大小、警告、输出包列表。
8. 生成。
9. 结果：校验报告、“导入并启用”、“打开输出目录”。

新增服务：

- `BemComposeService.cs`：调用 CLI、解析 JSON、管理临时文件。
- `BemComposeModels.cs`：计划、槽位、原子项、候选的 C# 模型。

### 9.3 已安装包来源

PC 直接使用 `BemPackage.File`。需要确认：

- 文件存在；
- 包格式可读；
- 当前平台有资源；
- 同角色或同武器。

不复制源包，直接只读打开。

### 9.4 多包输出

- 角色包写入包目录，命名 `<project>-character.bem`。
- 武器包写入包目录，命名 `<project>-weapon-<id>.bem`。
- 同时生成 `<project>.zip` bundle，包含全部输出与 `compose-report.json`。
- UI 提供“仅生成”、“生成并导入”、“生成并启用”三个动作。

## 10. Android 端实现

### 10.1 原生层

新增 `android/app/src/main/cpp/installer/compose_jni.cpp`：

```java
static native String composePlanNative(
        String[] paths,
        String[] selections,
        String optionsJson) throws IOException;

static native String composeBuildNative(
        String planJson,
        String choicesJson,
        String outputDirectory) throws IOException;

static native int composeProgressNative();
static native void cancelComposeNative();
```

实现直接调用 `PlanBemComposition` / `BuildBemComposition`。

进度用原子变量轮询，避免 JNI 回调的生命周期问题。

### 10.2 Java 层

新增：

- `BemComposer.java`：任务编排、暂存、进度、取消、结果发布。
- `BemComposePlan.java`：解析计划 JSON。
- `BemComposePage.java`，或在 `BemInstallPage` 内嵌组合对话框。

流程：

1. 从 `BemInstaller.index(context)` 读取已安装包。
2. 按 `BemOptions.targetKey` 分组。
3. 对每个来源：优先使用本地 `installed.bem`；没有本地副本时用 `FrameworkSettings.openBem(remote)` 暂存。
4. 调用 `composePlanNative`。
5. 展示普通部件槽位与原子项。
6. 调用 `composeBuildNative` 生成到 stage。
7. Android 包自动执行纹理转换。
8. 校验通过后发布到现有安装索引。
9. 清理 stage。

### 10.3 Android UI 约束

遵循 `android/AGENTS.md`：

- 不添加说明性段落、帮助提示、风险说明；
- 只保留控件标题、状态、数值和操作结果；
- 实验性功能仍放在第三方模型页统一区域；
- 保留现有配置 key 与行为。

建议布局：

- 顶部：角色选择；
- 中部：来源包多选；
- 下部：普通部件槽位列表 + 原子项下拉框；
- 底部：项目名、生成按钮、进度、取消。

小屏使用全屏 Activity 或全屏 Dialog。

### 10.4 任务与生命周期

- 组合任务独立于导入任务，避免长时间占用 `BemInstaller.worker`。
- 全局只允许一个组合任务。
- 旋转时通过 `BemComposer` 静态状态恢复进度与计划。
- 应用退到后台时任务继续，但只在页面可见时刷新 UI。
- 取消后删除 stage，不触碰已安装包。

### 10.5 存储与发布

- stage：`filesDir/bem-compose/stage-<uuid>/`
- 输出：`stage/character.bem`、`stage/weapon-<id>.bem`
- 报告：`stage/compose-report.json`
- bundle：`stage/bundle.zip`
- 发布：沿用 `BemInstaller` 的 generation、索引、`FrameworkSettings.publishBem`。
- 磁盘检查：至少需要 `预计输出 × 2.2` 的可用空间。

### 10.6 包来源

Android 必须支持两种来源：

1. 本地 `installed.bem`；
2. framework remote file。

用户可能关闭“保留本地副本”，所以不能假设 `installed.bem` 一定存在。

暂存策略：

- 一次组合任务内，每个来源只暂存一次；
- 多个来源共用同一 stage；
- 任务结束或取消时统一清理；
- 不修改 framework 中的原始 remote file。

### 10.7 多包发布

- 每个输出包独立生成 generation 并独立发布。
- 发布失败时不影响已经成功的包，并报告失败项。
- 角色包与武器包属于不同资源目标，可以同时启用。
- 同一武器的重复包仍按现有冲突规则处理。

### 10.8 Android 1.4 的额外约束

Android 与 Windows 的 1.4 资源合同不同：

- Android 普通角色通常使用 LOD1 接收器，不能拿 Windows 的 LOD0 donor 组件表当同一份合同；
- 大招 prefab 的 Android 资源根和 LOD 可能与 Windows 不同；
- 武器的 Android 资源可能存在 `android_unavailable` 标记，此时只能生成 Windows 包；
- `BemOptions.supportsAndroid` 已经实现了这套判定，组合计划必须复用同样的规则。

因此 Android 上：

- 来源包必须全部声明 `android-arm64` 资源；
- 原子项必须按 Android 资源根集合重新分组，不能直接沿用 Windows 的原子项；
- 生成前先用 `BemOptions.requireAndroid` 等价逻辑校验；
- 没有 Android 资源的武器直接排除，并显示在计划中。

### 10.9 Android 与 PC 的计划不互通

计划 JSON 包含：

- 平台；
- 源包 ID；
- 资源 ID；
- 组件编号；
- 输出包列表。

这些在 PC 与 Android 上可能不同。计划文件只用于同一端的旋转恢复和任务恢复，不能跨端导入。

跨端只共享：

- 组合算法；
- manifest 结构；
- 校验规则。

不共享：

- 已解析的计划 JSON；
- 暂存路径；
- 安装索引。

## 11. 校验与预算

### 11.1 结构校验

每次构建后必须通过：

- header / directory / payload 完整性；
- manifest 结构和字段上限；
- target 部件唯一匹配；
- mesh 流、索引、骨骼、权重；
- 材质槽和材质名；
- 1.4 资源与 donor 作用域；
- 1.5 驱动器与材质覆盖。

复用现有 `ReadBemPackageInfo` 与 `LoadBem`，不要新写一套校验器。

### 11.2 组合空间

组合器生成的规则是“每个槽位一个组、每个候选一个 `eq`”，因此：

- 符号校验可以在不枚举全部组合的情况下证明“每个部件恰好一个候选”；
- 预算按最坏合法组合计算；
- 内存、贴图数量按条件同时生效的资源计算，互斥绘制段和贴图槽候选不能直接累加；同一资源中的相同贴图去重。容量证明触及复杂度保护时，明确提示“未能证明”，不能误报实际超限。
- 临时预览只校验当前选择；最终保存保留符号预算及各可达候选的加载校验。输出 BEM1.2+ 的驻留预算与运行时一致，为 1536 MiB / 64 张贴图，不再额外套用 768 MiB 组合器限制。
- PC 预览导出独立使用可用物理内存的四分之一（256 MiB 至 4 GiB）作为场景预算，替代固定 192 MiB。未声明 desktop 的移动端调用仍使用原有预算。预览容量不等同于游戏运行时格式上限。
- 原子项只生成一个组，内部组件共享同一 choice，不需要额外约束。

### 11.3 抽样加载

完整 `LoadBem` 很重。建议：

- 默认选择：完整加载；
- 每个冲突槽位的每个候选：至少各加载一次；
- 每个原子项的每个候选：至少各加载一次；
- 随机 / N-wise 组合：最多 32 组；
- 其余只做 `ReadBemLoadPlan` 预算检查。

### 11.4 组件预算检查

构建前先做静态预算：

```text
普通部件组件 + sum(每个原子项的所有候选组件) <= 64
```

超过时：

- 默认切换到原子项快照；
- 重新计算并再次展示；
- 仍超限则要求用户减少来源或启用拆分输出。

### 11.5 输出包集合检查

- 每个输出包单独校验 target、资源、组件和预算。
- bundle 中的包 ID 不得重复。
- 角色包与武器包的资源根不得重叠。
- 同一 `weapon + 武器 ID` 只能有一个武器包。

## 12. 错误码与用户提示

| 错误码 | 含义 | 用户提示 |
| --- | --- | --- |
| `COMPOSE_TARGET_MISMATCH` | 角色或目标类型不同 | 只能合并同一角色/武器的包 |
| `COMPOSE_RESOURCE_MISMATCH` | 资源目标不兼容 | 当前平台资源不一致 |
| `COMPOSE_COMPONENT_IDENTITY_MISMATCH` | 部件表不一致 | 来源包对应不同游戏版本 |
| `COMPOSE_UNSUPPORTED_PLATFORM` | 包没有当前平台资源 | 该包不支持当前平台 |
| `COMPOSE_TEXTURE_FORMAT` | 手机端存在 BC 贴图 | 需要先转换手机纹理 |
| `COMPOSE_BONE_PALETTE_OVERFLOW` | 骨骼表超过 256 | 减少来源或拆分包 |
| `COMPOSE_MATERIAL_IDENTITY` | 材质名/槽不一致 | 来源包材质表不一致 |
| `COMPOSE_COMPONENT_BUDGET` | 组件超过 64 | 使用原子项快照或拆分输出 |
| `COMPOSE_ATOMIC_ROOT_MISMATCH` | 原子项资源根集合不同 | 不能作为同一原子项候选 |
| `COMPOSE_TOO_MANY_GROUPS` | 选项组超过 64 | 减少来源 |
| `COMPOSE_TOO_MANY_RULES` | 规则超过上限 | 减少来源 |
| `COMPOSE_PAYLOAD_LIMIT` | 输出超过 2 GiB | 减少来源或降低贴图 |
| `COMPOSE_MANIFEST_LIMIT` | manifest 超过 4 MiB | 减少来源或拆分 |
| `COMPOSE_DISK_SPACE` | 磁盘不足 | 清理空间 |
| `COMPOSE_DRIVER_LIMIT` | 驱动器超过 16 | 减少来源 |
| `COMPOSE_PARAMETER_LIMIT` | 参数超过 64 | 减少来源 |
| `COMPOSE_SOURCE_UNREADABLE` | 源包无法读取 | 重新安装源包 |
| `COMPOSE_VALIDATION_FAILED` | 生成包校验失败 | 查看报告 |
| `COMPOSE_PUBLISH_PARTIAL` | 部分输出包发布失败 | 查看失败项并重试 |

## 13. 测试计划

### 13.1 单元测试

- 槽位键与冲突检测；
- 原子项判定与资源根分组；
- donor 闭包；
- 选项组生成（普通部件 + 原子项）；
- payload 重映射；
- 骨骼/材质 component 重映射；
- 参数与驱动器重命名；
- 版本选择；
- 资源 ID 重分配；
- 多包输出与 package_id 生成。

### 13.2 固定样本

1. 1.0 单外观；
2. 1.0 多外观；
3. 1.1 选项组；
4. 1.2 贴图槽；
5. 1.3 形变参数；
6. 1.4 普通角色多资源；
7. 1.4 大招（ultimate + ability + mirror）；
8. 1.4 静态武器；
9. 1.5 材质覆盖 + 驱动器；
10. ZIP 容器；
11. Android ASTC 包；
12. Android BC 包（应要求转换）；
13. 角色 + 大招 + 武器的多包项目。

### 13.3 属性测试

随机选择来源与候选，验证：

- 每个槽位恰好一个候选；
- 每个原子项恰好一个候选；
- 所有 payload 引用存在；
- 所有 component 引用存在；
- 没有跨资源 donor；
- 输出包数量与目标身份一致；
- 输出大小与预算不超限。

### 13.4 双端回归

- Windows：`BemValidate` + 游戏内加载普通角色、大招、武器；
- Android：NDK 构建 + host test + 真机导入与加载；
- 取消、旋转、后台、磁盘不足；
- 多包部分失败；
- 源包只读校验。

## 14. 实施阶段

### Phase 0：接口与样本（3–5 天）

- 冻结 `bem_compose.h` API；
- 抽取 `BemPackageReader`；
- 准备 1.0–1.5、ZIP、大招、武器样本；
- 定义计划/选择/报告的 JSON schema；
- 明确原子项判定规则。

### Phase 1：共享核心（2–2.5 周）

- 1.0–1.3 快照模式；
- 细粒度槽位、donor 闭包、规则生成；
- payload 重映射与二进制写入；
- `BemCompose.exe` CLI；
- 单元测试与 `BemValidate` 回归。

### Phase 2：PC GUI（1.5 周）

- `BemComposeService`；
- `BemComposeWindow`；
- 部件矩阵 + 原子项列表；
- 组件预算与冲突提示；
- 生成后导入/启用。

### Phase 3：Android 原生（2 周）

- `compose_jni.cpp`；
- `BemComposer` 任务与暂存；
- Java 组合页；
- 进度、取消、旋转恢复；
- 多包发布到现有安装索引。

### Phase 4：1.4 原子项与多包（1.5–2 周）

- 大招原子项；
- 武器原子项与独立输出包；
- 多包 bundle；
- 组件预算与原子项快照降级；
- 双平台资源包（可选）。

### Phase 5：硬化与发布（1 周）

- 大包、低内存、磁盘不足；
- 本地化；
- 文档与更新日志；
- PC / Android 实机验收。

单人总工期约 **9–11 周**。PC GUI 与 Android UI 可并行，核心完成后可压缩到 **7–9 周**。

## 15. 风险与对策

| 风险 | 影响 | 对策 |
| --- | --- | --- |
| 来源包游戏版本不同 | 部件表/骨骼表不一致 | 强校验，直接拒绝 |
| 部件互相依赖 | 混搭后接缝或缺件 | 同源锁定、警告、原子组 |
| 骨骼 donor 漏收集 | 运行时索引越界 | donor 闭包算法 + 属性测试 |
| 材质槽不一致 | 材质错位 | 逐槽比对，拒绝 |
| 原子项误拆 | 大招缺 prefab | 无法确定时按原子项处理 |
| 原子项资源根不同 | 拼出不完整大招 | 根集合必须一致，缺失用 keep 补齐并警告 |
| 组件超过 64 | 1.4 包无法生成 | 原子项快照或拆分输出 |
| Android BC 贴图 | 花屏或加载失败 | 生成后自动转换 |
| 组合槽位过多 | 超过 64 组/4096 规则 | 减少来源 |
| 大包内存峰值 | Android OOM | 流式 spool，O(单 payload) |
| 磁盘空间不足 | 写入中断 | 预检查 + 失败清理 |
| 1.5 驱动器冲突 | ID 重复/超限 | 命名空间 + 上限检查 |
| 多包发布部分失败 | 角色可用武器不可用 | 独立发布 + 部分失败报告 |
| 双端资源差异 | 一端可用一端不可用 | MVP 按平台分别生成 |

## 16. 验收标准

1. PC 与 Android 都能从已安装包创建组合包，不依赖另一端。
2. 两个同角色、同版本的来源包可以合并，生成包通过 `BemValidate`。
3. 每个普通槽位在任意合法组合下恰好一个候选。
4. 每个原子项在任意合法组合下恰好一个候选。
5. 普通角色 + 大招输出同一个 1.4 `character` 包。
6. 武器输出独立 1.4 `weapon` 包，并可通过 ZIP bundle 一起交付。
7. 组合包在 PC 与 Android 实机加载，普通角色、大招、武器显示正确。
8. 源包不被修改；取消与失败不留下半成品。
9. 手机端全程使用原生 C++ 组合核心，Java 只做 UI/任务/安装。
10. 组件超过 64 时自动使用原子项快照，并明确提示。
11. 输出超过限制、版本不一致、贴图格式不兼容时有明确错误。
12. 组合包可以在游戏内继续切换普通部件与原子项，不需要重新导入。
13. 组合报告包含来源、选择、警告、大小与输出包列表，不包含产物哈希。
14. 现有单包导入、选项、热切换、纹理转换行为不回归。

## 17. 已确认的产品决策

1. **默认模式**：快照模式。源包内部选项在组合时解析为固定状态。
2. **默认候选**：复现第一个来源的状态。
3. **同源锁定**：默认关闭。
4. **Android 纹理转换**：生成后自动转换。
5. **输出位置**：PC 默认写入包目录；Android 默认导入并保留本地副本。
6. **1.4 组合模型**：
   - 普通角色按细粒度部件槽位处理；
   - 大招作为原子可组合项，与普通角色同属 `character + 角色 ID`，放在同一个 1.4 角色包内；
   - 武器作为原子可组合项，属于 `weapon + 武器 ID`，单独输出 1.4 武器包；
   - 最终仍输出 1.4 包；涉及武器时输出一组 1.4 包，以 ZIP bundle 交付。
7. **平台**：MVP 按平台分别生成 1.4 包；单文件双端 1.4 包后置。
8. **组件超限**：默认使用原子项快照，保留普通部件自由组合。

## 18. 设计结论

你的 1.4 设计是可行的，而且比“所有东西都细粒度拆分”更稳健：

- 普通角色保持最大自由度；
- 大招按资源根集合整体替换，避免只替换其中一个 prefab 导致表现不完整；
- 武器遵守 1.4 的 `weapon` 包级身份，不强行塞进角色包；
- 最终产物仍然是标准 1.4 包，现有运行时、安装器和校验器都不需要改；
- 多包通过现有 ZIP bundle 机制交付，不引入新格式。

唯一需要接受的产品事实是：**一次组合可能产出多个 1.4 包**，而不是永远只有一个文件。

### PC 选项分组界面（2026-10-27）

- 细分操作行按源包选项依赖集合分组，直接使用源包选项名，同组绘制变体一起启用、停用和高亮。
- 对所有记录都生效的共用开关或配色单独显示；不反复拼在每个具体部件名称中。无具体开关的内容显示为固定部分。
- 多个具体选项共同控制的记录只归入一个联动项，不能重复分配到多个独立开关。贴图槽条件也参与依赖识别。
- 这是操作分组，底层原包条件、命名空间和跨部件选项联动保持不变；原包选项仍在预览内切换。

- 选项项可展开选择原始绘制段；组选项行和细分行均有直接启用/停用按钮，复选框只承担批量选择。部分启用项的直接按钮会启用全部细分。

### PC 身体区域粗分与人工修正（2026-10-27）

- 对 body 接收器按三角形三个顶点的骨骼权重总和粗分为颈、头、躯干、骨盆、左右臂、左右腿。未知骨骼和退化索引三角形保留在未分类区域，不猜测或丢弃。
- 只切分索引；坐标、UV、材质、蒙皮和源包条件保持原样。区域可独立选来源，缺失来源禁选，并显示实际三角形数量，不承诺区域完整或跨包无缝。
- 修正模式仅显示当前 body 并按区域着色；Ctrl 点击最近可见三角形，将其归入目标区域，普通拖动保留旋转和平移。支持撤销及恢复自动分区。
- 点击映射使用当前场景 ID、原始来源绘制记录和三角形编号；主机校验来源和编号。原始选择与修正统一保存，预览高亮不能误保存。
- 手工修正改变区域归属及该区域的选源范围，不生成新几何，不自动缝合。再次组合已生成的包时，保留其中已保存的区域标签及原有候选配置。
- 不引入额外运行时或发布依赖。该编辑入口仅在 PC 组合窗口提供，移动端默认预览行为保持原样。
