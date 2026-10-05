# 武器真实源包转换研究

已完成 `wpn_lance_0006` 的 Windows LOD0 局部替换候选：真实 Nait3D 源几何、
3 张武器材质贴图，BEM 1.4；通过 Python 几何检查与正式 C++ BEM 校验器。
**尚未游戏内验证，不是 Android 安装包，也不包含源 Mod 的全局雨水修改。**

- 供检查的候选：`build/bem14/samples/Nait3D-GaeBolg-weapon-only-Windows-LOD0.bem`。
- `build/bem14/samples/probes/Nait3D-GaeBolg-weapon-only-Windows-LOD1.bem`
  **仅是离线 donor 探针**。当前 Windows runtime 拒绝该 LOD，不能推荐启用；
  Windows LOD1 的证据不能改标为 Android。
- 原始构建产物留在本目录 `output/`，未覆盖 `G:/zmd_bem/成品`，未修改游戏、
  源 Mod 或 Release，未安装设备，未计算产物哈希。

## 源与当前游戏身份

源为 `G:/zmd/艾维文娜/艾维文娜-邓·斯凯斯的兔女郎 by Nait3D/` 内独立的
`Nait3D-CohesiveTraction_GaeBolg/Nait3D-CohesiveTraction_GaeBolg/mod.ini`。
这里仅转换该独立武器子包，不代表其外层角色服装 Mod 已完整转换。

当前 Windows VFS snapshot：`2954fa80-23c1-1579-2b22-4ecfd6d70418`。
资源是 `assets/beyond/dynamicassets/gameplay/prefabs/weapons/wpn_lance_0006.prefab`。
两个原始网格均为 MeshRenderer + MeshFilter，无骨骼：

| 证据 | LOD0 | LOD1 |
| --- | --- | --- |
| Mesh | `S_wpn_lance_0006_01_lod0` | `S_wpn_lance_0006_01_lod1` |
| 游戏 IB CRC32C / 源 EFMI hash | `8526aec1` | `89b38228` |
| 原始索引数 | 19818 | 10158 |

源两个入口的 `match_index_count` 均与原生 mesh 完整索引区一致；第三个旧入口
`1265a39c` 未匹配当前原生数据，不用它推断任何资源。IB 身份是游戏资源匹配，
不是对生成 BEM 计算哈希。

替换数据为 **12525 顶点、59928 索引**，源 VB0 为 40 字节位置/法线/切线流，
VB1 为 8 字节 UV 流；保持原始静态网格语义，不加伪骨骼或空蒙皮流。
所有使用这个共享武器 prefab 的对象都会受影响，不能解释为只影响艾维文娜。

三个纹理绑定通过原生材质 `M_wpn_lance_0006_01` 的引用、纹理像素身份和已采用的
DX11 Texture2D 描述规则重建 EFMI hash 后精确对应。没有伪造本次 runtime 观察：

| 源 hash | 原生属性 | 原生纹理名 |
| --- | --- | --- |
| `7135f6c0` | `_BaseMap` | `T_wpn_lance_0006_01_D` |
| `ae53cc49` | `_BumpMap` | `T_wpn_lance_0006_01_N` |
| `70b367d7` | `_MetallicGlossMap` | `T_wpn_lance_0006_01_P` |

法线维持原生 BC5 槽的 XY 采样语义，载入源 BC7 替换数据；不替换 shader。
运行时法线、材质渲染及共享武器实例生命周期仍须游戏内验证。

## 明确未应用的源行为

源 `7d330dbd` 对应全局 `T_actor_common_rain_01_M`，不在上述武器材质中。
此源 DDS 解码后全图是 RGBA `(127,126,0,32)`；与历史提取的原生雨水图逐像素
不等，不能称为无效果导出或直接沿用。候选明确**不应用全局雨水扁平化**。
这不影响三张局部武器贴图与几何的表达，但与完整源 Mod 的雨天效果有差异。
其他源包中同 hash 的 DDS 必须分别比较，不能套用本包结论。

## 盘点及其他候选

只读扫描了 `G:/zmd` 的 1274 个 INI 和 `G:/zmd_bem/成品` 的 503 个 BEM；
已有成品没有独立 weapon 目标。本次分两批提取 160 个精确武器 prefab 的闭包，
用于找真实源 hash 对应，随后停止扩大提取。

- `wpn_misc_0051` 弓有精确 LOD0/LOD1 身份：`53427dcc`/52296、`02333a88`/34032，
  源来自“提弗洛斯-珊瑚海岸”的 `Weapon` 及“可爱薰衣草”的 `Bow_Colored`。
  前者涉及 `gpu_posed=1`、VB3 与按 LOD 绑定；后者还有不同 VB2、52248 索引子集，
  与原始 52296 索引不同。原生 LOD0/LOD1 的九骨顺序也不同；保留独立弓弦组件
  还需正确的 draw/skin 映射。没有跳过这些行为并声称完整蒙皮替换成功。
- 陈千语“东海帝皇”的剑/鞘源 hash `31684ff4`、`26b7a82c` 未得到当前身份对应。
  “剑鞘-赤霄”纹理 `96fd6241` 也缺可核实消费位置；不会硬套抽样的
  `wpn_sword_0014` 或普通角色衣服。
- `wpn_sword_0014`、`wpn_funnel_0014` 草稿仍只是此前抽样的真实原生合同，
  并不意味着它们对应这些源 Mod。

Android 默认 LOD1 的实现边界与 Windows 离线图谱分开。当前没有这两把武器的
真实 Android LOD1 mesh/material/bone 证据；候选未声明 `android-arm64`。

## 重跑与验证

从仓库根目录运行，保留源与提取目录；若 snapshot 更新，应建立新提取目录复审，
不覆盖旧版本身份记录：

```powershell
python -X utf8 research/custom_model/1.5.3/Windows/weapon-ultimate-bem/weapon-conversion/inventory.py
python -X utf8 research/custom_model/1.5.3/Windows/weapon-ultimate-bem/weapon-conversion/identify_weapons.py --families misc lance pistol
python -X utf8 research/custom_model/1.5.3/Windows/weapon-ultimate-bem/weapon-conversion/convert_lance.py
python -X utf8 research/custom_model/1.5.3/Windows/weapon-ultimate-bem/weapon-conversion/verify_manager_pairs.py
```

最后一步需已构建 `ManagerChecks`，并已有大招两个研究包。它用实际生产 C# 解析器
仅读六个真实 BEM：两个旧角色包、两个大招扩展包、长枪两种 donor 候选。
15 组冲突检查覆盖旧角色与大招并存、大招之间互斥、武器与角色并存、长枪两个
资源相同的 donor 版本互斥；不触碰真实安装库，也不启用 LOD1 探针。

小结和检查状态见 `summary.json`。大型 source inventory、原生图谱、精确 profile、
完整日志和 BEM 均只在本机保留。
