# 旧模型资源释放实验

- PC、安卓第三方模型实验区：**实验：释放旧模型资源**。
- 默认关闭，各实验开关独立，重启游戏后生效。
- 配置项：[CustomModel] release_retired_resources=true；安卓偏好为 bem_release_retired_resources。
- 仅在替换事务成功发布后，将上一代由本模块创建的 Mesh、Material、Texture 放入待释放队列。
- 等待至少 1000 ms，在游戏线程调用对象销毁。模型构建任务未结束时暂缓。
- 已知当前发布实例仍使用的 Mesh、材质、纹理继续保留；共享纹理在之后不再使用时再尝试释放。
- 原版游戏资源和游戏创建的材质副本不属于销毁候选；此实验未引入原版资源重载策略。
- 此实验假设短暂延迟后未被观察到的旧实例已不再使用资源，尚未证明游戏对象池或其他隐藏引用均已退出。
- 关闭开关并重启可恢复原有策略；已经销毁的对象无法通过运行中关闭开关恢复。

## 反馈与日志

日志前缀：[RetiredResourceCleanup]。

- enabled=1 delayMs=1000：本次进程已开启实验。
- queued role=... selection=... objects=...：成功替换后进入待释放队列。
- released=... alreadyGone=... currentShared=... failed=... pending=...：实际释放及保留数量；不是内存/显存释放字节数。
- deferred: current binding inspection unavailable：无法可靠读取当前绑定，本轮没有销毁。

建议反馈平台、实验开关状态、切换的包、发生在开局/热切换/切主控/关闭哪个阶段，并附对应日志。重点观察模型或材质丢失、切主控异常、闪退，以及多轮切换后的内存趋势。
