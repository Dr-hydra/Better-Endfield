# Android runtime audit — 2026-09-26

## Scope

This audit covers the Android/LSPosed launch path, the in-game control overlay,
the shared camera module, and the sustained-dash module. GitHub issue #18 and its
2026-09-26 Android 16 / game 1.5.3 reproduction report were used as the field
evidence.

## Findings

1. `native_bridge.cpp` started every module serially. A camera metadata lookup or
   native hook that stalled therefore prevented every later module from starting;
   the actions module could produce no startup log and no sustained dash.
2. `RuntimeBootstrap` installed the Unity first-frame trigger only once. If
   `UnityPlayer` had not been loaded at `Application.attach()`, the native
   runtime was never loaded for that process.
3. The camera module treated `_ProcessDitherByPitch` as its mandatory hook and
   installed it before the late-tick and Cinemachine hooks. A client revision
   could therefore disable the usable free-camera/first-person path or stall
   initialization before the Android input thread was started.
4. The overlay performed a one-shot Unity class probe, used legacy-only inset
   access on newer edge-to-edge devices, and reset its handle position whenever
   Unity recreated the Activity.
5. Release builds did not publish the native diagnostics path, making Android
   startup failures difficult to distinguish from a module that was never
   entered when logd rate limiting was active.

## Changes in this branch

- Start each configured native module from its own IL2CPP-attached worker. A
  blocked camera startup no longer prevents actions or other modules from
  entering. Actions are scheduled before the camera worker.
- Wait briefly for lazy Unity Java glue before installing the `nativeRender`
  first-frame hook.
- Make the camera dither contract optional for the core camera readiness check.
  On Android its hook is installed off the initialization path, after the
  late-tick/Cinemachine hooks and input thread are available.
- Keep the native diagnostics file enabled for release builds.
- Defer overlay setup to Activity resume, support Android 11+ system-bar and
  cutout insets, preserve the handle position across Activity recreation, and
  keep held virtual keys released on lifecycle transitions.
- Refactor the overlay around its actual role as a touch replacement for PC
  hotkeys: independent actions use two-column, large touch cards with their
  matching key labels, while free-camera movement uses a dedicated held-key
  direction pad. The panel now communicates “tap to trigger” versus “hold to
  keep pressed”, and adds a compact status header, touch feedback, and open/
  close animation instead of presenting as a settings page.
- Add startup milestone logs for camera and actions contract/hook phases.

## Verification

- `git diff --check` passes.
- `native_bridge.cpp` passes a C++20 syntax check with a JNI stub and the
  Android compatibility include paths.
- The repository Gradle wrapper could not download Gradle in this environment,
  so an APK/device test remains required on an Android 16 / game 1.5.3 device.
