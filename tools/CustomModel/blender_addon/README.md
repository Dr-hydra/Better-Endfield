# BEM Blender exporter

This add-on exports an editable BEM project. It does not write the final `.bem`
container itself; run the generated `export.bemproj.json` with BEM Tools.

## Quick start

1. Install `bem_exporter` as a Blender add-on.
2. Set the catalog JSON, output directory and character ID in the **BEM** panel.
3. Name replacement objects `BEM_C0`, `BEM_C1`, and so on, or set the object
   custom property `bem_component_id`.
4. Bind the mesh to the catalog bone names and give it an active UV layer.
5. Click **Export BEM project**.
6. Run:

```text
BetterEndfieldNext.BemConverter.exe build <exported-directory>/export.bemproj.json
```

The first version supports the verified 16/12/12 vertex layout, fixed
appearances, and explicit texture identities. For a texture replacement, set
`bem_original_name` on the Blender image to the original game texture name;
`bem_semantic=normal` and `bem_normal_encoding=xyz-unorm` may be set for normal
maps. Unsupported layouts and missing bone weights are rejected during export.

For advanced packages, set the scene properties `bem_option_groups_json`,
`bem_component_rules_json`, and optionally
`bem_selection_constraints_json`. An existing BEM 1.3 deformation JSON can be
selected with `bem_deformations_path`; the add-on copies it into the task
workspace and lets the normal CLI validate it.

The exporter does not include game character reference models. Authors provide
their own Blender reference or local template. Option groups and shape sliders
remain advanced project features for now.
