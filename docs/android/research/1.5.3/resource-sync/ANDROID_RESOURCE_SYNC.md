# Android 资源同步与 GitHub issue 核查（2026-10-01）

## 最终四语言交付

**最新韩语包已从指定手机定点读取并验证，最终索引为 136 目录、四语言当前 Android 来源。** 所有此前 135 目录及 54,172 对路由均保留，新增噗切娜韩语；不存在语言功能减少。噗切娜四语言目录齐全，每种语言映射 638 条短语音，全局路由各 6,859 对。此结果取代此前缺韩语 payload 的 135 目录保留式方案。

| 文件 | 当前用途 |
|---|---|
| `android/resources/character-names.json` | 35 个名称，包含噗切娜，复用主分支合并的通用角色名称 |
| `android/resources/character-presets.json` | 当前 Android manifestHash `664d17e6f5f4fd44513249dbeabf4c53`；32 模型 / 4,210 动作，模型/动作哈希未变 |
| `android/resources/voice-catalog-index.json` | schema 1；136 目录、4 个真实 Android stream package；所有目录标记 current Android BNK/media-index provenance，未再合并旧韩语描述 |
| `android/resources/manifests/model/action-manifest.json` | 独立 Android 模型/动作来源 |
| `android/resources/manifests/voice/voice-event-media-manifest.json` | 四语言 Android BNK/HIRC/PCK 映射与 snapshot SHA-256 |
| `android/resources/manifests/shared/resource-manifest-report.md` | 四语言生成统计 |
| `android/resources/resource-source.json` | 当前平台、设备、输入/输出哈希、12 个来源包、韩语范围读取证据，缺失语言/新角色目录均为空 |
| `android/resources/voice-index-android-evidence.json` | 最终四语言静态验证与 135 基线无回退证据 |
| `android/resources/voice-index-merge-evidence.json` | 历史 135 阶段保留旧韩语的证据，当前索引不使用该基线，保留用于追溯 |
| `scripts/GenerateResourceManifests.py` | Android snapshot 路径；PCK 完整语言名；SHA/大小/路径校验；验证选择性 BNK 范围与 header 的精确对应关系，不要求搬整个 banks 包 |
| `scripts/GenerateVoiceCatalogIndex.py` | schema 1、来源标记、冲突拒绝；历史缺源情况下可通过 legacy-index/evidence 增量保留；默认 PC schema 2 不变 |

资源已落盘，父代理 Release 代码编译完成后需用新资源重新 packaging。本子任务没有构建/安装 APK 或进行语音播放验收。

## 固定设备与来源目录

所有设备调用使用 `F:\Better Endfield\tools\android-toolchain\sdk\platform-tools\adb.exe -s 192.168.31.12:36477`，未使用默认设备或同台 mDNS 重复项。只执行只读 shell、exec-out su cat/dd/stat/sha256sum，不安装、不卸载、不 push、不修改游戏文件/凭据。

- Xiaomi `25102RKBEC`；Android 17 / SDK 37；root 可读。
- 官服 `com.hypergryph.endfield`，versionName 1.5.3 / versionCode 50。
- 游戏数据 `/data/user/0/com.hypergryph.endfield`；实际资源 `/sdcard/Android/data/com.hypergryph.endfield/files/VFS`。
- 本轮日期来源 `android/resources/.sources/20261001-wireless-192.168.31.12-36477`；韩语新增来源 `android/resources/.sources/20261001-wireless-192.168.31.12-36477/korean-20261001-022224`。旧快照和 135 阶段候选、来源/报告均保留；原始输入由 `.sources/.gitignore` 排除，只提交可审查资源与来源摘要。

第一阶段定点读取 7 个已知 BLC，再依据记录取 manifest、AudioDialog、34 个 PrefabInfo 范围，中英日 banks 和头部，约 74,574,815 bytes（71.12 MiB）。没有提取 APK、完整游戏目录、音频 stream payload 或动画 bundles。动画 LoopTime/时长复用旧 Android `walk-clip-metadata.json` 缓存；通用头像使用主分支已合并资产。

当前 Android snapshot 为 346,314 assets / 253,902 bundles / 30,180 行 AudioDialog / 33 个有效 PrefabInfo。AudioDialog 相对旧缓存新增 189 行，旧 29,991 行全部未变、未删除，且当前表内容与 `main@2c0cc12c` PC 表相同；模型 hash 仍严格使用 Android 来源。噗切娜 PrefabInfo 没有有效 correspondingCharId，转换器排除该记录，祀仍缺完整 model/sit chain，因此短语音更新不代表新增替换模型。

## 本次韩语增量读取

