# EF Start Change UI

WinUI 3 controller for the login-scene model replacement mod.

The app writes `EFStartChange.ini` next to `Il2cppDumper.exe`. The injected DLL
reads the file once during initialization, so saved values take effect on the
next launch/injection.

## Controls

- 31 character presets and the full indexed animation list
- Final-action selection with clip duration display
- Initial yaw, turn duration, scale, and forward-lean sample time
- Independent playback speed for all four animation phases
- Native, shared, and two-Playable crossfade loop modes
- Editable loop range and fade time, plus a force-loop option
- Per-character Chinese, English, Japanese, Korean, or global voice routing
- Game/mapper path persistence, log access, launch, and runtime status

## Build

From the repository root:

```powershell
.\scripts\BuildEFStartChange.ps1
```

The combined output is written to `artifacts\EFStartChange-win-x64`.
