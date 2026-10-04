# 世界模型绑定核对（2026-10-03）

基线：`main dd7131b4`。已读 `android/AGENTS.md`；未启动游戏、操作手机或增加 UI 文字。共享工作区只修改本任务负责的 world adapter、其既有测试及本记录。

## 结论与局部修复

- **角色已确认**：用户确认第 5 项是游戏里的艾尔黛拉，正式目标为 `chr_0025_ardelia`。
- **当前资源证据**：艾尔黛拉、狼卫、提弗洛斯的 PC/Android world/UI 根名及对应 Renderer 名称相同。三者分别 15、15、17 个 UI LOD0 Renderer 均有精确的 Android `Mesh_all/lod1/<同名_lod1>` 对应，没有平台资源根命名差异。
- **艾尔黛拉匹配缺陷已证实**：C9 源 Mesh 为 `S_actor_ardelia_fur_01_lod0_20`，实际 UI/PC world LOD0 Renderer 为 `S_actor_ardelia_fur_01_lod0`；Android world 为 `S_actor_ardelia_fur_01_lod1`。现有 6 个该角色 BEM 均保留正确的完整 Mesh 名，旧绑定却要求 Renderer 同名，导致正常模式找不到 C9。新增 `ComponentRendererName` 只映射该角色、该完整 Mesh 名；原 Mesh 身份、索引、骨骼、材质和纹理校验保持。world/proxy 使用映射后的 Renderer 名；共享 `module.cpp` 的两处 Renderer 比较已由主代理整合。
- **已有样本普遍性补充**：后续只读盘点的 32 个 PC 角色中，7 个存在 fur LOD0 Mesh/Renderer 名差异，故不限艾尔黛拉；狼卫的非阴影 Renderer 在该样本中全部同名，不能将其故障归因于同类问题。完整列表与覆盖边界见下文。
- **代码与日志证实、已修复**：Android 普通模式首次 world 加载准备 UI donor 后，`prepared_ui` 输出原先被 `g_hot_switch_runtime` 限制。world 提交后 `ensure_ui` 再构建同一份 UI，导致重复首次上传。现在只要两个输出指针都提供，就返回新建的 UI bindings 和 donor，让已有 `ProcessResource` 联合事务发布。cached donor 仍复用既有完成记录；无输出参数或只有一个输出参数的行为保留。
- paired UI 修复解决首次 world/UI 重复构建。主代理已报告当前安装 APK 的首次 world 上传后 `ensure_ui` 没有再次构建；后续独立 UI delivery 的重复上传另由主代理调查。本任务没有修复后外观验收。没有狼卫对应失败证据；艾尔黛拉已发现上述具体匹配缺陷，但用户实际报错包和完整失败链仍待日志核对。生产 PC/Android 编译由主代理统一执行。

## 当前来源与精确名称

忽略目录 `artifacts/world-binding-20261003/` 保存定点读取脚本、来源 receipt、原始 prefab graph 和 `prefab-comparison.json`。使用既有 NativeAssetReader 只解析元数据/对象引用；没有重新提取依赖闭包或导出贴图、网格 payload。

| 来源 | Windows | Android |
|---|---|---|
| 安装位置/设备 | `D:/Arknights Endfield`，StreamingAssets → Persistent | `adb-78572d34-Z9UIst._adb-tls-connect._tcp`，当前 Persistent VFS |
| manifest Version | `2954fa80-23c1-1579-2b22-4ecfd6d70418` | 同左 |
| manifest 内置 Hash | `8b9cf90e2097d4b4767c76ae00807eb6` | `664d17e6f5f4fd44513249dbeabf4c53` |
| 来源有效性 | 当前有效 manifest 原始字节与本地缓存一致 | 当前设备 BundleManifest BLC 与缓存逐字节一致；bundle 范围从当前设备读取 |
| 本次 prefab 范围 | 4 个 bundle，351,599 字节 | 4 个 bundle，338,857 字节 |

两个平台直接命中的路径相同：

```text
assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0006_wolfgd_postmodel.prefab
assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0006_wolfgd_uimodel.prefab
assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0034_typhoea_postmodel.prefab
assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0034_typhoea_uimodel.prefab
```

