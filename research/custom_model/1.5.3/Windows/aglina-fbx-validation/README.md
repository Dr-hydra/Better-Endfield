# 阿列什娜特殊冲刺：外部导出验证版

2026-09-09。按用户要求，导出后停止；未裁剪循环、未修改游戏运行时。

## 可查看文件

- `aglina_spdash_left_full.fbx` / `aglina_spdash_right_full.fbx`
- `aglina_spdash_left_review.blend` / `aglina_spdash_right_review.blend`

两侧均保留原始 209 个采样点、60 FPS、3.466667 秒。Blender 已实际导入并保存，Action 范围 1–209；每侧 400 条导出 Transform 轨道，导入场景含 421 根骨骼（含导出器生成的辅助节点）和 8 个网格。`full` 指完整时间范围，不表示无损还原完整游戏角色。

## 已验证与限制

现有 AnimeStudio ModelConverter 对这两段游戏 ACL 动画直接转换得到零条轨道。本次复用原骨架/网格和 FBX 写出器，将独立解码的 ACL 数据先在本机 Unity 2022.3.62f3 中还原成骨骼采样，再导出 FBX。左右 Transform binding 均完整映射到原 Avatar。

- Avatar 有效，原始和重建 humanScale 均为 0.99172956。
- Humanoid 主体由 HumanPoseHandler 重建；逐帧叠加原 ACL 的衣物、头发等 Transform 曲线。没有使用倒放、混合或人工补帧。
- 为原地查看，已移除 Motion 的平移/旋转参考；原始带位移数据仍保留于上级 `aglina-samples`，这份 FBX 不是根运动无损副本。
- 原游戏额外 IK、物理、材质、特效、道具没有在外部模拟。当前 LOD0 仅载入 8 个网格，身体和脸部网格缺失；这是骨骼动作验证场景，不是完整美术模型。
- FBX 写出器的单位转换使 Blender 中尺寸约为源数据的 1/100。为保留此次验证结果尚未修正；后续编辑/回导必须统一单位。
- Humanoid 往返核对最大 bodyPosition 差约 0.00001476、bodyRotation 差约 1.0914 度；已映射肌肉通道最大数值差约 4.5，原因尚未继续分析，不能据此宣称与游戏姿势无损一致。
- Blender 已检查完整时长、多帧求值，并实际渲染第 98 个源采样帧。尚未完成整段视觉一致性核对，也没有确定裁剪边界。

详细证据：`unity-bake-report.json`、`blender-left-report.json`、`blender-right-report.json`、`renders/left_098.png`、`renders/right_098.png`。

后续应先补齐网格、单位与主体姿态一致性，再讨论中间段裁剪。此次停在导出验证阶段。
