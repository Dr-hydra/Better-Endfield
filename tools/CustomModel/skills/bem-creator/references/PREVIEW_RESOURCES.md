# Optional preview resources / 可选预览资源

DSH can fill original character components from the resource release. It needs a preview
pack only, not a core pack. These resources never become part of an authored BEM.

## Choose for the viewing environment

DSH can also run on Android. Its Node server and browser can be on different machines.
The server OS, CPU architecture and BEM target alone do not determine the texture pack.
Let the user select BC7 (Windows) or ASTC (Android) in the resource panel, or explicitly
pass platform to the bem tool. Do not silently download or change formats.

| Viewing environment | Suggested preview pack |
| --- | --- |
| Resource panel reports ASTC support | ASTC (platform=android) avoids BC decoding; BC7 remains an option |
| Browser lacks ASTC, including some Android browsers | BC7 (platform=windows), decoded by the bundled worker |
| Capability unknown, or browser is remote | Check the resource panel or ask which viewing device is used; do not infer from process.platform |
| Native Windows application | Windows / BC7 |
| Native Android application | Android / ASTC |

Windows / Android 是发布标签。DSH 不按自身系统强制选包；安卓上的 DSH 也能选 BC7，
支持 ASTC 的浏览器可选 ASTC。以用户选择和实际浏览器能力为准。

Core resources still depend on the native application platform and actual game baseline.
Do not apply DSH's texture choice to a native application's core pack.

## Reuse local resources before downloading

1. Run bem with action=resources. Reuse existing extracted resources (including an installed
   Better Endfield resources directory) if they match the user's selected texture format.
2. Accept a shared resources root, or the exact version directory containing manifest.json
   and characters.json. The shared root honors state.json's preview.windows / preview.android
   pin; otherwise it selects the highest numeric four-part version for the selected format.
   A missing pinned version is an error, not a silent upgrade.
3. If a download is needed and requested, read distribution-index.json from the release.
   Select exactly one resources entry with packageType=preview and the chosen platform.
   Match the source/game baseline and exact character identities, rather than choosing the
   greatest version string. Windows and Android gameVersion values can differ. Use the index's
   asset URL and bytes; do not guess filenames. Reuse an existing ZIP for local verification.
4. Extract into a separate version directory. Do not overwrite immutable resources still
   held by an open preview. Validate manifest identity, safe paths and listed file lengths.
   The reader validates consumed files; no artifact hashes are calculated by default.

Release: https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources

Index: https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/distribution-index.json

Do not download both large packs just to test a change. Prefer local validation. Opening
the panel or invoking resources, preview or doctor performs no external download.

## Connect the extracted directory

~~~json
{ "action": "resources", "path": "/local/resources", "platform": "android" }
~~~

This sets the current DSH session and refreshes its latest preview. The directory belongs
to the DSH server, not necessarily the device displaying the browser. The panel has the same
operation, the current resource version, an ASTC capability indication, and an explicit manual
release link. A remote server cannot read the viewing phone's local files directly.

For one preview only:

~~~json
{ "action": "preview", "path": "/local/outfit.bem", "resourcesPath": "/local/resources/preview/windows/1.6.3.2", "platform": "windows" }
~~~

For persistent configuration set BEM_PREVIEW_RESOURCES to the shared root or exact pack
directory and BEM_PREVIEW_PLATFORM to windows (BC7) or android (ASTC) before starting DSH.
Without configuration it probes resources under the DSH working directory, initially BC7.
This default does not claim ASTC compatibility; users can always select either format.

## Completion and diagnostics

Match the exact character and legacy prefab root, or explicit prefab asset path and renderer
relative path, plus material slot names. Ambiguous variants are not guessed. Only components
in the BEM target contract can be completed. Native geometry appears only for effective keep;
hide/replace still apply when options or appearances change. Authored package albedo wins.

nativeCompletion reports resource version, original mesh/texture counts, missing resources,
uncovered character, identity mismatch, invalid pack or partial character failure. A bad or
missing pack degrades to package meshes. ASTC is checked separately in the viewing browser;
if unsupported, suggest BC7 without downloading automatically. Static previews do not
reproduce game shaders or animation.
