# Runtime B handoff — 2026-10-03

Owned edits: `module.cpp`, `native/tests/custom_model_binding_tests.cpp`,
`model_job_runtime.inc`, `model_asset_cache.inc`, `model_content_identity.h`,
and this note. Existing edits were preserved. No APK build/install/game launch,
branch, commit, push, clean or directory removal was performed.

Production Finish now records a rooted delivery and calls the original exactly
once. The confirmed Canvas pump reads `UnityEngine.Time.get_frameCount`; duplicate
frames cannot reset the global ledger. CPU work uses task C's single
`AsyncBemLoader`; requests copy path/options/parameters/key/revision/generation
and a file-only lease, without Unity objects or raw adapter pointers.

Jobs own detached `ConstructionScope`s. The pump activates one scope per step.
Texture ctor, raw copy and Apply use separate frame permits; geometry, material
preparation, capture and complete commit use the same ledger. Four observed roles
(plus one UI role) rotate through at most five builders; a second receiver of the
same role waits for publication, then borrows the finished pool. This is an
observed-request queue, not a claim of discovering the entire active team.
Foreground derives from actual registered `Renderer.get_isVisible`, refreshed
at 100 ms; other observed demand uses Visible priority. CPU and GPU queues give
three foreground opportunities followed by a fair background opportunity.
No current-player or complete-team identity is invented from a resource name.

The compact plan contains metadata only. Complete live hits avoid CPU decode and
Mesh/Texture GPU construction; materials and bones remain receiver-local.
Texture identity uses worker-produced SHA-256 of the selected decoded content,
entry description, immutable file-lease epoch, complete appearance/options with
only POSITION Morph parameters projected out, original Texture/shader weak
identity, and U/V/W/filter/aniso/bias. Missing sampler/content certificates cause
conservative misses. Mesh identity includes selection, output declaration/draws,
palette source identities, receiver-relative bone paths and bindpose matrices.
Windows leases deny package writes while the plan/job is live; Android relies on
the existing immutable imported file generation. Idle plans expire after 10 s.

Mesh/Texture share a 256 MiB **estimated input-byte** idle lease budget, 10 s TTL
and 512 weak entries. Active bindings are never rejected by this idle limit.
Eviction only drops extra roots, never destroys published,
borrowed, Original or game assets. Rootless old Original leases expire separately;
their weak lineage remains. Original bone arrays are weak and are reconstructed
from the receiver root. Unknown old clones can retain their display but may lack
future restoration after Original retirement.

Full ReceiverKey identity is used for capture, saved Original lookup, completed
readback, Android donor and disable/restore. The matcher include follows
PreparedBinding/Donor and callback declarations, before PrepareResource/world
adapter. Android world borrows the Ready UI recipe and commits world/UI atomically;
materials are cloned separately. Mesh-space validation uses each verified root's
local coordinates, so moving a world instance does not compare its scene position
to a prefab. Exact cross-LOD Mesh relations still require A's actual resource
snapshot/evidence; no `_20`→`_8` name heuristic was added.

Android clone hooks use the shared HostBroker chain for Internal_CloneSingle and
Internal_CloneSingleWithParent. Windows uses its existing Host create_hook
contract and conservatively logs unavailable hooks on conflicts. Only registered
roots are queried; other clone variants/pool lifecycle entrances remain a limited
first-version coverage. Failed selection/target pairs wait for a new delivery or
selection instead of repeatedly uploading every frame. Configuration changes
cancel stale jobs; failed rollback preserves potentially bound assets.

Local verification: MSVC Release CustomModel DLL and binding tests; original
parser/hot-switch/rollback tests, `--geometry`, and new `--async` checks. The latter
exercise real production Finish/pump/coordinator with a gated CPU fixture backend:
distinct-frame texture steps, new pristine UI reuse before decode, Morph texture
reuse, private material/bone ownership, game-side writeback repair, four-role
fairness, revision cancellation, SHA-256 vectors, Mesh donor/weak identity and TTL.
NDK aarch64 Android module translation-unit syntax check passes. Full APK build
and installation remain with the main agent.

