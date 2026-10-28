# 终末地资源映射清单

## 动作

- 游戏 VFS manifest 版本：`2954fa80-23c1-1579-2b22-4ecfd6d70418`
- 角色：37
- 动作：4719
- 未纳入角色：0

## 版本输入

- `Bundles/Windows/manifest.hgmmap`：`D6210FB23C0ADBC5B1C15FD506A5D910BF002A6F228B3B340EE4E6D69A78A905`（54530637 bytes）
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

- `chinese-pck-1` chinese, sfx：存在 6，缺少 1765（`Endfield_Data/Persistent/VFS/E1E7D7CE/00388C6B3EAAB63D886FFFAEC6094C7E.chk`）
- `chinese-pck-2` chinese, sfx：存在 1765，缺少 6（`Endfield_Data/Persistent/VFS/E1E7D7CE/E1CFD45D3E8072857C3D7B15871D72EB.chk`）
- `english-pck-1` english, sfx：存在 1656，缺少 114（`Endfield_Data/Persistent/VFS/A31457D0/DA3FA56FA899EF08C207D716B13F8D8A.chk`）
- `english-pck-2` english, sfx：存在 114，缺少 1656（`Endfield_Data/Persistent/VFS/A31457D0/E590002D7FF16F66C4FD491D70D2700C.chk`）
- `japanese-pck-1` japanese, sfx：存在 165，缺少 1606（`Endfield_Data/Persistent/VFS/F668D4EE/10EF89B45E3C188379CEF587A1BFB808.chk`）
- `japanese-pck-2` japanese, sfx：存在 1606，缺少 165（`Endfield_Data/Persistent/VFS/F668D4EE/8B4288CB4A47E92B8A8AB30A011ACADD.chk`）
- `korean-pck-1` korean, sfx：存在 165，缺少 1606（`Endfield_Data/Persistent/VFS/E9D31017/CF9273716D741D96B5682DE1E7FCA1BF.chk`）
- `korean-pck-2` korean, sfx：存在 1606，缺少 165（`Endfield_Data/Persistent/VFS/E9D31017/F64E64F99DE10C2D44CD5D1C024B4455.chk`）

## 解释

- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。
- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。
- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。
- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。
- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。
