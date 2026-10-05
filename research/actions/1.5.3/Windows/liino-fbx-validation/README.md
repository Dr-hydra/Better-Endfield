# 梨诺特殊冲刺烘焙与完整片段

用与洁尔佩塔 v3 相同的流水线处理 `chr_0035_liino`，脚本已参数化。

## 复现

```
python tmp_analysis/extract_liino_animation_inputs.py                  # Controller / Perform 配置
python tmp_analysis/extract_liino_animation_inputs.py --controller-deps # 依赖包
tmp_analysis/BundleTreeDump/bin/Release/net9.0/BundleTreeDump.exe dump <bundle> tmp_analysis/liino-controller-raw "" 91
tmp_analysis/CharFbxProbe/bin/Release/net9.0/CharFbxProbe.exe <bundles> tmp_analysis/liino-fbx-validation --char liino --char-id chr_0035_liino --frames 174
python tmp_analysis/prepare_bake.py --char liino --avatar tmp_analysis/liino-fbx-validation/avatar.json \
  --clips tmp_analysis/liino-clip-raw --samples tmp_analysis/liino-samples \
  --out tmp_analysis/liino-fbx-validation --left CAB-fd73 --right CAB-b1f8 --frames 174
"D:\Unity Hub\Unity\editor\2022.3.62f3\Editor\Unity.exe" -batchmode -nographics \
  -projectPath tmp_analysis\aglina-fbx-validation\UnityBake -executeMethod CharBakeV1.Run \
  -bakeOutput tmp_analysis\liino-fbx-validation -logFile tmp_analysis\liino-fbx-validation\unity-bake.log
tmp_analysis/CharFbxProbe/bin/Release/net9.0/CharFbxProbe.exe <bundles> tmp_analysis/liino-fbx-validation --char liino --char-id chr_0035_liino --frames 174 --baked
```

## 资源定位

| 对象 | 位置 |
| --- | --- |
| `ac_chr_0035_liino.controller` | `main/417788f35781781fe3524732.ab`，CAB-0e5631821e52346d0cae34bc59e3d10f |
| `SpDash_L` | Base Layer 状态 37，Clip 索引 74，`A_actor_liino_sprint_dash_sp_l`，CAB-fd73af73ce3823a8aa11a189db1b3199 |
| `SpDash_R` | Base Layer 状态 38，Clip 索引 75，`A_actor_liino_sprint_dash_sp_r`，CAB-b1f88b2c00ddd1fbe11c689fae75abdb |
| 模型 | `chr_0035_liino_postmodel.prefab`，`main/2b3309373794eab53df96fdb.ab` + 114 个依赖 |

两个状态的名称哈希与洁尔佩塔相同（左 528430122、右 -445642423），因为状态名字符串一样；运行时读的是 `HASH_SP_DASH_L/R` 静态字段，与角色无关。出口过渡 `SPdashLtoSprint` / `SPdashRtoSprint` 的 Exit Time 为 0.9 / 0.9183。

## 校验

`unity-bake-report.json`：Avatar 有效且为 Humanoid，源缩放与重建缩放同为 0.9917296，muscle 往返最大差 0.00023（最差通道 Right Forearm Twist In-Out），本体位置误差 9.9e-6，往返局部旋转差 0。忽略的六条原生额外通道位于 29–31 与 40–42，与洁尔佩塔一致。

`liino_full_clip.mp4` 是完整片段预览：她踩板滑行，约 2.13 秒落地转入疾跑。
