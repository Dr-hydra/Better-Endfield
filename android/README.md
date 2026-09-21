# Better Endfield Android

Android ARM64 and LSPosed workspace for Better Endfield. The Android port keeps
the desktop project's module boundary: a small runtime owns IL2CPP access and
independent feature modules implement game behavior.

The packaged Android release is an LSPosed module and requires a working
LSPosed/LSP framework. Installing the APK alone does not inject it into the game.

The first feature module is `voice.character`. It combines two desktop routes:
resident `BEVCAT01` Media-ID replacement through Wwise `CSharp_SetMedia`, and
external-source replacement through `AudioAdapter.PostEventExternal`. External
paths such as `voice/chinese/.../chr_0013_aglina/...wem` are changed to the
selected character's Chinese, English, Japanese, or Korean path after mounting
the corresponding PCK. Unselected characters continue using the game's global
voice language.

## Runtime lifecycle

Loading the native runtime during `Application.attach()` is too early for this
client: `libil2cpp.so` may be mapped while its domain is still unsafe to enter.
The Xposed entry therefore hooks `UnityPlayer.nativeRender()` and loads the
runtime only after the first successful Unity frame. The one-shot render hook is
removed immediately after loading.

On the tested Android 1.4.3 client, all native targets are resolved from IL2CPP
by assembly, namespace, class, method name, and parameter count. Dobby then
patches the resolved ARM64 entry addresses; no game-version offsets are stored.
Aglina and Liino Japanese catalogs generated from the device PCK passed Media
validation and Wwise accepted all 436 routes (188 + 248). Device verification
also confirmed Japanese PCK mounting and successful external-path replacement
for both `chr_0013_aglina` and `chr_0035_liino` while the global language stayed
Chinese. The per-character duration route is active as well: device logs showed
`chr_0013_aglina_sim_talk_lv01_01` changing from 7.939479 seconds (Chinese) to
9.828813 seconds (Japanese), and `chr_0035_liino_sim_talk_lv01_01` from
11.639063 to 12.834063 seconds.

Version 3.0.1 mirrors all 15 desktop voice Hook points. In addition to the core
Media, external-source, package, duration, language, and lip-track hooks, it
includes the four context routes `VoicePlayer._PlayVoice`,
`VoiceSpeakChannelProcessor._PlayVoice`, `VoicePlayer._PlayEvent`, and
`VoiceManager._SpeakNarrative`. `VoiceContext.voiceData` and
`RuntimeVoiceData.speakerChannel` are resolved through IL2CPP metadata rather
than fixed offsets. Per request, this parity update is build-verified but was
not launched for another device test.

Version 3.1.1 keeps the Android voice module unchanged and only aligns the
version with desktop. The desktop 3.1.1 duration fix (hooking
`VoiceUtils._GetVoDurationFromVoData` instead of the `TryGetVoiceDuration`
entry) addresses an x64 IL2CPP delegate-invoke inlining path that the ARM64
build does not exhibit; the `TryGetVoiceDuration` entry hook above is still
observed working on device.

## Login model module parity

Version 3.0.1 also ports `betterendfield.model`. Android does not maintain a
second rewritten implementation: CMake compiles the desktop source file
`native/modules/model/module.cpp` directly into the Android ARM64 library. An
Android Host adapter supplies exact IL2CPP method/field/class resolution,
managed invocation and object helpers, GC handles, Dobby hooks, configuration,
and logging. Model method resolution checks assembly, namespace, class, method
name, parameter types, return type, and parameter count; no fixed game offsets
are introduced.

The complete desktop Hook set is therefore present in the Android binary:

