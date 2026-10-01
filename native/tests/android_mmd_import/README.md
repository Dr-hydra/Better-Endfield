# MMD import JVM checks

Uses the production Java parser, archive reader and import planner without Android or Gradle.
Provide a directory containing Commons Compress 1.28.0, xz 1.10 and its Commons IO,
Codec and Lang dependencies:

```powershell
./native/tests/android_mmd_import/Run.ps1 -DependencyDirectory C:/path/to/jars
```

Checks actual bone/morph/camera classification, invalid VMD section lengths,
nested ZIP set references with UTF-8/GBK names, unflagged UTF-8 names,
multiple independent works and ambiguous music, motion/face 2–4 generation,
camera-only works, actual 7z extraction, unsafe/duplicate paths, RAR rejection and cancellation.
