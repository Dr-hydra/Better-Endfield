# 噗切娜开屏资源索引补全

本轮仅修复双端开屏角色/动作索引与生成规则。没有编译、安装或发布新产物，也没有修改游戏文件。

## 原因与修改

- 当前 `npc_chr_0038_purrche.json` 没有 `correspondingCharId`，输入刷新脚本直接排除了该记录。它有自身的 `chr_0038_purrche_postmodel` 和明确的 `NPC/AnimationConfig/Humanoid/Panda/purrchena`。新规则允许这类记录，并仍拒绝缺自身模型或 humanoid 配置的记录。
- 角色 ID 使用 `purrche`，动画/美术目录使用 `purrchena`。扫描器改从 PrefabInfo 的动画配置获取真实 codename，不从角色 ID 拼目录。
- 双端各新增模型、三段坐姿链和 52 个当前可加载动作，预设总数由 32 / 4,210 增至 **33 / 4,262**。该角色没有旧默认站姿动作，明确选择其坐姿循环作为默认动作，避免按字母序选中攻击结束动作。
- Windows 补中文与英文名称；Android 原有噗切娜名称，无需再次修改。
- 更新双端动作 manifest 和统计；所有旧 32 个角色预设、动作和绑定保持不变。祀仍缺完整 model/sit chain，不猜测资源。

## 当前来源与平台区别

工作输入保存在忽略目录 `artifacts/title-purrche-20261002/`，含定点提取脚本、原始/解码 PrefabInfo、manifest、来源记录、扫描结果及检查报告。未将游戏 bundle、网格、贴图或动画 payload 加入分发目录。

PC 从 `E:\Endfield Game` 的 StreamingAssets → Persistent 有效 VFS 读取当前 manifest 与角色 PrefabInfo；Android 从已连接设备 `78572d34` 的当前 Persistent VFS 只读读取 BundleManifest / JsonData 索引及必要文件范围。目标资源均直接命中当前 Persistent，不使用旧缓存补缺。未读取 AudioDialog 或音频包。

| 数据 | PC | Android |
|---|---|---|
| manifestHash（资源内已有标识） | `8b9cf90e2097d4b4767c76ae00807eb6` | `664d17e6f5f4fd44513249dbeabf4c53` |
| 模型 pathHash | `0x044527ED1A1CC77C` | `0x044527ED1A1CC77C` |
| 模型 bundleHash | `0x824A49EACC851526` | `0xE7722DB90A0B9723` |
| sitLoop pathHash | `0x041AD9AE74520A2B` | `0x041AD9AE74520A2B` |
| sitSpecial pathHash | `0x035D5CBAB4BA9C52` | `0x035D5CBAB4BA9C52` |
| sitToWalk pathHash | `0x0C6A17700B7635A4` | `0x0C6A17700B7635A4` |

上述 Hash 是原资源 manifest 字段，不是本轮计算的产物哈希。双端 bundle 标识不同，不能复制 PC 模型 bundleHash 到 Android。

旧角色动作的 LoopTime/时长保留已验证的各平台缓存。新角色 52 个动作的 LoopTime/时长未单独导出，保持未确认信息，不按名称伪造 `nativeLoop` 或时长；默认动作选择与原生 LoopTime 判断相互独立。

动作 manifest 的 `source` 保留原基线来源，并增加此次角色增量的 `updates` 证据。Android 来源文件中两个已变化输出移除旧 SHA-256，改记实际字节大小与本记录；未计算新的产物哈希。原 manifest/音频来源记录仍保留。

## 检查

- `py -3 -B native/tests/character_presets_test.py`：2 项测试通过，覆盖缺角色 ID 的输入筛选、codename 差异和平台 bundle 区分的扫描→预设生成链。
- 与 Git HEAD 比较：双端旧 32 个预设全部相等，无角色或动作删除，仅新增噗切娜。
- 双端每个模型、坐姿和动作 path/pathHash 都与分别读取的当前 manifest 一致，模型 bundleHash 也与当前 bundleIndex 对应。
- 双端动作 manifest、角色名称、唯一动作 ID、默认动作存在性和统计通过检查。
- Windows 通过 `BetterEndfield.UI.csproj` 嵌入预设资源，Android 通过 `prepareAndroidResourceAssets` 打包 `android/resources` 预设；更新在下一次正常构建生效。

未进行噗切娜开屏游戏内播放验收，未宣称当前已安装的 3.4.1 包包含此修复。
