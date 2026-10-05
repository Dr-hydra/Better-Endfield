# Android runtime rebuild — 2026-09-27

## Baseline and scope

Branch: `dev/android-runtime-rebuild-20260927`, created from main commit
`9b1e89599b1d3fbdbf1bcccd2d1b3c48340125c0`. No merge or cherry-pick from the
abandoned `dev/android-audit-refactor-20260926` branch. No CI workflow is included.
The temporary source/SDK retrieval workflows were removed at the owner's request.

This is an implementation and local-regression checkpoint, **not an APK or device
acceptance result**. The requested UI is a touch replacement for PC hotkeys, not a
second settings screen.

## Evidence reviewed, not blindly imported

- Issue #18 and the Android-specific follow-up by yang-34: JNI loader isolation,
  null instances for static IL2CPP fields, and inability to unpause when game-side
  update hooks stop. The reporter's device results belong to their patch.
- Issue #19 by NukumizuKazuhiko: loader isolation, enum boxing/failure propagation,
  Activity view replacement and z-order, and truthful runtime diagnostics.
- Inspected the fork's Android runtime field reader and its build setup; did not
  merge either fork or import either patch wholesale.

References: https://github.com/Dr-hydra/Better-Endfield/issues/18 and
https://github.com/Dr-hydra/Better-Endfield/issues/19.
Android JNI guidance: https://developer.android.com/ndk/guides/jni-tips.

Correction to the preceding audit: `NativeCommandBridge.releaseKeys` already had
an exported JNI implementation in main and the abandoned branch. The issue is
binding/reachability, not a missing C++ function.

## Runtime changes

1. Keep the native library in the game namespace. During load, temporarily pass
   the module loader through the current thread's context loader; JNI_OnLoad
   explicitly loads the bridge class and RegisterNatives binds its methods.
   Restore the original context loader in a finally block. Confirm the bridge
   protocol before declaring Java bootstrap loaded. A private-file flock blocks
   a second library copy from installing duplicate hooks; no second-copy fallback.
2. Wait for the connected IL2CPP domain, required loaded images and an observed
   nativeRender callback. A module whose required images are absent stays pending
   while other eligible modules are considered. Keep actual Start calls serial:
   no abandoned per-module startup threads and no fake promise ordering barrier.
   Startup exceptions and timeouts become observable native status entries.
3. Allow null instances only for static fields. If literal enum boxing returns
   null, resolve its owning enum type and use named System.Enum.Parse; no numeric
   constants, offsets or hash values are guessed. A failed fallback returns null.
   IL2CPP thread scopes detach only attachments they own.
4. Serialize Dobby patch/unpatch operations in a process-wide ownership registry.
   Refuse duplicate targets instead of clobbering another module. Failed removal
   retains ownership; stale handles cannot remove a replacement hook. This is
   conflict detection, **not generic hook chaining**.
5. Keep a first-frame Java hook as a render-thread control pump. A stalled
   gameplay pump can receive camera/unpause requests on this observed thread;
   no timer thread calls Unity object/time APIs. Publish actual camera capability
   and active-state bits separately from configuration and library-loaded state.
6. Android camera readiness depends on successful hooks and a real control entry.
   Failed optional dither does not disable usable camera paths; failed core hooks
   do not become Ready merely because a task was scheduled. PC hook-install policy
   is retained. Android excludes Windows mouse-hook code and uses a touch look pad.
7. Android HUD does not install desktop platform/device spoof hooks. Virtual-key
   pulses are bounded queued edges, distinct from held keys. Lifecycle changes
   clear input and suspend camera state on the next available Unity frame.

The shared command queue and BEM/model/voice algorithms were not comprehensively
redesigned. In particular, detecting a shared target does not make two arbitrary
module detours safely composable. Custom-model/login-model coexistence on every
shared target remains a separate acceptance item, not a claimed fix here.

## Overlay

A 320dp maximum-width graphite tool deck with muted gold accents, custom line
icons and one compact row for HUD/free camera/pause/first person. Camera movement,
look, roll/FOV and keyframe controls are collapsible. Held controls have cancellation
and lifecycle release paths; collapsing/rebuilding also releases held input.

The root is reattached to the Activity decor if replaced and its z-order is checked
only while resumed. Handle position survives Activity recreation. System/cutout
insets bound its placement. The status strip shows native startup/capability data;
configured controls are disabled until available, and active modes use native
feedback rather than optimistic local toggles. Settings changes still require a
game restart for module loading; the panel states that explicitly.

## Local verification

Run `bash native/tests/android_rebuild/run.sh` on Linux with Clang and a JDK.
Tests use production runtime/input/broker code and synthetic IL2CPP/Dobby fixtures;
no test executes a game pointer or patches a live game.

Passed in this workspace:
- Bounded pulse queues, held/released keys, foreground cleanup, look limits and
  render-thread affinity.
- Hook duplicate/foreign/stale-handle rejection, failed removal, install failure
  cleanup and concurrent serialization (mock Dobby).
- Static/null field validation, enum fallback and independent failures, loaded
  image lookup and nested thread-attachment ownership (mock IL2CPP exports).
- Runtime status parsing and configured-versus-ready semantics.
- Explicit JNI registration between two isolated ClassLoaders, using the production
  binding helper on the host JVM with `-Xcheck:jni`.
- Camera hook outcomes using the production camera translation unit: all failed
  is failure; dither-only does not advertise core camera; actual render entry and
  successful state hook support core camera.
- Android-conditional syntax checks for shared camera/UI/actions/model and the
  native bridge. Input, broker and runtime tests also passed AddressSanitizer and
  UndefinedBehaviorSanitizer.
- Java source compilation against Android API 37 with locally generated resource
  constants and minimal libxposed compile-time signatures. This is **not** Gradle
  dependency resolution, Android resource linking, dex generation or APK packaging.

## Still required before release

A real Android/LSPosed game run must validate RegisterNatives with that module
ClassLoader, the nativeRender thread identity, actual Dobby patching and the
client's metadata lifetime rules. The metadata readiness checks do not prove
that every runtime lookup is nonblocking. There is no unsafe attempt to cancel
a blocked native Start call; a stuck `starting` state remains a diagnostic signal.

Test UI-only, camera-only, actions-only, all three together, and combinations
with voice/login/BEM. Test repeated cold launch; pause/unpause and scene changes;
rapid taps and simultaneous movement/look; backgrounding, Activity replacement
and reopening the panel. No APK, ARM64 execution, emulator or physical-device
result is claimed by the local tests. Render-thread recreation, arbitrary dynamic
module unload/hot reload, and every shared-target combination remain unverified.

## Main-based combined delivery

The combined delivery is now one patch against `main@9b1e89599b1d3fbdbf1bcccd2d1b3c48340125c0`.
It includes the runtime/overlay work above and the BEM open/share entry described
in `../bem-open-with/ANDROID_BEM_OPEN_WITH.md`. Do not apply the older rebuild patch first.
The BEM change and the combined patch are delivered as files for manual Git
application; this delivery does not add a CI workflow or publish a new remote commit.
