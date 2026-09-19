[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$Databases,
    [Parameter(Mandatory=$true)][string]$Reader,
    [Parameter(Mandatory=$true)][string[]]$Runs,
    [string]$Captures = (Join-Path $env:LOCALAPPDATA 'BetterEndfield\catalog\custom-model\native-probe'),
    [string]$Catalog = '',
    [string]$Evidence = ''
)
$ErrorActionPreference = 'Stop'
$toolsRoot = Split-Path -Parent $PSScriptRoot
$repoRoot = Split-Path -Parent (Split-Path -Parent $toolsRoot)
if (-not $Catalog) { $Catalog = Join-Path $toolsRoot 'catalog' }
if (-not $Evidence) { $Evidence = Join-Path $repoRoot 'artifacts\native-parser\sweep-identities' }
$databaseRoot = (Resolve-Path -LiteralPath $Databases).Path
$readerFile = (Resolve-Path -LiteralPath $Reader).Path
$null = New-Item -ItemType Directory -Path $Evidence -Force
# Process roles separately to bound backend memory; output contains identity
# metadata, not mesh/texture payloads. Never reads creator Mod archives.
foreach ($catalogFile in Get-ChildItem -LiteralPath $Catalog -Filter '*.json' -File) {
    $role = Get-Content -LiteralPath $catalogFile.FullName -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($role.kind -ne 'bem-character-catalog') { continue }
    $character = $role.character_id
    if ($character -notmatch '^chr_[a-z0-9_]+$') { throw 'Invalid character ID' }
    $inputs = Join-Path $databaseRoot "$character\inputs"
    $raw = Join-Path $Evidence "$character.json"
    $log = Join-Path $Evidence "$character.log"
    & dotnet $readerFile $inputs $raw --resource-identities > $log 2>&1
    if ($LASTEXITCODE -ne 0) { throw "Native identity extraction failed for $character; see $log" }
    Write-Host "Read original resource identities: $character"
}
& python (Join-Path $toolsRoot 'import_efmi_identities.py') --catalog $Catalog --databases $databaseRoot --identities $Evidence --captures $Captures --runs @Runs
if ($LASTEXITCODE -ne 0) { throw 'Source identity join failed; see reported missing or conflicting evidence' }
