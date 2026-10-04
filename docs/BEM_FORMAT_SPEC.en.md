# BEM Format Specification (1.0–1.3)

BEM (Better Endfield Model) is a character model replacement package with the `.bem` extension. Windows and Android read the same file. This document combines the 1.0–1.3 protocol; version-specific differences are marked below. See the [Creator Guide](BEM_CREATOR_GUIDE.en.md) for production workflow and [Runtime Behavior and Compatibility](BEM_RUNTIME_COMPATIBILITY.en.md) for in-game matching and restrictions.

Reference implementations: writers and validators are `tools/CustomModel/bem_v1.py`, `bem_v11.py`, and `bem_v13.py`; the native reader is `native/modules/custom_model/bem.cpp`.

## 1. Versions

| Minor version | Added | Required capability |
| --- | --- | --- |
| 1.0 | Fixed appearances, `appearances` | `fixed-appearances` |
| 1.1 | Composable options, `option_groups` + `component_rules`, conditional draws, and texture overrides on keep components | `composable-options`; add `keep-material-textures` when used |
| 1.2 | `texture_slots`, resource-specific bone aliases, 32-byte uncompressed skinning, and higher resource limits | Add `texture-slots` and/or `resource-bone-aliases` as used |
| 1.3 | Continuous parameters, `parameters` + `mesh_deformations` | `body-parameters`, `mesh-position-deltas` |

- Writers choose the **lowest version that can express the package**. A package without newer features remains readable by older runtimes. Newer content in an older header is rejected.
- Readers apply the limits and semantics for the header version. The current runtime accepts 1.0–1.3.
- Every version requires `native-materials`, `palette-u8`, and `indices-u32`. Unknown or duplicate capabilities, and used features without their capability, are rejected. Declaring an unused capability is allowed.

## 2. Container

The file contains, in order: a 40-byte header, a UTF-8 JSON manifest, a payload directory, and contiguous payload bytes. Integers are little-endian, structures have no padding, the file limit is 2 GiB, and there is no file hash.

| Header offset | Type | Field |
| --- | --- | --- |
| 0 | byte[8] | magic `42 45 4D 00 50 4B 47 00` (`BEM\\0PKG\\0`) |
| 8 | uint16 | major = 1 |
| 10 | uint16 | minor = 0..3 |
| 12 | uint32 | header_size = 40 |
| 16 | uint64 | file_size, equal to the actual file length |
| 24 | uint64 | manifest_size, 1 byte..4 MiB |
| 32 | uint32 | payload_count |
| 36 | uint32 | flags = 0 |

Each payload-directory entry is 32 bytes; its array index is the Payload ID:

| Entry offset | Type | Field |
| --- | --- | --- |
| 0 | uint32 | codec: 0 raw, 1 Zstandard |
| 4 | uint32 | reserved = 0 |
| 8 | uint64 | offset, absolute file offset |
| 16 | uint64 | stored_size |
| 24 | uint64 | decoded_size |

- Each payload is 1 byte..512 MiB; raw payloads have equal stored and decoded sizes.
- Entries are ordered by offset. The first payload follows the directory, each later payload follows the previous one, and gaps, overlaps, out-of-range offsets, and trailing bytes are forbidden.
- Zstandard data must be one independent frame with no dictionary and an exact content size. Compression level does not affect compatibility; raw is used when compression provides no benefit.
- Byte-identical data shares one Payload ID.

One or more `.bem` files may be placed in a standard ZIP for distribution. ZIP does not change the BEM files; the manager extracts and manages them separately.

## 3. Manifest rules

- JSON must not contain duplicate keys or NUL characters, and nesting may not exceed 48 levels. Numeric fields are UInt32 integers and `srgb` is boolean.
- Stable IDs match `[A-Za-z0-9][A-Za-z0-9_.-]{0,95}` and are case-sensitive. Names and identity strings are at most 256 UTF-8 bytes.
- Resources are referenced by array index; manifests contain no local paths or runtime addresses.
- Unknown ordinary fields are optional metadata. New rendering semantics must be introduced by a capability or version, not hidden in an optional field.

