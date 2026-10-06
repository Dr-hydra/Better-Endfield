# Converting Source Mods to BEM

The general `convert` command and character profiles below use legacy character targets. BEM 1.4 multi-resource and static-mesh projects are supported by `pack/unpack/build/validate/bundle`, but ordinary character profiles and recipes do not automatically apply to weapons or independent ultimate prefabs. Obtain native prefab, LOD, receiver, bone and material identities for the intended platform, generate an unverified starting project with `target-profile`, then supply explicit source mappings and an editable project. Windows gameplay verification is not Android donor evidence; mobile resources and texture compatibility require separate checks. See [1.4 authoring](BEM_CREATOR_GUIDE.en.md#weapon-and-ultimate-projects-14) and [the 1.4 specification](BEM_V1_4_SPEC.md).

This document explains how to convert EFMI / 3DMigoto character Mods to BEM: what can be automated, when a recipe is required, how the converter proves identity, and what common errors mean. See the [Creator Guide](BEM_CREATOR_GUIDE.en.md) for tool and export-task usage.

## 1. Core principles

- The converter reads source INI declarations and resources, evaluates static conditions, and writes the resulting meshes and textures using the identity of the original game resources.
- It does not execute source hotkeys, INI command lists, HLSL shaders, or GUI scripts.
- Identity is established from actual INI declarations and resource hashes, not from character names, archive names, or author descriptions.
- A successful conversion means only that offline validation passed. `render_verified` remains `false`; test the package in the game.
- Effects that cannot be represented must be listed in `excluded_features`; they must not disappear silently. Unsupported effects, low-detail LODs, and animation systems may be removed. LOD0 base meshes, base material textures, and all valid static appearances must remain. If an effect command provides base drawing, bone transforms, or base textures, it must first be mapped to an equivalent BEM operation.

## 2. Inputs

- Supported inputs are directories, ZIP, RAR, and 7z archives. RAR and 7z are read through the 7-Zip 26.03 backend bundled with the tool without extracting the archive to disk.
- Limits are 512 MiB per file, 4 GiB total, and 8192 entries. Encrypted archives, incomplete multi-volume archives, symbolic links, unsafe paths, and case-colliding paths are rejected.
- The entry INI is selected by scanning all `.ini` files while skipping paths whose first component starts with `disabled` and components named `backup` or `backups`. Exactly one entry must be found or the tool reports `ENTRY_SELECTION`. Use `--ini` or the recipe's `ini` field to select it explicitly.

## 3. Automation levels

The `inspect` report exposes `automation.status`:

| Status | Condition | Direct conversion |
| --- | --- | --- |
| `ready` | Standard ComponentN, unique catalog match, and complete default appearance validation | Yes, no recipe required |
| `standard_candidate` | Standard ComponentN, but identity or validation is incomplete | No; inspect `issues` |
| `requires_mapping` | Hash/LOD format, or ComponentN uses `ps-tN =` segmented texture writes, merged skeletons, or 16-bit bone indices | Requires a reviewed profile and recipe |
| `manual_only` | Custom shader (other than EFMI's bundled `CustomShader\\EFMIv1\\`), any RabbitFX, ShapeKey, or mixed entry formats | No automatic conversion; use a specially reviewed recipe |

Entry formats are recognized as:

- `TextureOverride_(EntryPoint_)?ComponentN(_LODn)?` for ComponentN.
- `TextureOverride_(EntryPoint_)?LOD<n>.<hash8>_<index count>_<first index>` for Hash/LOD.

## 4. Supported scope

| Source feature | Result | Reason |
| --- | --- | --- |
| Standard ComponentN static appearances | Automatic | Catalog data proves identity and layout |
| Hash/LOD | Recipe | The profile must map skeleton and materials |
| `ps-tN` segmented texture writes | Recipe | There is no universal table from slot numbers to native material properties |
| Merged skeletons or 16-bit bone indices | Recipe | Bone groups and native order require review |
| RabbitFX, including Stable Textures | Not automatic | An external material framework cannot be assumed equivalent to native materials |
| GlowFX, custom shaders, ShaderOverride/Regex | Not automatic | The runtime has no corresponding executor |
| ShapeKey | Not a static conversion; can be explicitly bound to a 1.3 slider (section 8) | Source GUI code is not executed; the author must supply slider names and ranges |
| Multiple hotkey appearances | Static default only by default; organize additional appearances as 1.1 options | Do not blindly enumerate hotkey combinations |
| Low-detail LODs and animation | Not converted | BEM replaces LOD0 only |
| Global weather textures | Reused when identical to the game resource; modified global textures are rejected as `GLOBAL_TEXTURE` | BEM does not modify global effects |

The automatic ComponentN route supports a standard local-skeleton entry; three vertex streams vb0/vb1/vb2 (vb3 may only equal vb0); catalog-matching layouts; UINT16/UINT32 indices; zero-based vertices; statically evaluable default switches; and fixed texture overrides using a single `this = Resource...` assignment. `drawindexed = index count, first index, 0` preserves an original draw, while an empty callback hides the component. Multiple native materials are replayed in game order.

## 5. Identity and character catalog

- **Component identity:** the entry `hash` (the EFMI region hash, a CRC32C of the original index bytes in the draw region) and `match_index_count` are used. `match_first_index` must be 0 or the converter reports `SUBMESH_MAPPING`.
- **ComponentN numbers are only package labels.** They do not identify a fixed body part.
- **Character catalog:** the tool includes `catalog/` and matches by hash. Every hash must match exactly one catalog record or the converter reports `CHARACTER_CATALOG`. A matching hash with a different index count indicates a game update and reports `CHARACTER_REVISION`.
- **Current catalog size:** 33 characters, 371 component entries, and 1,210 texture identities. Shared public textures are counted once per character entry.
- **Excluded from automatic conversion:** 62 entries have unsupported layouts or multiple submeshes and report `NATIVE_LAYOUT_UNSUPPORTED`; 10 multi-submesh entries require dedicated rules; Cammy's `skill_01` and Typhoea's `cloth_01` differ between scene and detail resources and are not treated as shared components.
- **Texture identity:** catalog hashes map to the original game Texture name used as BEM `original_name`. Unknown hashes report `TEXTURE_MAPPING`; the converter never guesses from a DDS filename.

The catalog contains names, identities, layouts, and evidence. It does not contain game geometry or pixels. Maintainers recapture it with `developer-tools/` after game updates.

## 6. Source DDS files

| Type | Supported |
| --- | --- |
| DX10 header | BC1, BC3, BC4_UNORM, BC5_UNORM, BC7, RGBA8, BGRA8 (converted to RGBA), and R8 |
| Legacy FourCC | `DXT1`, `DXT5`, `ATI1`/`BC4U`, and `ATI2`/`BC5U` |
| Uncompressed (FourCC 0) | RGBA32, BGRA32 masks, and L8 (mapped to R8) |
| Rejected | BC2/DXT3, BC6H, SNORM BC4/BC5, cubemaps, volume/array textures, padded uncompressed rows, and BC dimensions that are not multiples of 4 |

- sRGB is read only from the DDS header. Legacy FourCC and legacy RGBA headers are treated as linear.
- Dimensions are limited to 32768 and mip levels to 16. Format errors use the `DDS_FORMAT:` prefix.
- Filename format or usage labels are never used as evidence.

## 7. Conversion recipes

Recipes fix package metadata, combine multiple appearances, or provide profiles for sources that need mapping. See `examples/conversion.recipe.json` in the tool directory. Paths are relative to the recipe file.

```json
{
  "schema": 1,
  "package": {"id": "creator.character.outfits", "name": "Outfit Collection", "author": "Author", "version": "1.0.0"},
  "target": {"character_id": "chr_0013_aglina", "world_resource": "chr_0013_aglina_postmodel",
    "ui_resource": "chr_0013_aglina_uimodel", "profile_id": "aglina.windows", "revision": "20260918"},
  "default_appearance_id": "default",
  "appearances": [{"id": "default", "name": "Default", "format": "hash-lod", "profile": "profiles/verified-native.json", "ini": "character.ini"}]
}
```

| Field | Meaning |
| --- | --- |
| `package` | ID, name, author, and version; keep the ID when updating a published package |
| `target` | Target character; all appearances must match or `APPEARANCE_TARGET` is reported |
| `deformations` | Optional path to a 1.3 deformation file |
| `appearances[].format` | `auto`, `component-n`, `hash-lod`, or `reviewed-draws`. `auto` recognizes entry format but cannot invent missing mappings |
| `appearances[].source` | Optional source path when combining appearances; targets must match |
| `appearances[].description` / `preview` | Appearance description; preview PNG is limited to 8 MiB |
| ComponentN | May specify `source_profile` and `material_profile`; a profile must provide `v24_draws` for conversion bridging |
| Hash/LOD | Profile must be `schema=2`, `verified=true`, backed by evidence, and include each component's mesh name, index count, strides, attributes, bone names, materials, skinning, and material rules |
| `reviewed-draws` | A `reviewed` object containing `recipe`, `database`, `observations`, `native_textures`, and `texture_dir`; only for specifically reviewed sources, which must be re-reviewed if the source INI or shader changes |

The `verified` flag must be supported by actual evidence. Do not set it to `true` merely to bypass a missing observation.

## 8. Binding a ShapeKey to a 1.3 slider

A source package containing ShapeKeys cannot be statically converted (`SHAPE_BINDING_REQUIRED`). After confirming which shape key corresponds to which parameter, bind it explicitly in the deformation file:

```json
{"value": 1000, "efmi": {
  "source": "source-mod.zip", "ini": "character/mod.ini", "component": 0,
  "shape_key": 1, "vertex_order": "exported"
}}
```

- `source` is a directory, ZIP, RAR, or 7z source. `ini` identifies the declaration. `component` selects a `Resource_ComponentN_*` resource group. `shape_key` is the integer assigned to `$shapekey_id` before `CommandListSetShapeKey` runs.
- `vertex_order:"exported"` is the author's assertion that exported and source vertices correspond one-to-one. The tool does not infer this from a component number, stride, or name.
- If vertices were copied or reordered, provide `vertex_map`, assigning a source vertex ID to every BEM vertex. Different LODs require separate maps.
- The tool reads `ShapeKeyBatchConfigs`, `ShapeKeyVertexIds`, and `ShapeKeyVertexOffsets`, selects the requested shape-key records, converts FP16 deltas to Float32, and writes BEM position deltas.
- The author supplies the parameter name, range, and default; the tool does not infer them from the source GUI.

See the [Creator Guide's slider section](BEM_CREATOR_GUIDE.en.md#body-sliders-13) for other deformation-file forms.

## 9. Common conversion errors

| Error | Meaning | Fix |
| --- | --- | --- |
| `ENTRY_SELECTION` | No entry INI or more than one entry INI | Check archive nesting or use `--ini` |
| `ARCHIVE_FORMAT` / `ARCHIVE_READ` / `ARCHIVE_PASSWORD` / `ARCHIVE_LIMIT` / `ARCHIVE_BACKEND` | Bad, damaged, encrypted, oversized archive, or missing 7-Zip backend | Provide a complete unencrypted archive and the full tool directory |
| `CHARACTER_CATALOG` | Hash did not match, or matched multiple catalog records | Confirm the source targets the current game version; a maintainer may need to add catalog data |
| `CHARACTER_REVISION` | Hash matched but index count changed | Update the character catalog |
| `NATIVE_LAYOUT_UNSUPPORTED` | Component layout or multiple submeshes are unsupported | Use a reviewed recipe |
| `SUBMESH_MAPPING` / `ENTRY_PROGRAM` / `STATIC_STATE` / `DRAW_STATE` / `VERTEX_LAYOUT` / `INDEX_FORMAT` | Non-standard entry, unevaluable switch, draw, vertex layout, or index format | Provide a dedicated mapping or recipe |
| `TEXTURE_MAPPING` / `TEXTURE_STATE` | Unknown texture hash, or the override is not one `this = Resource...` assignment | Simplify the override; unknown textures need evidence |
| `GLOBAL_TEXTURE` | The source changes a global weather texture | Remove that override |
| `COMPANION_INI` / `MIXED_ENTRY` / `MANUAL_ADAPTATION` | Multiple active INIs, mixed entries, custom shader, or RabbitFX | Require dedicated review |
| `AUTO_MAPPING_PENDING` | Skeleton or material mapping is missing | Use a verified profile and `--recipe` |
| `TARGET_PROFILE` | Recipe lacks a verified profile or `v24_draws` | Complete the profile |
| `source INI/shader differs ...; re-audit required` | A reviewed source changed | Review it again |
| `Legacy ... differs` | Intermediate conversion differs from the profile | Check the profile; this is independent of runtime version |
| `SHAPE_BINDING_REQUIRED` | Source has ShapeKeys without a binding | Bind them as in section 8 |
| `DDS_FORMAT:` | Unsupported texture format | Convert the texture as in section 6 |
| `Bone index outside palette` / `Palette/draw limit exceeded` | Bone mapping error or more than 256 palette bones | Correct the mapping; never truncate |
| `Draws must partition IB` | Draws do not continuously cover the index buffer | Explicitly duplicate the relevant indices for overlapping draws |
