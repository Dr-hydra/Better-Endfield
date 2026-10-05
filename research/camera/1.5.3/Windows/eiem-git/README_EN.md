# EIEM Importing Endfield MMD

English | [中文](README.md) | [日本語](README_JA.md)

Provides MMD animation playback capabilities for *Arknights: Endfield*. Supports direct VMD motion playback (and pre-baked muscle animations), facial expressions, finger animation, camera motion, and synced background music, all controlled through an in-game GUI panel.

Demo Video: [bilibili](https://www.bilibili.com/video/BV1YdEC6bEfP/)
Community group: 1036919766

## User Agreement & Disclaimer

<details>
<summary>Please read this agreement carefully before downloading, installing, or using this plugin (EIEM). <b>By using this plugin, you acknowledge that you have fully read, understood, and agreed to all of the following terms.</b></summary>

### 1. Open Source License & End User Rights
- This plugin is fully open-sourced on GitHub under the **AGPL-3.0** license. Users may freely use, modify, and distribute the source code of this plugin in compliance with the license.
- End Users may use and distribute this plugin **without any restrictions**, provided they do not modify it. This right is not affected by whether the user violates this agreement.

### 2. Anti-Fraud Statement
- You **must not** openly sell this plugin **itself** on online retail platforms without providing the GitHub repository address and after-sales service.
- This plugin is entirely free and open-source on GitHub. If you obtained it through a paid purchase, please be aware that it is freely available on GitHub.

### 3. Content Compliance & Conduct
- This plugin does not contain any game art assets. Users acknowledge and agree that the official animations, scenes, models, and other assets built into *Arknights: Endfield* are copyrighted by Hypergryph and are not covered by the AGPL-3.0 license. You **should not** use this plugin, or any in-game official assets, to create, play, or distribute any inappropriate motions/animations (including but not limited to pornographic, violent, politically sensitive, or other content that violates laws and regulations or causes community discomfort). Users are responsible for ensuring that they hold the copyright to any resources they import and for complying with the applicable licenses and terms of use.

### 4. Risk & Disclaimer
- This project is for educational, technical research, and communication purposes only. The author is not responsible for how users use this plugin, including any misuse. All Arknights game data assets used in this plugin are copyrighted by Hypergryph. Using this tool may violate the game's terms of service and carries a risk of account suspension. For any loss directly or indirectly caused by using this plugin (including but not limited to account bans, game data corruption, etc.), **this project assumes no legal or financial liability**. Users bear all risks and are strongly advised to use it on a test account.

</details>

## Features

### Implemented
- **Direct VMD playback mode**: Directly reads standard VMD motions without requiring PMX or pre-converting to MUS4
- **Full-body bone retargeting**: Supports Root, upper/lower body, head/neck, shoulders/arms, wrists, fingers, and leg motions
- **Leg FK/IK**: Dynamically switches between FK and FinalIK per leg based on VMD IK keyframes
- **Facial expressions & eyes**: Plays VMD Morphs, basic expressions (blinks, mouth shapes, etc.), and eye gaze motion
- **Camera motion**: VMD camera keyframes
- **Audio sync**: Supports WAV/MP3 with synchronous play, pause, seek, loop, and end handling
- **Terrain & staircase stepping**: Optional terrain post-processing, adaptively snapping to flat ground, slopes, and stairs
- **MUS4 muscle mode**: Full-body motion driven by 95 muscle values
- **Clothing enhancement**: Uses the game's native cloth system, combining built-in outfit-specific configurations with automatic runtime adaptation

### Planned
- Multi-character screen playback
- ...

## Download

You can download the latest release from [Releases](https://github.com/Sasye/EIEM/releases) or compile it yourself from source.

> Use [Applepie Manager](https://github.com/Sasye/ApplepieManager) to easily manage and configure this plugin.

## Installation

Copy the following files into the game directory (the folder containing `Endfield.exe`):

```
bin/eiem.dll             → game_dir/plugin/eiem.dll
bin/vulkan-1.dll         → game_dir/vulkan-1.dll
bin/d3dcompiler_47.dll   → game_dir/d3dcompiler_47.dll
```

> **Note**: `d3dcompiler_47.dll` (DX environment) and `vulkan-1.dll` (Vulkan environment) are proxy loaders. You can place either one or both. If you already use another plugin that shares a proxy loader (such as [AntiKick](https://github.com/Sasye/EndFieldAntiKick), [SynchroFocus](https://github.com/Sasye/EndfieldSynchroFocus), or [EndfieldCombatHUD](https://github.com/Sasye/EndfieldCombatHUD), etc.), there is no need to place the proxy loader again.

> If you have **never installed a plugin of this type before**, you may need to create the `plugin` folder yourself.

## Playback Modes

| Mode | Motion Source | Conversion Required | Description |
|------|---------------|---------------------|-------------|
| **Direct VMD Mode (Default)** | Motion VMD | No | Directly drives full-body bones, IK, Morphs, and Camera |
| **Pre-baked MUS4 Muscle Mode** | MUS4 muscle data | Yes (pre-export required) | Pre-baked Humanoid Muscle playback pipeline |

## Preparing Resource Files

Specify the following files:

### Direct VMD Mode (Default)

| File | Description | Required |
|------|-------------|----------|
| `*.vmd` Motion VMD | Provides bones and IK, and defaults to providing Morph and Camera data; auto-loaded upon selection | **Yes** |
| `camera.vmd` Camera VMD | Optional camera VMD; if left empty, uses the Camera section of the motion VMD | Optional |
| `*.vmd` Face VMD | Optional facial VMD; if left empty, uses the Morph section of the motion VMD | Optional |
| `bgm.wav` or `bgm.mp3` | Background music | Optional |

### Pre-baked MUS4 Muscle Mode

| File | Description | Required |
|------|-------------|----------|
| `muscle_anim.bin` | MUS4-format motion data (exported via ExportMuscleAnimation.cs) | **Yes** |
| `*.vmd` | VMD file (facial expression morph data) | Optional |
| `camera.vmd` | Camera motion data | Optional |
| `bgm.wav` or `bgm.mp3` | Background music | Optional |

## Usage

1. Install as described above, launch the game, and enter the game.
2. Press **Insert** to open the GUI panel.
3. Select your playback mode at the top of the "File" tab (defaults to **Direct VMD Mode**).
4. Load the desired files, then click the **Play** button on the "Control" tab to start playback.

## Optional: Pre-baked MUS4 Muscle Mode Export (VMD → MUS4)

> **Note**: This step is only required when using the Pre-baked MUS4 Muscle Mode.

You must first convert VMD animations to MUS4 format (`muscle_anim.bin`) using the Unity editor.

### Prerequisites

- Unity Editor
- [MMD4Mecanim](https://stereoarts.jp/#:~:text=MMD4Mecanim_Beta_20200105.zip) — converts VMD to Unity AnimationClip
- Any Humanoid MMD model

### Steps

1. Place `ExportMuscleAnimation.cs` into your Unity project's `Assets/Editor/` folder
2. Import an MMD model (set to Humanoid Rig) and use MMD4Mecanim to convert a VMD file into an AnimationClip
3. Create an Animator Controller and add the converted AnimationClip as the default state
4. Assign the Animator Controller to the model in the scene
5. **Select the model**, then click `Tools > Export Muscle Animation` in the menu bar
6. The exported file `Assets/muscle_anim.bin` will be created — copy it to the game's `plugin/` directory
