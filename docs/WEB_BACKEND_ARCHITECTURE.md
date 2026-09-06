# 网页版后端架构

网页版有两套后端：用户身份与个人云存档走 B站 Toy 平台，排行榜与分享记录走 CloudBase 云函数。
本文描述两者的分工、数据在哪一层转换，以及记录的生命周期。

## 职责边界

| | Toy 平台 | CloudBase 云函数 |
|---|---|---|
| 用户身份 | `getUserProfile()` → 昵称/头像/`toyOpenId` | 不做登录，认 `ownerToken` |
| 个人云存档 | `setCloudStorage` / `getCloudStorage` | — |
| 寻访记录 | 同上（`lib/gachaCloud.ts`） | — |
| 排行榜 | ✗ 能力不足，见下 | ✓ 按关卡分榜 |
| 分享记录 | `share()` / `getQrCode()` 只负责拉起面板 | ✓ 存储与过期 |

**本地 JSON → 紧凑格式的转换全部在浏览器里完成**，两个后端都只搬运已经压好的字节。
所需的字典、id 表、关卡表由 `tools/CombatDataExporter` 在构建期产出，随发布版本分发。

## Toy 侧：云存档

### 容量预算

Toy 云存储的硬限制（SDK 1.6.0）：

- 单个 Toy 最多 **128 个 key**，按「登录用户 + Toy」隔离
- key ≤128 字节，只能含字母、数字、下划线、短横线，不能以 `__` 开头
- **value ≤1024 字节**——是**字节**不是字符，所以用多字节字符提高密度无效

`lib/archive.ts` 用 base64url 存 deflate 后的字节，`PART_CHARS = 960`，
即每个 key 承载 **720 二进制字节**。128 片中 **28 片预留给寻访记录**
（`GACHA_RESERVE`），战斗存档可用 100 片。

预留是单向的：寻访真实占用超过 28 片时按真实值扣减，但战斗存档永远挤不掉那 28 片。
`archiveQuota()` 返回完整账本，云存档页面把它画成进度条，用户据此决定删什么。

### key 布局

```
be8_own              owner token（写入 CloudBase 的凭证）
be8_idx              索引头 {version, pages}
be8_idx_<p>          索引页：每页 8 条 ArchiveMeta 的 JSON
be8_rec_<id>_<n>     某场存档的 BEC 分片
```

key 只用 `[A-Za-z0-9_-]`。写入分片后会回读校验，校验失败即回滚并且不提交索引，
所以索引永远不会指向半截数据。

### 档位

`ARCHIVE_TIERS` 四档，用户每次保存时自选，界面直接显示各档要几片。
以 51.3 秒的真实记录实测（deflate 后）：

| 档 | layers | 字节 | 片 |
|---|---|---|---|
| 名片 | 头部+队伍+角色聚合+归因表+火花线 | 1,092 | 2 |
| 摘要 | +技能统计 | 1,218 | 2 |
| 完整 | +动作轨道+增益轨道 | 1,967 | 3 |
| 全量 | +逐次命中 | 2,838 | 5 |

## CloudBase 侧：排行榜与分享

**这一侧不碰数据库。** 环境 `endfield-d3gdy9wg4afba9d16` 是 PostgreSQL 型云开发环境，
但 PG 按核秒计费，且是被唤醒就开始算、不是按查询的 CPU 算——实测一个开发日的零星操作
就吃掉 828 核秒 = 78.7 资源点。公开榜单每个访客都是一次读，让数据库撑读路径等于花钱买
实例常驻。同环境的 `fh6-decode` 早就是这个结论（只用云存储 + 函数内存缓存），
它的注释还记了另一条：云函数外网出流量 800 点/GB 是价目表上最贵的一项。

于是读写分成两条路：

```
读  浏览器 --<script>--> 云存储公共域名（前面有 CDN）        无函数、无网关、无数据库
写  浏览器 --fetch-----> HTTP 网关 /combat -> combat-api    上传/删除/两个开关，低频
```

| 读路径 | 单次成本 | 1500 资源点能撑 |
|---|---|---|
| PG 查询 | 唤醒计费 | 约 19 天 |
| 云函数代理（128MB + gzip） | ~0.015 点 | 10 万次 |
| 云存储 `<script>` 直读 | ~0.009 点，CDN 命中后 0 | 17 万次以上 |

### 为什么读是 JSONP

存储对象公开可读，但**不带 `access-control-allow-origin`**，浏览器跨域 `fetch` 一律被拦。
新的 `/v1/storages/object/public/...` 路径更严，带任何 `Origin` 直接 403，连安全域名白名单里
的域也一样；而往白名单里加 `www.bilibili.com` 会被套餐挡回来。