| Group | Managed Hook point |
| --- | --- |
| Model lifecycle | `LoginSceneRoot.OnBindToManager` |
| Resource lifecycle | `StringPathHashBinary.InitMain` |
| Resource lifecycle | `StringPathHashBinary.InitInit` |
| Animation | `LoginSceneAnimCtrl.Tick` |
| Animation | `LoginSceneAnimCtrl.OnRelease` |
| Animation | `LoginSceneAnimCtrl._ChangeToState` |
| Animation | `LoginSceneAnimCtrl._ResetToA1` |
| Animation | `LoginSceneAnimCtrl._PlayA1sAndTriggerNext` |
| Animation | `LoginSceneAnimCtrl._PlayA1A2Impl` |
| Actor capture | `UnityEngine.Object.Internal_CloneSingleWithParent` |
| Logo | `LoginDecorateUI.Tick` |
| Logo | `LoginDecorateUI.OnRelease` |
| Login band | `LoginEnterGamePanel.OnValueChanged` |
| Login band | `UIMaterialAnimation.LateTick` |
| Login band | `CanvasUpdateRegistry.PerformUpdate` |

This is 10 model/animation Hooks, 2 Logo Hooks, and 3 login-band Hooks. The
local Android metadata snapshot contains every declaring class and method name.
The same-source native code and its Android Host adapter compile successfully.
Device testing then confirmed that all three contracts are ready, the original
14 Hooks install, and their runtime paths execute. The `PerformUpdate` prefix
and the neutralized sprite/texture copies (see `docs/GAME_INTERFACES.md`) were
added later for the baked-yellow login assets and are verified on desktop.

On the tested client, `chr_0013_aglina_postmodel(Clone)` loaded and replaced
`SK_actor_female(Clone)`. All four configured animation clips became resident,
the replacement PlayableGraph reached the final looping action, and the
original 11 renderers were hidden while the replacement was active. The Logo
route applied its theme to 9 Graphics, and the login-band route captured 28
Graphics and themed all 27 intended targets without a material-remap failure.
The game process remained alive without a fatal signal, and the replacement
was visually confirmed on the device.

Version 3.0.2 compiles the current desktop model source instead of the 2.3.1
baseline that 3.0.1 shipped. The desktop 3.0.1 "Main path hash recovery" in
`LoadConfiguredAssets` was removed on both platforms after an Android A/B test
on the same game client: with the recovery present, the prefab was loaded
before `InitMainPathHash`, and the clone's Animator reported `avatar=null`,
`human=false`; with the original gate restored, the replacement was verified
working again. The 3.0.1 improvements that remain (Animator enumeration, avatar
copy fallback, full rollback on failure, no actor capture during login scene
release) were verified in the same run.

## Interface, camera and sustained dash

Version 3.3.0 ports `BetterEndfield.UI`, `BetterEndfield.Camera` and
`BetterEndfield.Actions` the same way the login-model module was ported: CMake
compiles the desktop sources
(`native/modules/{ui,camera,actions}/module.cpp`) straight into the Android
ARM64 library. There is no second Android implementation of any of them, and no
game offsets are introduced - every method is still resolved by assembly,
namespace, class, name, parameter types, return type and parameter count.

`native/shared/android_compat` holds everything platform specific:

- `include/Windows.h` -> `android_win32.h`, the Win32 surface those three
  sources use. Most of it is a direct POSIX equivalent (`GetTickCount64`,
  `GetCurrentThreadId`, `GetModuleHandleW`/`GetProcAddress` over
  `libil2cpp.so`, `QueryPerformanceCounter`). `GetForegroundWindow` reports this
  process, because the library is only ever mapped inside the running game, and
  `CaptureStackBackTrace` reports no frames rather than inventing addresses that
  would be read as real GameAssembly offsets.
- `android_virtual_keys.h`, the latch behind `GetAsyncKeyState`. The desktop UI
  and camera modules decide what to do by polling virtual keys; rather than fork
  those code paths for a device with no keyboard, the in-game panel presses the
  keys. `Pulse` auto-releases after 180 ms so one tap is exactly one rising
  edge; `Press`/`Release` back the free-camera movement pad.
- `touch_input_android.cpp`, the stand-in for the desktop mouse-to-touch
  injector. An Android client already has a real Touchscreen device.

