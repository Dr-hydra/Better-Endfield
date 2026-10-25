# 小白全自动转换的可行性评估

> 问题：只靠 skill，agent 能否对任意（含不规范）源包独立完成"输入 → .bem"，中途只在开头问一次（制作/转换、水平、参与度）？
> 证据：`F:\zmd_bem` 551 包账本 + 161 个逐包构建脚本 + `tools/CustomModel` 实现 + 本轮实测
> 日期：2026-10-25

## 一、结论

**现在做不到。** 按语料实测，只有 **19.4%（107/551）** 的包能走通现有自动路线；**64.6%（356）** 需要专有装配代码，而唯一能写这种代码的引擎既没文档也不能随包分发。

开头的三问可以做到（现在只问了两问，缺"水平"）；"中途不打扰"的规则也写了。真正缺的不是交互协议，是**能力底座**。

## 二、需求逐条判定

| 要求 | 现状 | 判定 |
| --- | --- | --- |
| 开头问：制作 / 转换 | SKILL.md Turn 0 第 1 问 | ✅ 有 |
| 开头问：参与度 | Turn 0 第 2 问（全托管 / 协同） | ✅ 有 |
| 开头问：水平 | 只写了 "determine experience level"，问题里没有 | ⚠️ 缺一问 |
| 中途尽量不打扰 | SKILL.md §13 + playbook §12：全托管下禁止中途求助 | ✅ 有，但边界模糊（见 R2） |
| 脚本不一定对，agent 自行判断 | 没有任何"可信度分级"或交叉校验规则 | ❌ 缺机制 |
| 任意包 → .bem | 19.4% | ❌ 主缺口 |

### R2 的边界模糊

playbook §12 说"仅当核心主体资产（主 Diffuse、基础网格、目标角色 catalog）彻底缺失且完全不可恢复时才终止"，§13 说"严禁停下"。两条之间没有可判定的分界：什么叫"彻底不可恢复"？缺一张 NormalMap 算不算核心资产？agent 只能自己解释，结果要么过度打扰，要么静默降级。

需要一条可机器判定的规则，例如：**任一 draw 路径上的贴图缺失、且同角色全部同类包零命中 → 停；其余缺失 → 记入 `excluded_features` 继续。**

### R3 缺"脚本可能错"的机制

SKILL.md 把 6 个脚本描述为"read-only diagnostic probes"，输出被当成事实。但实测（本轮全量）：

- `bem_triage` 误报 36/502 = 7.2%，漏报 19/48 = 40%
- `bem_triage` 不报 `ENTRY_SELECTION` / `COMPANION_INI`，550 包中 23 个多入口、51 个伴随 INI
- `bem_repair` 7 项探针全部失败（见第四节）

"agent 自行判断"要求脚本输出带**可信度标注**（事实 / 启发式 / 猜测）和**交叉校验规则**，现在一律是陈述句。

## 三、缺口量化

### 3.1 语料分布（`bem_triage` 全量 551 包）

| 体检结论 | 包数 | 占比 | 现有路线 |
| --- | --- | --- | --- |
| `standard_candidate` | 107 | 19.4% | `convert` 可直接走通 |
| `requires_mapping` | 60 | 10.9% | 需审阅配方（有文档，无编写工具） |
| `manual_only` | 356 | 64.6% | 需专有装配代码（无 API 文档、引擎不可分发） |
| `unrecognized` | 27 | 4.9% | 无入口，需人工定目标 |

### 3.2 人工产线的实际投入（`F:\zmd_bem`）

| 事实 | 数量 | 含义 |
| --- | --- | --- |
| 逐包构建脚本 | 161 包（29.2%） | 近三成包必须写代码才能出包 |
| 逐包人工审阅脚本 | 95 个 | 断言"我审的是这个版本的源" |
| 最终失败 | 48（8.7%） | 专家 + 定制代码的天花板 |
| 失败中体检查不出的 | 19/48（40%） | 身份未证、跨格式机制、非角色对象 |

