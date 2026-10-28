# 外置游戏资源：读取接口与基础资源迁移

实施日期：2026-10-28。完整规划见 [方案](RESOURCE_PACK_DECOUPLING_PLAN.md)。

## 已实现范围

- 双端预览从外置仓库定位角色 / 变体，不再把梨诺资源 ZIP 编入程序 / APK，不在每个会话重复解压。共用 native 接口兼容旧单角色目录，新增仓库索引、平台验证和版本诊断。Windows 用 BC7 包，Android 用 ASTC 包。
- 当前全角色生成器已覆盖 37 个可玩模型/变体；PC 提取一次，输出 BC7 / ASTC 两个包。实机显示验收仍与生成覆盖分开记录。
- 角色预设 / 名称、语音索引、战斗字典、Windows 战斗语义 / Buff 来源 / 头像 / 武器图标、动作姿态、Android 法线规则改走外置基础包。
- Windows 原生悬浮窗、战斗和动作消费者同步迁移。Android 游戏进程通过现有授权桥接获取只读描述符，不开放任意文件路径，不要求所有文件访问权限；动作副本按资源版本及长度更新，不仅看大小。
- Android “关于”页增加“导入资源包”，后台解压校验后启用。同版本不同内容拒绝覆盖；重复导入相同的已安装旧包可以切回该版。
- 已接入统一分发索引、双端资源自动更新/手动导入/固定回退，以及全角色提取生成器；协议与复现步骤见 [统一分发](DISTRIBUTION.md)。

## 目录与选择

Windows 根目录为程序旁的 `resources/`；Android 为应用私有目录 `files/betterendfieldnext/resources/`，通过文件选择器导入，不要求用户访问私有目录。

```text
resources/
  state.json
  core/windows/1.6.3.1/manifest.json
  core/android/1.6.4.1/manifest.json
  preview/windows/1.6.3.1/manifest.json
  preview/android/1.6.4.1/manifest.json
```

示例版本来自仓库输入快照，不证明设备当前游戏版本。每端只安装自己的包。目录名为四段数字，必须与 manifest 一致。`state.json` 的 `core.windows` / `preview.windows`（安卓用 `.android`）字段可固定版本。未固定则选本平台数值最高的可读取包；固定版缺失不悄悄升级。

当前读取层尚未自动检测游戏版本，不把最高资源版本冒充兼容结论。手动安装由用户选择，预览另按精确角色、prefab、renderer、材质身份匹配；自动候选匹配留给后续更新层。

预览固定持有已解析目录，使用期间不要覆盖 / 删除。Windows 基础数据按进程生命周期加载，更新后重启程序及游戏侧模块。Android 导入重建管理界面，游戏副本在重新加载配置 / 会话时刷新。

## 协议

包根为 `manifest.json`，主要字段：

| 字段 | 含义 |
| --- | --- |
| schemaVersion / kind | `1` / `betterendfield-resource-pack` |
| packageType / platform | `core` 或 `preview`；`windows` 或 `android` |
| version / gameVersion | 四位资源版本 / 目标平台三位版本基线 |
| source | 来源和验证边界，不是设备实时版本 |
| files | 相对路径到字节长度的 JSON 对象，不列 manifest 自身 |

读取器只允许清单内文件并校验长度、路径；导入检查 ZIP 完整性与额外文件。默认不计算产物哈希。基础目录包括 `model/`、`voice/`、`combat/`、`actions/`、`icons/`。Android 法线规则维护源迁到 `android/resources/android-normal-rules.json`。OptiScaler 可信锁定清单继续随软件保留，不纳入可替换游戏数据。

预览仓库另有 `characters.json`，其 `schemaVersion=1`，`characters` 为条目数组。每项记录 `character`、`variant`、`path`、`resourceRoots`、`assetPaths` 和来源快照。BEM 1.4 按完整 prefab assetPaths 匹配，旧格式按 resourceRoots 匹配；多个条目同时匹配时拒绝猜测。角色目录的 `pack.json` 保留原有几何 / 材质 / 纹理及 world/UI 绑定协议。

`scene.nativeCompletion` 增加 `reason`、`resourceVersion`，保留 `status`、`meshes`、`textures`。区分未安装、未覆盖角色、身份不匹配、错误平台、索引 / 角色无效及完成。缺失或损坏不阻止 BEM 自带内容显示；keep / hide / replace 规则不变。

## 打包

```powershell
python scripts/BuildResourcePacks.py --platform windows --type core --output temp/resource-packs/resources --archives temp/resource-packs/zips
python scripts/BuildResourcePacks.py --platform android --type core --output temp/resource-packs/resources --archives temp/resource-packs/zips
python scripts/BuildResourcePacks.py --platform windows --type preview --output temp/resource-packs/resources --archives temp/resource-packs/zips
python scripts/BuildResourcePacks.py --platform android --type preview --output temp/resource-packs/resources --archives temp/resource-packs/zips
```

- 默认前三位取平台输入，第四位由 `config/resource-packs.json` 按类型 / 平台 / 游戏版本独立维护，新基线默认 `.1`；可用 `--version` 显式指定。同版本内容不同必须自增，不覆盖已生成目录。Android 基础包初始开发过程已递增到 `1.6.4.2`，避免覆盖之前测试产物；不分发 Windows 专用战斗数据。
- `--workspace-config` 复用工作区路径；MSBuild 可显式传角色预设、语音索引、战斗字典路径。
- preview 默认封装现有梨诺样包；可重复使用 `--preview-archive` 指定其他已生成角色 ZIP。不负责游戏提取，不自动宣称跨平台实测完成。
- ZIP 根直接放 manifest 和内容。Windows 解压到对应类型 / 平台 / 版本目录；Android 从“关于”导入。
- Windows Build / Publish 自动放置基础目录。Android preBuild 生成外置基础目录与 ZIP，位于工作区 build 根的 `resource-packs/`、`resource-pack-archives/`，不放回 APK；预览另行生成安装。

## 验证与边界

- `scripts/tests/test_game_resource_packs.py`：清单、长度、路径、四位版本、同版本不同内容和平台纹理。
- `ui/tests/GameResourceStoreTests/`：版本数值比较、平台过滤、回退固定、固定版缺失、非法路径和坏长度。
- `native/tests/bem_preview_cli_tests.py`：既有预览回归，以及仓库定位、平台、损坏降级、歧义、缺包和无关角色惰性读取。
- 编译 Windows UI / native 消费模块与 Android Java / native / APK，并检查 APK 不再含旧内嵌数据。实机导入、游戏进程跨 UID 读取和显示体验需后续设备验证，构建成功不代替实机验收。
