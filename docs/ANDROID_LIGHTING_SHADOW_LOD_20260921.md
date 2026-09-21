# Android 光照、阴影和 AI LOD 诊断

## 用户反馈

关闭上一轮全局 LOD/裁剪覆盖后，世界与详情替换可见。替换衣服在世界受光时为白色，详情偏灰；原版详情有光照，因此不能归因于 Android 本来不提供详情光照。

## 材质诊断

`model-rollback-20260921-022523/runtime.log` 记录材质复制前后的 shader、render queue、完整 shader keyword 序列及已存在的常用标量/颜色属性一致。衣服 `_D` 原始 ASTC 与替换 BC7 均为 sRGB，`_N`、`_P` 均为 Linear。该检查不包含所有自定义 shader 参数，也不证明 normal map 通道编码、运行时光照控制与原版一致。

不得用调亮贴图或材质颜色掩盖未定位的差异；后续需做同详情场景下的单变量对照。

## 世界阴影

参照 PC 历史 `module_ai_shadow_coverage.inc` 的独立阴影策略：替换的 LOD1 mesh 开启 ShadowCastingMode.On，清空其单独 shadowProxyMesh 使投影使用新 mesh，关闭当前角色对应的 `Shadow_Proxy/SP_Mobile/*_shadowProxyMobile`。隐藏的 BEM 部件也关闭对应代理。

所有状态进入同一资源事务，保存并回读原始 shadow mode、proxy mesh、enabled，失败及 rollback 测试时逐项恢复。已完成记录额外校验阴影状态，避免只凭 mesh/material 身份认为完成。

当前测试包匹配 7 个移动端阴影代理；世界事务共 18 个绑定（11 个主体与 7 个代理），详情为 11 个。上述日志确认提交/恢复均通过。画面投影形状和角色切换后的维持仍须用户验证。

用户随后确认影子问题解决。详情仍可见但光照未改善；AI 拉远仍回原版，不能把当时的 pipeline-only PASS 当作 AI 问题解决。

## 管线级 LOD

PC 的 `NPCCrowdLOD.LockLod` 已有历史失败试验，不按“接口存在”宣称可用。本轮写出的单角色试验代码已撤下。

读取 Android 当前进程具名入口后，`pipeline-disassembly.txt` 显示 `HGRenderPipeline.EnableForceLOD0` 把 parent bias 和 art tag 0..34 的 bias 设为浮点 1e-7，`DisableForceLOD0` 转回 `RegisterArtTagLODBias`。行为与 PC 管线机制一致。没有按 PC RVA 写 Android 地址。

调试配置 `lod_pipeline=1` 只开启管线 bias，保留游戏原有 QualitySettings.maximumLODLevel、NPC 距离/数量、camera/physics culling。新 pipeline 实例重新应用；关闭时先解锁 bias，再调用 DisableForceLOD0 注册游戏参数。默认未设置该诊断选项时不启用管线覆盖。

`model-rollback-20260921-023206/runtime.log`（PID 6690）确认管线调用、世界和详情绑定/回滚通过。PC DLL/BindingTests 编译与 synthetic tests 通过。调用成功尚不证明 AI 队友实际渲染层级稳定，需切换控制角色、距离变化和详情页复测。

### 持续维护与 NPC 参数隔离测试

当前 Android 的 RegisterArtTagLODBias 编译代码直接调用 native icall，可能绕过对托管 setter 的拦截。增加 RegisterArtTagLODBias 完成后的管线重新应用；DisableForceLOD0 前先解除锁定，恢复路径不会被拦回。

`lod_npc=1` 在管线模式下另行启用 PC 的五项 NPC 参数维护，每项通过字段元数据定位，保存原值、写入后读回并在停止时恢复；仍不修改 QualitySettings.maximumLODLevel。该选项用于进一步验证 AI 路径，不按配置成功宣称画面解决。

`model-rollback-20260921-024448/runtime.log`（PID 12049）捕获游戏多次重新注册 bias，并确认维护点 PASS；世界/详情事务、五项 NPC 参数恢复与重新应用均 PASS。`inspect=1` 可采集可见目标 renderer 的真实 LOD 与 custom/source mesh 计数，以及原始衣服法线的 GPU 采样。

PC 测试包衣服法线 BC7 解码 RGBA 均值为 `[122.855,126.616,0.009,255.0]`，明显为 RG 数据。需要与 Android 原始法线和 shader 读取方式核对，尚不能仅凭这个统计确认光照根因。

`model-replace-20260921-024633/runtime.log`（PID 12907）采样 Android 原法线得到 RGBA 均值 `[0.491068,0.494989,0.976035,1]`，原版存储包含正 Z 通道，与 PC Mod 的 RG-only 数据不同。下一步只对该法线重建正 Z 做可回退的对照，保留颜色贴图和其他资产。

用户已确认这版“管线注册后持续维护 + NPC 参数、QualitySettings 不变”解决 AI 角色拉远变回原模型的问题。阴影也已确认解决。光照仍未通过用户验收；其他三个角色尚待独立验证。

## 当前限制

世界仍只修改 LOD1，没有生成低模。管线测试的目标是让实际渲染选择已适配的最高可用层级；若 Android 仍选择未适配层级，需要继续查该管线的调用和覆盖关系，不能把 LockLod 或 getter 回读当作画面成功。

## 三角色实机验收

后续实机验证覆盖管理员、佩丽卡、庄方宜和洁尔佩塔。管理员与佩丽卡的双角色同时替换通过；佩丽卡详情和大世界的帽子、衣服材质均完整清晰。庄方宜与洁尔佩塔在多包配置下同时替换通过，用户复查确认庄方宜衣服颜色和洁尔佩塔服装均无异常。

多角色配置按包分别建立资源事务，并为每个包独立解析默认外观；未显式指定外观时不会把首个包的外观选择带给后续角色。庄方宜 v25 包的原始 draw、材质槽、骨骼和索引语义已通过迁移校验，Android Gradle、PC binding tests 和本地模拟器运行验证均通过。
