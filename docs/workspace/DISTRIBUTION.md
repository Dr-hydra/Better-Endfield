# 统一分发与资源更新

## 分发入口

仓库仍为 Dr-hydra/Better-Endfield。软件按正常 v4.x.x Release 发布；资源集中在固定的 game-resources Release，make_latest=false。旧版 Windows 的 releases/latest、旧版 Android 的 APK Release 筛选继续有效。

唯一的新入口是 game-resources 下的 distribution-index.json，schemaVersion=1，kind=betterendfield-distribution-index：

- software：软件 version、releaseUrl、assets（name/url/bytes）。
- tools：bem、dsh 各自的 version 和 assets，不把工具版本冒充软件版本。
- resources：packageType、platform、四位 version、三位 gameVersion、resourceApi、asset、url、bytes、expandedBytes、optional。
- generatedAtUtc 只表示索引生成时间，不用于比较版本。

客户端仅接受本仓库固定资源 Release 的下载地址。索引 404 时，软件检查回到原 Releases API；资源更新保留原资源并报错。网络错误不改变已启用版本。

## 四类资源

| 类型 | 平台 | 本次版本 | 来源 |
| --- | --- | --- | --- |
| core | windows | 1.6.3.2 | G 盘游戏的当前有效 VFS，基础战斗/图标/姿态维护输入 |
| core | android | 1.6.4.3 | ADB 读取手机当前 VFS、AudioDialog、PrefabInfo 和四语言音频索引 |
| preview | windows | 1.6.3.2 | 一次提取当前 Windows 全角色几何，保留原 BC7 mip，最高 2048 |
| preview | android | 1.6.4.2 | 同一套 Windows 几何，开发机离线转码 ASTC 6×6，最高 2048 |

预览源只提取一份，不重复提取/比对 Android 角色模型。两份预览包的前三位分别属于各平台目标游戏基线；source 另外记录 Windows 来源版本、manifest 身份、角色数和纹理编码方式。桌面和手机不需要安装 Python、Pillow、astcenc 或新的客户端运行时。

当前覆盖 37 个可玩角色/模型变体，包括没有独立 UI prefab 的明河三个变体；按当前 manifest 中实际存在的 world/UI 资源生成。导出完整最高细节层角色几何和主要色贴，不限于头部。材质、骨骼和 prefab 身份保留；运行时继续按 keep/hide/replace 补齐，缺少/不匹配的原生资源保持降级。

本次生成和清单检查不等于所有角色均已在设备上验收。特殊游戏 shader、动画、灯光和非底色纹理不属于静态预览包。

## 自动更新、导入和回退

- 软件与资源共用启动检查开关。软件提示下载正式安装包/APK；资源自动下载、解压、校验后切换目录。
- core 默认参与。preview 第一次由用户点击下载，安装后才参与自动更新。
- Android 按已安装游戏包的 versionName 选择相同三位基线；Windows 当前按已启用基础包的基线选择，不猜测跨游戏版本兼容性。Windows 跨基线通过新软件携带的基础包或手动导入切换。
- 同基线按四位数值比较。历史版本保留；手动导入旧 ZIP 即可回退。手动导入会写 auto.<类型>.<平台>=false，固定当前版本，防止下次启动又升回去；“恢复资源自动更新”取消固定。
- state.json 的 <类型>.<平台> 仍存启用版本；没有对应 auto 标记的既有显式选择按固定版处理，兼容旧本地导入结果。
- 取消、下载中断、错误平台、错误基线、ZIP 路径越界、漏文件、未列出文件和长度不符不切换启用状态。相同版本不同内容拒绝覆盖。
- 下载临时文件使用随机 .part，解压至 staging，验证完成后迁移不可变目录，最后替换状态文件。失败清理本次临时目录；不自动删除旧版本。
- Windows 基础数据需要重启程序及游戏模块。Android 首次后台取得基础包会重建管理界面；后续更新按界面提示重启应用/游戏。已打开的预览持有原快照目录，不删除正在使用的资源。

手动入口：双端“关于”页面均有资源更新、可选预览下载、ZIP 导入/回退、恢复自动更新和取消下载。Android 使用系统文件选择器，不开放任意共享目录权限。

ZIP 根包含 manifest.json 和 payload。Windows 也可解压到 resources/<类型>/windows/<版本>/；Android 使用导入入口。不要解压覆盖不同版本目录。

## 可复现生成

1. 用 RefreshEndfieldResourceInputs.py 明确传入 G 盘 --game-path 和新的 --output，得到当前 Windows manifest / PrefabInfo / AudioDialog。
2. 用 ExportDeviceResources.py --serial <设备> --output <新目录> 重新取得 Android 输入。只读取当前 BLC 引用的 CHK；音频仅保留 PCK header 和 Bank 数据作为忽略的生成输入，不复制 stream 音频，不随资源包分发音频。
3. 复用 ScanCharacterAssets.py、GenerateModCharacterPresets.py、GenerateResourceManifests.py、GenerateVoiceCatalogIndex.py 生成平台基础输入。版本来自实际来源，第四位在 config/resource-packs.json 维护。
4. 用 BuildPreviewResources.py --game-path <G盘游戏> --manifest <当前Windows manifest> --prefabs <当前PrefabInfo目录> --output <新目录> --astcenc <开发机编码器> 生成全角色。默认 scope=character；--character 仅供诊断。输出逐角色日志、可恢复缓存、report.json 和 BC7/ASTC 两份归档。
5. 用 BuildResourcePacks.py --type preview --platform windows/android --preview-report <report.json> 封装。存在任何失败或角色遗漏则拒绝打包；基础包继续使用 --type core。
6. PublishResourceRelease.py --archives <四类ZIP目录> --output <目录>/distribution-index.json 默认只校验并生成索引，显式 --publish 才上传。

发布先完成不可变 ZIP 上传及字节数验证，再替换索引，最后公开初始草稿。已有版本按逐字节比较确认，不用同名覆盖不同内容。后续软件 PublishRelease.py 发布成功后自动刷新索引中的软件/工具版本，保留资源历史。

软件发布必须走 PublishRelease.py：只有它的 --publish 分支会在公开 release 之后刷新索引（--refresh-software，旧索引保留为 distribution-index.previous.json）。用 gh release create 或网页直接发布会跳过这一步，索引仍停在上一版，已装客户端读 software.version 后判定为最新，收不到更新提示。发完用 CheckDistributionIndex.py 自检，它比对最新稳定 release 与已发布索引的 software.version、releaseUrl、资产名与字节数、tools 下载地址，不一致时退出码为 1。索引过期时单独补跑：PublishResourceRelease.py --refresh-software --publish --output <目录>/distribution-index.json。

默认不计算产物哈希。已有音频来源协议要求的输入 PCK 完整性字段继续保留；它们不属于分发 ZIP 的额外产物哈希。源码、生成输入、资源版本与设备实测边界分别记录。
