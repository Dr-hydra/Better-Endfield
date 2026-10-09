# BEM 1.4 synthetic authoring example

This example uses fictional resource identities and a triangle. It is a format
and authoring test, not a playable Mod.

The packaged BEM Tools distribution includes a ready-made `project/` directory.
From the distribution root, run:

```powershell
.\BetterEndfieldNext.BemConverter.exe build examples/multi-resource/project/export.bemproj.json
.\BetterEndfieldNext.BemConverter.exe validate examples/multi-resource/project/dist/synthetic.bem --resource weapon --platform windows-x64
```

The project includes skinned body/ultimate resources, a static weapon, shared
geometry payloads, separate position morph endpoints and an option that hides
the weapon. No Python installation is needed for these commands.

When running from the repository, `python tools/CustomModel/examples/multi-resource/create_project.py OUTPUT`
generates the same project. `--native-fixtures` generates additional deliberately
invalid packages for parser regression testing.

For a real offline graph, the packaged target generator is available as:

```powershell
.\BetterEndfieldNext.BemConverter.exe target-profile NATIVE_GRAPH.json --spec profiles/bem14-drafts/sword-0014.spec.json -o MY_PROFILE.json --project MY_PROJECT.json
.\BetterEndfieldNext.BemConverter.exe pack MY_PROJECT.json -o MY_TARGET.bem
```

Generated real-target projects initially contain only `keep` operations. They
provide exact target identities; replacement geometry and runtime verification
remain author work. The profile explicitly records `conversion_ready: false`
and `runtime_verified: false`.
