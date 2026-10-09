# BEM 1.4 原生目标草稿

这些资料来自本机当前 Windows VFS 的真实离线对象图谱，manifest 版本为
`2954fa80-23c1-1579-2b22-4ecfd6d70418`。没有进行游戏内替换、对象池复用或
停用恢复验证；所有 `.profile.json` 均明确标记 `runtime_verified: false`、
`conversion_ready: false`、`render_verified: false`。

| 文件前缀 | 实际资源 | LOD0 组件 |
| --- | --- | ---: |
| `zhuangfy-ultimate` | 庄方宜大招主体、技能实体、镜像 | 36（每资源 12） |
| `sword-0014` | `wpn_sword_0014` 静态剑 | 1 |
| `funnel-0014` | `wpn_funnel_0014` 蒙皮法器 | 4 |

每组有三个文件：

- `.spec.json`：可重跑的精确 prefab 路径、平台和 LOD 请求。
- `.profile.json`：目标合同、原生 renderer/mesh 序列化身份，以及未验证状态。
- `.project.json`：可由 `bem_tool.py pack` 打包的创作起点。全部组件操作为
  `keep`，没有替换网格或 payload，因此单独安装它不会改变模型。

创作替换时须加入真实 stream/index/texture payload、mesh descriptor，再将对应
`component_rules` 改为 `replace`；不同 resource 的 descriptor 必须分别引用本地
donor，全局 payload 可共享。静态剑不用添加 skin 流或伪骨骼。

重新解析游戏资源后，使用以下命令生成新草稿；输出路径可以放在自己的创作目录：

```powershell
python tools/CustomModel/build_bem14_target.py NATIVE_GRAPH.json --spec tools/CustomModel/profiles/bem14-drafts/zhuangfy-ultimate.spec.json --output MY_PROFILE.json --project MY_PROJECT.json
```

独立制作工具包也提供同一入口，无需安装 Python：

```powershell
.\BetterEndfieldNext.BemConverter.exe target-profile NATIVE_GRAPH.json --spec profiles/bem14-drafts/zhuangfy-ultimate.spec.json -o MY_PROFILE.json --project MY_PROJECT.json
```

输入可以是 NativeAssetReader 原始图谱，或新版 `parse_native_models.py` 输出。
生成器只按精确 `asset_path` 选择 prefab。默认只选层级中精确的 `lod0` 等分支；
没有此命名约定的资源必须在 spec 的资源项中声明 `renderer_paths` 精确相对路径列表。
不按相似名字配对、不将 Windows 证据虚构成 Android donor、不猜测 runtime 布局。
来源 snapshot 不同应更新 spec 并重新审核，不要覆盖 snapshot 证据强行通过检查。

原始读取保留两条 backend 告警（CNKeys 默认模板提示、公共 bundle 子流读取告警）；
选入的 41 个 renderer 的 mesh/bone/material 引用均完整。无 renderer 的
`abilityentity_chr_0030_zhuangfy_sword_postmodel` 是独立空/效果 prefab，未伪造为
剑网格目标。真正的静态样本来自 `wpn_sword_0014`。

精确格式约束见 [BEM 1.4 规范](../../../../docs/custom_model/BEM_V1_4_SPEC.md)。
