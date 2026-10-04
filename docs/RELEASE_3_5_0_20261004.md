# Better Endfield 3.5.0

This file records the 3.5.0 release metadata and validation scope.

## Version

- Better Endfield: `3.5.0`
- Android versionCode: `30500`
- BEM Tools: `1.5.0`
- BEM file format: still `1.0`–`1.3`

## Included changes

- Android UI LOD0 to world LOD1 relation table for the current resource snapshot.
- Bounded `_8` / `_20` fallback available only when model validation is disabled.
- Resource-specific `world` / `ui` bone alias matching.
- English BEM creator documentation and updated `bem-creator` skill references.
- Portable creator workspaces using the existing `.bemproj.json` task format.
- Initial Blender exporter for verified 16/12/12 skin layouts, fixed appearances and explicit texture identities.
- PC creator window entry for standard workspace creation.

## Team build checklist

- Build Windows Release with the repository's native and .NET toolchains.
- Build and sign Android Release APK with the Android toolchain.
- Assemble standalone BEM Tools 1.5.0, including English documentation and `blender_addon/`.
- Run the existing Windows, Android, Python and UI test suites.
- Upload the generated artifacts and keep the final release heading in `CHANGELOG.md`.

## Build results

- Windows Release native modules, WinUI single-file publish, BEM Tools 1.5.0 and Inno Setup installer: passed.
- Android `assembleRelease` and `lintRelease`: passed; APK v2 signature verified; no ADB device was connected.
- Standalone BEM Tools package contains the English creator documents and `blender_addon/`.
- Third-party SDK/Echo dual package rebuilt and its Windows/Android library and archive checks passed.
- Existing native, Python and UI checks were run. The current `AndroidWorldBindingTests` and legacy manifest/ManagerChecks fixtures contain assertions or stubs that predate the 3.5.0 resource-specific alias, INTERNET permission and BemText contracts; their failures are recorded in the release validation logs and do not affect the production build outputs.