经典 `<script>` 加载不是 CORS 请求，压根不发 `Origin`，所以走得通。对象因此写成
`__be("<key>", <json>);`，且文件名以 `.js` 结尾——桶要回 `application/javascript`，
否则 Chrome 的 ORB 会在执行前把 `application/json` 的响应丢掉。

### 数据模型

桶 `combat`，公开的一半是浏览器直读的 JSONP，私有的一半藏在不可猜的路径段后（gzip）。

| 对象 | 谁读 | 内容 |
|---|---|---|
| `index.js` | 浏览器 | 每个分类的记录数 |
| `cat/<分类>.js` | 浏览器 | 该分类每个有记录的关卡，各前三名 |
| `stage/<关卡>.js` | 浏览器 | 单关卡前 20，两个维度各一份 |
| `rec/<shortId>.js` | 浏览器 | BEC1 快照 + 昵称头像视频号 |
| `<私有>/cat/<分类>.json` | 仅函数 | 该分类全部行，含 owner hash、退榜的、掉榜的 |
| `<私有>/own/<ownerHash>.json` | 仅函数 | 某人的分享列表（≤20） |
| `<私有>/index.json` | 仅函数 | 索引的可写副本 |

榜单不是存起来的排名，是每次写入时从分类行现算的（`board.js`，纯函数）。
分类粒度是照着页面来的：排行榜就是按游戏内分类浏览，一次读一个对象。

### 一致性

对象存储没有事务，也没有 CAS，所以两件事要盯：

- **函数不能通过 CDN 读自己的状态。** 即使对象写着 `no-cache`，CDN 也会在几秒内回旧副本，
  而这个函数是读-改-写——一次陈旧读会静默吞掉上一次写入的行（实测连续三次上传只剩两行）。
  私有对象的读一律加唯一 query 强制回源。
- **并发写同一分类仍可能丢一次。** 写完再读回来验证自己的改动还在，不在就重做，最多三次。
  这把静默丢失变成重试，但不是原子的。上传频率很低，这个取舍是有意的。

### 身份

- 写入凭证是 `ownerToken`：客户端随机生成，存在 Toy 云存储的 `be8_own`。
  Toy 云存储按「登录用户 + Toy」隔离，天然只有本人的浏览器能读到。
  服务端只存 `sha256(ownerToken)`，且只出现在私有对象里
- 私有对象里的 owner hash 不是凭证——写入要拿得出 token 本身，hash 只是它的摘要
- 按需求不做反作弊：服务端不校验伤害数值是否自洽

### 榜单字段来自快照本身

`ingest()` 解析 BEC 的 HEAD 层拿到时长、DPS、rDPS、总伤害、命中数，**不采信 body 里的
同名字段**，所以榜单上的数字与点开详情看到的必然一致。body 只提供快照推不出来的东西：
关卡 id、昵称头像、B站视频号、队伍摘要。

### 榜单规则与生命周期

- 按关卡分榜，两个维度（`dps` / `time`），各 20 名；单用户单关卡只占一个名次，同分先到先得
- 只收录每个关卡的最高难度（关卡表在导出时就按难度族收敛过）
- 分享默认参加排行榜，可在「我的分享」退出；退出后链接仍然有效
- 每人最多 20 条分享

```
在榜        f = 0        永久保存
掉出榜单    f = 掉榜时刻  每天 04:30 由 combat-api 的定时触发器维护
超过 7 天   删除记录对象，并从该用户的分享列表里移除
```

清理逻辑要用到存储布局，所以挂在 combat-api 的定时触发器上，而不是再开一个函数抄一遍布局。

### 写入接口

| 方法 | 路径 | 说明 |
|---|---|---|
| POST | `/records` | 上传，返回 `{shortId, categoryId}` |
| DELETE | `/records/:shortId` | 删除自己的分享 |
| PATCH | `/records/:shortId/ranked` | 参与 / 退出排行榜 |
| PATCH | `/records/:shortId/video` | 绑定 B站视频 |
| GET | `/me/records?ownerToken=` | 我的分享 |
| GET | `/health` | 探活 |

浏览器到网关这一跳是跨域的，由函数自己回的 CORS 头解决，实测
`Origin: https://www.bilibili.com` 正常返回。

## 为什么排行榜不能用 Toy 自带的

Toy SDK 提供 `submitScore` / `getRankList` / `getMyRank`，但有三个致命限制：

1. **`board` 固定只有 1 / 2 / 3。** 游戏有 354 个关卡，三个榜位放不下。
   把关卡编进 score 高位也不行——排序会变成「先按关卡再按分数」，读满 100 名也覆盖不到几关。
2. **`score` 是 24 位整数**（-16777216 ~ 16777215），且服务端只保留历史最高分。
3. **返回值只有 `{ rank, score, nickname, avatar }`**，没有任何字段能挂记录 id，点开详情无从谈起。

所以按关卡分榜、条目可点进详情，只能由 CloudBase 承担。

