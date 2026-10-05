# 2026-09-14 实例化漏点

0.0.36 两次采样：管理员在队伍及不在队伍均在启动后约 22 秒交付 postmodel。后者在管理员 UI 出现前约 35 秒交付，不能将管理员预加载归因于队伍。安洁在队伍时于 gameplay 创建前交付。SMS 目标事件均为零，不能据此断言没有调用。

局部 IDA：仅新增 Object.Instantiate(Object,Vector3,Quaternion,Transform)，RVA 03BD3740，结果见 instantiate-route.json；沿用此前 PrefabInstantiateProxy._PreloadPrefab 导出。

- _PreloadPrefab 在 02F67292 调用 03BD3740，父节点来自 PrefabInstantiateProxyManager.get_s_prefabParent。
- 03BD3740 入口将 RCX 保存为原对象、RDX/R8 保存为位置/旋转地址、R9 保存为父节点。
- 正常非空父节点路径在 03BD3809 调用缓存引擎 icall 指针。该指针由 03BD38CF 解析并在 03BD38DD 缓存。
- 这条路径不调用目前已挂钩的两个 Internal_CloneSingle 入口。空父节点分支另外调用 Instantiate 重载，未声称覆盖。

0.0.37 新增上述公开重载的 metadata hook；原调用返回后只输出 endminf/aglina 对象和部件。没有修改原参数或原结果。另对 SMS 前 8 次调用输出未筛选入口/出口，以区分未命中和名称过滤。构建通过；尚待实机确认该路由是否对应本轮 gameplay postmodel 实例。

尚未证明：CreateNpcComponentByPostModel 静态实现是否为实际 gameplay 路径、IFix 分派是否改变装配路径、交付对象与实例是否可逐一关联。不能据已有数据实施通用替换。
