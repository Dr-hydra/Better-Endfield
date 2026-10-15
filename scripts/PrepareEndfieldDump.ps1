[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string]$SourceRoot,
    [string]$OutputRoot = '',
    [Alias('workspace-config')][string]$WorkspaceConfig = ''
)

# Reuse the existing extractor and rebuild only its small VFS library. The
# historical source/tool installation is never modified.
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
if (-not $OutputRoot) { $OutputRoot = Join-Path $ws.paths.toolchains 'EndfieldStudio' }
$SourceRoot = [IO.Path]::GetFullPath($SourceRoot)
$OutputRoot = [IO.Path]::GetFullPath($OutputRoot)
if ($SourceRoot.TrimEnd('\') -eq $OutputRoot.TrimEnd('\')) {
    throw 'SourceRoot and OutputRoot must differ; preserve the original tool.'
}
$sourceLibrary = Join-Path $SourceRoot 'AnimeStudio.Endfield'
$sourceRuntime = Join-Path $SourceRoot 'AnimeStudio.Endfield.Cli/bin/Release/net9.0'
$library = Join-Path $OutputRoot 'AnimeStudio.Endfield'
$runtime = Join-Path $OutputRoot 'AnimeStudio.Endfield.Cli/bin/Release/net9.0'
foreach ($required in @(
    (Join-Path $sourceLibrary 'AnimeStudio.Endfield.csproj'),
    (Join-Path $sourceLibrary 'VfsParser.cs'),
    (Join-Path $sourceRuntime 'endfield-dump.exe')
)) {
    if (-not (Test-Path -LiteralPath $required -PathType Leaf)) { throw "Missing tool input: $required" }
}
New-Item -ItemType Directory -Path $library, $runtime -Force | Out-Null
Get-ChildItem -LiteralPath $sourceLibrary -File -Recurse |
    Where-Object { $_.Extension -in @('.cs', '.csproj') -and $_.FullName -notmatch '[\\/](bin|obj)[\\/]' } |
    ForEach-Object {
        $relative = [IO.Path]::GetRelativePath($sourceLibrary, $_.FullName)
        $destination = Join-Path $library $relative
        New-Item -ItemType Directory -Path (Split-Path -Parent $destination) -Force | Out-Null
        Copy-Item -LiteralPath $_.FullName -Destination $destination -Force
    }
Get-ChildItem -LiteralPath $sourceRuntime |
    ForEach-Object { Copy-Item -LiteralPath $_.FullName -Destination $runtime -Recurse -Force }
$parser = Join-Path $library 'VfsParser.cs'
$text = [IO.File]::ReadAllText($parser)
$old = 'int tagRaw = ReadI32Le(span, ref cursor);'
if (($text.Split(@($old), [StringSplitOptions]::None).Length - 1) -ne 2) {
    throw 'Unexpected VfsParser source: expected exactly two v4 tag readers.'
}
$new = $old + "`n                // VFS code 5 appends a 32-bit extension after both tags.`n                if (codeVersion >= 5) _ = ReadI32Le(span, ref cursor);"
[IO.File]::WriteAllText($parser, $text.Replace($old, $new), [Text.UTF8Encoding]::new($false))
Set-BEWorkspaceEnvironment $ws
& $ws.tools.dotnet build (Join-Path $library 'AnimeStudio.Endfield.csproj') -c Release -o $runtime --nologo
if ($LASTEXITCODE -ne 0) { throw 'Endfield VFS code 5 library build failed.' }
Write-Host "Prepared VFS code 5 extractor: $(Join-Path $runtime 'endfield-dump.exe')"
