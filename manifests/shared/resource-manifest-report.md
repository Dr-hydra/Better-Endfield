# 终末地资源映射清单

## 动作

- 游戏 VFS manifest 版本：`5f521eb8-5202-dcdf-2412-02d992d0d771`
- 角色：31
- 动作：4075
- 未纳入角色：1

## 版本输入

- `Bundles/Windows/manifest.hgmmap`：`BF8AF16CBD7ACA2E1498CBC7F266779930934A020983A36108CB92E58CCB8B2F`（47235775 bytes）
- `TableCfg/AudioDialog.bytes`：`533763086BE069FC42CF7E48669601A921506E0A8F09A8FDC6A22879C8827E82`（4697156 bytes）

## 角色短语音

- `AudioDialog` 具名角色短语音 Event：937
- 覆盖角色：31
- 至少一种语言通过 BNK/HIRC 映射：595
- 四语均通过映射：595
- 原生 Wwise 容器事件：595
- v9 显式降级事件：342
- 无可读名称的 Bank：621
- Media 引用：6792（唯一 6424）

### 各语言

- Chinese：映射 595，缺 Bank 342，无效 0，Media 存在 1606 / 1606
- English：映射 595，缺 Bank 342，无效 0，Media 存在 1606 / 1606
- Japanese：映射 595，缺 Bank 342，无效 0，Media 存在 1606 / 1606
- Korean：映射 595，缺 Bank 342，无效 0，Media 存在 1606 / 1606

## PCK 覆盖

- `chinese-pck-1` c, s：存在 1606，缺少 0（`Endfield_Data/Persistent/VFS/E1E7D7CE/A3728E994CAA69BEABC06175423B8D7E.chk`）
- `english-pck-1` e, s：存在 1606，缺少 0（`Endfield_Data/Persistent/VFS/A31457D0/5F55D1D0B60B1027715F113D4235BE0F.chk`）
- `japanese-pck-1` j, s：存在 1606，缺少 0（`Endfield_Data/Persistent/VFS/F668D4EE/6220E8284303F6AEF3DA3589870FD33E.chk`）
- `korean-pck-1` k, s：存在 1606，缺少 0（`Endfield_Data/Persistent/VFS/E9D31017/720F0E510E16E27EC261FC9B224A9E7D.chk`）

## 解释

- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。
- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。
- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。
- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。
- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。
