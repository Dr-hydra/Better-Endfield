# BetterEndfield.CustomModel Runtime Probe

状态：Windows 只读研究探针  
分支：`dev/custom-model-design`  
模块：`BetterEndfield.CustomModel` (`0.0.2-probe`)

## 目的

该探针用于确认 EFMI `Endmin Casualwear` 样本与当前游戏运行时 `SkinnedMeshRenderer` / `Mesh` / Skeleton 的对应关系。

它不会修改 Mesh、Renderer、Material、Skeleton 或游戏资源，只在用户按下热键时读取 Unity 对象并写 Better Endfield 日志。

## 第一轮测试结论

2026-09-10 第一轮实机日志确认：

- CustomModel 模块能够正常加载；
- `SkinnedMeshRenderer` / `Mesh` / `Renderer` / `Resources` 所需接口全部能通过 metadata contract 解析；
- `CameraMono.EvaluateAllTouchedEntities()` Hook 安装成功；
- 但该方法并不是当前测试场景可靠的逐帧入口，因此 F8 检测没有执行到，日志中没有出现 dump begin/end。

因此 `0.0.2-probe` 改为：

1. 后台 Win32 线程只负责检测 F8 边沿并锁存 `dump_requested`；
2. Unity managed object 枚举仍只在游戏主线程执行；
3. 测试环境关闭其他 Better Endfield 功能模块时，优先复用 `BetterEndfield.Camera` 已验证稳定的 `CameraMono._ProcessDitherByPitch()` 主线程入口；
4. `EvaluateAllTouchedEntities()` 与 `_ProcessDitherByTrace()` 只作为后备入口。

这种“后台线程锁存热键 + 游戏主线程消费请求”的方式与仓库现有 BuffProbe 一致，可避免主线程回调频率不足导致短按键被漏掉。

## 构建

`BetterEndfield.CustomModel` 已加入 `native/CMakeLists.txt` 的默认 Windows 构建与 stage 布局。

正常构建当前分支后，stage 应包含：

```text
modules/BetterEndfield.CustomModel.dll
modules/betterendfield.custom_model.module.ini
```

模块描述符默认启用，研究版不需要额外 UI 开关。

## 推荐测试环境

这轮为了优先拿到模型数据，建议只启用：

```text
BetterEndfield.Host
BetterEndfield.CustomModel
```

其他功能模块可暂时移出 `modules/` 或设为禁用，尤其是 `BetterEndfield.Camera`，因为正式 Camera 模块同样会占用 `_ProcessDitherByPitch()`。

这是研究版测试约束，不代表正式 `CustomModel` 最终会依赖该 Hook；正式实现会在确认 Character/Renderer 生命周期后选择自己的稳定触发机制。

## 使用

1. 使用 `dev/custom-model-design` 最新提交构建并启动 Better Endfield。
2. 暂时关闭其他功能模块。
3. 正常进入游戏，使 **Endministrator (F)** 当前角色模型已经实例化并可见。
4. 保持游戏窗口处于前台。
5. 按一次 `F8`。
6. 日志首先应出现：

```text
Probe main-thread pump installed: pump.process_dither_pitch
Probe main-thread pump observed first runtime call.
```

7. 按 F8 后应出现：

```text
F8 dump request consumed on Unity main thread.
=== CustomModel probe dump begin; target_hint=endminf; sample=Endmin Casualwear ===
...
=== CustomModel probe dump end ===
```

8. 退出游戏后，把以下日志文件发回用于分析：

```text
%LocalAppData%\BetterEndfield\logs\BetterEndfield.log
```

如果要减少无关历史内容，也可以只截取本次启动时所有 `[betterendfield.custom_model]` 行。

## 输出内容

探针通过：

```text
UnityEngine.Resources.FindObjectsOfTypeAll(typeof(SkinnedMeshRenderer))
```

扫描当前已加载的 `SkinnedMeshRenderer`。

默认只详细输出 Renderer 名称、Mesh 名称、Transform 层级路径或 rootBone 名称中包含 `endminf` 的候选。

每个候选输出：

- Renderer name / instance ID / enabled；
- Transform hierarchy path；
- rootBone name；
- sharedMesh name；
- vertexCount；
- subMeshCount；
- bindpose count；
- index format；
- isReadable；
- 每个 submesh 的 indexCount；
- 每个 submesh 的 baseVertex；
- sharedMaterials 名称；
- 完整 `bones[i] -> Transform.name` 表。

对 `Endmin Casualwear` 样本，探针还会把 submesh index count 与样本的 11 个原始 EFMI Component 做精确比较：

```text
C0   27615
C1    9000
C2    4524
C3   20577
C4    1638
C5   16524
C6     117
C7    1386
C8      90
C9  101994
C10   2286
```

完全相等时日志显示：

```text
EFMI_EXACT=Cx
```

这些数值仅用于当前研究样本的诊断，不会作为未来 CustomModel 的稳定身份键。

## 没找到 endminf 时

如果没有任何 Renderer 匹配 `endminf`，探针不会直接失败，而是输出所有已加载 `SkinnedMeshRenderer` 的轻量摘要：

```text
renderer name
mesh name
root bone name
transform path
```

这样可以判断当前版本是否改了命名，或者按 F8 时女管理员模型尚未加载。

## 主线程触发点

`0.0.2-probe` 的 Hook 优先级：

```text
1. Beyond.Gameplay.View.CameraMono._ProcessDitherByPitch()
2. Beyond.Gameplay.View.CameraMono.EvaluateAllTouchedEntities()
3. Beyond.Gameplay.View.CameraMono._ProcessDitherByTrace()
```

热键检测本身不再依赖这些回调的频率。后台线程只设置原子请求位；所有 Unity API 调用都由上述主线程 Hook 消费。

## 本次日志要回答的问题

拿到一次有效 dump 后，应优先回答：

1. EFMI Component 0..10 与 Unity Renderer/submesh 的关系；
2. EFMI Component 是否对应独立 Renderer，还是多个 Component 被装在同一个 Mesh 的多个 submesh；
3. Component 9 的 7 个自定义 draw range 应该映射为一个还是多个 Unity submesh；
4. 对应 Renderer 的 `bones.Length` 是否覆盖 EFMI 最大 bone index `155`；
5. EFMI `BLENDINDICES` 是否可以直接解释为对应 Renderer 的 `bones[index]`；
6. `bindposes.Length` 与 `bones.Length` / Mesh 结构的关系；
7. Material 数量和 submesh 数量是否足以建立第一版纹理/材质映射。

这些结论确认后，再冻结 BEM v1 的 Renderer identity、Component/submesh mapping 和 skeleton binding 字段。
