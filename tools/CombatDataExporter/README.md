# CombatDataExporter

统一生成最新版战斗字典、Buff 来源反向索引和网站实际引用的图标资源。默认先
从当前游戏的 `Persistent/VFS` 热更层读取 Table 与 JsonData，并在缺少热更
分片时回退 `StreamingAssets/VFS` 基础层，不再直接信任仓库中的旧数据快照。

首次完整导出：

```powershell
python tools/CombatDataExporter/export_combat_data.py `
  --game-path "E:\Endfield Game" `
  --export-icons --verify-icons --verify
```

导出器会自动尝试 `E:\Endfield Game\Endfield_Data\StreamingAssets`，也可以显式指定：

```powershell
python tools/CombatDataExporter/export_combat_data.py `
  --export-icons `
  --vfs "D:\Endfield Game\Endfield_Data\StreamingAssets" `
  --verify-icons `
  --verify
```

资源扫描结果缓存在 `.icon-cache`。缓存覆盖当前字典后，后续 `--export-icons` 会跳过游戏资源重扫；普通字典更新仍会先刷新 Table：

```powershell
python tools/CombatDataExporter/export_combat_data.py --verify-icons --verify
```

只有没有游戏安装目录的离线构建才允许显式使用仓库快照：

```powershell
python tools/CombatDataExporter/export_combat_data.py `
  --no-refresh-tables --no-refresh-json-data --verify
```

输出包括：

- `manifests/combat/combat-dictionary.json`
- `manifests/combat/buff-sources.bemap`
- `ui/BetterEndfield.UI/Assets/combat/combat-dictionary.json`
- `web/src/data/combat-dict.min.json`
- `web/public/icons/characters`
- `web/public/icons/skills`
- `web/public/icons/items`
- `web/public/icons/suits`
- `web/public/icons/icon-manifest.json`

导出器只将字典精确引用的 PNG 写入网站目录，并删除不再引用的旧图标。网页图标最长边压缩到 128 像素；`--verify-icons` 遇到任何缺失资源时返回非零退出码。

`buff-sources.bemap` 只接受 Table 中的技能/被动/武器/套装入口，以及与真实
`SkillData`、`BuffData` 文件名完全匹配的直接引用。运行时还会结合施加角色、
当前武器、套装和 `skillCastInfo` 唯一确认；候选冲突时保持未知，不按 ID 前缀猜测。