Three desktop-only facilities could not be represented honestly and carry an
explicit `#if defined(_WIN32)` at their call site instead: structured exception
handling (`__try`/`__except`, which clang/AArch64 has no equivalent for, so
those calls run unguarded here), the module's own DLL directory (Android reads
the bone-pose directory from the configuration), and the v11 AssetBundle trace
file. The desktop build of all three modules was rebuilt and is unaffected.

`DesktopModule` (`modules/desktop/desktop_module.*`) is the shared
`BE_HostApiV1` adapter all three use, so the login-model adapter's boilerplate
is not copied per module.

### What is wired where

Anything that needs a keypress on desktop is on the in-game panel; everything
else is on the settings screen.

| Feature | Desktop hotkey | Android |
| --- | --- | --- |
| Hide UID watermark | none | Enhancements page |
| Hide all HUD | `0` | switch on the page, button on the panel |
| Remove near-camera dither | none | Enhancements page |
| Free camera | `9` | switch on the page, button on the panel |
| Time freeze | `8` | switch on the page, button on the panel |
| First person | `-` | switch on the page, button on the panel |
| Free-camera movement | arrows, PageUp/PageDown | press-and-hold pad on the panel |
| Movement speed, both FOVs, head/neck options | ini values | sliders and switches on the page |
| Sustained special dash | none | Enhancements page only |

The panel only offers a control whose module was actually configured to load. A
button that presses a key nothing reads is worse than a button that is not
there.

Each module has its own configuration string and its own environment variable
(`BETTER_ENDFIELD_UI_CONFIG`, `BETTER_ENDFIELD_CAMERA_CONFIG`,
`BETTER_ENDFIELD_ACTIONS_CONFIG`). An empty string keeps that module out of the
game process entirely, which is also what the diagnostics page reports.

The Android-only `betterendfield.enhancement` module that 3.0.2 through 3.2.2
shipped is gone: its two switches (hide UID, disable dither) are now served by
the shared desktop sources, and keeping both would have installed two hooks on
`GameObject.SetActive` from two different brokers. The old preference keys are
read once on upgrade so the user's choice carries over.

### Sustained dash bone-pose banks

The sustained dash always drives its looping segment from the bone-pose banks,
not the native-only hold: `external_loop` is written as `true` whenever the
module is configured. `native/modules/actions/assets/pose_*.bin` - the same
files the desktop module reads from beside its DLL - are packaged into the APK
uncompressed and copied into the game's own files directory on first launch,
and the native side is pointed at them through
`BETTER_ENDFIELD_ACTIONS_ASSET_ROOT`.

Character names, the clean-exhaust option and the camera/interface labels use
the desktop UI's wording (洁尔佩塔, 梨诺, 隐藏机甲与光效, 启用时间冻结功能,
视野（FOV）) so the two platforms describe the same switch the same way.

### Contract evidence

Every distinctive contract these three modules need was checked against the
1.5.3 client's own `global-metadata.dat`, pulled from the installed APK:
`UIStyleByState.UpdateStyle`, `CameraUtils.get_cameraManager`,
`CameraManager.AddUICamCullingMaskConfig` / `RemoveUICamCullingMaskConfig`,
`CameraMono._ProcessDitherByPitch` / `ForceClearDither`,
`CameraManager.TailLateTick`, `PlayerController.GetMainCharacter`,
`Entity.get_modelCom`, `BaseModelComponent.GetModelGo`,
`CinemachineBrain.PushStateToUnityCamera`,
`SnapshotCameraController.SetFirstPerson` / `_ShowChar`,
`Animator.GetBoneTransform`, `CharacterAnimationComponent.StartSpDash` /
`PreLateTick` / `InterruptSpDashPerform` / `ForceStopSpDashPerform` and
`CharacterSpecialDashBrain.ShouldInterruptSpDash` are all present.

Note that `CinemachineBrain.PushStateToUnityCamera` is **absent** from the
1.4.3 snapshot under `android/research/device-1.4.3` and present in 1.5.3. Free
camera and first person rewrite the camera pose there, so those two features
need a 1.5-series client; the module reports the contract as unavailable rather
than pretending on an older one.

