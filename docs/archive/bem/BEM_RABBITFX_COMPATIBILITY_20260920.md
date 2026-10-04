# RabbitFX 兼容性与自动化边界

## 结论

RabbitFX 不是 EFMI 官方随包定义的角色模型格式，而是独立的 3DMigoto/EFMI 外部框架。它同时包含稳定纹理转交、GlowFX、解遮罩、Shader 重写和实验性骨架/组件扩展。不同功能的自动化边界不同：

- **稳定纹理接口**是确定的、可解析的，适合接入 BEM 的逐绘制段转换。
- **GlowFX**有固定资源名称和触发方式，静态情况下可以保留为 BEM 的附加效果描述；当前 BEM 运行时没有 GlowFX 执行器，不能宣称视觉等价。
- **实验性骨架/组件扩展**依赖 Shader 模式识别、GPU 缓冲区和作者约定，作者说明明确表示不能由修复工具自动迁移，必须人工重写；当前不能自动转换。
- 解遮罩和通用 Shader 重写是 RabbitFX 全局框架行为，不属于某个角色外观的几何/材质数据，不能打进单个 BEM 包后独立复现。

因此 RabbitFX 包可以“自动识别并部分转换”，不能笼统标记为可自动转换或完全不支持。

## 作者公开说明中的固定接口

RabbitFX 作者在官方发布页给出的 Stable Textures / Set Textures 形式是在即将绘制前，把资源放入命名空间并调用：

```ini
Resource\\RabbitFX\\Diffuse = ref ResourceDiffuseTexture
Resource\\RabbitFX\\Lightmap = ref ResourceLightmapTexture
Resource\\RabbitFX\\Normalmap = ref ResourceNormalmapTexture
Resource\\RabbitFX\\Discardmap = ref ResourceDiscardmapTexture
Resource\\RabbitFX\\Rainmap = ref ResourceRainmapTexture
run = CommandList\\RabbitFX\\SetTextures
drawindexedinstanced = ...
```

这组名称是框架接口约定，可以被静态解析。作者还说明，设置应紧邻实际 `drawindexedinstanced`，因为同一个部件可能在多个绘制段使用不同状态；不能把它提升成整个 Component 的唯一材质。

GlowFX 使用：

```ini
Resource\\RabbitFX\\GlowMap = ref ResourceGlowMap
run = CommandList\\RabbitFX\\Run
```

作者说明 GlowMap 的 alpha 通道是发光遮罩/强度，另有 HSV 调整变量。这些是效果参数，不是普通原生材质属性。BEM 可以保存资源与段的关系，但需要新增运行时效果实现才能复现。

## 哪些是作者自定义或实验性

RabbitFX 的实验性 4.E 骨架/组件扩展通过重写 Shader、复制/重定向骨骼缓冲区和模式过滤来模拟合并骨架。作者说明其目标是减少游戏更新影响，但同时称该功能仍是 experimental，并明确表示普通修复脚本不能把既有 Mod 迁移到这个骨架系统，必须手动重写。

这意味着以下内容不能从 `Resource\\RabbitFX\\*` 名称推断：

- `ResourceCB1`、`ResourceT0` 等缓冲区的布局、偏移和生命周期；
- 组件附着到哪个 donor 骨架、16 位骨骼索引如何解释；
- Shader pattern 对应的 pass、变体和常量缓冲区；
- 自定义 `GlowMap`、Discardmap 或 Materialmap 的实际采样语义；
- 作者对不同角色、LOD、绘制段的特殊条件。

这些属于 RabbitFX 版本与作者 Mod 的实现约定，不是一个可跨所有角色的稳定结构。

## 与 EFMI 官方接口的关系

EFMI 官方模板提供的是按原始资源 hash 检查 ps-t0..ps-t22 的纹理覆盖流程；RabbitFX 的 `SetTextures` 是另一条外部框架路径。RabbitFX 作者说明中也建议在实际绘制前设置资源，并可移除 `CheckTextureOverride`，这说明两者不是同一层的协议：

| 层级 | EFMI 标准模板 | RabbitFX |
| --- | --- | --- |
| 原生贴图替换 | `TextureOverride` + `CheckTextureOverride = ps-tN` | 资源写入 `Resource\\RabbitFX\\...` 后 `SetTextures` |
| 绘制段 | Component/LOD 模板和 `drawindexed*` | 由作者在段前调用框架，再绘制 |
| Shader | 游戏原生 Shader/已知模板 | RabbitFX 动态识别、重写或替换路径 |
| 骨架 | EFMI 合并骨架模板 | RabbitFX 实验性缓冲区扩展 |
| 生命周期 | Mod/游戏版本匹配 | 全局框架，整个 Mods 目录只允许一个实例 |

RabbitFX 发布说明还警告多个实例会导致异常，且与其他解遮罩 Mod 不兼容；这进一步证明它不是适合嵌入每个 BEM 包的普通材质库。

## 自动化分层方案

### A. 可以自动化：稳定纹理段

静态入口满足以下条件时，可以转为 BEM 的逐段材质绑定：

