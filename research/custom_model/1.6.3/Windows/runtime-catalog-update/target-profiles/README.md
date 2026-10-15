# 1.6.3 开屏采集：BEM 1.4 显式世界模型资料

这些目标使用现有 `build_bem14_target.py` 从本专题的
`database.observed.json` 生成。输入来自 Windows 1.6.3 离线对象图谱与开屏资源交付探针，
manifest 为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`。每个选中 renderer 均有
运行时顶点声明；离线身份与捕获的对应结果见上级目录 `runtime-match-report.json`。

| 文件前缀 | 实际世界资源 | LOD0 组件 |
| --- | --- | ---: |
| `chr_0037_minghe-world` | `chr_0037_minghe_postmodel` | 20 |
| `chr_0037_minghe02-world` | `chr_0037_minghe02_postmodel` | 20 |
| `chr_0037_minghe03-world` | `chr_0037_minghe03_postmodel` | 20 |
| `chr_0036_jsspsi-cloth04-world` | `chr_0036_jsspsi_postmodel` 的 cloth_04 | 1 |

三种明河模型的 target owner 均为真实角色 `chr_0037_minghe`，用各自精确世界资源和
独立 profile ID 区分，不构造新的角色身份。每份保留全部 20 个 LOD0 renderer，
其中包含 2 个 vfxpart。祀 cloth_04 使用精确 renderer 路径，保留其真实世界材质合同；
该部件世界与详情材质不同，因此没有强行加入旧双根共享合同。

每组有三个文件：

- `.spec.json`：精确 prefab 路径、Windows 平台、LOD0 和可选 renderer 路径。
- `.profile.json`：现有 BEM 1.4 目标合同与序列化身份；选中组件的
  `runtime_layout_observed` 均为 `true`。
- `.project.json`：现工具可读取、继续编辑的工程；所有操作为 `keep`，不含替换
  payload。本次没有构建或安装 `.bem` 包。

资料只声明 `windows-x64`，不推断 Android donor。明河没有详情根，故使用 1.4
显式资源表，不填造 `ui_resource`，也不写入旧 `bem-character-catalog`。
现通用 EFMI 自动转换和 Blender 导出仍面向旧双根 catalog，不能直接把这些资料
当作其角色库。没有做新来源 Mod 的资源身份映射；明河每路仅 10/20 部件可被旧
`source_layout()` 推导器直接识别，其余实际声明已完整保留在 observed database。

`runtime_verified`、`conversion_ready`、`render_verified` 保持 `false`：
本次已观察原始资源交付与布局，尚未证明自定义网格替换、切换与恢复行为。
`profile` 的通用 limitations 来自现生成器；采集证据和布局保存在同角色的
`database.observed.json`、`runtime-observations.json` 以及上级 `captures/`。

在项目目录重建这四组 JSON：

```powershell
python research/custom_model/1.6.3/Windows/runtime-catalog-update/target-profiles/build_targets.py
```

脚本调用现生成器的完整目标结构验证；不计算产物哈希，不改生产 schema 或 native。
