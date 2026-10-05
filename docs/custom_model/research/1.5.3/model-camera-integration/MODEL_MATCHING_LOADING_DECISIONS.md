# 本轮方案结论（2026-10-03）

仅调研、讨论和文档整理，没有新增生产代码或配置改动、构建和安装。三个子任务已并行完成，第一人称由主代理直接读手机日志排查。

- **通用匹配**：放宽 Renderer 必须等于 Mesh 名的假设，按实际 sharedMesh/已保存 Original 定位，并保留唯一性、布局、骨骼、材质、索引和角色/LOD 边界校验。跨平台、跨 LOD 差异表按真实对象引用生成，作为后备，不继续逐角色写 if 或一律去数字后缀。详见 `../matching-identity/GENERIC_MODEL_MATCHING_DESIGN.md`。
- **四角色加载**：一个全局队列管理最多四个队伍角色及当前详情目标；后台解压按共同内存额度控制，Unity 创建和上传按全局帧预算推进。当前角色优先，其余角色分批预热，同包同选择请求合并，换队取消过期任务。当前大纹理单次 Apply 仍可能长帧，真实 GPU 在途量也不是只等两帧便能确认。四套样本约 1.45GiB 必要贴图量不会因分帧消失。详见 `../loading-memory/TEAM_MODEL_LOADING_DESIGN.md`。
- **UI 复用**：优先复用经过兼容验证的存活 Mesh/Texture，实例自己的 bones 和可变材质状态重新绑定。缓存命中时避免重复解压和 GPU 构建，游戏仍可正常重建 UI 展示对象。仅额外闲置留存设短 TTL/全局 LRU 额度；必要活跃资产单独记账，不能用过小固定缓存门槛阻止四角色完成加载。真正重复的 UI donor Load 可再用短 tracked handle lease 合并，避免永久强持有完整 prefab。详见 `../ui-model-cache/UI_MODEL_CACHE_DESIGN.md`。
- **逐角色第一人称**：已实现通用 Head/tail 分类和实际权重裁剪，但完整逐角色额外骨骼表尚未实现。手机当前提弗洛斯 face/hair 有 clone/index bindings unavailable，裁剪被门槛跳过，尚未查询 BEM CPU 几何。这一执行缺口需要先修复，之后补角色帽骨路径和局部 Neck 边界；只增加名单不能让不可用的索引写入接口工作。当前安装索引状态不能证明此前测试会话的包选择，旧包统计不当成现场唯一根因。详见 `../../../../camera/research/1.5.3/first-person-typhoea-campus/FIRST_PERSON_TYPHOEA_CAMPUS_DIAGNOSIS.md`。

后续实施优先级：安卓裁剪适配及提弗洛斯角色规则；通用定位；成品复用；四角色分帧调度。后两项共享任务/资产所有权设计，避免另起互相不知情的缓存和上传队列。
