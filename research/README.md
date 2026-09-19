# 研究归档

这里保存能够帮助复现、审阅和继续研究的轻量资料，不是运行时模块的生产输入。

## `custom-model/`

`custom-model/legacy-runtime/` 保存已经从生产目录移出的旧 CustomModel PoC、诊断 hook 和对应的研究测试。它们用于追溯历史调用链和复现实验，不进入当前 CMake 目标，也不能代表当前运行时合同。目录说明和独立研究构建命令见 [custom-model/README.md](custom-model/README.md)。

## `postmodel-focused/`

这里保存 PostModel 资源交付、Mesh 归档和实例化路径的定点静态分析结果，包括：

- `findings.md`、`delivery-findings.md`、`model-consumer-findings.md`、`instantiate-route-findings.md`：人工整理的结论；
- `inspect*.py`、`decompile-mesh-native.py`：局部反汇编和结果整理脚本；
- `*.json`：由上述脚本导出的轻量地址、调用和字段证据。

IDA `.i64` 数据库、日志和 Python 缓存不纳入版本库。它们体积大、依赖本机分析环境，且可以由结论文件和脚本重新生成或补采。若需要在另一台机器继续 IDA 工作，应从对应的本地二进制和 dump 重新建立数据库。

## 当前生产入口

生产实现位于 `native/modules/custom_model/`，面向工具链的设计与验证文档位于 `docs/`，可分发的 BEM 工具位于 `tools/CustomModel/`。研究归档中的旧路径不能覆盖这些生产入口的行为定义。
