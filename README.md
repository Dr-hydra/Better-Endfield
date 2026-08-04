# EF Start Change

EF Start Change is an experimental Windows mod controller for the Arknights:
Endfield login scene. It combines a WinUI 3 configuration app with a native
IL2CPP injector.

> Preview software. The native hooks are tied to a verified game build and are
> automatically disabled when their entry signatures do not match.

## Preview features

- 31 character postmodels and 4,058 resolved animation clips.
- Per-stage animation speed, facing, scale, forward-lean sampling, and loop
  controls.
- Native loop, forced loop, and dual-Playable crossfade modes.
- Per-character voice-language routing for Chinese, English, Japanese, and
  Korean voice packs.
- `speakerChannel` discovery in `IL2CPPDump_Log.txt` for correcting voice rules.
- A self-contained WinUI 3 controller that saves `EFStartChange.ini` and starts
  the injector.

The preview was verified against `GameAssembly.dll` SHA-256:

```text
0C5573679BC6DEC2D068A14335466DB7CCF20AF9BAE2B983FB9D45677D80FFCE
```

## Use

1. Download and extract the Windows x64 archive from Releases.
2. Run `EFStartChange.exe`.
3. Select `Endfield.exe` and the bundled `Il2cppDumper.exe` if they were not
   detected automatically.
4. Choose a postmodel, final animation, and the desired animation parameters.
5. For character voice routing, enable the switch, choose a language, and click
   **Add / update current character**.
6. Save and launch. Configuration changes are read on the next injection.

Voice rules are stored in this form:

```ini
voice_router_enabled=true
voice_language_rules=aglina:Japanese,chen:Chinese,*:FollowGlobal
```

If a generated character alias does not match the game data, look for
`[voice-lang] observed speakerChannel=...` in `IL2CPPDump_Log.txt` and use that
value on the left side of the rule.

The first preview switches the game's shared voice-language pack immediately
before each utterance is submitted. Simultaneous voices therefore still share
the most recently selected global language.

## Build

Requirements:

- Windows 10 or 11 x64
- Visual Studio 2022 with the MSVC v143 C++ toolset and Windows SDK
- .NET SDK 9.0

Build the native injector and self-contained WinUI package:

```powershell
pwsh -File .\scripts\BuildEFStartChange.ps1
```

Output is written to `artifacts/EFStartChange-win-x64`.

## Source layout

- `src/EFStartChange.UI` — WinUI 3 controller and checked-in character catalog.
- `tools/IL2CPP-Dumper-src/Dump` — injected runtime and hooks.
- `tools/IL2CPP-Dumper-src/Mapper` — self-elevating launcher/manual mapper.
- `tools/IL2CPP-Dumper-src/third_party/minhook` — MinHook dependency and license.
- `scripts/BuildEFStartChange.ps1` — reproducible release build.

## Disclaimer

This project is unofficial and is not affiliated with GRYPHLINE or Hypergryph.
It modifies a running game process and may stop working after any game update.
Use it at your own risk and comply with the game's terms of service. No game
assets, extracted tables, or decompiled assemblies are included in this
repository or its release archive.
