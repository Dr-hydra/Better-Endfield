# BEM Runtime Behavior and Compatibility

### BEM 1.4 resources on both platforms

- Windows and Android expand `target.resources` for the current platform and match exact receiver paths and renderer kinds. Same-name Sprite/Texture2D assets do not enter model APIs.
- Skinned ultimate resources use their own prefab bones and material donors. Static weapons use a MeshRenderer with the unique MeshFilter on the same GameObject, without dummy bones. Explicit Android resources do not borrow a legacy character UI donor.
- Android first-enable discovery handles explicit resources declared for `android-arm64`, validating their own LOD and receiver contracts before rebinding. Windows still accepts explicit LOD0 only. Legacy Android packages retain UI LOD0→world LOD1 adaptation.
- Version 1.4 conflicts use resource roots on the current platform. Packages without current-platform resources cannot be imported; texture conversion does not change platform declarations.
- Weapon/ultimate loading and PC hot switching have user gameplay acceptance. Android implementation and regression tests are not device rendering acceptance, and adding a platform label to a Windows sample does not establish Android compatibility.

The world/UI and skinning details in sections 1–6 below primarily describe legacy character packages; explicit 1.4 contracts are defined in the extension specification.

The sections below describe the legacy 1.0–1.3 character world/UI route. The current release supports [BEM 1.4 resource targets](BEM_V1_4_SPEC.md), with explicit receiver paths and platform declarations. Windows executes LOD0 targets; Android uses the declared resource itself as donor. New game targets still require platform evidence and in-game validation.

Android world LOD1 is the existing design: legacy packages use UI LOD0 donors with world LOD1 receivers, while 1.4 packages can explicitly declare their own LOD1 receivers and donors. The Windows LOD0 restriction does not apply to Android. All enabled Android model packages participate in the existing `lod_pipeline` bias maintenance without changing QualitySettings. The function name `EnableForceLOD0` does not require Android to render LOD0.

This document describes what the game actually does when loading a BEM package: how replacement components are found, what geometry and texture requirements apply, loading cost, when selections take effect, and what rejection messages mean. See the [Format Specification](BEM_FORMAT_SPEC.en.md) for fields and the [Creator Guide](BEM_CREATOR_GUIDE.en.md) for production workflow.

Windows and Android use the same native reader and builder (`native/modules/custom_model`). Differences are limited to scene models, shadows, and texture formats, as marked below.

## 1. When replacement occurs

- When the game loads character resources (`*_postmodel` scene models and `*_uimodel` detail models), the module synchronously builds replacement meshes and materials before handing the resource to the game, then submits them as one transaction. Every instance created afterward uses the replacement.
- If any step fails, the original bindings are restored and the original model is delivered; the game never receives a partial result.
- Only one package per character may be enabled. If two enabled packages target the same character, or if two packages use the same resource roots, all of them are disabled for that load and the log reports `Conflicting enabled package`.
- Before resource-name comparison, `(Clone)` and `#number` suffixes are removed, so scene copies of the same character are recognized.
- The title-screen character uses the scene-model (`*_postmodel`) path and follows scene-model replacement rules.

## 2. Component matching

**Every** component in `target.components`, including keep and hide entries, must resolve to exactly one matching Renderer or the whole package is rejected.

Matching requires:

1. The Renderer is under `Mesh_all/lod0/` beneath the resource root.
2. The complete name of its current Mesh equals `mesh_name`. Suffixes such as `_lod0_20` and `_8` are part of the name; they are not removed, and case is not normalized.
3. The sum of index counts of all Mesh submeshes equals `original_index_count` (at most 256 submeshes).
4. There are 1..256 valid materials and at most 256 bones, all under the resource root.

Additional rules:

- The Renderer name is not part of identity. A Renderer and Mesh with different names can match.
- Two or more matching Renderers are ambiguous and rejected. Two components cannot point to the same Renderer.
- If a game update changes a Mesh name or index count, the old package is rejected and must be re-exported against updated character data.

## 3. LOD

**Windows:** both scene and detail models replace LOD0. When any package is enabled, the module forces LOD0 (`EnableForceLOD0`, `QualitySettings.maximumLODLevel=0`) and increases NPC crowd LOD distance. When all packages are disabled, the original settings are restored. If forcing fails, the log reports `LOD prerequisite unavailable` and the original model is delivered. With no package enabled, “Lock high-detail LOD” can be enabled separately in the manager.

**Android:** the phone scene model uses LOD1, while replacement data is built from detail-model LOD0:

