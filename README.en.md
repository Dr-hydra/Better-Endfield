# Better Endfield

[English](README.en.md) | [简体中文](README.md)

Better Endfield is an open-source modular toolkit for *Arknights: Endfield*. It provides third-party character models, MMD playback, camera and UI controls, per-character voice languages, title-screen customization, and PC combat/gacha tools. Windows and Android share the main native feature sources; standard BEM model packages and MMD works can be used on both platforms. An experimental loader also supports third-party native modules and a web UI container.

[Download](https://github.com/Dr-hydra/Better-Endfield/releases/latest) · [Release notes](CHANGELOG.md) · [Android setup/build guide](android/README.md) · [BEM creator guide](docs/custom_model/BEM_CREATOR_GUIDE.en.md) · [Module developer guide](docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md)

The current version is **3.5.3**, with standalone BEM Tools **1.5.2**. The tool ZIP includes the double-clickable `BetterEndfield.BemTools.exe` creator GUI. Version 3.5.3 adds a Windows [Steam CN launch preview](docs/host/STEAM_CN_LAUNCH.md) and prevents duplicate injection alongside a local XInput proxy. Steam integration still awaits complete metadata and real-client testing. BEM 1.4, weapon/ultimate resources, model hot switching, Android PCUI input and desktop DPI placement carry forward from 3.5.2.

## Feature overview

Supported means an implementation and controls exist; it does not mean every character, work, device or game version has been tested in game.

| Feature | Windows x64 | Android ARM64 | Details |
| --- | --- | --- | --- |
| Third-party models (BEM) | Supported | Supported | Same standard package; import/update, multiple installed packages, per-character activation, appearance and component options |
| BEM 1.4 resources and forms | Supported (3.5.2) | Supported (3.5.2) | Explicit resource targets, static meshes and LOD/platform declarations; weapons and ultimate-form resources, with 1.0–1.3 compatibility |
| Third-party native modules and web UI | Experimental | Experimental | Unified under Enhancement; game Host loads DLL/SO, with author-defined pages and features |
| Model hot switching | Experimental | Experimental | Enable before game startup; selections apply on normal game resource reloads |
| Model loading optimization | Experimental | Experimental | Fewer decode copies and reuse of equivalent textures within one build; no quality reduction |
| Title-screen models, animation and colors | Supported | Supported | Character/action selection, stage speeds, scale, turning, looping and crossfades |
| Per-character voice languages | Supported | Supported | Chinese, English, Japanese and Korean, with optional story voice and lip-sync routing |
| Free camera, first person and world pause | Supported | Supported | Independent controls, FOV, camera motion, keyframes, VMD cameras and near-camera dither handling |
| Global FOV and character-follow free camera | Supported (3.4.2) | Supported (3.4.2) | Override the ordinary main camera FOV; translate free camera with the character while retaining manual offsets |
| MMD library and multiple dancers | Supported | Supported | Up to four dancers, motion/face/camera/local music, timeline and cloth options |
| UID/HUD visibility and UI layouts | Supported | Supported | Touch layout and mouse-to-touch on PC; PC-style layout on Android |
| Sustained special dash | Supported | Supported | Individual Gilberta/Liino toggles; optional Liino mech/VFX hiding |
| Combat stats and rDPS | Supported | — | In-game overlay, character/skill rankings, timelines, history filters and web-analysis entry |
| Gacha records | Supported | — | Game sync, local statistics, JSON import/export and optional cloud sharing |
| OmniMix music integration | Supported | — | External backend audio routed into Wwise, with native-music fallback |
| OptiScaler display enhancements | Supported | — | DLSS/FSR/XeSS upscaling, frame generation and sharpening; availability depends on hardware/backend/rendering path |

The Windows app includes Chinese/English localization, light/dark themes, runtime status and logs, game-path discovery, launch arguments, shortcuts, update checks and XInput autostart management. Android has automatically saved category pages, package import and a collapsible in-game control deck.

## Install and start

### Windows

Download the Windows installer from [Releases](https://github.com/Dr-hydra/Better-Endfield/releases), open Better Endfield, check the game path, enable the features you need and launch the game. Windows 10/11 x64 is required.

- **Built-in injector:** the default mode. Better Endfield launches the game and loads Host/modules from the application directory, without deploying Better Endfield runtime files into the game directory.
- **XInput autostart:** optionally install the `xinput1_4.dll` proxy in Settings to load through the official launcher or a game shortcut. Installation/removal checks ownership and does not overwrite another tool's existing file.

OptiScaler is a separate deployment feature that writes to the game directory and applies on the next launch. Launch arguments also apply to generated one-click shortcuts.

### Android

The APK is an **LSPosed/libxposed API 102 module**, requiring Android 10+, ARM64 and a compatible framework that can inject the target game. Installing the APK alone does not activate game features.

Enable the module in your framework, scope it to the Endfield client you actually use, configure features in the module app, then fully stop and restart the game. The in-game deck is attached to the target Activity and does not require overlay permission. Use it for camera, pause, first-person and MMD controls. See the [Android README](android/README.md) for setup, scope troubleshooting and build requirements.

## BEM models and creator tools

**Players only need a `.bem` package.** No Python, source Mod framework, character database or hand-written runtime configuration is required. A package targets one character and can contain fixed appearances, configurable components and material/texture replacements. Multiple packages can be installed for one character; activating one disables the others. Updates preserve valid saved selections.

Both platforms use the same BEM parser/assembly core and support BEM 1.0–1.4. **Version 3.5.2 adds BEM 1.4 resource targets and static meshes**: packages can declare platform, LOD, resource paths and donor relationships for weapons and ultimate forms. Older 1.0–1.3 packages keep their existing behavior. Source hotkey scripts, arbitrary GUI expressions and arbitrary shaders are not executed.

Normal selections apply after a game restart. With experimental hot switching enabled, package/component/1.3 parameter changes apply when the game **normally reloads the resource**, such as changing the team or reopening character details. Slider dragging does not instantly rebuild an already displayed mesh. Importing, replacing package files and deleting packages should still be done with the game closed.

The in-game model overlay supports character filtering, disabling all models, one active package per character, appearances, component choices and shape parameters, with expandable details. On Windows, the default show/hide key is the main keyboard `=` without Shift; its settings are on the third-party models page. On Android, use the model tab on the left side of the overlay. Both use the existing library without making extra model or texture copies.

Hot switching and loading optimization are independent and disabled by default. Hot switching retains original model resources for cache rebuilding, increasing memory use. Loading optimization reduces decode copies and duplicate resources; it does not guarantee a lower in-game VRAM peak on every device.

Android provides optional mobile texture conversion for packages with incorrect-looking textures. Success publishes a new generation and preserves selections; failure/cancellation keeps the old package. Conversion requires verified normal-map encoding metadata. A portable model format does not guarantee that desktop texture formats display correctly on every mobile GPU.

**Creator tools** offer a standalone GUI, the main application's “BEM Creator Tools…” entry and CLI, directory/ZIP/RAR/7z inputs, conversion reports, validation, saved `.bemproj.json` tasks, portable workspaces and reproducible builds. BEM Tools 1.5.2 supports BEM 1.4 while keeping the legacy Blender/EFMI character pipeline boundaries; authors must declare resource targets, LOD, platform and donor evidence. It does not reconstruct arbitrary source GUIs or guess vertex correspondence.

```powershell
BetterEndfield.BemConverter.exe new-project editable/project.json --mode pack -o character.bemproj.json
BetterEndfield.BemConverter.exe build character.bemproj.json
```

Both platforms also have an off-by-default experimental option to disable model validation. It bypasses compatibility/policy checks while retaining the decoding and representation requirements needed to read the file; it does not add new encodings. Developer tests may render incorrectly or crash the game.

- [Creator guide](docs/custom_model/BEM_CREATOR_GUIDE.en.md): tools, workflows, testing and distribution
- [Format specification, 1.0–1.4](docs/custom_model/BEM_FORMAT_SPEC.en.md)
- [Runtime behavior and compatibility](docs/custom_model/BEM_RUNTIME_COMPATIBILITY.en.md)
- [Converting other Mods](docs/custom_model/BEM_SOURCE_MOD_CONVERSION.en.md)
- [Runnable shape-slider example](tools/CustomModel/examples/body-slider/)
- [Experimental hot-switch/loading behavior](docs/workspace/releases/3.4.1/RELEASE_3_4_1.md)

## Third-party modules (experimental)

Both apps have a dedicated Third-party Modules entry for importing author-supplied ZIP packages, managing enabled state/order, and opening each module's page; newly imported modules start disabled. A package contains `module.json`, a Windows x64 DLL / Android ARM64 SO for its target platforms, and optional HTML/CSS/JS. It can target one platform or provide only a web UI. Modules are managed separately from `.bem` model packages.

The **Host inside the game process** loads native code. Windows uses WebView2 and Android uses WebView for author-written pages. A generic bridge reads/saves the module's JSON configuration, sends business messages, receives results and queries status. Configuration can be saved without a game connection; messages to a native module require a running Host connection.

The loader provides the entry ABI, lifecycle, configuration and message transport. Authors maintain their own game function tables, version adaptation, calling threads, feature implementation and restoration. They can resolve interfaces themselves or optionally use available Host helpers; initialization does not require IL2CPP readiness. Host callbacks run on a worker, not the Unity main thread. Configuration and activation of already-loaded modules can update at runtime. Native libraries remain resident; binary updates, removal and order changes require a game restart rather than arbitrary hot unloading.

Updating/removing a package retires its old installation generation while retaining the old directory; Android also retains its old ZIP. There is currently no automatic garbage collection or deletion after restart, so an active library or page can continue reading its files.

Android phone navigation scrolls horizontally, while large screens retain a side rail. Each module's web UI opens in its own screen instead of a small management card. Bundle static resources for portable pages: Android serves only package-local offline resources; authors can handle network functionality in their native module.

Shared Hooks are optional: chain participants share a target and call `next`. Existing exclusive Hooks still report conflicts and are not automatically converted into chains. Participants coordinate function signatures, arguments/results and feature interactions. Successful loading does not establish compatibility with every built-in or third-party module.

Start with the [module creator guide](docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md), then [ThirdPartyModule.h](native/shared/include/BetterEndfield/ThirdPartyModule.h), [HookChain.h](native/shared/include/BetterEndfield/HookChain.h) and the [Echo example](tools/ThirdPartyModules/echo/). Echo builds for Windows/Android; messaging and lifecycle have isolated regression coverage. Authors still test and document their real game modules' behavior, stability and compatibility.

## Cameras, first person and MMD

Free camera offers position/orientation, roll/FOV, mouse rotation, orbit/dolly/crane/truck motion, saved keyframe paths and VMD cameras. World pause is independent of free camera. First person includes head hiding, neck-hole filling, side-view limits and smooth turning. Windows controls support the main keyboard, numpad, mouse and configurable key combinations; Android uses the in-game deck.

The global FOV setting added in 3.4.2 affects the ordinary main camera; free camera, first person and imported cameras keep their own FOV. Character follow translates free camera without changing orientation/manual offsets, reanchors after character changes and teleports, and pauses during camera-motion playback. [Implementation boundaries](docs/camera/research/1.5.3/fov-follow/CAMERA_FOV_FOLLOW_IMPLEMENTATION.md)

First-person hair removal in 3.4.2 classifies **live bone weights and actual drawn geometry**, rather than hiding an entire mixed head/clothing mesh. BEM replacement geometry can supply CPU data. Unknown characters use generic live-skeleton fallback without a second mandatory character catalog. Separate parts attempt shadow-only rendering; mixed parts retain full shadow geometry. Uncertain parts remain when no geometry source is available. Shadows and residual hair still need in-game validation. [Implementation and verification scope](docs/camera/research/1.5.3/first-person-geometry/BEM_HAIR_SHADOW_IMPLEMENTATION.md)

The MMD library groups motion, face, camera and local music, with `set.ini` work descriptions. It includes play/pause/stop, seeking, loops, game/free/VMD camera modes, up to four dancers, cloth physics and experimental terrain fitting. Windows local music does not require OmniMix. Android plays local media alongside game BGM; turn down the game's BGM when needed. Body/face, cloth and terrain capabilities depend on the client interfaces; successful builds/imports do not establish in-game visual correctness. [Cross-platform integration record](docs/camera/research/1.5.3/android-mmd/ANDROID_CAMERA_MMD.md)

## Other modules

**Title screen:** replace the login actor, choose each character's sitting chain and final action, adjust scale/initial angle/turning/stage speeds, and use native loops, forced loops or dual-Playable blending. Logo and login-band colors can be changed independently. There are 33 characters and 4,262 final-action entries; 3.4.2 adds the missing Purrche resources. [New-character resource fix](docs/model/research/1.5.3/purrche-title/TITLE_MODEL_PURRCHE_FIX.md)

**Voice:** set Chinese/English/Japanese/Korean individually while keeping the game's global voice language. Optional routing covers story dialogue, duration and lip-sync. Download the required language pack in the game first; catalogs are generated from local game resources. PCK/BNK/WEM audio is not shipped. [Voice routing](docs/voice/VOICE_CUSTOM_LANGUAGE_SYSTEM.md)

**UI and actions:** hide UID and toggle HUD through a hotkey/deck. PC touch layout with mouse-to-touch conversion is intended for streaming/touch devices; the default conversion toggle is `Ctrl+Alt+T`. Android can enable PC-style layout, preferably with a keyboard/controller. Layout is independent of account-platform identity. Sustained special dash currently targets Gilberta and Liino, with an additional Liino mech/VFX option. [Action module](native/modules/actions/README.md)

**PC combat data:** manual and automatic dungeon sessions, damage-number visibility, damage/DPS overlays, skill categories, character/skill timelines, history filtering and web analysis. rDPS uses validated Buff/skill semantics bundled with the software to reattribute confirmed teammate contributions; unverified candidates do not participate. Records stay local unless the user opens web analysis. [Combat contracts](docs/combat_stats/COMBAT_RUNTIME_CONTRACTS.md)

**PC gacha:** opt-in sync through the game connection, pool statistics, six-star/UP results, pity and free pulls, JSON import/export and user-initiated cloud sharing. [Web functionality](web/docs/GACHA_WEB_PLAN.md)

**PC music/display:** OmniMix uses the user's existing backend without copying its program or library. Login, main/base and gameplay music can be replaced separately, with native fallback on stream failure. OptiScaler supplies upscaling, frame generation and sharpening according to the actual GPU/backend. [OmniMix integration](docs/music/OMNIMIX_INTEGRATION_HANDOFF.md) · [Display pipeline](docs/ui/DISPLAY_PIPELINE.md)

## Architecture and compatibility

Windows Host loads individual native feature DLLs. Android compiles shared feature sources into the game runtime with platform adapters. Host owns discovery, lifecycle, configuration, dynamic IL2CPP resolution and Hook management. Built-in modules identify interfaces through assembly/type/method/signature/field descriptions instead of a single official-client address set or `GameAssembly.dll` identity whitelist.

This enables shared code across clients, but **does not guarantee automatic compatibility with every game update**. Method signatures, assets, renderer layouts and device interfaces may still require adaptation. Missing built-in contracts disable the affected capability and produce logs. Scope Android to the actual client; world/detail/title and desktop/mobile resource layouts are not assumed identical.

Windows stores its main configuration at `%LocalAppData%\BetterEndfield\BetterEndfield.ini`, UI settings in `ui-settings.json`, and BEM packages/state in `catalog\custom-model`. Android publishes settings through the framework and copies resources into the game's private storage. Resource/voice indexes ship with the software; original game payloads are read locally as needed and are not distributed.

| Directory | Contents |
| --- | --- |
| `ui/BetterEndfield.UI/` | WinUI desktop controller and assets |
| `native/modules/` | Model, BEM, voice, music, combat, UI, camera, actions and gacha modules |
| `native/shared/` | Host, public C ABI, platform compatibility and native dependencies |
| `native/loaders/` | Windows injector and XInput proxy |
| `android/` | Android app, framework entry, runtime and in-game deck |
| `tools/CustomModel/` | BEM exports, conversion, validation, projects and character profiles |
| `manifests/`, `resources/` | Model/action/voice/combat indexes and generation inputs |
| `web/` | Combat/gacha web-analysis and sharing sources |
| `scripts/`, `docs/` | Build/resource scripts, interfaces and research records |

See [GAME_INTERFACES.md](docs/host/GAME_INTERFACES.md) for internal protocols. Research directories and historical records do not represent shipped features.

## Build from source

Windows requires Visual Studio 2022 C++ tools, CMake, .NET SDK 9 and PowerShell; installer packaging also requires Inno Setup 6. BEM tool build dependencies are in [`requirements-build.txt`](tools/CustomModel/requirements-build.txt).

```powershell
pwsh -File .\scripts\BuildBetterEndfield.ps1
pwsh -File .\scripts\BuildInstaller.ps1
pwsh -File .\scripts\BuildBemTools.ps1
```

Android requires JDK 17+, SDK, NDK and CMake; use the versions declared in [`android/app/build.gradle.kts`](android/app/build.gradle.kts). With the toolchain configured:

```powershell
.\android\gradlew.bat -p android :app:assembleRelease --no-daemon
```

Build scripts consume generated repository indexes. Refresh affected data after game updates; local research outputs and original game payloads must not be packaged as release assets.

## License

Better Endfield is licensed under [AGPL-3.0-only](LICENSE). It is an independent, unofficial project and is not affiliated with the game developers/publishers. Dependencies/references including MinHook, Dobby, EIEM and 7-Zip retain their own licenses and attribution. Creators are responsible for distribution rights to their models, motion, audio and modules.

Behavior depends on the client, device and imported content. Review the applicable service rules and account/client risks before use. Disable affected features when a game update breaks a contract and wait for adaptation.