用户下载韩语包后，以下三个 CHK 均存在，设备 stat 大小与当前 BLC 相符。重新保存韩语 BLC，SHA-256 为 `2F5E8A3D01E2CF9933C1EA5F04F4912F52CD1BE6047CAB86530FFC05A67E5BA3`，与此前索引相同；资源输入完成后七个设备 BLC SHA-256 再核对一致。

| 韩语文件（E9D31017 分区） | 设备完整大小 | 本次读取 |
|---|---:|---:|
| `A5CDA735E6E6081BD8BCB708EC1CB609.chk` banks | 1,813,710 | 25,332 字节 header + 638 个匹配 Event 的 BNK 范围 |
| `6C17D7CA48BD06744AD80660A538154D.chk` stream | 1,520,788,196 | 745,876 字节 header |
| `0E8CEA3312A5E5BC58750E8F44E93C48.chk` external source | 10,544,600 | 3,968 字节 header |

仅选取当前 AudioDialog/voice manifest 996 个 Event ID 与 header 1,263 个 bank entry 的交集，得到 638 个必要 BNK，原始范围共 **1,057,552 字节**。以成批只读 dd 输出这些精确 offset/size，保存拼接文件 `korean-selected-banks.bin` 及每个 fileId、设备 offset、payload offset、size、SHA-256。没有读取其余 625 个未命名事件 BNK。

本次韩语总读取 **1,833,347 bytes（约 1.75 MiB）**，包含 BLC/头部/12-byte 前缀，不含少量 stat/校验和文本。**没有读取 1.52 GB stream payload，也没有搬完整韩语 banks 包。**

生成器校验每个选取范围与 PCK header 的 fileId/offset/size 对应、拼接范围连续且无重复、原始 SHA-256、请求 Event 交集完整覆盖，再按既有算法解码 HIRC Sound slot。选择性 payload 明确标记在 bank descriptor 中，declaredBankCount=1,263 / readBankCount=638。

## 四语言验证结果

中英日继续使用本日已直接读取的 Android banks/stream 头部；重新核对当前设备 BLC 哈希，来源记录未变化。韩语现在有直接 BNK/header 证据，不再仅依赖 BLC 声明或旧缓存。

- 136 = 4 ×（33 个有短语音的角色 + 默认规则）。四语言 mappedVoiceCount 均为 638，缺目标 Media=0、invalid BNK=0。
- 当前具名语音 996；每语言其余 358 条无匹配短语音 BNK，保持未映射，不猜测。
- Chinese / English / Japanese 目标 Media 数为 1,715 / 1,714 / 1,715；Korean 为 1,715，全部存在于对应设备 stream 头部。
- 四语言全局各 6,859 route pairs，所有 schema1 source Media ID 唯一且 uint32 合法；所有此前 135 目录及路由均是新表子集。
- `nativeMediaRoute` 有 638 个完整四语言 HIRC routing graph 验证通过；仍不是实际语音播放验收。
- 当前索引所有目录 provenance 为 `current-Android-BNK-and-media-index`，顶层为 `validated-Android-device-manifest`；全部 package 均为真实 Android 设备路径及完整 stat 大小，旧 PC 韩语分区提示不再进入当前表。
- 选择性 BNK 范围被篡改时生成器拒绝；脚本语法、默认 PC schema2 JSON 等价、输出哈希、PC manifests 与 `2c0cc12c` 等价、`git diff --check` 均通过。

索引/manifest/source 的 SHA-256：

| 文件 | SHA-256 |
|---|---|
| `android/resources/character-presets.json` | `3CD48F58425C31AD59A936CB8D03CBFB07A6E0573F9D4FBE70B170FE5E1F3AC0` |
| `android/resources/voice-catalog-index.json` | `5F5D1609DF7A0D2306F459676DA3EDDA536B6682514AA06C58979F6A1F35EBE6` |
| `android/resources/manifests/voice/voice-event-media-manifest.json` | `170E152D2C5188B0F19CB3A7D186A7EBEDEF50E3E6FD1B8A043445AE8D5BF263` |
| `android/resources/resource-source.json` | `020FB1FC29ED04433052BF7102D8104CBC79606F8EF40F3766949875A7D4FFF5` |
| `android/resources/voice-index-android-evidence.json` | `8061A128F1B09B4EC51189D373327CAAF4F7CF1B27D9CA93520641CEF660F013` |

## 复现生成与历史基线

