# BEM 工具链与多包 ZIP（2026-09-19）

实现 `bem_tool.py` 的 inspect/convert/unpack/pack/validate/bundle，工具版本 1.0.0。
`bem_projects.py` 负责可编辑项目和 ZIP，GUI 通过打包后的同一 CLI 校验/解包，不实现第二套转换规则。
运行时 BEM wire 格式无变化；独立工具链压缩分发约 9.0 MB，不包含角色采样数据库。

管理页支持 ZIP 勾选批量导入，展示角色、版本、外观及逐包失败原因；同 ID 更新、同角色单选、
默认停用和 LOD 偏好沿用现有逻辑。ZIP staging 随对话完成清理，正式导入再次验证将安装的字节。
创作者窗口新增解包、打包和多包 ZIP 封装按钮；新增 `skills/bem-creator` 及可独立安装的完整引用文档。
主分支固定注入器路径和真实 EXE 根目录定位已同步，新工具/文档路径也遵循该规则。

验证：

- 八项项目/ZIP 定向测试：编辑/解包重打包、stored 合集、多包暂存、坏包隔离、重复 ID、越界路径、失败保留原输出、CLI 报告防覆盖。
- C# 管理服务检查：ZIP 中两份有效包加一份损坏包，选择导入、新包停用、同角色选择、外观回退、独立 LOD 持久化及 staging 清理。
- 打包后 CLI 对真实洁尔佩塔双外观 BEM 解包再打包：Manifest 与所有解压后的资源字节完全相等。
- WinUI Release 构建与发布通过；Skill quick_validate 通过。
- 已生成洁尔佩塔 ZIP 样本 37,816,398 字节；内层 BEM 37,816,260 字节。

未执行游戏实机测试或 UI 点击测试，由用户负责。没有计算产物哈希。
完整功能说明见 [创作者指南](BEM_CREATOR_GUIDE.md)，协议见 [BEMv1 规范](BEM_V1_SPEC.md)。