也就是说：**"全自动"要替代的是 161 份定制代码 + 95 份人工审阅**，不是 6 个探针。

### 3.3 分发环境缺什么

| 依赖 | 是否随包分发 | 后果 |
| --- | --- | --- |
| `bem_v1 / bem_v11 / bem_v15 / component_auto / bem_projects` | ❌ 编进 PyInstaller exe | 脚本 `import` 必失败 |
| `standard_worker.normalize_entries` | ❌ 只在 zmd_bem | playbook §13.1 骨架直接 ImportError |
| `normalize_ini.py`（440 行） | ❌ 只在 zmd_bem | 36% 的 parse_error 包没有归一化入口 |
| `task_ledger / source snapshot` | ❌ 只在 zmd_bem | 批量场景无账本、无快照、无撤回 |

`package_toolchain.py` 只复制 `skills/`、`catalog/`、`profiles/`、`examples/`、`blender_addon/`、`7zip/` 和 exe。

## 四、`bem_repair.py` 实测：确认必须重写

构造 7 组合成夹具逐项验证（脚本：`.workbuddy-ai/tmp/probe_repair.py`）：

| # | 探针 | 期望 | 实际 | 后果 |
| --- | --- | --- | --- | --- |
| T1 | `clamp-ib` 不给 `-o` | 拒绝或输出副本 | **原地覆盖输入**（602 → 300 字节） | 改坏用户源文件 |
| T2 | `first=200, count=150`，缓冲 300 索引 | 报不足 | `satisfied=True` | 越界被漏判 |
| T3 | DX10 立方体（6 slice）丢 Mip0 | 每 slice 各丢一层 | 只丢 slice0，头部声称全丢 | **文件损坏**（1492 vs 8212 字节） |
| T4 | 单 Mip 超限贴图 | 报错 | `dropped=0`，退出码 0，文件未变 | 静默失败，调用方以为已处理 |
| T5 | 两个同名不同尺寸 donor | 列冲突、不选 | 取遍历到的第一个（4 字节那份） | 换错贴图 |
| T6 | CLI 注入 colors / vectors | 支持 | 只有 `--floats` | 文档承诺的一半能力不可用 |
| T7 | `_Cutoff=500` | 拒绝 | 原样写入 | 打包时才炸，或 skip_validation 下静默通过 |

### 根因（不是 bug，是设计）

1. **没有"输入即只读"的约束**。默认写回输入，与 skill 自身"源只读"原则冲突。
2. **把 IB 当成"文件长度问题"**。真实判据是 `first + count ≤ 容量` 且缺口落在整三角形边界，函数签名里根本没有 `first`。
3. **DDS 解析只覆盖单 slice 无数组的 BC 纹理**。`array_size` 读出来就丢掉。
4. **把"没做"报成"做完了"**。`dropped=0` 仍返回成功。
5. **借用逻辑与 `bem_peer` 的 doctrine 相反**。`bem_peer` 明确"名字不同不算候选、同名不同尺寸单列冲突"，`borrow_peer_dds` 直接取第一个。
6. **CLI 面窄于函数面**。`add_material_override` 支持 colors/vectors，CLI 没接。
7. **不做任何格式校验**。写进去的值不查 `[-128,128]`、不查属性名正则、不查 `root_offset ≤ 0.2`。

结论：**同意重写。** 逐函数打补丁会保留"默认覆盖输入"这个最危险的行为。

## 五、重写规格

### 5.1 硬约束

1. **输入只读**：所有命令必须 `-o` 显式指定输出；缺省时报错退出，不提供"原地"选项。
2. **每个动作产出证据**：`{input, output, rule, before, after, evidence_level}`，`evidence_level ∈ exact | computed | inferred`。
3. **不可修复时返回非零**，并给出"缺什么才能修"，不允许静默 no-op。
4. **不替人做取舍**：拿不到 hash/尺寸证据的 donor 一律拒绝借用，只列候选。

