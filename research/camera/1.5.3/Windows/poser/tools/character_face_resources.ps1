# Shared by the installer and packager. Only reusable face profiles are shipped.
function Get-PoserResourceHash([string]$Path) {
    $stream = [IO.File]::OpenRead($Path)
    $sha = [Security.Cryptography.SHA256]::Create()
    try { return [BitConverter]::ToString($sha.ComputeHash($stream)).Replace('-', '') }
    finally { $sha.Dispose(); $stream.Dispose() }
}

function Assert-PoserResourcePath([string]$Path) {
    $node = $Path
    while ($node) {
        if ((Test-Path -LiteralPath $node) -and
            ((Get-Item -LiteralPath $node -Force).Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Symbolic links / junctions are not supported: $node"
        }
        $node = Split-Path -Path $node -Parent
    }
}

function Get-PoserFaceResources([string]$SourceRoot) {
    $directory = Join-Path $SourceRoot 'resources\character-faces'
    Assert-PoserResourcePath $directory
    if (-not (Test-Path -LiteralPath $directory -PathType Container)) {
        throw 'Missing resources/character-faces. Use the complete source tree or extract the complete installation package.'
    }
    $files = @(Get-ChildItem -LiteralPath $directory -Filter '*.face.json' -File | Sort-Object Name)
    if ($files.Count -eq 0 -or $files.Count -gt 4096) { throw 'Invalid bundled face profile count.' }
    foreach ($file in $files) {
        Assert-PoserResourcePath $file.FullName
        if ($file.Name -cnotmatch '^[a-z0-9_]{1,128}-[a-f0-9]{12}\.face\.json$' -or
            $file.Length -gt 8MB) { throw "Invalid bundled face profile: $($file.Name)" }
        $before = Get-PoserResourceHash $file.FullName
        $profile = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8 | ConvertFrom-Json
        if ($profile.version -ne 1 -or $profile.model -cnotmatch '^[a-z0-9_]{1,128}$' -or
            $profile.source_hash -cnotmatch '^[a-f0-9]{64}$' -or
            $file.Name -cne ($profile.model + '-' + $profile.source_hash.Substring(0, 12) + '.face.json') -or
            $profile.bones -isnot [Array] -or $profile.bones.Count -lt 1 -or $profile.bones.Count -gt 256 -or
            $profile.morphs -isnot [Array] -or $profile.morphs.Count -gt 512) {
            throw "Invalid bundled face profile schema: $($file.Name)"
        }
        if ((Get-PoserResourceHash $file.FullName) -ne $before) { throw 'Face profile changed while reading.' }
        @{ name = $file.Name; source = $file.FullName; hash = $before }
    }
}