Top-level fields:

| Field | Meaning |
| --- | --- |
| `schema` | Always `1` |
| `package_id` | Package identity; keep it for updates |
| `name`, `author`, `version` | Display name, author, and display version |
| `required_capabilities` | Required capability array |
| `target` | Target-character contract; see section 4 |
| `meshes` | Mesh resources, at most 4096 |
| `textures` | Texture resources, at most 4096 |
| 1.0: `default_appearance_id`, `appearances` | Fixed appearances; see section 5 |
| 1.1+: `option_groups`, `component_rules`, optional `selection_constraints` | Composable options; see section 6 |
| 1.2+: optional `texture_slots` | See section 9 |
| 1.3: optional `parameters`, `mesh_deformations` | See section 10 |

## 4. Target contract

```json
"target": {
  "character_id": "chr_0013_aglina",
  "platform": "windows-x64",
  "profile_id": "aglina.windows", "revision": "20260918", "snapshot": "",
  "world_resource": "chr_0013_aglina_postmodel",
  "ui_resource": "chr_0013_aglina_uimodel",
  "components": [
    {"id": 0, "mesh_name": "S_actor_example_body_01_lod0", "original_index_count": 300,
     "bone_names": ["Bip001_Pelvis"], "materials": ["M_actor_example_body_01"]}
  ]
}
```

- `character_id` is the complete character ID. A package targets one character.
- `platform` is a format constant and must be `windows-x64`. Android reads the same package; changing this value causes rejection.
- `world_resource` (scene root, `*_postmodel`) and `ui_resource` (detail-model root, `*_uimodel`) must differ.
- `profile_id`, `revision`, and `snapshot` record the catalog source; `snapshot` may be empty.
- `components` lists **all** replace, keep, hide, bone-donor, and material-donor components, 1..64 entries:
  - `id` is continuous from 0 to N-1; `mesh_name` is the complete original game LOD0 Mesh name and must be unique; `original_index_count` is the sum of index counts of all original submeshes and is a positive multiple of 3.
  - `bone_names` follow native bone-slot order, at most 65,536 entries; `materials` follow native material-slot order, at most 256 entries.
  - 1.2+ may add `bone_name_aliases`; see section 9.

Packages contain no bindposes, skeletons, or shaders. The runtime uses the current game's bindposes and original materials.

## 5. Fixed appearances (1.0)

```json
"default_appearance_id": "outfit-a",
"appearances": [
  {"id": "outfit-b", "name": "Another outfit", "description": "Fixed state",
   "components": [{"target": 0, "operation": "replace", "mesh": 0},
                  {"target": 1, "operation": "keep"},
                  {"target": 2, "operation": "hide"}]}
]
```

- There are 1..64 appearances. Each completely lists every target component; appearances do not inherit or layer.
- `operation` is `replace` (use one entry from `meshes`), `keep` (retain the original mesh), or `hide`.
- Optional `preview` is a PNG Payload ID, at most 8 MiB. The current manager shows the character's public avatar; preview images are reserved for tools.

## 6. Composable options (1.1+)

Starting with 1.1, use option groups and component rules instead of `default_appearance_id` and `appearances`.

```json
"option_groups": [
  {"id": "outfit", "name": "Outfit", "default": "a",
   "choices": [{"id": "a", "name": "Outfit A"}, {"id": "b", "name": "Outfit B"}]},
  {"id": "part21", "name": "Part", "default": "on",
   "available_when": {"eq": ["outfit", "b"]},
   "choices": [{"id": "on", "name": "Show"}, {"id": "off", "name": "Hide"}]}
],
"component_rules": [
  {"target": 0, "candidates": [
    {"when": {"eq": ["outfit", "a"]}, "operation": "keep"},
    {"when": {"eq": ["outfit", "b"]}, "operation": "replace", "mesh": 0}
  ]}
]
```

**Option groups**