### 5.2 命令面

| 命令 | 输入 | 输出 | 判据 |
| --- | --- | --- | --- |
| `ib-clamp` | IB + `--first` + `--count` | 新 IB + 新 count | `count ≤ (total-first)`，且 `count % 3 == 0`；四判据不满足则拒绝 |
| `ib-audit` | IB + draw 列表 | 报告 | 逐 draw 报 `first/count/容量/缺口`，不修 |
| `dds-mipdrop` | DDS + `--limit` | 新 DDS | 支持 array/cubemap（逐 slice 丢）；单 mip 超限 → 报错；重算 pitch/linear size |
| `dds-audit` | DDS | 报告 | 格式、尺寸、mip 链、是否超限，不修 |
| `donor-find` | 文件名 + 根目录 | 候选表 | 按相对路径 + 尺寸分组，冲突单列；不复制 |
| `donor-copy` | 候选 + `--sha256` 或 `--size` | 副本 | 无证据拒绝执行 |
| `override-set` | project.json + 参数 | 新 project.json | 校验正则与范围，越界报错不写 |
| `root-offset` | project.json + 值 | 新 project.json | `|t| ≤ 0.2`，校验骨骼路径存在 |

### 5.3 验收

用 `audit/complex-set.json` 的 15 个复杂包（`python audit/run_complex_set.py`，约 0.5 秒）：

- 不得修改任何源文件（前后哈希一致）
- `ib-audit` 对 M0089 / M0334 / M0207 判可修，对 M0466 判拒绝
- `dds-audit` 对 M0016 判走 1.2+（不 mipdrop），对 M0342 判不可修
- 对 M0342 / M0308 / M0488 这三个体检看不出的包，必须给出明确结论而不是沉默

全量 551 包只在发版前跑一次，日常改动用复杂集。

## 六、除 repair 之外还必须补的三件事

按投入产出排序：

1. **`convert` 改用 `bem_merge` 的语义**（约 1 个函数）。
   现在 `component_auto.prepare` 用 `efmi_source.sections()` 裸读入口 INI：遇到重复段直接抛 `duplicate INI section`（159/551 个包有重复段），不认 include、不认 `namespace =`、不去水印。诊断层与执行层读的不是同一份 INI，这是 7.2% 误报与"体检说没事、convert 说不行"的根因。

2. **低层装配 API 文档化 + 可分发**（工作量最大）。
   65% 的 `manual_only` 包要的是"从原始 vb/ib 构造 native 流 + 骨骼表 + 材质槽"的接口。现在只有 `component_auto.prepare`（标准路线）有文档；`bem_v1.Builder` 在任何 reference 里都没出现。要么把 `bem_assemble` 改成纯标准库 + 文档化，要么把 Builder 的 API 写进 reference。

3. **停止规则机器化**（小改动，收益大）。
   把 playbook §12 的七种"必须停"写成 `decisions.json` 的 schema，并让 `validate`/`pack` 检查它存在且非空。否则"自主判断"没有可审计的落点，等于允许静默猜。

## 七、能承诺什么

| 目标 | 现在 | 补齐后可达 |
| --- | --- | --- |
| 标准包自动出包 | 19% | 19%（+ 归一化后约 55%） |
| 非标准包自动出包 | ≈0 | 需专有代码，长期仍是 60–70% |
| 无法出包时给出可执行报告 | 部分（体检漏 40% 失败） | 90%+ |
| 全程零打扰 | 规则有、判据缺 | 需要 §12 机器化 |

**建议对用户的措辞**：不承诺"什么包都能做"，承诺"能做的自动做完，不能做的说清楚缺什么"。后者现在也还没做到，因为 40% 的失败体检看不出来。

## 八、落地记录（2026-10-25 第二轮）

