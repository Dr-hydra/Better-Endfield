# 终末地运行时 NPC LOD 锁定与发光部件治理逆向调研报告（2026-09-14）

状态：**原生渲染管线锁定已接入工作树源码；下文早期 Hook 调研作为历史记录保留**

分支：`dev/custom-model-design`

涉及模块：`BetterEndfield.CustomModel`

涉及源码文件：
- `native/modules/custom_model/module_npc_lod_lock.inc`
- `native/modules/custom_model/module_ai_shadow_coverage.inc`
- `native/modules/custom_model/module_poc2_part_02.inc`
- `native/modules/custom_model/module_poc2_part_03.inc`

---

## 0. 当前方案校正（2026-09-15）

当前采用并保留 **HG 原生渲染管线 LOD0 锁定 + NPC 距离与裁剪参数调整**。实际源码调用链为：

```text
PumpAiShadowCoverage()
  → PumpNpcLodLock()
  → ApplyLockedCrowdLODs()  [模型替换激活时]
      → HGRenderPipeline.EnableForceLOD0()
      → QualitySettings.maximumLODLevel = 0
      → NPCCrowdLODSetting 静态参数调整
```

NPC 参数设置为距离 `1000 / 1000`、最大渲染数量 `100`、裁剪索引 `0`、物理裁剪关闭，作用于全局设置。`InstallNpcLodLockHooks()` 当前仅输出初始化日志，不安装第 4 节记录的逐实例 Hook，也不再通过猜测的对象指针调用 `LODGroup.ForceLOD()`。

跨 LOD 骨骼补齐、材质同步、散件及 VFX 治理代码同时保留，属于辅助覆盖实现。保留当前锁定方案是本次确认的方向，不再按旧文档要求清空 `module_npc_lod_lock.inc`。

