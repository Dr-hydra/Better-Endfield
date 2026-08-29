# 战斗语义目录

`combat-semantics.besem` 是 CombatStats 随软件版本发布的只读紧凑目录。它只包含 Buff、技能、元素、
乘区、状态和少量公式键，不包含 PCK、BNK、WEM、角色资源、封包或抓包内容。运行时只从
`BetterEndfield.CombatStats.dll` 同目录读取这一个文件；不存在用户更新目录或远程热更新。

当前目录来源版本和 SHA-256 记录在 `combat-semantics-report.json`。版本更新时使用用户提供的
EndfieldLogsClient 数据包重新生成：

```powershell
python scripts/BuildCombatSemantics.py `
  --source "C:\path\to\EndfieldLogsClient.zip" `
  --output manifests/combat/combat-semantics.besem `
  --report manifests/combat/combat-semantics-report.json

python scripts/BuildCombatSemantics.py `
  --verify `
  --output manifests/combat/combat-semantics.besem
```

生成必须是确定性的；同一输入再次生成应得到相同 SHA-256。发布脚本会比较目录与报告中的哈希，
不一致时拒绝构建。候选和排除项可以随目录发布用于覆盖率审计，但 CombatStats 只分配
`verified` 效果。
