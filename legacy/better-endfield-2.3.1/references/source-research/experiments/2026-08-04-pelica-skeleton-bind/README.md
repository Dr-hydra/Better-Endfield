# Pelica skeleton-binding experiment

Archived on 2026-08-04 before returning to the complete-model replacement
approach.

This build instantiates `chr_0004_pelica_postmodel.prefab`, maps its
`SkinnedMeshRenderer.bones` and `rootBone` references to the original female
login actor skeleton, and leaves the original login root and Animator active.

Observed mapping on the tested client:

```text
781/1422 bone references (54.9%)
full renderers: 22
partial renderers: 24
zero-match renderers: 5
mapped root bones: 46/51
unique mapped bones: 164 source -> 164 target
```

The resulting model suffered severe deformation because the Pelica mesh bind
poses do not match the female login skeleton. This route is preserved as an
experiment and is not the active implementation direction.

## SHA-256

```text
model_replacer.cxx  6FB72D797EC56253176DFA45B2C0E61B8ABF0B0B6C03684A271CC40EB6E9B79D
il2cpp.dll          0E976BE8DE8686515693BEE7A88F06E7EBCFC0491E54593851BD3187EF64D4B4
Il2cppDumper.exe    DBBE378D02AA056C856507F3DAA034BA20C0E35FE46F743F465836A3C78FA005
```
