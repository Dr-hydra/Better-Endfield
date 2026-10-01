# 转换示例

`conversion.recipe.json` 是需要填写真实角色资料路径的模板，不是可直接用于任何角色的放行配方。
可在 appearances 数组添加固定外观。各外观 target 契约必须一致。
这是固定外观配方；普通资源仍输出 BEM 1.0，目标需要骨骼别名或 32 字节蒙皮时会正规升级为 BEM 1.2 的外观选项组与组件规则。

`export.bemproj.json` 是可保存、重复导出的任务工程模板，与底层 `project.json` 分开。将输入路径改为真实可编辑 BEM 项目后运行 `build export.bemproj.json`；也可在创作者 GUI 的“创建 / 打开导出工程”中打开、改参数并导出。`mode` 可为 `pack` 或 `convert`，后者可另设 `recipe`。所有路径相对任务工程目录，`package.id` 在反复导出时保持不变。

BEM 1.1 的组合外观由可编辑项目的 `manifest.option_groups`、`component_rules` 和带独立索引 payload 的条件 draw 表达，不能只在旧配方里增加热键名字。组可用条件、可达性约束及每个候选资源的原生身份需审阅后填写；同包保留全部候选资源以供未来热切换，但当前 UI 保存后下次启动才应用。

第四条参考路径是洁尔佩塔完整版：手工审阅 70 条源绘制及 33 个开关，生成一个 1.1 包，用精确条件分析核对 301,056 种有效静态选择。按这一流程保存源绘制映射、可复现构建脚本、局部规则真值表和原生读取报告；该样本只证明静态几何、材质/贴图映射与包读取，不包含源自定义 Shader 效果。

直接导出项目结构为 `{"manifest": {正式 BEM Manifest}, "payload_files": ["vb0.bin", "vb1.bin", ...]}`。
文件顺序就是原始 Payload ID；打包器会重新共享相同字节，并更新引用。
使用 `pack project.json -o output.bem`。

参见随软件发布的 `docs/BEM_CREATOR_GUIDE.md`、`docs/BEM_V1_SPEC.md`、`docs/BEM_V1_1_SPEC.md`。
