# 1.7.2 核心资源更新

更新日期：2026-11-25

## 来源与版本

| 平台 | 游戏版本 | 来源确认 | 输入快照 |
| --- | --- | --- | --- |
| Windows | 1.7.2 | 本机游戏目录 `F:\Arknights Endfield` 的 `globalgamemanagers` | `inputs/1.7.2/Windows/input-snapshot.json` |
| Android | 1.7.2 (versionCode 71) | 设备 `78572d34` 上 `com.hypergryph.endfield` 的已安装游戏 | `inputs/1.7.2/Android/input-snapshot.json` |

Windows 输入为当前 VFS overlay；Android 输入从设备当前 VFS 导出。Android 仅使用该平台的 manifest、PrefabInfo、AudioDialog、PCK 索引和所需 bundle 数据，没有把 Windows 战斗 payload 塞入 Android 包。

| 输入统计 | Windows | Android |
| --- | ---: | ---: |
| manifest assets | 373,175 | 369,902 |
| manifest bundles | 272,479 | 270,041 |
| AudioDialog 行 | 36,064 | 36,064 |
| PrefabInfo 角色 | 39 | 39 |

## 生成结果

- 两端 presets 均有 39 个角色、5,006 个动作；生成器未报告未纳入角色。
- 两端短语音清单覆盖 1,089 个具名 Event、37 个角色；706 个 Event 可映射到 BNK/HIRC/Media，另外 383 个按来源清单标记为缺 Bank。四语均通过映射的 Event 为 706 个。清单不携带 WEM 或 PCK 音频 payload。
- Windows 战斗数据从 1.7.2 Table/JsonData 生成：37 个战斗角色、280 个技能、87 把武器、291 件装备、738 个 Buff。非战斗角色米米诺不进入战斗字典；小刻进入。
- 角色名表补入 `chr_0039_mimino`（米米诺 / Mimino）与 `chr_0040_sweetpie`（小刻 / Kay）。
- MemoryPack schema 记录在 `config/compatibility/combat_stats/1.7.2/`：851 types、18 unions，union targets 闭合。StringPool 解析器已按每个 chunk 自身声明的条目数寻址，以兼容 1.7.2 的块大小变化。

## 核心包

| 平台 | 资源版本 | 文件数 | 解包大小 | ZIP |
| --- | --- | ---: | ---: | --- |
| Windows | `1.7.2.2` | 126 | 9,477,830 bytes | `temp/resource-packs/zips/BE-Core-Windows-1.7.2.2.zip` |
| Android | `1.7.2.2` | 6 | 5,720,615 bytes | `temp/resource-packs/zips/BE-Core-Android-1.7.2.2.zip` |

Windows 包包含 model、voice、combat、pose actions、角色头像和当前可用武器图标。Android 包包含 model、voice 和 pose actions，不包含 Windows combat / icons。两包 manifest 的 gameVersion 均为 `1.7.2`；构建器已核对文件清单与长度。没有计算产物哈希。

## 发布

- 已发布到 [game-resources Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources)：`BE-Core-Windows-1.7.2.2.zip`、`BE-Core-Android-1.7.2.2.zip`。
- Release 的 `distribution-index.json` 已更新，现含 6 条资源：原有四个 core/preview 条目及本次两个 core 条目。`distribution-index.previous.json` 保持原状；本次未清理历史归档。
- 直接下载链接：[Windows core](https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/BE-Core-Windows-1.7.2.2.zip)、[Android core](https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/BE-Core-Android-1.7.2.2.zip)、[分发索引](https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/distribution-index.json)。索引已通过 GitHub API 和带缓存绕过参数的下载校验；普通下载 URL 的 CDN 有短暂旧内容缓存。

## 已知缺口与边界

- 当前图标扫描仍有 24 个资源未能从 1.7.2 输入提取：6 个明河/小刻技能图标，以及 18 个装备/武器图标（15 个装备图标、3 个武器图标）。这些 ID 记录在 `web/public/icons/icon-manifest.json` 的 `missing` 字段；包内没有伪造或用其他图标替代它们。其他当前可用武器图标和全部 34 个头像进入 Windows 包。
- Table 共解析 798 张；`I18nTextServerTimeIdTable.bytes` 的 map Long 字段未能解析。这是通用时间文本表，不影响本轮已生成的角色战斗字典，但不代表所有 1.7.2 Table 都已覆盖。
- 生成与静态验证不等于游戏内实机验收。未启动游戏、未安装包、未执行界面或设备验收；安装后的显示和运行情况由用户测试。

## 验证

- `python -m unittest tools.CombatDataExporter.test_memorypack_refs -v`：4/4 通过。
- `python -m unittest scripts.tests.test_game_resource_packs -v`：8/8 通过。
- `scripts/BuildResourcePacks.py --platform windows --type core --output temp/resource-packs/resources --archives temp/resource-packs/zips`：成功，版本 `1.7.2.2`。
- `scripts/BuildResourcePacks.py --platform android --type core --output temp/resource-packs/resources --archives temp/resource-packs/zips`：成功，版本 `1.7.2.2`。
- 两个生成包均通过构建器 `verify()` 的 manifest、路径、文件清单、字节长度及平台内容检查。
