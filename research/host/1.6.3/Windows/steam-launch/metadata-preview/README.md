# Steam metadata normalization and ACF preview

This small console links the current production parser and ACF generator. It reads a saved SteamCMD appinfo response (or normalized metadata), validates it, and writes only `normalized-metadata.json` and `preview-only.acf` in the explicit output directory. It does not instantiate `SteamIntegrationService`, discover Steam, write Steam configuration, cache production settings, or launch processes.

From the repository root in PowerShell:

```powershell
dotnet run --project research/host/1.6.3/Windows/steam-launch/metadata-preview/SteamMetadataPreview.csproj -c Release '-p:BEWorkspaceBuildRoot=G:\Better Endfield\build\steam-metadata-1.6.3' -- research/host/1.6.3/Windows/steam-launch/steamcmd-info-4732690.json research/host/1.6.3/Windows/steam-launch
```

Checks are limited to the production metadata validation, normalized identity round-trip, and generated ACF identity/depot manifest round-trip. IDs remain strings throughout. All compiler output is redirected to `build/steam-metadata-1.6.3` by the command above. Change that absolute build root when moving the repository.

On 2026-10-15 the upstream installation-directory spelling is `Arknights Endfiled`; preserve it exactly. The selected default launch entry is `Endfield.exe` with empty arguments. The alternate `-force-d3d11` entry is not selected by the production default-launch parser. These preview files do not prove the Steam-to-CN executable launch path works with the 1.6.3 launcher check.
