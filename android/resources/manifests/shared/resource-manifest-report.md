# 终末地资源映射清单

## 动作

- 游戏 VFS manifest 版本：`2954fa80-23c1-1579-2b22-4ecfd6d70418`
- 角色：32
- 动作：4210
- 未纳入角色：1

## 版本输入

- `Bundles/Android/manifest.hgmmap`：`65455F5554D87BF3E6B2A81A641E7DDDC38CBD8378A3C8C5F20E3DE828E6C57E`（49558742 bytes）
- `TableCfg/AudioDialog.bytes`：`48EF3EFB67D417A38EDA94B04C1309E88162049F28750CAD941AD803DD82AC7E`（4875268 bytes）

## 角色短语音

- `AudioDialog` 具名角色短语音 Event：996
- 覆盖角色：33
- 至少一种语言通过 BNK/HIRC 映射：638
- 四语均通过映射：638
- 原生 Wwise 容器事件：638
- v9 显式降级事件：358
- 无可读名称的 Bank：625
- Media 引用：7252（唯一 6859）

### 各语言

- Chinese：映射 638，缺 Bank 358，无效 0，Media 存在 1715 / 1715
- English：映射 638，缺 Bank 358，无效 0，Media 存在 1714 / 1714
- Japanese：映射 638，缺 Bank 358，无效 0，Media 存在 1715 / 1715
- Korean：映射 638，缺 Bank 358，无效 0，Media 存在 1715 / 1715

## PCK 覆盖

- `chinese-pck-1` chinese, sfx：存在 1715，缺少 0（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/E1E7D7CE/F2F65C121E37C9A4C694A27A3FD1F903.chk`）
- `english-pck-1` english, sfx：存在 1714，缺少 0（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/A31457D0/FC0EB9CB8B7B21E4FC85D7AAD0A8498A.chk`）
- `japanese-pck-1` japanese, sfx：存在 1715，缺少 0（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/F668D4EE/2F1317FEE2F58878B7AA28C20F8F23EE.chk`）
- `korean-pck-1` korean, sfx：存在 1715，缺少 0（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/E9D31017/6C17D7CA48BD06744AD80660A538154D.chk`）

## 解释

- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。
- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。
- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。
- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。
- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。
