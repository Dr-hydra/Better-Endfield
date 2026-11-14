# 创意工坊：双入口与主备方案

- 状态：**第 1–5 阶段已落地**（2026-11-09），仅剩故障演练未做
- 适用：`web/mod-center/`（社区资源站），线上部署在 `146.235.16.65:/opt/endfield-resource-center`
- 相关文档：[网页版后端架构](WEB_BACKEND_ARCHITECTURE.md)（那是游戏内寻访/排行榜，与本方案无关）

## 0. 摘要

| 目标 | 结论 |
|---|---|
| 入口 A：IP + 非标准端口 | 保留，`https://146.235.16.65:8443/endfield/` |
| 入口 B：域名 | 保留，`https://endfield.03250799.xyz/endfield/` |
| 双入口共用一份后端、一份前端产物 | 可以，但要先把 media URL 改成相对路径 |
| 主备（AMD 挂 → ARM 接管） | 只覆盖域名入口；IP 入口无法切换 |
| 双活（两边同时服务） | **不做**，SQLite 不支持跨机写入 |

## 1. 现状

### 1.1 服务与端口

| 进程 / 服务 | 监听 | 作用 |
|---|---|---|
| `endfield-resource-center` | `127.0.0.1:9017` | 创意工坊后端（node v24，`node:sqlite`） |
| `hydra-cloudflared` | 主动外连 | 隧道 `hydra-secondary-singapore` 的 connector |
| `fh6-heybox-tls` | `0.0.0.0:8443` | HTTPS 代理，为 IP 入口提供 TLS 与证书 |
| `fh6-heybox-acme` | `0.0.0.0:80` | ACME 证书挑战（IP 证书续期） |

### 1.2 两条访问链路

```
入口 A（IP 直连）
  https://146.235.16.65:8443/endfield/
    → fh6-heybox-tls（TLS 终止）
    → proxy-resource-center.mjs
    → 127.0.0.1:9017

入口 B（域名）
  https://endfield.03250799.xyz/endfield/
    → Cloudflare 边缘（橙云）
    → CNAME 10fc35e7-0dfa-47ba-a9be-f4f931f7c44d.cfargotunnel.com
    → 隧道 hydra-secondary-singapore
    → cloudflared on AMD
    → 127.0.0.1:9017
```

入口 B 的 tunnel ingress 指向 `http://localhost:9017`，**不经过 8443**，也不使用源站证书。

## 2. 双入口改造

### 2.1 冲突点

`server.mjs:63` 只读一个 `PUBLIC_URL`，它同时决定：路由前缀、`allowedOrigins`、**media 绝对地址**、OAuth 回调。

```js
// uploads.mjs view()
url: new URL(`media/${row.id}`, publicUrl).href   // 只能是某一个 origin
```

只要 media URL 是绝对地址，就必然有一个入口的图片指向另一个入口。

### 2.2 解法：media 改 site-root-relative

```js
// 改后
url: `${prefix}media/${row.id}`     // /endfield/media/<uuid>
```

以 `/` 开头的相对路径由浏览器按**当前页面 origin** 解析，两个入口自动各自正确，无需任何运行时判断。

### 2.3 改动清单

| # | 文件 | 改动 |
|---|---|---|
| 1 | `server/uploads.mjs` | `view()` 的 `url` 改为 `${prefix}media/${id}` |
| 2 | `server/server.mjs:169` | 内部 media 地址校验改比 `new URL(draft.url).pathname.startsWith(`${prefix}media/`)`（`link()` 已把输入规范化成绝对 URL） |
| 3 | 线上 env | `PUBLIC_URL=https://endfield.03250799.xyz/endfield/`、`ALLOWED_ORIGINS=https://146.235.16.65:8443` |
| 4 | `server/server.mjs` | 新增 `githubCallbackUrl(req)`：按请求的 `Host` + `X-Forwarded-Proto` 生成 `redirect_uri`，且仅当 origin 在 `allowedOrigins` 内才采用，否则回退 `PUBLIC_URL`；授权与换取 token 两处共用 |
| 5 | GitHub OAuth App | 已注册两条 Redirect URI（域名 + `IP:8443`，上限 10 条，不支持通配符） |

**不需要改动**：`vite.config.ts` 的 `base`（仍为 `/endfield/`）、`proxy-resource-center.mjs` 的路径判断、任何 301 重定向。两个入口共用同一前缀，站内链接天然同源。

### 2.4 PUBLIC_URL 的作用已经很小

media 相对化 + 回调地址动态化之后，`PUBLIC_URL` 只剩两个用处：`prefix`（路由前缀，两个入口必须一致）和 origin 不可信时的回退值。域名与 IP 入口在功能上完全对等。

