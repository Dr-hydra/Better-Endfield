# Better Endfield Next 4.2.3

Windows / Android 软件版本 4.2.3，Android versionCode 40203。
BEM Tools / DSH 升级到 1.7.1。
core 资源包升级到 `1.7.2.2`（Windows / Android）。

## 1.7.2 游戏适配与工具链 1.7.1

- **核心基础资源升级至 `1.7.2.2`。**
  - 双端补齐 1.7.2 角色名表（新增米米诺 `chr_0039_mimino` 与小刻 `chr_0040_sweetpie`）、短语音索引（1,089 个具名 Event，706 个已映射）与 1.7.2 Windows 战斗字典、装备/武器/头像图标和 MemoryPack schema。
  - 修复核心资源扫描把明河 `02` / `03` 变体重复扫入角色预设的问题：按同 `(rigFamily, codename)` 分组去重，自动剔除共享明河动画目录且无 UI prefab 的 `chr_0037_minghe02` / `chr_0037_minghe03`，双端角色清单统一为 37 个角色、4,790 个动作。
  - 兼容 1.7.2 `StringPool` 按 chunk 自身条目数寻址的变化。
- **创作者工具（BEM Tools / DSH 插件）升级至 1.7.1。** 新增小刻（`chr_0040_sweetpie`）1.7.2 角色资料（`20261125-1.7.2-native-sweep1`），并标定 `game_baseline` 为 `1.7.2`。

## 相机增强与 MMD 播放（Windows）

- **MMD 播放支持直接解码 `.ogg` 本地音轨。** 内置 `stb_vorbis` 1.25 解码 `.ogg`（Ogg Vorbis）并接入现有重采样与环形缓冲链路，绕开 Windows Media Foundation 不支持 `.ogg` 的限制（`.opus` 仍暂不支持）。本地曲库和动作绑定音轨均可选用 `.ogg`。
- **新增「原地齐舞 / 各自原地起舞」开关（`vmd_per_actor_anchor`）。** 启用后全队每位参演角色都在各自当前位置原地起舞，不再全员叠回领队原点；动作位移缩放下限同步放宽到 0%，可完全固定原地播放。
- **修复 MMD 停止后切换或重新加载动作时偶发卡死的问题。** 解除 `Log` 临界区阻塞对 EIEM 生命周期屏障的饿死风险，并为屏障等待加上超时保护与更清晰的工作线程状态标识。
- **新增 MMD 角色白色描边及按槽位独立控制。** 基于游戏内置 `CharacterOutline` 渲染通道在播放期间为参演角色开启描边，支持在相机增强页按角色/槽位独立开关并调节描边宽度倍率（50%–800%，默认 300%）；停止播放、队员离场或配置变更时自动恢复游戏原状态。

## 发布流程

- 云端发布 workflow（`.github/workflows/release.yml`）收口为仅由 `v*` tag 触发签名与发布，不再因 `workflow_dispatch` 与自动建 tag 并发互相顶掉。