- Each group has a stable `id`, display `name`, `default`, and mutually exclusive choices. There are at most 64 groups; choice limits are in section 11. The total Cartesian combination count has no standalone limit.
- `available_when` may reference only earlier groups. When unavailable, the UI hides the group but retains its saved value; while evaluating, it has no active value and `eq` against it is false. Its value returns when it becomes available.
- Optional `selection_constraints` lists conditions that must all hold for a legal combination, representing states the source Mod can actually reach. The default combination must be legal.
- A 1.3 package with sliders but no discrete choices may use an empty `option_groups` array and unconditional rules. 1.1/1.2 require at least one group.

**Condition syntax:** `true`, `false`, `{"eq":["group","choice"]}`, `{"all":[...]}`, `{"any":[...]}`, and `{"not": condition}`. Depth is at most 16 and each `all`/`any` node has 1..32 children. No source INI, expression, or shader is executed.

**Component rules**

- `component_rules` covers target IDs 0..N-1 in order. Each candidate is `keep`, `hide`, or `replace` with `mesh`.
- For every legal combination, exactly one candidate must hold for each component; a selected `replace` mesh must select at least one draw. The creator tool proves this with an exact decision diagram rather than sampling and computes worst-case draw, index, texture, and memory use. A decision diagram may contain at most 250,000 nodes.

**Texture overrides on keep (`keep-material-textures`)**

```json
{"operation": "keep", "material_overrides": [
  {"material_slot": 0, "material_name": "Native material name", "textures": [0]}]}
```

The slot is located in the target component's native material table and the name must match. A slot cannot be repeated and every entry needs at least one texture. `hide` and `replace` cannot use this field.

**Saving and reading**

- Settings are stored as `group:choice&group:choice`, for example `outfit:a&part21:on`; omitted groups use their defaults. 1.0 stores an appearance ID.
- Packages retain every candidate resource instead of trimming to the install-time selection, so one package can switch A → B → A.
- The runtime reads, decompresses, and uploads only payloads used by the current selection.

## 7. Mesh

| Field | Rule |
| --- | --- |
| `vertex_count` | 1..1,048,576 |
| `index_size` | 2 or 4 (UInt16/UInt32); each index is below `vertex_count` |
| `streams` | Exactly 3 `{"stride":N,"payload":ID}` entries; stride 1..64 and payload length `vertex_count × stride` |
| `attributes` | `[semantic, format, dimension, stream, offset]`, using Unity `VertexAttributeDescriptor` enum values |
| `bones` | Local bone palette, 1..256 entries |
| `draws` | Ordered draw segments, at most 256 per component |
| 1.0 `indices`, `index_count` | One index payload for the whole mesh |

**Vertex attributes**

- At most 16 attributes; semantic 0..13, unique; dimension 1..4; stream 0..2.
- Format 0..11 element sizes are `4,2,1,1,2,2,1,1,2,2,4,4` bytes. Attributes are tightly packed in declaration order within a stream; `offset` is the sum of preceding sizes and the sum equals stride.
- The runtime builds the mesh from the package declarations; its layout need not match the original Mesh.

**Skinning stream (stream 2):** only these three layouts are allowed, and BlendIndices must be last:

| Stride | Declaration | Meaning |
| --- | --- | --- |
| 4 | `[13,6,4,2,0]` | UInt8×4 bone indices, rigid skinning; only the first index is used with weight 1 |
| 12 | `[12,4,4,2,0]`, `[13,6,4,2,8]` | UNorm16×4 weights + UInt8×4 indices |
| 32 (1.2+) | `[12,0,4,2,0]`, `[13,10,4,2,16]` | Float32×4 non-negative finite weights + UInt32×4 indices |

Each vertex's weights sum to 1±0.01. The runtime also limits the number of influences; see the runtime document.

**Bone palette**

```json
{"component": 0, "index": 17, "name": "Bip001_Head"}
```

Vertex indices refer to this table. Each donor component, index, and name must match the target table and actual game resource. The table has at most 256 entries. Source 16-bit indices may be remapped offline; more than 256 entries is rejected, never truncated or split automatically. Bone and material donors may be different components.

**Draws**

1.0 uses one mesh-level index payload:

```json
{"start": 0, "count": 300, "material_component": 1, "material_slot": 0,
 "material_name": "M_actor_example_body_01", "textures": [0, 1]}
```

