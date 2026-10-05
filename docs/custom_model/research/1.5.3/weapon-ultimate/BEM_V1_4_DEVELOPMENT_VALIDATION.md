# BEM 1.4 武器与大招开发验证

日期：2026-10-05。工作区：`G:\Better Endfield`。分支：`dev/bem-1.4-weapons-forms`，基于 `3510fa7`。

本文记录首轮实现与草稿验证。后续已修复 Android 显式包的 LOD 偏置回归，并利用 G 盘真实 EFMI 素材重建研究包，见 [Android LOD1 与 EFMI 复查](BEM_ANDROID_LOD1_EFMI_FOLLOWUP.md)。

## 实现范围

实现 [BEM 1.4](../../../BEM_V1_4_SPEC.md) 的多资源目标与静态网格；保留 1.0–1.3 读取、选项、纹理与位置形变。武器按武器 ID 管理，角色大招按角色 ID 下的独立资源管理，状态切换继续由游戏负责。

- C++ 与 Python 校验资源、平台、精确 receiver 路径和资源内 donor；即使关闭普通模型校验，也不能跨资源引用 donor 或绕过 LOD 合同。
- 注册表按当前平台展开资源，异步请求、缓存与组件编号按资源隔离。静态 MeshRenderer/MeshFilter 已接入上传、提交、回滚、克隆和停用恢复。
- Android 显式资源使用自身 donor。阴影代理需由实际 Mesh 引用、骨骼对象与空间证明唯一归属；外观切换恢复原 shadow 状态，相关对象随保存状态保活。
- Windows UI、原生浮窗、Android 导入和启用入口使用当前平台资源判断冲突；旧包同角色互斥保留。没有本平台资源的 1.4 包不能启用，同包 ID 更新不能更换所有者。
- NativeAssetReader 与离线解析器增加静态 renderer/filter 身份连接；空 prefab 与资源缺失分别报告。制作工具提供 `target-profile`、1.4 打包/解包/校验及可移植的合成示例。

## 真实资源证据

Windows 1.5.3，snapshot `2954fa80-23c1-1579-2b22-4ecfd6d70418`。详细基线见 [可行性调研](BEM_WEAPON_ULTIMATE_FEASIBILITY.md)。

| 草稿 | LOD0 组件 | 状态 |
| --- | ---: | --- |
| 庄方宜大招主体、技能实体、镜像三个资源 | 36 | 已生成独立组件契约；全部 keep |
| `wpn_sword_0014` | 1 | 静态 MeshRenderer/MeshFilter；全部 keep |
| `wpn_funnel_0014` | 4 | 蒙皮；全部 keep |

草稿位于 `tools/CustomModel/profiles/bem14-drafts/`。`runtime_verified`、`conversion_ready` 与渲染验收标记保持 false。Windows 证据没有被当作 Android 证据使用。`abilityentity_chr_0030_zhuangfy_sword_postmodel` 是无 renderer 的空/效果 prefab，未伪造可替换飞剑网格。

## 已完成验证

| 范围 | 结果 |
| --- | --- |
| Python 制作、格式、离线解析及原生校验联测 | 运行 221 项，220 通过、1 项跳过 |
| 冻结 EXE 实际命令 | 10/10；含 build、pack/unpack/repack、资源/平台筛选、真实图谱 target-profile 与错误 LOD 拒绝 |
| 工具发行包内容与链接 | 11/11 |
| 原生 1.4 资源测试 | 53 项通过；资源重映射、形变、静态隐藏、donor/版本/LOD 拒绝、冲突、重写 |
| 原生 1.3 形变与旧版兼容 | 86 项通过；1.1 33/64 组选项接受、65 组拒绝 |
| 显式/旧式资源匹配 | 86 项通过 |
| Windows 绑定回归 | 静态 MeshFilter、事务回滚、克隆、停用、旧版绑定、异步 fallback、几何桥接通过 |
| Android 原生绑定与阴影 | 197 项通过；含 replace → keep、replace → hide → keep 恢复 |
| 独立异步 CTest | 3/3 |
| 原生模型浮窗 | 71 项通过 |
| Android 管理状态 | 122 项通过，另含浮窗、热切换和 35 项第三方模块写入边界检查 |
| C# 管理器 | `ManagerChecks --v14` 通过；完整 Windows UI 构建 0 警告、0 错误 |
| Windows 原生最终构建 | CustomModel、ModelOverlay 及相关校验/绑定测试目标通过 |
| Android 最终构建 | `assembleRelease` 和 `lintRelease` 通过；lint 0 错误、174 警告 |

Python 跳过项是源码环境既有 7zip 后端路径测试，冻结 EXE 的实际打包操作已单独验收。Android 编译仍有平台未使用函数等警告；本次没有把构建通过表述为零警告。

最终日志位于 `build/bem14/logs/`；制作工具日志、命令验收和包内容报告位于 `build/bem14/creator/`。`git diff --check` 通过。未计算发布产物哈希。

## 开发产物

- 制作工具 ZIP：`build/bem14/creator/dist/BEM-Tools-win-x64.zip`。
- Android 开发测试 APK：`build/bem14/android/android/gradle/app/outputs/apk/release/app-release.apk`。
- Windows 模块：`build/bem14/native/stage/Release/modules/BetterEndfield.CustomModel.dll` 与 `BetterEndfield.ModelOverlay.exe`。

APK 使用现有版本配置与本地签名，尚未安装到设备；它不是新的正式 Release。构建明确排除了 `:app:archiveAndroidRelease`，本任务没有覆盖 `releases/3.5.1`，也没有上传 GitHub Release。

## 仍需游戏验收

Windows 当前只执行显式 LOD0 目标。同资源仍要求 mesh 名称与 receiver 路径分别唯一。Android 目标必须先补齐 Android 实际资源布局、顶点声明及阴影证据。

真实草稿没有替换几何；普通角色的 EFMI/Blender 自动映射没有被直接套到武器或大招。本次完成的是格式、制作和运行时支持，不能宣称庄方宜大招或武器已完成视觉替换。

后续应使用实际制作的模型分别验收：首次及连续开大、打断与结束、回池复用、加载后启用/停用、替换与 keep/hide 切换、武器挂接、阴影、恢复原模型。法器的序列化 UInt32×1 骨骼索引仍需运行时布局证据；可见飞剑 VFX 的资源链也尚未完成定位。
