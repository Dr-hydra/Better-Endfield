# PC 模型热切换停顿调查与第一阶段优化

工作区 `G:\Better Endfield`，分支 `dev/bem-1.4-weapons-forms`。调查基线为 `76db3dbe`。用户已确认武器、大招加载和 PC 主世界热切换生效，并明确卡顿集中在切换阶段，完成后恢复。

## 实机证据

本次游戏会话日志保存于 `build/bem14/local-test/audit/hot-switch-20261006/runtime-stutter.log`，共 731 行，20:52:52–20:54:44。没有用旧会话的错误推断本次停顿。

| 操作 | 观察 | 含义 |
| --- | --- | --- |
| 20:54:14 禁用佩丽卡 | 20:54:15 模板恢复；20:54:20 根诊断结束；20:54:22 其他 renderer 诊断结束；20:54:25 扫描结束，共 19,892 个 renderer | 日志显示扫描相关阶段约十秒，属于切换阶段的明显热点；旧日志未逐函数计时，不能精确拆分各函数占比 |
| 20:54:42 再启用 | 模板分帧 Job 入队后，被实例同步重绑取消；扫描 19,551 个 renderer | 当前实例路径优先使用同步事务，未接入已有分帧上传 |
| 20:54:44 Clone 提交 | `elapsedMs=828`、`uploadFrames=1`、`maxFrameUploadBytes=69473332`、17 张纹理、10 个网格、10 次 render sync | 一个事务在同一帧处理约 66.3 MiB 上传，其中纹理 64 MiB；828ms 不包含前面的场景扫描 |

## 代码原因

1. `ScanSceneInstancesForRebind` 的 stale 分支先执行两轮只用于输出诊断的全场遍历，再执行真正的 stale Mesh 查找。第一轮逐 renderer 遍历祖先、读取名称；第二轮虽然最多打印 40 条，但没有匹配时仍查遍所有 renderer。本次打印数为零。两轮诊断来自既有 `5f82b3ca`，首次启用修复又增加了变更资源的发现路径。
2. `ConstructionScope::Root` 通过线性扫描整个 `roots` vector 去重。`Invoke` 无差别持有返回对象，`InvokeValue` 的 boxed 标量也经过该函数。整个扫描处在同一个 pump scope 内，K 个不同返回对象可累计 K(K-1)/2 次比较。这是代码可证明的复杂度问题；其实际耗时占比尚未单独测量。
3. `PumpInstanceRebind` 将完整 `ProcessResource` 视作一个 `{bytes=0, steps=1, heavy=true}` 步骤，扫描还发生在申请预算之前。预算只能决定是否开始，不能中途暂停同步事务，所以“一帧一个 root”仍然可能长时间阻塞。
4. 当前 `fast_loading=false` 的同步纹理路径按累计约 4 MiB 发起 render-thread sync，本次共 10 次。提高同步阈值只能缓解这一部分等待，还会改变上传瞬时内存占用，不能解决扫描和整包同帧处理。

本次 `textureLiveReuse=0` 符合当前活资源借用策略：停用已把模板与 Clone 恢复为 Original，重新启用时没有当前绑定且身份/采样器匹配的候选；代码不强持有闲置纹理。它不证明旧纹理已被 Unity 销毁。事务内去重正常：33 个引用合并为 17 张纹理。CPU 缓存也只保留几何与元数据，TTL 为三秒，不能代替闲置 GPU 资产缓存。

## 本轮已完成的优化

- 保留 `roots` 的 GC 句柄释放列表，增加 `rooted_objects` 哈希集合执行去重；只在成功取得句柄后插入。句柄失败仍置 `failed`，异常时已取得句柄仍在释放列表中。
- 删除两轮纯诊断扫描，保留实际 stale Mesh 定位、首次启用发现、完整 donor 校验、入队和提交/恢复逻辑。
- 场景扫描摘要增加 `elapsedMs`，便于下一次实测。它仅覆盖扫描函数到日志输出前，不包含外层 scope 的集中释放、模板恢复和后续上传，不能当作整帧停顿时长。

这两项属于第一阶段止损。实际扫描仍同步进行，临时句柄仍在 scope 结束时集中释放，实例替换仍同步上传；本轮没有声称已经实现无卡顿切换。

## 后续优化顺序

1. 扫描按批次使用独立只读 scope，及时释放名称、boxed 标量和临时 Transform；外层保留 FindAll 数组，入队 root 使用独立强引用。进一步把扫描拆为有预算的跨帧任务，在当前批次复用祖先归属查询。跨批/跨帧不能保留无所有权的裸指针，revision 变化必须重新收敛待发现资源。
2. 启用/换包接入现有 `ModelBuildJob`：后台执行文件读取和纯数据解码，Unity 对象查询、构建、上传、读回和恢复仍在游戏线程。保留旧外观直到新资源准备完毕后统一提交；停用采用 Original 恢复。同步队列门禁需改为统一任务调度，避免自己的 pending 记录挡住自己的 Job。
3. 保留同角色互斥、最新 revision 取消、绑定漂移检查、原子提交/回滚和 `desired/verified_selection/failed_selection` 状态更新。新产生的对象池实例也须覆盖，不能为了分帧丢掉自然 Clone。
4. 如果往返切换仍有明显重建成本，再设计有字节预算和 TTL 的闲置资产缓存，按内容、采样器、donor/receiver 与骨骼兼容身份复用，不永久强持有每个历史模型。

分帧预算限制的是下一步的准入，不能保证单次 Unity 调用在两毫秒内完成。尤其本次最大单张纹理为 16 MiB，其他包存在 36 MiB 纹理；单次 Apply 仍须测量。不能直接把整套 Unity API 移到后台线程，也不能通过关闭验证解决此问题。

## 验证与交付

Windows 模块及 binding 测试构建成功。默认完整回归（包含 scene rebind、类型边界、静态资源）、async 回归、first-enable 十场景均通过；覆盖提交、Original 恢复、连续 revision、取消调度和 GC 句柄清理。Android `externalNativeBuildRelease` 成功，保留 26 条既有警告，未修改 LOD1 适配、设置键或 UI。

日志为本轮审计目录下的 `stutter-windows-build.log`、`stutter-android-build.log`、`stutter-binding.log`、`stutter-async.log`、`stutter-first-enable.log`。未计算产物哈希。

2026-10-06，用户在第一阶段优化版本 `9a0b882` 部署后反馈“现在这个切换速度已经还行了”。本轮实机切换速度已获用户验收；此结论来自实际游玩反馈，没有采集量化帧耗时或消除全部上传尖峰的证据。后续分帧改造保留为优化方案。