manifest 还存在同名 `actors/postmodels/npc/` 资源；本次只读取上述 characters/UI 路径，没有把 NPC prefab 当作玩家模型。这四个 bundle 的双端文件名相同，manifest 索引不同，实际字节数也不同；不能因为文件名相同就复制 PC bundle 或其平台标识到 Android。

- 狼卫实际名称包括 `S_actor_wolfgd_hs_01_lod0`、`S_actor_wolfgd_eyebrow_01_lod0`、`S_actor_wolfgd_furcard_01_lod0`、`S_actor_wolfgd_furcard_02_lod0`；Android world 只将后缀换为 `_lod1`，其余名称不变。
- 提弗洛斯主体使用小写 `typhoea`；三个 VFX Renderer 实际为 `S_actor_Typhoea_vfxpart_01/02/03_lod0`，保留大写 `T`。现有 BEM catalog 覆盖 14 个主体部件；prefab 中的 17 个包含这三个 VFX，不代表 catalog 已支持它们的所有绘制。
- Android world 各有 53 个 SkinnedMeshRenderer。所核对的 Transform 路径没有重复，狼卫 398 个后代 Transform、提弗洛斯 536 个；UI 所需狼卫骨骼相对路径均可在 world 对应。
- 提弗洛斯 `cloth_01` 的 UI 路径 `Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/skirt_base_L_c_01_jnt/skirt_base_L_c_02_jnt/skirt_base_L_c_03_jnt` 在 world 同父路径使用 `skirt_base_R_c_03_jnt`。当前 catalog C13 的 bone index 24 已声明 world alias。这是双端共有的 world/UI 差异，不能仅按名称跨父路径找骨骼；具体包是否携带 alias 仍须读该包元数据。

本次 prefab bundle 不包含外部 Mesh/Material/Texture 对象，因此上述证据能核实 Renderer 名称、路径、骨架及引用，**不能证明当前材质/贴图内容、顶点声明、bindpose 或游戏内可见性相同**。

## 艾尔黛拉定点补充与修复

用户确认后沿同一份已核对的 manifest，新增只读 **PC 2 个 prefab bundle / 190,095 字节、Android 2 个 / 186,119 字节**，复用已读取的 Bundle 索引，没有读取 Mesh/Material/Texture 依赖。证据位于 `artifacts/world-binding-20261003/chr_0025_ardelia/`，两端 NativeAssetReader 均为 2 files、backend issues 0。

两端路径分别相同：

```text
assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0025_ardelia_postmodel.prefab
assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0025_ardelia_uimodel.prefab
```

- 两端 UI 的 15 个 Renderer 名集合完全相同；Android world 有 48 个 Renderer，其中 LOD1 完整对应 UI 15 个。catalog 的 14 个主体组件另加 prefab 的 `vfxpart_01`；没有缺失部件。UI 骨骼相对路径均能在 world 找到，没有添加骨骼 alias 的依据。
- fur UI/PC world LOD0 引用的精确序列化身份为 `cab-1ab91a54c08168733b74f0cf17ede8cb:-2740753347919904246`，与两份已有原生元数据完全一致；已有 Mesh 记录和运行时 catalog 均明确记录名称 `S_actor_ardelia_fur_01_lod0_20`。当前 prefab 的 GameObject/path 则均为无 `_20` 的 `S_actor_ardelia_fur_01_lod0`。Android LOD1 引用 `cab-4a694cb1e5f47df3d68de056fc2659cf:-1471254278406609160`，已有对应 Mesh 名是 `S_actor_ardelia_fur_01_lod1`。未重新读取上述 Mesh payload。
- **world Mesh 名称证据边界**：新读取的 Android prefab 对该 LOD1 Mesh 只有 external ref（`resolved=false`），不能独立证明当前设备 Mesh 的 `m_Name`。两份已有原生 graph 对同一精确 ID 均记录无 `_20` 的 `S_actor_ardelia_fur_01_lod1`；尚无 world Mesh 为 `...lod1_20` 的证据。因此保持 adapter 对 world Mesh 的严格 `ObjectName(DonorMesh(target))==target_name` 校验。若当前 Mesh 名已变化，会输出 expected/actual 并拒绝；只有取得确切名称证据后才可分别维护 Renderer/Mesh 身份，不猜测后缀或放宽检查。
- 只读 `F:/zmd_bem/成品/艾尔黛拉/` 现有 6 个 BEM 的 header/JSON：半果、去外套夹克、去鞋、日常穿着、爱芮、甜心重色。所有 target 根正确，C9 均为 `_lod0_20`；target 骨骼/材质/索引计数与 catalog 相符。共 178 处 draw/keep 材质绑定的材质名和 texture pin 与已有 catalog 均无新增冲突。这只是元数据核对，不能排除运行时同名不同 Texture 对象等问题。
- 不改 catalog/BEM 的 Mesh 名，不做通用去数字后缀或大小写归一化。`resource_policy.h` 的限定函数返回正确 Renderer 名，world adapter 同时使用它派生 `fur_01_lod1` 和 `fur_01_shadowProxyMobile`。
- 提供主代理的补丁：`artifacts/world-binding-20261003/chr_0025_ardelia/module_renderer_mapping.patch`（整合前 `git apply --check` 已通过）；现已确认两处调用由主代理整合。`PrepareResource` 的 Renderer 比较为 `ObjectName(renderer)!=ComponentRendererName(adapter.id,identity.name)`；`ReadCompletedAndroidDonor` 为 `ObjectName(renderer)!=ComponentRendererName(adapter.id,adapter.components[binding.component_id].name)`。源 Mesh 比较仍使用 `identity.name`，不得一起替换。

