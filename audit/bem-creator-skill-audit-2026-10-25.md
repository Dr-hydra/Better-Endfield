# bem-creator skill 全量审核结论

> 对象：`F:\Better Endfield\worktree\tools\CustomModel\skills\bem-creator`（11 篇文档 + 8 个脚本）
> 基准：`F:\zmd_bem`（551 包账本、九道关口实现、能力评估）与 `tools/CustomModel`（CLI 1.6.2 实现）
> 日期：2026-10-25

## 一、结论

**不能声称"全链路覆盖"。**

成立的部分：标准 ComponentN 单态包的机械链路（inspect → triage → convert → validate → unpack → pack → bundle）真实可用，且可复现。
不成立的部分有三条断链：

1. 非规范包自愈分支在分发环境不可执行；
2. 批量产线层只有只读审计，没有写入端；
3. 上限与能力表述有 3 处与代码冲突，直接决定"能不能做"的判断。

## 二、可复现的正面结论

对 551 个参考包全量运行 `bem_triage.py`（worktree 版）：

| 指标 | 实测 | 文档声称 |
| --- | --- | --- |
| 崩溃 | 0 | 0 |
| 成功包误报 | 36 / 502 = 7.2% | 36 / 503 = 7.2% |
| 失败包检出 | 29 / 48 | 29 / 48 |

诊断层不是纸面声明，数字对得上。`bem_merge.py` 还新增了 `namespace =` 指令解析，`bem_triage.py` 新增了 `Resource-<hash>-` 身份锚定（补上了 supplement README 自认的已知缺口）。

## 三、缺陷清单

### P0-1 脚本职责声明与实际内容相反

SKILL.md 第 21 行：scripts/ 下是**只读探针**，"never modify source files, copy assets, clamp buffers, or generate projects"。

实际 `scripts/` 有 8 个文件，其中两个未在 SKILL.md 出现：

- `bem_assemble.py`（545 行）：生成工程、裁剪 IB 绘制数、删除全局雨雪段、改写 Constants。
- `bem_repair.py`（469 行）：裁剪 IB、剥离 Mip0、**复制 DDS**、改写 project.json。

`bem_repair.py` 的 `clamp-ib` / `drop-mip` 在**不给 `-o` 时默认覆盖输入文件**（`out_target = ib_path`）。这与 skill 自身的"源只读"原则、以及 zmd_bem `AGENTS.md` 的"F:\zmd 保持不变"直接冲突。

### P0-2 自愈分支在分发环境不可执行

- `bem_assemble.py` 依赖 `bem_v1 / bem_v11 / bem_v15 / component_auto / bem_projects / convert_efmi_poc / efmi_source`，按 `SCRIPTS_DIR.parents[2]` 找模块。
- `package_toolchain.py` 只把 `skills/` 与 PyInstaller `--onedir` 的 exe 复制进发行包，不复制任何 `.py` 模块。
- playbook §13.1 的"标准骨架"直接写死 `F:/Better Endfield/worktree/tools/CustomModel` 与 `F:/zmd_bem/tools`（`standard_worker`），后者在本机之外不存在。

结果：SKILL.md 把"convert 失败时写专有脚本"定为核心职责，而这条分支在用户机器上无法运行，只能重写。

### P0-3 draw 上限表述错误

| 位置 | 说法 | 代码事实 |
| --- | --- | --- |
| SKILL.md:47 | max draws is 4096 | 候选 4096；**每次选择 ≤256**（`bem_v11.py:394`，`bem.cpp:1129`） |
| 格式规范 §7 | 每部件最多 256 条 | 1.2+ 候选 4096 |
| 格式规范 §11 | 每部件 draw 256，各版本相同 | 1.0 为 256，1.1+ 为 512/4096 候选 |

后果：`BEM能力对照评估` 里 M0308"1551 draw → 4096 已解除"的结论只在"同时选中不超过 256 条"时成立，而文档没有这条约束。照文档做出来的包能打包，实机会被 `Selected draw limit exceeded` 拒绝。

### P1-1 仍在教人删动画

`BEM_SOURCE_MOD_CONVERSION.md` §4：`低模 LOD、动画 | 不转换`。
与 SKILL.md（1.3 参数 + 1.5 driver）、playbook §8.3（可近似）冲突。评估文档点名警告过这类残留"比信息缺失更糟"。Shader 那一行已按建议改写，动画这一行没改。

### P1-2 `skip_validation` 的"自动钳制"不存在

