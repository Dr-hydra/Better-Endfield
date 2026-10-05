# 阿列什娜特殊冲刺：第二版导出与整段验证

2026-09-09。用户恢复工作后，补齐第一版的模型和单位问题，并核对主体姿态及 FBX 转换。第一版目录保留。

## 查看入口

- `aglina_full_comparison.mp4`：左右完整动作并排播放，1100×660、60 FPS、209 帧。
- `aglina_full_comparison.blend`：对应的并排预览工程。
- `aglina_spdash_left_review.blend`、`aglina_spdash_right_review.blend`：单侧编辑场景。
- `aglina_spdash_left_full.fbx`、`aglina_spdash_right_full.fbx`：修正后的完整时长导出。
- `left-full-contact-sheet.png`：左侧十个时刻的实际渲染。

## 本轮修正

1. 从当前 Persistent overlay 补入 `s_actor_aglina_face_01_lod0.asset` 和 `s_actor_aglina_body_01_lod0.asset`，每侧由 8 个网格补齐至 10 个 LOD0 网格。源 bundle 分别为 `main/58a2d38f0d8cd1de01748a8b.ab`、`main/fb73de3aee578aa1cf545e28.ab`。
2. 写出器 `FbxSystemUnit(scaleFactor)` 的参数改为 100，即每源单位为 100 厘米。Blender 场景使用米，模型动作包围盒最大跨度由约 0.0224 恢复至约 2.24 米。未手工放大骨骼或破坏绑定。
3. 第一版“肌肉数值差 4.5”经几何验证不等同于动作扭曲：最大差出现在左右手腕 Down-Up；前臂 Twist 也存在约 4 的表示差。把 GetHumanPose 结果重新 SetHumanPose 后，Unity Quaternion.Angle 检查未检测到局部旋转差，全部层级世界位置最大差约 0.00007006 米。该结果支持这些大数值差主要来自旋转表示的重新归一，而非相同幅度的几何错误。

## 验证结果与范围

| 检查 | 左侧 | 右侧 |
|---|---:|---:|
| 原始采样点 / 导出轨道 | 209 / 400 | 209 / 400 |
| Blender Action 范围 | 1–209 | 1–209 |
| LOD0 网格 | 10 | 10 |
| 全部采样时刻骨骼世界位置最大转换误差 | 0.000001995 m | 0.000001675 m |
| 所有半帧位置与线性位置 / SLERP 参考的最大差 | 0.00012272 m | 0.00014743 m |

位置误差比较的是离线 Unity 重建骨骼 → FBX → Blender 的转换，不是与游戏实时画面的逐帧对照。半帧核对未发现转换导致的大幅旋转跳跃。已实际导入、求值、渲染两侧十个时刻；完整视频完成编码，并重新读取确认 1100×660 / 209 帧 / 60 FPS，抽取编码后画面检查左右角色都在镜头内。

原版 Avatar 与重建 Avatar 的 humanScale 同为 0.99172956。仍使用普通 Unity 2022.3.62f3 解释 Humanoid 数据，尚未对游戏自定义 IK、约束、实时物理作等价验证。预览未加载原版材质贴图、粒子和道具。保持原地预览，Motion 的行进参考被移除；带行进数据的原始 ACL 样本保留，当前 FBX 不作为根运动无损回导资源。

## 对中间段循环的初步判断

已改为比较全身关节位置、世界方向与首尾速度，不再只用 Root Y 高点判断。搜索起点 0.20–1.00 秒、终点 1.867–2.733 秒，间隔至少 1.2 秒；这是有界启发式候选搜索，并非全局最优证明。

候选之一为源帧 **22–125（0.367–2.083 秒，长 1.717 秒）**：选定 11 个身体关节首尾位置 RMS 左约 0.253 米、右约 0.230 米；原先 98–154 高点窗口左约 0.489 米。较长中间段在全身匹配上有改善，但仍存在明显姿态及速度差，不能直接裁剪后设置循环。

因此，下一阶段应在完整模型上选定较长过程段，修正首尾附近的身体、手脚曲线，再观察连续多轮播放。此次没有把任何候选当成最终 Loop，没有修循环曲线或修改游戏运行时。

证据：`unity-bake-report.json`、`blender-left-report.json`、`blender-right-report.json`、`subframe-report.json`、`fullbody-loop-candidates.json`、`comparison-video-check.png`。
