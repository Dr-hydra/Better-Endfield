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
$dotnet = $ws.tools.dotnet
if (-not (Get-Command $python -ErrorAction SilentlyContinue)) {
    throw "Configured Python was not found: $python"
}
if (-not (Get-Command $dotnet -ErrorAction SilentlyContinue)) {
    throw "Configured dotnet was not found: $dotnet"
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
$toolVersionText = & $python (Join-Path $repo 'tools/CustomModel/bem_tool.py') --version
if ($LASTEXITCODE -ne 0 -or ($toolVersionText -join ' ') -notmatch '(\d+\.\d+\.\d+)') {
    throw 'BEM Tools version could not be resolved.'
}
$toolVersion = $Matches[1]
# Creator tools are self-contained at runtime. Build dependencies are explicit.
& $python -c "import PyInstaller, zstandard"
if ($LASTEXITCODE -ne 0) { throw "Install tools/CustomModel/requirements-build.txt into the build Python environment first." }
& $python -m PyInstaller --noconfirm --clean --onedir --console `
    --name BetterEndfieldNext.BemConverter `
    --hidden-import bem_v11 `
    --hidden-import bem_v13 `
    --hidden-import bem_v14 `
    --hidden-import efmi_shapes `
    --distpath (Join-Path $Destination "dist") `
    --workpath (Join-Path $Destination "work") `
    --specpath $Destination `
    (Join-Path $repo "tools\CustomModel\bem_tool.py")
if ($LASTEXITCODE -ne 0) { throw "BEM converter build failed." }
$guiPublishDir = Join-Path $Destination 'gui-publish'
& $dotnet publish (Join-Path $repo 'ui/BetterEndfieldNext.BemTools/BetterEndfieldNext.BemTools.csproj') `
    -c Release -r win-x64 --self-contained true -p:Platform=x64 `
    -p:DebugType=None -p:DebugSymbols=false "-p:PublishDir=$guiPublishDir\" `
    "-p:BEWorkspaceBuildRoot=$($ws.paths.build)" `
    "-p:Version=$toolVersion" "-p:InformationalVersion=$toolVersion"
if ($LASTEXITCODE -ne 0) { throw "BEM creator GUI build failed." }
& (Join-Path $PSScriptRoot 'SignNextRelease.ps1') -Path $guiPublishDir -WorkspaceConfig $WorkspaceConfig
& (Join-Path $PSScriptRoot 'SignNextRelease.ps1') -Path (Join-Path $Destination 'dist\BetterEndfieldNext.BemConverter') -WorkspaceConfig $WorkspaceConfig
$packageArgs = @((Join-Path $repo 'tools\CustomModel\package_toolchain.py'),
    (Join-Path $Destination 'dist\BetterEndfieldNext.BemConverter'),
    '--gui-directory', $guiPublishDir,
    '--archive-backend', $ArchiveBackend)
if ($ws.config_file) { $packageArgs += @('--workspace-config', $ws.config_file) }
& $python @packageArgs
if ($LASTEXITCODE -ne 0) { throw "BEM toolchain packaging failed." }
$toolArchive = Join-Path $Destination 'dist\BEM-Tools-win-x64.zip'
$releaseDir = Get-BEReleaseDirectory -Workspace $ws
New-Item -ItemType Directory -Path $releaseDir -Force | Out-Null
$releaseArchive = Join-Path $releaseDir "BEM-Tools-$toolVersion-win-x64.zip"
if ([System.IO.Path]::GetFullPath($toolArchive) -ne [System.IO.Path]::GetFullPath($releaseArchive)) {
    Copy-Item -LiteralPath $toolArchive -Destination $releaseArchive -Force
}
Write-Host "BEM toolchain ZIP: $releaseArchive"
Write-Host "BEM creator GUI: BEM-Tools/BetterEndfieldNext.BemTools.exe inside the ZIP"
Write-Host "BEM converter: $Destination\dist\BetterEndfieldNext.BemConverter"
