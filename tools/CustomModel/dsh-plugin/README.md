# dsh-bem-creator

A DeepSeek Harness bundle for Endfield BEM packages. It contributes four
things, and carries the converter while leaving its runtime to the host:

1. **The `bem-creator` skill** — converts Endfield source Mods to BEM 1.0–1.5,
   repairs non-standard packages, builds 1.4 projects, and validates the result.
2. **The `bem` tool** — `inspect`, `validate`, `preview`, `resources`, `convert` and `doctor`
   on a `.bem` file, so the agent stops depending on shell dialect (`bash` on
   POSIX, `pwsh` on Windows) for the read-only paths.
3. **A preview tab** — the last previewed package rendered in the conversation,
   with appearances, option groups, shape sliders and per-part visibility.
4. **The CLI itself, as sources.** `convert` no longer needs a BEM Tools
   installation: `lib/bin/cli/` holds the full Python tree, so the bundle works
   on a machine that has only an interpreter. What it does *not* carry is the
   CPython runtime (33 MB of PyInstaller output), the `zstandard` wheel, or the
   7-Zip backend — all three are cheap to fetch and none is needed to read or
   write a package.

## Optional preview resources

The preview tab's **Preview resources / 预览资源** panel reads an already extracted pack
and refreshes the current preview. Choose **BC7 (Windows)** or **ASTC (Android)** yourself:
DSH can run on Android too, and the viewing browser can be on another device. The panel
reports browser ASTC support. BC7 uses the bundled decoder; ASTC uploads directly to the
GPU when supported. Neither selection downloads anything automatically.

Use bem action=resources for status, or pass path and platform=windows/android to select.
Accepts a shared resources root or an exact pack directory containing manifest.json.
Persistent configuration: BEM_PREVIEW_RESOURCES and BEM_PREVIEW_PLATFORM before DSH starts.
Paths belong to the DSH host. Reuse existing native-app resource directories first.

Original geometry matches exact prefab/renderer/material identities and appears only for
keep components. Package textures and hide/replace retain priority. Missing resources report
diagnostics and leave package meshes visible. The bundled skill reference PREVIEW_RESOURCES.md
covers choosing one release asset by browser capability, version pins and manual extraction.
No additional runtime dependency is introduced.

## Install

```sh
# from the release tarball
dsh plugin --profile web add ./dsh-bem-creator-1.6.5.tgz

# from a checkout
dsh plugin --profile web add ./tools/CustomModel/dsh-plugin

# once published to a registry
dsh plugin --profile web add dsh-bem-creator
```

`dsh plugin` is a pnpm forwarder. After the install it reconciles
`dsh.profile.bundles` in the profile's `package.json` with the bundle
declarations it finds, so this package activates without a manual patch edit.
Restart the harness afterwards.

**Reinstalling the same version.** pnpm keys a tarball by version, so replacing
1.6.3 with a rebuilt 1.6.3 may be treated as already-installed. Remove first:

```sh
dsh plugin --profile desktop remove dsh-bem-creator
dsh plugin --profile desktop add "F:\path\to\dsh-bem-creator-1.6.5.tgz"
```

