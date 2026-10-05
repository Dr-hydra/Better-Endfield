# Android LOD1 与现有 EFMI 素材复查

日期：2026-10-05。分支：`dev/bem-1.4-weapons-forms`。复查基线：`c872e171`。

## Android 判断修正

Android 普通场景使用 LOD1 是既有设计。旧 BEM 的 UI LOD0 是 donor，world LOD1 才是场景接收器。1.4 显式资源可声明自己的 LOD1 接收器、网格、骨骼和材质，Windows 的 LOD0 限制在预处理分支内，不作用于 Android。

本次确实发现基线的一处回归：ResourcePump 只有检测到 legacy 包才维护 Android 管线偏置，导致只启用显式武器/大招包时没有维持已适配的最高可用层级。`EnableForceLOD0` 虽含 LOD0 字样，Android 入口实际设置 parent/art-tag bias 为 `1e-7`，不是把 QualitySettings 改成 LOD0。相关原始观察见 [Android 光照、阴影与 LOD 记录](../android-lighting-lod/ANDROID_LIGHTING_SHADOW_LOD.md)。

修复提交为 `b5d167d`，恢复“有任意启用模型 + lod_pipeline 开启 + 未停止”的原策略，不改变 Android LOD1 接收路径，不修改 QualitySettings。生产 LodState 提取到 `model_lod_state.inc` 供行为测试直接编译，避免只验证一份复制的条件表达式。

验证结果：

- 68 项生产状态机检查通过：普通包、只启用显式 LOD1 蒙皮/静态资源、配置关闭、全部停用、停止、管线切换、入口失败后的恢复；测试确认没有访问 QualitySettings。
- Android world/显式资源绑定及阴影检查共 213 项通过，含 LOD1 的实际索引构造、精确匹配、本地 donor 和阴影事务；显式路径没有加载 UI donor。
- Windows CustomModel 与测试目标构建通过；旧绑定、静态生命周期及异步回归通过。
- Android `assembleRelease`、`lintRelease` 通过。日志：`build/bem14/logs/android-lod1-*.log`。

测试 APK：`build/bem14/android/android/gradle/app/outputs/apk/release/app-release.apk`。没有安装设备或发布 Release。

## G 盘已有的大招源与旧成品

上轮只制作了资源草稿，漏查了 `G:\zmd` 和 `G:\zmd_bem` 中已有的大招材料。本轮找到以下两组：

| ID | EFMI 源 | 旧 BEM |
| --- | --- | --- |
| M0178 | `G:\zmd\庄方宜\庄方宜-大招状态-终极状态 by 幽魂小猫\[DaisyMeow]Zhuang Fangyi Ult 1.5FIX\ZUlt\mod.ini` | `G:\zmd_bem\成品\庄方宜\庄方宜-大招状态-终极状态 by 幽魂小猫.bem` |
| M0184 | `G:\zmd\庄方宜\庄方宜-心灵+大招形态\庄方宜\大招形态\mod.ini` | `G:\zmd_bem\成品\庄方宜\庄方宜-心灵+大招形态.bem` |

旧成品都是普通 `chr_0030_zhuangfy_postmodel/uimodel` 的 9 组件目标，不是已绑定到大招 prefab 的包。M0178 原转换把部分大招部件合并到普通 cloth；M0184 原报告记录整份大招 INI 被剔除。因此新转换从 EFMI 源与真实大招 donor 重建，不把旧成品换一个版本号当作完成迁移。

10 个源部件的原生游戏 index identity 和 index count 已与真实大招逐项匹配。`ultimate` 和 `ability` 两个资源各有 12 个 LOD0 组件：源覆盖 10 个，额外两个 VFX 部件保留。mirror 是另一套网格/骨骼合同，没有套用这份映射。

本轮采用独立的大招扩展包及独立 package ID，原输入、正式成品和批量任务台账保持不变。M0184 原普通形态包可继续承担普通形态，扩展包补回原来未转换的大招部分。

研究包已输出到 `build/bem14/samples/`：

| 文件 | 内容 |
| --- | --- |
| `M0178-zhuangfy-ultimate-extension-Windows-LOD0.bem` | 24 个目标组件、18 个 mesh、15 张纹理、10 组选项/1024 种组合；每资源 9 replace、1 hide、2 keep |
| `M0184-zhuangfy-ultimate-extension-Windows-LOD0.bem` | 24 个目标组件、20 个 mesh、5 张纹理；每资源 10 replace、2 keep，固定大招外观 |

