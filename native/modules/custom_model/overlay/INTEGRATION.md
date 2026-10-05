# Model overlay integration

The Windows target is `BetterEndfield.ModelOverlay` with output name
`BetterEndfield.ModelOverlay.exe`, staged beside `BetterEndfield.CustomModel.dll`.
Sources: `overlay/main.cpp`, `overlay/model_overlay.rc`; link
`BetterEndfield.CustomModelCore`, `gdiplus`, `shell32`, `user32`, `gdi32`.
Use C++20, UNICODE/_UNICODE, WIN32_LEAN_AND_MEAN, NOMINMAX, UTF-8 and the existing
static MSVC runtime. As for MmdOverlay, use `/MANIFEST:NO` with the explicit rc.
Only add this target on Windows. None of these helpers enters Android builds.

## Settings transaction (UI and companion)

- Exact mutex: `Local\BetterEndfield.CustomModel.Settings`. This is a dedicated
  per-login-session custom-model settings scope, independent of other overlays.
- UTF-8 without BOM; same profile `catalog/custom-model/runtime.ini` as the host.
  Preserve unknown sections, keys, dormant selections and experimental flags.
- Lock, read current bytes, merge only the operation delta, create a unique temp
  file in the same directory, flush/close, atomically replace, release the mutex.
  Never overwrite current settings with an old whole-library snapshot.
- Parse BEM metadata outside the lock. The native helper uses an optimistic variant:
  read under lock, resolve the operation outside the lock, then lock and compare the
  full baseline bytes before replacement; if changed, rebase and retry. Mutex timeout,
  invalid selection and replacement failure leave the previous file intact.
- Explicit enable forces peer packages for the character (and overlapping resource
  roots) false. Explicit close-all sets every `Mod.*` section false, including entries
  whose files are missing. Per-control option/tick deltas merge into the latest complete
  default-filled selection. `parameters` contains effective ticks (neutral for unavailable
  controls); `parameters_saved` contains remembered ticks including dormant controls.

```
[CustomModel]
overlay_enabled=true
overlay_visible=false
overlay_hotkey=PLUS

[Mod.package-id]
package=<existing absolute or root-relative BEM path>
enabled=true
options=group:choice&other-group:choice
parameters=width:500&height:300
parameters_saved=width:700&height:300
```

BEM 1.0 uses `appearance` instead of `options`. PLUS is bare VK_OEM_PLUS (0xBB),
the main keyboard `=` key; ADD is VK_ADD (0x6B), the numpad key. Read aliases are
OemPlus, OEM_PLUS, `=`, and literal `+`; modifier chords remain supported. The
custom-model adapter uses the shared input parser and rejects unknown bindings.

## Lifecycle and IPC

Host creates `Local\BetterEndfield.ModelOverlay.<game-pid>` mapping, initializes
`model_overlay_protocol.h::Shared`, then starts the companion with `--game-pid`
and `--mapping`. Shared paths are immutable for the session. The installed library
is `<install-root>/models`, the legacy library is `<runtime-root>/packages`; saved
`Mod.* package` paths are authoritative. The host only handles Win32 process lifetime
and publishes startup hot-switch mode plus the existing registry pump acceptance
result/hash. It performs no Unity calls on a companion/overlay thread.

Companion polls fresh runtime settings and package mtimes. It uses a per-game mutex
to prevent duplicate windows, follows the game client with per-monitor DPI, hides on
loss of foreground/minimize, and exits on game exit or host shutdown. Closing the
window saves `overlay_visible=false`; disabling leaves management files intact.
The companion remains available to observe later `overlay_enabled=true` even when
initially disabled. Runtime experimental mode changes continue to require restart.
Only the original registry/generation/resource-job path handles model replacement;
the overlay has no payload decode, warmup, replacement or model hook command.

Save feedback means persisted/accepted by the existing runtime, not that a frame-sliced
load has finished. With hot_switch off it reports restart required. No explanatory
small print is rendered in the window.

## Offline verification

`tests/model_overlay_tests.cpp` is a standalone local test executable linked with
the freshly compiled bem.cpp/mod_registry.cpp and the existing BemZstd static library.
It exercises complete/default/dormant selections, constraints, ticks, unknown fields,
per-character exclusivity, close-all, optimistic concurrent UI rebase, parallel writers,
metadata reuse and OEM aliases. Compile the companion and module translation units
locally; do not run a full build, deployment or a game session for this work.