`bem_repair.py` 从 721 行扩到 2413 行，命令面从 8 条扩到 15 条。不是"加几个函数"，
每一条都对着已有修复实现（`F:\zmd_bem\tools\normalize_ini.py`、
`F:\zmd_bem\tools\standard_worker.py`、`research/M0172_隔离复核/.../build_donor_repair.py`、
`F:\zmd_bem\tmp\g1\symbuild.py`）重写，并在真实包上跑出数字。

| 新增段 | 命令 | 对照实现 | 真实包验证 |
| --- | --- | --- | --- |
| §5 INI 重复段 | `ini-audit` / `ini-normalize` | `normalize_ini.py` 的 `effective_projection` / `normalize_text` | `伊冯-沁夏霓光` 3 个活动 INI：2 个源文件抛 `duplicate INI section`，归一化后 202 / 5 / 6 段全部通过 `efmi_source.sections()`；投影不变、幂等 |
| §6 条件贴图 | `texture-normalize` | `standard_worker.normalize_textures` | `伊冯-围裙` 21 条覆盖全部选中（环境顺序写反时全丢） |
| §7 蒙皮/顶点 | `skin-audit` / `skin-fix` / `vertex-compact` | `symbuild.py:404-451` 的调色板换算与顶点压缩 | `洁尔佩塔-侦探` 5 个 mesh：203284 顶点 / 168 骨骼调色板，权重和 0 违规；压缩按共享组判定后正确拒绝（无多余顶点） |
| §8 入口程序 | `entry-normalize` | `standard_worker.normalize_entries` | `伊冯-沁夏霓光` 10/10 降级并通过 `component_auto.validate_entry` |

**这一轮最重要的发现**：`伊冯-围裙` 在复杂集里判为**成功**，但 `entry-normalize` 指出它的
`ENTRY_PROGRAM: 分发流程超出已支持的 ComponentN 模板` 不是包的问题——包的本地分发器比
`component_auto.py:106-111` 的九行模板**多** 4 条语句：

```
$\EFMIv1\bones_count = $bones_count
$\EFMIv1\instance_count = $max_instance_count
$\EFMIv1\custom_mesh_scale = 1.00
CommandList\EFMIv1\Callback_MergedSkeleton_ConnectComponent = ref CommandList_MergedSkeleton_ConnectComponent
```

按"改包"的思路删掉这 4 条，会同时删掉合并骨架连线、实例计数与自定义网格缩放。所以第六节
第 1 条（"`convert` 改用 `bem_merge` 的语义"）之外还要加一条：**`validate_entry` 的九行模板
需要跟上 EFMIv1 运行时**，否则越新的包越会被判成"不支持"。

同包第 11 个入口声明 `$\EFMIv1\gpu_posed = 0`，属于 `ENTRY_CPU_POSED_COMPONENT`——draw 留在
原骨架里，BEM 无法重新指向，也不能靠改写解决。

**验收**：`python audit/test_bem_repair.py` 53 用例通过（原 19 条），
`python audit/run_complex_set.py` 15 包 / 519 ms，判定结果与基线一致。

`伊冯-沁夏霓光` 三段串联实测（源文件本身过不了解析门）：

| 阶段 | 结果 |
| --- | --- |
| 源 | `efmi_source.sections()` 抛 `duplicate INI section: Constants` |
| `ini-normalize` | 206 → 202 段，投影不变、幂等 |
| `texture-normalize` | 22 条覆盖全部选中，0 拒绝 |
| `entry-normalize` | 10 个入口全部降级，`validate_entry` 复检通过 |
| 产物 | `efmi_source.sections()` 解析出 207 段 |

顺带修掉两个"看起来像包的问题、其实是工具的问题"：变量名大小写（`$SocksColor` vs
`$sockscolor`，改正前 5 条贴图段被误拒）与倒卖水印（横幅行被当成不支持的语句）。