Limits: task C currently decodes complete selected packages. Packages larger than
256 MiB use its documented exclusive exception; this is not payload streaming or
a hard 256 MiB process cap. A 64 MiB raw/Apply unit remains indivisible and can
still produce a long frame. The ledger limits submission density and adds cooldown;
it is not a GPU fence, residency measurement, or proof of lower GPU peaks. No
device/performance measurement was performed.

## Device regression fix — 2026-10-03 (evening)

Device log (new APK): CustomModel stopped after "Android MeshData replacement
transaction ready"; no build/commit; betterendfield.model failed with "native
hook target already owned" on `unity.object.clone_with_parent`.

- Root cause A (no model ever replaced): the Job Mesh phase called
  `PreparePalette` with the metadata-only `SmallModelPlan`, whose vertex
  streams are dropped, so `DecodeComponentSkin` rejected every geometry
  component and the Job was silently dropped (`failed_selection`, no retry).
  The `--async` test only used NoGeometry components. Fix: palette from plan
  metadata (`validate_skin=false`), skin stream validated from the decoded
  payload right before the Mesh build. Regression test reproduces the bug.
- Root cause B: CustomModel hooked `Internal_CloneSingleWithParent` (Android
  via a shared HookBroker chain, Windows via create_hook; custom_model loads
  first on both), which the Host then refused for betterendfield.model.
  CustomModel now installs no clone hook. Without complete clone coverage an
  instance created before an async commit would keep the original forever, so
  template deliveries use the proven synchronous transaction inside Finish
  (`ModelAsyncDeliveryBlocker`: clone coverage / pump started / pump stalled
  > 2 s). Async Jobs remain for hot-switch updates of registered receivers.
  Re-enabling async templates requires a Host-provided shared clone observer.
- Fallbacks: a Job failing after decode runs `ProcessResource` once on the
  pump (decode refusals are not retried); a missing `Time.frameCount` no
  longer skips LOD maintenance.
- Diagnostics: "Model pump first run", "Model delivery mode=…/synchronous
  resource=… reason=…", "Model delivery registered", "Model Job queued/phase/
  failed … reason=…", "Model fallback=synchronous … / result=…", 5 s
  "Model queue …" status.
- Gradle `verifyDesktopModelHookParity` also fails if CustomModel declares
  `"Internal_CloneSingleWithParent"`.

## Load peak reduction — 2026-10-03 (night)

- No idle retention: `model_asset_cache.inc` keeps weak observations only (no
  StrongReference, no 256 MiB/10 s idle lease). Textures are reused only while
  a published receiver of the same role still binds them
  (`IndexLivePublishedTextures`: live renderers -> materials -> texture
  instance ID -> `g_generated_texture_identity`, metadata only).
- Per-texture decode: `LoadBem(..., defer_texture_payloads=true)` keeps texture
  entries in the package (`BemTexture::Deferred`, generation = path/size/mtime);
  `DecodeBemTexturePayload` streams one Zstd entry. Synchronous transactions
  decode in `CreateTextureFromBem` and free right after `LoadRawTextureData`;
  Jobs use the shared `TexturePayloadStreamer` (1 worker, 96 MiB live budget,
  larger entries exclusive, a Job prefetches the next entry only after its
  current raw upload). Payload cache: geometry + metadata, 48 MiB / 3 s.
- Transaction dedup: `TextureTransactionCache` / `StageModelTexture` build one
  texture per (selected entry, donor sampler state), independent of
  `loading_optimization`.
- Clone hooks: `kModelCloneHooksRequested=true` (Host create_hook chains).
  Internal_CloneSingle + Internal_CloneSingleWithParent (+ Instantiate
  position/rotation entries when present) -> frame-sliced template delivery;
  any refusal -> logged synchronous fallback. Gradle no longer forbids the
  target. Priority: visible receiver nearest Camera.main = Foreground.
- Logs: `Model upload transaction ... mode= textureRefs= textureDedupHits=
  textureDedupSavedBytes= textureLiveReuse= decodedPeakBytes= uploadFrames=
  maxFrameUploadBytes=`; `Model clone hooks installed/unavailable`;
  `Model queue ... textureDecodeLiveBytes= textureDecodePeakBytes=
  clonesRegistered= maxFrameUploadBytes=`.
