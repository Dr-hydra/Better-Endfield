param([Parameter(Mandatory = $true)][string]$Destination)
$ErrorActionPreference = 'Stop'
$source = Join-Path (Split-Path $PSScriptRoot -Parent) 'resources\character-faces'
$profiles = @(Get-ChildItem -LiteralPath $source -File -Filter '*.face.json')
if ($profiles.Count -eq 0) { throw 'No character expression profiles found.' }
foreach ($profile in $profiles) {
  $data = Get-Content -LiteralPath $profile.FullName -Raw -Encoding UTF8 | ConvertFrom-Json
  if ($data.version -ne 1 -or -not $data.model -or -not $data.bones -or -not $data.morphs) {
    throw "Invalid character expression profile: $($profile.Name)"
  }
}
New-Item -ItemType Directory -Path $Destination -Force | Out-Null
foreach ($profile in $profiles) {
  Copy-Item -LiteralPath $profile.FullName -Destination (Join-Path $Destination $profile.Name) -Force
}
Write-Host "Copied $($profiles.Count) character expression profiles."