1. `Resource\\RabbitFX\\Diffuse/Lightmap/Normalmap/Discardmap/Rainmap` 的资源声明完整且路径安全。
2. `SetTextures` 调用位于明确的 draw 段之前；条件可以用默认常量静态求值。
3. 原始 Component/IB/索引范围和目标角色资料已匹配。
4. 每个 RabbitFX 资源能通过目标 Shader/材质资料对应到 BEM donor 属性；对应不唯一就拒绝。
5. 没有同时依赖 RabbitFX 骨架扩展、ShaderOverride/ShaderRegex、动态常量或未解析的命令列表。

BEM 已能表达多个 draw、多个材质槽和共享纹理 payload；所需是转换器新增 RabbitFX 段状态解析与材质属性映射，不是修改容器格式。

### B. 可以识别但暂不自动复现：GlowFX

可以提取 GlowMap、HSV 参数、作用资源和 draw 范围，报告为可审阅的效果记录。当前运行时没有 RabbitFX Glow 执行器，因此只能：

- 作为诊断报告保存；或
- 在作者选择“忽略效果”时继续做几何/稳定纹理转换，并明确效果会丢失。

不能把 GlowMap 当作普通 BaseMap/EmissionMap 静默写入。

### C. 必须人工适配：骨架扩展与 Shader 重写

出现以下任意特征时，保持 `manual_only`：

- `ResourceCB1`/`ResourceT0` 等缓冲区及 RabbitFX skeleton 命令；
- RabbitFX 4.E/experimental skeleton、组件附着或自定义骨骼读写；
- `ShaderOverride`、`ShaderRegex`、自定义 Shader、常量缓冲区重定向；
- 运行时生成/修改的纹理或无法静态确定的 draw 条件。

## 样本收集计划

逐绘制段能力需要收集机制样本，不要求每个 Mod 都提供一份配方。每个样本只需要保存结构化诊断和必要的映射证据，不把原始游戏纹理/完整 Frame Dump 放入玩家工具。

### 必收样本类别

| 类别 | 现有样本 | 还需验证 |
| --- | --- | --- |
| EFMI 原生 hash 覆盖、无直接 ps-t | 女管理员 | 保持回归基线 |
| RabbitFX Stable Textures，单段单材质 | 莱万汀部分衣服段 | 一个身体、一个头发、一个多材质部件 |
| RabbitFX Stable Textures，同 Component 多 draw | 莱万汀 C0/C8 | 段间材质状态、索引范围和 LOD 分支 |
| `ps-t` 直接绑定但不依赖 RabbitFX | 庄方宜部分段 | 与 Shader 反射属性建立对应 |
| RabbitFX GlowFX | 暂无完整可用验证 | GlowMap alpha/HSV/段范围；验证 BEM 明确报告丢失 |
| RabbitFX 4.E 骨架扩展 | 洁尔佩塔、庄方宜相关样本 | 保持人工适配拦截，记录结构而不放行 |
| 旧版/新版 EFMI 模板 | 莱万汀 ALPHA-4、管理员 ALPHA-12 | 至少一个 ALPHA-8/13 纯标准包 |
| 冲突/多个 RabbitFX 实例 | 发布说明警告 | 验证导入器能报告全局依赖，不复制框架实例 |

### 每个样本的最小记录

- 源入口版本、活动 INI 和依赖命令列表；
- 每个 draw 的默认条件、IB/VB、索引范围、RabbitFX 资源状态；
- 原始 hash、资源尺寸/编码/mip、目标原生材质 Shader/pass/属性；
- 是否使用 `SetTextures`、GlowFX、骨架扩展、ShaderOverride/Regex；
- 自动分类：`stable-texture-auto`、`effect-report-only`、`manual-only`；
- 运行时画面由用户确认，记录“几何正确、材质正确、效果缺失/错误”的独立结果。

### 放行规则

同一机制至少有两个不同角色或不同材质类别的样本，且离线映射和一次实机结果一致，才把规则提升为通用自动路线。单个作者包只能生成候选规则，不能改变全局白名单。任何新游戏版本或 RabbitFX 版本都要重新检查 Shader/框架版本标识。

## 官方来源与范围说明

RabbitFX 的公开接口和限制来自其 [GameBanana 发布页](https://gamebanana.com/mods/651557) 的作者说明；页面公开描述了 Stable Textures、GlowFX、实验性 4.E 骨架、Shader 重写、单实例要求和 CN Mod Fix 工具限制。该页面是作者发布说明，不是 EFMI 官方标准文档。

EFMI 官方模板和 API 只规定原生资源覆盖与 Component/LOD 导出流程，参见 [EFMI Tools 模板](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/templates/mod.ini.j2) 和 [EFMI Package API](https://github.com/SpectrumQT/EFMI-Package/blob/3cc2f4b89e791983484ac00ad5a3e15779957350/EFMI/Core/EFMI/API.ini)。RabbitFX 不在这两个官方仓库的标准接口里。

本文件结论是：**Stable Textures 可以建立通用静态转换器；GlowFX 可以解析并报告；RabbitFX 实验性骨架和 Shader 重写不能自动迁移。**
