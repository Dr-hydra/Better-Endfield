# BetterEndfield.CustomModel Runtime Probe

状态：Windows 只读研究探针  
分支：`dev/custom-model-design`  
模块：`BetterEndfield.CustomModel` (`0.0.1-probe`)

## 目的

该探针用于确认 EFMI `Endmin Casualwear` 样本与当前游戏运行时 `SkinnedMeshRenderer` / `Mesh` / Skeleton 的对应关系。

它不会修改 Mesh、Renderer、Material、Skeleton 或游戏资源，只在用户按下热键时读取 Unity 对象并写 Better Endfield 日志。

## 构建

`BetterEndfield.CustomModel` 已加入 `native/CMakeLists.txt` 的默认 Windows 构建与 stage 布局。

正常构建当前分支后，stage 应包含：

```text
modules/BetterEndfield.CustomModel.dll
modules/betterendfield.custom_model.module.ini
```

模块描述符默认启用，研究版不需要额外 UI 开关。

## 使用

1. 使用 `dev/custom-model-design` 构建并启动 Better Endfield。
2. 正常进入游戏，使 **Endministrator (F)** 的当前角色模型已经实例化并可见。
3. 保持游戏窗口处于前台。
4. 按一次 `F8`。
5. 等日志出现：

```text
=== CustomModel probe dump begin; target_hint=endminf; sample=Endmin Casualwear ===
...
=== CustomModel probe dump end ===
```

6. 退出游戏后，把以下日志文件发回用于分析：

```text
%LocalAppData%\BetterEndfield\logs\BetterEndfield.log
```

如果要减少无关历史内容，也可以只截取上述 begin/end 标记之间，以及本次启动时所有 `[betterendfield.custom_model]` 行。

## 输出内容

探针首先通过：

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
- isReadable（API 可用时）；
- 每个 submesh 的 indexCount；
- 每个 submesh 的 baseVertex（API 可用时）；
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

完全相等时会在日志中显示：

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

探针必须在 Unity 主线程执行 managed object 枚举。

它优先 Hook：

```text
Beyond.Gameplay.View.CameraMono.EvaluateAllTouchedEntities()
```

如果该 Hook 入口无法安装（例如将来被其他模块占用），则退化到：

```text
Beyond.Gameplay.View.CameraMono._ProcessDitherByTrace()
```

现有 `BetterEndfield.Camera` 使用的是 `_ProcessDitherByPitch()`，因此当前设计不会与 Camera 模块争用同一个 HookBroker target。

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
