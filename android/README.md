# Better Endfield Android

Android ARM64 and LSPosed workspace for the Better Endfield voice module.

The current POC is intentionally read-only. It loads only in
`com.hypergryph.endfield`, waits for Unity IL2CPP initialization, and resolves
`Beyond.Gameplay.Audio.VoiceI18n.GetCurrentLanguage()` through exported IL2CPP
APIs. It does not install native hooks or invoke the resolved method.

## Requirements

- JDK 17 or newer
- Android SDK platform 35 and build-tools 35.0.0
- Android NDK 27.2.12479018
- CMake 3.22.1

Create `local.properties` with the SDK path and build:

```powershell
.\gradlew.bat :app:assembleDebug
```

After installing the APK, enable the module in LSPosed for
`com.hypergryph.endfield`, force-stop the game, and launch it again. Filter
Logcat by the `BetterEndfield` and `BetterEndfield.Xposed` tags.

## LSPosed scope troubleshooting

If Endfield is missing from every module's scope list, open the scope page's
overflow menu, choose `Hide`, and turn off the `Games` filter. LSPosed applies
that filter globally and Android classifies Endfield as a game.

The module declares the same recommended scope in both
`META-INF/xposed/scope.list` and the legacy-compatible `assets/xposed_scope`.
