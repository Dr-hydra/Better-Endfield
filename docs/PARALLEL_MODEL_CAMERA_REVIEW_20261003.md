# 双端模型与安卓相机改动记录（2026-10-03）

基线为 main `dd7131b4`（3.4.3），本轮改动保存于工作区。使用五个代理并行处理，界面只增加必要控件、标题、数值与操作结果，没有增加说明段落。

## 完成内容与研究范围

1. 双端第三方模型页增加关闭全部模型和按角色筛选。关闭作用于完整包列表，复用现有原子保存与更新链路；筛选只改变卡片可见性，保留展开项和未应用的参数。详见 `BEM_MODEL_MANAGEMENT_20261003.md`。
2. 安卓悬浮窗设置增加透明度滑条与自动贴边。即时保存；吸附后自动收起为 24×40dp 窄标签并贴安全左右边，点击/拖动恢复普通图标，松手再次收小；保留旋转适配与本次关闭。详见 `ANDROID_OVERLAY_SETTINGS_20261003.md`。
3. 第一人称修复未知顶点数时的头饰漏扫、骨骼重新挂接后的分类/重试失效；保留未知混合躯干。缺乏权重、位于 Spine2 下的部分帽骨仍不能安全整体隐藏。详见 `FIRST_PERSON_HEAD_ACCESSORIES_20261003.md`。
4. 第一人称眼位使用游戏最终方向，包括 correction/Dutch；按当前 push 转身后再采样头骨。空闲转向改用实体 Rotator，攻击/技能/移动/root motion 等状态交还游戏。用户反馈视角已有改善、头饰仍不足，尚不能据此证明所有动作完全解决。详见 `FIRST_PERSON_ACTION_FACING_20261003.md`。
5. 艾尔黛拉已确认 fur 源 Mesh 带 `_20` 后缀、Renderer 不带，旧绑定混淆两种身份；双端局部映射已整合，完整 Mesh 校验保留。已有 32 角色 PC 元数据中，7 个 fur 组件有同类差异，其它 6 个还存在 LOD1 `_8` 差异；不能仅去 LOD0 后缀。狼卫样本的 72 条非阴影绑定全部同名，其失败仍需对应包/日志。提弗洛斯参考的历史问题按用户纠正为眉毛部件命名，裙骨 alias 是另一问题。最新定点结果见 `WORLD_MODEL_BINDING_20261003.md`。
6. 首次加载峰值与热切换回收分开。手机日志确认普通 world-first 重复构建 UI 贴图；修正为已有的 world/UI 联合发布，减少确定的一轮重复上传，并增加每次事务的实际构造数/上传 payload 量统计。分帧上传属于下一阶段方案，未把 CPU 解压节省误报成 GPU 峰值节省。详见 `MODEL_UPLOAD_PEAK_REVIEW_20261003.md`。

## 集成检查

- PC WinUI Release：编译通过，0 警告/错误。
- Windows Camera/CustomModel 生产 Release：编译通过；已有 shared_ptr atomic 弃用及 MinHook 警告保留。
- Android arm64 Release APK 和 `lintRelease`：通过。整合时修正了新读取角色名称代码的 API 29 兼容问题；旧默认字符串有明确语言回退，只对该旧资源文件标记允许缺少翻译，新增控件的五组资源完整验证。
- 实际生产翻译单元的第一人称/头饰回归 CTest 2/2 通过，包含动作方向 1222 项和头饰/阴影 114 项检查；CPU 几何/重试测试通过。
- 悬浮窗自由/贴边/窄标签共 301,021 个几何案例通过，含安全区、极小窗口、旋转、归一化和横向贴边。
- 实际 parser/material/ownership/upload-count 回归通过；增加实际 PrepareResource 的艾尔黛拉正负身份测试。Android world 132 项回归通过，含普通/热切换共用资产、联合发布、失败双端回滚、cached donor 和精确命名检查。
- 元数据上传工具在本地实际包及合成 hidden/default 选择上核对；手机只读采集仅取需要的包元数据，没有复制整份 394MB 包。
- `git diff --check` 通过。

## 产物与设备

Android 包：`artifacts/BetterEndfield-Android-3.4.3-20261003-model-camera.apk`，10,354,101 字节，已用 `adb install -r` 覆盖安装成功，保留应用数据；15:37 再次安装包含窄标签和艾尔黛拉修正的包。UI/Dex 的窄标签入口与 native 映射返回名、上传统计已核对打包。读取旧日志时游戏没有进程，后续只读检查出现又退出游戏；本轮没有主动启动游戏、点击界面或修改用户功能开关。已有新日志确认首次 world 的 paired donor 不再二次上传；单次约 0.9GB 峰值的剩余暂存仍需调研。新注入代码按正常启动流程加载。PC UI 及两个 native 模块已构建，桌面部署未在本轮执行。

本轮构建、离线测试及已有手机日志不能代替新的游戏内验收。尚需核验头饰实际可见性/阴影、攻击期间的方向和眼位、具体包的世界模型失败原因，以及首次加载峰值的改善程度。

用户追加的原生加载/单次上传卡顿调研和 legacy 热切换/旧资源缓存调研分别写入 `NATIVE_MODEL_LOADING_RESEARCH_20261003.md`、`HOT_SWITCH_LEGACY_PATH_RESEARCH_20261003.md`。两项已完成，只做调研，没有实施下述生产行为变更：

- 加载：后台纯数据解压、主线程分帧/字节/对象预算、预热 Ready 成品、最终事务发布；单张 64MiB Apply 仍可能长帧。游戏原生队列与符合条件的 Unity async upload 可参考，但运行时 Apply 不能直接认定进入它们。当前 Unity 2021.3 没有新版跳过初始化的公开接口，未知 flag 不硬编码。
- 热切换：保留现有事务和 pristine donor，增加配置驱动的目标实例重绑、同原始资产身份的有界成品复用及分代生命周期管理；关包记录可能被自身 Original 引用保活，先定点验证。旧 PoC 全局轮询和长期强持有不整套搬回。

用户给出 `https://github.com/ItsTheSewerRat/renodx` 后，追加只读源码对照，记录为 `RENODX_FIRST_PERSON_HIDING_RESEARCH_20261003.md`；不新增生产隐藏行为。

该对照已完成。公开 enhancer 功能分支取得的是 2026-09-13 的 `7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7`，默认 main 的 Endfield 画质代码不能替代相机功能源。网络中断后以已取得快照交付，不声称它等同于此前 9 月 28 日插件二进制的所有实现。

可借鉴深层 includeInactive 扫描、投影 proxy 身份核对和 clone/外部接管恢复约束；其名称整块隐藏、Head 权重多数和 Head+Neck 过半更激进，可能删除 BEM 混合身体。它仍没有通用 Spine2 帽链、普通 MeshRenderer 或 Android 无 GPU 数据的完整解决方案。下一阶段优先核对被现有规则跳过的 `hairshadow` 是否实际可见，再补有证据的帽链/独立 Neck 边界语义，并继续用当前 BEM CPU 数据裁剪；不全局放行 Neck/Spine2 或按关键词强删。MIT 许可来源/分发声明与私有 Windows 后端限制已记入专项文档。
