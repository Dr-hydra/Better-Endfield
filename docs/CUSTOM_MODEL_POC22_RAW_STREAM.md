# BetterEndfield.CustomModel PoC-2.2: exact source layout via engine bindings

Date: 2026-09-11

Supersedes `CUSTOM_MODEL_POC21_RAW_CHANNEL.md`, whose central premise turned out
to be wrong.

## What PoC-2.1 got wrong

PoC-2.1 assumed the client could not express the Endfield character vertex
layout, because IL2CPP metadata resolution reports `Mesh.SetVertexBufferParams`,
`Mesh.SetVertexBufferData`, the writable `MeshData` API and the per-attribute
`GetVertexAttribute*` getters as missing. It therefore tried to smuggle the
packed channels through whatever Unity's default per-channel format policy would
accept.

That failed in a way worth recording, because each step looked plausible:

1. `SetNativeArrayForChannelImpl` accepted `Normal Float32x1` and
   `TexCoord2 SNorm8x4` without raising, then silently coerced them to
   `Float32x3` and `Float32x4`. Unity derives the layout from its own defaults
   when no explicit layout was declared.
2. Carrying the packed bits inside wider Float32 channels produced a mesh that
   assigned cleanly (`applied=1`) and rendered nothing.
3. A one-unit box over the torso, rigid-bound to a real bone with the original
   bindposes and material, also rendered nothing — so the failure had nothing to
   do with the EFMI data.

Offline analysis had already cleared the data itself: triangle winding agrees
with the decoded normals (mean dot +0.9754), `ENCODEDDATA0` decodes as a 10+10
bit snorm octahedral normal in bits 0-19, the seven EFMI draw ranges are
contiguous with base vertex 0 and sum to exactly 59073 indices, and the skin
weights sum to 1 with max bone 155 against a 156-entry palette. The live
bindpose palette is valid (no zero, identity or non-finite matrices) and the
original mesh's bounds match the replacement's to within the difference you
would expect between two outfits.

## The actual constraint, and why it is not one

Managed metadata stripping removes the C# signature of an engine binding. It
does not remove the binding. `UnityPlayer.dll` registers every engine icall by
name, independently of what the game's managed code references, and
`GameAssembly.dll` exports `il2cpp_resolve_icall`.

Probed in the live client:

```text
[negative control] UnityEngine.Mesh::BetterEndfieldNoSuchIcall -> NULL
[positive control] UnityEngine.Mesh::get_vertexCount           -> unityplayer.dll
[layout]           UnityEngine.Mesh::SetVertexBufferParamsFromPtr -> unityplayer.dll
[upload]           UnityEngine.Mesh::InternalSetVertexBufferData  -> unityplayer.dll
[upload]           UnityEngine.Mesh::InternalSetIndexBufferData   -> unityplayer.dll
[submesh]          UnityEngine.Mesh::SetSubMesh_Injected          -> unityplayer.dll
[stripped getters] UnityEngine.Mesh::GetVertexAttributeDimension  -> unityplayer.dll
```

21 of 22 names resolved; the only NULL was the deliberate negative control.

## ABI validation

The native signatures come from Unity's published 2021 LTS `Mesh.bindings.cs`
and are treated as unverified until proven. `HasExplicitThis` bindings take the
instance pointer as the first native argument.

Two checks run behind F7 before any writer is trusted:

- Read-only: the raw `GetVertexAttributeDimension/Format/Stream` are compared
  against the managed enumeration API on the live C9 mesh. Result: 6 of 6
  attributes match, 0 differ.
- Writer: `SetNativeArrayForChannelImpl` exists in both managed metadata and the
  icall table, so the same channel write is issued through both paths onto two
  unused attributes of a scratch Mesh we own, and the resulting declarations are
  compared. Result: identical.

The same probe reported, for the first time from inside the client:

```text
vertexBufferCount=3
  stream 0 stride=16
  stream 1 stride=12
  stream 2 stride=12
```

which independently confirms the strides previously only inferred from the EFMI
buffer sizes.

