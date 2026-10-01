# 双端赞助入口（2026-10-01）

分支：`dev/android-main-bem-import-20260927`。

- 安卓：标题栏右侧「赞助」，使用杏橙底色 `#FFCFA3`、深棕文字 `#4A2B18`。标题在窄栏自动调整大小。点击展示渠道选择，微信码独立展示并可保存到相册 `Pictures/BetterEndfield`，供微信从相册识别；保存使用 Android 10+ MediaStore，无需新增存储权限。
- PC：左侧底部「给作者充一点token」，位于「关于／设置」上方。提供浅色、深色和高对比主题配色，中英文标签及收起侧栏提示。点击打开可关闭、可滚动的赞助面板，保留当前页面选中状态；微信码支持另存为 PNG。
- 爱发电：`https://afdian.com/u/e9a7e6ac6fa411ed980752540025c377`，从用户提供的二维码识别并移除推广参数。
- PayPal：`https://paypal.me/hydra405`，由用户提供。
- 微信原始赞赏图片分别保存于安卓 `res/drawable-nodpi/sponsor_wechat.png`、PC `Assets/sponsor/wechat-appreciation.png`，完整复制原图。PC 图片作为嵌入资源加载，兼容单文件发布，不依赖临时附件目录。

链接通过系统浏览器或关联应用打开；不增加应用内支付、赞助记录或自动提示。

验证：安卓 Release / Lint / APK 签名通过；PC Release 编译通过，零警告、零错误。本机 PC SDK 为 9.0.308，构建从仓库外调用，未修改仓库固定的 SDK 配置。安卓 APK 已覆盖安装到用户手机并保留数据。外部页面视觉、保存图片和付款流程由用户验收，未操作手机页面或发起支付。

APK：`artifacts/BetterEndfield-Android-3.4.0-20261001-sponsor.apk`。版本仍为 3.4.0 / 30400。
