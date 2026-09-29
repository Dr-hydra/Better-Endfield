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
