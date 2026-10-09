# 工作区配置与日常入口

当前游戏资料为1.5.3，更新前资料归pre-1.5.3；同版热更新记录资源快照。工作区源码在本目录，旧目录仅作回退备份。

配置读取顺序：workspace.defaults.json → workspace.local.json（本机，不提交Git）→ 显式脚本参数。游戏目录默认从注册表自动发现，本机BE实际测试目标配置为E:\Better Endfield Next。

常用入口：

```powershell
python scripts/workspace_doctor.py
python scripts/run_workspace_tests.py --list
python scripts/run_workspace_tests.py --module workspace --plan
& scripts/UpdateResourceManifests.ps1 -Plan
& scripts/Clean-Workspace.ps1
& scripts/BuildBetterEndfieldNext.ps1 -Configuration Release
& scripts/BuildWeb.ps1
```

Clean默认只列出计划，-Apply才清理temp/build/cache；输入、研究、工具、发布包和安装目录禁止作为清理对象。构建脚本统一输出build，发行保留releases；部署单独执行。具体构建入口和本机验证记录见[构建说明](BUILDING.md)。

[完整规范](WORKSPACE_POLICY.md)、[测试管理](TESTING.md)、[研究资料清单](../../config/research-catalog.json)、[文档导航](../INDEX.md)。有用研究资料已经复制到新工作区；legacy没有正常输入依赖。
