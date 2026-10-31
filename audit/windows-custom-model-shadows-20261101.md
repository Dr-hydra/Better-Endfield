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