两包均使用真实原生资源内 donor；ultimate 与 ability 逐对共享同一 Mesh、bindpose 和材质对象，骨骼相对路径顺序、root_bone 及递归变换逐项核对相同。源 VB0/1/2 及各 draw 的索引数据保持，hair/eyeshadow 的双材质槽保留。M0178 的 Component1 在源中只有 handling=skip、绘制被注释，因此按隐藏处理。

Python 对写出的包检查了 M0178 全部 1024 种组合、M0184 的 1 种固定组合，以及 2048/2 个资源选择计划。正式 C++ 校验器分别检查 2/1 种选择，普通与优化加载的数据一致，均接受实际写出的包和资源路由；没有把原生抽样描述成 1024 种原生逐态回放。

可重跑素材盘点与后续转换脚本位于 `research/custom_model/1.5.3/Windows/weapon-ultimate-bem/efmi-conversion/`；武器工作位于相邻 `weapon-conversion/`。游戏资源 CRC32C 用于匹配 EFMI 已有资源身份，没有计算产物校验哈希。

## 已完成的武器转换与剩余范围

从本地 1274 个 INI、503 个旧成品中查找武器材料，随后对候选类型的 160 个精确 prefab 提取依赖和原生身份；没有把抽样的 sword-0014 强行对应到名字相近的源。

已生成 `Nait3D-GaeBolg-weapon-only-Windows-LOD0.bem`，来源为艾维文娜“邓·斯凯斯的兔女郎”附带的独立 GaeBolg 武器 EFMI。其目标精确匹配 `wpn_lance_0006`，为静态 MeshRenderer/MeshFilter，替换几何 12525 顶点、59928 索引；三张贴图分别匹配原生 `_BaseMap`、`_BumpMap`、`_MetallicGlossMap`。Python 几何校验与正式 C++ 校验通过。

此包修改共享武器资源，影响所有使用该资源的对象，不是角色专属实例覆盖。源中的全局雨水修改没有应用；该武器源的雨水 DDS 与 M0178 中的版本不是同一份，不能照搬 M0178 的原图结论。

LOD1 另有 Windows 离线 donor 探针，放在 `build/bem14/samples/probes/`。Windows 运行时当前拒绝 LOD1 目标，它不能作为可用 Windows 模型包或 Android 包启用。

提弗洛斯的弓源精确匹配 `wpn_misc_0051`，但包含 `gpu_posed=1`、VB3、不同 LOD 骨骼顺序及 draw 范围差异。没有省略这些行为并输出号称完整的蒙皮武器包。细节与复跑命令见武器目录的 README。

生产 C# 管理器对六个真实包的 15 对资源冲突检查全部通过：旧普通形态和大招扩展可并存；两种大招扩展互斥；武器和角色包可并存；同一长枪资源的两个 LOD 候选互斥。检查仅读元数据，没有改动真实安装库。

## 纹理证据的范围

M0178 的七张未直接命中当前原生材质的 DDS，可用源 mip0 与合法 DX11 描述重建 INI 的原纹理身份，支持按“作者未修改该基础 mip”处理；其中雨滴 `7d330dbd` 另有历史原生 mip0 逐字节相等证据。其余项不声称当前原生材质属性或完整 mip 链等价，转换保留当前游戏纹理。重跑脚本为 `audit_original_textures.py`。

M0178 Component0 的显式 `ps-t2` 经引用 Shader 的离线参数表核对，属于全局阴影纹理，不是局部材质属性。与 t17/t18/t19 的 BaseMap/MetallicGlossMap/BumpMap 布局一致的 26 个参数记录，其 t2 分别为全局 `_CSMShadowmapTex` 或 `_PunctualLightShadowTexV2`；没有伪造一个 BEM 材质槽来承接它。重跑脚本为 `audit_shadow_slot.py`。两个源中的 `fe1d6277` 均未证明为当前原生纹理的无效覆盖，相关源行为明确保留为未复刻项。

研究包均尚未实机验收。这批具体模型目标来自 Windows，包平台明确为 `windows-x64`；Android 运行时 LOD1 修复已完成，但这些新资源的 Android 目标包仍需相应 donor 资料。原素材、原成品、批量台账与正式 Release 均未改动。
