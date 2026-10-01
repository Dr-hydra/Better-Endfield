# EIEM upstream snapshot

- Source: https://github.com/Sasye/EIEM (AGPL-3.0, see LICENSE.EIEM)
- Commit: 1bc9baa (2026-09-29, "feat(vmd): 添加DirectVmd膝盖弯曲方向混合")
- `upstream/` holds the files as copied. Local changes are limited to the patches
  listed below so future upstream commits can be re-applied with a diff.

Not copied: `eiem.cpp`, `init.h`, `trojan.h`, `gui*.h`, the cloth enhancement
(`cloth/core/cloth_collision.h`, `bonecloth/`, `collision/`, `assets/`,
`generated/`), MUS4/muscle UI, audio playback, update checks. `trojan.h` and `init.h` are kept only as
reference (not compiled). The parts of them DirectVmd needs (backend
enter/leave, FinalIK/grounder/leg callbacks, FindFloor sampling, IL2CPP
resolution, worker loop) are re-implemented in `eiem_slot.inc`.

## Layout

- `eiem_slot.inc`: upstream headers + glue, compiled four times
  (`eiem_slot0..3.cpp`), each inside its own namespace, so every dancing
  character has private copies of EIEM's file-level state (ghost rig, worker,
  clip, face caches). `eiem_slot.h` is the per-slot function table.
