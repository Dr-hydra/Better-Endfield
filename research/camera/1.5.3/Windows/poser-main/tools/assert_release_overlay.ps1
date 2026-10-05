# Verify the actual DLL without loading it or executing its initialization code.
param([Parameter(Mandatory = $true)][string]$Dll)

$ErrorActionPreference = 'Stop'
$dllPath = (Resolve-Path -LiteralPath $Dll).Path
$description = [Diagnostics.FileVersionInfo]::GetVersionInfo($dllPath).FileDescription
if ($description -notmatch '\[layered-overlay=([01])\]$') {
  throw "Missing overlay build marker in $dllPath; rebuild before packaging."
}
if ($Matches[1] -ne '0') {
  throw 'Release packages require layered-overlay=0. Disable POSER_ENABLE_LAYERED_OVERLAY and rebuild.'
}
Write-Host 'Release overlay check OK: layered-overlay=0'