Draws must be ordered, gapless, and cover the full index stream; each `count` is a positive multiple of 3.

In 1.1+, each draw has its own index payload and the mesh-level `indices`/`index_count` fields are not used:

```json
{"when": {"eq": ["part21", "on"]}, "indices": 7, "count": 300,
 "material_component": 0, "material_slot": 0, "material_name": "Native material name", "textures": [0]}
```

- The `indices` payload contains exactly `count × index_size` bytes. Draws with identical geometry may share a payload.
- The runtime concatenates selected draws in manifest order and recalculates each `start`.
- Do not merge an index payload unique to an unselected draw into another draw; the runtime reads selected payloads independently.

**Materials:** `material_component` and `material_slot` identify a native material slot on the donor component, and `material_name` must match. The runtime copies the original material, preserving its shader, keywords, parameters, and sampling state; shader programs are not included in BEM.

**Texture references:** `textures` refers to the top-level texture table; 1.2+ may also use `{"slot":"slot-id"}`. A draw cannot reference one entry twice or assign two values to the same original texture.

## 8. Texture

| Field | Rule |
| --- | --- |
| `width`, `height` | 1..32768; BC formats require dimensions divisible by 4 |
| `mips` | 1..16; payload contains the complete declared mip chain, largest to smallest, with halved dimensions rounded down to at least 1 and no row padding |
| `format` | Unity TextureFormat, listed below |
| `srgb` | Color-space flag |
| `original_name` | Complete name of the original Texture object replaced on the donor material |
| `payload` | Data Payload ID |
| optional `semantic` | `normal` for a normal map |
| optional `normal_encoding` | `xy-unorm` or `xyz-unorm`; required for Android normal-map conversion |

| Format | Meaning | Note |
| --- | --- | --- |
| 4 | RGBA32 | 4 bytes per pixel, R/G/B/A order |
| 10 | DXT1 (BC1) | |
| 12 | DXT5 (BC3) | |
| 25 | BC7 | |
| 26 | BC4 | |
| 27 | BC5 | |
| 48 / 49 / 50 | ASTC 4×4 / 5×5 / 6×6 | Usually generated by the Android installer; usually unsupported by desktop GPUs |
| 63 | R8 | 1 byte per pixel; `srgb` must be false |

- BC6H is unsupported.
- The runtime replaces textures by exact `original_name` matching on the copied material. A source DDS filename is not a substitute.
- Identical pixel data assigned to different names can share one payload.

## 9. 1.2 additions

### Texture slots (`texture_slots`, capability `texture-slots`)

Recoloring can select a replacement for one original texture without duplicating a draw:

```json
"texture_slots": [
  {"id": "dress_d", "candidates": [
    {"when": {"eq": ["colour", "c0"]}, "texture": null},
    {"when": {"eq": ["colour", "c1"]}, "texture": 7}
  ]}
]
```

- Draw `textures` and keep `material_overrides[].textures` may contain `{"slot":"dress_d"}`.
- Exactly one candidate must hold for every legal combination; `null` retains the original texture.
- All non-null candidates for one slot must replace the same original texture (`original_name`). Within one draw or keep list, fixed textures and slot replacements must target different original textures.

### Bone-name aliases (`bone_name_aliases`, capability `resource-bone-aliases`)

Use aliases when the same bone has different names in scene and detail resources, for example because of a game skeleton spelling error.

```json
{"id": 13, "mesh_name": "S_actor_typhoea_cloth_01_lod0",
 "bone_names": ["...", "skirt_base_L_c_03_jnt", "..."],
 "bone_name_aliases": [{"index": 24, "resource": "world", "name": "skirt_base_R_c_03_jnt"}]}
```

- `bone_names` contains the canonical names and the palette refers to those names.
- Each alias contains `index`, `resource` (`world` or `ui`), and `name`; it cannot equal the canonical name, and one `(index, resource)` may appear only once.
- The intended runtime behavior is to accept the canonical name or a declared alias for the corresponding resource and still bind the bone object at that index. The current native parser stores aliases without retaining the `resource` dimension, so resource-specific isolation is not fully enforced yet. A later game name correction does not require rebuilding a published package once the runtime path uses the alias.

