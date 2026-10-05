# Endfield Poser Bridge（Blender 5.2 LTS）

把《明日方舟：终末地》角色骨骼桥接进 Blender，在 Blender 的 Pose 模式下摆姿势，
再同步回游戏（冻结态）。

## 安装

1. 游戏内插件 Endfield Poser 已加载（web 服务器 `http://127.0.0.1:18923` 随插件启动）。
2. Blender：编辑 → 偏好设置 → 插件 → 安装…，选择本文件夹（或本 `__init__.py`），
   勾选启用 **Endfield Poser Bridge**。
3. 3D 视图 N 面板 → **Endfield** 分类。

## 使用

- **连接游戏并创建骨架**：拉取全骨骼（含手指/配饰），当前游戏姿势作为 Blender 的 rest pose，
  骨架自动以角色根骨骼为中心。
- **导入姿势（游戏→Blender）**：把游戏当前姿势的 delta（旋转+位置）写到 Blender 姿态骨骼。
- **导出姿势（Blender→游戏）**：把 Blender 当前姿势写回游戏**全骨骼**（自动冻结角色，
  跳过锁定骨）。在 Blender 里可以用自带的 Pose 模式、Auto IK、约束等工具摆姿。
- **自动同步**：开启后约 6 次/秒把 Blender 姿势写回游戏（Blender→游戏单向，避免回环）。

## 换算约定（v1）

- 位置：`B = (U_x, U_z, -U_y)`；逆换算 `U = (B_x, -B_z, B_y)`。
- 局部旋转增量：`B_delta = R_x(-90°) * U_delta * R_x(90°)`。
- 连接时游戏当前姿势 = Blender rest；同步回游戏时 `target = delta * rest`。

> 注意：该换算对 Unity/Blender 骨骼局部轴做了"整体轴对齐"近似。若某些骨骼方向
> 在 Blender 里与游戏不一致（常见于非轴向对齐的末端骨），以实测为准调整
> `uq_to_bq` / `bq_to_uq` 中的 AXIS_Q。

## API（游戏侧，已内置）

`GET /api/allbones`（全骨骼）、`GET /api/bones`、`GET /api/pose`（带名字）、
`POST /api/pose`、`POST /api/freeze`、`POST /api/tpose`、`POST /api/reset`。

## 备注

- 同步覆盖全骨架（含手指/配饰）的旋转与位置。
- 首次使用建议先在游戏里冻结角色，Blender 侧点"连接"观察骨架朝向是否正确。