替换停用或模块卸载时走 `ResumeLockedCrowdLODs()`，调用 `DisableForceLOD0()` 并回写已保存的 NPC 参数。当前源码尚未保存/恢复 `maximumLODLevel` 原值和启用前的管线锁定状态，距离字段也未逐项独立保存，不能宣称全局状态已无损还原。详细范围见 [当前方案与骨骼补齐记录第 6 节](CUSTOM_MODEL_MULTI_LOD_BONE_ELEVATION_20260915.md#6-当前原生管线锁定实现)。

**阅读范围**：第 1—7 节保留早期问题、Hook 实验及当时的验证记录，不代表当前实现。历史编译、部署记录不能证明当前 DLL 已加载；本次只核对源码并修正文档，未进行新的游戏验证。

---

## 1. 背景与问题陈述

在自定义模型系统（`BetterEndfield.CustomModel`）应用第三方模型（以休闲服女管理员 `endmin in casualwear` 为基准测试样本）的过程中，暴露了三个严重的视觉与渲染缺陷：

1. **现象 A：“变成奇怪的模型”（骨骼网格严重扭曲）**：
   - 休闲服 Mod 仅为最高精度级别（LOD0）制作了替换网格（156 根骨骼绑定）。
   - 当相机拉远或切换至其他角色跑开时，终末地引擎底层自动将角色降级到 LOD1/2/3。低精度级别网格不仅带有原版黑色战术大衣，且骨骼树被精简至约 40 根。若强行将 LOD0 的网格绑定到低精度渲染器上，会产生严重的网格拉扯与破面。
2. **现象 B：“低模不渲染 / 远了直接人没了”**：
   - 在初步修复中，为了杜绝低模黑大衣的穿模，我们在后台循环中将非 `_lod0` 渲染器全部执行了 `SetRendererEnabled(false)`。
   - 但当距离拉远时，引擎原生的 LOD 调度逻辑将 LOD0 渲染器也关掉了（`NPCAvatar.SetLodVisible(1)`）。
   - **引擎关掉了高模，模块关掉了低模**，导致角色全身所有 SkinnedMeshRenderer 均为关闭状态，角色瞬间在屏幕中完全隐形消失。
3. **现象 C：“发光部件没有被隐藏或者替换”**：
   - 女管理员原版黑色大衣背部/胸前带有科技发光信标/电池部件（`S_actor_endminf_vfxpart_01_lod0`）。
   - 休闲服 Mod 去除了大衣，且未制作该部件的替代品（无替代网格）。
   - 在后台遍历中，原本存在“保障未隐藏的 LOD0 部件处于激活状态”的逻辑，导致该原版发光部件被反复强制点亮，孤立浮空悬挂在 T 恤外面发光。

---

## 2. 终末地 NPC LOD 与渲染调度体系逆向分析

通过对 `Gameplay.Beyond.dll` 及游戏运行时内存的反编译与调试追踪，确认了终末地引擎对 NPC / 角色实例的完整 LOD 控制链条：

### 2.1 核心组件分工

```text
NPCCrowdEntityComponent (NPC 实体总控组件)
  ├── 0x140: avatar (Beyond.NPC.Avatar.NPCAvatar)  -> 掌管网格部件可见性、Animator、LODGroup、骨骼配置
  └── 0x170: lod    (Beyond.NPC.Lod.NPCCrowdLOD)    -> 掌管距离计算、LOD 分级、相机距离剔除、模型卸载
```

### 2.2 `NPCAvatar` 内存结构与调度机制

- **类命名空间**：`Beyond.NPC.Avatar.NPCAvatar : Beyond.Gameplay.View.IModelComponent`
- **关键字段偏移**：
  - `+0x18`: `m_npcComponent` (`NPCCrowdEntityComponent*`)
  - `+0x20`: `avatarParams` (`FNpcAvatarGenericParams` 结构体，内含 `id`, `correspondingCharId`, `avatarMeshName` 等托管字符串指针)
  - `+0xF0`: `avatarGoRef` (`FNPCAvatarGOReference` 结构体，内含 `go` 在 `+0x110`/`0x120`，`lodGroup` 在 `+0x130`/`0x140`)
  - `+0x1F0`: `bIsSelfRenderVisible` (`bool`)
  - `+0x1F1`: `bActiveInHierarchy` (`bool`)
  - `+0x210`: `m_npcLodVisible` (`int32_t`，当前生效的 LOD 层级，`0` 为 LOD0)
- **关键控制方法**：
  - `SetLodVisible(int32_t lodVisibleData)`：底层网格开关咽喉。传入 `0` 时开启 LOD0 部件并关闭低精度部件；传入 `1/2/3` 时关闭 LOD0 部件。
  - `SetLodVisibleWithFade(int32_t lodVisibleData, float fadeTime)`：带淡入淡出的 LOD 可见性控制。
  - `ForceSetLod(int32_t index)`：强制指定层级。
  - `_SetPartEnable`：内部调用 `<_SetPartEnable>g__SetSkinMeshRendersEnable`，通过改变 `Renderer.enabled` 切换部件显示，**并不销毁或禁用 GameObject**。

### 2.3 `NPCCrowdLOD` / `NpcCrowdLODDowngrade` 距离与裁剪逻辑

- **类命名空间**：`Beyond.NPC.Lod.NPCCrowdLOD` 及派生类 `NpcCrowdLODDowngrade`
- **关键字段偏移**：
  - `+0x18`: `renderLodIdx` (`int32_t`，当前决策的 LOD 索引)
  - `+0x20`: `npcComp` (`NPCCrowdEntityComponent*`)
  - `+0x38`: `isLockLOD` (`bool`，LOD 锁定开关)
  - `+0x39`: `isDowngrade` (`bool`，是否允许降级)
  - `+0x3C`: `isLoadedModel` (`bool`，模型是否保持加载)
  - `+0x44`: `isCulled` (`bool`，超远距离是否剔除)
  - `+0x4F`: `m_tempCloseUnloadMode` (`bool`，是否阻止卸载模型)
- **关键控制方法**：
  - `PreUpdateLOD2(float deltaTime, Camera camera, int32_t distanceIdx)`：每帧为场景中的 NPC 计算视角与距离，产生 `distanceIdx`。
  - `LockLod(int32_t index)` / `ResumeLod()`：官方提供的 LOD 锁定/解锁接口。
  - `CloseUnloadModelForLod(bool close)`：关闭远距离卸载模型行为。
  - `CloseAnimatorCull(bool close)`：关闭远距离 Animator 动画剔除行为。
  - `SetLodInvisible()`：超远距离时彻底隐藏 NPC。

---

## 3. 关键故障根因剖析（Root Cause Analysis）

### 3.1 IL2CPP 调用约定与 SEH 隐蔽陷阱

在早期的检测实现中，编写了如下帮助函数：

```cpp
static void* SafeInvokePtrFn(void* fn, void* target) {
    if (!fn || !target) return nullptr;
    __try {
        return reinterpret_cast<void*(__fastcall*)(void*, void*)>(fn)(target, nullptr);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}
```

- **问题本质**：
  - 在 Unity x64 Windows IL2CPP 环境下，所有的非静态 C++ 生成函数签名均为：
    `ReturnType Method_Name(InstanceType* __this, const RuntimeMethod* method)`
  - 寄存器分配为：`RCX = __this`，`RDX = MethodInfo*`。
  - 大量方法（特别是属性 getter、虚方法桩、带 Profiler/iFix 拦截的方法）进入函数体后，**第一条或前几条汇编指令即对 RDX 执行解引用**（例如 `movsxd rax, dword ptr [rdx + r8 + 4]`）。
  - 当传入 `nullptr` 作为 RDX 时，立刻触发硬件级内存访问违规（`0xC0000005: Access Violation`）。
  - 该崩溃被 Windows SEH（`__try ... __except`）捕获，函数静默返回 `nullptr`。
- **级联反应**：
  - `IsEndminfAvatar` 中的 `get_npcId()`、`GetModelGo()` 全部返回 `nullptr`；
  - 判定结果回退到指针扫描，而硬编码的偏移列表并未命中正确的 `GameObject` 指针；
  - 导致判定返回 `false`，并调用 `InsertAvatarCache(avatar, false)` 将其永久缓存在哈希表中；
  - **结果**：所有安装的 LOD 拦截钩子（Detour）认为当前实体不是女管理员，全部放行；引擎降级逻辑如常执行，导致与模块治理逻辑冲突，全屏隐藏（人没了）。

### 3.2 发光部件（`vfxpart`）的孤立残留

女管理员默认战术大衣附带 `S_actor_endminf_vfxpart_01_lod0`。EFMI 导出的休闲服 Mod 将其排除在替换列表之外。

在 `PumpAiShadowCoverage` 维护循环中，原本有一段逻辑：
```cpp
// Ensure non-hidden LOD0 renderers for Endminf stay enabled:
if (name.find(kActorMeshPrefix) == 0 && name.find("_lod0") != std::string::npos &&
    name.find("shadowProxy") == std::string::npos) {
    ...
    if (!is_hidden_by_mod) {
        SetRendererEnabled(renderer, true);
    }
}
```
由于 `vfxpart` 既不是 Mod 显式要求隐藏的部件（不在 BEM 构件列表中），又属于 `_lod0` 名字空间，它被这段代码判定为“必须保活的 LOD0 部件”，**每 100ms 强制被重新启用**。

---

## 4. 历史逐实例 Hook 方案（已被原生管线控制替代）

针对上述逆向成果，实施了四位一体的重构与修复：

### 4.1 零调用直接内存指针链辨识（Zero-Invoke Identification）

摆脱对 IL2CPP 函数调用的依赖，直接利用确定的原生内存布局进行判定，其当时的设计如下；后续暴露的问题见第 7 节：

1. **`NPCAvatar` 辨识**：
   - 直接读取 `avatar + 0x20` 开始的 `avatarParams` 结构体，提取 `correspondingCharId` 或 `avatarMeshName` 托管字符串指针，使用 `ManagedString` 校验是否包含 `"endminf"`。
   - 直接读取 `avatarGoRef` 内嵌偏移（`+0x110` / `+0x120`）获取 `GameObject` 指针，使用原生 `ObjectName(go)` 校验是否包含 `"endminf"`。
2. **`NPCCrowdLOD` 辨识**：
   - 直接读取 `lod + 0x20`（`npcComp`），再读取 `npcComp + 0x140`（`avatar`），将判定直接归结至 `IsEndminfAvatar`。
3. **安全反射降级支持**：
   - 必须通过 IL2CPP 调用时，全面改用宿主标准接口 `g_host->runtime_invoke(context, method.method_info, target, params, &exception)`，传递合法的 `MethodInfo`。

### 4.2 全链路 LOD0 强制压制与防裁剪

在检测到女管理员时，全方位接管引擎的分级调度：

```text
每帧生命周期调用
  │
  ├── NPCCrowdLOD.PreUpdateLOD2 / NpcCrowdLODDowngrade.PreUpdateLOD2
  │     └── 拦截：强制入参 distanceIdx = 0，调用 EnforceLod0OnCrowdLOD
  │
  ├── NPCCrowdLOD.SetLodInvisible
  │     └── 拦截：阻止超远距离隐藏，强制调用 EnforceLod0OnCrowdLOD 并 return
  │
  ├── NPCAvatar.SetLodVisible / SetLodVisibleWithFade
  │     └── 拦截：强制入参 lodVisibleData = 0
  │
  └── NPCAvatar.RefreshAvatarNPCLOD / ForceSetLod
        └── 拦截：强制重设为 LOD0，直接重写 avatar->m_npcLodVisible = 0
```

在 `EnforceLod0OnCrowdLOD` 中实施深度状态加固：
- `LockLod(0)` 与 `set_isLockLOD(true)`；
- `renderLodIdx = 0` 与 `isDowngrade = false`；
- `CloseUnloadModelForLod(true)` 与 `m_tempCloseUnloadMode = true`（防卸载）；
- `CloseAnimatorCull(true)` 与 `isCulled = false`（防动画与网格剔除）；
- `isLoadedModel = true`。

### 4.3 发光部件（`vfxpart`）特异性治理与状态回滚

在 `PumpAiShadowCoverage` 循环首部加入优先级前置拦截：

1. **自动识别**：凡名称包含 `"vfxpart"` 的渲染器（包括主渲染器和 `shadowProxy` 阴影代理）；
2. **定制替代检查**：若 `g_replaced` 中没有该渲染器的 `custom_mesh` 替换记录；
3. **即刻静默隐藏**：调用 `SetRendererEnabled(renderer, false)`，并记入 `g_ai_coverage_hidden_vfxparts` 状态列表（通过 `gchandle_new` 保护引用）；
4. **F11 无损回滚保证**：当用户按下 F11 回滚或模块卸载时，`RestoreAiCoverageHiddenVfxParts` 精准恢复其原始可见性与句柄释放。

---

## 5. 女管理员构件覆盖矩阵（Component Matrix）

| 构件标识 | 渲染器名称 | 顶点索引数 | 休闲服 Mod 行为 | 运行时处理策略 |
| :--- | :--- | :--- | :--- | :--- |
| **C0** | `S_actor_endminf_body_01_lod0` | 13,878 | 替换为裸手臂与腿部网格 | 替换 sharedMesh，保持激活 |
| **C1** | `S_actor_endminf_cloth_01_lod0` | 18,348 | 替换为短外套与白 T 恤 | 替换 sharedMesh，保持激活 |
| **C2** | `S_actor_endminf_cloth_02_lod0` | 10,722 | 隐藏（原版大衣部件 1） | 标记 `hidden_by_mod`，禁用渲染器，F4 可控 |
| **C3** | `S_actor_endminf_cloth_03_lod0` | 2,748 | 隐藏（原版大衣部件 2） | 标记 `hidden_by_mod`，禁用渲染器，F4 可控 |
| **C4** | `S_actor_endminf_cloth_04_lod0` | 13,578 | 替换为休闲鞋网格 | 替换 sharedMesh，保持激活 |
| **C5** | `S_actor_endminf_cloth_05_lod0` | 1,848 | 隐藏（原版大衣领饰） | 标记 `hidden_by_mod`，禁用渲染器，F4 可控 |
| **C6** | `S_actor_endminf_eyebrow_01_lod0`| 384 | 保留原版/微调 | 替换或保活 |
| **C7** | `S_actor_endminf_eyeshadow_01_lod0`| 192 | 保留原版/微调 | 替换或保活 |
| **C8** | `S_actor_endminf_face_01_lod0` | 3,366 | 保留原版脸部网格 | 保持激活 |
| **C9** | `S_actor_endminf_hair_01_lod0` | 19,302 | 替换为休闲发型 | 替换 sharedMesh，保持激活 |
| **C10**| `S_actor_endminf_iris_01_lod0` | 384 | 保留原版眼睛网格 | 保持激活 |
| **N/A**| `S_actor_endminf_vfxpart_01_lod0` | 1,152 | **未覆盖（大衣发光信标）** | **运行时自动隐藏，记入回滚池** |
| **N/A**| `*_lod1`, `*_lod2`, `*_lod3` | 各异 | **未提供低模** | **历史方案曾强行禁用；当前保留跨 LOD 辅助覆盖，主方案由原生管线锁定 LOD0** |

---

## 6. 历史版本验证与结论（不作为当前版本验收）

1. **编译产物**：MSVC x64 Release 编译通过，0 错误，0 警告。
2. **部署就绪**：
   - 源码构建库：[`artifacts/betterendfield-native-build/stage/Release/modules/BetterEndfield.CustomModel.dll`](file:///E:/Dr.Hydra/Better%20Endfield/.worktrees/dev-custom-model-design/artifacts/betterendfield-native-build/stage/Release/modules/BetterEndfield.CustomModel.dll)
   - 发布目录：[`artifacts/BetterEndfield-win-x64/modules/BetterEndfield.CustomModel.dll`](file:///E:/Dr.Hydra/Better%20Endfield/artifacts/BetterEndfield-win-x64/modules/BetterEndfield.CustomModel.dll)
   - 游戏部署目录：[`E:\Better Endfield\modules\BetterEndfield.CustomModel.dll`](file:///E:/Better%20Endfield/modules/BetterEndfield.CustomModel.dll)
3. **功能闭环**：
   - 彻底消除了因直接调用 IL2CPP 虚方法引发的 SEH 异常；
   - 实现了相机全距离下拉远不消失、不降级、不裂模；
   - 彻底消除了原版发光信标浮空残留的问题，并支持 F11 完美恢复。

---

## 7. 替换时崩溃排查与彻底修复（Crash Analysis & Fix）

### 7.1 崩溃现象与堆栈定位
用户按下 F12 替换完成后，游戏在进入主循环或执行首帧 LOD 评估时立即闪退崩溃。
Windows 事件查看器（Application Event Log）捕获到异常：
- **故障应用程序**：`Endfield.exe`
- **故障模块**：`unityplayer.dll`
- **异常代码**：`0xC0000005`（Access Violation）
- **故障偏移**：`0x00000000000BE9DF`

通过 Capstone 反汇编 `UnityPlayer.dll` 偏移 `0xBE9D0` 至 `0xBE9F5`：
```x86asm
0x000BE9D0: cmp byte ptr [rcx + 0x10], 0
0x000BE9D4: jne 0xbe9f5
0x000BE9D6: cmp qword ptr [rcx], 0
0x000BE9DA: je 0xbe9fa
0x000BE9DC: mov rax, qword ptr [rcx]
0x000BE9DF: mov rdx, qword ptr [rax + 0x10]  ; <--- CRASH HERE (0xC0000005)
0x000BE9E3: xor eax, eax
0x000BE9E5: test rdx, rdx
0x000BE9E8: cmovne rax, rdx
0x000BE9EC: mov qword ptr [rcx + 8], rax
0x000BE9F0: mov byte ptr [rcx + 0x10], 1
0x000BE9F4: ret
```

### 7.2 崩溃根因分析（Root Cause）
1. **Unity 原生对象解包机制**：
   函数 `0xBE9D0` 是 Unity 内部的核心函数 `ScriptingObjectOfType<T>::Get()` / `PPtr<T>::GetScriptingObject()`。它接收一个 C# 托管对象句柄 `rcx`，解包取出位于 `[rax + 0x10]` 的原生 C++ 对象指针 `m_CachedPtr`。
2. **错误的内存探测与方法调用**：
   在 `module_npc_lod_lock.inc` 的 `EnforceLod0OnCrowdLOD` 和 `ResumeLockedCrowdLODs` 中，代码尝试强行控制原生 LODGroup：
   ```cpp
   const size_t kLodOffsets[] = { 0x130, 0x140, 0x138, 0x148 };
   for (size_t off : kLodOffsets) {
       void* lod_group = nullptr;
       if (SafeReadPtr(reinterpret_cast<const uint8_t*>(avatar) + off, lod_group) && lod_group) {
           SafeRuntimeInvokeVoid(g_lod_lock_lod_group_force_lod, lod_group, p_0);
       }
   }
   ```
   然而，`avatar + 0x130 / 0x140` **根本不是 `UnityEngine.LODGroup` 托管对象**，而是 NPCAvatar 内部的其他结构或数组指针！
   将这些野指针当作 `this` 传给 `UnityEngine.LODGroup.ForceLOD(0)` 时，Unity 引擎内部直接解包非法指针，在 `0x000BE9DF` 处引发非法的 `0xC0000005` 内存访问越界，瞬间使游戏崩溃退出！
3. **`ObjectName` 盲扫风险**：
   原 `IsEndminfAvatar` 中通过 `kGoOffsets` 盲扫读取指针后直接传递给 `ObjectName(go)`，同样会调用 `UnityEngine.Object.GetName` 解包原生句柄，存在同样的越界崩溃隐患。
4. **Hook 递归重入（Reentrancy）**：
   在 `DetourAvatarRefreshAvatarNPCLOD` 中调用 `NPCAvatar.ForceSetLod` 和 `NPCAvatar.SetLodVisible`，而这两个方法本身已被 Hook，若没有线程局部防重入标志，容易发生循环重入和状态紊乱。

### 7.3 当时提出的 Hook 修复方案（历史记录）
1. **完全剔除 `UnityEngine.LODGroup.ForceLOD`**：
   - 终末地使用的是自有层 `Beyond.NPC.Lod.NPCCrowdLOD` 和 `Beyond.NPC.Avatar.NPCAvatar`，而非 Unity 默认的 `LODGroup`；
   - 彻底删除 `g_lod_lock_lod_group_force_lod` 的解析与调用，根除了非法对象解包的唯一源头。
2. **消除盲扫，改用正规托管方法**：
   - `IsEndminfAvatar` 优先调用官方方法 `NPCAvatar.get_npcId()` 与 `NPCAvatar.GetModelGo()`，彻底删除盲扫 `kGoOffsets`。
   - `IsEndminfCrowdLOD` 与 `IsEndminfNpcComp` 统一通过官方属性获取。
3. **加入线程局部防重入保护（Reentrancy Guard）**：
   - 全局引入 `static thread_local bool t_in_lod_lock_detour = false;`；
   - 确保所有 detours 在内部互相调用或二次触发时直接放行原函数，杜绝任何死循环与递归状态错乱。
4. **运行时性能优化**：
   - 每帧更新只写入直达内存标志（`SafeWriteBool/Int32`，纳秒级开销）；
   - 仅在首次发现新实例时调用一次 `LockLod(0)` 等托管方法，彻底降低 IL2CPP GC 与 JNI 调用开销。