### 2.5 双入口的登录态

cookie 是 host-only，`146.235.16.65:8443` 与 `endfield.03250799.xyz` 是两个 host，浏览器不会共享：

- 两个入口各自登录、各自独立，互不影响
- GitHub 登录在两个入口都可用：`redirect_uri` 按当前 origin 生成，`be_oauth` state cookie 写在同一 host 上，回调也在同一 host，链路闭合
- 唯一需要保证的是 GitHub 里注册了对应的 Redirect URI（当前已注册两条）

### 2.6 验证方法

1. 两个入口各打开一次首页，查看任意作品图片的 `src` 是否为 `/endfield/media/...`
2. 两个入口各做一次写操作（点赞或编辑资料），确认不被 Origin 校验拒绝
3. 两个入口各走一次 GitHub 登录，确认都能建立各自登录态

## 3. 主备

### 3.1 为什么不双活

SQLite 的一致性依赖进程内文件锁与 WAL 共享内存（`-wal` / `-shm`），假设同一文件系统、同一内核：

- 两台各自挂盘 → 两个分叉的库
- 放 NFS 共享 → **官方明确不支持**，WAL 需要真正的共享内存映射，表现是随机损坏而不是变慢

读写分离（写转发回主）技术上可行，但本站访问量是「一天几十次浏览」，改造换不来收益。

**结论：主备（单写），备机平时不接流量。**

### 3.2 备机形态（ARM，`ubuntu@134.185.93.243`）

ARM 本身是 docker 主机（`hydra-monitor-*`、`dfh-*` 共 8 个容器），备机按容器部署以保持
该机运维方式一致：

```
/opt/endfield/
  Dockerfile                     node:24-alpine + app/
  docker-compose.yml             app + tunnel
  .env                           从主站复制的环境变量（600 root）
  app/{server,dist,shared,node_modules/nodemailer}
  failover-key                   接管用的受限密钥
  endfield-failover.sh           接管
  endfield-standby-stop.sh       回切前停备机
/etc/endfield-cloudflared/token  hydra 隧道 token（600 root）
```

- 镜像 `endfield-resource-center:local`，在 ARM 本地构建（aarch64）
- 数据卷挂 `/srv/backup/endfield:/data`——**备份目录即运行数据**，切换时不必再复制一次
- `tunnel` 用 `network_mode: "service:app"` 共享网络命名空间，容器内的 `127.0.0.1:9017`
  与 Cloudflare ingress 指向的地址一致
- 平时停着（手动 `stop` + `restart: unless-stopped`，不会自行拉起）

**不需要**：8443 TLS 代理、IP 证书、ACME、`fh6-heybox` 相关——ingress 直连 9017。
**已验证**：以真实数据启动后 `/endfield/api/resources` 返回真实作品，`/endfield/media/<id>` 返回 200。

### 3.3 数据同步

每 5 分钟，单向 AMD → ARM，由 `endfield-sync.timer` 触发 `/usr/local/bin/endfield-sync.sh`：

| 项 | 取值 |
|---|---|
| 密钥 | `/root/.ssh/endfield-sync`（ed25519，专用，不复用 root 密钥） |
| 目标 | `endfield-sync@134.185.93.243`，被 `authorized_keys` 里的 `rrsync` 限制在 `/srv/backup/endfield` |
| 顺序 | 先 `uploads/`，后 `catalog.sqlite` |
| 删除保护 | `--backup --backup-dir=/srv/backup/endfield/trash/<date>`，保留 30 天 |

```bash
rsync -a --numeric-ids --delete \
      --backup --backup-dir="/srv/backup/endfield/trash/$STAMP" \
      -e "$SSH_CMD" "$SRC/uploads/" "$DEST:uploads/"

python3 - "$SRC/catalog.sqlite" "$SNAP" <<'PY'   # 服务器没有 sqlite3 CLI
import sqlite3, sys
src = sqlite3.connect(sys.argv[1]); dst = sqlite3.connect(sys.argv[2])
src.backup(dst); dst.close(); src.close()
PY
rsync -a -e "$SSH_CMD" "$SNAP" "$DEST:catalog.sqlite"
```

实测耗时：首次全量 1.4 GB / 140 文件 **28 秒**，无变更时增量 **1 秒**。

**顺序不能反**：先拍库后传文件，快照里会引用尚不存在的图片，切换后整页 404。反序最坏只产生无害的孤儿文件。

- `uploads/` 文件名是 UUID，只增不改，rsync 天然安全（先写临时文件再原子改名）
- `catalog.sqlite` 仅约 244 KB，`backup()` 毫秒级
- 服务器没有 `sqlite3` CLI，用 `python3` 内置 `sqlite3` 模块