Pass an **absolute path**, not a `file:` URL — the `file:` prefix drops the drive
letter on Windows and pnpm looks under `C:\`.

Verify the skill reached the catalog:

```sh
dsh skill list
```

`bem-creator` appears as a model-invocable and user-invocable skill, so the model
can load it with the `skill` tool and you can invoke it as `/bem-creator`.

## What `convert` needs

The bundle ships the converter's **sources**, in `lib/bin/cli/`, and looks for
them in this order:

1. `BEM_CLI` — a `bem_tool.py`, a checkout directory, or a standalone build.
2. `lib/bin/cli/bem_tool.py` — the tree this bundle carries.
3. `bem_tool` / `BetterEndfieldNext.BemConverter` on `PATH`.
4. The conventional install directories (`/data/local/tmp/bem-cli`,
   `%LOCALAPPDATA%\BEM Tools`, `~/.local/bin`, …).

Every candidate is *run* (`--version`) before it is accepted, so "the file is
there" never gets mistaken for "it works". `bem doctor` reports the winner, the
version it printed, and — when nothing answers — the first four failures plus a
remedy.

What the tree needs from the host:

| Host | Requirement |
| --- | --- |
| Python 3.14+ | nothing; `compression.zstd` is in the standard library |
| Python 3.10–3.13 | `python -m pip install zstandard` once |
| No Python at all | install one, or set `BEM_CLI` to a standalone converter build |

The bundle should not carry a runtime, and does not: 33 MB of PyInstaller output
is almost entirely CPython. A machine that runs a mod toolchain already has one,
and shipping the sources collapses Windows and Android into the same tree
instead of two platform directories.

`zstandard` is not bundled either. Wheels are ABI-tagged, so covering Python
3.10 through 3.14 would mean five copies and a rebuild on every new minor
release; one `pip install` is cheaper. Devices on 3.14+ need nothing at all.

`catalog/` and `profiles/` **are** bundled. `component_auto.catalogs()` resolves
`catalog/` from the entry point's directory, so a conversion that matches
components fails without it — and it costs 1.3 MB compressed for 10 MB of JSON.

## What the preview does

Call `bem preview` with a `.bem` path. The tool builds a scene on the host and
registers a preview id; the 预览 tab in the conversation polls for the newest one
and renders it.

| Surface | Behaviour |
| --- | --- |
| Orbit / pan / zoom | Left drag to orbit; right/middle drag or Shift+drag to pan; wheel to zoom. Touch: one finger to orbit, two fingers to pan/pinch. 重置视角 reframes and clears pan. |
| Appearances (1.0–1.2) | Chips select an `appearances` entry and its component set |
| Option groups (1.1+) | One chip row per group; choices re-evaluate `component_rules` |
| Shape parameters (1.3+) | 0–1000 sliders drive `mesh_deformations` sparse frames |
| Parts | Per-mesh visibility, including "全部隐藏" for a component hunt |
| Textures | BC1/BC3/BC4/BC5/BC7 decoded in workers; 素模 switches to clay |

Limits, so nobody is surprised in联调:

- **ASTC uses the viewing browser GPU.** Its compressed blocks are served directly.
  Without WEBGL_compressed_texture_astc the tab explains that BC7 is the alternative;
  it does not automatically download or change the selected resource format.
- **Geometry is served Y-up.** Packages store Z-up vertices (a 1.67 m character
  measures 1.668 along Z, 0.58 along Y), so `buildScene` rotates positions and
  deformation deltas once and reports what it did in `scene.orientation`.
- **Base mip only.** Blobs ship the top level; decoded BC/RGBA mipmaps are generated on the GPU, while ASTC uses base-level linear filtering.
- **Original geometry is optional.** Matching local preview resources complete untouched
  keep components; without them, the stage shows only meshes from the package.
- **Deformation moves positions only** — not normals, UVs or skin weights. That
  matches what the format stores.
- **Preview validation is not the CLI's validation.** `bem validate` checks the
  things that make a *render* wrong (index ranges, short payloads, dangling mesh
  refs). Schema conformance is the CLI's job.

`bem doctor` also reports `surface.routes` and `surface.tool`, which is the
fastest way to tell "the bundle loaded but mounted nothing" from "the bundle did
not load at all". A headless profile legitimately reports `tool: false`.

## Routes

The bundle owns one webserver prefix and never touches `/api/*` (that table
belongs to the connection plugin):

| Route | Purpose |
| --- | --- |
| `/dsh-bem/api/latest` | The newest preview id plus its capability summary |
| `/dsh-bem/api/scene/<id>` | Scene JSON: meshes, draws, rules, parameters, channels |
| `/dsh-bem/api/blob/<id>/<blob>` | Positions, UVs, indices, deformation frames, texture blocks |
| `/dsh-bem/assets/bcn.js` | The vendored BCn decoder |
| `/dsh-bem/assets/decode-worker.js` | Module worker that decodes texture blocks off-thread |

Scenes are capped at six and evicted oldest-first, so a stale tab gets `410` with
"run preview again" rather than an empty stage. Geometry and texture blobs travel
as `application/octet-stream`; a 10 MB package never goes through base64 in JSON.

**The prefix is registered as `/dsh-bem`, without a trailing slash.** The
webserver's matcher is `pathname !== prefix && !pathname.startsWith(prefix + '/')`
— it appends the separator itself. Registering `/dsh-bem/` therefore waits for
`/dsh-bem//…`, which no client sends: the route stays active, the handler is
never called, and every request falls through to the SPA fallback as a bodyless
404. The handler itself tolerates `/dsh-bem`, `/dsh-bem/` and `/dsh-bem//…`.

## Requirements

- Node.js `^22.19.0 || >=24.0.0`.
- DeepSeek Harness `>=0.2.1-rc.2` with the web surface (`ctx.webServer`). A
  headless profile still gets the skill and the tool; the preview tab is simply
  not registered. Verified against `0.2.1-rc.3` / Cordis 4.0.5; earlier release
  candidates are untested.
- Python is only needed for the skill's helper scripts. The frozen CLI needs no
  Python.

## Notes

- **Never read an optional service as a property.** A Cordis context is a proxy
  over the service table: `ctx.webServer` throws
  `cannot get property "webServer" without inject` when the name is not in the
  plugin's own `inject` set — it does not return `undefined`, and `source?.name`
  does not save you. Use `ctx.get(name)` (non-strict) or declare the dependency on
  a child fiber with `ctx.inject([name], cb)`. This bundle's `mountService` does
  both: synchronous mount when the service is already up, a child fiber only for
  late arrival. Getting this wrong failed the whole fiber on every desktop
  profile, taking the skill down with it.
- **Catalog description length.** The harness truncates catalog descriptions at
  `catalogDescriptionMaxLength`, which defaults to `500`. The skill description
  is held at or below that ceiling so the routing summary the model sees is
  never cut mid-sentence; `test/smoke.mjs` enforces the bound.
- **Versioning.** The bundle version tracks BEM Tools. `scripts/BuildDshPlugin.py`
  fails when the two disagree.
- **The decoder is GPL.** `lib/vendor/bcn.js` is `@bis-toolkit/bcn` 1.0.2
  verbatim, GPL-3.0-or-later. AGPL-3.0-only §13 permits combining with it; the
  header in that file says so and names the upstream.

## Tests

```sh
node test/smoke.mjs        # skill registration and frontmatter
node test/host.test.mjs    # tool, routes, scene JSON, blobs, 1.0 and 1.5 surfaces
node test/client.test.mjs  # browser half against a fake React + WebGL2 context

# optional: the same host assertions on the Cordis inside an installed dsh
ELECTRON_RUN_AS_NODE=1 "<DeepSeek Harness.exe>" test/real-cordis.mjs
```

`host.test.mjs` needs two real packages; it reads `BEM_TEST_PACKAGE_10` and
`BEM_TEST_PACKAGE_15`, falling back to the repository's own build outputs and
skipping a section when a file is absent. `client.test.mjs` needs no package at
all — it serves a canned scene and asserts that the tab renders, decodes,
deforms, and cleans up.

Service injection is modelled by `test/cordis.mjs`, not by a plain object: it
reproduces the proxy that throws on an undeclared read, which is the failure mode
a naive stub hides. `real-cordis.mjs` additionally proves the child-fiber
semantics on the real module; it skips with exit 0 when no Cordis is readable.

Rebuild the browser half after editing `src/client/`:

```sh
node tools/build-client.mjs          # writes lib/client.js
node tools/build-client.mjs --check  # fails when it is stale
```

## Build

```sh
python scripts/BuildDshPlugin.py
```

The script stages `package.json`, `cordis.patch.yml`, `lib/` and a copy of
`tools/CustomModel/skills/bem-creator/` into a temporary package directory, runs
the three Node tests, packs with `npm pack`, verifies the archive contents and the
three manifest fields `dsh-client-modules` throws on when missing
(`exports["./client"]`, `dsh.client.platform`, a `__ModuleLoader__`-wrapped
`lib/client.js`), then copies the tarball into the release directory.

## License

AGPL-3.0-only, the same license as the project this bundle ships with. The
vendored BCn decoder is GPL-3.0-or-later; see `lib/vendor/bcn.js`.
