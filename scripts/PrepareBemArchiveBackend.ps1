[CmdletBinding()]
param(
    [string]$WorkspaceConfig = "",
    [string]$ArchiveBackend = ""
)
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
# Runtime input is retained in toolchains; downloaded installers and extracted
# administrative images are disposable. Tracked third-party source stays in tools.
$ready = if (-not [string]::IsNullOrWhiteSpace($ArchiveBackend)) {
    [System.IO.Path]::GetFullPath($ArchiveBackend)
} elseif ($ws.tools.archive_backend) {
    $ws.tools.archive_backend
} else {
    Join-Path $ws.paths.toolchains 'bem-archive-backend\7zip'
}
if ((Test-Path -LiteralPath (Join-Path $ready '7z.exe') -PathType Leaf) -and
    (Test-Path -LiteralPath (Join-Path $ready '7z.dll') -PathType Leaf) -and
    (Test-Path -LiteralPath (Join-Path $ready 'License.txt') -PathType Leaf)) { return }
$downloadRoot = Join-Path $ws.paths.temp 'downloads\bem-archive-backend'
New-Item -ItemType Directory -Path $downloadRoot -Force | Out-Null
$msi = Join-Path $downloadRoot '7z2603-x64.msi'
if (-not (Test-Path $msi)) {
    Invoke-WebRequest 'https://github.com/ip7z/7zip/releases/download/26.03/7z2603-x64.msi' -OutFile $msi
}
# Administrative extraction only; does not install 7-Zip or change associations.
$stage = Join-Path $ws.paths.temp ('bem-archive-backend\extract-' + [Guid]::NewGuid().ToString('N'))
$job = Start-Process msiexec.exe -ArgumentList @('/a', "`"$msi`"", '/qn', "TARGETDIR=`"$stage`"") -WindowStyle Hidden -Wait -PassThru
if ($job.ExitCode -ne 0) { throw "7-Zip extraction failed: $($job.ExitCode)" }
$exe = Get-ChildItem -LiteralPath $stage -Recurse -Filter '7z.exe' | Select-Object -First 1
if (-not $exe) { throw 'Official archive backend missing 7z.exe' }
New-Item -ItemType Directory -Path $ready -Force | Out-Null
foreach ($name in @('7z.exe', '7z.dll', 'License.txt')) {
    Copy-Item -LiteralPath (Join-Path $exe.DirectoryName $name) -Destination (Join-Path $ready $name) -Force
}
Write-Host "BEM archive backend: $ready"