格式规范 §12.4 与 SKILL.md 第 133 行：超界浮点/颜色"自动饱和截断（Clamp）"。
实际 `bem_v15.py:81-84` 与 `material_overrides.h:41` 只是**跳过范围校验**，不 clamp。作者写 `_Cutoff: 500` 会通过校验并原样下传。

### P1-3 借用逻辑违背自身证据纪律

playbook §13.3 与 `bem_repair.borrow_peer_dds` 都取第一个同名候选（`candidates[0]`），只按 basename 匹配、忽略子目录。
而 `bem_peer.py` 的设计原则是"名字不同的不算候选、同名不同尺寸单列冲突、不猜身份"。文档把最危险的"换错贴图"路径写成了标准模式。

### P1-4 体检漏两个关口

CLI 有 `ENTRY_SELECTION`（必须恰好 1 个入口）与 `COMPANION_INI`（伴随 INI 需专项审阅）两道否决；`bem_triage.py` 只报 `active_ini_count`，不判阻断。

全量 550 包：23 个多入口、51 个伴随 INI；其中 3 个仍判 `standard_candidate` 且输出 "No mechanical blocker found"（庄方宜-心灵+大招形态、萤石-巴西、陈千语-赤脚）。

### P2-1 写包回读未落地

playbook §9.3 把"临时→读回→逐字节相等→原子替换→再读回"列为必须。
`bem_v1.atomic_write` 只有 temp→replace；`write_package` 写完即返回，生产路径无回读（只有测试里有）。CLI 产出的包没有这层保护。
另：§9.3 示例用 `merged.manifest`，实际属性是 `builder.m`。

### P2-2 版本号自相矛盾

SKILL.md 开头写 "BEM 1.5 (BEM Tools 1.6.2)"，后文又出现 "BEM Tools 1.5.4 … supports BEM 1.0–1.4"、"older 1.5.0 lacks 1.4"、"1.5.3/1.5.4 target refresh"。`--version` 实际输出 `1.6.2 / BEM 1.0+…+1.5`。

### P2-3 命令漏项

CLI 有 `pack-zip`（支持 `--preview`），SKILL.md 的"Choose the operation"未列。

### P2-4 小项

- §9.1 标题"双端离线校验"，内容无平台维度。
- `bem_assemble.auto_repair_draw_counts` 只按文件名猜 `Meshes/ComponentN_IB.buf`，不读 `ib = ref` 绑定；且裁剪时忽略 draw 的 `first` 偏移，非零 first 的越界会漏判。

## 四、与 zmd_bem 九道关口的覆盖对照

| 关口 | skill 覆盖 |
| --- | --- |
| 1 产物不覆盖 | 部分（只挡 output==source） |
| 2 单入口 | 仅 CLI，triage 不报 |
| 3 分类 | 覆盖 |
| 4 伴随 INI | 仅 CLI，triage 不报 |
| 5 状态 BFS | 覆盖（`bem_state_probe`） |
| 6 身份匹配 | 覆盖 |
| 7 结构归一 | 覆盖 |
| 8 外观合并 + 64 上限 | 覆盖 |
| 9 原子写 + 逐字节回读 | 只有原子写 |
| 账本 / 快照 / 隔离候选 / 撤回 | 只有只读审计，无写入端 |

## 五、建议优先级

1. 把 `bem_assemble.py`、`bem_repair.py` 写进 SKILL.md，并把 `bem_repair` 默认行为改成"必须显式 `-o`，绝不覆盖输入"。
2. 统一 draw 上限口径：**候选 4096 / 单次选择 256**，三处文档一起改；同时复核评估文档里 M0308 的结论。
3. 删掉 §13.1 里的本机绝对路径，改成"从 CLI 所在目录导入"或提供可移植的加载方式；否则把 §13 明确标注为"仅源码仓库内可用"。
4. §13.3 的 `candidates[0]` 改为"必须给出 hash 或尺寸证据，否则拒绝借用"。
5. `BEM_SOURCE_MOD_CONVERSION.md` §4 的"动画：不转换"改为"无原生通道，可按 §8.3 有损近似"。
6. 修正 `skip_validation` 的"自动钳制"表述（实际是"跳过校验"，不是"钳制"）。
7. `bem_triage` 补 `ENTRY_SELECTION` / `COMPANION_INI` 两个阻断码。
8. 清理 SKILL.md 里 1.5.4 / 1.5.0 / 1.6.2 的混用，补 `pack-zip`。