1. Prepare or reuse the detail-model LOD0 replacement for the same selection.
2. Detail `Mesh_all/lod0/X_lod0` maps to scene `Mesh_all/lod1/X_lod1`. A detail Renderer whose name does not end in `_lod0` cannot be mapped and reports `unsupported UI receiver path`.
3. In strict validation mode, the scene LOD1 Mesh name must be exactly `X_lod1`. Characters with other suffixes are rejected with `world mesh identity differs`. When model validation is disabled, the runtime tries a bounded `_8` / `_20` suffix match within the same character, resource root, and LOD region; ambiguous or structurally invalid candidates are still rejected.
4. Detail and scene Renderers must have the same local-space geometry or the adapter reports `Android world mesh space differs`.
5. Bones are mapped by complete paths relative to each resource root. If a path is missing, the adapter tries a package-declared bone alias under the same parent node. Names must match the package.
6. Detail and scene models are committed in one transaction; failure rolls both back.

Android LOD bias is enabled by the installer and does not modify `QualitySettings`.

The production callers load a generated Android relation table for the current resource snapshot. A verified row can map a UI LOD0 Renderer to a different world LOD1 Renderer and Mesh identity, including a confirmed `_8` or `_20` Mesh suffix. If no verified row exists, strict validation uses the legacy exact `_lod0` to `_lod1` path; the developer validation-bypass route may additionally use the bounded suffix fallback described above.

## 4. Shadows (Android scene model)

- Replaced components use their own shadow casting (`ShadowCastingMode.On`) instead of a shadow proxy mesh.
- Shadow proxies under `Shadow_Proxy/SP_Mobile/*` for replaced or hidden components are disabled. The relation is first determined by whether the proxy's original Mesh is the same object as the component's original LOD1 Mesh, then by `Shadow_Proxy/SP_Mobile/X_shadowProxyMobile` path.
- If one proxy maps to multiple components and one of them changes, replacement is rejected with `ambiguous mobile proxy owner`.
- Kept components and `SP_Desktop` are unaffected.

## 5. Geometry and skinning

| Item | Requirement |
| --- | --- |
| Vertex declaration | The runtime builds the new mesh from package declarations, not the original layout; it reads the built declaration and stride back and requires them to match the package |
| Normals and tangents | Not recalculated; package data is used |
| Bindposes | Not included in BEM; original Mesh bindposes are used, with count greater than the largest bone index. Geometry must use the original Mesh space and bindposes |
| Influences per vertex | Cannot exceed the original Mesh native setting (1, 2, or 4). A rigid original requires rigid replacement skinning |
| Weights | At least one non-zero influence per vertex; sum 1±0.01 |
| Cross-component bones | Taken from the donor component's original Renderer bones and Mesh bindposes; donor and target must be in the same Mesh space |
| Bone names | Runtime names must equal package names or declared aliases; resource-specific alias isolation is not yet complete in the native parser |
| New Mesh name | Retains the original Mesh name |

## 6. Materials and textures

**Materials**

- Each `replace` draw copies the specified original material, whose name must match, preserving the original shader, keywords, and numeric parameters.
- `keep` retains the original mesh but copies its material set; `material_overrides` replaces only named texture slots.
- `hide` disables the Renderer and leaves the original mesh untouched.
- Android scene models reuse the materials and meshes prepared for the detail model. Keep overrides require matching material names on both sides or the supported `M_actor_X` to `M_actor_lod_X` relationship.

**Texture replacement** looks up the original texture object by exact `original_name` on the copied material.

- If multiple properties point to the same original texture object, all are replaced.
- Different texture objects with the same name are ambiguous and rejected. Use a 1.2 texture slot or another donor.
- If no matching property exists, the build reports `Texture name pin missing`.
- The replacement copies wrap mode, filtering, anisotropy, and mip bias; color space comes from `srgb`; mipmaps are not regenerated.

**Platform formats**

- Windows supports BC1/BC3/BC4/BC5/BC7, RGBA32, and R8.
- Android rejects formats unsupported by the device backend with `Texture format unsupported by game backend`. Most phones do not support BC formats, so use “Convert textures” during installation:
  - ASTC devices convert color textures to ASTC 6×6 and linear data to ASTC 4×4; non-ASTC devices convert to RGBA32. R8 and already-ASTC textures are unchanged.
  - Converted textures are limited to 8192 pixels and 64 MiB. The installer drops the highest mip, or downsizes a single-level texture by powers of two. The report records this as `android_install_mip_skip`.
  - Normal-map encoding must be known through `semantic:"normal"` and `normal_encoding`, or a built-in normal rule. Built-in rules currently cover only some character `_N` textures. `xy-unorm` reconstructs Z.

## 7. Loading cost and memory

