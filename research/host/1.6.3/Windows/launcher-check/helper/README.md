# LaunchBridge research helper

Standalone Windows x64 console; it is not integrated into BE production. It creates a new parent executable suspended and hidden, never resumes its primary thread, then creates the requested target with `PROC_THREAD_ATTRIBUTE_PARENT_PROCESS` referring to that exact owned parent handle. It waits for the target and preserves its complete DWORD exit code. On normal return or handled failures it terminates only its own suspended parent; it never terminates the target or an existing launcher.

```text
LaunchBridge.exe --parent <absolute parent executable> --game <absolute target executable> -- <target arguments>
```

Target arguments are quoted using Windows CRT rules, including empty strings, Unicode, embedded quotes and trailing backslashes. Target cwd is its executable directory. The calling environment is inherited. The tool neither guesses authentication arguments nor changes game files, registry, installation state or production settings. It does not perform signature verification itself or claim the selected parent passes any game check.

Both process creations use `CREATE_NO_WINDOW`; the owned parent additionally uses `CREATE_SUSPENDED`, and the target additionally uses `EXTENDED_STARTUPINFO_PRESENT`. `CREATE_NO_WINDOW` prevents console probes from attaching to an uninitialized suspended-parent console and is ignored for GUI targets. The helper's own stdout remains available to its caller; child probes should write explicit result files. Without this flag on the target, the first console fixture exited during initialization with `0xC0000142`.

There is no automatic `runas` fallback. Win32 failures, including 740 / elevation required, are reported explicitly. If an authorized experiment requires elevation, invoke the helper elevated deliberately. Forced termination of the helper cannot run C++ cleanup; keep it running until the target has exited.

## Build

From the repository root:

```powershell
& 'C:\Program Files\CMake\bin\cmake.exe' -S research/host/1.6.3/Windows/launcher-check/helper -B build/launcher-helper-1.6.3 -G 'Visual Studio 17 2022' -A x64 '-DCMAKE_GENERATOR_INSTANCE=C:/Program Files (x86)/Microsoft Visual Studio/2022/BuildTools'
& 'C:\Program Files\CMake\bin\cmake.exe' --build build/launcher-helper-1.6.3 --config Release --parallel 2
```

MSVC static CRT; no Host/Injector/game dependency. Output: `build/launcher-helper-1.6.3/Release/LaunchBridge.exe` and `LaunchBridgeFixture.exe`.

## Harmless fixture verification

Requires PowerShell 7 for `ProcessStartInfo.ArgumentList`:

```powershell
& ./research/host/1.6.3/Windows/launcher-check/helper/Test-Fixture.ps1 -BuildDirectory ./build/launcher-helper-1.6.3
```

The test uses only the fixture executable for both parent and child. It does not start the actual official launcher or game. On 2026-10-15 it passed:

- Child observed the exact explicitly selected parent PID, executable and live state.
- Eight argument cases, cwd and environment survived exactly.
- Helper waited for the 1.2-second fixture and preserved exit bits `0xFFFFFFEF` (PowerShell signed exit `-17`).
- Owned parent was gone after successful target exit.
- Inert non-executable target failed with Win32 193 and the owned parent was still cleaned.

Results: `build/launcher-helper-1.6.3/fixture-report.json` and `fixture-validation.json`. This validates the helper mechanics only; actual SDK-checker and gameplay validation is separate.

## Current game validation and convenient entry

On 2026-10-15 the installed, unmodified `OneChannelLC.dll` returned `0` when the target probe used a suspended official `Games.exe` as its parent. The probe's configuration was an explicit fixture reconstructed from that file's valid signing certificate. A separate actual CN 1.6.3 game launch then passed using its own configuration: SDK log time `09:32:34.945`, parent PID `29624`, target PID `7368`, inner result code `0`. The captured game window reached opening-screen resource loading. See the [full investigation](../../../../../../docs/host/research/1.6.3/launcher-check/LAUNCHER_CHECK.md).

Run from the repository root to start the current installed game:

```powershell
& './research/host/1.6.3/Windows/launcher-check/Start-LaunchBridge.ps1' -Mode Game
```

The wrapper requests normal Windows UAC consent, checks the official parent's Authenticode signature, and keeps its console hidden. It defaults to `D:\Hypergryph Launcher\1.6.0\Games.exe` and `G:\Arknights Endfield\Endfield.exe`; override `-ParentPath` or `-GamePath` if the installed paths change. It refuses a second game launch while `Endfield` is running. Run records are under `../runs/`. `-Mode Probe` instead performs the isolated checker experiment and exits.

This still requires the installed official signed executable. The bridge leaves its primary thread suspended and the launcher UI never initializes. The game can be closed normally; the bridge then cleans its owned suspended parent. The full-game trial did not install XInput, alter game files, or change BE/Steam configuration. Server login, BE loading through this entry, and Steam overlay/playtime are not validated by this test.

The actual game trial subsequently exited with code `0`; its bridge log confirms `Suspended parent cleaned: 29624`. The real-process lifecycle is therefore verified through cleanup as well as launch.

API reference: [Microsoft UpdateProcThreadAttribute](https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-updateprocthreadattribute), `PROC_THREAD_ATTRIBUTE_PARENT_PROCESS` (the selected process handle requires `PROCESS_CREATE_PROCESS`).
