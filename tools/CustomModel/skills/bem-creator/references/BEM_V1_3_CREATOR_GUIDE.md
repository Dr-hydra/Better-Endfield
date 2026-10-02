# BEM 1.3 position sliders — creator workflow

Create the shape in your modeling tool, preserve the exported mesh's vertex topology/order, then provide a target or sparse position delta. The slider changes position; the existing bone skinning, native material, normals and tangents are retained. Coordinate changes to a body usually need corresponding clothing/accessory shapes. Large deformations can change lighting, intersections and silhouette because this first version retains base surface directions.

## Existing export project

Continue using the same `.bemproj.json` task; `deformations` is an optional author-data path, not a second export project format:

```json
{
  "schema":1,"kind":"bem-export-task","mode":"pack",
  "source":"editable/project.json","deformations":"body-morphs.json",
  "output":"dist/character.bem","report":"reports/build.json",
  "package":{"id":"creator.my-character","name":"My character","author":"Me","version":"1.0.0"}
}
```

Tasks use `mode:"pack"` for an unpacked editable BEM project or `mode:"convert"` for a source Mod with the existing optional conversion recipe. Task paths are relative to the task's directory. The morph configuration's own input paths are relative to its directory. A conversion recipe may also declare `deformations`; the task/CLI explicit selection takes precedence. Keep package, finite option and slider IDs stable when updating a package.

In the Windows creator window choose “创建 / 打开导出工程”, select the morph configuration or use “编辑 / 新建形态配置”, then save and export. The JSON editor exposes the actual author schema rather than hiding arbitrary source scripts behind a nominal slider. The output report contains parameter/channel counts, explicit EFMI binding evidence, `normals:"retained-base"` and `render_verified:false`. A successful structural export still needs game testing.

CLI equivalents:

```powershell
BetterEndfield.BemConverter.exe new-project editable/project.json --mode pack --deformations body-morphs.json -o character.bemproj.json
BetterEndfield.BemConverter.exe build character.bemproj.json
BetterEndfield.BemConverter.exe pack editable/project.json --deformations body-morphs.json -o character.bem
BetterEndfield.BemConverter.exe convert source-mod --recipe conversion.recipe.json --deformations body-morphs.json -o character.bem
BetterEndfield.BemConverter.exe validate character.bem --report validation.json
```

Use distinct input, output, task and report paths. Export tasks protect explicitly named targets, sparse inputs and EFMI source files from report/output collisions.

## Target or sparse delta inputs

```json
{
  "schema":1,"kind":"bem-position-morphs",
  "parameters":[{"id":"body","name":"Body size","min":0,"max":1000,"neutral":0,"default":0,"step":1}],
  "mesh_deformations":[{"mesh":0,"parameter":"body","frames":[
    {"value":0,"neutral":true},
    {"value":1000,"target_positions":"body-target.json"}
  ]}]
}
```

`mesh` is the mesh array index in the exported BEM manifest. `body-target.json` is exactly one XYZ array per final exported vertex, e.g. `[[0,0,0],[1.2,0,0],[0,1.2,0]]`. Alternatively use a binary file with exactly three little endian Float32 values per vertex and a non-`.json` extension. The tool subtracts the actual exported BEM base position and emits only nonzero sparse records. Positions must already use the native exported axis/unit convention. Matching vertex count alone does not establish matching topology; the author must preserve order, including UV/normal seam duplication.

To supply deltas directly, replace `target_positions` with `deltas`, either an inline list or a path to a JSON list of `[vertex_index, dx, dy, dz]` records. Indices must be unique and in range; positions must be finite. An empty delta list is valid for a nonneutral zero-effect endpoint. A frame has exactly one delta/target/EFMI input. A neutral frame has only `value` and `neutral:true`.

For a parameter with a centered neutral, provide endpoints and the zero frame, e.g. values `0`, `500 neutral`, `1000`. Multiple target frames are piecewise linear; they are absolute deltas from one base, not changes relative to each other. Several parameters add their offsets; use the same parameter ID on body, clothes and attachments to control them together. Optional `available_when` uses the existing finite option conditions, e.g. `{"eq":["outfit","dress"]}`. An unavailable parameter uses neutral and retains the user's preference.