**仍未落地**：`TEXTURE_MAPPING` 冲突、`CHARACTER_CATALOG` 未覆盖的 4 个包、合并骨架（stride
8/16 → 12/32）的换算、CrossIB 几何分组、ShapeKey / `material_overrides` / `option_groups`
的构造。这些属于第六节第 2 条（低层装配 API 文档化），不是 repair 的职责。

### 8.1 第三轮落地：审计面补齐（同日）

上一轮留下的"仍未落地"里，凡属于**审计**而非**构造**的，这一轮全部补上了。
`bem_repair.py` 2413 → 3982 行，命令面 15 → 21 条，测试 53 → 128 条。

| 新增段 | 命令 | 对照实现 | 判定依据 |
| --- | --- | --- | --- |
| §9 合并骨架 | `skin-convert` | `F:\zmd_bem\tmp\g1\symbuild.py:395-451` 的 `merged_palette` 换算 | 调色板按首次使用顺序；写前逐顶点核对 `(权重, 骨骼名)`；5 个拒绝码 |
| §10 贴图覆盖 | `texture-audit` | `component_auto.texture_table:125-166` | 复现该函数会抛的那条错误（`auto_error`）；只有重复 hash 是阻断 |
| §11 角色资料 | `catalog-gap` | `component_auto.match_catalog:27-54` | 五种 verdict；识别四种入口族，指出哪两种没有读取器 |
| §12 形态参数 | `deformation-audit` | `bem_v13.validate_manifest:31-83` + `decode_deltas:97-121` | 汇总全部违规（不是遇错即抛），并真解一遍 `4+J+16N` |
| §13 选项空间 | `option-audit` | `bem_v11.selection_plan:303-420` | 枚举合法组合；256 上限**按单网格**判，不是求和 |
| §14 CrossIB | `ib-groups` | `bem_triage.index_overruns:420-480` 的 CrossIB 跳过规则 | 按实际索引字节分组；只有 `index_size_conflicts` 是错误 |

**这一轮最重要的发现**：`catalog-gap` 在复杂集 15 包上跑出来的分布，解释了第三节
"356/551 判 manual_only"里相当大的一部分：

| verdict | 包数 | 代表 | 含义 |
| --- | --- | --- | --- |
| `matched` | 3 | M0012 / M0495 / M0488 | 恰好一个资料全覆盖，可自动转换 |
| `unknown-identity` | 10 | M0019（10/10 缺）、M0334（9/9 缺）、M0089（10 缺 1） | 缺口身份在 36 份资料里一个都找不到 |
| `not-required` | 2 | M0001（`bare-hash`）、M0342（`mixed`） | 入口自带 index count，降低流程不查资料 |

即：真正"资料缺身份"的是 10 个。另外两个被**入口族**挡住，不是数据问题：

- `M0001` 入口是 `TextureOverride_<hash>_<count>_<first>`（`bare-hash`）；
- `M0541` / `M0016` 入口是 `TextureOverride_IB_<hash>_<label>_ComponentN`（`ib-component`，
  其中名字里的 8 位十六进制是**资源组标签**，真正的绘制身份是段体里的 `hash =`）。

`source_classification.entry_family` 只认 `ComponentN` 与 `LODn.<hash>`，`bem_tool` 对这两类
直接报 `ENTRY_SELECTION`（找到 0 个入口）。这条要并进第六节第 1 条：**入口族的识别面需要扩展**，
否则这两类包永远进不了自动流程。

`option-audit` 顺带纠正了一个容易写错的判定：`bem_v11.py:392-394` 的 256 上限是对
**每个被选中的 mesh** 单独做 `logic.maximum`，不是各部件 draw 数相加。按"相加"判会把
"两个各 200 draw 的网格（永不同时出现）"误判成超限。

**验收**：`python audit/test_bem_repair.py` 128 用例通过；
`python audit/run_complex_set.py` 15 包 / 578 ms，判定结果与基线一致。

