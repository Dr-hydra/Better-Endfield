# EFMI 源侧参考（未改动副本）

本目录是 **EFMI 加载器随包装配的 INI 库与官方模板的未改动副本**，用于离线查证
"源 Mod 里这条语句到底做什么"。它不参与构建、不会被加载、也不被任何脚本自动执行。

## 来源与许可

| 路径 | 上游 | 提交 | 许可 |
| --- | --- | --- | --- |
| `core/`、`shaders/` | [`SpectrumQT/EFMI-Package`](https://github.com/SpectrumQT/EFMI-Package) · `EFMI/Core/EFMI/` | `3cc2f4b89e791983484ac00ad5a3e15779957350` | GPL-3.0 |
| `mod.ini.j2` | [`SpectrumQT/EFMI-Tools`](https://github.com/SpectrumQT/EFMI-Tools) · `efmi-tools/templates/` | `eaa9036` | GPL-3.0 |
| `per_component.ini.j2` | 同上 | `926dd49`（ALPHA-4 历史模板） | GPL-3.0 |

上述文件版权归原作者所有，按 GPL-3.0 分发。本项目的 GPLv3 §13 组合条款下整体按
AGPL-3.0-only 分发；本目录内的文件**保持其原有许可**，不因本项目而改变。

这些文件只作为参考资料收录，未作任何修改。上游有新版本时，这里不会自动跟随——
需要更新时按上表的路径与提交重新取一份，并在转换记录里写明用的是哪个提交。

## 怎么用

先读[执行手册](../BEM_SOURCE_CONVERSION_PLAYBOOK.md)的附 A。四个要点：

1. **段名不等于功能**。`OverrideTextures` 听起来像在做材质，实际只是按资源身份触发一堆检查。
   判断方法永远是：看每条命令改的是哪个槽/缓冲，再看 `run` 链落到哪个 draw。
2. **只有 loader 自带库里的东西语义确定**。`CommandList\EFMIv1\*`、`Resource\EFMIv1\*`
   在这里能查到定义，按定义读。
3. **第三方框架不在这里，但接口不是未知**。最常见的是 `RabbitFX`（作者 caverabbit，
   终末地对应 EFMI 版：<https://gamebanana.com/mods/651557>）。它**不收录**——发布页是
   CC BY-NC-ND 4.0，ND/NC 与 AGPL-3.0 的 §10 冲突。但它的槽位表、寄存器绑定、委托 draw
   公式写在手册附 A.3，读源包够用。只有**连本体都拿不到**的命名空间才判"绑定未知"。
4. **查不到就别补**。静态文本里读不出的运行期行为，按手册 §12 停下并披露。

## 文件索引

| 文件 | 内容 |
| --- | --- |
| `core/API.ini` | 对外 API：贴图覆盖入口、命名空间声明、实例配置读取 |
| `core/SpatialIdentification.ini` | 按空间位置区分实例身份 |
| `core/MergedSkeleton.ini` | 骨骼实例的合并与偏移 |
| `core/ShapeKeys.ini` | 形态键的加载与逐键设置 |
| `core/Helpers.ini` | 通用命令片段 |
| `core/KeyBindings.ini` | 快捷键绑定 |
| `core/TextPrinter.ini`、`core/help.ini`、`core/main.ini` | 屏幕文本、帮助与入口 |
| `shaders/ShapeKey*.hlsl`、`shaders/SkapeKeySetter.hlsl` | 形态键反量化与写入（原文件名即此拼写） |
| `shaders/MergedSkeleton_*.hlsl` | 骨骼数据导入与实例配置覆盖 |
| `shaders/TextPrinter.hlsl` | 屏幕文本渲染 |
| `mod.ini.j2`、`per_component.ini.j2` | 官方模板：一份标准源 INI 的结构 |