- `eiem_body.cpp`: installs each hook once through the Host and offers every
  callback to the slots; EIEM's own ownership checks make only the slot that
  drives that character act. SkeletalMorphCore instances are mapped to slots by
  their first face bone (under the slot's rig root) instead of EIEM's
  "first SMC seen" rule.
- `eiem_body.h`: the only header `module.cpp` sees.

## Local patches (search for `BE-PATCH`)

| File | Patch | Why |
| --- | --- | --- |
| `il2cpp_api.h` | `Hook()` calls `g_eiemCreateHook` (Better Endfield Host `create_hook`) instead of MinHook | Host owns every hook; conflicts are reported instead of double-hooking |
| `il2cpp_api.h` | `Log` forward declaration is `static` | EIEM is one TU inside the Camera DLL |
| `globals.h` | `Log` is `static`, capped at 32 MiB | same; DirectVmd diagnostics are verbose |
| `vmd_parser.h` | `LoadVmd` opens the path as UTF-8 via `_wfopen_s` | library paths may contain CJK characters |
| `smc_face.h` | `morphMappingNames` has no fixed-offset fallback | a missing field disables the name table instead of reading 0x38 |
| `smc_face.h` | no permanent eye look-at disable when an SMC is first confirmed | the ghost rig saves/disables/restores it for the playback owner |
| `ghost_rig.h` | `g_beFixedAnchor*`: optional shared stage origin, no first-frame placement | squad playback keeps the formation authored in multi-dancer VMDs |
| `cloth.h` | main-thread check uses `g_beMainThreadId` instead of the game window | no window handle in the slot |
| `cloth.h` | includes `compat/cloth_be.h` instead of `cloth/core/cloth_collision.h`; service gated by the cloth mode; freeze mode and one-time collider report in the tick loop | custom models replace meshes, so the outfit-specific enhancement is not used |
| `smc_face.h` | mouth aliases 「ワ」→あ, 「口横広げ」→い; grin morphs 「にやり」「にやり２」→ `mouth_happy_s/m_ctrl` | face VMDs made for models without あいうえお still move the mouth |

`compat/cloth_be.h` (not an upstream file) stubs the cloth enhancement in its
disabled state, so the playback gate stays idle, and adds the cloth mode
(game / stable / freeze) and the `[BE-CLOTH]` component report.

## Behavioural differences from EIEM (in eiem_body.cpp)

- No RVA hook fallbacks and no `SafeOff` defaults for the player controller path:
  `g_playerController` stays null; the Camera module supplies entity, Animator and
  MovementComponent (`Entity.get_movementComponent`).
- `MovementComponent.m_bipedIK` / `currentFloor` start unresolved (-1) and are only
  set from metadata. `ComputeFloorDist` is used only after the `FindFloorResult`
  field layout (bool@0, bool@1, float@8, Vector3@0x10, Vector3@0x1C) is verified.
- SkeletalMorphCore hooks are installed only when every SMC field EIEM reads is
  found by name.
- The worker follows the Camera module's MMD director clock; timeline jumps are
  published as EIEM seeks and loop wraps as loop cycles. EIEM's own audio and
  camera followers are no-ops (music: Music module; camera: free camera VMD).
- Not hooked: `MovementComponent.Tick`, `AnimatorMono` probes, `SetMainCharacter`,
  transform write probes.
- Squad playback (Better Endfield addition, squad access modelled on
  Endfield-Poser `game/squad.h`: `GameInstance.get_player` → `GamePlayer.squadManager`
  → `SquadManager.GetMemberBySlot`): up to four slots, one shared clock and origin.

## Updating

1. Copy the new upstream files over `upstream/`.
2. Re-apply the `BE-PATCH` hunks above (`git diff` of the previous snapshot).
3. Diff upstream `trojan.h`/`init.h` against the previous snapshot and port changes
   that touch DirectVmd into `eiem_slot.inc`.
4. If upstream adds a system header include, add it to the pre-include list at
   the top of `eiem_slot.inc` (headers must not be first included inside the
   slot namespace).

## Android / ARM64 adapter (2026-10-01)

`eiem_slot.inc` selects `compat/android_slot.inc` only on Android. The PC
runtime, including its real SEH guards, remains in the other branch. Android
does not redefine SEH as C++ exceptions and does not execute the PC's object,
array, NativeArray, or MethodInfo offset reads. Reference `init.h`/`trojan.h`
remain uncompiled and unchanged.

This is a managed DirectVmd adapter, not a claim that the PC GhostRig/FinalIK
implementation has been ported verbatim. It shares `vmd_parser.h`,
`direct_vmd_pose.h`, `direct_vmd_source_sample.h`, source basis conversion,
semi-standard bone folding, retargeting, reach projection, toe aim and knee
direction math with PC. It keeps four independent clip/sampler/state instances.
The Android source configuration currently uses the canonical EIEM reference;
the PC source-preset/PMX configuration command interface is not wired here.

`compat/android_managed_features.inc` resolves concrete classes, fields and
method signatures through `BE_HostApiV1`. Retained objects and temporary boxes
are pinned with host GC handles. Box payloads come from host `object_unbox`;
there are no `+16`/array-data-offset substitutes. Generic List/NativeArray
methods are resolved by name and complete signatures on their actual managed
instances using named IL2CPP metadata exports, then invoked through the host.
Missing or ambiguous metadata closes the affected feature. `il2cpp_free`
releases names returned by `il2cpp_type_get_name`. The optional named
`il2cpp_field_set_value` export restores distinct cloth property/serialized
originals; without it, Stable refuses components requiring that restoration.

| Capability | Actual Android path and boundaries |
| --- | --- |
| Body FK, root, fingers, eyes | Worker calls shared DirectVmd sampling; main-thread `PublishClock` calls `Apply`, evaluates owned POD poses and writes Unity transforms via managed setters. `Avatar.humanDescription.skeleton` supplies neutral positions/rotations/scales; an animated entry pose is not used as a bind fallback. Missing required humanoid/Avatar metadata rejects Start. Optional missing bones are skipped. |
| Twist bones | Hierarchy/name lookup finds the PC's eight `Bip001_*Twist*` transforms. Shared signed-twist extraction and axis-angle math distribute each source twist 50/50 over its verified pair. Natural Avatar local rotations provide the bind; entry rotations are restored on Stop. Missing pairs disable that channel. |
| Leg / toe IK and knee mix | `SolveLeg` is an independent POD two-segment solve using shared reach projection and knee direction blending; toe tracks use shared `DirectVmdSolveToeAim`. It honors sampled foot IK switches. It does not call PC native FinalIK solver layouts. A host hook on named `RootMotion.dll / RootMotion.FinalIK.BipedIK.UpdateSolver` skips only the active actor's solve, identified through managed transform ancestry; other actors pass through. The hook is required so later native solves cannot overwrite the adapter's pose. |
| Game SMC expressions | Entity `get_skMorphCom` and named component `m_core` identify the actor's SMC. Shared PC vowel/alias rules and 21 expression aliases become managed `MorphCtrlValue` lists and `SkeletalMorphPose`; controllers absent from the avatar's `morphMappingNames` are skipped. The adapter calls SMC `SetPose`, `Update(0)` and `CompleteJob`, reserves/pauses metadata-resolved `DefaultEx2`, and pauses emotion progression. It snapshots seven tracker/transition buffers plus original override/pause flags and restores them on Stop. Buffer identity and initialization are checked before restore; rebuilt/disposed buffers are not accessed. Native SMC job detours are not installed. Actual game SMC tick ordering remains unverified on device. |
| Cloth Game / Stable / Freeze | Game performs no takeover. Stable uses named simulate-weight/pose-ratio setters; Freeze disables the component. Both capture serialized values, public property values and enabled state, park verified non-humanoid root anchors at Avatar local poses, and restore originals. Restoration reads values back; failures retain originals and block a new session/mode change. Missing root-list/Avatar metadata closes the anchor subfeature explicitly. PC native caller-address weight guarding, process startup/LOD auditing, collider reporting and collider adjustment are not replicated. |
| Terrain | `compat/android_terrain.inc` resolves `MovementComponent.ComputeFloorDist` (seven exact argument types), with named `FindFloor` as a fallback. A verified nested value class `FindFloorResult` is allocated/pinned by the host; its unboxed payload is the managed out/ref argument. `Clear`, `IsWalkableFloor`, `get_floorNormal`, and named `isHit`/`floorDist`/`floorHit` plus `RaycastHit.m_Point` read the result. Five probes per foot feed the shared plane clustering/contact/height-filter state; root height, IK/toe targets, normals and camera terrain offset receive its output. Pause holds state; seeks reset it. No raw struct size/offset assumptions or native Grounder callbacks are used. This is the shared POD terrain solver with game floor queries, not a port of PC's native Grounder hybrid cache/height heuristics. Metadata/boxing/query failures close terrain only and report a specific reason. |
| Playback / four actors | Real file loading and shared sampling run on per-slot `std::thread` workers with condition variables and director clock publication. Unity/SMC/cloth calls stay on the game thread captured during Initialize. Stop/Start works without worker Shutdown, including background restart. Shutdown only joins workers under its process-shutdown contract. Camera/Actions pose leases remain owned by the parent integration. |

The unused Android entries in the slot's desktop hook callback table are not
substitutes for these features: pose, SMC and cloth work runs through the
managed paths above. `eiem_body.cpp::InstallFaceHooks` has an explicit Android
managed-prepare branch and never attempts to hook empty face targets.

`EiemBody::TerrainAvailable(actor)` and `TerrainUnavailableReason(actor)` expose
the actual per-actor terrain contract for the parent camera/UI status flow.
They must be queried on the game thread. A missing movement target is reported
separately from missing value-type, method, field or boxing contracts; the UI can
show the explicit reason rather than an ambiguous capability placeholder.
`TerrainMethodUnavailable` specifically reports missing ComputeFloorDist and
FindFloor signatures. This closes only terrain: the next pose uses flat body
FK/IK and the camera reference reports zero terrain offset. Query exceptions
also clear the terrain offset and expose their reason; they do not disable
body/SMC/cloth playback or fabricate a successful floor query.

### Encoding and file portability

Android uses bounded UTF-8 `fopen` and POSIX 64-bit file positioning. CP932 /
Windows-31J names use `compat/android_cp932_table.h` (9,604 double-byte entries,
generated by `compat/generate_cp932.py` from Python's CP932 codec), including
single-byte kana. Invalid names are not replaced by invented bone names.
PMX UTF-16LE decoding handles surrogate pairs explicitly; Android's 32-bit
`wchar_t` is never treated as Windows UTF-16. No iconv/JNI encoding bridge,
Bcrypt, Win32 thread/event shim, or MinHook dependency is required by this path.

### Offline verification and limits

Run from the workspace with:

```powershell
& native/modules/camera/eiem/compat/tests/run_android_tests.ps1
```

The script checks all four slots and `eiem_body.cpp` using NDK 27.2 clang++
`--target=aarch64-linux-android24 -std=c++20 -fno-char8_t -fsyntax-only`, with
warnings treated as errors. It builds/runs two native host fixtures against
owned synthetic managed objects, never loading or attaching to a game:

- Avatar neutral bind independent of an animated entry pose; real managed SMC
  call path, opaque tracker restore, pause restore, eight twist targets,
  independent leg/toe math, Stable/Freeze numeric/enabled/root-anchor restore,
  managed-exception gating, GC pin cleanup, CP932 and UTF-16. The terrain-focused
  extension verifies boxed/out-ref queries, rejected non-walkable hits, root and
  foot height response, paused state and managed-exception capability reasons.
- All four slot TUs plus body dispatcher, asynchronous loading/sampling of
  synthetic CP932 VMD fixtures, independent displacement, seek/pause, actor-only
  FinalIK suppression and unrelated-actor pass-through, SMC evaluation,
  terrain height applied through actual body transform setters, background
  Stop/Start and worker shutdown cleanup.

Only test sources, scripts and synthetic fixtures belong in source control;
`compat/tests/.gitignore` excludes `.exe`, object files and `.build/`.
These tests establish the adapter's processing and restoration paths under the
fixture contract. They do **not** establish Android game metadata availability,
AOT generic method availability, visual/retarget equivalence to PC, SMC/cloth
job scheduling on a device, or four-actor performance. No game injection
validation or full Gradle build was performed by this EIEM worker.