同步间隔决定丢失窗口：最坏丢 5 分钟内上传的文件，但文件仍留在 AMD 磁盘上，实例可救回时能补。

### 3.4 切换（人工）

**在 ARM 上执行** `/opt/endfield/endfield-failover.sh`：

```
1. 探活 AMD 的 8443；若还在服务则提示并要求确认
2. 经受限密钥通知 AMD 执行 standby-on：
   停 hydra-cloudflared + endfield-sync.timer
3. docker compose up -d（app + tunnel）
4. 本地健康检查，再等 15 s 观察 Cloudflare 路由
```

**为什么必须停 AMD 的 timer**：不停的话 AMD 每 5 分钟把旧数据推过来，覆盖备机在故障期间
产生的写入。connector 同理——机器活着但后端死了时，它会持续转发到死后端，永远 502。

预期恢复 **1–2 分钟**（旧连接摘除 + 新连接注册）。

**探活不要用域名**：切换后两台都可能连着同一隧道，域名探活分不清是谁在应答。

### 3.5 回切（人工）

两步，分别在两台机器上执行：

```bash
# 1. 在 ARM 上：先断 tunnel，再停 app
/opt/endfield/endfield-standby-stop.sh

# 2. 在 AMD 上：拉回数据并恢复
/usr/local/bin/endfield-failback.sh
```

`endfield-failback.sh` 会：停本地服务 → `rsync` 从 ARM 拉回 `uploads/` → 拉回 `catalog.sqlite`
（并删除 `-wal`/`-shm`）→ `chown endfield-mods` → 启动服务、同步 timer、隧道 connector。

**必须做反向同步**，否则切换期间 ARM 上产生的作品和上传会在回切时被覆盖。
`rrsync` 不带 `-ro`，所以 AMD 用同一把同步密钥就能从 ARM **拉取**，不需要额外授权。

### 3.6 不能自动切换的理由

- 同一隧道多 connector 是**负载均衡**，不是主备。备机 connector 必须平时关闭，否则一半请求打到无服务的机器
- 误判会导致「两台都停」或「两台都在服务」的分裂状态
- 若引入 OCI API 自动转移 Reserved IP，等于把租户级凭据放在公网机器上

监控（ARM 上的 beszel，已在监控 AMD）只做**告警通知**，切换由人确认后执行。

## 4. 实施进度

| 阶段 | 内容 | 状态 |
|---|---|---|
| 1 | media 相对路径 + 内部地址校验 + OAuth 回调按入口生成 | ✅ 2026-11-09 |
| 2 | 部署后端并验证双入口（env 线上原本已是目标值，未改） | ✅ |
| 3 | ARM 备机就绪：node 24.22.2、镜像 `endfield-resource-center:local`、真实数据冒烟通过 | ✅ |
| 4 | 定时同步：`endfield-sync.timer` 每 5 分钟，首次全量 28 s | ✅ |
| 5 | 切换/回切脚本：`endfield-failover.sh`、`endfield-standby-stop.sh`、`endfield-failback.sh`，中止路径已验证 | ✅ |
| 6 | **演练**：真停 AMD 服务，实测恢复耗时 | ⬜ 待排期（需低峰时段） |

## 5. 风险与回滚

| 风险 | 缓解 |
|---|---|
| 改 media 相对路径后老页面图片失效 | 路径前缀未变，只是从绝对变相对，同源下等价 |
| 改 `PUBLIC_URL` 后现有登录态失效 | cookie 是 host-only，用户重新登录一次 |
| 双入口写操作被 Origin 拒绝 | `ALLOWED_ORIGINS` 必须包含 IP 的 origin |
| 部署后服务起不来 | `/root/endfield-before-deploy-<stamp>.tar.gz` 回滚 + restart |
| 切换时数据落后 | 切换前尽量做一次手动同步 |

## 6. 已定事项

2026-11-09 确认：

1. **同步使用独立专用密钥**（不复用 root 密钥），在 ARM 侧用受限 `authorized_keys` 条目
   （`no-pty,no-port-forwarding,no-agent-forwarding`）限制用途
2. **IP 入口不换**：保持 `https://146.235.16.65:8443/endfield/` 现状，不做 Reserved IP
   自动转移。故障时 IP 入口不可用，用户改用域名兜底
3. **保留被删文件的副本**：`rsync` 加 `--backup --backup-dir`，保留 30 天。
   ARM 磁盘 96 GB 当前仅用 6.7 GB，空间不构成约束

## 7. 唯一待确认

- ARM 上是否已装 node（上次连接被中断，未核实）。未装则需装 24.x，后端依赖 `node:sqlite`（≥22.5）。
