# 终末地资源映射清单

## 动作

- 游戏 VFS manifest 版本：`2954fa80-23c1-1579-2b22-4ecfd6d70418`
- 角色：37
- 动作：4719
- 未纳入角色：0

## 版本输入

- `Bundles/Android/manifest.hgmmap`：`FFA614CA2932B8C09892E7000EADE968`（53700330 bytes）
- `TableCfg/AudioDialog.bytes`：`649E7513131B8CAB64F91A0F7DFA0FF90D3DDFB0F4BC7C1C05967021C93CB04C`（5388652 bytes）

## 角色短语音

- `AudioDialog` 具名角色短语音 Event：1025
- 覆盖角色：34
- 至少一种语言通过 BNK/HIRC 映射：660
- 四语均通过映射：660
- 原生 Wwise 容器事件：660
- v9 显式降级事件：365
- 无可读名称的 Bank：699
- Media 引用：7488（唯一 7083）

### 各语言

- Chinese：映射 660，缺 Bank 365，无效 0，Media 存在 1771 / 1771
- English：映射 660，缺 Bank 365，无效 0，Media 存在 1770 / 1770
- Japanese：映射 660，缺 Bank 365，无效 0，Media 存在 1771 / 1771
- Korean：映射 660，缺 Bank 365，无效 0，Media 存在 1771 / 1771

## PCK 覆盖

- `chinese-pck-1` chinese, sfx：存在 1771，缺少 0（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/E1E7D7CE/C8B18CE5AF55BDC131CE168D08FADE69.chk`）
- `english-pck-1` english, sfx：存在 3，缺少 1767（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/A31457D0/9A51A883293C0A72B8D2C171B3F51726.chk`）
- `english-pck-2` english, sfx：存在 1767，缺少 3（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/A31457D0/C830E93FDB642740B99BD3EEF9FDD6E2.chk`）
- `japanese-pck-1` japanese, sfx：存在 1715，缺少 56（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/F668D4EE/62CFCED4F31B46803AFD058CF4CB1643.chk`）
- `japanese-pck-2` japanese, sfx：存在 56，缺少 1715（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/F668D4EE/93937607BD7D32C4E905DEA36C0E4CB6.chk`）
- `korean-pck-1` korean, sfx：存在 56，缺少 1715（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/E9D31017/196C7041D4C16FC42431C5C7E1820A59.chk`）
- `korean-pck-2` korean, sfx：存在 1715，缺少 56（`/sdcard/Android/data/com.hypergryph.endfield/files/VFS/E9D31017/B8D63126D7ED7F01EBF5C756668D685D.chk`）

## 解释

- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。
- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。
- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。
- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。
- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。
