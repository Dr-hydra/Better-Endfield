$ErrorActionPreference = 'Stop'

# One-time setup: fetch Windows SDK headers/libs as NuGet packages into deps/.
# Needed only on machines without an installed Windows SDK (like this one).
# Run with network access:  powershell -ExecutionPolicy Bypass -File tools\setup_winsdk.ps1

Add-Type -AssemblyName System.IO.Compression.FileSystem

$root = Join-Path $PSScriptRoot '..'
$ver  = '10.0.28000.2705'

function Get-Package([string]$Id, [string]$OutDir) {
  $nupkg = Join-Path $env:TEMP ($Id + '.nupkg')
  Write-Host "Downloading $Id $ver ..."
  Invoke-WebRequest -Uri "https://www.nuget.org/api/v2/package/$Id/$ver" -OutFile $nupkg -UseBasicParsing
  Write-Host "  $((Get-Item $nupkg).Length) bytes"
  if (Test-Path $OutDir) { Remove-Item -LiteralPath $OutDir -Recurse -Force }
  New-Item -ItemType Directory -Force -Path $OutDir | Out-Null
  [System.IO.Compression.ZipFile]::ExtractToDirectory($nupkg, $OutDir)
  Write-Host "  extracted to $OutDir"
}

# Headers (um/shared/ucrt): deps\winsdk\c\Include\<ver>\...
Get-Package 'microsoft.windows.sdk.cpp'    (Join-Path $root 'deps\winsdk')
# x64 libs (um\x64, ucrt\x64): deps\winsdkcpp\c\...
Get-Package 'microsoft.windows.sdk.cpp.x64' (Join-Path $root 'deps\winsdkcpp')

Write-Host ''
Write-Host 'Windows SDK NuGet packages are ready. Run tools\build_msvc.ps1 (or build.bat) to build.'
