# 转换示例

`conversion.recipe.json` 是需要填写真实角色资料路径的模板，不是可直接用于任何角色的放行配方。
可在 appearances 数组添加固定外观。各外观 target 契约必须一致。
这是固定外观配方；普通资源仍输出 BEM 1.0，目标需要骨骼别名或 32 字节蒙皮时会正规升级为 BEM 1.2 的外观选项组与组件规则。

`export.bemproj.json` 是可保存、重复导出的任务工程模板，与底层 `project.json` 分开。将输入路径改为真实可编辑 BEM 项目后运行 `build export.bemproj.json`；也可在创作者 GUI 的“创建 / 打开导出工程”中打开、改参数并导出。`mode` 可为 `pack` 或 `convert`，后者可另设 `recipe`。所有路径相对任务工程目录，`package.id` 在反复导出时保持不变。

需要把源文件和输出目录整理成可移动工程时，可以运行 `BetterEndfieldNext.BemConverter.exe workspace init <目录> --source <源文件或 project.json> --mode convert|pack`。它会复制输入、创建 `source/project/textures/dist/reports` 目录，并生成同样的 `export.bemproj.json`。

`body-slider/create_project.py` 生成可运行的 BEM 1.3 位置滑条示例（只需 Python 标准库）：`python body-slider/create_project.py --output NEW_DIRECTORY`，再运行 `BetterEndfieldNext.BemConverter.exe build NEW_DIRECTORY/export.bemproj.json`。任务使用同一工程的 `deformations` 字段，包含三角形 base、同拓扑 target 和 body 滑条；这是格式测试，不是游戏角色。详见 `docs/custom_model/BEM_CREATOR_GUIDE.md` 的“形态滑条”一节和 `docs/custom_model/BEM_SOURCE_MOD_CONVERSION.md` 的 ShapeKey 绑定。

`multi-resource/create_project.py NEW_DIRECTORY` 生成 BEM 1.4 混合蒙皮/静态资源工程，依赖工具的 Python 环境。示例包含三个独立资源、跨资源同名 mesh、共享几何 payload、各资源独立形变，以及平台和选项筛选。使用 `bem_tool.py build NEW_DIRECTORY/export.bemproj.json` 构建；`--native-fixtures` 可另外生成正常包和三种故意损坏的解析测试包（跨资源 donor、资源 LOD 不一致、旧版本头混入新字段）。所有资源身份均为合成测试数据，不可作为游戏 Mod。详见 [BEM 1.4 规范](../../../docs/custom_model/BEM_V1_4_SPEC.md)。

BEM 1.1 的组合外观由可编辑项目的 `manifest.option_groups`、`component_rules` 和带独立索引 payload 的条件 draw 表达，不能只在旧配方里增加热键名字。组可用条件、可达性约束及每个候选资源的原生身份需审阅后填写；同包保留全部候选资源；选择在下次启动游戏后生效，开启实验热切换时在下一次资源加载时生效。

第四条参考路径是洁尔佩塔完整版：手工审阅 70 条源绘制及 33 个开关，生成一个 1.1 包，用精确条件分析核对 301,056 种有效静态选择。按这一流程保存源绘制映射、可复现构建脚本、局部规则真值表和原生读取报告；该样本只证明静态几何、材质/贴图映射与包读取，不包含源自定义 Shader 效果。

直接导出项目结构为 `{"manifest": {正式 BEM Manifest}, "payload_files": ["vb0.bin", "vb1.bin", ...]}`。
文件顺序就是原始 Payload ID；打包器会重新共享相同字节，并更新引用。
使用 `pack project.json -o output.bem`。

参见随软件发布的 `docs/custom_model/BEM_CREATOR_GUIDE.md`、`docs/custom_model/BEM_FORMAT_SPEC.md`。
