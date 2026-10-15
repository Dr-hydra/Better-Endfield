# 1.6.3 runtime/offline join and BEM staging

Run: `bem-1_6_3-login-20261015-091033-3bb4aa`, game process 14396.

All 352 captured renderers match the offline resource identities, mesh names, index/vertex counts, bone paths/order, material slots and bindposes using the existing `validate_observation`. The bindpose check uses the existing explicit matrix transpose. Runtime declarations and stream strides also pass `apply_observations` for all 352 renderers. No game operation or new capture was performed during this join.

| Character | Matched and attached layouts | Staged BEM components |
| --- | ---: | ---: |
| chr_0036_jsspsi | 109 (world 86 + UI 23) | 15 |
| chr_0037_minghe | 81 | 0 |
| chr_0037_minghe02 | 81 | 0 |
| chr_0037_minghe03 | 81 | 0 |

Each character directory contains `runtime-observations.json` and `database.observed.json`. Observation IDs are joined to serialized renderer IDs by resource root and full renderer path; the captured ID is retained as `runtime_id`. Original offline databases and capture files remain unchanged. The observed databases retain conservative conversion approval flags: attaching evidence is not approval of a conversion profile.

`catalog-staging/chr_0036_jsspsi.json` was produced by the existing `build_role`; only the output revision and evidence references were updated to this capture. `catalog-staging/coverage.json` covers this staged character alone. The formal catalog and its 33-character coverage were not changed. No production importer or schema changes were made.

## Exact remaining boundaries

- The staged 15 Jsspsi components have direct observations for both roots. Eight use layouts supported by the current importer rules; seven retain `source_layout_supported=false`.
- Unsupported Jsspsi layouts: `brow_01` has strides `[40,8,32]` and two submeshes; `cloth_02`, `cloth_05`, `cloth_07`, `cloth_08` have `[16,20,12]`; `cloth_06`, `cloth_09` have `[16,20,4]`. Their native layouts are captured, but the current conversion rule table does not describe them.
- Jsspsi `S_actor_jsspsi_cloth_04_lod0` is excluded by world/UI pairing. World has `M_fx_jsspsi_cloth_04_01`, `_02`, `_03`; UI has only `_03`. Both roots were captured successfully. They must not be labeled equivalent.
- Each Minghe variant has 18 non-VFX LOD0 primary parts. All 54 are observed but not imported because the existing importer requires a UI counterpart. No fictional UI resource was created. Exact part names, layouts and material slots are in `unimported-primary-parts.json`.
- VFX parts and non-LOD0 renderers remain available in the observed databases; the existing importer intentionally excludes them from its primary component selection.
- The staged catalog remains `source-resource-identities-pending`, with no source buffer/texture identity mappings. No GPU hash or texture identity scan was performed. It is BEM catalog data for review and subsequent mapping, not a fully conversion-ready character package.
- The offline backend warning for the common default MRO texture dependency of Minghe 02/03 remains recorded. It did not cause any of the 352 runtime/offline identity checks to fail.

Machine-readable results: `runtime-match-report.json`, `runtime-observed-summary.json`, `unimported-primary-parts.json`.