当前四语言 PCK snapshot：`android/resources/.sources/20261001-wireless-192.168.31.12-36477/pck-snapshot-four-languages-korean-20261001-022224.json`，SHA-256 `9937FACD93CF779BAED9382A552934CADE42F176E82069CA5DDBD67678D3A6F7`。其中三个韩语 header 和选择性 bank payload 文件均位于独立新增目录，旧中英日路径仍引用同日来源，未覆盖旧 snapshot。

```powershell
$r = 'android/resources/.sources/20261001-wireless-192.168.31.12-36477'
$ko = 'android/resources/.sources/20261001-wireless-192.168.31.12-36477/korean-20261001-022224'
$four = 'android/resources/.sources/20261001-wireless-192.168.31.12-36477/pck-snapshot-four-languages-korean-20261001-022224.json'
python -B scripts/GenerateResourceManifests.py --actions "$r/generated/character-presets.json" --audio-dialog "$r/current-inputs/Table/AudioDialog.json" --input-snapshot "$r/current-inputs/input-snapshot.json" --bnk-dir "$r/no-offline-bnk-cache" --pck-snapshot $four --no-pck-discovery --output-dir "$ko/generated/manifests"
python -B scripts/GenerateVoiceCatalogIndex.py --manifest "$ko/generated/manifests/voice/voice-event-media-manifest.json" --schema-version 1 --output "$ko/generated/voice-catalog-index.json"
```

当前流程不传 legacy-index，不需以旧韩语条目补齐。此前 102 候选减少语言的处理已撤回，随后 135 阶段完整保留 Git HEAD 132 基线（53,272 对旧路由）并新增噗切娜中英日，冲突数为 0；本轮再核验 135 阶段 54,172 对全部保留并升为 136。历史 `voice-index-merge-evidence.json` 仅作审计，不是当前来源。父代理 optional exports + named fallback 已实现，下方旧 runtime 源码观察已标为历史。

## GitHub 最新交流和“导出”的含义

2026-10-01 成功读取 GitHub 网页的嵌入 JSON；API 已知 403 限速，不依赖 API。短暂 TLS EOF 后，使用 `curl.exe --tlsv1.2 --tls-max 1.2 -L` 恢复读取。#18 的完整前向 timeline 含 14 个事件，#19 含 2 个事件，均为 `hasNextPage=false`。