**仍未落地**（全部属于"构造"，不属于"审计"）：`material_overrides` 的批量生成、
ShapeKey 源数据的提取（EFMI ShapeKey → `parameters`/`mesh_deformations` 的自动映射）、
`option_groups` 从按键循环自动推导。这三项仍依赖第六节第 2 条的低层装配 API 文档化。

> 第四轮修正：ShapeKey 这一项已落地（见 8.2），`material_overrides` 批量生成按作者口径**不再做**
> ——它不是缺口，是不该自动化。原文"这三项"里的前两项需要按下文重新理解。

## 8.2 第四轮：口径修正与滑条落地

### 8.2.1 两处口径纠正

前一版审计把 `F:\zmd_bem` 的处理方式写成"官方产线主动不做"。这是错的：两个仓库都是作者本人的
项目，不存在第三方"官方"。真实原因是**时间线**——转换仓库写在 BEM 1.2 期间，1.3 的
`parameters`/`mesh_deformations` 和 1.5 的 draw 级 `material_overrides` 当时还不存在。

实测证据：

| 检查项 | 结果 |
|---|---|
| 仓库自带 CustomModel 副本 | 只有 `bem_v1.py` / `bem_v11.py`，**没有 v12/v13/v15** |
| `mesh_deformations` 出现次数 | **0** |
| `disable_keywords` / `enable_keywords` | **0**（draw 级材质覆盖从未写过） |
| `material_overrides` | 456 处，**全部**是 keep 形态（`material_slot`/`material_name`/`textures`） |
| ShapeKey 处理方式 | 烘焙（`shape_defaults()` + `bake_position()` + 删 `CommandListSetShapeKey` 调用） |

直接后果：这三项**没有可参考的既有实现**。前几轮"参考已有修复实现"的路子对审计类命令成立，对
构造类不成立——仓库里没有对应的写入代码可以抄。

### 8.2.2 滑条能力：`shape-params`

已落地。关键判断是**不要烘焙**：源缓冲区存的是权重 1 的完整偏移，Unity 按权重线性混合，
`[Constants]` 那个值只是起始点。所以输出两帧 0/1000 覆盖整条滑条，`default` 取源常量。

实现上没有新写解码/编码，而是复用了既有且已验证的两段：`efmi_shapes.decode_binding`（读官方
缓冲）+ `bem_v13.apply_author_spec`（写载荷并校验）。缺的只是"发现哪些 (部件, 键, 权重) 被驱动"
这一步，现在由 `bem_repair.py shape-params` 完成，输出 `kind: bem-position-morphs` 配置。

**端到端验收**（`.workbuddy-ai/tmp/check_shape_chain.py`，用带 `zstandard` 的
`toolchains/python/bem-build` 跑）：

- `shape-params` 生成 1 参数 / 1 通道，`default=500`（源权重 0.5）；
- `bem_v13.apply_author_spec` 接受该配置，载荷 4 → 5，能力位自动补
  `body-parameters` + `mesh-position-deltas`；
- 非中性帧解码出 2 条记录，fp16 `0.1` 解成 `0.09997558`（符合 r16 精度）；
- `morph_streams` 在 tick 1000 把顶点 2 位移 `(0.1, 0, 0)`，tick 0 保持原位。

一个被端到端测试抓到的真实缺陷：`load_project` 返回的是**整个文件**，项目文件外面还包了一层
`manifest`。初版直接把它当 manifest 用，于是 `target`/`appearances` 全读不到，结果"0 参数、
0 阻断"——看起来成功，实际什么都没生成。已修，并补了 `wrapped=True` 的回归用例。

**真实包验证**（M0265 提弗洛斯-旗袍，台账 `进度/tasks.json` 有 `source_path`，源里确实有形态键）：

- 缓冲区结构全部读通：C4 `batches=1 / ids=46944 / offsets=281664 / 最高顶点=59967`，
  C8 `batches=1 / ids=336234 / offsets=2017404 / 最高顶点=207168`；`offsets == ids×6` 在两组上都成立。