## PoC-2.2 runtime

```text
new Mesh()
SetVertexBufferParamsFromPtr(vertexCount, 6 VertexAttributeDescriptors)
InternalSetVertexBufferData(stream 0 | 1 | 2, raw EFMI bytes)
SetIndexBufferParams(indexCount, IndexFormat.UInt16)
InternalSetIndexBufferData(raw EFMI index bytes)
set_subMeshCount(1) + SetSubMesh_Injected(0, whole index range)
set_bindposes(original palette)
RecalculateBounds
```

The declared layout is exactly the source declaration:

| Attribute | Format | Dimension | Stream |
|---|---|---:|---:|
| Position | Float32 | 3 | 0 |
| Normal | Float32 | 1 | 0 |
| TexCoord0 | Float32 | 2 | 1 |
| TexCoord2 | SNorm8 | 4 | 1 |
| BlendWeight | UNorm16 | 4 | 2 |
| BlendIndices | UInt8 | 4 | 2 |

Strides are therefore 16/12/12, matching the original.

Nothing decodes, renormalizes or repacks. The legacy `Mesh.boneWeights` path is
gone: skin data reaches the shader from the declared `BlendWeight`/`BlendIndices`
channels in stream 2, at full UNorm16 precision.

Note that EFMI reports the packed character channel under its source D3D
semantic `TEXCOORD4`, while the live Unity declaration calls the same bytes
`VertexAttribute.TexCoord2`. The two index numbering domains are unrelated and
must not be assumed equal.

## Verify before assigning

`MatchesValidatedC9Declaration` is one function used twice: as the gate on the
live source mesh, and again on the mesh we build before it is handed to the
renderer. It checks semantic, format, dimension and stream per attribute plus
`vertexBufferCount` and all three strides, using the getters F7 validated.

Because the writer signatures are inferred rather than proven, this ordering is
the safety property that matters: a wrong writer ABI surfaces as a logged
refusal and a destroyed scratch Mesh, not as a corrupted renderer. Every raw
call is additionally wrapped in its own `__try`, so a bad signature is a caught
access violation rather than a dead client.

## BEM PoC-2.2 payload

Magic `BEMPC22\0`, version 22, same 36-byte header shape. Payload order:

```text
stream 0   vertexCount * 16   POSITION f32x3 + ENCODEDDATA u32
stream 1   vertexCount * 12   TEXCOORD0 f32x2 + packed snorm8x4
stream 2   vertexCount * 12   BLENDWEIGHTS unorm16x4 + BLENDINDICES u8x4
indices    indexCount * 2     uint16 triangle list
```

For the validated Endmin Casualwear C9 sample:

```text
vertexCount = 12873
indexCount  = 59073   (19691 triangles)
maxBone     = 155
fileSize    = 633102 bytes
```

All four sections are byte-for-byte identical to the corresponding
`Component9_VB0/VB1/VB2/IB.buf` in the EFMI mod.

## Diagnostic hotkeys

| Key | Action | Risk |
|---|---|---|
| F9 | apply/roll back the PoC-2.2 replacement | writes to the renderer |
| F10 | apply a trivial box through Unity's default layout policy — the historical control that renders nothing | writes to the renderer |
| F11 | `BakeMesh` the live renderer into our own Mesh; returns 0 vertices because the source is `isReadable=false` | read-only on the game |
| F8 | read renderer `GraphicsBuffer` metadata; both buffers are null, so Unity is not skinning these characters | crashed the client in PoC-3 |
| F7 | validate the native ABI of the stripped engine bindings | read-only on the game |

## What remains after this

Successful in-game rendering would establish that EFMI character geometry can be
reconstructed with Endfield's exact packed vertex semantics without any
graphics-API hook, using only engine bindings reachable through
`il2cpp_resolve_icall` — a platform-neutral primitive suitable for the Windows
and Android paths.

The next step after that is still not texture replacement. Extend the same path
across C0-C10 and the LOD variants first, keeping Merged Skeleton and GPU
ShapeKey as explicit later capabilities.
