# Windows custom-model world shadows — 4.1.7

## Problem and cause

Windows resource delivery replaced visible model geometry without taking over the independent SP_Desktop shadow proxies. Original silhouettes remained visible. The historical Windows shadow implementation had not been migrated into the production resource pipeline; Android retained its separate shadow strategy.

The first implementation also treated equal renderer matrices as an ownership requirement. Live instances use different local axes for the visible renderer and proxy (approximately 90 degrees about X), so hot-switch preparation refused valid owners. Detailed diagnostics confirmed type and bone ownership succeeded while matrix comparison failed. Matrix equality is unnecessary when only toggling an independent proxy, without transferring geometry or bindposes.

## Final implementation

- Resource-local desktop proxy enumeration; no additional scene scan.
- Replaced visible renderers clear shadowProxyMesh and enable casting when needed. Hidden parts disable their matching proxy. Material-only parts retain their original shadow behavior.
- Desktop proxy ownership uses an exact canonical LOD/proxy path and compatible bone ownership, or a unique pristine mesh/shadow-mesh identity with an exact bone palette.
- Neither ownership route depends on transform matrices. Potential competing owners participate regardless of coordinate space. Type, bone ownership and ambiguity checks remain.
- Original retention, commit/rollback, completion readback, hot switching, clone lineage and disabled-resource restoration include Windows shadow state.
- Existing geometry/palette conversion checks remain unchanged.
- Windows-only preparation and calls are guarded by _WIN32. Android preparation behavior is unchanged; the Android test fake gains a field already required by production.

## Diagnostics cleanup

Temporary object-address, matrix, bone-detail and successful-preparation counters are removed from runtime logging. Shadow preparation no longer reads matrices even for diagnostics. Concise failure reasons and normal transaction logs remain. Tests capture necessary failure logs without printing diagnostic noise.

## Validation

Before cleanup, Release builds and six regression groups passed: Windows shadows, complete bindings, async Jobs, clone support, material cache and retired-resource lifecycle. Earlier Android world adapter regression passed 213 checks. Coverage includes apply/rollback, repeated replacement, keep/hidden restoration, completion drift, different axes, matrix API absence, foreign skeleton refusal and ambiguous ownership. The user confirmed the in-game fix works.

The diagnostic-only formatter crash was traced through the native crash report to a Mesh passed to Component.get_transform; that formatter is now removed entirely. Test fixtures retain receiver-type guards to prevent this class of error.

For the final 4.1.7 cleanup, only static source/version/whitespace checks are performed as requested. No new compilation, installation overwrite, release tag, package upload or GitHub Release is performed. The previously tested local DLL remains installed; the cleaned source will take effect on a future build.

## Version and notes

Desktop Version/InformationalVersion: 4.1.7. Android versionName/versionCode: 4.1.7 / 40107. BEM Tools & DSH and resource packages remain unchanged. User-visible changes are recorded in CHANGELOG.md.

## Follow-up: switching between package schemas

The November 1 12:31–12:32 game log distinguishes toggling from direct package replacement. Revision 9 successfully applied the legacy package bem.845ae2cf2f3406ac2dd3371ea60aff22 to the template and four world instances. Revision 10 switched to the explicit package bem.subaru.dr_hydra.whole_outfits.v14; all five roots exhausted retries. The new package expected 27468 original body indices, but validation saw 92889 indices from the previous generated appearance. This fails before Windows shadow preparation.

Legacy adapters pair world/UI resources and classify receiver paths as LOD0, whereas explicit adapters identify one resource, have a package-local resource id and classify paths as Explicit. Strict adapter and receiver-key equality therefore hid the previous package's saved Original. The source fix separates Original lineage from package selection: on Windows only, adapters may share lineage for the same character and actual resource name, and the same exact receiver path may bridge its classified region and Explicit. SameAdapter, package completion and geometry validation stay strict. Both pristine-identity lookup and saved binding recovery use this lineage rule. No new runtime diagnostics or transform gates are added. Android keeps the previous equality behavior.

The Windows shadow regression now covers explicit/legacy/explicit replacement with same-name meshes of different index counts, rollback on a failed cross-format commit, saved proxy state, inherited scene clones with clone-local bones, package-local resource-id changes, refusal of unknown meshes and foreign character/resource/LOD paths, and restoration after switching. These are new source test cases, not executed results. Source whitespace and structural checks are performed; the prior request to avoid compilation remains in effect. No installation overwrite or release is performed for this follow-up.

## Follow-up build and local deployment — November 1, 12:40 CST

After the user explicitly authorized compilation and E-drive overwrite, Release builds of BetterEndfieldNext.CustomModel and BetterEndfieldNext.CustomModelBindingTests succeeded. Six regression groups passed: Windows shadows (including the new cross-schema switching cases), full bindings, async, clone support, material cache sync and retired resources. Existing variable-shadowing and conversion warnings were reported; there were no build errors.

The new 2,109,952-byte module was installed at E:/Better Endfield/modules/BetterEndfieldNext.CustomModel.dll. The previous module was backed up at E:/Better Endfield/backups/windows-package-schema-hotswitch-20261101-124006/BetterEndfieldNext.CustomModel.dll. Direct byte comparisons verified the backup against the previous installation and the installed file against the new build output. No artifact hashes were calculated. This deployment includes the earlier diagnostic-log cleanup. After installing this build, the user confirmed in-game package switching works and authorized pushing the fix. No release was published.
