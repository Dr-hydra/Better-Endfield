# Character asset catalog

Generated from the decrypted playable-character `PrefabInfo` records and the full 
VFS bundle manifest. No runtime replacement behavior is changed by this catalog.

## Files

- `characters.json`: authoritative detailed catalog, including logical model keys, 
  resolved postmodel paths, model-directory assets, every character-owned animation 
  clip path, and bundle mapping.
- `selection-options.json`: compact model/final-animation choices intended for later 
  parameterization.
- `models.csv`: flattened model and postmodel paths.
- `animations.csv`: flattened list of all character-owned animation clips.
- `walk-animations.csv`: walk-only subset with exact Unity loop metadata where exported.
- `walk-loop-report.md`: per-character loop-walk counts and exact clip list.
- `walk-bundles.txt`: minimal bundle set needed to re-export walk metadata.
- `walk-clip-metadata.json`: compact exact loop/length metadata for all 148 walk clips.

## Rebuild

```powershell
python scripts\ScanCharacterAssets.py
```

The scanner automatically consumes `walk-clip-metadata.json`; if it is absent, it 
falls back to full AnimeStudio JSON exports in `walk-animation-json/`.
The neutral-loop count is deliberately conservative: the clip must have Unity 
`m_LoopTime` enabled and its name must end in `_dialog_state_walk_loop`.
