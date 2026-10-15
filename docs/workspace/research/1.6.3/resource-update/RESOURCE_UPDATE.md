# 1.6.3 Windows 资源增量更新

来源：`G:\Arknights Endfield`，目标工作区：`G:\Better Endfield`。游戏版本由当前 `globalgamemanagers` 的 PlayerSettings 确认。输入保存到 `inputs/1.6.3/Windows/`，未覆盖 1.5.3 输入；仅维护新版客户端。

## 现有入口与必要修复

运行入口仍是 `scripts/UpdateResourceManifests.ps1`。当前有效资源按 StreamingAssets → Persistent 覆盖读取。

- 当前 17 个基础 BLC 和 25 个 Persistent BLC 全部使用 code version 5。chunk/file tag 后各新增 4 字节扩展，旧解析器因此错位。输入刷新已针对当前 v5 更新。
- 当前工作区缺少 `endfield-dump`，历史现有 CLI 的 VFS 库也只支持旧布局。`scripts/PrepareEndfieldDump.ps1 -SourceRoot 'G:\Better Endfield_legacy\tools\EndfieldStudio'` 复用该 CLI，只重编小型 VFS 库并放进当前 `toolchains/EndfieldStudio/`，不修改历史目录。
- 媒体 PCK 的语言名使用 UTF-16LE，bank PCK 使用 UTF-8。修正后报告显示完整语言名，不再截成 `c/s` 等首字母。
- 战斗导出默认保留已经存在的网页图片；入口通过 `--fill-missing-icons` 只补真实缺图，并先由当前 manifest 限定 bundle，避免无目标扫描。
- 头像脚本新增 `--missing-only`，保留既有图片、来源和资源 ID。本次运行下载 0 张；CEP 索引暂未提供明河/祀的新头像，未猜测替代图。

## 已更新资源

| 项目 | 更新前 | 当前 |
|---|---:|---:|
| 模型/动作预设 | 33 | 37 |
| 动作 | 4,262 | 4,719 |
| 具名角色短语音 Event | 996 | 1,025 |
| 四语完整映射 Event | 638 | 660 |
| 唯一 Media ID | 6,859 | 7,083 |
| 未映射具名 Event | 358 | 365 |

新增模型条目为 `chr_0036_jsspsi` 和 `chr_0037_minghe`、`minghe02`、`minghe03`。明河名称由当前 ForesightCharGrowthTable/I18n 得到：中文“明河”，英文“Argent Flow”。NpcTemplateGroupTable 将 02/03 关联到同一人物的模型变体；名称标记“模型 02/03”，不当成独立干员。

角色条目没有删除。动作增加 459 条、删除 2 条，净增 457 条；删除项在当前 manifest 中已不存在：

- `chr_0015_lifeng / a_actor_lifeng_battle_dead`
- `chr_0027_tangtang / a_actor_tangtang_dialog_state_kneeldown_catchwind`

动作 LoopTime/时长继续使用既有明确采样的 clip 元数据；新动作没有另做全量动画导出，不凭名称填写时长。

语音四语各有一个当前 localized bank PCK，每包 1,359 个 bank，均命中 660 个具名 Event。不存在因漏选另一语言 bank 分包造成的覆盖缺口；未标明语言的公共 bank 不推断角色归属。映射 HIRC 无效数为 0，四语已引用 Media 均在相应语言 PCK 中存在。365 个缺 bank 事件和 699 个无具名归属 bank 保持显式缺口。

战斗资源读取了正常工具链需要的 Table 与 JsonData 两块：770 张表、23,358 个配置文件。字典包含 35 个角色、269 个技能、84 把武器、276 件装备和 407 个关卡；永久 ID 表追加 573 项，总数 5,077，原索引顺序保留。

网页真实缺图共 24 张：祀的 3 个技能图标、21 个装备/武器图标。由当前 manifest 限定到 2 个 bundle（约 16.4 MB），现有 CLI 用时约 1 秒，24 张全部提取成功。既有网页头像恢复原文件，未重编码覆盖；图标覆盖为技能 104、物品 353、套装 19。

## Buff 配置结构变化

新版 SkillData/BuffData 已用 StringPool 索引替换旧的内嵌字符串，文件首字节是 MemoryPack 成员数，不能当格式版本。旧 ASCII 扫描曾产生 0 条 skill-buff edge；该退化输出已撤回，不能用于发布。

已完成 `string_pool.py` 与 `memorypack_refs.py`，并接回正常战斗导出入口。USPC 块按 LZ4 解压，字符串只在真实 String 字段读取；普通整数不查字符串池。当前 schema 由两个根类型的 setter、继承字段、非引用值类型大小及 18 个局部 formatter 构造函数生成，保存于 `config/compatibility/combat_stats/1.6.3/memorypack-schema.json`。Union 标签取自构造函数的显式注册值，没有按类型排序猜测。数组的裸结构和 List 的逐项 formatter 分开处理。

2,928 个 SkillData 和 3,395 个 BuffData 均完整消费到文件末尾。正常导出成功生成 13,594 条配置引用、706 个有来源候选的 Buff、1,959 条候选及 2,772 条技能—Buff 关联；751 个来源根均存在。288 个存在多来源的 Buff 继续保留候选，不擅自归成单一来源。新增三项小测试覆盖整数误认字符串、结构数组/List 的布局差异、截断输入和 LZ4 回溯复制。

最终续跑复用已刷新的输入：

```powershell
python tools/CombatDataExporter/export_combat_data.py --game-path 'G:\Arknights Endfield' --no-refresh-tables --no-refresh-json-data --fill-missing-icons --verify
```

导出成功，日志位于 `build/logs/game-update-1.6.3-combat-export.log`。后续正常更新仍运行 `scripts/UpdateResourceManifests.ps1`，无需手工替换 Buff 资源。

## 检查范围

- 输入刷新：37 个 PrefabInfo、33,273 条 AudioDialog、371,043 项资产索引成功。
- VFS 库局部构建：0 警告、0 错误。
- 3 项小型二进制格式回归通过，覆盖当前 BLC v5 的文件边界/加密 seed、两种 PCK 字符编码及截断拒绝。
- 只读取了必要配置、PCK 索引/目标 bank 和 2 个图标 bundle；没有全量游戏资源提取、全量反编译或额外产物哈希检查。
- 未进行游戏内动作/语音播放验收，也未部署或发布。
