# v13 骨骼姿态数据：修正映射 + 细修循环 + 骨骼取舍

2026-09-13。数据本身在 2026-09-13 定稿。2026-09-14 模块改为角色档案表驱动（Actions 1.12.0）后，同一份字节以 `actions/pose_aglina.bin` 的名字部署，内容未变（sha256 `ed77144f…`）；本目录同时保留旧名 `aglina_pose_v12.bin` 与新名 `pose_aglina.bin` 两个副本。

## 与 v12 数据的差别

1. **来源**：`aglina-loop-fine-v1`（修正 101 通道 muscle 映射后的重建 + 真实数据交叠接缝），不再是 `aglina-loop-test-v1`。右腿、双臂恢复正常运动。
2. **骨骼取舍**：399 → 226 根。
   - 剔除 84 根在原动作里完全不动的骨骼（各 Nub 末端、`IK_Root`、`Funnel_Point_*`、`Mesh_all`、各 lod 网格节点、`Root`、`Bip001_Pelvis`、`Bip001_HeadNub`）。它们由原生保持即可，减少每帧写入。
   - 剔除 89 根面部与视线骨骼（嘴唇、下颌、牙齿、舌、鼻、脸颊、眼、眉、`head_up/mid/dn_jnt`、`Head_Lookat_Joints`、`Head_Local`），交还游戏的口型、注视、表情系统。冲刺过程中有语音事件，v12 覆盖嘴部会与口型系统互相覆盖。
   - 保留全部 22 根 Humanoid 骨骼（标记 required）、手指、扭转/矫正骨、头发、耳饰、飘带、尾饰、袖口、道具挂点、武器挂点与 IK 目标骨。
3. **时序**：104 个采样点、60 Hz、周期 103 帧、相位 18、入口 40/208，与 v12 一致，运行时无需改动。

## 校验

- `validate_aglina_pose_bank.py` 按 `pose_policy.h` PoseBank::Load 的全部规则复核通过（魔数、维度、路径规则、required ≥ 10、四元数模长、端点闭合、无多余数据）。
- 文件 1,348,105 字节；已替换 `build/native/stage/Release/modules/actions/aglina_pose_v12.bin`，替换前的文件备份在 `backup/aglina_pose_v12.stage-before-v13.bin`（与 v12 产物字节相同）。

## 线上安装目录的现状

`%LOCALAPPDATA%\BetterEndfield\BetterEndfield.ini` 的 `modules_root` 指向 `artifacts/BetterEndfield-win-x64/modules`。宿主日志 `logs/BetterEndfield.log.bak` 证明 v12 试播发生在 2026-09-10 03:48–03:50（`bone overlay bound: matched=399`，`TailLate pose applied` 62 条，`mean_apply_us` 约 157）。但 2026-09-10 08:00 与 09-11 16:00 的两次主线重建后，该目录里已经没有 `BetterEndfield.Actions.dll`、`betterendfield.actions.module.ini` 和 `actions/` 数据，`BetterEndfield.ini` 也没有 `[betterendfield.actions]` 节。stage 的宿主（09-07 构建）与 artifacts 的宿主（09-10 构建）不是同一份，本分支还改了 `dynamic_resolver.cpp` / `type_name_contract.h`，所以本轮没有把单个 DLL 拷进线上目录，也没有重新构建（会覆盖 `artifacts/betterendfield-native-build` 里正在进行的 custom-model 构建）。

要实机测试，需要从本工作树运行 `scripts/BuildBetterEndfield.ps1`（可用 `-PublishDir` 发布到单独目录并在启动器里选择该目录的注入器），或手工把 stage 的 Actions 模块三件套复制进 modules 目录，并在 `BetterEndfield.ini` 加入：

```ini
[betterendfield.actions]
schema_version=2
enabled=true
external_loop=true
diagnostics=true
```


## 2026-09-13 21:45 测试发布

已从工作树 `dev/aglina-pose-overlay` 运行 `scripts/BuildBetterEndfield.ps1 -PublishDir "E:\Dr.Hydra\Better Endfield\artifacts\BetterEndfield-win-x64-aglina-test"`，完整发布到该测试目录（启动器、runtime、modules、loaders、payloads）。`modules/` 含本分支构建的 `BetterEndfield.Actions.dll` 与 `betterendfield.actions.module.ini`；`modules/actions/aglina_pose_v12.bin` 为本目录的 v13 文件（sha256 ed77144f…，PoseBank 校验通过）。v12 模式不加载 `aglina_native_return_v2.bundle`，所以未复制。

`%LOCALAPPDATA%\BetterEndfield\BetterEndfield.ini` 已加入上面的 `[betterendfield.actions]` 节，并把 `[Host] modules_root` / `[Loader] install_root` 指向测试目录；改动前的文件备份为同目录 `BetterEndfield.ini.before-aglina-test-20260913-214542`。启动器里的“艾洁莉娜：持续特殊冲刺”开关只重写 `enabled`，会保留 `external_loop=true`。要切回线上版本：用线上目录的 `BetterEndfield.exe` 重新注入一次（它会重写两处路径），或直接还原该备份。

在工作树里构建的两个坑：(1) 工作树按 `core.autocrlf=true` 检出，`voice-event-media-manifest.json`、`combat-semantics.besem`、`buff-sources.bemap` 变成 CRLF，构建脚本按字节哈希校验索引会报“stale”；已从主检出复制 LF 版本覆盖（git 内容不变，只是 `git status` 会因换行显示 M）。(2) `native/tests/` 被 .gitignore 忽略，CMake 却引用 `tests/combat_semantics_tests.cpp`；已从提交 c5ada656 取出放入工作树。工作树的原生构建目录是 `.worktrees/dev-aglina-pose-overlay/artifacts/betterendfield-native-build`，与主检出的构建目录互不影响。

## 观察要点

宿主日志中 `Aglina v12: bone-pose file loaded` 这一行仍会打印固定文案“104 samples”，这是 DLL 里写死的字符串，与本版一致所以未改。`bone overlay bound: matched=` 应为 226（或略少，缺少的可选骨骼会单独记录）。如出现 `bone binding rejected`，说明 required 骨骼路径与实机层级不符，请回传日志。

本轮未启动游戏。生成脚本：`prepare_aglina_pose_overlay_v13.py`。

## 2026-09-14 更名与再部署

模块通用化后数据文件按角色命名。同一份字节现以 `pose_aglina.bin` 部署到：

- `build/native/stage/Release/modules/actions/pose_aglina.bin`（旧名 `aglina_pose_v12.bin` 已删除）
- `artifacts/BetterEndfield-win-x64-aglina-test/modules/actions/pose_aglina.bin`

同目录另有梨诺的 `pose_liino.bin`，见 `tmp_analysis/liino-pose-overlay-v1/README.md`。