### 32-byte skinning

See the skinning table in section 7. This supports native meshes that already use uncompressed skinning, such as Typhoea's `cloth_01`.

## 10. 1.3 shape parameters

Shape evaluation follows EFMI ShapeKey semantics: `position = original position + sum(parameter deltas)`. Only positions change; original normals, tangents, UVs, weights, bindposes, and bones remain. The package contains data, not INI files, shaders, or UI scripts.

```json
"parameters": [
  {"id": "body", "name": "Body", "min": 0, "max": 1000, "neutral": 0, "default": 0, "step": 1}
],
"mesh_deformations": [
  {"mesh": 0, "parameter": "body", "frames": [
    {"value": 0, "neutral": true},
    {"value": 1000, "payload": 12, "count": 350, "encoding": "sparse-position-f32"}
  ]}
]
```

**Parameters**

- At most 64. Values are integer ticks from 0..1000; `min < max`, `step ≥ 1`, and `max`, `neutral`, `default`, and all frame values align to `min` by `step`.
- `neutral` is the zero-deformation coordinate; `default` is the author's initial preference and may differ.
- Optional `available_when` uses option-group conditions and may refer only to discrete choices, not other sliders. An unavailable parameter evaluates at `neutral` while retaining its saved value.
- Settings are stored as `id:tick&id:tick` in manifest order. Invalid or unknown values are sanitized and never used to index data.

**Deformation channels**

- At most 4096. `mesh` is a mesh index and `parameter` is a parameter ID; each `(mesh, parameter)` is unique.
- A channel has 2..64 strictly increasing frames covering `min` and `max`, with exactly one `{"value": neutral, "neutral": true}` frame, which has no payload.
- Other frames contain exactly `value`, `payload`, `count`, and `encoding`; encoding is `sparse-position-f32`. `count` may be 0 and cannot exceed the mesh vertex count.
- A deformation mesh must declare Float32 XYZ positions (semantic 0, format 0, dimension 3). A channel applies only when that mesh is selected for replacement. Shared meshes must produce equivalent geometry for the same settings.

**Delta payload**

| Offset | Contents |
| --- | --- |
| 0 | UInt32: JSON description length J (1..4096) |
| 4 | J-byte JSON: `{"count":N,"encoding":"sparse-position-f32"}` |
| 4+J | N records, each 16 bytes: UInt32 vertex index + Float32 ΔX/ΔY/ΔZ |

- Total length is exactly `4 + J + 16N`. Indices are below the vertex count and unique within a frame; deltas are finite. Missing records mean zero delta.
- Deltas use the mesh's coordinate system and units. They are absolute offsets from the original position, not offsets from the previous frame.

**Evaluation:** an exact frame uses that frame's delta. Between adjacent frames a and b, linearly interpolate their absolute deltas using `(b-v)/(b-a)` and `(v-a)/(b-a)`. Sum all channel results and add them once to the original position.

## 11. Limits summary

| Item | 1.0 | 1.1 | 1.2 / 1.3 |
| --- | --- | --- | --- |
| Appearances / option groups | 1..64 appearances | 64 groups | 64 groups |
| Choices per group | — | 16 | 64 |
| Component + draw + texture-slot candidates | — | 512 | 4096 |
| Texture bindings in one selection | 32 | 32 | 64 |
| One texture (complete mip chain) | 64 MiB | 64 MiB | 256 MiB |
| Decoded data / resident resources in one selection | 512 / 512 MiB | 768 / 768 MiB | 1536 / 1536 MiB |
| Payload directory entries | 4096 | 4096 | 16384 |

All versions share these limits: 2 GiB container; 4 MiB manifest; 512 MiB per payload; 1..64 components; 256 palette bones; 256 draws per component; 16,777,216 selected indices; 1,048,576 vertices; 64 shape parameters; and 4096 deformation channels.

Budgets apply to one selection and are not the same as the actual GPU peak. The 1.3 budget conservatively counts all frames for each channel. See the runtime document for actual loading cost.