**Upload cost is based on decoded texture size, not BEM file size or compression ratio.** Approximate sizes for a complete mip chain:

| Size | RGBA32 | BC7 / ASTC 4×4 | ASTC 6×6 |
| --- | --- | --- | --- |
| 2048² | about 21 MiB | about 5.3 MiB | about 2.4 MiB |
| 4096² | about 85 MiB | about 21 MiB | about 9.5 MiB |
| 8192² | about 341 MiB | about 85 MiB | about 40 MiB |

For reference, seven 8K BC7 textures upload about 448 MiB per load. Several characters loading together add these costs.

Author guidance:

- One 8K BC7 texture is about 85 MiB, above the 64 MiB per-texture limit in 1.0/1.1, so use 1.2+; phone installation can still downsize it.
- Prefer 4K or 2K textures for mobile packages.
- Do not include textures that do not need replacement; untouched slots continue to use original textures.

**Loading modes**

- The default is low-peak mode: synchronize with the render thread after about every 4 MiB of texture data to reduce transient CPU/GPU peaks.
- “Prioritize loading speed” synchronizes every 128 MiB instead. It loads faster but has higher peaks and suits devices with more memory.
- Both modes complete during the same resource delivery. The log reports `Model loading mode=...`. This option requires a game restart.

**Other behavior**

- Texture data is decompressed one texture at a time and the CPU copy is released after upload.
- Repeated texture references in one load upload once. If a visible model for the same character still uses the same texture, rebuilding the detail page can reuse it.
- There is no idle cache: closing a detail page and reopening it loads its resources again.

## 8. Selection timing and hot switching

- **Default:** enable/disable, appearance, option, and slider changes take effect at the next game start.
- **Experimental hot switching** (enable it, then restart once): the game-frame scheduler applies selection changes to registered templates and scene instances without waiting for a team switch or detail-page rebuild. PC discovers cached roots on first enable; Android 1.4 also discovers explicit resources. Later normal resource deliveries still use the latest selection.
  - Disabling a package restores the original model. The original Mesh is retained while a replacement is displayed, so memory use is slightly higher.
  - If the original resource has already been unloaded, rebuilding is rejected with `Hot switch Original ... unavailable`; re-enter the scene or restart the game.
  - Hot switching, model-validation bypass, and loading-speed priority each require a restart when their own flag changes.
  - A model loaded before hot switching was enabled cannot be switched directly; restart once.
- 1.3/1.4 sliders request selection rebuilds rather than per-frame vertex animation. They follow the scheduler above with hot switching enabled, or apply at the next start otherwise.

## 9. First-person compatibility

First-person mode hides head-area geometry so it does not block the camera. The primary test is **bone weights**; names only narrow the scan.

- Head bones include the `Bip001_Head` subtree, tail chains (`tail`, `tail_*`, `bip001_tail*`), and head-accessory bones registered in character data such as `maozi_*`. Neck is not considered head geometry.
- If every influence of a component is on head bones, the complete component becomes shadow-only: invisible but still casting a shadow.
- When head and body share a mesh, only triangles whose three vertices have all non-zero weights on head bones are clipped. Any neck, torso, or clothing weight keeps the face.

Author guidance:

- Bind hair, headwear, and ears only to the Head subtree or tail bones.
- Do not bind body or clothing vertices to Head bones or they may be clipped in first person.
- Exact clipping is used up to 200,000 vertices, 6,000,000 indices, and 64 draws. Larger replacements use the generic fallback.

## 10. Disabling model validation (experimental)

When disabled, compatibility checks such as capability declarations, limits, bone names, index counts, Mesh space, weights, influence counts, and Android post-submit readback are skipped. Container bounds, payload integrity, texture format, skinning layout, and receiver/resource boundaries are still checked. Android may use a bounded `_8` / `_20` Mesh-name fallback within the same character, resource root, component path, and LOD region; ambiguous candidates are still rejected. Missing textures retain the original; multiple textures with the same name are all replaced.

This is for development only and may render incorrectly or crash the game. The log reports `Developer mode: model validation disabled`.

## 11. Log rejection reference

Logs are in `%LOCALAPPDATA%\BetterEndfieldNext\logs\BetterEndfieldNext.log` on Windows and in the framework module log on Android.

**Package and configuration**

