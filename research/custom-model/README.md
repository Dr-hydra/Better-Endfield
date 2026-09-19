# CustomModel 历史研究归档

2026-09-18 将旧模型实现从生产源码目录移入本目录。使用仓库已有的 `research/` 命名。

`legacy-runtime/` 保存 `module_legacy.cpp`、24 个旧 `.inc` 片段，以及只测试该实现的
`custom_model_lifecycle_tests.cpp`。保留相对目录关系，旧测试仍能引用旧实现。
这些文件包含旧 PoC、全局实例扫描、早期交付试验、LOD/AI 补丁和诊断探针；
不能作为当前运行时行为或 BEM 第一版能力的依据。

当前生产代码在 `native/modules/custom_model/`，当前绑定测试在
`native/tests/custom_model_binding_tests.cpp`。现用的 `native_probe.inl` 是原生资料采集链
的一部分，并非废弃探针，仍保留。`convert_efmi_poc.py` 仍被新转换器导入并承担既有样本转换，
不能仅凭名称归档。

旧测试默认不进入 CMake 目标列表。需要研究历史行为时，单独配置研究构建目录：

```powershell
cmake -S native -B build-legacy-research -DBETTER_ENDFIELD_BUILD_LEGACY_CUSTOM_MODEL_RESEARCH=ON
cmake --build build-legacy-research --config Release --target BetterEndfield.CustomModelLifecycleTests
```

历史文档中的旧源码路径保留为历史记录；原 `native/modules/custom_model/module_*.inc`
和 `module_legacy.cpp` 现在对应 `legacy-runtime/modules/custom_model/`。
归档没有修改 BEM v24/v25、运行时替换算法、部署目录或现有包。
