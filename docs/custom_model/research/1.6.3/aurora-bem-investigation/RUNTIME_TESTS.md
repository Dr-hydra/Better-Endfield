# 昼雪旧 BEM 兼容：独立验证

2026-10-15，Windows x64 Release。测试不读取或修改真实游戏状态，不启动或注入游戏；未额外计算产物哈希。

## 定向测试

- `native/tests/aurora_legacy_compat_tests.cpp`：228 项通过。验证旧 C1 87,120 / 当前 C1 87,288 索引和 23,141 顶点的精确边界；角色、资源、组件、显式目标隔离；87 个骨槽映射及 10 个迁移槽；重复/缺失骨、未知计数、错误路径、NaN/Inf 和 bindpose 漂移拒绝；共享 matcher 仍保留原有严格计数规则。
- `native/tests/aurora_runtime_checks.inc`：666 项通过。通过现有假 Unity host 调用真实生产函数，覆盖 world/UI、keep/hide、同步准备、跨组件骨与 bindpose 配对、完整 87 槽位 palette、首次启用/热切换 discovery、Job 从 Capture 到 Commit 前的准备、未知 donor 拒绝。
- SavedOriginal 场景实际执行假对象提交，令当前 custom palette 与原始 donor 顺序不同，再验证 discovery 与 recapture 能识别保存的原始来源。修改记录中的原始骨路径后必须拒绝。所有只读/准备阶段核对真实 renderer setter 调用计数保持不变。

独立审查发现首次启用/热切换 preflight 最初未走兼容分支；已补为与 Capture 共用精确候选选择及完整 donor 证明。新增测试曾准确触发此失败，修复后通过。

## 既有回归

以下生产 binding harness 用例均通过：主套件、`--async`、`--first-enable`、`--geometry`、`--clone-support`、`--npc-roots`。输入为原审计保留的 1.0/1.4 合成包复制件，位于 `build/aurora-compat-native/fixtures`。

冲刺修复遵照用户要求仅编译 `BetterEndfieldNext.Actions`，未重复运行任何 Actions 测试。

## 构建与证据

仅构建以下 native targets，没有调用全量发布脚本或编译 Media/Combat：

```powershell
cmake -S native -B build/aurora-compat-native -G "Visual Studio 17 2022" -A x64
cmake --build build/aurora-compat-native --config Release --target BetterEndfieldNext.CustomModel BetterEndfieldNext.CustomModelBindingTests --parallel 2
build/aurora-compat-native/Release/BetterEndfieldNext.CustomModelBindingTests.exe --aurora-legacy
cmake --build build/aurora-compat-native --config Release --target BetterEndfieldNext.Actions --parallel 2
```

纯策略测试有独立入口：`cmake -S native/tests/aurora_legacy_compat -B build/aurora-compat-policy -G "Visual Studio 17 2022" -A x64`，构建后运行 `build/aurora-compat-policy/Release/AuroraLegacyCompatibilityTests.exe`。

可供最终整包签名/封装的产物位于：

- `build/aurora-compat-native/stage/Release/modules/BetterEndfieldNext.CustomModel.dll`
- `build/aurora-compat-native/stage/Release/modules/BetterEndfieldNext.Actions.dll`

构建日志及各回归日志分别留在 `build/aurora-compat-native`、`build/aurora-compat-policy`。此步骤没有写入实际安装目录或已发布版本目录。

## 验证范围

独立测试证明选取、拒绝、palette 重排、SavedOriginal、同步/Job 准备路径的行为，不能替代真实旧包画面验收。兼容规则仅适用于精确验证的 Windows 昼雪旧版非显式 C1；未知未来 donor、其他角色、其他组件及显式 BEM 1.4 目标仍走原有严格规则。实际 world/UI 采集和旧包实机验收由主任务单独记录。
