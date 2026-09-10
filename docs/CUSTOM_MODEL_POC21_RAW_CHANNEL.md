# BetterEndfield.CustomModel PoC-2.1: guarded Mesh raw-channel path

Date: 2026-09-10

## Why PoC-3 was abandoned

The PoC-3 experiment attempted to preserve EFMI's complete GPU vertex streams by obtaining `UnityEngine.GraphicsBuffer` objects from `Mesh.GetVertexBufferImpl()` and writing them directly. On the validated Endministrator (F) C9 target the game crashed during the first GraphicsBuffer access, before any custom vertex data was uploaded.

That route is permanently out of scope for the normal CustomModel runtime. The current runtime does not call `GetVertexBufferImpl`, `GraphicsBuffer.InternalSetNativeData`, or any equivalent graphics-resource API.

## New semantic finding from EFMI Tools

EFMI Tools does not treat `ENCODEDDATA0` as an unrelated custom vertex channel. Its Endfield semantic remap maps the source character vertex declaration:

```text
NORMAL0 / R32_FLOAT / input slot 0
```

to the Blender-facing EFMI abstraction:

```text
ENCODEDDATA0 / R32_UINT / input slot 0
```

The same 32 bits contain the octahedral normal, encoded tangent scalar, packed-data flag and bitangent sign. Decoding those bits to `UnityEngine.Vector3[] normals` changes the source vertex declaration from 4 bytes (`Float32 x1`) to 12 bytes (`Float32 x3`) and discards the original shader-facing packed representation.

EFMI Tools also remaps the character source semantic:

```text
TEXCOORD4 / R8G8B8A8_SNORM / input slot 1
```

to EFMI `COLOR0` for Blender-facing processing. Therefore the runtime should restore these bytes as `TexCoord4/SNorm8 x4`, not as ordinary Unity `Color`.

## PoC-2.1 strategy

PoC-2.1 remains entirely at the Unity `Mesh` / `SkinnedMeshRenderer` layer.

Stable paths retained from PoC-2:

- `Mesh.vertices` for POSITION0.
- `Mesh.uv` for TEXCOORD0.
- legacy `Mesh.boneWeights` for four-weight skin bookkeeping.
- original `Mesh.bindposes` array.
- `Mesh.SetTriangles(Int32[])` for the combined C9 index buffer.
- original renderer `bones[]`, `rootBone`, materials and shader.

Exact shader-facing channels:

- packed TBN bytes -> `VertexAttribute.Normal`, `VertexAttributeFormat.Float32`, dimension 1.
- character packed bytes -> `VertexAttribute.TexCoord4`, `VertexAttributeFormat.SNorm8`, dimension 4.

These two channels use Unity's non-generic CPU Mesh binding `SetNativeArrayForChannelImpl(...)`. This binding is the native endpoint behind Unity's NativeArray channel setters. It copies CPU data into the Mesh and does not retrieve or expose a `GraphicsBuffer` object.

## Runtime guard

Before creating or assigning a replacement Mesh, PoC-2.1 requires the live original C9 Mesh to report the validated semantic declaration through public metadata-only Mesh getters:

| Attribute | Expected format | Dimension |
|---|---:|---:|
| Position | Float32 | 3 |
| Normal | Float32 | 1 |
| TexCoord0 | Float32 | 2 |
| TexCoord4 | SNorm8 | 4 |
| BlendWeight | UNorm16 | 4 |
| BlendIndices | UInt8 | 4 |

Stream and offset are logged for evidence but are not currently required to match a hard-coded layout. Shader attributes are restored by semantic/format/dimension rather than by assuming a particular Unity stream packing policy.

The following method contracts are optional at module startup:

- `Mesh.HasVertexAttribute`
- `Mesh.GetVertexAttributeDimension`
- `Mesh.GetVertexAttributeFormat`
- `Mesh.GetVertexAttributeStream`
- `Mesh.GetVertexAttributeOffset`
- `Mesh.SetNativeArrayForChannelImpl`

If any are stripped from the current client, the module still loads but F9 refuses the PoC-2.1 replacement. There is deliberately no GraphicsBuffer fallback.

If the original C9 declaration differs from the validated EFMI semantic map, F9 logs the actual declaration and refuses to write. If either raw channel write raises a managed exception or read-back metadata does not retain `Normal Float32x1` plus `TexCoord4 SNorm8x4`, the newly created Mesh is destroyed before the renderer is modified.

## BEM PoC-2.1 payload

The temporary PoC file keeps the existing 36-byte header shape but uses magic `BEMPC21\0` and version 21.

Payload order:

```text
Float32x3 POSITION
Float32x2 UV0
4-byte packed NORMAL bits
4-byte TEXCOORD4 SNorm8 data
Float32x4 normalized legacy weights
UInt8x4 renderer-local bone indices
UInt16 triangle indices
```

For the validated Endmin Casualwear C9 sample:

```text
vertexCount = 12873
indexCount = 59073
maxBone = 155
fileSize = 736086 bytes
```

All packed Normal values in the sample have bit30 set, and the maximum triangle index is 12872.

## What this PoC proves if successful

A successful in-game test would establish that EFMI character geometry can be reconstructed without D3D11/Vulkan hooks and without GraphicsBuffer access while preserving Endfield's shader-specific packed vertex semantics. It would also give the BEM runtime a platform-neutral primitive suitable for the Windows and Android paths.

The next step after that is not texture replacement yet. First extend the same semantic-preserving importer/runtime path across C0-C10 and LOD variants, while keeping Merged Skeleton and GPU ShapeKey as explicit later capabilities.