## 旧修复与本次上传证据

- 用户纠正：此次参考的提弗洛斯历史修复是**眉毛部件命名**，不是 BEM 1.2 裙骨 alias。前述裙骨记录仅是独立的当前骨架证据，不能归为该次修复。本轮按 `typhoea.*(brow|eyebrow)` 检索相关 Git 路径，只定位到 `7022e71f` 的 catalog/研究导入，未定位到独立的 `eyebrow` → `brow` 修复 diff，故不虚构提交号或修复方向。
- 当前原始 prefab Renderer 名、已有原生 Mesh 元数据和 catalog 三者一致：

  | 角色 | UI/PC LOD0 Renderer 与 Mesh 名 | Android world LOD1 Renderer 与 Mesh 名 |
  |---|---|---|
  | 提弗洛斯 | `S_actor_typhoea_brow_01_lod0` | `S_actor_typhoea_brow_01_lod1` |
  | 狼卫 | `S_actor_wolfgd_eyebrow_01_lod0` | `S_actor_wolfgd_eyebrow_01_lod1` |
  | 艾尔黛拉 | `S_actor_ardelia_brow_01_lod0` | `S_actor_ardelia_brow_01_lod1` |

  `brow` 与 `eyebrow` 是当前不同角色的准确资源名称；没有三者眉毛的 PC/Android 名称差异证据，不新增通用替换。当前艾尔黛拉已证缺陷是 **fur Renderer 与 Mesh 的 `_20` 差异**。
- `510e83ba` 对齐 UI/world 的共享 Texture 对象匹配和校验开关；同一 Texture 被多个属性引用可以接受，多个不同的同名 Texture 在正常模式仍拒绝。
- `cf468234` 已接受 `M_actor_*` → `M_actor_lod_*` 的精确同后缀材质对应，既有回归包含 `M_actor_typhoea_face_01` → `M_actor_lod_typhoea_face_01`。没有从本次仅 prefab 的读取重新推导材质规则。
- `docs/archive/bem/BEM_MATCHING_REVIEW_20261001.md` 另记校园包共享 Texture pin、逆兔 `cloth_03` 错误贴图元数据的定点修复；这与资源根命名及裙骨 alias 是不同问题。
- 主代理提供的 `artifacts/model-upload-20261003/device-diagnostics.log:324–434` 有两组各 8 条 `8192x8192` 的 `built t=`。第 382 行 world committed 后，第 386 行开始再次构建，第 433 行 UI committed、第 434 行 paired publication PASS。旧 adapter 只在 hot switch 模式返回 paired UI，解释了普通模式该重复构建。本任务仅读取指定窗口，未重复抓全量日志。
- 主代理后续报告 `artifacts/model-upload-20261003/device-current-peak-diagnostics.log`：world 首轮 8 张约 370 MiB，紧接的 `ensure_ui` 零再构建，说明已安装 APK 的首轮 paired 修复生效；后续 3561/3619 两次独立 UI 又各构建 8 张，属于另一条首次 UI delivery 重复路径。本任务未改该性能路径，也未重复抓日志。

