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

## Enhancement module

Version 3.0.2 adds `betterendfield.enhancement`, a port of the two desktop
features that do not depend on a keyboard: hide UID/watermark (from
`BetterEndfield.UI`) and disable near-camera character dither (from
`BetterEndfield.Camera`). Hook points are identical to desktop:
`GameObject.SetActive` plus a 2-second `GameObject.Find` sweep driven by
`UIStyleByState.Awake`, `UIStyleByState.UpdateStyle` and
`EventSystem.Update` for the UID panels, and
`CameraMono._ProcessDitherByPitch` followed by the game's own
`CameraMono.ForceClearDither` for the dither. Both switches live on the
Enhancements page and are passed to the native library through
`BETTER_ENDFIELD_ENHANCEMENT_CONFIG`; leaving both off keeps the module out of
the process entirely.

## Android settings UI

The settings screen is split into Model Replacement, Character Voice and
Enhancements pages.
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
- The first launch after selecting a new character/language waits for its
  device-local catalog preparation before arming the native hooks. Missing or
  stale language packages are reported in LSPosed logs; external-source routing
  is still allowed to start when resident catalog generation fails.
- The current build is ARM64-only and targets `com.hypergryph.endfield` user 0.
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
.\tools\android-toolchain\gradle\gradle-8.9\bin\gradle.bat `
  -p android :app:assembleDebug --offline --no-daemon
```

The APK is written to `android/app/build/outputs/apk/debug/app-debug.apk`.

After installing or updating the APK, disable and re-enable the module once in
LSPosed. This makes LSPosed register the module's protected shared-preference
store. Keep the only scope set to `com.hypergryph.endfield`, enable the desired
character rules in the module app, force-stop the game, and launch it again.

The debug build writes a short native diagnostic log to
`/data/user/0/com.hypergryph.endfield/cache/betterendfield-diagnostics.log`.
The native library is linked with 16 KiB ELF LOAD-segment alignment and the APK
is also zip-aligned for Android 16 page-size compatibility.

## LSPosed scope troubleshooting

If Endfield is missing from every module's scope list, open the scope page's
overflow menu, choose `Hide`, and turn off the `Games` filter. LSPosed applies
that filter globally and Android classifies Endfield as a game.

The module declares the same recommended scope in both
`META-INF/xposed/scope.list` and the legacy-compatible `assets/xposed_scope`.
