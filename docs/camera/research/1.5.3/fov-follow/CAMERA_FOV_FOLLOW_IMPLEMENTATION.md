# 双端全局 FOV 与自由镜头跟随实现

2026-10-02。本轮已实现共用原生逻辑、Windows 设置与持久化、Android 设置与持久化；没有安装或发布测试包，没有进行游戏内效果验收。

## 行为与配置

| 配置键 | 默认值 | 行为 |
|---|---|---|
| `global_fov_enabled` | false | 独立启用普通主相机 FOV 覆盖；单独开启也会装载相机模块 |
| `global_fov` | 60 | 垂直 FOV，允许 5–150 度，非有限值回退为 60 |
| `free_camera_follow_character` | false | 仅在手动自由镜头中跟随当前人物平移 |

旧配置没有上述键时保持原行为。Windows 放在相机页面，保存时保留其它高级运镜/MMD 参数；Android 相机设置页增加对应开关、滑条和模式说明，沿现有配置发布流程在重启游戏后装载。

全局 FOV 只处理当前透视 `Camera.main` 对应的 Cinemachine Brain，缺少必要元数据、另一 Brain 或正交相机时不覆写。第一人称、自由相机和导入运镜仍使用其自身 FOV。没有新增剧情/菜单相机分类猜测：若这些场景使用同一透视主相机且没有进入其它模块模式，同样会受到覆盖。

`ScopedGlobalFovState` 在原生 push 前暂存上游 FOV，调用原 push 时提供用户值，返回后恢复上游输入。关闭后，下一次原生相机 push 直接输出游戏值；不将启用时某个旧场景的 FOV 写回新相机。原生 push 完全停止时不会额外接管相机，恢复发生在游戏下一次更新。

跟随每次取得当前角色→ModelCom→ModelGo→Transform，检查 Transform 存活，只缓存用于比较的身份和上次位置，不长期解引用角色指针。位移同时加到目标、平滑状态和实际自由视图，保留镜头旋转、FOV 和手动偏移，避免平滑引入额外跟随拖尾。

切人、ModelGo/Transform 更换、空角色窗口、无效坐标、关闭跟随及开始轨迹/VMD 播放时失效锚点；恢复时重新建立参考，不累计缺失期间位移。一次采样超过 50 个世界单位视为位移不连续，重新建立参考并保持镜头世界位置。当前只跟平移，不自动跟角色转向或增加避障。

## 检查

- `native/tests/camera_playback/fov_follow_tests.cpp`：25 项检查，执行实际 production TU，覆盖 push 上游状态还原、关闭、另一 Brain、正交相机、模式优先级、非法参数、平滑原点跟随、null 角色、传送、播放暂停与模型替换。
- 相机播放与生命周期 CTest：10 / 10 通过，包含新增第一人称 313 项检查。
- 隔离 C# 配置 harness：189 项检查通过；默认关闭、单独 FOV 启用模块、文件读写、其它配置保留及数值/地区格式验证见 `artifacts/camera-config-audit-20261002/REVIEW.md`。
- Windows UI Release 编译通过，0 警告、0 错误；Windows Camera Release 目标通过。
- Android Java Release、arm64 原生 Release 编译通过。原生构建有现有 unused 警告，无编译错误。
- 新原生函数与 UI 变更不依赖 BEM 格式升级；头发精细剔除和体型滑条仍是独立研究，未混入本轮实现。

完整构建/测试日志保存在忽略目录 `artifacts/title-purrche-20261002/`，没有计算产物哈希，也没有改用户安装目录或设置。
