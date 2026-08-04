# IL2CPP Dumper

Static + live runtime metadata extractor for Unity IL2CPP games on Windows x64.
Originally written for Arknights: Endfield, works on any modern IL2CPP build.

This release replaces the old INSERT-based workflow with a single self-contained
launcher (`Il2cppDumper.exe`). The DLL is embedded as a resource and manually
mapped into the target process - no separate files to copy around.

## What's new in this update

- Single launcher `.exe`, embedded DLL via `RCDATA` resource
- File-picker dialog at startup (or pass exe path on the command line)
- Auto-elevates via UAC if the target requires admin
- Auto-dump on inject (INSERT key no longer needed)
- Output goes next to the launcher, not `C:\`
- Skips re-dumping if `IL2CPP_Dump_Normal/AI/` already exist
- Hotkeys for live runtime snapshots (camera, scene, components, mesh colliders, targeted class scan)
- GC-aware worker thread with `il2cpp_thread_attach` + retry, no more spontaneous "Collecting from unknown thread" aborts
- Dump format gained: properties, events, attributes, generic types, class size, type modifiers, method tokens, RVA reasons (abstract/extern/icall/generic def), const literal values
- 92%+ RVA resolution rate (direct read of `MethodInfo->methodPointer` as fallback)

## Build

1. Open `Dump.sln` in Visual Studio 2022 (v143 toolset, Windows SDK 10.0)
2. Build `Release | x64`

The launcher project depends on the DLL and pulls it in as an embedded
resource. Output: `x64/Release/Il2cppDumper.exe`.

## Run

Double-click `Il2cppDumper.exe`. A file picker asks for the target `.exe`.
The console auto-closes 5 seconds after a successful inject; the dumper's own
console takes over inside the game.

You can also drag a target `.exe` onto the launcher, or pass it as an argument.

## Static dump (auto, on inject)

Written next to the launcher:

- `IL2CPP_Dump_Normal/<assembly>.cs` - C#-shaped output with namespaces,
  inheritance, properties, events, attributes, RVA, tokens, sizes, type
  modifiers (`sealed`/`abstract`/`static`)
- `IL2CPP_Dump_AI/<assembly>.cs` - line-tagged format for grep/LLM input
- `IL2CPP_Dump_Strings.txt` - every `static const string` literal across all assemblies
- `IL2CPPDump_Log.txt` - session log

Re-running with the dump folders already present skips the static pass.
Delete the folders to force a fresh dump.

## Live runtime (hotkeys)

After the static dump completes, the DLL stays loaded and listens for keys:

| Key | Action |
| --- | ------ |
| F5  | Full snapshot - camera + scene + mesh colliders |
| F7  | Scene only (light) - GO list, positions, components, materials |
| F11 | Scene deep - same plus MonoBehaviour field values (slower) |
| F8  | Main camera state (position, forward, fov, clip planes) |
| F2  | Every `MeshCollider` in the scene → world-space mesh binary |
| F3  | Targeted class scan - reads `IL2CPP_ScanList.txt` |
| F6  | Exit, detach from runtime, unload DLL |

### Targeted class scan

Drop a file named `IL2CPP_ScanList.txt` next to the launcher with one
fully-qualified class name per line (`#` for comments). F3 walks every live
instance of those classes, dumping primitive fields and (for components)
their world position. Useful for iterative work without scanning 40k+
GameObjects every time.

```
# AI bot navigation
Beyond.Gameplay.Core.NavigationManager
Beyond.Gameplay.Core.NavigationComponent
```

### Output binaries

`IL2CPP_MeshColliders_<ts>.bin`:

```
per collider:
  uint32 instance_id
  float  pos[3]
  uint32 vertex_count
  Vector3[vc]   verts (local-space)
  uint32 index_count
  int32[ic]     tris
```

A short text index (`IL2CPP_MeshColliders_<ts>.txt`) explains the layout
and counts.

## Notes

- Works on x64 IL2CPP only.
- Some games (e.g. Endfield) ship with `requireAdministrator` manifests; the
  launcher auto-elevates via UAC.
- Light scene dumps complete in well under a second on 40k GameObjects;
  deep dumps are capped at 8 seconds / 2000 MonoBehaviours per pass.
- Hardware tier (CPU cores + RAM) is detected at startup and used to scale
  yield intervals - the game stays responsive even mid-dump.

## License

MIT.
