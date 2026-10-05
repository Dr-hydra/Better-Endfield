[CmdletBinding()]
param(
    [string]$Destination = "",
    [string]$WorkspaceConfig = "",
    [string]$ArchiveBackend = ""
)
$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$repo = $ws.repo_root
$python = $ws.tools.python
if (-not (Get-Command $python -ErrorAction SilentlyContinue)) {
    throw "Configured Python was not found: $python"
}
if ([string]::IsNullOrWhiteSpace($ArchiveBackend)) {
    $ArchiveBackend = if ($ws.tools.archive_backend) {
        $ws.tools.archive_backend
    } else {
        Join-Path $ws.paths.toolchains 'bem-archive-backend\7zip'
    }
}
$ArchiveBackend = [System.IO.Path]::GetFullPath($ArchiveBackend)
& (Join-Path $PSScriptRoot 'PrepareBemArchiveBackend.ps1') `
    -WorkspaceConfig $WorkspaceConfig -ArchiveBackend $ArchiveBackend
if ([string]::IsNullOrWhiteSpace($Destination)) {
    $Destination = Join-Path $ws.paths.build "tools\bem"
}
$Destination = [System.IO.Path]::GetFullPath($Destination)
# Creator tools are self-contained at runtime. Build dependencies are explicit.
& $python -c "import PyInstaller, zstandard"
if ($LASTEXITCODE -ne 0) { throw "Install tools/CustomModel/requirements-build.txt into the build Python environment first." }
& $python -m PyInstaller --noconfirm --clean --onedir --console `
    --name BetterEndfield.BemConverter `
    --hidden-import bem_v11 `
    --hidden-import bem_v13 `
    --hidden-import efmi_shapes `
    --distpath (Join-Path $Destination "dist") `
    --workpath (Join-Path $Destination "work") `
    --specpath $Destination `
    (Join-Path $repo "tools\CustomModel\bem_tool.py")
if ($LASTEXITCODE -ne 0) { throw "BEM converter build failed." }
$packageArgs = @((Join-Path $repo 'tools\CustomModel\package_toolchain.py'),
    (Join-Path $Destination 'dist\BetterEndfield.BemConverter'),
    '--archive-backend', $ArchiveBackend)
if ($ws.config_file) { $packageArgs += @('--workspace-config', $ws.config_file) }
& $python @packageArgs
if ($LASTEXITCODE -ne 0) { throw "BEM toolchain packaging failed." }
$toolArchive = Join-Path $Destination 'dist\BEM-Tools-win-x64.zip'
$releaseDir = Get-BEReleaseDirectory -Workspace $ws
New-Item -ItemType Directory -Path $releaseDir -Force | Out-Null
$toolVersionText = & $python (Join-Path $repo 'tools/CustomModel/bem_tool.py') --version
if ($LASTEXITCODE -ne 0 -or ($toolVersionText -join ' ') -notmatch '(\d+\.\d+\.\d+)') {
    throw 'BEM Tools version could not be resolved.'
}
$releaseArchive = Join-Path $releaseDir "BEM-Tools-$($Matches[1])-win-x64.zip"
if ([System.IO.Path]::GetFullPath($toolArchive) -ne [System.IO.Path]::GetFullPath($releaseArchive)) {
    Copy-Item -LiteralPath $toolArchive -Destination $releaseArchive -Force
}
Write-Host "BEM toolchain ZIP: $releaseArchive"
Write-Host "BEM converter: $Destination\dist\BetterEndfield.BemConverter"
