# Better Endfield Next 4.2.0

Windows / Android 软件版本 4.2.0，Android versionCode 40200。
BEM Tools / DSH 升级到 1.6.8。
资源包不重发：core 仍为 1.6.3.3 / 1.6.4.4。

本次主题是**给模型包记录游戏版本基线**，为后续按版本适配打底。它不改变任何现有模型的显示效果。

## 为什么需要

模型包是照着某个版本的游戏资源做的。游戏更新后，如果某个部件改名或更换了几何，旧包就会失效。以前只能等"模型花了"再逐个排查是哪一批包受影响。

现在每个包可以记录它创建时的游戏版本——Windows 一个、Android 一个，外加一个热更序号。将来游戏改版时能直接按版本圈定需要适配的包，不必重新试一遍。

## 补全版本信息

已有的包没有这项记录，可以在软件里补上：

- **Windows**：模型管理页顶部新增「补全版本信息」按钮。
- **Android**：安装页「存储」区，与「清理未使用数据」并排。

点击后先扫描，给出三类结果：可补全几个、已有记录几个、无法识别几个。确认后才写入。

- 只按包内已有的资料批次判断，对不上的包**跳过而不是猜**，因此不会写错。
- 已记录过的包不会被覆盖。
- 只往包里写这一个字段，模型内容一个字节都不动。

模型卡片会显示当前平台的基线（例如 `基线 1.6.3 · hf1`）；没有记录时显示「基线未记录」。显示值永远等于包内实际内容，推断结果不会出现在卡片上。

## 创作者工具 1.6.8

- **打包自动带上基线。** 从角色资料（`catalog/` 条目的 `game`）读取，不用手动填。
- **用旧资料打新包会报错**，要求显式声明：
  `--game-baseline windows-x64=1.6.3,android-arm64=1.6.4,hotfix=0`
  旧资料对应哪个版本只有作者知道，工具不会替作者猜。
- **新增 `set-baseline` 命令**，可给已发布的包补记基线，不需要源工程：
  `BemConverter set-baseline <文件或目录> [--dry-run] [--out 目录]`
  写入只插入字段并平移目录偏移，payload 块原样搬运，写前写后都做复读校验。
- Blender 导出器同步：导出时从角色资料取基线，缺失即报错。

角色资料已回填完毕：9 月批次标 `1.5.3`，10 月 15 日批次标 `1.6.3 / 1.6.4 / hf0`，11 月 5 日热更批次标 `1.6.3 / 1.6.4 / hf1`。批次依据写在 `config/game-baseline-map.json`。

## 格式规范

`BEM_FORMAT_SPEC` 新增第 15 节，定义 manifest 根级的 `game_baseline`：

- 三个键必须齐全：`windows-x64`、`android-arm64`、`hotfix`（从 0 起，`0` 表示该版本首发无热更）。
- 版本值不限定格式，只推荐 `主.次.修订`；它只作为查表标签，不参与大小比较，也不做区间匹配。
- 缺失时整个字段忽略，不影响加载。老包不受影响。
- 写回 ZIP 容器时保留该字段。
- 运行时已经能读到它，但**暂不据此改变任何加载或修复行为**——按版本匹配修复的逻辑留到后续版本。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.2.0-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.2.0-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.8-win-x64.zip | Windows 创作者工具 |
| BEM-Tools-1.6.8-android-arm64.tar.gz | Android CLI |
| dsh-bem-creator-1.6.8.tgz | DSH 插件 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发。
