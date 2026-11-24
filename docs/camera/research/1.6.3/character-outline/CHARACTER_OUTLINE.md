# 相机增强：角色描边

## 目的

角色描边现在是相机增强中的独立功能，与 MMD 播放和游戏拍照站位模式分开。默认关闭。开启后，Camera 模块按队伍栏位置给角色添加独立的白边效果；MMD 播放是否开启不影响描边。

## 快捷键

`character_outline_hotkey` 默认值为 `BACKSLASH`，对应 Windows 虚拟键 `0xDC`，中文键盘通常显示为“、”。按键是边沿触发：按下时在“显示”和“隐藏”之间切换，松开后不会重复切换。快捷键只影响本功能添加的描边，不改变拍照站位模式的选中状态。

UI 中可直接录入快捷键，也可使用 `CTRL+BACKSLASH`、字母、数字或 F1–F24。配置解析接受 `、`、`\`、`OEM_5` 和 `BACKSLASH`。

## 位置与独立样式

`character_outline_slot1` 到 `character_outline_slot4` 对应队伍栏 1–4 号位。每个位置都有独立的：

- `enabled`：该位置是否参加描边；
- `color`：六位 RGB 十六进制，例如 `40C8FF`；
- `width`：效果材质的 `_OutlineWidth`，默认 `0.06`，范围 `0–1`；
- `intensity`：`_TintColorIntensity`，默认 `8.3`，范围 `1–100`；
- `opacity`：`_TintColorAlpha`，默认 `1`，范围 `0–1`。

示例：

```ini
[betterendfieldnext.camera]
enabled=true
character_outline_enabled=true
character_outline_hotkey=BACKSLASH
character_outline_slot1_enabled=true
character_outline_slot1_color=FFFFFF
character_outline_slot1_width=0.06
character_outline_slot1_intensity=8.3
character_outline_slot1_opacity=1
character_outline_slot2_enabled=false
character_outline_slot3_enabled=true
character_outline_slot3_color=40C8FF
character_outline_slot3_width=0.10
character_outline_slot3_intensity=6
character_outline_slot3_opacity=0.85
```

## 实现边界

拍照站位模式的原生入口是 `SnapshotSystem._ApplySelectedCharVisual`，它调用 `BaseModelComponent.PlayRenderHelper(outlineAsset)`，资源名为 `P_fxgp_common_char_outline_01_nofade_asset`。资源使用材质 `M_common_character_outline_01` 和 Shader `HGRP/Effect/VFXCharacterOutline`。

本功能不调用 `PlayRenderHelper`，也不改角色原材质或 Renderer 的 `MaterialPropertyBlock`。它通过 `EntityRenderHelperMaterialController.AddMaterialToAllRenderer` 添加私有材质副本，并保存返回的句柄；关闭、位置变化、样式变化或模块退出时只调用对应句柄的 `RemoveMaterialFromAllRenderer`。游戏已有的拍照白边句柄因此不会被清理。

效果资源的默认材质参数来自当前 1.6.x 客户端：`_TintColor` 为白色、Alpha 约 `0.502`，`_OutlineWidth=0.06`，`_TintColorIntensity=8.3`，`_TintColorAlpha=1`。本功能按位置创建材质副本后再覆盖这些参数；不再修改角色材质上的 `_OutlineWidth`。

加载入口为效果资源 `assets/beyond/dynamicassets/gameplay/effects/vfx/p_fxgp_common_char_outline_01_nofade_asset.asset`，类型为 `Beyond.Rendering.EntityVFXAddictiveMaterialAsset`；通过具名字段 `data.material` 读取材质，并保留资源句柄。不能直接按材质路径加载：2026-11-25 实机日志显示材质哈希 `778097339778936631` 不在运行时清单中（`Can not find Asset In RuntimeManifest`），尽管材质存在于依赖资源包中。旧实现因此始终未进入角色挂载。

资源尚未完成加载时保留句柄等待，加载失败时按两秒间隔重试。Renderer helper 不可用时移除相应位置的句柄，队伍加载或离场时清理本功能的全部句柄；场景恢复后重新添加。清理失败时不会强行释放对象，避免留下游戏仍在使用的材质引用。

## 验证范围

已完成：

- Camera 原生 Release 编译；
- `camera_character_runtime`、`camera_character_outline`、`camera_input` 三个最小测试通过；
- UI Release 编译通过；
- 配置模型覆盖默认关闭、单独启用 Camera、位置开关、颜色、数值范围和序列化；
- 资源分析确认白边材质和 Shader 的参数名与默认值。

实机仍需确认：1–4 号位与游戏队伍栏的对应关系、站位模式选中白边与本功能叠加时的视觉效果、以及不同距离下 `width` 与 `intensity` 的主观观感。

2026-11-25 加载修复的最小回归：验证加载效果资源的路径与类型、通过 `data.material` 读取材质、等待期间不释放/重建句柄、清理释放资源。修复版已由用户实机确认描边可以显示；各站位映射、复杂场景切换和与拍照选中效果叠加仍未逐项验证。
