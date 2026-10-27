# 梨诺原版头部预览资源样包

验证日期：2026-10-27。

## 实测结果

| 资源规格 | ZIP 实际大小 | 解包后大小 |
| --- | ---: | ---: |
| 保留全部原材质贴图 | 11.670 MiB | 26.100 MiB |
| PC BC7，最高 1024 | 2.263 MiB | 4.099 MiB |
| PC BC7，最高 2048 | 4.850 MiB | 8.099 MiB |
| Android ASTC 6×6，最高 1024 | 1.312 MiB | 2.443 MiB |
| Android ASTC 6×6，最高 2048 | 2.561 MiB | 4.228 MiB |

- 当前游戏来源版本：2954fa80-23c1-1579-2b22-4ecfd6d70418；按当前游戏 VFS 覆盖关系提取 134 个依赖包，缺失数为 0。
- 包含 6 份唯一头部几何、12 条 world/UI renderer 绑定；眉毛、脸、虹膜、头发、眼影及发影。world/UI 去重要求几何、submesh、bindpose 和骨骼名称哈希数据全部相同，不按名称强行合并。
- 几何 positions/UV/indices 共 591260 字节（约 0.564 MiB）。静态预览包带 5 张主要色贴/阴影遮罩；全部材质贴图版带 23 张纹理。
- PC 只裁剪原有 mip，不重新编码。Android 在开发机上从对应 mip 编码 ASTC 6×6，保留 mip 链；最终用户不需要 Pillow、Python 或 astcenc CLI。
- 保留原始纹理颜色空间、材质标量和颜色参数、纹理 scale/offset、骨骼及 bindpose 元数据。无动画片段和游戏 shader 字节码，不是可安装的 BEM 替换包。

## 可复现流程

1. 用现有 extract_native_bundles.py 提取 chr_0035_liino 的当前 world/UI 依赖。
2. NativeAssetReader 新增 --preview-assets 目录模式，输出解析后的 positions/UV/indices、原纹理压缩块以及引用图。普通元数据导出行为保持兼容。
3. 运行 tools/CustomModel/build_native_head_preview.py，传入 --graph、--payloads、--character、全新 --output；可选 --astcenc 指向开发机编码器。输出五种 ZIP 和 size-report.json。
4. tools/CustomModel/test_native_head_preview.py 的 6 项测试通过；五个实际 ZIP 均通过数据回读、引用和几何长度检查。未计算产物哈希。

## 显示与接入边界

- 已在现有共享 WebGL 查看器中单独显示原版眉毛、脸、虹膜和头发，纹理错误数 0，GL error 0。使用与 BEM 一致的坐标轴转换；游戏投影眼影和发影未当作普通不透明几何显示。
- 这是独立资源包与静态显示原型，尚未接入已安装包的自动补齐逻辑，也未更新正式 APK。截图位于 output/native-head-liino/head-preview-1024.png。
- 后续应只对当前有效规则为 keep 的几何补齐；replace 不叠加原网格，hide 不恢复。替换网格的原生纹理槽也需按材质与组件身份补齐，不能按同名贴图猜测。
- 必须校验角色、资源 world/UI、组件路径与来源版本。缺失或不匹配时保持现有预览并报告，不改安装配置。
- 建议先用 2K 档验证脸部特写和实际包的组合显示，再决定全量内置或按角色下载；不能用梨诺单个样本直接推算全部角色。


## 已接入的自动补齐（2026-10-27）

PC/Android 的已安装包预览使用同一套 native 导出逻辑。两端临时携带已核验的梨诺 2048 本地预览资源；PC 保留 BC7，Android 使用开发阶段转换的 ASTC 6x6，无需用户安装转换器。此步骤不涉及 Release、下载或更新机制。

- 从 LoadBem 解析后的有效操作读取 keep / replace / hide，重新选择选项会重新计算。
- keep 才补原版几何；hide 永不恢复；replace 保留替换几何，按原材质槽补空缺的原版底图。
- 以角色、world/UI 资源、渲染器及材质槽匹配；1.4 使用完整 prefab asset_path 和相对 renderer_path，不凭文件名猜贴图。
- 底图按 _BaseMap 的确切原纹理名匹配已选择的 BEM 覆盖，覆盖优先于原版底图；保留原材质 UV 缩放/偏移。
- 游戏投影阴影材质不作为不透明原版几何补入。
- 缺少、损坏或不匹配的本地资源不阻止原有预览，scene.nativeCompletion 记录状态；不会改动安装包和保存设置。
- 当前真实资源覆盖梨诺，其他角色暂不自动补齐；不包含游戏材质完整着色、原生骨骼动画。

已用手机上的“梨诺-恶魔公主”验证：额外补眉毛及虹膜共 2 个网格，替换脸部和白发保持唯一，不叠加原版脸/头发。
