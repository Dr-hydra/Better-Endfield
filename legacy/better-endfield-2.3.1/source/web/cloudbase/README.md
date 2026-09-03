# CloudBase 部署

创建 `combat_records`、`combat_record_parts`、`combat_board_snapshots` 和
`combat_uploads` 四个文档集合。部署 `combat-api` 为 HTTP 云函数，并仅允许
`https://www.bilibili.com` 与本地开发地址访问；将 HTTP 地址写入前端构建环境变量
`VITE_CLOUDBASE_API`。

将 `cleanup` 部署为每天运行一次的定时云函数。生产环境建议为以下字段建立索引：

- `combat_records`: `dungeonId`、`ownerHash + uploadedAt`、`ranked + expireAt`
- `combat_record_parts`: `recordId + index`
- `combat_uploads`: `uploadId + index`、`createdAt`

榜单请求只读取 `combat_board_snapshots`；完整记录仅在打开详情时解压读取。
