# 冲刺持续姿态数据来源与说明

本目录包含冲刺持续模块（`BetterEndfield.Actions`）在 `external_loop=true` 模式下运行时所需的骨骼姿态覆盖二进制数据文件（BEPOSE12 格式）。

## 文件清单

1. **`pose_aglina.bin`**
   - 对应角色：洁尔佩塔（`chr_0013_aglina`，Perform: `CharIntPerform_Aglina_Spdash`）
   - 规格：226 骨骼，周期 103 帧（约 1.717 秒，60 fps），包含左右双侧采样。
   - 详见：`docs/SPECIAL_DASH_CONTINUOUS_ANIMATION.md`

2. **`pose_liino.bin`**
   - 对应角色：梨诺（`chr_0035_liino`，Perform: `CharIntPerform_Liino_Spdash`）
   - 规格：339 骨骼，低位循环 40 帧（约 0.667 秒，60 fps），包含左右双侧采样。
   - 生成脚本：`tools/actions/build_liino_glide.py`
   - 详见：`docs/LIINO_LOW_GLIDE.md`

## 验证与测试

数据契约与反序列化边界通过 `BetterEndfield.ActionsPoseTests` 进行回归验证：

```powershell
cmake --build build/native --config Release --target BetterEndfield.ActionsPoseTests
& build/native/Release/BetterEndfield.ActionsPoseTests.exe native/modules/actions/assets/pose_aglina.bin native/modules/actions/assets/pose_liino.bin
```

## 构建分发

在 `native/CMakeLists.txt` 的 `BetterEndfield.Layout` 目标中，构建系统会自动将本目录下的 `.bin` 数据文件复制到安装包/交付目录的 `modules/actions/` 子路径下。
