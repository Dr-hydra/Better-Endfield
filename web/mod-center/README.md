# 终末地下载站 / Better Endfield Downloads

独立部署的终末地社区下载站，提供中英双语界面，沿用仓库 Web 的 Preact、米白/深色主题、侧栏和黄色强调色。与 Toy 和战斗统计业务独立。

作者可以使用 GitHub 或邮箱验证码登录，自助提交外部下载地址。必填项只有名字、类型、链接。版本名字、封面链接、介绍和更新说明均可选；不接收资源包上传，不设置发布审核，不按地区屏蔽访问。

## 感谢名单

感谢名单的维护表在 `supporters.json`，构建脚本会生成只包含排序后姓名的 `src/supporters.generated.ts`；金额、日期和来源不会进入浏览器资源。每条记录可以填写姓名、金额、日期和来源；页面会合并同名记录后按累计金额排序，只显示姓名，不显示金额或内部排序。没有可识别姓名的记录把 `name` 留空，页面会显示为“匿名 / Anonymous”。增量更新时追加记录并更新表格顶部的 `cutoffDate`，然后运行 `npm run mods:build` 即可。

## 本地开发

需要 Node.js 24（后端使用内置 `node:sqlite`）。在 `web` 目录执行：

```powershell
npm ci
npm run mods:build
npm run mods:test
npm run mods:serve
```

访问 `http://127.0.0.1:9017/endfield/`。本地未配置 GitHub 时只开放浏览与表单预览，不开放匿名发布。表单草稿只保存在当前浏览器。

热更新开发可另开终端运行 `npm run mods:dev`，后端的 `PUBLIC_URL` 应设为 `http://127.0.0.1:5174/endfield/`，然后访问该地址。

## 数据与版本

- SQLite 保存用户、会话、作品和发布记录。GitHub 用户数字 ID 绑定站内账号，改昵称不会改变归属。
- 自助发布必填名字、枚举类型和 HTTP(S) 下载链接。纯文本正文保留换行；页面不执行作者 HTML。
- 历史版本按发布顺序排列，版本名字可以留空；填写的版本名字在同一作品内不能重复。
- 更新下载链接或版本名字时新增发布记录；修改作品介绍不会新增记录。外部文件内容由作者托管，站点只记录链接。
- 写入验证登录、作品归属、Origin 和 CSRF；作者写入限制为每小时 60 次。GitHub 授权包含浏览器绑定的单次 state 和 PKCE；授权 token 不存入数据库。

## 当前服务器部署

公开地址：`https://146.235.16.65:8443/endfield/`。

服务：`endfield-resource-center.service`；代码目录：`/opt/endfield-resource-center`；数据库：`/var/lib/endfield-resource-center/catalog.sqlite`；环境配置：`/etc/endfield-resource-center.env`。

服务仅监听 `127.0.0.1:9017`，由现有 HTTPS 8443 入口转发 `/endfield` 和 `/endfield/`。现有 80、443 与其他 HTTPS 业务继续使用原有路由。使用现有 IP 证书和自动续期任务。

## 开通 GitHub 登录

在 GitHub Settings → Developer settings → OAuth Apps → New OAuth App 创建应用：

- Application name：`Endfield Downloads`（可以自行改名）。
- Homepage URL：`https://146.235.16.65:8443/endfield/`。
- Authorization callback URL：`https://146.235.16.65:8443/endfield/auth/github/callback`。
- 使用精确回调匹配，关闭 wildcard matching。不需要仓库访问权限或 Device Flow。

在服务器编辑配置，填入 Client ID 和 Client Secret：

```sh
sudoedit /etc/endfield-resource-center.env
sudo systemctl restart endfield-resource-center
```

不要把 Client Secret 放到前端、Git 或聊天记录。凭据未填时，服务仍可浏览，真实 GitHub 登录和发布保持关闭。

## 开通邮箱登录

邮箱登录不保存用户密码，用户提交邮箱后收到六位验证码，10 分钟有效，只能使用一次。验证码绑定申请时的浏览器，最多允许五次验证尝试；每个邮箱每 15 分钟最多发送三次，每个 IP 每 15 分钟五次，网站每天最多发送 100 封。

已有 GitHub 作者可以在「我的发布」中验证并绑定邮箱，之后通过邮箱登录仍管理原来的作品。直接通过未绑定的邮箱登录会建立新账号，不根据第三方邮箱自动合并账号；公共作者资料不展示邮箱。

邮件通过 SMTP 发送。起步可准备专用 QQ 邮箱，在邮箱设置中开启 SMTP 并生成授权码；无需自建邮件服务器或购买域名。使用 OCI Email Delivery 等域名邮件服务时，按服务商要求配置发件域名和 SPF/DKIM。

在服务器编辑 `/etc/endfield-resource-center.env`，填写 SMTP 配置：

```sh
sudoedit /etc/endfield-resource-center.env
sudo systemctl restart endfield-resource-center
```

填写 `SMTP_HOST`、`SMTP_PORT`、`SMTP_USER`、`SMTP_PASS` 和 `SMTP_FROM`。QQ 邮箱示例：

```dotenv
SMTP_HOST=smtp.qq.com
SMTP_PORT=465
SMTP_USER=专用发信邮箱@qq.com
SMTP_PASS=QQ邮箱生成的SMTP授权码
SMTP_FROM=专用发信邮箱@qq.com
```

配置完成后，登录窗口会自动显示邮箱入口；未配置 SMTP 时不会显示邮箱登录。建议使用专用发件地址和应用专用密码，不要把邮箱主密码提交到仓库或聊天中。

部署包需包含构建后的 `dist`、`server`、`shared`、`deploy`、`README.md` 和 `web/node_modules/nodemailer`（放在部署包的 `node_modules/nodemailer`）。安装脚本会同时复制邮件运行依赖。

## 备份与运维

用 SQLite 的备份 API 创建一致快照，不在服务运行时单独复制 WAL 模式的 `.sqlite` 文件。升级只替换代码与静态构建，不覆盖数据库和环境配置。

```sh
sudo systemctl status endfield-resource-center --no-pager
sudo journalctl -u endfield-resource-center -n 30 --no-pager
```

域名迁移后，更新 `PUBLIC_URL`、GitHub 首页/回调 URL 与 TLS 入口，再重启服务。