实际 adapter 路径是 `android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc`；本基线没有 `android_world_binding.h`。本任务修改 adapter、`resource_policy.h` 的限定 Renderer 映射及对应测试；未写入主代理负责的 `module.cpp/bem.cpp/texture_install.cpp`。首次重复上传修复无需额外 module 补丁；后续确证的艾尔黛拉 Renderer 映射另提供上述两处补丁，已由主代理整合。

## 已有元数据的普遍性盘点（只读补充）

仅使用 `F:/zmd_bem/research/identities/roles/*/native.json` 的已有 NativeAssetReader graph、33 份角色 catalog 和已有 `artifacts/android-refactor/android-source-metadata.jsonl`；没有读取新平台资源、BEM payload 或追加生产角色特例。复现脚本及明细保存在忽略目录 `artifacts/world-binding-20261003/audit_existing_renderer_mesh_names.py`、`existing-renderer-mesh-name-audit.json`。

比较通过 Renderer → GameObject 和 Renderer → Mesh 的**精确对象引用**取两个真实名称，Transform parent 引用定位实际资源根/路径。只纳入明确 AssetBundle container 路径对应的玩家 `characters/*_postmodel` / `uimodels/*_uimodel`；不按角色中文名、数字后缀或相似名称连接对象。依赖包带入的 34 条同身份重复记录去重，未发现相同路径身份冲突。

覆盖 **32 个 Windows 原生 graph、32 个角色、64 个 world/UI 根、2,267 条可解析 Renderer/Mesh 绑定**；其中 LOD0 818 条、LOD1 418 条、LOD2 353 条、LOD3 330 条、阴影代理 280 条、其它路径 64 条。所有源均标记 Windows bundle 和 manifest Version `2954fa80-23c1-1579-2b22-4ecfd6d70418`；样本内无未解析 Mesh 引用或 backend issue，但这是**已有离线快照**，不能代表当前所有 PC/Android bundle 的名字仍相同。

LOD0 有 **7 个角色、7 个 fur 组件、14 条 world/UI 绑定**的名称差异，全部与相应 catalog 的源 Mesh 名及 C 编号一致：

| 角色 / catalog C | 实际 LOD0 Renderer 名 | 实际 LOD0 Mesh 名 | 已有 PC world LOD1 Mesh 名 |
|---|---|---|---|
| 昼雪 `chr_0014_aurora` / C8 | `S_actor_aurora_fur_01_lod0` | `S_actor_aurora_fur_01_lod0_20` | `S_actor_aurora_fur_01_lod1_8` |
| 大潘 `chr_0018_dapan` / C6 | `S_actor_dapan_fur_01_lod0` | `S_actor_dapan_fur_01_lod0_20` | `S_actor_dapan_fur_01_lod1_8` |
| 秋栗 `chr_0019_karin` / C6 | `S_actor_karin_fur_01_lod0` | `S_actor_karin_fur_01_lod0_20` | `S_actor_karin_fur_01_lod1_8` |
| 埃特拉 `chr_0021_whiten` / C6 | `S_actor_whiten_fur_01_lod0` | `S_actor_whiten_fur_01_lod0_20` | `S_actor_whiten_fur_01_lod1_8` |
| 阿列什 `chr_0024_deepfin` / C5 | `S_actor_deepfin_fur_01_lod0` | `S_actor_deepfin_fur_01_lod0_20` | `S_actor_deepfin_fur_01_lod1_8` |
| 艾尔黛拉 `chr_0025_ardelia` / C9 | `S_actor_ardelia_fur_01_lod0` | `S_actor_ardelia_fur_01_lod0_20` | `S_actor_ardelia_fur_01_lod1` |
| 汤汤 `chr_0027_tangtang` / C8 | `S_actor_tangtang_fur_01_lod0` | `S_actor_tangtang_fur_01_lod0_20` | `S_actor_tangtang_fur_01_lod1_8` |

