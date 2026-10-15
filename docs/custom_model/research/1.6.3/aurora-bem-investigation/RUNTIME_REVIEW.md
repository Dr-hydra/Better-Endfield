# 昼雪 BEM 失效：运行时只读调查

2026-10-15。对象：Windows `chr_0014_aurora` / Snowshine。未修改游戏、配置或 BEM，未启动游戏、注入或计算哈希。

## 现有证据的边界

- 本机 `%LOCALAPPDATA%/BetterEndfieldNext/logs/BetterEndfieldNext.log` 调查时约 1.03 MB，最后写入 12:22:51。精确检索 `aurora|chr_0014|snowshine|昼雪` 无记录；现有 native-probe JSONL 也没有昼雪。
- 12:11、12:22 两次 CustomModel 初始化均为 `Resource runtime enabled: mods=0`。这些会话没有加载昼雪包，不能用于证明或否定昼雪 donor 变化。没有以普通数字 `0014` 检索，避免误匹配坐标小数。
- 游戏目录既有文本日志未检索到昼雪标识；没有输出 SDK 日志原文或账号内容。
- [既有 BEM 链路审计](../bem-chain-audit/BEM_CHAIN_AUDIT.md) 的独立记录在 10:18:19 已成功提交 `chr_0013_aglina_postmodel`，10 个组件、3 个网格、15 张纹理。它只证明该代表包，不能外推昼雪。用户另已说明大多数其他角色正常，因此后续优先核对昼雪资源身份。
- 随后对本机存量目录 `G:/zmd_bem/成品/昼雪` 中 7 份 BEM 的包头和 manifest 只读审计确认：格式为 1.1/1.2，均为 12 组件，mesh 名、原索引数、骨名与材质表仍与旧 catalog `20260920-identities1` 一致。它们是本机候选样包，不能视为用户已经明确指定或逐个实测的同一批包。证据：`research/custom_model/1.6.3/Windows/snowshine-bem-investigation/package-targets.json`。

## 后续静态对照已确认的拒绝点

当前资源图谱确认：昼雪 C1 `S_actor_aurora_cloth_01_lod0` 在 world/UI 两根资源中均由 87,120 索引、23,073 顶点变为 87,288 索引、23,141 顶点。上述 7 份候选包仍要求 87,120，因此在严格校验下均无法通过该组件的原索引门槛。

生产控制流会遍历所有组件；C1 非唯一匹配时 `CaptureGenericResourceBindings` 立即返回 false，`PrepareResource` 随之失败，`ProcessResource` 保留原始模型。骨骼槽序和 bindpose 同时变化，不能把只放宽索引数视为完整兼容修复。详见同目录 `STATIC_FINDINGS.md` 与独立静态 JSON 对照。

## 哪些变化会拒绝昼雪资源

[通用 donor 选择器](../../../../../native/modules/custom_model/generic_model_matcher.h) 与 [绑定入口](../../../../../native/modules/custom_model/module.cpp) 对每个组件要求唯一匹配：

1. 角色与 UI/world 资源名称对应，原始网格来源可验证。
2. Windows 普通角色匹配 LOD0；BEM 1.4 显式资源也要求 Windows LOD0，且 receiver 路径与资源 LOD 一致。
3. 原 mesh 名称和 `original_index_count` 相符。这里直接校验的是所有 submesh 的索引总数，不是 `vertex_count`。
4. 显式资源的 renderer 路径、静态/蒙皮 renderer 类型相符；材质、shader、骨骼引用有效，骨骼在资源根内部。
5. 不允许同一接收 renderer 被多个组件重复匹配。

任一必要组件失败会拒绝该资源，不会仅跳过失配组件。因此多个昼雪包若共享同一旧 donor 身份，一个角色资源变化就可能同时影响它们。当前缺少昼雪失败日志，尚不能确定是哪项。

Windows 上述门槛不直接比较游戏 manifest UUID。UUID、bundleIndex 或顶点数改变可提示资源更新，但仅凭这些变化不能断定具体拒绝原因。

[诊断构造器](../../../../../native/modules/custom_model/generic_model_matcher.inc) 的日志前缀为 `Generic model donor is missing/ambiguous`，会列出 `resource`、`status`、`expectedIndices`，以及最多六个候选的 mesh、indices 和原因。常见原因有 `mesh-name`、`index-count`、`receiver-path`、`renderer-kind`、`bone-outside-resource-root`。热切换发现另有 `Hot switch discovery refused resource=... reason=...`。

## 更新 catalog 不会自动修改现有包

[mod_registry.cpp](../../../../../native/modules/custom_model/mod_registry.cpp) 第 165–174 行直接从每个 BEM 的 `target.components` 创建运行时 adapter：mesh 名称、原索引数、receiver 路径都取自包内信息。

因此，如果当前图谱证实昼雪 donor 身份变动，更新 character-presets 仅能影响后续转换；现有 BEM 是否能迁移、需要何种精确兼容规则，应依据新旧 mesh、骨骼和材质对照另行决定。不能通过全局放宽校验直接宣称修复。

## 最小补证路径（尚未执行）

优先采一轮正常 BEM 失败日志：启用一个已知失效的昼雪包，在角色详情显示昼雪，随后返回大世界并切到昼雪。无需截图或上传文件；本机读取新增 CustomModel 日志即可区分 `uimodel` 与 `postmodel` 拒绝。若切换仅复用缓存资源，关闭并重新打开角色页，仍没有资源交付再考虑下一次正常游戏启动。

若日志不足以得到原始网格身份，已有定向采集入口可以只收昼雪两个根资源：

```powershell
pwsh -NoProfile -File tools/CustomModel/developer-tools/Probe.ps1 `
  -Action arm -SingleSession -Characters chr_0014_aurora `
  -Manifest research/custom_model/1.6.3/Windows/runtime-catalog-update/minghe-shared-inputs/Bundles/Windows/manifest.json `
  -Run aurora-1_6_3-20261015
```

该命令会写 opt-in probe 请求，须在需要采集时另行执行；本调查未运行。下一次正常装载模块时读取请求，采集记录到 `%LOCALAPPDATA%/BetterEndfieldNext/catalog/custom-model/native-probe/`。Sweep 会在该进程内暂停模型替换以观察原始 donor，不修改保存配置；因此它与正常失败日志是两个不同验证步骤。

显示一次昼雪角色页和大世界模型后，用同入口 `-Action status -Run aurora-1_6_3-20261015` 检查两根资源是否完整；最后 `-Action stop` 结束采集。记录包括 renderer 路径、mesh 名称、vertex/index count、骨骼与材质槽，足以对照旧包身份。不需要全角色 sweep。
