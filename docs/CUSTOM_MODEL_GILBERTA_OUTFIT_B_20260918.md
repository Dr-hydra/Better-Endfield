# 洁尔佩塔外观分支修正与 UInt32 索引

## 原因与用户选择

用户报告“没替换”。实际 catalog 配置正常，日志记录：

```text
2026-09-17 15:11:47 Resource committed: chr_0013_aglina_postmodel components=4
2026-09-17 15:12:04 Resource committed: chr_0013_aglina_uimodel components=4
2026-09-18 13:58:27 Resource committed: chr_0013_aglina_postmodel components=4
```

进一步从原生 Bundle 读取位置/三角形，对默认 `$swapkey9=0` 的衣服、头发、尾巴进行几何核对。
按位置合并重复顶点后，55,814／21,710／2,360 个三角形各自与原版完全一致，
最大位置误差小于 6.2e-8。源面板将其称为 Outfit A，P 键把 `$swapkey9` 切到 1（Outfit B）。
因此首版完成了替换交付，但冻结了保留原服装的默认状态；此前将其作为用户目标外观交付不恰当。
日志提交不能独自证明最终显示，不过源几何核对解释了当前外观与原版相同的现象。

用户明确答复：“是，做另一套服装，允许调整索引或分片限制”。据此新增固定 Outfit B 配方，
所有其他持久化开关仍取默认 0，支持完整 UInt32 索引，不做自动分片。

## 实现

- v25 组件头已有的 `index_element_size` 现在允许 2 或 4；v24 仍只允许 2。
  旧 DLL 遇到新四字节索引会拒绝解析，需要更新 DLL。
- Python lowering 需显式 `allow_uint32_indices=true` 才允许超过 65,536 个顶点；
  只有实际超过上限的组件输出 UInt32，其他组件仍输出 UInt16。保留原生 1,048,576 顶点上限。
- C++ 根据元素宽度读取并核对索引范围/截断，删除重复的固定两字节扫描。
  `UploadComponentIndices` 核对字节数与元素数，选取正确 Unity index format 后上传完整缓冲区。
- 配方 `tools/CustomModel/profiles/gilberta-12.outfit-b.reviewed.json` 仍严格匹配源 INI 和五份 Shader。
  选择的是另一份静态绘制清单，不执行包内脚本、不新增快捷键/面板或 Shader 改写。

| 组件 | 顶点 | 索引 | 宽度 | 骨骼 palette | draw |
| --- | ---: | ---: | --- | ---: | ---: |
| cloth_01 | 151210 | 726447 | UInt32 | 80 | 19 |
| hair_02 | 2041 | 7080 | UInt16 | 13 | 1 |
| hair_01 | 21527 | 191448 | UInt16 | 38 | 4 |
| body_01 | 136 | 588 | UInt16 | 3 | 1 |

衣服前五段复用原生皮肤材质，保留对应 cloth 骨架；第六段保留原衣物材质与纹理。
剩余十三段用原生衣物 Shader，绑定源新增颜色、法线及参数图。
头发基础几何和新增几何分别使用原生两个材质槽，共四段；新增几何的颜色／LightMap／NormalMap
分别映射 BaseMap／MetallicGlossMap／SplitNormalMap，需实机确认这一材质近似的效果。
原生 body 材质的法线仍保持关闭状态；这不代表源法线逐像素等价，沿用首版已说明的取舍。

## 验证与部署

- lowering 20 项测试通过，包含未授权大网格拒绝、开启 UInt32 后 65,537 索引不截断。
- C++ 定向测试通过：UInt32 读取/越界/截断/无效宽度拒绝、v24 拒绝四字节扩展，
  生产上传函数的格式、元素数、字节数正确，UInt16 上传路径保持原行为。
- 实际新包通过生产解析；原默认 UInt16 包也通过同一新版解析器。
- 独立回读逐项核对所有选中三角形、VB0/VB1 字节、蒙皮权重/来源骨骼、材质来源及纹理掩码，
  七份纹理 payload 与源 DDS 完全一致，EOF 精确。未重复其他角色或 LOD/懒加载实测。

包 `gilberta-outfit-b-v25.bempoc` 为 **480,065,480 字节**。
七个纹理记录来自三份自定义 8K DDS，因不同原生纹理名称 pin 分别记录，导致包较大；
本轮未另改包协议做纹理去重或降低分辨率。

已逐字节确认并部署到指定测试目录的 `custom-model/` 及实际 catalog 的 `custom-model/`。
测试目录 `modules/BetterEndfield.CustomModel.dll` 已覆盖为新编译版本（543,744 字节）。
actual catalog `runtime.ini` 的 `[Mod.aglina]` 改指向 Outfit B，另外三个角色保持原配置。
副本：`artifacts/native-parser/gilberta-outfit-b-runtime.ini`，以及测试目录同名文件。
未创建备份，未计算产物哈希，未合并/push/发布。

生成命令（工作树根目录）：

```powershell
python tools/CustomModel/convert_reviewed_draws.py artifacts/converter-new-mods/gilberta --recipe tools/CustomModel/profiles/gilberta-12.outfit-b.reviewed.json --database artifacts/native-parser/gilberta-database.json --observations artifacts/native-parser/gilberta-observed.json --native-textures artifacts/native-parser/gilberta-texture-raw.json --texture-dir artifacts/native-parser/gilberta-texture-bytes --output artifacts/native-parser/gilberta-outfit-b-v25.bempoc --report artifacts/native-parser/gilberta-outfit-b-conversion.json
```

证据：`gilberta-default-native-geometry-comparison.json`、`gilberta-outfit-b-conversion.json`、
`gilberta-outfit-b-conversion.profile.json`、`validate_gilberta_outfit_b_package.py`，位于 `artifacts/native-parser/`。
几何审计使用本地临时读取器副本，未修改正式读取器；其唯一 warning 是副本缺失 CNKeys.json
而自动创建，目标 Mesh 位置与索引均完整读出。

实机外观仍待用户确认：重新从原测试启动器进入，只检查洁尔佩塔另一套服装的世界/详情外观、
动作拉伸、皮肤接缝及新增衣物/头发材质，不重测其他角色。
