# Better Endfield Next 4.1.8

Windows / Android 软件版本 4.1.8，Android versionCode 40108。
BEM Tools / DSH 升级到 1.6.7。
资源包不重发：core 仍为 1.6.3.3 / 1.6.4.4，preview 仍为 1.6.3.3 / 1.6.4.3。

## 第三方模型

- **模型包改成看得懂的文件名。** 以前一个包在 `models\` 里存成 `<package_id>.bem`，实际是 `bem.d57cbd944d5c49e2956869bd6a8d73fb.bem` 这样的哈希串，想分享给别人时无法从文件名判断哪个是哪个。现在按 `<包名> v<版本> [<id 末 8 位>].bem` 存放，例如 `提弗洛斯-校园死库水 v1.0 [6a8d73fb].bem`。尾缀用来和 `runtime.ini` 里的 `[Mod.…]` 段反查，也避免同名同版本的包互相覆盖。
  - 已经在 `models\` 里的包不需要手动处理：**打开一次模型管理页就会自动改名**。游戏正在运行时这一步会跳过，关闭游戏后再进一次页面即可补上。
  - 只改文件名仍是纯包 ID 的包；自己改过名的文件不会被覆盖，重复进入也不会再动。
  - 更新或重新导入同一个包时按包 ID 删除旧文件，不会留下同 ID 的两份。
- **阴影接管失败不再否决整个包。** 以前 Windows 世界模型的投影代理准备失败会把几何与材质替换一起回滚；现在只有构造事务本身失败才否决，代理准备失败改为保留游戏原投影、照常替换模型，并输出 `Windows shadow takeover skipped` 告警。
  - 同时放宽了代理归属判定。LOD1 投影网格的骨骼数可能比可见的 LOD0 接收器多（伊冯 `cloth_04` 26/24、提弗洛斯 `body_01` 39/35），以前这类包会被判成不属于同一套骨架而拒绝；现在允许任一侧是另一侧的切片。按 mesh 身份兜底时才要求骨骼长度、顺序与指针完全一致，并保留竞争代理扫描。
  - 已知限制：降级路径不写代理绑定，跨包热切换或关闭时可能沿用上一代的代理状态，属纯视觉、可逆。Android 的 `SP_Mobile` 归属失败仍然拒绝整包，未放宽。

## 创作者工具

BEM Tools / DSH 1.6.7：

- `BEM_V1_4_SPEC` 补充 Windows `SP_Desktop` 投影代理的归属规则：按资源内 canonical 路径 `Mesh_all/lodN/<stem>_lodN` → `Shadow_Proxy/SP_Desktop/<stem>_shadowProxyDesktop` 一对一建立，骨骼证据只用于证明两者共用同一套实时骨架，允许调色板互为切片。
- 规范明确阴影接管是可选附加项，以及拒绝时的降级行为与已知限制。bem-creator 技能内的规范副本同步更新。

## 分发

- 发布脚本的上传连接改为读 `HTTPS_PROXY`，不再写死某个本地代理端口；没有该变量时直连。
- 归档复用改为比对 GitHub 已存资产的 SHA-256，不再把已发布的归档下载回来逐字节比较。内容不同仍会要求升版本号。
- 资源发布新增可选的 `--prune-assets`：按 `(类型, 平台)` 只保留版本号最高的一个归档，其余从 release 删除并从索引剔除。被删的旧归档没有运行时消费者，留下的那份即留档。本次已执行，资源 release 从 1.33 GB 降到 647 MB。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.8-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.8-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.7-win-x64.zip | Windows 创作者工具 |
| BEM-Tools-1.6.7-android-arm64.tar.gz | Android CLI |
| dsh-bem-creator-1.6.7.tgz | DSH 插件 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发。
