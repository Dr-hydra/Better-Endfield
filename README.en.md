# Better Endfield

[English](README.en.md) | [简体中文](README.md)

Better Endfield is a modular modding runtime for the *Arknights: Endfield* Windows client. Features such as title screen models & choreography, per-character voice language routing, OmniMix dynamic music replacement, real-time combat stats & rDPS metering, display enhancement (OptiScaler DLSS/FSR/XeSS) and mobile touch HUD emulation are provided as decoupled native DLL modules. The core Host handles dynamic IL2CPP runtime symbol resolution, Hook lifecycle management, configuration persistence, and module discovery.

---

## Architecture

```text
BetterEndfield.exe
  runtime/BetterEndfield.Host.dll
  modules/BetterEndfield.Model.dll
  modules/BetterEndfield.Voice.dll
  modules/BetterEndfield.Music.dll
  modules/BetterEndfield.CombatStats.dll
  modules/BetterEndfield.UiModule.dll
  modules/BetterEndfield.Camera.dll
  loaders/BetterEndfield.Injector.exe
  payloads/xinput1_4.dll
```

- `BetterEndfield.Host.dll`: The in-process host, runtime symbol resolver, and HookBroker.
- `BetterEndfield.Model.dll`: Title screen choreography, custom login characters, asset substitution, and camera animation controls.
- `BetterEndfield.Voice.dll`: Per-character audio routing (Chinese/English/Japanese/Korean), Wwise media redirection, and lip-sync synchronization.
- `BetterEndfield.Music.dll`: OmniMix PCM stream injection, Wwise Audio Input integration, and native game music fallback.
- `BetterEndfield.CombatStats.dll`: Damage number toggles, real-time DirectX combat overlay, team rDPS attribution, and session history recording.
- `BetterEndfield.UiModule.dll`: Native mobile touch UI layout and mouse-to-touch injection.
- `BetterEndfield.Camera.dll`: Free camera, field of view (FOV) scaling, and near-object dither disabling.
- `BetterEndfield.Injector.exe`: Default external loader; Host and all modules load directly from the application folder without modifying game files.
- `payloads/xinput1_4.dll`: Optional XInput DLL hijack loader, deployed to the game directory only upon user confirmation.

---

## Source Directory Layout

```text
ui/BetterEndfield.UI/          WinUI 3 desktop controller application
native/modules/model/          Title screen visual, model, and animation module
native/modules/voice/          Voice language routing and Wwise media module
native/modules/music/          OmniMix music integration module
native/modules/combat_stats/   Combat data metering and in-game DirectX HUD
native/modules/ui/             Mobile touch UI and input injection module
native/modules/camera/         Free camera and viewport enhancement module
native/loaders/injector/       External standalone injector
native/loaders/xinput/         XInput DLL proxy and in-process bootstrap
native/shared/                 Host, public ABI headers, and third-party dependencies
manifests/                     Resource manifests for models, voices, and dependencies
resources/                     Maintenance inputs for voice and catalog generators
installer/                     Inno Setup installer scripts and localization files
scripts/                       Build, manifest generation, and asset scanning scripts
docs/                          Runtime interfaces, reverse engineering notes, and docs
```

---

## Key Features

1. **Title Screen Customization**: Replace the default title screen character with any operator, select custom animations/poses, tweak camera angles, and apply custom theme accent colors.
2. **Voice Language Routing**: Assign custom voice languages (Chinese, English, Japanese, Korean) individually for each character in both combat and story dialogue.
3. **OmniMix Audio Engine**: Dynamically replace in-game music with custom audio sources via OmniMix.
4. **Real-Time Combat Stats Overlay**: High-performance DirectX in-game HUD displaying damage metering, team rDPS contribution, hit counts, crits, and skill breakdown.
5. **Display & Pipeline (OptiScaler)**: Upscaling with DLSS, FSR, or XeSS, frame generation, sharpness control, and free camera adjustments.
6. **Mobile Touch Emulation**: Experience the mobile touch UI on PC with mouse-to-touch conversion (`Ctrl+Alt+T`) and HUD toggling.
7. **Bilingual Localization**: Built-in support for both English (US) and Simplified Chinese with instant, runtime language switching.

---

## Loader Modes

1. **Injector Mode (Recommended)**:
   - Starts the game from the Better Endfield controller or command line.
   - Zero files written to the game directory.
2. **XInput Autostart Mode**:
   - Deploys `xinput1_4.dll` to the game folder for automatic loading when launching the game via official launchers or desktop shortcuts.
   - Clean uninstall supported directly from the Settings page.

---

## Building from Source

### Prerequisites
- Windows 10/11 (x64)
- Visual Studio 2022 / MSBuild with C++ (v143) and .NET 9 SDK
- CMake 3.20+
- Inno Setup 6 (for installer packaging)

### Build Steps
```powershell
# 1. Build all native modules and loaders
cmake -B build -S native -A x64
cmake --build build --config Release

# 2. Build the WinUI 3 Controller
dotnet build ui/BetterEndfield.UI/BetterEndfield.UI.csproj -c Release

# 3. Package the full distribution
.\scripts\BuildBetterEndfield.ps1
```

---

## Disclaimer

Better Endfield is an unofficial, experimental open-source project. It is not affiliated with, endorsed by, or associated with Hypergryph, Mountain Contour, or GRYPHLINE. Please use responsibly and adhere to all relevant terms of service.