## Android settings UI

The settings screen is split into Model Replacement, Third-party Models,
Character Voice, Enhancements and Diagnostics pages.
It uses a dependency-free native Android dark card layout with the desktop
amber accent, a segmented page switcher, and the existing desktop
`Assets/shared/gilberta.png` artwork as both the launcher icon and settings
header mark.
The model page reads the generated Android `character-presets.json` and
`character-names.json` resources and currently exposes 32 replacement models and
4,210 final actions, plus final-action looping, model scale, and the desktop
Logo/login-band theme switch. Saving a preset serializes the same schema-5
model configuration consumed by the desktop module. The voice page retains the
per-character language table and Android catalog materializer workflow.

The model page exposes the desktop loop modes: native LoopTime, forced looping,
and dual-Playable crossfade with editable loop start, loop end, and blend
duration. Logo and login-band colors can be selected from swatches or entered
as an exact `#RRGGBB` value.

## Character rules and embedded comparison table

The Android settings page exposes every character present in the desktop
short-voice table, plus the desktop-style default rule. Each row supports
Chinese, English, Japanese, Korean, or Follow Global. The generated files under
`android/resources` are copied into the APK at build time. Model bundle hashes
come from the Android manifest, while voice route IDs may be shared with the
desktop table only after the current `AudioDialog` and device PCK indexes agree.
The current table contains 32 model presets and 132 character/language catalog
entries.

## Android catalog materialization

The desktop app already generates `BEVCAT01` files automatically when its
configuration is saved. Android now has a separate on-device materializer with
the same catalog format, route deduplication, PCK header/media parsing, VFS
decryption, target-Media validation, atomic output, and cache validation. It
runs in the target game process before the native runtime is loaded.

The route pairs are stored in the validated Android
`voice-catalog-index.json`. Only the payload lookup differs: Windows validates
the exact desktop package descriptor, while Android extracts the language VFS
partition from that descriptor, scans the target app's downloaded CHKs, and
selects the current device package that contains every required target Media
ID. This is necessary because Windows and Android PCK filenames, sizes, hashes,
and WEM payloads are not interchangeable.

Generated catalogs are private to the game at
`files/betterendfield/catalog`. They contain only the selected routes and are
rebuilt when the embedded table or device PCK identity changes. The APK does
not contain PCK, BNK, or WEM payloads. A selected language must first be
downloaded through the game.

For offline research, the existing build script still accepts
`--package-path` for an explicitly copied Android CHK:

```powershell
py -3 .\scripts\BuildVoiceCatalog.py `
  --game-path .\android\research\device-1.4.3\vfs `
  --package-path .\android\research\device-1.4.3\vfs\japanese-main.chk `
  --language Japanese `
  --character-id chr_0013_aglina `
  --output .\android\research\device-1.4.3\catalog\voice.japanese.chr_0013_aglina.becat
