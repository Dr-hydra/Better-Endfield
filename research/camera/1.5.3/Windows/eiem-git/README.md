# EIEM Importing Endfield MMD

[English](README_EN.md) | 中文 | [日本語](README_JA.md)

为《明日方舟：终末地》提供 MMD 动画播放能力。支持 VMD 动作直接播放（及预烘焙肌肉动作）、面部表情、手指动画、相机运动和背景音乐同步，通过游戏内 GUI 面板控制。

演示动画: [bilibili](https://www.bilibili.com/video/BV1YdEC6bEfP/)
交流群：1036919766

## 用户协议与免责声明

<details>
<summary>在下载、安装或使用本插件（EIEM）之前，请您仔细阅读本协议。<b>使用本插件即代表您已完整阅读、充分理解并同意遵守以下所有条款。</b></summary>

### 1. 开源许可与最终用户权利
- 本插件基于 **AGPL-3.0** 许可证在 GitHub 平台完全开源。用户可在遵守该许可证的前提下自由使用、修改和分发本插件的源代码。
- 最终用户（End User）在不对本插件进行修改的前提下，使用和分发本插件**不受任何限制**。此权利不因用户是否违反本协议而改变。

### 2. 反欺诈声明
- 您**不得**在网络销售平台公然售卖本插件**软件本体**且未提供 GitHub 仓库地址与售后服务。
- 本插件在 GitHub 平台完全免费开源，如果您是付费购买获取的，请知悉本插件可从 GitHub 免费获取。

### 3. 内容合规与行为约束
- 本插件本身不包含任何游戏美术资产。用户知悉并同意，《明日方舟：终末地》游戏内置的官方动画、场景、模型等资产其版权完全隶属于鹰角网络，并不适用 AGPL-3.0 协议。您**不应该**利用本插件，或利用游戏内置的官方动画、场景、模型等游戏资产，制作、播放或传播任何不合适的动作/动画（包括但不限于色情、暴力、政治敏感等违反法律法规或引起社区不适的内容）。使用者应自行负责确保起拥有任何汇入资源的版权，并遵守其许可和使用条款。

### 4. 风险与免责声明
- 本项目仅供学习、技术研究和交流目的。作者不对使用者如何使用本插件负责，包括任何误用情况。本插件中使用的明日方舟游戏数据资产版权均隶属于鹰角网络。使用本工具可能违反游戏服务条款，存在账号封禁的风险。因使用本插件而直接或间接导致的任何损失（包括但不限于账号封禁、游戏数据损坏等），**本项目不承担任何法律或经济责任**。用户需自行承担所有风险，强烈建议您在测试账号上运行。

</details>

## 功能

### 已实装
- **VMD 直接播放模式**：直接读取标准 VMD 动作，无需 PMX 或预先转换 MUS4
- **全身骨骼重定向**：支持 Root、上下半身、头颈、肩臂、手腕、手指和腿部动作
- **腿部 FK/IK**：根据 VMD IK 开关逐腿切换 FK 与 FinalIK 解算
- **面部表情与眼睛**：播放 VMD Morph、眨眼、口型等基础表情和眼睛动作
- **相机运动**：VMD 相机关键帧
- **音频同步**：支持 WAV/MP3，并同步播放、暂停、拖动、循环和结束
- **地形与台阶跟随**：可选地形后处理，支持平地、斜坡和楼梯自适应吸附
- **MUS4 肌肉模式**：通过 95 个 muscle 值，驱动全身动作
- **服装增强**：基于原生布料系统，结合内置专用配置与运行时自动适配

### 已计划
- 多角色同屏播放
- ...

## 下载

您可以在 [Releases](https://github.com/Sasye/EIEM/releases) 下载最新发布版或从源代码自行编译。

> 使用 [Applepie Manager](https://github.com/Sasye/ApplepieManager) 来便捷地管理和配置此插件。

## 安装

将以下文件复制到游戏目录（`Endfield.exe` 所在文件夹）：

```
bin/eiem.dll             → 游戏目录/plugin/eiem.dll
bin/vulkan-1.dll         → 游戏目录/vulkan-1.dll
bin/d3dcompiler_47.dll   → 游戏目录/d3dcompiler_47.dll
```

> **注意**：`d3dcompiler_47.dll`（DX环境）和 `vulkan-1.dll`（Vulkan环境）为代理加载器，二者放其一或全放均可。如果你同时在使用其他共用的代理加载器插件（如 [AntiKick](https://github.com/Sasye/EndFieldAntiKick) [SynchroFocus](https://github.com/Sasye/EndfieldSynchroFocus) 或 [EndfieldCombatHUD](https://github.com/Sasye/EndfieldCombatHUD)等），无需重复放置代理加载器。

> 如果您**没有安装过此类型的插件**，您可能需要自行创建plugin文件夹。

## 播放模式

| 模式 | 动作来源 | 是否需要转换 | 说明 |
|------|----------|--------------|------|
| **VMD直接模式（默认）** | 动作 VMD | 不需要 | 直接驱动全身骨骼、IK、Morph 和相机 |
| **预烘焙 MUS4 肌肉模式** | MUS4 肌肉数据 | 需要预先导出 | 预烘焙 Humanoid Muscle 播放管线 |

## 资源文件准备

指定以下文件：

### VMD直接模式（默认）

| 文件 | 说明 | 必要性 |
|------|------|--------|
| `*.vmd` 动作 VMD | 提供骨骼、IK，并默认提供 Morph 和相机数据；选择后自动加载 | **必须** |
| `camera.vmd` 镜头 VMD | 可选相机VMD；留空时使用动作 VMD 的 Camera section | 可选 |
| `*.vmd` 面部 VMD | 可选面部 VMD；留空时使用动作 VMD 的 Morph section | 可选 |
| `bgm.wav` 或 `bgm.mp3` | 背景音乐 | 可选 |

### 预烘焙 MUS4 肌肉模式

| 文件 | 说明 | 必要性 |
|------|------|--------|
| `muscle_anim.bin` | MUS4 格式动作数据（由ExportMuscleAnimation.cs导出） | **必须** |
| `*.vmd` | VMD 文件（面部表情 morph 数据） | 可选 |
| `camera.vmd` | 相机运动数据 | 可选 |
| `bgm.wav` 或 `bgm.mp3` | 背景音乐 | 可选 |

## 使用方法

1. 按上述方式安装后启动游戏，进入游戏。
2. 按 **Insert** 键打开 GUI 面板。
3. 在「文件」页顶部选择播放模式，默认是 **VMD直接模式**。
4. 加载指定文件后在「控制」页点击 **播放** 按钮开始播放动画。

## 可选：预烘焙 MUS4 肌肉模式动作导出（VMD → MUS4）

> **注意**：只有使用预烘焙 MUS4 肌肉模式时才需要此步骤。

需要先通过 Unity 编辑器将 VMD 动画转换为 MUS4 格式的 `muscle_anim.bin`。

### 前置条件

- Unity 编辑器
- [MMD4Mecanim](https://stereoarts.jp/#:~:text=MMD4Mecanim_Beta_20200105.zip) — 用于将 VMD 转换为 Unity AnimationClip
- 任意 Humanoid MMD 模型

### 步骤

1. 将 `ExportMuscleAnimation.cs` 放入 Unity 项目的 `Assets/Editor/` 文件夹
2. 导入 MMD 模型（设为 Humanoid Rig），使用 MMD4Mecanim 将 VMD 转换为 AnimationClip
3. 创建 Animator Controller，将转换好的 AnimationClip 添加为默认状态
4. 将 Animator Controller 挂载到场景中的模型上
5. **选中模型**，点击菜单栏 `Tools > Export Muscle Animation`
6. 导出文件 `Assets/muscle_anim.bin`，将其复制到游戏的 `plugin/` 目录即可
