# CloudBase 部署

排行榜与分享记录的后端。个人云存档与登录走 Toy 平台，不在这里。

## 为什么没有数据库

环境 `endfield-d3gdy9wg4afba9d16`（ap-shanghai）是 PostgreSQL 型云开发环境，
但排行榜**一个数据库都不碰**。PG 按核秒计费，而且是被唤醒就开始算、不是按查询的
CPU 算：一个开发日的零星操作就吃掉了 828 核秒 = 78.7 资源点。公开榜单每个访客都是
一次读，用数据库撑读路径等于花钱买实例常驻。

同环境的 `fh6-decode` 早就是这个结论——它也只用云存储对象加函数内的内存缓存，
并且把最贵的一条记在了注释里：云函数外网出流量 800 点/GB 是价目表上最贵的一项。

所以：

```
读  浏览器 --<script>--> 云存储公共域名（前面有 CDN）        无函数、无网关、无数据库
写  浏览器 --fetch-----> HTTP 网关 /combat -> combat-api    上传/删除/两个开关，低频
```

## 为什么读是 JSONP

存储对象是公开可读的，但**不带 `access-control-allow-origin`**，浏览器跨域 `fetch`
一律被拦（新的 `/v1/storages/object/public/...` 路径更严，带任何 `Origin` 直接 403，
连安全域名白名单里的域也一样；而往白名单里加 `www.bilibili.com` 会被套餐挡回来）。

经典 `<script>` 加载不是 CORS 请求，压根不发 `Origin`，所以走得通。对象因此写成
`__be("<key>", <json>);`，文件名以 `.js` 结尾——这样桶回的是 `application/javascript`，
否则 Chrome 的 ORB 会在执行前把 `application/json` 的响应丢掉。

## 存储布局

桶 `combat`，公共域名 `https://656e-endfield-d3gdy9wg4afba9d16-1474357318.tcb.qcloud.la/combat`。

| 对象 | 谁读 | 内容 |
|---|---|---|
| `index.js` | 浏览器 | 每个分类的记录数，用来点亮导航 |
| `cat/<分类>.js` | 浏览器 | 该分类每个有记录的关卡，各前三名 |
| `stage/<关卡>.js` | 浏览器 | 单关卡前 20，伤害榜与速通榜各一份 |
| `rec/<shortId>.js` | 浏览器 | BEC1 快照（base64url）+ 昵称头像视频号 |
| `<私有前缀>/index.json` | 仅函数 | 上面那份索引的可写副本 |
| `<私有前缀>/cat/<分类>.json` | 仅函数 | 该分类**全部**行，含 owner hash、未上榜的、退榜的 |
| `<私有前缀>/own/<ownerHash>.json` | 仅函数 | 某人的分享列表（≤20） |

私有那半藏在一段不可猜的路径后（`COMBAT_DATA_KEY`），内容 gzip。里面的 owner hash
不是凭证——写入要拿得出 token 本身，hash 只是它的摘要。

榜单不是存起来的排名，是每次写入时从分类行现算的：每人每关取最优、按维度排序、取前 20。
规则在 `functions/combat-api/board.js`，纯函数，不碰 I/O。

## 两个坑

**函数不能通过 CDN 读自己的状态。** 桶前面有 CDN，即使对象写着 `no-cache` 也会在几秒内
回一份旧的。这个函数是读-改-写自己的状态，一次陈旧读就会静默吞掉上一次写入的行——实测
连续三次上传只剩两行。所以 `store.readPath` 对私有对象一律加唯一 query 强制回源。

对象存储没有 CAS，同一分类的并发写仍可能丢一次。`mutateCategory` 写完会再读回来验证自己
的改动还在，不在就重做，最多三次——把静默丢失变成重试，但不是原子的。上传频率很低，
这个取舍是有意的。

**`tcb db execute --sql` 只取参数的第一行**，多行脚本会被悄悄截断（开头是注释就什么都不做，
而且照样报「成功」）。要先去注释压成一行再传：

```bash
tcb db execute --sql "$(python -c "
import re,sys,pathlib
text = re.sub(r'--[^\n]*', '', pathlib.Path(sys.argv[1]).read_text(encoding='utf-8'))
print(' '.join(line.strip() for line in text.splitlines() if line.strip()))
" sql/01-bucket.sql)"
```

## 部署

```bash
cd web/cloudbase
tcb fn deploy combat-api --force
tcb deploy --only gateway          # 仅新增 /combat 路由
```

环境是共享的，另有 `fh6-decode` 与 `*` 域名上的 `/fh6` 路由，部署时不要碰它们
（`tcb deploy --only gateway` 只声明本项目的路由，dry-run 已确认对目标域名是增量的，
不产生删除步骤）。

`stage-map.json`（关卡 id → 分类 id）由导出器生成，跟着函数一起发布：
函数要靠它知道一条新记录该并进哪个分类文件。

前端两个地址写在 `web/.env.production`：`VITE_COMBAT_STORAGE`（读）与
`VITE_CLOUDBASE_API`（写）。只填前者就是只读模式，排行榜能看不能传。

## 接口

写入接口全部要 `ownerToken`（存在 Toy 云存储里，只有本账号的浏览器有）。

| 方法 | 路径 | 说明 |
|---|---|---|
| `POST` | `/records` | 上传快照，返回 `{shortId, categoryId}` |
| `DELETE` | `/records/:id` | 删除自己的分享 |
| `PATCH` | `/records/:id/ranked` | 参与 / 退出排行榜 |
| `PATCH` | `/records/:id/video` | 绑定 B 站视频号 |
| `GET` | `/me/records?ownerToken=` | 我的分享，上限 20 条 |
| `GET` | `/health` | 探活 |

榜单字段从 BEC 快照的 HEAD 层解析，**不采信 body 里的同名字段**，这样榜单上的数字与
点开详情后看到的必然一致。body 只提供快照推不出来的东西：关卡 id、昵称头像、视频号、
队伍摘要。注意快照用 fflate 的**裸 DEFLATE**（无 zlib 包头），服务端要用 `inflateRawSync`。

## 榜单规则与生命周期

- 按关卡分榜，两个维度（`dps` / `time`），各保留 20 名
- 单用户单关卡只占一个名次，取其最优成绩；同分先到先得
- 分享默认参加排行榜，可以在「我的分享」里退出——退出后链接仍然有效
- 每人最多 20 条分享，超了要先删

```
在榜        f = 0        永久保存
掉出榜单    f = 掉榜时刻  每天 04:30 由 combat-api 的定时触发器维护
超过 7 天   删除记录对象，并从该用户的分享列表里移除
```

宽限期从真正掉榜那天算起：已经掉榜数天的行不会被重新打时间戳；重新上榜则清零。
清理逻辑要用到存储布局，所以直接挂在 combat-api 的定时触发器上，
而不是再开一个函数把同一套布局抄一遍。
