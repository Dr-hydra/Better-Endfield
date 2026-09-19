# 庄方宜定向运行时探测

全角色详情页连续采集见 [2026-09-19 连续探针](CUSTOM_MODEL_RUNTIME_SWEEP_20260919.md)。下文保留定向采集流程。

## 当前结果：采集完成

成功 run：`zhuangfy-20260917-045215-521682`，游戏进程 ID `60248`，状态 `complete`。
world/UI 各四个 renderer 全部通过身份、骨骼/材质顺序及顶点声明验证，已导入资料库。
请求已自动改名为 `.completed`，下次启动不会再次采集；不需要重复以下历史采集步骤。
后续真实转换包与定向实机验证见[庄方宜验证记录](CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION_20260917.md)。

## 用户操作

探测版覆盖测试目录的 `modules/BetterEndfield.CustomModel.dll`，无需新增 Mod 包。
从 `E:\Dr.Hydra\Better Endfield\artifacts\BetterEndfield-win-x64\BetterEndfield.exe` 正常启动游戏。
必须是装好探测版后新启动的游戏进程，以捕获首次资源交付。

首轮实际未生成 JSONL；历史公共日志也没有保留探针启用记录，不能据此断定具体原因。
已通过实际部署 DLL 的导出入口验证真实请求能被读取，离线测试在缺少游戏托管方法时预期停止，
不把此测试记作实机初始化成功。

新版在 `catalog/custom-model/native-probe.status.json` 独立记录实际 DLL 路径、进程 ID、
请求状态、回调计数及最多 32 个角色模型资源名。下一轮先启动游戏并由开发侧确认状态
`ready` 或后续 `receiving_resources`，再执行角色操作；不要在探针尚未就绪时盲目重复采集。
运行时附加暂未使用。

后续实机状态已定位断点：`contract_unavailable / probe.texture_names`，实际 DLL 路径正确，
客户端没有暴露 `Material.GetTexturePropertyNames()`。现已移除这个契约和字符串版 GetTexture，
改用生产替换路径已解析成功的 `GetTexturePropertyNameIDs()` 和 `GetTexture(Int32)`。
状态 build 标识为 `native-probe-property-ids-v2`；此修复需重启游戏加载新 DLL。

1. 把庄方宜加入编队，进入大世界，切换到她并停留约 5 秒。
2. 打开庄方宜角色详情页，让人物模型显示，停留约 5 秒。
3. 告知采集操作已完成；无需截图、快捷键或手工导出文件。

先详情再世界也可以。等待只是确保加载完成，并非定时采样；两个资源各成功采集一次即停用。
本轮只采集资料，不要求判断替换效果，不重测其他角色、LOD 或懒加载。

## 行为与边界

开关是 catalog/custom-model 下的 `native-probe.request`。无此文件时默认不启用，
探针专用托管方法也不解析。配置由离线资料生成，最多两种资源、16 个明确 renderer。
本次只选庄方宜 world/UI 的头发、皮肤、衣服 1、衣服 2，各四件。

在既有 `_FinishWithAsset` 交付点、任何模型替换之前读取。只匹配精确资源名、renderer 路径、
Mesh 名及原索引数，不使用全局扫描，不安装 D3D 绘制拦截，不调用模型/材质 setter。
仅将资源根的 Unity `(Clone)` 后缀规范化，与已有 Mod 路由一致，子节点保持原样；
JSONL 另存 `observed_resource_root`，保留真实观察到的根名称。
一次交付内的临时托管引用在结束时释放，不长期持有角色对象。

采集内容：完整 VertexAttributeDescriptor、每流 stride、顶点/索引数量、原骨骼顺序及路径、
材质槽顺序、Shader 名、纹理属性 ID 和当前 Texture 名/尺寸/格式。`property` 字段为 null，
保留实际 `property_id`；属性名称继续由离线材质资料提供，不能猜测 ID→名称映射。
Unity 材质属性 ID 不是 D3D `ps-t` 槽号，本探针不会伪造 GPU 纹理 hash 或资源槽的对应关系。
这些结果先解决原生顶点布局和材质身份，GPU 槽/hash 仍需另有证据。

每种资源最多尝试三次交付；成功后去重。两种都成功后把 request 改名为 `<run>.completed`，
同一进程也立即关闭采集。输出已存在的 run ID 不会跨游戏进程追加，避免混入旧观测。
失败保留不完整记录，不自动标记转换完成。

## 开发侧命令

```powershell
python tools/CustomModel/runtime_native_probe.py arm --database artifacts/native-parser/zhuangfangyi-database.json --preparation artifacts/native-parser/zhuangfangyi-profile-preparation.json
```

自动生成唯一 run ID，输出位于：
`%LOCALAPPDATA%\BetterEndfield\catalog\custom-model\native-probe\<run>.jsonl`。
并列 `<run>.request.json` 保存本次预期对象身份与离线提取快照版本。

采集后用实际 run ID 执行：

```powershell
python tools/CustomModel/runtime_native_probe.py collect --database artifacts/native-parser/zhuangfangyi-database.json --run RUN_ID --output artifacts/native-parser/zhuangfangyi-observed.json
python tools/CustomModel/parse_native_models.py artifacts/native-parser/zhuangfangyi-raw.json --observations artifacts/native-parser/zhuangfangyi-observed.json --output artifacts/native-parser/zhuangfangyi-database.json
```

收集器检查版本快照、单一进程、八个对象完整性、Mesh/路径/索引数、骨骼名称与路径顺序、
材质槽身份，以及顶点声明与 stride 的一致性。缺项/重复/身份变化拒绝合并。
输出的 manifest_version 是本次探测请求所依据的离线快照版本，不是游戏内实时查询；
若这期间游戏更新，应重新提取并生成请求。运行时采集成功也不自动批准纹理映射或生成实机包。

arm 会将独立状态重置为 `awaiting_game`（进程 ID 为 0），避免把上轮状态或离线测试状态误认成游戏结果。
`initializing`、`armed` 还不等于探针就绪；`ready` 表示资源 hook 安装完成；
`captured`/`complete` 才表示至少一种/全部目标资源的数据已写入。

验证范围：生产探针的定向匹配、JSON 转义、读取、去重、自动停用和临时引用释放，
以及 Python 请求/收集器的四项针对性测试。用户已完成上述实机采集，八个目标均已收齐。
