[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateSet('windows', 'android')]
    [string]$Platform,

    [Parameter(Mandatory)]
    [string]$Archive,

    [string]$ExpectedVersion = '',

    [string]$WorkspaceConfig = ''
)

$ErrorActionPreference = 'Stop'
$scriptsRoot = Split-Path -Parent $PSScriptRoot
. (Join-Path $scriptsRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws

$archivePath = (Resolve-Path -LiteralPath $Archive).ProviderPath
$outputs = $ws.resource_update.outputs

if (-not $ExpectedVersion) {
    # The revision registry is what the packager itself reads to name a build,
    # so it is also the only honest statement of which core this workspace
    # expects to embed. A published archive that disagrees means the registry
    # and the release have drifted -- fail here instead of shipping an
    # installer built against a resource snapshot nobody declared.
    $registry = Get-Content -LiteralPath (Join-Path $ws.repo_root 'config/resource-packs.json') -Raw |
        ConvertFrom-Json
    if ($registry.schemaVersion -ne 1) {
        throw 'Unsupported resource revision registry'
    }
    # Windows tracks the workspace game version, Android tracks its own source
    # snapshot -- they are on different game builds (1.6.3 vs 1.6.4), and the
    # packager reads each from the matching place.
    if ($Platform -eq 'android') {
        $sourcePath = Join-Path $ws.repo_root 'android/resources/resource-source.json'
        $source = Get-Content -LiteralPath $sourcePath -Raw | ConvertFrom-Json
        $gameVersion = if ($source.provenance -and $source.provenance.gameVersion) {
            [string]$source.provenance.gameVersion
        } else {
            [string]$source.gameVersion
        }
    } else {
        $gameVersion = [string]$ws.game.version
    }
    if (-not $gameVersion) {
        throw "The $Platform source game version could not be resolved from the workspace"
    }
    $coreBlock = $registry.revisions.core.PSObject.Properties[$Platform]
    $revisionEntry = if ($coreBlock) { $coreBlock.Value.PSObject.Properties[$gameVersion] } else { $null }
    $revision = if ($revisionEntry) { [int]$revisionEntry.Value } else { 1 }
    $ExpectedVersion = "$gameVersion.$revision"
}

# A published core package is the only place these build inputs exist now that
# they are no longer tracked. The paths inside the archive are the ones the
# packager writes, not the ones the compilers read, so each entry is restored
# explicitly. Nothing is extracted wholesale: a hostile member name has no way
# to reach outside the workspace.
$map = [ordered]@{}
if ($Platform -eq 'windows') {
    $map['model/character-presets.json'] = $outputs.character_presets
    $map['voice/voice-catalog-index.json'] = $outputs.voice_index
    $map['combat/combat-dictionary.json'] = $outputs.combat_dictionary
    $map['combat/combat-semantics.besem'] = Join-Path $outputs.manifests 'combat/combat-semantics.besem'
    $map['combat/buff-sources.bemap'] = Join-Path $outputs.manifests 'combat/buff-sources.bemap'
}
else {
    $map['model/character-presets.json'] = Join-Path $ws.repo_root 'android/resources/character-presets.json'
    $map['voice/voice-catalog-index.json'] = Join-Path $ws.repo_root 'android/resources/voice-catalog-index.json'
}

Add-Type -AssemblyName System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
try {
    $manifestEntry = $zip.GetEntry('manifest.json')
    if (-not $manifestEntry) {
        throw "Core archive has no manifest.json: $archivePath"
    }
    $reader = New-Object System.IO.StreamReader($manifestEntry.Open())
    try {
        $manifest = $reader.ReadToEnd() | ConvertFrom-Json
    }
    finally {
        $reader.Dispose()
    }

    if ($manifest.kind -ne 'betterendfield-resource-pack' -or $manifest.packageType -ne 'core') {
        throw "Archive is not a core resource package: $archivePath"
    }
    if ($manifest.platform -ne $Platform) {
        throw "Core archive targets $($manifest.platform), not $Platform"
    }
    if ($manifest.version -ne $ExpectedVersion) {
        throw "Core archive is version $($manifest.version), expected $ExpectedVersion"
    }

    $restored = @()
    foreach ($name in $map.Keys) {
        $entry = $zip.GetEntry($name)
        if (-not $entry) {
            throw "Core archive is missing $name"
        }

        # A size mismatch means the archive was produced by a different
        # packager revision than the one this workspace expects. Writing it
        # anyway would surface later as an unexplainable build difference.
        $declared = $manifest.files.PSObject.Properties[$name]
        if (-not $declared) {
            throw "Core manifest does not describe $name"
        }
        if ([int64]$declared.Value -ne $entry.Length) {
            throw "Core archive entry $name is $($entry.Length) bytes, manifest declares $($declared.Value)"
        }

        $target = $map[$name]
        New-Item -ItemType Directory -Path (Split-Path -Parent $target) -Force | Out-Null
        [System.IO.Compression.ZipFileExtensions]::ExtractToFile($entry, $target, $true)
        $restored += [ordered]@{ entry = $name; target = $target; bytes = $entry.Length }
    }
}
finally {
    $zip.Dispose()
}

[ordered]@{
    coreVersion = $manifest.version
    gameVersion = $manifest.gameVersion
    platform = $Platform
    archive = $archivePath
    restored = $restored
} | ConvertTo-Json -Depth 5