- [#18：恶性BUG:大量功能失效!](https://github.com/Dr-hydra/Better-Endfield/issues/18) 当前 OPEN，最后更新时间 `2026-09-27T15:47:16Z`；最后一条可见评论为 `2026-09-27T15:42:42Z`。
- [#19：Android (LSPosed) 移植分支](https://github.com/Dr-hydra/Better-Endfield/issues/19) 当前 CLOSED，最后更新时间 `2026-09-27T16:13:47Z`；维护者最后可见评论为 `2026-09-27T13:34:56Z`，称修复分支开发中、BEM1.2 兼容性可能有问题，尚未发布。
- open issues 列表另列出 [#13](https://github.com/Dr-hydra/Better-Endfield/issues/13)，内容是 PC 战斗历史批量选择/清除建议，不构成 Android 资源或符号导出证据。

这里的“导出”已确认指 **`libil2cpp.so` 的 IL2CPP 动态符号导出及符号解析结果**，不是资源导出、角色 manifest 或机型资源哈希。

### 机型和已知环境

| 样本 | 公开明确的环境 | 公开结果与限制 |
|---|---|---|
| realme RMX5200 | ColorOS V16.1.0；Android 16 / SDK 36；arm64-v8a；官服 1.5.3；KernelSU `4.1.0-1210-g933abca5`；官方 GKI `6.12.38-android16`；SELinux Enforcing；Zygisk Next `1.5.0 (843)`；LSPosed `v2.2.0 (7854)`，libxposed API 102 | #18 作者报告：原 `e547bae` 分支/群内 bem-release 包停在 `waiting_il2cpp`，120 秒后超时；自编译符号回退产物四模块 active，功能恢复。BEM1.2 导入当时未测试 |
| 维护者的小米多机样本 | 维护者明确表示全部为小米、同份代码、官服 1.5.3；未给具体型号、OS、Root、LSPosed 版本 | 同分支正常运行；未提供成功机的动态符号表，不足以断定“只有某厂商导出该符号” |
| 一加 Ace 3 Pro | Sukisu `40796`；LSPosed-It `7872`；#19 未明确 OS/SDK、游戏版本、完整二进制身份 | 作者报告自己的移植分支真机可用、CI 通过，HUD/自由视角/第一人称/冻结/持续冲刺有效；并未报告 `il2cpp_domain_get_assemblies` 导出是否存在 |

realme 完整环境来自 [#18 最后评论](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5857333703)；小米样本和同代码/同官服版本说明来自 [维护者回复](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5857307831)；一加信息来自 #19 正文。

### 符号证据、早期假设和后续结论

1. [realme 分支验收报告](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5856818363) 声称逐符号记录中 `il2cpp_domain_get_assemblies` 缺失，其余所需符号可解析；失败日志为 `IL2CPP exports/domain/loaded images did not become ready`。作者报告将程序集枚举符号改为可选、使用已存在的按名查询路径后，四模块全部启动。该结论是 issue 作者真机观察，本轮未取得其 `libil2cpp.so`，没有独立复核 ELF 导出表。
2. [A/B 补充](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5857238678) 明确表示清缓存、卸载重装后原包仍失败；自编译产物日志为 `il2cpp_domain_get_assemblies unavailable; using domain_assembly_open fallback`，并附两张截图。因此“只是旧配置未清理”尚未得到这个失败样本支持。
3. [维护者同代码/版本回复](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5857307831) 随后确认同一份代码、均为官服 1.5.3，要求 Root 和 LSP 信息。评论中的“渠道或源码不同”仍是待检验假设，不能写成已确定根因；相同版本名也不能证明游戏 SO/APK 字节相同。
4. 更早的 [09-26 取证](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5841916743) 提出域锁、程序集惰性加载、串行模块启动及丢日志等假设。后续 [桥接补丁报告](https://github.com/Dr-hydra/Better-Endfield/issues/18#issuecomment-5844178429) 将按钮故障定位到模块 ClassLoader 的 JNI 解析错误，并报告静态字段读取和冻结解除问题。不要把早期域锁假设、后续 JNI 问题与 09-27 的必需导出符号门控混成一个已经证实的故障。
5. #19 正文另报告 JNI 双 ClassLoader、枚举装箱失败、共享 `ok` 标志等问题及该作者分支的实测结果。它是独立设备/分支证据，不证明小米与 realme 的符号表差异，也不证明所有 Android 设备均适用。

参考分支：[yang-34 符号回退分支](https://github.com/yang-34/Better-Endfield/commits/fix/android-rebuild-il2cpp-fallback)、[NukumizuKazuhiko 移植分支](https://github.com/NukumizuKazuhiko/Better-Endfield/tree/fix/android-camera-compat)。仅读取公开报告，没有应用附件补丁或修改 runtime。

### 本地源码历史观察与父代理修复注记

以下记录的是前一阶段读取源码时的历史状态，行号可能随父代理修改而变化；与 issue 当时的 `e547bae` 产物状态应分开看。**2026-10-01 父代理已明确反馈：`il2cpp_domain_get_assemblies` / `il2cpp_image_get_name` 可选化，以及 `il2cpp_domain_assembly_open` 按名查询回退均已实现。下述旧观察不代表当前仍缺少 fallback。** 本子任务没有修改 runtime，也未独立执行该修复的真机验收。

- `android/app/src/main/cpp/core/runtime.cpp:113` 的 `Il2CppRuntime::Connect()` 通过 `OpenLoadedIl2Cpp()` 打开已加载库。
- 同文件 `:126` 当时已解析必需的 `il2cpp_domain_assembly_open`，`:128` 已解析 `il2cpp_assembly_get_image`；`:179` 的必需符号检查当时已不包含程序集枚举符号，`:196` 才解析可选的 `il2cpp_domain_get_assemblies`。
- 同文件 `:332` 的 `FindLoadedImage()` 当时在 `domain_get_assemblies_` 或 `image_get_name_` 缺失时直接返回 null（`:333`），尚未调用按名查询路径；`HasAssembly()`（`:350`）和后续类型查询依赖它。此观察已交给父代理，按前述反馈，命名回退已经实现。
- `native/shared/android_compat/loaded_il2cpp.h:6` 的 `OpenLoadedIl2Cpp()` 当时先用裸 `libil2cpp.so` 加 `RTLD_NOLOAD | RTLD_NOW`，失败后通过 `dl_iterate_phdr` 获取已加载库路径并再次打开。#18 作者早期补丁另报告通过 `/proc/self/maps` 定位库；两者不是同一段实现。
- issue 没有给出失败/成功设备所加载 SO 的完整绝对路径、SHA-256 或 build-id。不能自行填入某个 `/data/app/...` 路径或声称已验证路径/二进制相同。

## 交接

四语言资源已完整落盘，可供父代理重新 packaging。平台/来源/哈希及旧功能保留检查已完成；未修改相机、runtime、PC manifest、设备数据或旧快照。动画元数据仍沿用旧 Android 缓存，没有 APK 或真机语音播放验收；这些验证范围与数据来源限制继续明确保留。
