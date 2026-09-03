# 终末地资源映射清单

## 动作

- 游戏 VFS manifest 版本：`2954fa80-23c1-1579-2b22-4ecfd6d70418`
- 角色：32
- 动作：4210
- 未纳入角色：1

## 版本输入

- `Bundles/Windows/manifest.hgmmap`：`23D975D590799BFF4C53A4EE39733B52FA2D67F4E1107ABA9187BA3913F32A3C`（50037905 bytes）
- `TableCfg/AudioDialog.bytes`：`1417104925FFD91FA3A239AC8014DDFF3F952023693949D9A25E3AF4F7465885`（4843212 bytes）

## 角色短语音

- `AudioDialog` 具名角色短语音 Event：970
- 覆盖角色：33
- 至少一种语言通过 BNK/HIRC 映射：618
- 四语均通过映射：618
- 原生 Wwise 容器事件：618
- v9 显式降级事件：352
- 无可读名称的 Bank：645
- Media 引用：7040（唯一 6659）

### 各语言

- Chinese：映射 618，缺 Bank 352，无效 0，Media 存在 1665 / 1665
- English：映射 618，缺 Bank 352，无效 0，Media 存在 1664 / 1664
- Japanese：映射 618，缺 Bank 352，无效 0，Media 存在 1665 / 1665
- Korean：映射 618，缺 Bank 352，无效 0，Media 存在 1665 / 1665

## PCK 覆盖

- `chinese-pck-1` c, s：存在 1665，缺少 0（`Endfield_Data/Persistent/VFS/E1E7D7CE/1844D9322FD68F41FB781DF1BC1CD905.chk`）
- `english-pck-1` e, s：存在 10，缺少 1654（`Endfield_Data/Persistent/VFS/A31457D0/E88476EE278BA4CDCAE36B0C0EBA4DFE.chk`）
- `english-pck-2` e, s：存在 1654，缺少 10（`Endfield_Data/Persistent/VFS/A31457D0/EF6B93FC939DCCAC408F2225A00FCE56.chk`）
- `japanese-pck-1` j, s：存在 1606，缺少 59（`Endfield_Data/Persistent/VFS/F668D4EE/6791DE59747DC8A1A00357BDB480A6E3.chk`）
- `japanese-pck-2` j, s：存在 59，缺少 1606（`Endfield_Data/Persistent/VFS/F668D4EE/CED27EEC11B08B003FBA09DF5957D5B8.chk`）
- `korean-pck-1` k, s：存在 59，缺少 1606（`Endfield_Data/Persistent/VFS/E9D31017/8AF084B294FBBC895335F68653E1A61B.chk`）
- `korean-pck-2` k, s：存在 1606，缺少 59（`Endfield_Data/Persistent/VFS/E9D31017/CE9159205153937A164BBCBC8CF770E2.chk`）

## 解释

- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。
- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。
- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。
- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。
- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。