- 但**没有任何调用方驱动它们**：`[Constants]` 里只有 `run = CommandListInitializeShapeKeys`，
  `$shapekey_id`/`$shapekey_value` 恒为 0，`run = CommandListSetShapeKey` 只出现在注释里。
  这正是转换仓库当年记下的"6 个 GUI 形态滑条按源默认 0 保留基础 VB0"。
- 初版对这种情况报 `SHAPE_DATA_ABSENT`（"没有资源声明"），是错的——声明明明齐全。已拆成独立码
  `SHAPE_NOT_DRIVEN`，并把带缓冲区的部件列出来，提示"先在源里确认哪一处真正驱动它们"。
- 15 包复杂集里**没有**任何一包带形态键，所以真实样本只有这一个，且它是死滑条；活滑条的端到端
  验证只能靠合成夹具。这一点必须记住，不能拿"复杂集跑过"当活滑条已被真实数据覆盖的证据。

### 8.2.3 不做自动化：draw 级材质覆盖

按作者口径，BEM 1.5 的 draw 级 `material_overrides`（`floats`/`colors`/`vectors`/
`disable_keywords`/`enable_keywords`）**不提供自动化方式**。理由充分：生成它需要逐属性判断
（哪个属性、什么范围、哪个 keyword 真的改变外观），这不是脚本能替人做的决定，也不是给小白的功能。

但**不是禁止**。边界写成两层：

- 默认不提供、不宣传成支持的能力，agent 不应主动编批量循环；
- 用户明确要求时，由 agent 自己想办法：`override-set` 一次写一个候选并走完整 `bem_v15` 校验，
  名字对不对查 `catalog/material-attributes.json`，最后如实告知这是手工产物、不是受支持路径。

`SKILL.md` 已按这个边界改写（原文是"do not offer it"，过于绝对）。

### 8.2.4 材质属性名字典

从 32 个角色 dump（747 个 Material / 9 个 shader）提取属性名并集，落成
`catalog/material-attributes.json`：

| 内容 | 条目 | 体积 |
|---|---|---|
| `floats`（可覆盖） | 1195 | 合计 **39.5 KiB** |
| `floats_not_writable` | 2 | （`InteriorParallaxMode`、`stretchByVelAmount`，不符合 `^_[a-zA-Z0-9_]{1,95}$`） |
| `colors` | 191 | |
| `keywords` | 37 | |
| `textures` | 82 | |

**只发名字，不发默认值。** 实测 1197 个 float 属性里 300 个在不同材质里取不同值
（`_Smoothness` 51 种、`_AnisotropyValue` 31 种），所以"属性的默认值"不存在，默认值属于材质。
per-material 全量表要 3.4 MiB（gzip 210 KiB），而每个材质平均 166.5 个 float、`bem_v15` 单
candidate 只允许 32 个——约八成只能看不能写，不值得预置。

也不按 shader 分组：只有 184/1197 只属于一个 shader，而 BEM 清单本身不记录 draw 用哪个 shader，
分组校验在验证阶段根本做不了。

### 8.2.5 工具链体积与平台约束

用户有游戏本体时，现场 dump 需要的工具：

| 组件 | 体积 | 平台 |
|---|---|---|
| `toolchains/EndfieldUnpacker` | 47 MB | Python，需读游戏 VFS |
| `toolchains/native-asset-reader` | 14 MB | **Windows only**（`net8.0-windows` + 原生 `Texture2DDecoderWrapper`） |
| `toolchains/FkArkEnd`（ResConv） | 2.0 MB | **Windows only**（.NET 10） |
| 合计 | ≈63 MB | 不含游戏本体与 .NET 8 运行时 |

`NativeAssetReader` 是 framework-dependent，用户还要装 .NET 8 Desktop Runtime。其中对象图必需的
只有 2.3 MiB，另外 3.3 MiB 是 `SixLabors.ImageSharp`/`Fonts` + 贴图解码器，只为导出贴图。

