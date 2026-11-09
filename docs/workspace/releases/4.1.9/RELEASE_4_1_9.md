# Better Endfield Next 4.1.9

Windows / Android 软件版本 4.1.9，Android versionCode 40109。
BEM Tools / DSH 仍为 1.6.7，本次没有功能改动。
资源包不重发：core 仍为 1.6.3.3 / 1.6.4.4。

本次改动全部在客户端界面。

## 创意工坊

- **入口从「跳浏览器」改成软件内页面。** 以前点侧栏的创意工坊会直接把默认浏览器打开到站点首页；现在先进入一个页面，上面并列「直连」和「域名」两个地址，各自可以打开或复制。
  - 直连走 `146.235.16.65:8443`，不经过 Cloudflare，连通性更好；域名走 Cloudflare，作为直连不通时的备用。
  - 两个地址都以文本形式摆着，断网时也能看到并复制，方便换设备或发给别人。
  - 两个入口指向同一个站点，作品与账号互通；登录状态各自独立，在哪个入口登录就只在该入口保持。
- Windows 与 Android 的改动一致。Android 页面顶部提供「返回」回到首页。

## 第三方模型

- **新增「前往下载」。** Windows 在模型管理页顶部按钮行、Android 在「导入 BEM / ZIP」下方，点击打开 Discord 社区。

## 其他

- 关于页的 Discord 邀请链接换成新地址。旧链接已失效。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.9-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.9-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.7-win-x64.zip | Windows 创作者工具 |
| BEM-Tools-1.6.7-android-arm64.tar.gz | Android CLI |
| dsh-bem-creator-1.6.7.tgz | DSH 插件 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发。