## Official EFMI ShapeKey buffer bindings

Automatic inspection recognizes shape-key requirements and stops static conversion instead of silently dropping them. Existing user GUI code cannot reliably supply universal slider names, ranges, defaults or mesh correspondence. Use an explicit reviewed conversion recipe and a manual author binding:

```json
{"value":1000,"efmi":{
  "source":"source-mod.zip","ini":"character/mod.ini","component":0,
  "shape_key":1,"vertex_order":"exported"
}}
```

`source` may be a Mod directory, ZIP, RAR or 7z. `ini` identifies the declaring source INI; `component` selects the exact `Resource_ComponentN_*` triplet, and `shape_key` is the same integer ID assigned to `$shapekey_id` before `CommandListSetShapeKey`. `vertex_order:"exported"` is a mandatory author declaration that the exported mesh has the same final vertex correspondence; it is never guessed from component number, stride or name. The tool reads:

| Resource | Official format |
| --- | --- |
| `Resource_ComponentN_ShapeKeyBatchConfigs` | R32G32B32A32_UINT; initial two scale vectors followed by 33 UInt32x4 vectors per batch |
| `Resource_ComponentN_ShapeKeyVertexIds` | R32_UINT |
| `Resource_ComponentN_ShapeKeyVertexOffsets` | R16_FLOAT, packed FP16 XYZ records |

Each batch contains 127 shape IDs and 128 offsets. The tool validates complete batch ranges, IDs and finite values, reads only the explicitly selected key's records, converts FP16 to Float32 BEM deltas, and reports source filenames and the manual binding. Quantization scales describe EFMI's integer accumulation backend; they do not scale the source FP16 deltas again.

If an export duplicates/reorders vertices, add `vertex_map` as an inline list or JSON path giving one source exported vertex ID for every BEM vertex. Seam duplicates can reference the same source vertex. Every affected source vertex must be represented. Supply a separate accurate map for a distinct LOD; this workflow does not invent an EFMI LOD vertex correspondence.

The converter does not run `SetShapeKey`, arbitrary INI/GUI expressions, animation or Mod shaders. This binding maps the author's reviewed `component + shape_key` to the BEM parameter; copy the authored desired range/default into that parameter. Custom shaders/material frameworks still require their existing reviewed conversion route. Unbound source shapes and automatic GUI reconstruction are not claimed supported: deliberately review which keys the parameter covers. Official EFMI evidence is fixed in [the upstream research](EFMI_BODY_SLIDER_RESEARCH_20261002.md).

## Runnable end-to-end example

The distributed `examples/body-slider/create_project.py` uses only the Python standard library to write a synthetic triangle, target positions, morph configuration and existing export task:

```powershell
python examples/body-slider/create_project.py --output demo-body-slider
BetterEndfield.BemConverter.exe build demo-body-slider/export.bemproj.json
BetterEndfield.BemConverter.exe validate demo-body-slider/dist/synthetic.bem
```

From the repository, run `python tools/CustomModel/examples/body-slider/create_project.py --output artifacts/bem-v13-demo`, then replace the executable above with `python tools/CustomModel/bem_tool.py`. Use a new directory. This is a format/creator test, not a playable character package. At tick 500, positions interpolate halfway from the triangle base to the target. An exported playable package still needs a verified character/resource contract and actual author body data.

## Runtime behavior and portability

Windows and Android consume the same neutral/target deltas and saved ticks. Android installation retains every candidate payload in the exported package. With experimental hot switching enabled, changes apply when the game next delivers that resource (such as switching a team or reopening details); otherwise they apply after restarting the game. This first backend does not update an already displayed mesh on every slider drag. Resetting sliders to author defaults is distinct from setting them to neutral.

Old packages remain at 1.0–1.2 when they need no shape features. New 1.3 packages require a 1.3-capable runtime; do not label them 1.2 to bypass a version check. The developer skip-validation option does not supply missing shape data or permit truncated/non-finite payloads.
