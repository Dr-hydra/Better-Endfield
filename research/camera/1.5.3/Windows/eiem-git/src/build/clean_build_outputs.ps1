$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

try {
    $repoPath = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..\..')).ProviderPath
    $binPath = [IO.Path]::GetFullPath((Join-Path $repoPath 'bin'))
    # Fixed names only. Never clean bin wholesale or follow directory junctions.
    $outputs = [ordered]@{
        'eiem.map' = $false
        'version.res' = $false
        'build' = $true
        'cloth' = $true
        'cloth_asset_obj' = $true
        'cloth_asset_decoder.lib' = $false
        'cloth_bundle_delivery.json' = $false
    }
    if (Test-Path -LiteralPath $binPath) {
        $bin = Get-Item -LiteralPath $binPath -Force
        if (!$bin.PSIsContainer -or ($bin.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Invalid or redirected build output directory: $binPath"
        }
    }
    $targets = @()
    foreach ($output in $outputs.GetEnumerator()) {
        $path = [IO.Path]::GetFullPath((Join-Path $binPath $output.Key))
        if (![string]::Equals([IO.Path]::GetDirectoryName($path), $binPath, [StringComparison]::OrdinalIgnoreCase)) {
            throw "Cleanup target escapes the build output directory: $path"
        }
        if (!(Test-Path -LiteralPath $path)) { continue }
        $target = Get-Item -LiteralPath $path -Force
        if ($target.PSIsContainer -ne $output.Value) { throw "Unexpected output type: $path" }
        # Validate every existing target before deleting any of them. Enumerate
        # one level at a time so even validation cannot traverse a junction.
        $pending = [Collections.Generic.Stack[IO.FileSystemInfo]]::new()
        $pending.Push($target)
        while ($pending.Count) {
            $item = $pending.Pop()
            if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
                throw "Refusing to follow a link in build outputs: $($item.FullName)"
            }
            if ($item -is [IO.DirectoryInfo]) {
                foreach ($child in Get-ChildItem -LiteralPath $item.FullName -Force) { $pending.Push($child) }
            }
        }
        $targets += $path
    }
    foreach ($path in $targets) {
        Remove-Item -LiteralPath $path -Recurse -Force
        if (Test-Path -LiteralPath $path) { throw "Output was not removed: $path" }
    }
    Write-Host '[OK] Build intermediates cleaned.'
    exit 0
} catch {
    Write-Host "[ERROR] Build output cleanup failed: $($_.Exception.Message)"
    exit 1
}