## 统一的数据格式：BEC1

`lib/bec.ts`。**Toy 云存档与 CloudBase 记录用同一种格式**，只是 `layerMask` 不同：
云存档默认「完整」档，榜单/分享默认「全量」档。

- 列式 varint + delta 编码，ids 全部是**永久数字 id** 的下标
- 单条 `deflate-raw` 流，用 `fflate` 压/解（各端一致，不依赖 CompressionStream）
- 头部带 `layerMask` 与 `idSpaceVersion`，读端据此知道缺什么

层的划分与实测开销见 `lib/bec.ts` 顶部注释与 `lib/bec.test.ts`。

### 增益轨道的过滤

`LaneFilter` 默认 `semantic`：丢掉 `contributionKind === "other"` 的状态
（特效标记、出生、巡逻回血、韧性抗性）。真实记录里这是 466 条中的 358 条、
该层约 70% 的字节，而时间轴默认也不显示它们。

不用更激进的 `credited`（只留实际吃到伤害的实例），因为那会让 uptime 失真——
一个常驻但恰好没覆盖到命中的 buff 会整条消失。

### 永久数字 id

`tools/CombatDataExporter/id_registry.json` 是**只增不改**的注册表，
覆盖角色、技能（含运行时别名）、天赋、武器、装备、套装、关卡、关卡分组，
以及 `research/combat-jsondata/Data/Json/BuffData` 下 2,872 个 buff 的 id
（只读文件名，内容是 MemoryPack 二进制，见 `BUFF_TABLE_EXPORT.md`）。

当前 4,425 个 id。导出为 `web/src/data/combat-ids.min.json`（149 KB / 22.8 KB gz），
**按需懒加载**——只有编解码快照时才下载，首页与寻访页不受影响。
`combat-dict.min.json` 里只留一个 `idv` 版本号。

### 转换在浏览器完成

```
本地 JSON（数十 MB）
   │  Web Worker：解析 → 归因 → 汇总
   ▼
CombatRecord（内存）
   │  encodeSnapshot：字典下标 + 列式 varint + deflate
   ▼
BEC 快照（KB 级）──> Toy 云存档 / CloudBase 上传
```

解码产出的 `CombatRecord` 带 `provenance: "snapshot"`，UI 据此提示
「云端快照 · 不含逐次命中的乘区明细」。`raw.effects` / `raw.actions` 留空，
因为没有任何消费方读它们。

## 端上差异

手机端不解析本地 JSON（原始记录动辄数十 MB），导入区在窄屏下换成说明文案，
但**云存档与榜单详情在手机上完全可用**——快照解压后只有几百 KB。

## 关卡表

`web/src/data/combat-stages.min.json`，由导出器 `build_stage_table()` 产出：
354 个关卡分入 94 个组。原始 `DungeonTable` 有 77 个关卡没有 `seriesId`、
若干分组在 `DungeonSeriesTable` 里查不到名字，导出时按 id 前缀补分组、按固定表补名字，
**不再有未分组关卡**。榜单页左侧按「分组 → 关卡」两级渲染，只列出有记录的关卡。

## 已修正的问题

改造过程中一并修掉的既有缺陷：

1. `combat-api` 的 `ingest()` 原本调用 `rebuildBoard(record.dungeonId)`，但记录顶层没有
   这个字段（它在 `record.battle.dungeonId`），因此每次上传都在给 `undefined` 重建榜单，
   真实关卡的榜单快照从未更新过。现在榜单字段全部从 BEC 头部解析，关卡 id 显式校验非空。
2. `validateRecord()` 只接受 `schemaVersion === 11`，而 native 写的是 14——任何上传都会被拒。
   该函数随 JSON payload 一起删除。
3. `lib/toy.ts` 的 `openBilibiliVideo()` 调用 `navigate({ url })`，但 SDK 要求
   `{ type, id, extra }`。已改为 `navigate({ type: "video", id: bvid })`。
4. `archive.ts` 的 key 含冒号（`be8:owner`），不符合 Toy 的 `[A-Za-z0-9_-]` 约束。已全部改名。
5. `archive.ts` 与 `gachaCloud.ts` 各自假设能用满 128 个 key。现在 `archiveQuota()`
   是统一账本，战斗存档永远挤不掉给寻访预留的 28 片。

## 待办

- **buff 资源表的在线导出**（见 `BUFF_TABLE_EXPORT.md`）。id 注册表已经收录了全部
  2,872 个 buff id，但机制字段（叠加规则、持续时间、真实图标路径）还需要在游戏进程里
  遍历 `BattleDataLoader.TryGetBuff` 才能拿到。表就位后，schema 14 的
  `statusApply.stacking` 可以从记录里整块移除。
- 榜单页目前只按记录数排列关卡分组，等真实数据进来后可能需要按版本/难度再排一遍。