**远端与仓库的真实分工**（这一节原先把"不在 release 里"误写成"不在仓库里"，此处更正）：

| 东西 | 在 release | 在仓库 | 说明 |
|---|---|---|---|
| GUI / CLI / catalog / profiles / skills | ✅ `BEM-Tools-1.6.2-win-x64.zip` | ✅ 源码 | 唯一受支持的下载 |
| `NativeAssetReader` 源码 | ❌ | ✅ `tools/CustomModel/NativeAssetReader/`（csproj + 2 个 .cs + README，已跟踪） | 可构建，但要外部后端 |
| 三个驱动脚本 | ❌ | ✅ `extract_native_bundles.py` / `parse_native_models.py` / `prepare_native_backend.py`（合计约 26 KB） | 只负责驱动，不含本体 |
| 构建步骤 | ❌ | ✅ `docs/custom_model/CUSTOM_MODEL_NATIVE_PARSER.md` | 含 `-p:AnimeStudioDir` 等完整命令 |
| AnimeStudio 后端 | ❌ | ❌ | 上游 `Escartem/AnimeStudio`（fork 链 `Perfare/AssetStudio` → `RazTools/Studio` → 此），MIT 谱系，Windows x64，**仍在** |
| `AnimeStudio.Endfield`（VFS 插件） | ❌ | ❌ | 随 EndfieldUnpacker 一起，**已下架** |
| `EndfieldUnpacker` | ❌ | ❌ | 上游 `endGuaGua/EndfieldUnpacker`，MIT © 2026，**2026-10-01 被 DMCA 下架** |
| `ResConv`（FkArkEnd） | ❌ | ❌ | 上游 `shiikwi/FkArkEnd`，32 star，**无 LICENSE**，README 仅 "FOR LEARNING PURPOSES ONLY"，无 release |

所以"干净 clone 能不能自己 dump"的答案是**不能**：能重建的只有 reader 本身，后端、解包器、
ResConv 都得从本机既有安装供进来。

**不要写自动 fetch 脚本**，这不是懒，是三条硬约束：

1. `EndfieldUnpacker` 上游已被权利人（终末地版权方，理由：绕过 VFS 加密）下架，页面返回
   "unavailable due to DMCA takedown"；钉它的脚本现在就是坏的，fork 也可能被同样处理。
2. `FkArkEnd` 没有 LICENSE 文件，只有 "FOR LEARNING PURPOSES ONLY"，默认保留全部权利——既不能随
   发行包分发，也不能在文档里当可再分发依赖。
3. `AnimeStudio` 有 MIT 谱系可再分发，但它单独认不出终末地的 VFS，真正起作用的
   `AnimeStudio.Endfield` 插件随 EndfieldUnpacker 一起没了。

结论：文档里写清上游名字和状态、让用户自备，是唯一稳妥做法。这与仓库既有的
"外部工具/游戏资产不纳入仓库"立场一致。

**平台**：reader 目标 `net8.0-windows` 且加载原生 `Texture2DDecoderWrapper`，`ResConv` 是 .NET 10
Windows 二进制，所以 dump 链路**不能在 Android 上跑**。手机端只能吃随包分发的
`catalog/material-attributes.json`，dump 必须在 Windows 上带游戏本体做完再带走。

**验收**：`audit/test_bem_repair.py` 140 用例通过（用 `toolchains/python/bem-build` 跑；
托管 Python 缺 `zstandard`，会有 3 个预存在的导入错误，与本次改动无关）；
`audit/run_complex_set.py` 15 包 / 1101 ms，判定与基线一致。

**仍未落地**：`option_groups` 从按键循环自动推导。它同样没有既有实现可参考，且需要从源 INI 的
变量循环还原条件图——属于第六节第 2 条的低层装配 API 范畴，本轮不动。

