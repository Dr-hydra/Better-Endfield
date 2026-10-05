# 研究资料入口

研究按模块、游戏版本、平台和专题保存。当前客户端为1.5.3，旧客户端对照为pre-1.5.3；快照差异另记。日期只用于来源元数据。

- [完整本地资料清单](../config/research-catalog.json)
- [文档导航](../docs/INDEX.md)
- [工作区规范](../docs/workspace/WORKSPACE_POLICY.md)

原始共用输入位于inputs，工具环境位于toolchains，普通临时/构建输出位于temp/build。研究目录保存有价值的脚本、映射、采样、诊断和结论。legacy仅作备份，不作为正常工具链的数据源。

上游快照的.gitignore保存为.upstream-gitignore，避免其规则覆盖本工作区的资料管理规则。大型本地二进制与捕获数据通过清单引用，不自动提交Git；研究源码和说明随Git维护。
