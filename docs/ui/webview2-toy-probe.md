# WebView2 → Toy 技术验证（2026-09-14）

预览：https://www.bilibili.com/toy/preview/preview_yBlekLfW/index.html

## 当前结果

- Toy 前端构建和包体预检通过，已部署预览，未提交审核。
- 独立 WinUI WebView2 验证窗口构建成功，使用共享 Evergreen Runtime。
- 接收器测试通过：8 MiB 有序分块传输、乱序拒绝及状态清理。
- 用户已手动运行并确认验证成功，实际 Toy 内页 8 MiB 消息传输通过。用户确认原有登录与云存档链路已接通。
- 战斗与寻访按钮已接入独立 `ToyAnalysisWindow`，正式地址加载后自动传输并解析。共享运行时、持久化登录目录、逐块 ACK、关闭取消及主窗口退出时清理已接入。

## 手动运行

在仓库根目录 PowerShell 执行：

```powershell
& '.\ui\BetterEndfield.UI\bin\x64\Release\net9.0-windows10.0.19041.0\win-x64\BetterEndfield.exe' --webview-probe 'https://www.bilibili.com/toy/preview/preview_yBlekLfW/index.html'
```

如果预览要求创作者登录，先在这个独立窗口登录。WebView2 使用独立持久化目录 `%LOCALAPPDATA%\BetterEndfield\WebViewProbe`。

预期：Toy 页面出现“桌面直连验证成功：已接收 8 MiB，数据未上传”，窗口日志出现 `ack` / `step:end`。日志在上述 exe 同目录的 `webview-probe.log`，不输出传输正文或登录凭证。

验证窗口仅发送合成测试字符串。测试通道不会自动写入云存档。正式 combat 通道进入现有 Worker 解析器；gacha 通道接收现有桌面快照格式（并非原始寻访 API 记录），进入现有展示与登录后合并逻辑。

## 协议 v1

通过 `window.chrome.webview` 接收原生消息，不注册普通跨窗口 message，不请求 localhost。

- 公共字段：`protocol: better-endfield-desktop`、`version: 1`。
- 前端初始化发送 `type: ready`。
- 原生端发送 `begin`，包含 `id`、`kind: combat|gacha|probe`、`size`（UTF-8 字节数，最多 64 MiB）。
- 后续发送 `chunk`，包含同一 `id`、从 0 递增的 `index`、`text`（最多 262144 UTF-16 码元）。分块应避免切断代理对。
- 最后发送 `end`。接收完整后交给解析入口。
- 每步返回 `ack` 或 `error`。end ACK 表示已交付解析入口，不等于异步战斗解析成功。

## 正式接入前仍需完成

正式 Toy 版本需要发布本次前端接收器，才能与按钮的正式 URL 配套使用。传输完成后，业务解析错误由既有网页界面展示。后续可增加前端闲置传输超时释放。