表中 world LOD1 Renderer 都是对应的无 `_8` 名；除艾尔黛拉外，另外 6 个角色有 LOD1 的 `_8` 差异（6 条 world，另有大潘 UI LOD1 1 条）。LOD2/3 及上述 LOD0/1 之外的其它非阴影路径未发现名称差异。这个结果说明 Renderer 与 Mesh 身份应分别维护；即便以后定点处理其它角色，也不能只去 LOD0 的 `_20`，更不能全局删除数字后缀或放宽 world Mesh 校验。本轮只记录这些角色，没有给它们新增生产映射。

**狼卫单列**：已有 world 69 条、UI 15 条绑定，共 84 条。除 12 条阴影代理外的 **72 条全部 Renderer 名 = Mesh 名**；world/UI LOD0 各 15 条、world LOD1 15 条、LOD2 14 条、LOD3 13 条均相符，包含 `furcard_01/02`、`eyebrow_01` 与 `hs_01`。12 条差异都在 `Shadow_Proxy`，例如 `S_actor_wolfgd_cloth_01_shadowProxyDesktop` 引用 `S_actor_wolfgd_cloth_01_lod1`；这是明确的阴影代理/源 Mesh 命名关系，不能当作主体加载失败证据。

整个 PC 样本还有 273 条阴影代理/源 Mesh 不同名，属于单独的代理路径，不计入上述 7 个角色的主体命名问题。独立原始 runtime probe 本次可用的是 Android 女管理员 world 的 40 条记录：8 条阴影代理不同名，32 条非阴影记录相符；它不提供狼卫或上述 7 个角色的当前 Android Mesh 名确认。

覆盖限制：33 个 catalog 角色中，噗切娜没有本次同类完整原生 graph，未作结论；其余未收集角色也不能外推。先前取得的三个角色 Android prefab 只有 external Mesh ref，仍受前述边界限制。7/32 是该离线样本中的角色计数，**不是游戏总体故障率**；其余 25 个角色在样本 LOD0 没有名称差异，也不等于所有包必定能加载。狼卫仍需具体 BEM 和对应拒绝日志定位。

## 局部检查

- MSVC 19.44、C++20 独立编译既有 `native/tests/android_world_binding_tests.cpp`，避免触碰主代理生产构建目录。
- `artifacts/world-binding-20261003/build/Release/AndroidWorldBindingTests.exe`：**PASS 132**。原 109 项包括普通/热切换首次 world 的 paired 输出、共享 mesh/material、两套 live 骨架、联合发布/失败回滚、cached donor、可选参数，以及原有纹理/容量/阴影检查。新增 23 项覆盖艾尔黛拉真实 Renderer、fur 完整 Mesh 名不变、world/proxy/cached 对应，并拒绝其它角色、未核实数字后缀和大小写变体。
- 主代理已整合共享 `module.cpp` 两处 Renderer 映射，并报告生产 `PrepareResource` 真函数正负测试通过。已只读核对 `native/tests/custom_model_binding_tests.cpp` 的对应回归：无 `_20` 的 Renderer 加正确 `_20` 源 Mesh 能准备成功，错误源 Mesh 仍被精确身份校验拒绝。本任务未重复生产构建或修改主代理测试。
- 三个角色双端原始 prefab 名称集合及 LOD1 对应检查通过；累计每端 6 文件的 NativeAssetReader backend issues 均为 0。BEM JSON 元数据检查未读取或解码任何 payload。
- 普遍性补充脚本完成上述 32 个角色的引用解析/去重，未解析引用和身份冲突均为 0；此次只新增研究明细与本文，不修改生产代码或重复构建。
- `git diff --check -- native/modules/custom_model/resource_policy.h android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc native/tests/android_world_binding_tests.cpp docs/WORLD_MODEL_BINDING_20261003.md` 通过。

艾尔黛拉已确证的 Mesh/Renderer 身份区分已整合。当前 world Mesh 名仍有上述 external-ref 证据边界；剩余具体包材质、布局或 donor/Renderer 空间问题，以及狼卫的不加载，仍需对应包/失败日志，不继续全量提取或放宽全局校验。

本任务及上述只读普遍性补充已完成；world Mesh 严格校验保留，不再读取额外 payload。后续 UI 资源复用、原生加载与 legacy 热切换调研由主代理及其他代理负责。
