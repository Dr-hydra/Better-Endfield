[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$root = Join-Path $repo 'artifacts\bem-archive-backend'
$ready = Join-Path $root '7zip'
if ((Test-Path (Join-Path $ready '7z.exe')) -and (Test-Path (Join-Path $ready '7z.dll')) -and (Test-Path (Join-Path $ready 'License.txt'))) { return }
New-Item -ItemType Directory -Path $root -Force | Out-Null
$msi = Join-Path $root '7z2603-x64.msi'
if (-not (Test-Path $msi)) {
    Invoke-WebRequest 'https://github.com/ip7z/7zip/releases/download/26.03/7z2603-x64.msi' -OutFile $msi
}
# Administrative extraction only; does not install 7-Zip or change associations.
$stage = Join-Path $root ('extract-' + [Guid]::NewGuid().ToString('N'))
$job = Start-Process msiexec.exe -ArgumentList @('/a', "`"$msi`"", '/qn', "TARGETDIR=`"$stage`"") -WindowStyle Hidden -Wait -PassThru
if ($job.ExitCode -ne 0) { throw "7-Zip extraction failed: $($job.ExitCode)" }
$exe = Get-ChildItem -LiteralPath $stage -Recurse -Filter '7z.exe' | Select-Object -First 1
if (-not $exe) { throw 'Official archive backend missing 7z.exe' }
New-Item -ItemType Directory -Path $ready -Force | Out-Null
foreach ($name in @('7z.exe', '7z.dll', 'License.txt')) {
    Copy-Item -LiteralPath (Join-Path $exe.DirectoryName $name) -Destination (Join-Path $ready $name) -Force
}
Write-Host "BEM archive backend: $ready"
