# BEM 1.3 — authored position parameters

BEM 1.3 extends the BEM 1.2 composable manifest with continuous, author supplied mesh position deformation. Container framing, compression, native vertex declarations, original materials, bone identities, palette and draw rules remain those of BEM 1.2. The header major is `1`, minor `3`. BEM 1.0–1.2 packages remain readable and writers keep the oldest version capable of expressing a project's features.

This is the same position morph model used by EFMI shape keys: `position = immutable_base_position + sum(parameter_delta)`. The initial implementation preserves the base normals, tangents, UVs, weights, bind poses, bone names and indices. A package carries data, never executable INI, shader programs, GUI code or expressions.

## Manifest additions

`required_capabilities` may include `body-parameters` and `mesh-position-deltas`. Their declarations are mandatory when the corresponding tables are nonempty. Declaring an implemented but unused capability is allowed. Any declaration of these capabilities requires a 1.3 header, even if unused.

```json
{
  "parameters": [
    {"id":"body","name":"Body size","min":0,"max":1000,"neutral":0,"default":0,"step":1}
  ],
  "mesh_deformations": [
    {"mesh":0,"parameter":"body","frames":[
      {"value":0,"neutral":true},
      {"value":1000,"payload":12,"count":350,"encoding":"sparse-position-f32"}
    ]}
  ]
}
```

`parameters` has at most 64 entries. Each entry has a unique stable `id`, a nonempty UTF-8 `name` of at most 256 bytes, UInt32 `min`, `max`, `neutral`, `default` and `step`, and optional `available_when`. Ticks are integers in `0..1000`, `min < max`, `step >= 1`. `max`, `neutral`, `default` and frame values must be aligned to `min` by `step`. `neutral` is the zero deformation coordinate; `default` is the author's initial preference and may differ. `available_when` uses existing BEM option-group conditions and references option choices, not other sliders.

An unavailable parameter evaluates to its `neutral` without deleting the saved preference. Invalid or unknown saved values must never index geometry or payload tables. Runtimes sanitize saved preferences to known, aligned values; creator reference APIs reject invalid values. Canonical wire/config selection is `id:tick&other:tick` in manifest order, separately from finite option choices. Native metadata exposes `default_parameters` and `parameter_groups_json` containing the parameter descriptor array.

`mesh_deformations` has at most 4096 channels. `mesh` is the existing mesh array index, `parameter` a declared parameter ID, and `(mesh, parameter)` is unique. Each channel contains 2..64 strictly increasing frames, covers `min` and `max`, and contains exactly one `{"value":neutral,"neutral":true}` frame. The neutral frame has no payload/count/encoding. Every other frame has exactly `value`, `payload`, `count`, `encoding`. Nonneutral `count` may be zero for an author declared zero-effect endpoint. Counts are bounded by the mesh's vertex count. `encoding` is exactly `sparse-position-f32`.

For a slider-only 1.3 package, `option_groups` may be an empty array; component/draw rules then use unconditional predicates. Older 1.1/1.2 versions still require a finite group.

The deforming mesh must declare semantic `0` as Float32 XYZ (`format:0`, `dimension:3`) in an existing valid vertex stream. Its byte offset and stride come from that declaration; no fixed position stream or stride is inferred. A channel has no visibility side effects: it participates only when that mesh is selected for replacement. Several target components referencing a shared mesh must receive equivalent geometry for the same effective preferences.

## Delta payload

Payloads use the existing BEM directory; no payload type or flag is added. The frame's `encoding` identifies its role. Decoded bytes are:

| Offset | Contents |
| --- | --- |
| `0` | UInt32 little endian byte length `J` of the JSON descriptor |
| `4` | Exactly `J` UTF-8 JSON bytes: `{"count":N,"encoding":"sparse-position-f32"}` |
| `4+J` | `N` records, 16 bytes each: UInt32 little endian vertex index, then Float32 little endian delta X/Y/Z |

`0 < J <= 4096`; descriptor keys are exactly `count` and `encoding` with no duplicate keys. Descriptor count/encoding must equal the frame. Total decoded length is exactly `4 + J + 16*N`. Every vertex index is below the mesh vertex count and unique within the frame. Every delta is finite. Creator output sorts vertex indices; readers also accept unique unsorted records. Absent vertex records mean a zero delta. Original native stream bytes are authoritative base positions. Container deduplication remaps frame payload references along with stream/index/texture references.

Delta units and axes are the native replacement mesh's units and axes. Deltas are absolute offsets from the immutable base, never incremental offsets from a previously deformed result. Bone identity or a known character does not provide missing author shape data.

## Evaluation

At an exact frame coordinate, apply its delta with weight 1. Between adjacent frame values `a` and `b`, linearly combine their absolute deltas using `(b-value)/(b-a)` and `(value-a)/(b-a)`. Neutral frames are zero. Add each channel's contribution to the immutable base once. Never accumulate from the last slider output. Numeric results must remain finite Float32; reject a failed build before publishing its replacement mesh. Preserve the prior valid resource transaction on failure.

All geometry and texture limits inherited from 1.2 remain. Runtime selected decoded/resident budgets include the selected interpolation endpoint payloads. The creator's symbolic proof conservatively accounts for all frames of each active channel, so its report is an upper bound and can reject large multi-frame author projects earlier than a particular runtime selection. Sliders are not enumerated as thousands of complete option combinations. Container, byte-range and numeric safety checks remain necessary even when the developer compatibility-validation option is enabled.

## Runtime and author workflow

The current backend builds CPU positions before native mesh upload, preserving base TBN as EFMI does. A saved preference applies on the next normal resource delivery while model hot switching is enabled; otherwise it applies on the next game launch. The format does not itself promise live every-frame dragging, a GPU deformation backend, automatic cloth coordination or target creation for original models.

Use the existing `.bemproj.json` export task with optional `deformations` path. Low level unpacked projects retain these tables and raw payloads without conversion. See [creator workflow](BEM_V1_3_CREATOR_GUIDE.md) for target positions, sparse deltas, official EFMI buffer bindings, portable tasks and the runnable synthetic example.
