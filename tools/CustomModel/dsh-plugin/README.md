# dsh-bem-creator

A DeepSeek Harness bundle that installs the `bem-creator` skill. The skill
converts Endfield source Mods to BEM 1.0–1.5, repairs non-standard packages,
builds BEM 1.4 projects, and validates the result before it ships.

The bundle carries instructions only. It does not bundle the BEM Tools CLI; the
skill drives that executable, so install the toolchain alongside it.

## Install

```sh
# from the release tarball
dsh plugin --profile web add ./dsh-bem-creator-1.6.3.tgz

# from a checkout
dsh plugin --profile web add ./tools/CustomModel/dsh-plugin

# once published to a registry
dsh plugin --profile web add dsh-bem-creator
```

`dsh plugin` is a pnpm forwarder. After the install it reconciles
`dsh.profile.bundles` in the profile's `package.json` with the bundle
declarations it finds, so this package activates without a manual patch edit.
Restart the harness afterwards.

Verify the skill reached the catalog:

```sh
dsh skill list
```

`bem-creator` appears as a model-invocable and user-invocable skill, so the model
can load it with the `skill` tool and you can invoke it as `/bem-creator`.

## What it contributes

One row in the profile tree, mounting this package's plugin. The plugin reads
`skills/bem-creator/SKILL.md` from its own install directory and registers it
through `ctx.skills` with `resourceBase` pointing at that directory. The relative
references inside the skill body (`references/…`, `scripts/…`) therefore resolve
to the installed package rather than to the working directory.

Harness filesystem discovery is not used: it scans a fixed set of roots one level
deep and cannot see inside an installed package. A runtime registration is the
supported way to contribute a packaged skill.

## Requirements

- Node.js `^22.19.0 || >=24.0.0`.
- DeepSeek Harness `>=0.2.1-rc.2`.
- The BEM Tools CLI, either on `PATH` or at a path the skill is told about. The
  Windows build is `BetterEndfieldNext.BemConverter.exe`; the standalone
  `BEM-Tools-<version>-win-x64.zip` ships it beside the GUI and docs.
- Python is only needed for the skill's helper scripts. The frozen CLI needs no
  Python.

## Notes

- **Catalog description length.** The harness truncates catalog descriptions at
  `catalogDescriptionMaxLength`, which defaults to `500`. The skill description
  is held at or below that ceiling so the routing summary the model sees is
  never cut mid-sentence; `test/smoke.mjs` enforces the bound.
- **No tool yet.** This release contributes the skill only. A `bem` tool that
  wraps the CLI is planned so the skill stops depending on shell dialect
  (`bash` on POSIX, `pwsh` on Windows).
- **No client UI.** The bundle declares no `dsh.client`, so it adds nothing to
  the browser or desktop surfaces. The skill works through the existing chat and
  tool cards.
- **Versioning.** The bundle version tracks BEM Tools. `scripts/BuildDshPlugin.py`
  fails when the two disagree.

## Build

```sh
python scripts/BuildDshPlugin.py
```

The script stages `package.json`, `cordis.patch.yml`, `lib/` and a copy of
`tools/CustomModel/skills/bem-creator/` into a temporary package directory, runs
`npm pack`, verifies the archive contents, and copies the tarball into the
release directory.

## License

AGPL-3.0-only, the same license as the project this bundle ships with.