| Message | Cause | Action |
| --- | --- | --- |
| `Refused non-BEMv1 package` | Configured file is not `.bem` | Import a `.bem` |
| `Package refused: Mod.x: ...` | Package read failed; the suffix explains why | Fix the reported reason |
| `Conflicting enabled package` | Multiple packages for one character are enabled | Enable only one |
| `Appearance removed; using package default` / `Parameters removed or invalid` | Saved choice no longer exists in the new package | Select again |
| `Only BEM 1.0/1.1/1.2/1.3 packages are supported` | Invalid file header or version | Re-export with current tools |
| `Invalid BEM file size` / `Trailing or missing BEM bytes` / `Invalid payload ...` | Corrupt file or container limit exceeded | Re-export or download again |
| `Unsupported required capability` / `... capability mismatch` | Capability missing, unknown, or inconsistent with version | Repack; the tool adds required capabilities |
| `Unsupported target platform` | `target.platform` is not `windows-x64` | Keep `windows-x64` |

**Structure and limits**

| Message | Cause | Action |
| --- | --- | --- |
| `Target has multiple/no selected operation` / `Unreachable option combination` | Rules are not unique or complete | Fix `component_rules` or `selection_constraints` |
| `... exceeds decoded payload budget` / `... runtime memory budget` | Selected content exceeds the version budget | Reduce textures or use 1.2 |
| `Appearance exceeds N texture bindings` / `Texture exceeds N MiB` | Too many bindings or a texture is too large | Merge or reduce textures |
| `Unsupported texture format` / `R8 texture must be linear` / `Invalid texture dimensions` | Invalid texture format or dimensions | Use a supported format |
| `Invalid geometry counts/index type` / `Selected draw limit exceeded` / `Draws must partition IB` | Geometry/draw limits exceeded or draws do not cover the index stream | Split components or fix draws |
| `Unsupported skin layout/declaration` / `Declaration/stride or skin indices differ` | Skinning stream is not one of the three allowed layouts | Follow section 7 of the format specification |
| `Skin index outside palette` / `Bone identity differs` / `Invalid skin weight sum` | Bone index, name, or weights are invalid | Fix mapping and normalize weights |
| `Texture slot candidates must replace one original texture` | One texture slot targets several original textures | Split the slot |

**In-game construction**

| Message | Cause | Action |
| --- | --- | --- |
| `Generic model donor is missing/ambiguous: <mesh_name>` | No unique original component match | Check `mesh_name` and `original_index_count` against the current game catalog |
| `Merged palette donor missing or mesh spaces differ` | Bone donor is missing or uses another space | Borrow bones only from the same space |
| `bindpose palette too small` | Original bindpose array is too short | Check the bone table |
| `source native skin field cannot represent replacement influences` | Replacement uses more influences than the original | Reduce influences per vertex |
| `invalid skin weights at vertex` | Runtime decoded an invalid weight | Normalize weights |
| `Texture name pin missing` | Donor material has no matching original texture | Check texture name and donor material |
| `Ambiguous v25 texture name pin` | Several different original textures share a name | Use a texture slot or another donor |
| `Duplicate texture slot assignment` | One original texture is assigned twice | Remove the duplicate |
| `Texture format unsupported by game backend` | Device does not support the texture format, common for BC on phones | Convert textures during installation |
| `Android world adapter refused: ...` | Scene-model mapping failed | See sections 3 and 4 |
| `Android world bone path missing` / `bone name differs from package` | Scene skeleton path or name differs | Declare a bone alias |
| `LOD prerequisite unavailable` | Windows could not force LOD0 | Usually a game update; update the application |
| `Resource preparation failed; original retained` | Preparation failed; earlier logs contain the cause | Read the preceding entries |
| `Hot switch configuration rejected` / `Hot switch flags changed; restart the game` | Invalid hot-switch configuration or a restart-required flag changed | Restart the game |

## Experimental cloned model support

The Windows and Android model pages offer an independent **Experimental: cloned model support** switch. It defaults to off and requires a game restart. Windows stores `[CustomModel] clone_support=false`; Android stores `bem_clone_support` and carries the startup value through selection updates. Hot switching, upload pacing and validation remain independent.

Enabled observation records the source and package generation at instantiation. Template delivery remains synchronous. A clone that already inherited the complete result keeps its private materials and parameters without a duplicate upload. Rebuilds use the verified Original and map its bone paths into the clone's own hierarchy.

Private material copies alone do not reject an observed clone. Unknown Meshes, released Originals, changed source declarations, ambiguous receivers and missing/duplicate required bones still reject preparation and retain current bindings. The `(Clone)` suffix routes a resource; it does not prove ownership. Legacy Android characters retain their UI LOD0 to world LOD1 adapter, while explicit resources retain their own paths.

A completed receiver holding its exact recorded Original Mesh can be prepared again. This redelivery correction is always active and does not require the experimental switch.

Offline lifecycle regressions and both Release builds have been verified. Ultimate-state teleportation, pooling, NPC wrappers and game-specific private material changes still require device validation.
