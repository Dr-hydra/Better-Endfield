# 文档迁移与检查

本次只整理文档、登记来源和改写引用，不改变研究结论。用途调查为 `workspace_inventory_20261005/项目用途_文档.csv`（165 行）；源报告位置保存在清单元数据中。

## 组织与消费

- 现行说明：`docs/<module>/`；版本相关材料：`docs/<module>/research/<actual-game-version>/<topic>/`。首次迁移从最新工作区配置读取当前版本；用户已确认九月初后为 1.5.3，旧客户端使用 pre-1.5.3。
- BEM 历史格式按 `docs/custom_model/history/bem-<format>/` 组织；BE 发布记录按 `docs/workspace/releases/<software-version>/` 组织。两者不是游戏版本。
- 日期保留在原文标题、正文和 catalog 元数据中，不作为目录主分类。1.5.3 内部热更新由资源快照与来源清单区分。通用工具/格式的 `game_version` 为 `not_applicable`；跨版本对照列出两个版本，原文中未校正的 1.4.4 标记作为原始版本声明保留并注明与用户时间线差异。
- `config/documents.json` 的 `documents` 字典保存原公开 filename 到唯一源码 path。源码移动后，安装目录公开名仍为原 basename。
- 四组中英文 BEM 规范位于 `docs/custom_model/`，第三方模块作者指南位于 `docs/host/`。Skill reference 保留原位与原内容，parent 负责打包同步。
- `artifacts/workspace-reorganization-20261005/doc_reference_rewrites.json` 是旧 repo-relative path 到新 path 的普通 JSON 字典，包含 126 个迁移路径及 1 个已确认的过时引用别名。根 README、模块邻接 README、代码注释和其他外部消费者由 parent 集成，不由迁移脚本写入。

## 运行方式

从当前仓库运行配置选择的 Python；`--workspace-config` 或 `BE_WORKSPACE_CONFIG` 沿用共同工作区 API。首次计划、离线模拟和迁移需要传入用途调查 CSV：

```powershell
python scripts/migrate_documents.py plan --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py simulate --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py apply --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py check
```

脚本只读 Git 文件清单和已登记的小型文本，不遍历外置盘历史资料、不计算大文件哈希。移动使用同一仓库内 `Path.rename`；每次读写与移动前验证解析后的绝对路径仍在当前根目录内，拒绝目标冲突、越界路径和受其他代理管理的三个 workspace 页面。

`simulate` 在内存中验证引用重定位、标题与正文行数保留、目标唯一性和重复运行无变化。`check` 检查清单、源码路径、研究版本目录、导航覆盖、相对文档引用和工作区文档查找 API。现有缺失的历史证据文件单独报告，不伪造资源、不视为本轮实机验证。带 Git 标签或 commit 的外部来源 URL 保留其历史路径。

外部文档消费者的待集成命中仅作只读报告；脚本不改根 README、邻接 README、代码、已有脚本或 skill。清单保留各文件的原用途、状态细节、勘误与证据定位，方便 parent 重写迁移范围以外的引用。