```

Research catalogs and source PCK/CHK files stay under ignored
`android/research` paths. They must not be embedded in the APK or distributed.

## Current limitations

- Narrative lip-sync routing is ported through `_PlayLipSyncTrack`,
  `GetLipSyncTrackPath`, and `TryLoadTrack`, including a global-language
  fallback when the selected-language track is unavailable. The Android 1.4.3 contracts
  resolve and hook successfully, but a suitable narrative scene has not yet
  been available for behavioral verification.
- Media routes are reasserted after later game `SetMedia` and `UnsetMedia`
  calls, and the active global PCK is preserved while mounting the auxiliary
  Japanese package.
- Rule changes require force-stopping and restarting the game.
- The in-game panel's controls are wired: hide-HUD, free camera, time freeze,
  first person and the free-camera movement pad all press the virtual keys the
  ported desktop modules poll. BEM hot switching is still not connected.
- The three ported modules are build-verified for ARM64 and their settings and
  panel were exercised on a local emulator. The emulator has no LSPosed, so
  their in-game behaviour has not been run against the injected client; the
  contract evidence above is a metadata check, not a device test.
- The first launch after selecting a new character/language waits for its
  device-local catalog preparation before arming the native hooks. Missing or
  stale language packages are reported in LSPosed logs; external-source routing
  is still allowed to start when resident catalog generation fails.
- The current build is ARM64-only and runs in user 0.
- The target package is not hard-coded. The module attaches to whatever the
  LSPosed scope names, as long as it is that app's own main process, and then
  requires evidence before doing anything: `UnityPlayer.nativeRender` must
  exist, and every native hook is resolved by name through `libil2cpp.so`'s
  exports rather than by offset. So 官服 (`com.hypergryph.endfield`), 国际服
  (`com.gryphline.endfield.gp`) and channel builds such as the bilibili one are
  all supported without a per-variant build, and a client update does not
  invalidate the hooks unless the managed type or method names themselves
  change. Only the first two are declared in `xposed_scope`, because the
  bilibili package name has no authoritative source; tick it by hand.
- Model, animation, Logo, and login-band behavior is verified on the connected
  Android client with `chr_0013_aglina` and its default final action. Other
  character/action combinations remain data-driven but have not each been
  exercised individually.

## Requirements

- JDK 17 or newer
- Android SDK platform 35 and build-tools 35.0.0
- Android NDK 27.2.12479018
- CMake 3.22.1

The repository-local toolchain is under `tools/android-toolchain`. Build without
network access from the repository root:

```powershell
.\android\gradlew.bat -p android :app:assembleDebug --offline --no-daemon
```

The APK is written to `android/app/build/outputs/apk/debug/app-debug.apk`.

Version 3.3.0 dropped the legacy API 82 build variant. libxposed API 102 is the
only framework entry point, so there are no longer two flavors and `minSdk` is
29, the version that service requires.

The settings page uses bottom navigation on phones and a navigation rail at
720 dp and above, with a bounded content width and system-bar insets. The BEM
entry is separate from login-model settings. The enhancement page owns the
overlay switch and preview; the BEM page only manages packages. The framework
entry attaches a collapsed BE icon directly to the scoped Unity application's
Activity. Tapping it expands the panel, dragging repositions it, and the panel
follows pause/resume/destroy. It does not require
SYSTEM_ALERT_WINDOW permission or a foreground service. After first enabling
the option, restart the scoped game. Panel display in an injected game still
requires device verification; an ordinary emulator can verify the preview.

After installing or updating the APK, disable and re-enable the module once in
LSPosed. This makes LSPosed register the module's protected shared-preference
store. Set the scope to the Endfield build you actually play — the two
confirmed ids are pre-selected, any other channel build has to be ticked
manually — enable the desired character rules in the module app, force-stop the
game, and launch it again. Scoping the module to unrelated apps is harmless but
pointless: they fail the Unity check and are left alone.

The debug build writes a short native diagnostic log to
`/data/user/0/<game package>/cache/betterendfield-diagnostics.log`.
The native library is linked with 16 KiB ELF LOAD-segment alignment and the APK
is also zip-aligned for Android 16 page-size compatibility.

## LSPosed scope troubleshooting

Third-party BEM packages are managed from the separate `第三方模型` page.
Import validates every appearance and preserves the original package bytes;
texture conversion is optional. If textures look wrong in game, choose
`转换手机纹理` on that package's management card. A successful conversion
publishes a new generation while preserving its enabled state and selected
appearance. Failure or cancellation leaves the active package intact. Packages
without verified normal-map encoding can still be imported, but conversion
requires that metadata. Restart the game after changing packages or appearances.

If Endfield is missing from every module's scope list, open the scope page's
overflow menu, choose `Hide`, and turn off the `Games` filter. LSPosed applies
that filter globally and Android classifies Endfield as a game.

The module declares its recommended scope in `META-INF/xposed/scope.list`.
