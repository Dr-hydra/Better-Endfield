# Native prefab metadata reader

`NativeAssetReader` reads GameObject/Transform, SkinnedMeshRenderer,
MeshRenderer/MeshFilter, Mesh, Material and Texture2D references. Shader handling
remains metadata only. It does not write installed game assets.

Build with the compatible AnimeStudio backend already provisioned for the workspace:

```powershell
dotnet build tools/CustomModel/NativeAssetReader/NativeAssetReader.csproj -c Release -p:AnimeStudioDir="G:/Better Endfield/toolchains/native-asset-reader/identities"
```

The normal `extract_native_bundles.py --character chr_...` mode includes world/UI
prefabs. For a weapon or a standalone form, use `--asset-only` with one or more
`--extra-asset` arguments containing exact manifest asset paths. Other extractor
arguments (`--game`, `--unpacker`, `--resconv`, `--output`) remain required. Output
must be an empty directory outside the game installation.

Feed the reader's raw graph to `parse_native_models.py`. Static renderers resolve
their mesh through exactly one MeshFilter on the same serialized GameObject;
names are never used to join objects. Empty bindposes are valid for static meshes.
Missing or ambiguous filters, unresolved mesh references and additional vertex
streams are reported as errors. Existing skinned bone/bindpose checks remain in
effect. Every parsed renderer includes `renderer_kind` (`skinned` or `static`).
Parsed renderers also retain their root prefab's exact `resource_asset_paths`.
Loaded prefabs without a supported renderer are listed in `empty_resources`;
they are distinct from truly absent `missing_resources`.

Offline reference completeness does not establish runtime replacement support.
Serialized HG channel flags are retained, and `conversion_ready` remains false
until the separate runtime/profile validation process is completed.
